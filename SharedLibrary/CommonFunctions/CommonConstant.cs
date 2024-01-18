using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.CommonFunctions
{
    public static class CommonConstant
    {
        public static class LoginUserClaim
        {
            public const string Id = nameof(Id);
            public const string RoleId = nameof(RoleId);
            public const string Name = nameof(Name);
            public const string DisplayName = nameof(DisplayName);
            public const string SerialNumber = nameof(SerialNumber);
            public const string UserData = nameof(UserData);
            public const string CompanyId = nameof(CompanyId);
            public const string EmailConfirmed = nameof(EmailConfirmed);
            public const string CompanyName = nameof(CompanyName);
            public const string BranchId = nameof(BranchId);
            public const string Role = nameof(Role);
            public const string Currency = nameof(Currency);
            public const string DesignationId = nameof(DesignationId);
            public const string DesignationName = nameof(DesignationName);
            public const string DesignationPermission = nameof(DesignationPermission);
        }
        public static class ApiClaims
        {
            public const string DisplayName = nameof(DisplayName);
            public const string UserData = nameof(UserData);
            public const string CompanyId = nameof(CompanyId);
            public const string EmailConfirmed = nameof(EmailConfirmed);
            public const string CompanyName = nameof(CompanyName);
            public const string Role = nameof(Role);
            public const string BranchId = nameof(BranchId);
            public const string Currency = nameof(Currency);
            public const string DesignationId = nameof(DesignationId);
            public const string DesignationPermission = nameof(DesignationPermission);
        }

        public static class ControllersName
        {
            public const string Branches = nameof(Branches);
            public const string CompanyBranches = nameof(CompanyBranches);
            public const string Company = nameof(Company);
            public const string Currency = nameof(Currency);
            public const string SuperAdminDashBoardStat = nameof(SuperAdminDashBoardStat);
            public const string Designation = nameof(Designation);
            public const string SystemLogs = nameof(SystemLogs);
            public const string MenuRight = nameof(MenuRight);
            public const string ContactSetting = nameof(ContactSetting);
            public const string DashboardSetting = nameof(DashboardSetting);
            public const string ModuleSetting = nameof(ModuleSetting);
            public const string PrefixesSetting = nameof(PrefixesSetting);
            public const string ProductSetting = nameof(ProductSetting);
            public const string PurchaseSetting = nameof(PurchaseSetting);
            public const string SaleSetting = nameof(SaleSetting);
            public const string SystemSetting = nameof(SystemSetting);
            public const string TaxSetting = nameof(TaxSetting);
            public const string PaymentPlan = nameof(PaymentPlan);
            public const string CompanyUsers = nameof(CompanyUsers);
            public const string Subscription = nameof(Subscription);
            public const string UserDesignation = nameof(UserDesignation);
            public const string Account = nameof(Account);
            public const string Users = nameof(Users);
            public const string Customers = nameof(Customers);
            public const string DashBoardStat = nameof(DashBoardStat);
            public const string UnitOfMeasurements = nameof(UnitOfMeasurements);
            public const string ExceptionHandle = nameof(ExceptionHandle);
            public const string GoodsReceivedNotes = nameof(GoodsReceivedNotes);
            public const string GoodsReturnNotes = nameof(GoodsReturnNotes);
            public const string Images = nameof(Images);
            public const string Brands = nameof(Brands);
            public const string Categories = nameof(Categories);
            public const string Products = nameof(Products);
            public const string StockOfGoods = nameof(StockOfGoods);
            public const string PurchaseOrders = nameof(PurchaseOrders);
            public const string PurchaseRequisitionNotes = nameof(PurchaseRequisitionNotes);
            public const string PurchaseReturns = nameof(PurchaseReturns);
            public const string SaleOrders = nameof(SaleOrders);
            public const string SaleReturn = nameof(SaleReturn);
            public const string Vendors = nameof(Vendors);
        }

        public static class CacheKey
        {
            public const string PermissionList = nameof(PermissionList);
        }

    }
}
