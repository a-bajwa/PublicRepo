using BerryessaUnion.Domains.UserSetup;
using Microsoft.Extensions.Configuration;
using SharedLibrary.JwtModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.IServices.JwtServices
{
    public interface ITokenFactoryService
    {
        Task<JwtTokensData> CreateJwtTokensAsync(User user);
        string GetRefreshTokenSerial(string refreshTokenValue);
    }
}
