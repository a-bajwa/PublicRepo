using BerryessaUnion.Domains.UserSetup;
using BerryessaUnion.IServices.JwtServices;
using BerryessaUnion.IServices.RepositoryConfig;
using BerryessaUnion.IServices.UserSetup;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.CommonFunctions;
using SharedLibrary.Extensions;
using SharedLibrary.JwtModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace BerryessaUnion.Managers.JwtManager
{
    public class TokenFactoryManager : ITokenFactoryService
    {
        private readonly ISecurityService _securityService;
        private readonly IOptionsSnapshot<BearerTokensOptions> _configuration;
        private readonly IRoleService _rolesService;
        private readonly IRepositoryWrapperService _manager;
        private readonly ILogger<TokenFactoryManager> _logger;

        public TokenFactoryManager(
            ISecurityService securityService,
            IRoleService rolesService,
             IRepositoryWrapperService manager,
            IOptionsSnapshot<BearerTokensOptions> configuration,
            ILogger<TokenFactoryManager> logger)
        {
            _securityService = securityService;
            _securityService.CheckArgumentIsNull(nameof(_securityService));

            _rolesService = rolesService;
            _rolesService.CheckArgumentIsNull(nameof(rolesService));

            _configuration = configuration;
            _configuration.CheckArgumentIsNull(nameof(configuration));

            _logger = logger;
            _logger.CheckArgumentIsNull(nameof(logger));

            _manager = manager;
            _manager.CheckArgumentIsNull(nameof(manager));
        }

        public async Task<JwtTokensData> CreateJwtTokensAsync(User user)
        {
            var (accessToken, claims) = await createAccessTokenAsync(user);
            var (refreshTokenValue, refreshTokenSerial) = createRefreshToken();


            return new JwtTokensData
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue,
                RefreshTokenSerial = refreshTokenSerial,
                Claims = claims
            };


        }

        private (string RefreshTokenValue, string RefreshTokenSerial) createRefreshToken()
        {
            var refreshTokenSerial = _securityService.CreateCryptographicallySecureGuid().ToString().Replace("-", "");

            var claims = new List<Claim>
            {
                // Unique Id for all Jwt tokes
                new Claim(JwtRegisteredClaimNames.Jti, _securityService.CreateCryptographicallySecureGuid().ToString(), ClaimValueTypes.String, _configuration.Value.Issuer),
                // Issuer
                new Claim(JwtRegisteredClaimNames.Iss, _configuration.Value.Issuer, ClaimValueTypes.String, _configuration.Value.Issuer),
                // Issued at
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _configuration.Value.Issuer),
                // for invalidation
                new Claim(ClaimTypes.SerialNumber, refreshTokenSerial, ClaimValueTypes.String, _configuration.Value.Issuer)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _configuration.Value.Issuer,
                audience: _configuration.Value.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_configuration.Value.RefreshTokenExpirationMinutes),
                signingCredentials: creds);
            var refreshTokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return (refreshTokenValue, refreshTokenSerial);
        }

        public string GetRefreshTokenSerial(string refreshTokenValue)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenValue))
            {
                return null;
            }
            ClaimsPrincipal decodedRefreshTokenPrincipal = null;
            try
            {
                decodedRefreshTokenPrincipal = new JwtSecurityTokenHandler().ValidateToken(
                    refreshTokenValue,
                    new TokenValidationParameters
                    {
                        RequireExpirationTime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.Key)),
                        ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                        ValidateLifetime = true, // validate the expiration
                        ClockSkew = TimeSpan.Zero // tolerance for the expiration date
                    },
                    out _
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to validate refreshTokenValue: `{refreshTokenValue}`.");
            }

            return decodedRefreshTokenPrincipal?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
        }

        private async Task<(string AccessToken, IEnumerable<Claim> Claims)> createAccessTokenAsync(User user)
        {
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.SerialNumber, user.SerialNumber, ClaimValueTypes.String, _configuration.Value.Issuer),
                // Unique Id for all Jwt tokes
                new Claim(JwtRegisteredClaimNames.Jti, _securityService.CreateCryptographicallySecureGuid().ToString(), ClaimValueTypes.String, _configuration.Value.Issuer),
                // Issuer
                new Claim(JwtRegisteredClaimNames.Iss, _configuration.Value.Issuer, ClaimValueTypes.String, _configuration.Value.Issuer),
                // Issued at
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _configuration.Value.Issuer),
                new Claim(CommonConstant.LoginUserClaim.Id, user.Id.ToString(), ClaimValueTypes.String, _configuration.Value.Issuer),
                new Claim(CommonConstant.LoginUserClaim.Name, user.Email, ClaimValueTypes.String, _configuration.Value.Issuer),
                new Claim(CommonConstant.LoginUserClaim.DisplayName, user.DisplayName, ClaimValueTypes.String, _configuration.Value.Issuer),
                //  new Claim(CommonConstant.LoginUserClaim.SerialNumber, user.SerialNumber, ClaimValueTypes.String, _configuration.Value.Issuer),
                new Claim(ClaimTypes.UserData, user.Id.ToString(), ClaimValueTypes.String, _configuration.Value.Issuer),
                new Claim(CommonConstant.LoginUserClaim.EmailConfirmed, user.EmailConfirmed.ToString(), ClaimValueTypes.String, _configuration.Value.Issuer),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _configuration.Value.Issuer,
                audience: _configuration.Value.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_configuration.Value.AccessTokenExpirationMinutes),
                signingCredentials: creds);

            return (new JwtSecurityTokenHandler().WriteToken(token), claims);
        }
    }
}
