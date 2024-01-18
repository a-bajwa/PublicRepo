using AutoMapper;
using SharedLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Extensions;
using SharedLibrary.CommonFunctions;
using BerryessaUnion.Dto.EmployeeSetup;
using Microsoft.AspNetCore.Authorization;
using BerryessaUnion.IServices.JwtServices;
using BerryessaUnion.Domains.EmployeeSetup;
using BerryessaUnion.IServices.RepositoryConfig;
using static SharedLibrary.CommonFunctions.CommonEnums;
using Busd_Backend.HosteModel.EmployeeSetup;
using BerryessaUnion.Dto.EmployeeSetup.EmployeeContractSetup;
using BerryessaUnion.Dto.EmployeeSetup.EmployeePaymentSetup;

namespace Busd_Backend.Controllers.EmloyeeSetup
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly IRepositoryWrapperService _manager;
        private readonly ISecurityService _managerSecurity;
        private readonly IMapper _mapper;
        public EmployeesController(
            IRepositoryWrapperService manager,
            ISecurityService securityManager,
            IMapper mapper
            )
        {
            _manager = manager;
            _manager.CheckArgumentIsNull(nameof(manager));

            _managerSecurity = securityManager;
            _managerSecurity.CheckArgumentIsNull(nameof(securityManager));

            _mapper = mapper;
            _manager.CheckArgumentIsNull(nameof(mapper));

        }

        [HttpGet("[action]")]
        public IActionResult GetEmployeeInformation(int Id)
        {
            var _getDetails = _manager.EmployeeServices.GetDetailsById(Id);
            if (_getDetails != null)
            {
                var _mappedResult = _mapper.Map<EmployeeDto>(_getDetails);
                if (_mappedResult.EmployeeJobDetails.Count > 1)
                {
                    _mappedResult.EmployeeJobDetails.RemoveRange(0, (_mappedResult.EmployeeJobDetails.Count - 1));
                }
                var paymente = _manager.EmployeeServices.GetPaymentDetailsByEmployeeId(Id);
                if (paymente != null)
                {
                    _mappedResult.EmployeePayments = _mapper.Map<List<EmployeePaymentDto>>(paymente);
                    if (_mappedResult.EmployeePayments.Count > 1)
                    {
                        _mappedResult.EmployeePayments.RemoveRange(0, (_mappedResult.EmployeePayments.Count - 1));
                    }
                }
                return Ok(_mappedResult);
            }
            else
            {
                return NotFound(CommonFunction.Response(ResponseType.Failure, "Record Not Found"));
            }
        }

        [HttpGet("[action]")]
        public IActionResult GetEmployeeAllPayments(int EmployeeId)
        {
            var _getDetails = _manager.EmployeeServices.GetPaymentDetailsByEmployeeId(EmployeeId);
            if (_getDetails != null)
            {
                var _mappedResult = _mapper.Map<List<EmployeePaymentDto>>(_getDetails);

                return Ok(_mappedResult);
            }
            else
            {
                return NotFound(CommonFunction.Response(ResponseType.Failure, "Record Not Found"));
            }
        }

        //[HttpPost("GetPaginationList")]
        //public IActionResult GetPaginationList([FromBody] DataTablePostModel model)
        //{
        //    var obj = _manager.EmployeeServices.GetPaginationList(EmployeeAssignModel.PaginationAssign(model));
        //    var postMappedObj = _mapper.Map<List<EmployeeDto>>(obj);
        //    return Ok(new DataTableDto
        //    {
        //        Draw = model.draw,
        //        TotalRecords = obj.TotalCount,
        //        FilteredRecords = obj.TotalCount,
        //        Data = postMappedObj
        //    });
        //}


        [HttpPost("[action]")]
        [IgnoreAntiforgeryToken]
        public IActionResult PaginationList([FromBody] PaginationModel paginationModel)
        {
            var obj = _manager.EmployeeServices.GetPaginationList(paginationModel);
            var postMappedObj = _mapper.Map<List<EmployeeDto>>(obj);
            return Ok(new DataTableDto
            {
                Draw = paginationModel.PageSize,
                TotalRecords = obj.TotalCount,
                FilteredRecords = postMappedObj.Count,
                Data = postMappedObj

            });
        }

        [HttpPost("[action]")]
        [IgnoreAntiforgeryToken]
        public IActionResult CreateEmployee([FromBody] EmployeeCerateDto registerUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_manager.EmployeeServices.IsEmail(registerUser.EmployeeData.Email))
            {
                return BadRequest(CommonFunction.Response(ResponseType.Failure, "Email is already registered"));
            }
            long _currentLoginId = CommonFunction.GetCurrentLogin(User.Claims.ToList());
            System.Random random = new System.Random();
            ResponseMessage responseMessage = new ResponseMessage();
            //var _getNewSerial = _manager.EmployeeServices.NewSerialNumber();
            var postMappedObj = _mapper.Map<Employee>(registerUser.EmployeeData);
            postMappedObj.FullName = registerUser.EmployeeData.LastName + ", " + registerUser.EmployeeData.FirstName;
            var result = _manager.EmployeeServices.NewSerialNumber();
            var empId = result + 1;
            postMappedObj.EmployeeID = empId;
            var entityObj = _manager.EmployeeServices.CreateEntity(postMappedObj);
            _manager.Save();
            var ContreactpostMappedObj = _mapper.Map<EmployeeContact>(registerUser.EmployeeContractData);
            ContreactpostMappedObj.EmployeeID = empId;
            var entityjobObj = _manager.EmployeeContactServices.CreateEntity(ContreactpostMappedObj);
            _manager.Save();
            var PaymentpostMappedObj = _mapper.Map<EmployeePayment>(registerUser.EmployeePaymentData);
            PaymentpostMappedObj.EmployeeID = empId;
            var PaymentpostentityObj = _manager.EmployeePaymentServices.CreateEntity(PaymentpostMappedObj);
            _manager.Save();
            var jobdetailpostMappedObj = _mapper.Map<EmployeeJobDetail>(registerUser.EmployeeJobDetailData);
            jobdetailpostMappedObj.EmployeeID = empId;
            var jobdetailpostentityObj = _manager.EmployeeJobDetailServices.CreateEntity(jobdetailpostMappedObj);
            _manager.Save();

            return Ok(CommonFunction.Response(ResponseType.SuccessId, entityObj.EmployeeID.ToString()));
        }


        [HttpDelete("[action]")]
        [IgnoreAntiforgeryToken]
        public IActionResult DeleteEmployee(int EmployeeId)
        {

            ResponseMessage responseMessage = new ResponseMessage();
            var employeedata = _manager.EmployeeServices.GetDetailsById(EmployeeId);
            if (employeedata != null)
            {
                var jobdetail = _manager.EmployeeJobDetailServices.GetDetailsByEmployeeId(EmployeeId);
                _manager.EmployeeServices.Delete(employeedata);
                _manager.Save();
                return Ok(CommonFunction.Response(ResponseType.Success, " "));
            }
            else
            {
                return BadRequest(CommonFunction.Response(ResponseType.Failure, "The user you have provided is not exist"));
            }
        }



        [HttpPost("[action]")]
        [IgnoreAntiforgeryToken]
        public IActionResult CreateEmployeeContact([FromBody] EmployeeContactPostDto model)
        {
            try
            {
                var postMappedObj = _mapper.Map<EmployeeContact>(model);
                var entityObj = _manager.EmployeeContactServices.CreateEntity(postMappedObj);
                _manager.Save();
                return Ok(CommonFunction.Response(ResponseType.SuccessId, entityObj.Id.ToString()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("[action]")]
        public IActionResult GetContractByEmployeeId(int EmployeeId)
        {
            var _getDetails = _manager.EmployeeContactServices.GetDetailsByEmployeeId(EmployeeId);
            if (_getDetails != null)
            {
                var _mappedResult = _mapper.Map<EmployeeContactDto>(_getDetails);
                return Ok(_mappedResult);
            }
            else
            {
                return NotFound(CommonFunction.Response(ResponseType.Failure, "Record Not Found"));
            }
        }



        [HttpPut("[action]")]
        [IgnoreAntiforgeryToken]
        public IActionResult Put([FromBody] EmployeePutDto putModel)
        {
            try
            {
                //_logger.LogInfo("UpdateUser");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (putModel.EmployeeID <= 0 || putModel.EmployeeID == null)
                {
                    return BadRequest(CommonFunction.Response(ResponseType.Failure, "Please Provide Id of user you want to update"));
                }
                long _currentLoginId = CommonFunction.GetCurrentLogin(User.Claims.ToList());
                var _getDetails = _manager.EmployeeServices.GetDetailsById(putModel.EmployeeID);
                var mapObject = EmployeeAssignModel.UpdatedAssignModel(_getDetails, putModel, _currentLoginId);
                var entityModel = _manager.EmployeeServices.UpdateEntity(mapObject);
                _manager.Save();
                var contract = _manager.EmployeeContactServices.GetLastContractByEmployeeId(putModel.EmployeeID);
                if (contract != null)
                {
                    var contractmapobject = EmployeeContractAssignModel.UpdatedAssignModel(contract, putModel.EmployeeContract);
                    var ContractentityModel = _manager.EmployeeContactServices.UpdateEntity(contractmapobject);
                    _manager.Save();
                }
                var jobmodel = _manager.EmployeeJobDetailServices.GetLastJobEmployeeId(putModel.EmployeeID);
                if (jobmodel != null)
                {
                    var jobmapobject = EmployeeJobDetailAssignModel.UpdatedAssignModel(jobmodel, putModel.EmployeeJobDetail);
                    var JobentityModel = _manager.EmployeeJobDetailServices.UpdateEntity(jobmapobject);
                    _manager.Save();
                }

                var paymentmodel = _manager.EmployeePaymentServices.GetLastPaymentEmployeeById(putModel.EmployeeID);
                if (paymentmodel != null)
                {
                    var paymentmapobject = EmployeePaymentAssignModel.UpdatedAssignModel(paymentmodel, putModel.EmployeePayment);
                    var JobentityModel = _manager.EmployeePaymentServices.UpdateEntity(paymentmapobject);
                    _manager.Save();
                }
                return Ok(CommonFunction.Response(ResponseType.SuccessId, entityModel.EmployeeID.ToString()));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //[HttpPost("[action]")]
        //public IActionResult ChangePassword([FromBody] ChangePasswordDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var detailObj = _manager.EmployeeServices.GetDetailsById(model.UserId);
        //    var oldPasswordHash = _managerSecurity.GetSha256Hash(model.OldPassword);
        //    if (detailObj == null)
        //    {
        //        return NotFound(CommonFunction.Response(ResponseType.Failure, "Not Found"));
        //    }
        //    else if (oldPasswordHash != detailObj.PasswordHash)
        //    {
        //        return BadRequest(CommonFunction.Response(ResponseType.Failure, "Please Provide correct current password"));
        //    }
        //    detailObj.PasswordHash = _managerSecurity.GetSha256Hash(model.NewPassword);
        //    detailObj.Password = model.NewPassword;
        //    _manager.EmployeeServices.ChangePassword(detailObj);
        //    return Ok(CommonFunction.Response(ResponseType.Success, "Password has been changed successfully"));
        //}


    }
}

