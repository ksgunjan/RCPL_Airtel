<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<script RunAt="server">
    void Application_Start(object sender, EventArgs e)
    {
        //    Application["NoOfVisitors"] = 14029;
        RegisterRoutes(RouteTable.Routes);
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        //  Application.Lock();
        //   Application["NoOfVisitors"] = (int)Application["NoOfVisitors"] + 1;
        //   Application.UnLock();
    }

    static void RegisterRoutes(RouteCollection routes)
    {
        routes.MapPageRoute("Bulk-Email", "Bulk-Email", "~/BulkEmailSMS.aspx", true); 
        routes.MapPageRoute("Error", "Error", "~/Error.aspx", true);
        routes.MapPageRoute("CRM-Login", "CRM-Login", "~/Default.aspx", true);
        routes.MapPageRoute("Add-New-User", "Add-New-User", "~/FrmAddLoginRegistration.aspx", true);
        routes.MapPageRoute("View-AddUser", "View-AddUser", "~/frmviewUser.aspx", true);
        routes.MapPageRoute("CRM-DashBoard", "CRM-DashBoard", "~/FrmDashBoard.aspx", true);
        routes.MapPageRoute("FE-DashBoard", "FE-DashBoard", "~/frmFEDashBoard.aspx", true);
        routes.MapPageRoute("Manager-DashBoard", "Manager-DashBoard", "~/frmmanagerDashBoard.aspx", true);
        routes.MapPageRoute("Manager-DashBoard-MO", "Manager-DashBoard-MO", "~/frmmanagerDashBoardMO.aspx", true);
        routes.MapPageRoute("Manager-DashBoard-PRI", "Manager-DashBoard-PRI", "~/frmmanagerDashBoardPRI.aspx", true);
        routes.MapPageRoute("Add-Company", "Add-Company", "~/frmAddCompany.aspx", true);
        routes.MapPageRoute("View-Company", "View-Company", "~/frmViewCompany.aspx", true);
        routes.MapPageRoute("View-Product", "View-Product", "~/frmViewProduct.aspx", true);
        routes.MapPageRoute("Add-Product", "Add-Product", "~/frmAddProduct.aspx", true);
        routes.MapPageRoute("View-ProductType", "View-ProductType", "~/frmViewProductType.aspx", true);
        routes.MapPageRoute("Add-ProductType", "Add-ProductType", "~/frmAddProductType.aspx", true);
        routes.MapPageRoute("View-Zone", "View-Zone", "~/frmViewZone.aspx", true);
        routes.MapPageRoute("Add-Zone", "Add-Zone", "~/frmAddZone.aspx", true);
        routes.MapPageRoute("View-ZoneArea", "View-ZoneArea", "~/frmViewZoneArea.aspx", true);
        routes.MapPageRoute("Add-ZoneArea", "Add-ZoneArea", "~/frmAddZoneArea.aspx", true);
        routes.MapPageRoute("View-ZoneMapping", "View-ZoneMapping", "~/frmviewZoneMapping.aspx", true);
        routes.MapPageRoute("Add-ZoneMapping", "Add-ZoneMapping", "~/frmAddZoneMapping.aspx", true);
        routes.MapPageRoute("ViewUpload-Excel", "ViewUpload-Excel", "~/frmViewUploadExcel.aspx", true);
        routes.MapPageRoute("UnApproveUpload-Excel", "UnApproveUpload-Excel", "~/frmViweUnapproveExcel.aspx", true);
        routes.MapPageRoute("Upload-Excel", "Upload-Excel", "~/frmUploadExcel.aspx", true);
        routes.MapPageRoute("Add-Email", "Add-Email", "~/frmAddMail.aspx", true);
        routes.MapPageRoute("View-Email", "View-Email", "~/frmViewEmail.aspx", true);
        routes.MapPageRoute("View-History-FERecord", "View-History-FERecord", "~/frmHistoryFERecord.aspx", true);
        routes.MapPageRoute("FE-Job", "FE-Job", "~/frmAssignJobFE.aspx", true);
        routes.MapPageRoute("Manager-Assign-FE-Job", "Manager-Assign-FE-Job", "~/frmAssignJobFEByManager.aspx", true);
        routes.MapPageRoute("UpdateStatus-ByManager", "UpdateStatus-ByManager", "~/frmUpdateStatusSubmitManager.aspx", true);
        routes.MapPageRoute("Add-UserCategory", "Add-UserCategory", "~/frmUserCategory.aspx", true);
        routes.MapPageRoute("View-UserCategory", "View-UserCategory", "~/frmViewUserCategory.aspx", true);
        routes.MapPageRoute("Approve-Upload-Excel", "Approve-Upload-Excel", "~/frmApproveuploadExcel.aspx", true);
        routes.MapPageRoute("VerifyUploadExcel-MO", "VerifyUploadExcel-MO", "~/frmApproveuploadExcelMO.aspx", true);
        routes.MapPageRoute("Assign-Reschedule-Client-Visit-FE", "Assign-Reschedule-Client-Visit-FE", "~/RescheduleClientVisit.aspx", true);
        routes.MapPageRoute("Changepassword", "Changepassword", "~/frmFEChangePassword.aspx", true);
        routes.MapPageRoute("VerifyUploadExcel", "VerifyUploadExcel", "~/frmVerifiyUploadExcelAfterTL.aspx", true);
        routes.MapPageRoute("Fe-JobHistory-Report", "Fe-JobHistory-Report", "~/ViweMasterFEjobHistory.aspx", true);
        routes.MapPageRoute("Fe-JobMobility", "Fe-JobMobility", "~/frmAssignJobFEMobility.aspx", true);
        routes.MapPageRoute("Master-Email", "Master-Email", "~/frmmastersendmail.aspx", true);
        routes.MapPageRoute("Monthly-Report", "Monthly-Report", "~/frmAllWorkMonthReport.aspx", true);
        routes.MapPageRoute("Monthly-Report-AllocatedDate", "Monthly-Report-AllocatedDate", "~/frmMonthWorkAllocatedate.aspx", true);
        routes.MapPageRoute("FE-MonthWistWorkReport", "FE-MonthWistWorkReport", "~/frmMonthWiseFeWorkReport.aspx", true);
        routes.MapPageRoute("Fe-LastDayReport", "Fe-LastDayReport", "~/frmFEDayWiseMISReportAllCategory.aspx", true);
        routes.MapPageRoute("Fe-DayWiseFeedBack", "Fe-DayWiseFeedBack", "~/frmDaywiseFeedBackReport.aspx", true);
        routes.MapPageRoute("Weekly-NegativeReport", "Weekly-NegativeReport", "~/frmNegativeWeeklyReport.aspx", true);
        routes.MapPageRoute("AssignJob-FeByManager-Mobility", "AssignJob-FeByManager-Mobility", "~/frmAssignJobFEByManagerMobility.aspx", true);
        routes.MapPageRoute("AssignJob-FeByManager-PRI", "AssignJob-FeByManager-PRI", "~/frmAssignJobFEByManagerPRI.aspx", true);
        routes.MapPageRoute("Fe-JobPRI", "Fe-JobPRI", "~/AssignJobPRI.aspx", true);
        routes.MapPageRoute("VerifyUploadExcel-PRI", "VerifyUploadExcel-PRI", "~/frmApprovePRI.aspx", true);
        routes.MapPageRoute("Excel-Month-Report", "Excel-Month-Report", "~/ExcelMonthReport.aspx", true);
        routes.MapPageRoute("Not-Eligible-Mail", "Not-Eligible-Mail", "~/frmNotEligibleDataMail.aspx", true);
        routes.MapPageRoute("Weekly-PositiveReport", "Weekly-PositiveReport", "~/frmPositiveWeeklyReport.aspx", true);
        routes.MapPageRoute("FEDashBoardReportMonthly-SubCatWise", "FEDashBoardReportMonthly-SubCatWise", "~/frmFEDashBoardReportMonthlySubCatWise.aspx", true);
        routes.MapPageRoute("AllocationFEDashBoardReportMonthly-SubCatWise", "AllocationFEDashBoardReportMonthly-SubCatWise", "~/frmFEDashBoardReportMonthlySubCatWiseAllocatedDateWise.aspx", true);
        routes.MapPageRoute("Email-SMS", "Email-SMS", "~/BulkEmailSMS.aspx", true);
        routes.MapPageRoute("Test", "Test", "~/Test.aspx", true);
    }
    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        string currentUrl = HttpContext.Current.Request.Url.ToString().ToLower();
    }
    void Application_Error(object sender, EventArgs e)
    {
        var serverError = Server.GetLastError() as HttpException;
        if (null != serverError)
        {
            int errorCode = serverError.GetHttpCode();
            if (404 == errorCode)
            {
                Server.ClearError();
                Response.Redirect("~/Error");
            }
        }
    }
</script>
