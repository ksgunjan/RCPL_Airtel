<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Net" %>

<script RunAt="server">
    void Application_Start(object sender, EventArgs e)
    {
        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
        RegisterRoutes(RouteTable.Routes);
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        string currentUrl = HttpContext.Current.Request.Url.ToString().ToLower();
        var app = sender as HttpApplication;
        if (app != null && app.Context != null)
        {
            //  app.Context.Response.Headers.Remove("Server");
        }
    }
    void Application_Error(object sender, EventArgs e)
    {
        var serverError = Server.GetLastError() as HttpException;
        if (null != serverError)
        {
        }
    }
    void Session_Start(object sender, EventArgs e)
    {
        Session.Timeout = 20;
    }
    void Session_Remove(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
    }
    static void RegisterRoutes(RouteCollection routes)
    {
        routes.MapPageRoute("Login", "Login", "~/Default.aspx", true);
        routes.MapPageRoute("Error", "Error", "~/Error.aspx", true);
        routes.MapPageRoute("CRM-DashBoard", "CRM-DashBoard", "~/FrmDashBoard.aspx", true);
        routes.MapPageRoute("FE-DashBoard", "FE-DashBoard", "~/frmFEDashBoard.aspx", true);
        routes.MapPageRoute("Manager-DashBoard", "Manager-DashBoard", "~/frmmanagerDashBoard.aspx", true);
        routes.MapPageRoute("Manager-DashBoard-MO", "Manager-DashBoard-MO", "~/frmmanagerDashBoardMO.aspx", true);
        routes.MapPageRoute("Manager-DashBoard-PRI", "Manager-DashBoard-PRI", "~/frmmanagerDashBoardPRI.aspx", true);
        routes.MapPageRoute("Add-Company", "Add-Company", "~/frmAddCompany.aspx", true);
        routes.MapPageRoute("Add-Email", "Add-Email", "~/frmAddMail.aspx", true);
        routes.MapPageRoute("Add-Product", "Add-Product", "~/frmAddProduct.aspx", true);
        routes.MapPageRoute("Add-ProductType", "Add-ProductType", "~/frmAddProductType.aspx", true);
        routes.MapPageRoute("Add-New-User", "Add-New-User", "~/FrmAddLoginRegistration.aspx", true);
        routes.MapPageRoute("Add-UserCategory", "Add-UserCategory", "~/frmUserCategory.aspx", true);
        routes.MapPageRoute("ViewUpload-Excel", "ViewUpload-Excel", "~/frmViewUploadExcel.aspx", true);
        routes.MapPageRoute("Upload-Excel", "Upload-Excel", "~/frmUploadExcel.aspx", true);
        routes.MapPageRoute("VerifyUploadExcel", "VerifyUploadExcel", "~/frmVerifiyUploadExcelAfterTL.aspx", true);
        routes.MapPageRoute("VerifyUploadExcel-MO", "VerifyUploadExcel-MO", "~/frmApproveuploadExcelMO.aspx", true);
        routes.MapPageRoute("VerifyUploadExcel-PRI", "VerifyUploadExcel-PRI", "~/frmApprovePRI.aspx", true);
        routes.MapPageRoute("Excel-Month-Report", "Excel-Month-Report", "~/ExcelMonthReport.aspx", true);
        routes.MapPageRoute("Email-SMS", "Email-SMS", "~/BulkEmailSMS.aspx", true);
        routes.MapPageRoute("UnApproveUpload-Excel", "UnApproveUpload-Excel", "~/frmViweUnapproveExcel.aspx", true);
        routes.MapPageRoute("FEDashBoardReportMonthly-SubCatWise", "FEDashBoardReportMonthly-SubCatWise", "~/frmFEDashBoardReportMonthlySubCatWise.aspx", true);
        routes.MapPageRoute("AllocationFEDashBoardReportMonthly-SubCatWise", "AllocationFEDashBoardReportMonthly-SubCatWise", "~/frmFEDashBoardReportMonthlySubCatWiseAllocatedDateWise.aspx", true);
        routes.MapPageRoute("Monthly-Report", "Monthly-Report", "~/frmAllWorkMonthReport.aspx", true);
        routes.MapPageRoute("Monthly-Report-AllocatedDate", "Monthly-Report-AllocatedDate", "~/frmMonthWorkAllocatedate.aspx", true);
        routes.MapPageRoute("FE-MonthWistWorkReport", "FE-MonthWistWorkReport", "~/frmMonthWiseFeWorkReport.aspx", true);
        routes.MapPageRoute("Fe-LastDayReport", "Fe-LastDayReport", "~/frmFEDayWiseMISReportAllCategory.aspx", true);
        routes.MapPageRoute("Fe-DayWiseFeedBack", "Fe-DayWiseFeedBack", "~/frmDaywiseFeedBackReport.aspx", true);
        routes.MapPageRoute("Weekly-NegativeReport", "Weekly-NegativeReport", "~/frmNegativeWeeklyReport.aspx", true);
        routes.MapPageRoute("Weekly-PositiveReport", "Weekly-PositiveReport", "~/frmPositiveWeeklyReport.aspx", true);
        routes.MapPageRoute("Manager-Assign-FE-Job", "Manager-Assign-FE-Job", "~/frmAssignJobFEByManager.aspx", true);
        routes.MapPageRoute("AssignJob-FeByManager-Mobility", "AssignJob-FeByManager-Mobility", "~/frmAssignJobFEByManagerMobility.aspx", true);
        routes.MapPageRoute("AssignJob-FeByManager-PRI", "AssignJob-FeByManager-PRI", "~/frmAssignJobFEByManagerPRI.aspx", true);
        routes.MapPageRoute("Fe-JobHistory-Report", "Fe-JobHistory-Report", "~/ViweMasterFEjobHistory.aspx", true);
        routes.MapPageRoute("UpdateStatus-ByManager", "UpdateStatus-ByManager", "~/frmUpdateStatusSubmitManager.aspx", true);
        routes.MapPageRoute("Changepassword", "Changepassword", "~/frmFEChangePassword.aspx", true);
        routes.MapPageRoute("View-History-FERecord", "View-History-FERecord", "~/frmHistoryFERecord.aspx", true);
        routes.MapPageRoute("FE-Job", "FE-Job", "~/frmAssignJobFE.aspx", true);
        routes.MapPageRoute("Fe-JobMobility1", "Fe-JobMobility1", "~/frmAssignJobFEMobility1.aspx", true);
        routes.MapPageRoute("Fe-JobMobility", "Fe-JobMobility", "~/AssignJobMO.aspx", true);
        routes.MapPageRoute("Fe-JobPRI", "Fe-JobPRI", "~/AssignJobPRI.aspx", true);
        routes.MapPageRoute("Assign-Reschedule-Client-Visit-FE", "Assign-Reschedule-Client-Visit-FE", "~/RescheduleClientVisit.aspx", true);
        routes.MapPageRoute("Update-Mail", "Update-Mail", "~/ChangeDataEmail.aspx", true);
        routes.MapPageRoute("Update-Field", "Update-Field", "~/UpdateCaseFields.aspx", true);
    }
</script>
