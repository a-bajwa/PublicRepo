using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static SharedLibrary.CommonFunctions.CommonEnums;
using static System.Net.Mime.MediaTypeNames;

namespace SharedLibrary.CommonFunctions
{
    public static class CommonFunction
    {
        public static ResponseMessage Response(ResponseType responseType, string message)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            if (responseType == ResponseType.Success)
            {
                responseMessage.StatusCode = 200;
                responseMessage.StatusTitle = "success";
                responseMessage.StatusMessage = "Action Perform Successfully, " + message;
            }
            else if (responseType == ResponseType.Failure)
            {
                responseMessage.StatusCode = 400;
                responseMessage.StatusTitle = "BadRequest";
                responseMessage.StatusMessage = message;
            }
            else if (responseType == ResponseType.SuccessId)
            {
                responseMessage.StatusCode = 200;
                responseMessage.StatusTitle = "success";
                responseMessage.StatusMessage = "Action Perform Successfully";
                responseMessage.Id = long.Parse(message);
            }
            return responseMessage;
        }
        public static DateTime GetPakDateTime()
        {
            DateTime date;
            date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time"));
            return date;
        }


        public static long GetCurrentLogin(List<Claim> listClaims)
        {
            long Id = 0;
            if (listClaims.Count > 0 && listClaims!=null)
            {
                var cliamUserId = listClaims[4].Value;
                string loginUserId = listClaims.Where(c => c.Type == "Id").Select(x => x.Value).FirstOrDefault();
                if (!String.IsNullOrEmpty(loginUserId))
                {
                    Id = long.Parse(loginUserId);
                }
            }
            return Id;
        }

        public static ClaimsPrincipal ClaimsUpdate_PermissionType(ClaimsIdentity identity, string key, string keyValue)
        {
            Claim claim = identity.FindFirst(key);
            if (claim != null)
            {
                identity.RemoveClaim(claim);
            }

            identity.AddClaim(new Claim(key, keyValue));

            var userPrincipal = new ClaimsPrincipal(new[] { identity });

            return userPrincipal;
        }



        //public static ClaimDetailModel GetClaimDetail(List<Claim> listClaims)
        //{

        //    ClaimDetailModel model = new ClaimDetailModel();
        //    if (listClaims.Count > 0)
        //    {
        //        model.UserId = Guid.Parse(listClaims.Where(c => c.Type == ClaimTypes.UserData).Select(x => x.Value).FirstOrDefault());
        //        model.CompanyId = Guid.Parse(listClaims.Where(c => c.Type == CommonConstant.ApiClaims.CompanyId).Select(x => x.Value).FirstOrDefault());
        //        model.DesignatonId = Guid.Parse(listClaims.Where(c => c.Type == CommonConstant.ApiClaims.DesignationId).Select(x => x.Value).FirstOrDefault());
        //    }
        //    return model;
        //}


        public static string ExceptionMessage(Exception ex)
        {
            string violationMessage = String.Empty;
            var message = ex.Message;
            var innerException = ex.InnerException;
            while (innerException != null)
            {
                message = innerException.Message;
                innerException = innerException.InnerException;
            }
            bool PrimaryKey = message.Contains("Violation of PRIMARY KEY");
            bool ForginKey = message.Contains("REFERENCE");
            bool UniqueKey = message.Contains("UNIQUE KEY");
            if (PrimaryKey || UniqueKey)
            {
                violationMessage = "This Record is already added in Database.";
            }
            else
            {
                string[] arr = message.Split('.');
                if (arr.Length > 0)
                {
                    violationMessage = arr[0];
                }
            }
            return violationMessage;
        }

        public static string GenerateRandomNo()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            //Generate your random number
            int code = random.Next(0, 999999);
            //Output the random number including leading zeroes (it should be a string if you want the leadings zeros)
            return code.ToString("D6");
        }
        //public static Stream ToStream(this Image image)
        //{
        //    try
        //    {
        //        var stream = new MemoryStream();
        //        image.Save(stream, ImageFormat.Png);
        //        stream.Position = 0;
        //        return stream;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}


        public static DateTime ConvertDateTimeUItoAPI(string _date)
        {
            string _dateFormat = "dd-MM-yyyy";
            DateTime date;
            date = DateTime.ParseExact(_date, _dateFormat, CultureInfo.InvariantCulture);
            return date;
        }


        public static int GetValueOfEnumByName<T>(string enumName)
        {
            return (int)Enum.Parse(typeof(T), enumName, true);
        }

        public static string GetNameOfEnumByValue<T>(int enumValue)
        {
            return Enum.GetName(typeof(T), enumValue);
        }


        public static TAttribute GetEnumDispalyAttribute<TAttribute>(Enum enumValue) where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }


        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return (T)attributes.FirstOrDefault();//attributes.Length > 0 ? (T)attributes[0] : null;
        }


        public static string ToDisplayName(this Enum value)
        {
            var attribute = value.GetAttribute<DisplayAttribute>();
            return attribute == null ? value.ToString() : attribute.Name;
        }


        public static string CreateRandomPassword()
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&*?";
            Random random = new Random();
            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[8];
            for (int i = 0; i < 8; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        public static string ToDescription(this Enum value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }
        public static void Log(string _logMessage)
        {
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "Log_" + DateTime.Now.ToString("MM-dd-yyyy") + ".txt", string.Format("{0}{1}", DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss") + "==>" + _logMessage, Environment.NewLine));
        }
        //public static LoggingHistory LoggingHistory(LoggingModel model)
        //{
        //    LoggingHistory loggingHistory = new LoggingHistory()
        //    {
        //        Id = new Guid(),
        //        BranchId = model.BranchId,
        //        CompanyId = model.CompanyId,
        //        Controller = model.Controller,
        //        Description = model.Description,
        //        Method = model.Method,
        //        ExceptionMessage = model.ExceptionMessage,
        //        CreatedAt = GetPakDateTime(),
        //        CreatedBy = model.CreatedBy

        //    };
        //    return loggingHistory;
        //}
        

        
    }
}
