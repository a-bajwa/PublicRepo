using AutoMapper;
using BerryessaUnion.Domains.UserSetup;
using BerryessaUnion.Dto.UserRoles;
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
    public class RolesController : ControllerBase
    {
        private readonly IRepositoryWrapperService _manager;
        private readonly ISecurityService _managerSecurity;
        //private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        //private readonly IUploadService _uploadManager;
        //private readonly IEmailkitService _emailkitManager;
        private readonly IOptionsSnapshot<ApiSettings> _configuration;

        public RolesController(
            //ILoggerService logger,
            IRepositoryWrapperService manager,
            ISecurityService securityManager,
            IMapper mapper,
            //IEmailkitService emailkitService,
            //IUploadService uploadManager,
            IOptionsSnapshot<ApiSettings> configuration
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

            //_uploadManager = uploadManager;
            //_uploadManager.CheckArgumentIsNull(nameof(uploadManager));
            //_emailkitManager = emailkitService;

            //_emailkitManager.CheckArgumentIsNull(nameof(emailkitService));


        }
        [HttpGet("[action]")]
        public IActionResult GetRoleList()
        {
            var _getDetailsList = _manager.UsersRepoService.GetRoleList();
            return Ok(_getDetailsList);
        }


        [HttpGet("[action]")]
        public IActionResult GetRoleByRoleId(long RoleId)
        {
            try
            {
                var _getDetailsList = _manager.RoleRepoServices.GetRoleByRoleId(RoleId);
                if (_getDetailsList == null)
                    return NotFound(CommonFunction.Response(ResponseType.Failure, "No record found"));
                else
                    return Ok(_getDetailsList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("[action]")]
        public IActionResult GetRoleByUserId(long UserId)
        {
            try
            {
                var _getDetailsList = _manager.RoleRepoServices.GetRoleByUserId(UserId);
                if (_getDetailsList==null)
                    return NotFound(CommonFunction.Response(ResponseType.Failure, "No role has been assigned to this user"));
                else
                    return Ok(_getDetailsList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpPost("[action]")]
        public IActionResult CreateRole([FromBody] RolePostDto Role)
        {
            try
            {
                var postMappedObj = _mapper.Map<Role>(Role);
                var entityObj = _manager.RoleRepoServices.CreateEntity(postMappedObj);
                _manager.Save();
                return Ok(CommonFunction.Response(ResponseType.SuccessId, entityObj.Id.ToString()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPatch("[action]")]
        public IActionResult UpdateUserRole(long UserId, long RoleId)
        {
            try
            {
                long _currentLoginId = CommonFunction.GetCurrentLogin(User.Claims.ToList());
                var _getDetails = _manager.UsersRepoService.GetDetailsById(UserId);
                if (_getDetails != null)
                {
                    var updateObject = UserAssignModel.UpdatedRoleAssignModel(_getDetails, RoleId, _currentLoginId);
                    var entityModel = _manager.UsersRepoService.UpdateEntity(updateObject);
                    _manager.Save();
                    return Ok(CommonFunction.Response(ResponseType.Success, ""));
                }
                else
                {
                    return BadRequest(CommonFunction.Response(ResponseType.Failure, "Role is not updated"));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
