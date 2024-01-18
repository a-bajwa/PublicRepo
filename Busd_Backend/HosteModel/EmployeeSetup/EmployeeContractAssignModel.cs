using BerryessaUnion.Domains.EmployeeSetup;
using BerryessaUnion.Dto.EmployeeSetup;
using BerryessaUnion.Dto.EmployeeSetup.EmployeeContractSetup;

namespace Busd_Backend.HosteModel.EmployeeSetup
{
    public static class EmployeeContractAssignModel
    {
        public static EmployeeContact UpdatedAssignModel(EmployeeContact model, EmployeeContactDto putObj)
        {

            model.State = putObj.State;
            model.Address = putObj.Address;
            model.City = putObj.City;
            model.State = putObj.State;
            model.Zip = putObj.Zip;
            model.MailingAddress = putObj.MailingAddress;
            model.MailingCity = putObj.MailingCity;
            model.MailingState = putObj.MailingState;
            model.MailingZip = putObj.MailingZip;
            model.Phone = putObj.Phone;
            model.WorkPhone = putObj.WorkPhone;
            model.CellPhone = putObj.CellPhone;
            model.StateID = putObj.StateID;
            model.LocalID = putObj.LocalID;
            model.Age = putObj.Age;
            model.MaritalStatus = putObj.MaritalStatus;
            model.Ethnicity = putObj.Ethnicity;
            model.Race = putObj.Race;
            model.Languages = putObj.Languages;
            return model;
        }
    }
}
