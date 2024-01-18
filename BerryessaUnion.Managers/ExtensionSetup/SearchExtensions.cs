using BerryessaUnion.Domains.EmployeeSetup;
using BerryessaUnion.Domains.MenuSetup;
using BerryessaUnion.Domains.UserSetup;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BerryessaUnion.Managers.ExtensionSetup
{
    public static class SearchExtensions
    {
        
        
        
        public static IQueryable<Menu> Search_Menu(this IQueryable<Menu> obj, string searchTearm)
        {
            if (string.IsNullOrWhiteSpace(searchTearm))
            {
                return obj;
            }
            var lowerCaseSearchTerm = searchTearm.Trim().ToLower();
            obj = obj.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm)
            );

            return obj;
        }
        public static IQueryable<User> Search_User(this IQueryable<User> obj, string searchTearm)
        {
            if (string.IsNullOrWhiteSpace(searchTearm))
            {
                return obj;
            }
            var lowerCaseSearchTerm = searchTearm.Trim().ToLower();

            obj = obj.Where(p => p.Email.ToLower().Contains(lowerCaseSearchTerm)
            || p.DisplayName.ToLower().Contains(lowerCaseSearchTerm)
            || p.PhoneNumber.ToLower().Contains(lowerCaseSearchTerm)
            );
            return obj;
        }




        public static IQueryable<User> OrderBy_User(this IQueryable<User> obj, string OrderBy, string OrderDir)
        {
            if (string.IsNullOrWhiteSpace(OrderBy) || string.IsNullOrWhiteSpace(OrderDir))
            {
                return obj;
            }
            OrderDir = OrderDir.ToLower();
            switch (OrderBy)
            {
                case "Id":
                    switch (OrderDir)
                    {
                        case "asc":
                            obj = obj.OrderBy(x => x.Id);
                            break;
                        case "desc":
                            obj = obj.OrderByDescending(x => x.Id);
                            break;
                        default:
                            break;
                    }
                    break;

                case "FirstName":
                    switch (OrderDir)
                    {
                        case "asc":
                            obj = obj.OrderBy(x => x.FirstName);
                            break;
                        case "desc":
                            obj = obj.OrderByDescending(x => x.FirstName);
                            break;
                        default:
                            break;
                    }
                    break;

                case "LastName":
                    switch (OrderDir)
                    {
                        case "asc":
                            obj = obj.OrderBy(x => x.LastName);
                            break;
                        case "desc":
                            obj = obj.OrderByDescending(x => x.LastName);
                            break;
                        default:
                            break;
                    }
                    break;

                case "DisplayName":
                    switch (OrderDir)
                    {
                        case "asc":
                            obj = obj.OrderBy(x => x.DisplayName);
                            break;
                        case "desc":
                            obj = obj.OrderByDescending(x => x.DisplayName);
                            break;
                        default:
                            break;
                    }
                    break;
                case "Email":
                    switch (OrderDir)
                    {
                        case "asc":
                            obj = obj.OrderBy(x => x.Email);
                            break;
                        case "desc":
                            obj = obj.OrderByDescending(x => x.Email);
                            break;
                        default:
                            break;
                    }
                    break;
                case "Address":
                    switch (OrderDir)
                    {
                        case "asc":
                            obj = obj.OrderBy(x => x.Address);
                            break;
                        case "desc":
                            obj = obj.OrderByDescending(x => x.FirstName);
                            break;
                        default:
                            break;
                    }
                    break;
                
                default:
                    break;
            }

            return obj;
        }


        public static IQueryable<Employee> Search_Employees(this IQueryable<Employee> obj, string searchTearm)
        {
            if (string.IsNullOrWhiteSpace(searchTearm))
            {
                return obj;
            }
            var lowerCaseSearchTerm = searchTearm.Trim().ToLower();
            obj = obj.Where(p => p.Email.ToLower().Contains(lowerCaseSearchTerm)
            || p.LastName.ToLower().Contains(lowerCaseSearchTerm)
            || p.FirstName.ToLower().Contains(lowerCaseSearchTerm)
            || p.MiddleInitial.ToLower().Contains(lowerCaseSearchTerm)
            || p.FullName.ToLower().Contains(lowerCaseSearchTerm)
            || p.WorkEmail.ToLower().Contains(lowerCaseSearchTerm)
            || p.PortalEmail.ToLower().Contains(lowerCaseSearchTerm)
            || p.EmployeeID.ToString().ToLower().Contains(lowerCaseSearchTerm) 
            );

            return obj;
        }








        public static IQueryable<Employee> OrderBy_Employees(this IQueryable<Employee> obj, string OrderBy, string OrderDir)
        {
            if (string.IsNullOrWhiteSpace(OrderBy)|| string.IsNullOrWhiteSpace(OrderDir))
            {
                return obj;
            }
            OrderDir=OrderDir.ToLower();
            switch (OrderBy)
            {
                case "Id":
                    switch (OrderDir)
                    {
                        case "asc":
                            obj = obj.OrderBy(x => x.EmployeeID);
                            break;
                        case "desc":
                            obj = obj.OrderByDescending(x => x.EmployeeID);
                            break;
                        default:
                            break;
                    }
                    break;
                case "EmployeeID":
                    switch (OrderDir)
                    {
                        case "asc":
                            obj = obj.OrderBy(x => x.EmployeeID);
                            break;
                        case "desc":
                            obj = obj.OrderByDescending(x => x.EmployeeID);
                            break;
                        default:
                            break;
                    }
                    break;

                case "FirstName":
                    switch (OrderDir)
                    {
                        case "asc":
                            obj = obj.OrderBy(x => x.FirstName);
                            break;
                        case "desc":
                            obj = obj.OrderByDescending(x => x.FirstName);
                            break;
                        default:
                            break;
                    }
                    break;

                case "LastName":
                    switch (OrderDir)
                    {
                        case "asc":
                            obj = obj.OrderBy(x => x.LastName);
                            break;
                        case "desc":
                            obj = obj.OrderByDescending(x => x.LastName);
                            break;
                        default:
                            break;
                    }
                    break;

                case "FullName":
                    switch (OrderDir)
                    {
                        case "asc":
                            obj = obj.OrderBy(x => x.FullName);
                            break;
                        case "desc":
                            obj = obj.OrderByDescending(x => x.FullName);
                            break;
                        default:
                            break;
                    }
                    break;
                case "Email":
                    switch (OrderDir)
                    {
                        case "asc":
                            obj = obj.OrderBy(x => x.Email);
                            break;
                        case "desc":
                            obj = obj.OrderByDescending(x => x.Email);
                            break;
                        default:
                            break;
                    }
                    break;
                case "WorkEmail":
                    switch (OrderDir)
                    {
                        case "asc":
                            obj = obj.OrderBy(x => x.WorkEmail);
                            break;
                        case "desc":
                            obj = obj.OrderByDescending(x => x.WorkEmail);
                            break;
                        default:
                            break;
                    }
                    break;
                case "PortalEmail":
                    switch (OrderDir)
                    {
                        case "asc":
                            obj = obj.OrderBy(x => x.PortalEmail);
                            break;
                        case "desc":
                            obj = obj.OrderByDescending(x => x.PortalEmail);
                            break;
                        default:
                            break;
                    }
                    break;
                //case "State":
                //    switch (OrderDir)
                //    {
                //        case "asc":
                //            obj = obj.OrderBy(x => x.State);
                //            break;
                //        case "desc":
                //            obj = obj.OrderByDescending(x => x.State);
                //            break;
                //        default:
                //            break;
                //    }
                //    break;
                //case "City":
                //    switch (OrderDir)
                //    {
                //        case "asc":
                //            obj = obj.OrderBy(x => x.City);
                //            break;
                //        case "desc":
                //            obj = obj.OrderByDescending(x => x.City);
                //            break;
                //        default:
                //            break;
                //    }
                //    break;
                //case "ZipCode":
                //    switch (OrderDir)
                //    {
                //        case "asc":
                //            obj = obj.OrderBy(x => x.ZipCode);
                //            break;
                //        case "desc":
                //            obj = obj.OrderByDescending(x => x.ZipCode);
                //            break;
                //        default:
                //            break;
                //    }
                //    break;

                default:
                    break;
            }

            return obj;
        }
        public static IQueryable<User> Search_User_Status(this IQueryable<User> obj, Nullable<bool> searchStatue)
        {
            if (searchStatue == null)
            {
                return obj;
            }
            obj = obj.Where(p => p.IsActive == searchStatue);
            return obj;
        }
        
        public static IQueryable<Role> Search_Role(this IQueryable<Role> obj, string searchTearm)
        {
            if (string.IsNullOrWhiteSpace(searchTearm))
            {
                return obj;
            }
            var lowerCaseSearchTerm = searchTearm.Trim().ToLower();
            obj = obj.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm)
            );

            return obj;
        }
        //public static IQueryable<Logs> Search_Logs(this IQueryable<Logs> obj, string searchTearm)
        //{
        //    if (string.IsNullOrWhiteSpace(searchTearm))
        //    {
        //        return obj;
        //    }
        //    var lowerCaseSearchTerm = searchTearm.Trim().ToLower();
        //    obj = obj.Where(p => p.Message.ToLower().Contains(lowerCaseSearchTerm)
        //    || p.MessageTemplate.ToLower().Contains(lowerCaseSearchTerm)
        //    || p.Exception.ToLower().Contains(lowerCaseSearchTerm)
        //    || p.Level.ToLower().Contains(lowerCaseSearchTerm)
        //    );

        //    return obj;
        //}
    }
}
