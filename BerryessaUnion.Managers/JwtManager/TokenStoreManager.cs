﻿using Entity;
using SharedLibrary.JwtModel;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using BerryessaUnion.Domains.UserSetup;
using BerryessaUnion.IServices.JwtServices;

namespace BerryessaUnion.Managers.JwtManager
{
    public class TokenStoreManager : ITokenStoreService
    {
        private readonly ISecurityService _securityService;
        private readonly IUnitOfWork _uow;
        private readonly DbSet<UserToken> _tokens;
        private readonly IOptionsSnapshot<BearerTokensOptions> _configuration;
        private readonly ITokenFactoryService _tokenFactoryService;
        public TokenStoreManager(
            IUnitOfWork uow,
            ISecurityService securityService,
            IOptionsSnapshot<BearerTokensOptions> configuration,
            ITokenFactoryService tokenFactoryService)
        {
            _uow = uow;

            _securityService = securityService;

            _tokens = _uow.Set<UserToken>();

            _configuration = configuration;

            _tokenFactoryService = tokenFactoryService;
        }

        public async Task AddUserTokenAsync(UserToken userToken)
        {
            if (!_configuration.Value.AllowMultipleLoginsFromTheSameUser)
            {
                await InvalidateUserTokensAsync(userToken.UserId);
            }
            await DeleteTokensWithSameRefreshTokenSourceAsync(userToken.RefreshTokenIdHashSource);
            _tokens.Add(userToken);
        }

        public async Task AddUserTokenAsync(User user, string refreshTokenSerial, string accessToken, string refreshTokenSourceSerial)
        {
            var now = DateTimeOffset.UtcNow;
            var token = new UserToken
            {
                UserId = user.Id,
                // Refresh token handles should be treated as secrets and should be stored hashed
                RefreshTokenIdHash = _securityService.GetSha256Hash(refreshTokenSerial),
                RefreshTokenIdHashSource = string.IsNullOrWhiteSpace(refreshTokenSourceSerial) ?
                                           null : _securityService.GetSha256Hash(refreshTokenSourceSerial),
                AccessTokenHash = _securityService.GetSha256Hash(accessToken),
                RefreshTokenExpiresDateTime = now.AddMinutes(_configuration.Value.RefreshTokenExpirationMinutes),
                AccessTokenExpiresDateTime = now.AddMinutes(_configuration.Value.AccessTokenExpirationMinutes)
            };
            await AddUserTokenAsync(token);
        }

        public async Task DeleteExpiredTokensAsync()
        {
            var now = DateTimeOffset.UtcNow;
            await _tokens.Where(x => x.RefreshTokenExpiresDateTime < now)
                        .ForEachAsync(userToken => _tokens.Remove(userToken));
        }

        public async Task DeleteTokenAsync(string refreshTokenValue)
        {
            var token = await FindTokenAsync(refreshTokenValue);
            if (token != null)
            {
                _tokens.Remove(token);
            }
        }

        public async Task DeleteTokensWithSameRefreshTokenSourceAsync(string refreshTokenIdHashSource)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenIdHashSource))
            {
                return;
            }
            var findIdHash = _tokens.Where(t => t.RefreshTokenIdHash == refreshTokenIdHashSource).FirstOrDefault();
            var findIdHashSource = _tokens.Where(t => t.RefreshTokenIdHashSource == refreshTokenIdHashSource).FirstOrDefault();
            if (findIdHash != null)
            {
                await _tokens.Where(t => t.RefreshTokenIdHash == refreshTokenIdHashSource)
                                .ForEachAsync(userToken => _tokens.Remove(userToken));
            }
            else if (findIdHashSource != null)
            {
                await _tokens.Where(t => t.RefreshTokenIdHashSource == refreshTokenIdHashSource)
                               .ForEachAsync(userToken => _tokens.Remove(userToken));
            }
            else
            {
                await _tokens.Where(t => t.RefreshTokenIdHashSource == refreshTokenIdHashSource || t.RefreshTokenIdHash == refreshTokenIdHashSource && t.RefreshTokenIdHashSource == null)
                .ForEachAsync(userToken => _tokens.Remove(userToken));
            }



        }

        public async Task RevokeUserBearerTokensAsync(string userIdValue, string refreshTokenValue)
        {
            if (!string.IsNullOrWhiteSpace(userIdValue) && long.TryParse(userIdValue, out long userId))
            {
                if (_configuration.Value.AllowSignoutAllUserActiveClients)
                {
                    await InvalidateUserTokensAsync(userId);
                }
            }

            if (!string.IsNullOrWhiteSpace(refreshTokenValue))
            {
                var refreshTokenSerial = _tokenFactoryService.GetRefreshTokenSerial(refreshTokenValue);
                if (!string.IsNullOrWhiteSpace(refreshTokenSerial))
                {
                    var refreshTokenIdHashSource = _securityService.GetSha256Hash(refreshTokenSerial);
                    await DeleteTokensWithSameRefreshTokenSourceAsync(refreshTokenIdHashSource);
                }
            }

            await DeleteExpiredTokensAsync();
        }

        public Task<UserToken> FindTokenAsync(string refreshTokenValue)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenValue))
            {
                return Task.FromResult<UserToken>(null);
            }

            var refreshTokenSerial = _tokenFactoryService.GetRefreshTokenSerial(refreshTokenValue);
            if (string.IsNullOrWhiteSpace(refreshTokenSerial))
            {
                return Task.FromResult<UserToken>(null);
            }

            var refreshTokenIdHash = _securityService.GetSha256Hash(refreshTokenSerial);
            return _tokens.Include(x => x.User).FirstOrDefaultAsync(x => x.RefreshTokenIdHash == refreshTokenIdHash);
        }

        public async Task InvalidateUserTokensAsync(long userId)
        {
            var result = await _tokens.Where(x => x.UserId == userId).ToListAsync();
            await _tokens.Where(x => x.UserId == userId)
                        .ForEachAsync(userToken => _tokens.Remove(userToken));
        }

        public async Task<bool> IsValidTokenAsync(string accessToken, long userId)
        {
            var accessTokenHash = _securityService.GetSha256Hash(accessToken);
            var userToken = await _tokens.FirstOrDefaultAsync(
                x => x.AccessTokenHash == accessTokenHash && x.UserId == userId);
            return userToken?.AccessTokenExpiresDateTime >= DateTimeOffset.UtcNow;
        }
    }
}
