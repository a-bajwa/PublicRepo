using Entity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BerryessaUnion.Dto.UsersSetup;
using BerryessaUnion.Domains.UserSetup;
using BerryessaUnion.IServices.JwtServices;
using BerryessaUnion.IServices.UserSetup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using SharedLibrary.CommonFunctions;
using SharedLibrary.Extensions;
using SharedLibrary.Models;
using System.Security.Claims;
using BerryessaUnion.IServices.RepositoryConfig;
using static SharedLibrary.CommonFunctions.CommonEnums;

namespace Busd_Backend.Controllers.UserSetup
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private readonly IRoleService _rolesService;
        private readonly IUsersService _usersManager;
        private readonly ISecurityService _managerSecurity;
        private readonly IRepositoryWrapperService _manager;
        private readonly IAntiForgeryCookieService _antiforgery;
        private readonly ITokenStoreService _tokenStoreManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ITokenFactoryService _tokenFactoryManager;
        private readonly IOptionsSnapshot<ApiSettings> _apiconfiguration;
        public AccountsController(
            IMapper mapper,
            IUnitOfWork uow,
            IRoleService rolesService,
            IUsersService usersService,
            ISecurityService securityManager,
            IRepositoryWrapperService manager,
            ITokenStoreService tokenStoreService,
            IAntiForgeryCookieService antiforgery,
            IWebHostEnvironment hostingEnvironment,
            ITokenFactoryService tokenFactoryService,
            IOptionsSnapshot<ApiSettings> apiconfiguration,
            IConfiguration configuration
            )
        {
            _manager = manager;
            _manager.CheckArgumentIsNull(nameof(manager));

            _usersManager = usersService;
            _usersManager.CheckArgumentIsNull(nameof(usersService));

            _tokenStoreManager = tokenStoreService;
            _tokenStoreManager.CheckArgumentIsNull(nameof(tokenStoreService));

            _rolesService = rolesService;
            _rolesService.CheckArgumentIsNull(nameof(rolesService));
          
            _uow = uow;
            _uow.CheckArgumentIsNull(nameof(_uow));

            _antiforgery = antiforgery;
            _antiforgery.CheckArgumentIsNull(nameof(antiforgery));

            _managerSecurity = securityManager;
            _managerSecurity.CheckArgumentIsNull(nameof(securityManager));

            _tokenFactoryManager = tokenFactoryService;
            _tokenFactoryManager.CheckArgumentIsNull(nameof(tokenFactoryService));

            _configuration = configuration;
            _configuration.CheckArgumentIsNull(nameof(configuration));

            _apiconfiguration = apiconfiguration;
            _apiconfiguration.CheckArgumentIsNull(nameof(apiconfiguration));

            //_logger = logger;
            //_logger.CheckArgumentIsNull(nameof(logger));

            _mapper = mapper;
            _mapper.CheckArgumentIsNull(nameof(mapper));

            _hostingEnvironment = hostingEnvironment;
            if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
            {
                _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
        }


        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginUser)
        {
            CommonFunction.Log("Login IN");
            //_logger.LogInfo("Login");
            ResponseMessage responseMessage = new ResponseMessage();
            if (loginUser.Email == null || loginUser.Password == null)
            {
                responseMessage.StatusCode = 400;
                responseMessage.StatusTitle = "BadRequest";
                responseMessage.StatusMessage = "Email and password is missing";
                //_logger.LogInfo("BadRequest" + responseMessage);
                CommonFunction.Log("BadRequest");
                return BadRequest(responseMessage);
            }
            var user = await _usersManager.FindUserAsync(loginUser.Email, loginUser.Password);
            CommonFunction.Log("user");
            if (user == null)
            {
                responseMessage.StatusCode = 401;
                responseMessage.StatusTitle = "Unauthorized";
                responseMessage.StatusMessage = "Email or password is incorrect";
                //_logger.LogInfo("Unauthorized" + responseMessage);
                CommonFunction.Log("Unauthorized");
                return Unauthorized(responseMessage);
            }
            else if (user?.EmailConfirmed == false)
            {
                responseMessage.StatusCode = 403;
                responseMessage.StatusTitle = "In-Activate";
                responseMessage.StatusMessage = "This Email is not verified. Please Verify your email and then try to login";
                //_logger.LogInfo("Inactivate" + responseMessage);
                CommonFunction.Log("Inactivate");
                return Unauthorized(responseMessage);
            }

            var result = await _tokenFactoryManager.CreateJwtTokensAsync(user);
            CommonFunction.Log("CreateJwtTokensAsync");
            await _tokenStoreManager.AddUserTokenAsync(user, result.RefreshTokenSerial, result.AccessToken, null);
            CommonFunction.Log("AddUserTokenAsync");
            await _uow.SaveChangesAsync();
            _antiforgery.RegenerateAntiForgeryCookies(result.Claims);
            CommonFunction.Log("RegenerateAntiForgeryCookies");
            LoginResponseDto responseDto = new LoginResponseDto();
            responseDto.access_token = result.AccessToken;
            responseDto.refresh_token = result.RefreshToken;
            CommonFunction.Log("responseDto");
            //_logger.LogInfo("Ok" + responseDto);
            CommonFunction.Log("Login Out");



            return Ok(responseDto);
        }
        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<bool> Logout(string refreshToken)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userIdValue = claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value;

            // The Jwt implementation does not support "revoke OAuth token" (logout) by design.
            // Delete the user's tokens from the database (revoke its bearer token)
            await _tokenStoreManager.RevokeUserBearerTokensAsync(userIdValue, refreshToken);
            await _uow.SaveChangesAsync();

            _antiforgery.DeleteAntiForgeryCookies();

            return true;
        }

        // [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [HttpPost("[action]")]
        public IActionResult CreateUser([FromBody] UserPostDto registerUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_manager.UsersRepoService.IsEmail(registerUser.Email))
            {
                return BadRequest(CommonFunction.Response(ResponseType.Failure, "Email is already registered"));
            }
            if (registerUser.RoleId <= 0)
            {
                return BadRequest(CommonFunction.Response(ResponseType.Failure, "Please Provide Valid Role for the user"));
            }
            long _currentLoginId = CommonFunction.GetCurrentLogin(User.Claims.ToList());
            System.Random random = new System.Random();
            ResponseMessage responseMessage = new ResponseMessage();
            var _getNewSerial = _manager.UsersRepoService.NewSerialNumber();
            registerUser.DisplayName = registerUser.FirstName + " " + registerUser.LastName;
            registerUser.PhoneNumber = registerUser.PhoneNumber;
            var postMappedObj = _mapper.Map<User>(registerUser);
            postMappedObj.SrNo = _getNewSerial.SrNo;
            postMappedObj.Code = _getNewSerial.Code;
            postMappedObj.PasswordHash = _managerSecurity.GetSha256Hash(registerUser.Password);
            postMappedObj.EmailConfirmed = true;
            postMappedObj.IsActive = true;
            postMappedObj.CreatedAt = DateTime.Now;
            postMappedObj.CreatedBy = _currentLoginId;
            postMappedObj.ImagePath = "dummy image path here";
            postMappedObj.ThumbNailPath = "dummy thumbnail path";
            postMappedObj.SerialNumber = random.Next().ToString();
            postMappedObj.IsPasswordReset = false;
            postMappedObj.VerificationCode = CommonFunction.GenerateRandomNo();
            var entityObj = _manager.UsersRepoService.CreateEntity(postMappedObj);
            _manager.Save();
            return Ok(CommonFunction.Response(ResponseType.SuccessId, entityObj.Id.ToString()));
        }




    }
}
