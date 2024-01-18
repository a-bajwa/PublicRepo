using AutoMapper;
using BerryessaUnion.Dto.UserRoles;
using BerryessaUnion.Dto.UsersSetup;
using BerryessaUnion.IServices.JwtServices;
using BerryessaUnion.IServices.RepositoryConfig;
using Busd_Backend.HosteModel.UserSetup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SharedLibrary.CommonFunctions;
using SharedLibrary.Extensions;
using SharedLibrary.Models;
using static SharedLibrary.CommonFunctions.CommonEnums;

namespace Busd_Backend.Controllers.UserSetup
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryWrapperService _manager;
        private readonly ISecurityService _managerSecurity;
        //private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IOptionsSnapshot<ApiSettings> _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public UsersController(
            
            IRepositoryWrapperService manager,
            ISecurityService securityManager,
            IMapper mapper,
            IOptionsSnapshot<ApiSettings> configuration,
            IWebHostEnvironment hostingEnvironment
            )
        {
            _manager = manager;
            _manager.CheckArgumentIsNull(nameof(manager));

            _managerSecurity = securityManager;
            _managerSecurity.CheckArgumentIsNull(nameof(securityManager));

            _mapper = mapper;
            _manager.CheckArgumentIsNull(nameof(mapper));

            //_logger = logger;
            //_logger.CheckArgumentIsNull(nameof(logger));

            _configuration = configuration;
            _configuration.CheckArgumentIsNull(nameof(configuration));

            _hostingEnvironment = hostingEnvironment;
            if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
            {
                _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
        }


        [HttpGet("[action]")]
        public IActionResult GetUserInformation(long Id)
        {
            var _getDetails = _manager.UsersRepoService.GetDetailsById(Id);
            if (_getDetails != null)
            {
                var _mappedResult = _mapper.Map<UserDto>(_getDetails);
                return Ok(_mappedResult);
            }
            else
            {
                return NotFound(CommonFunction.Response(ResponseType.Failure, "Record Not Found"));
            }
        }


        [HttpPost("[action]")]
        [IgnoreAntiforgeryToken]
        public IActionResult PaginationList([FromBody] PaginationModel paginationModel)
        {
            var obj = _manager.UsersRepoService.GetPaginationList(paginationModel);
            var postMappedObj = _mapper.Map<List<UserDto>>(obj);
            return Ok(new UserDataTableDto
            {
                Draw = paginationModel.PageSize,
                TotalRecords = obj.TotalCount,
                FilteredRecords = postMappedObj.Count,
                Data = postMappedObj
            });
        }

        [HttpPut("[action]")]
        public IActionResult Put([FromBody] UserPutDto putModel)
        {
            //_logger.LogInfo("UpdateUser");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            long _currentLoginId = CommonFunction.GetCurrentLogin(User.Claims.ToList());
            var _getDetails = _manager.UsersRepoService.GetDetailsById(putModel.Id);
            var mapObject = UserAssignModel.UpdatedAssignModel(_getDetails, putModel, _currentLoginId);
            var entityModel = _manager.UsersRepoService.UpdateEntity(mapObject);
            _manager.Save();
            return Ok(CommonFunction.Response(ResponseType.SuccessId, entityModel.Id.ToString()));
        }

        [HttpPost("[action]")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var detailObj = _manager.UsersRepoService.GetDetailsById(model.UserId);
            var oldPasswordHash = _managerSecurity.GetSha256Hash(model.OldPassword);
            if (detailObj == null)
            {
                return NotFound(CommonFunction.Response(ResponseType.Failure, "Not Found"));
            }
            else if (oldPasswordHash != detailObj.PasswordHash)
            {
                return BadRequest(CommonFunction.Response(ResponseType.Failure, "Please Provide correct current password"));
            }
            detailObj.PasswordHash = _managerSecurity.GetSha256Hash(model.NewPassword);
            detailObj.Password = model.NewPassword;
            _manager.UsersRepoService.ChangePassword(detailObj);
            return Ok(CommonFunction.Response(ResponseType.Success, "Password has been changed successfully"));
        }
    }
}
