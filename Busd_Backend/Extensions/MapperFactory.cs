using AutoMapper;
using BerryessaUnion.Domains.EmployeeSetup;
using BerryessaUnion.Domains.UserSetup;
using BerryessaUnion.Dto.EmployeeSetup;
using BerryessaUnion.Dto.EmployeeSetup.EmployeeContractSetup;
using BerryessaUnion.Dto.EmployeeSetup.EmployeeJobDetailDetup;
using BerryessaUnion.Dto.EmployeeSetup.EmployeePaymentSetup;
using BerryessaUnion.Dto.UserRoles;
using BerryessaUnion.Dto.UsersSetup;

namespace Busd_Backend.Extensions
{
    public static class MapperFactory
    {
        public static IMapper CreateMapper()
        {
            var cfg = new MapperConfiguration(config =>
            {
                //typeof(MapperFactory).Assembly.MapTypes(config);


                #region User_Mapper_Region
                config.CreateMap<User, UserPostDto>().ReverseMap();
                config.CreateMap<User, UserPutDto>().ReverseMap();
                config.CreateMap<User, UserDto>().ReverseMap();
                #endregion



                #region user_role
                config.CreateMap<UserRole, UserRoleDto>().ReverseMap();
                config.CreateMap<UserRole, UserRolePostDto>().ReverseMap();
                config.CreateMap<UserRole, UserRolePutDto>().ReverseMap();
                #endregion


                #region Employee
                config.CreateMap<Employee, EmployeePostDto>().ReverseMap();
                config.CreateMap<Employee, EmployeePutDto>().ReverseMap();
                config.CreateMap<Employee, EmployeeDto>().ReverseMap();
                config.CreateMap<EmployeeContactDto, EmployeeDto>().ReverseMap();
                config.CreateMap<EmployeeContactDto, EmployeeDto>().ReverseMap();
                config.CreateMap<EmployeeJobDetailDto, EmployeeDto>().ReverseMap();
                config.CreateMap<EmployeePaymentDto, EmployeeDto>().ReverseMap();
                #endregion



                #region EmployeeContact
                config.CreateMap<EmployeeContact, EmployeeContactPostDto>().ReverseMap();
                config.CreateMap<EmployeeContact, EmployeeContactDto>().ReverseMap();
                #endregion
                #region EmployeeJobDetail
                config.CreateMap<EmployeeJobDetail, EmployeeJobDetailDto>().ReverseMap();
                config.CreateMap<EmployeeJobDetail, EmployeeJobDetailPostDto>().ReverseMap();
                config.CreateMap<EmployeeJobDetail, EmployeeJobDetailPostDto>().ReverseMap();
                #endregion
                #region EmployeepaymentDetail
                config.CreateMap<EmployeePayment, EmployeePaymentDto>().ReverseMap();
                config.CreateMap<EmployeePayment, EmployeePaymentPostDto>().ReverseMap();
                #endregion

            }).CreateMapper();
            return cfg;
        }
    }
}
