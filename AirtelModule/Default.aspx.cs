using System;
using BusinessLayer;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Text;
using System.Data;
using System.IO;
using System.Configuration;
using ClosedXML.Excel;

public partial class _Default : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Common Co = new Common();
    DateTime LastLoginTime = DateTime.Now;
    string Category = string.Empty;
    HybridDictionary hyLogin = new HybridDictionary();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
        }
    }
    public static bool IsValidEmailId(string InputEmail)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(InputEmail);
        if (match.Success)
            return true;
        else
            return false;
    }
    protected void btnlog_Click(object sender, EventArgs e)
    {
        if (txtpass.Text.Trim() != "" && txtusername.Text.Trim() != "")
        {
            DataTable DTReteriveLogin = Lo.RetriveCodeWithContidion("select UserName,Password,IsActive from tbl_trn_Login where UserName='" + txtusername.Text + "' and Password='" + txtpass.Text.Trim() + "' and IsActive='Y'");
            if (DTReteriveLogin.Rows.Count > 0)
            {
                try
                {
                    Int32 UpdateLoginTime = Lo.UpdateCodeOther("tbl_trn_Login", "Set LastLoginTime='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss") + "'", "UserName='" + txtusername.Text + "' and Password='" + txtpass.Text + "' and IsActive='Y' ");
                    if (UpdateLoginTime != 0)
                    {
                        GetUserDetail();
                    }
                    else
                    {
                        divErr.Visible = true;
                        divErr.InnerHtml = "Invalid login detail";
                        divErr.Attributes.Add("class", "alert alert-danger");
                    }
                }
                catch (Exception ex)
                {
                    divErr.Visible = true;
                    divErr.InnerHtml = ex.InnerException.Message;
                    divErr.Attributes.Add("class", "alert alert-danger");
                    if (Category == "Manager")
                    {
                        Response.RedirectToRoute("Manager-DashBoard");
                    }
                    if (Category == "Field Executive")
                    {
                        Response.RedirectToRoute("FE-DashBoard");
                    }
                    if (Category == "Admin")
                    {
                        Response.RedirectToRoute("CRM-DashBoard");
                    }
                }
            }
            else
            {
                divErr.Visible = true;
                divErr.InnerHtml = "Invalid login detail";
                divErr.Attributes.Add("class", "alert alert-danger");

            }
        }
        else
        {
            divErr.Visible = true;
            divErr.InnerHtml = "User not found. Please verify your email or password.";
            divErr.Attributes.Add("class", "alert alert-danger");
        }
    }
    public string Resturl(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }
    protected void GetUserDetail()
    {
        DataTable DtUser = Lo.RetriveCodeUser(txtusername.Text);
        if (DtUser.Rows.Count > 0)
        {
            Category = DtUser.Rows[0]["Category"].ToString();
            if (Category == "Manager")
            {
                Session["Name"] = DtUser.Rows[0]["Name"].ToString();
                Session["LoginEmail"] = DtUser.Rows[0]["UserName"].ToString();
                try
                {
                    #region SendMailAutoCodeAll or Send Mail RCPL InterNal
                    #region Send Mail AutoMode Fe-LastDayReport
                    FeDayWiseReportCurrentAllCategory();
                    SendMailAutoMonthReportOnlyPositiveNegRevi();
                    #endregion
                    #endregion
                }
                catch (Exception ex)
                {
                }
                Response.RedirectToRoute("Manager-DashBoard");
            }
            else if (Category == "Field Executive")
            {
                Session["Name"] = DtUser.Rows[0]["Name"].ToString();
                Session["LoginEmail"] = DtUser.Rows[0]["UserName"].ToString();
                try
                {
                    #region Send Mail Puru Sir
                    #region Send Mail AutoMode Dashboard Monthly-Report
                    SendMailAutoMonthReport();
                    #endregion
                    #region  Fe Current Month All Day Work Count Report
                    FeMonthWiseReport();
                    #endregion
                    #region Send Mail AutoMode Monthly-Report-AllocatedDate
                    SendMailAutoMonthReport1();
                    #endregion
                    #endregion
                    #region Send Mail AutoMode Fe Last Day Report By Time Date Mail Goes InterNal
                    FeStatusUpdateTimeDetailsLastDay();
                    #endregion
                    #region FeedBackMail InterNal
                    FeDayWiseFeedBack();
                    #endregion
                }
                catch (Exception ex)
                {
                }
                Response.RedirectToRoute("FE-DashBoard");
            }
            else if (Category == "Admin")
            {
                Session["Name"] = DtUser.Rows[0]["Name"].ToString();
                Session["LoginEmail"] = DtUser.Rows[0]["UserName"].ToString();
                Response.RedirectToRoute("CRM-DashBoard");
            }
            else if (Category == "Manager Sales")
            {
                Session["Name"] = DtUser.Rows[0]["Name"].ToString();
                Session["LoginEmail"] = DtUser.Rows[0]["UserName"].ToString();
                Response.RedirectToRoute("Manager-DashBoard");
            }
        }
        else
        {
            Response.RedirectToRoute("CRM-Login");
        }
    }
    #region MailCode1"
    protected void SendMailAutoMonthReport()
    {
        try
        {

            DataTable DTGetStatusSendMail = Lo.RetriveCodeWithContidion("select * from tbl_trn_AutoMailSendStatus where Category='MonthlyReportStatusUpdateDate' and (MailDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "')");
            if (DTGetStatusSendMail.Rows.Count == 0)
            {
                DateTime mTime = Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy hh:mm tt"));
                DateTime MailSendTime = Convert.ToDateTime(DateTime.Parse("18:32").ToString("dd/MMM/yyyy hh:mm tt"));
                //  DateTime MailSendTime = Convert.ToDateTime(DateTime.Parse("12:32").ToString("dd/MMM/yyyy hh:mm tt"));
                if (MailSendTime < mTime)
                {
                    DataTable dtll = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select upper(convert(varchar(3),[StatusUpdateDate],0)) as [Month],(case when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProductName='Leased Line') Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2");
                    DataTable dtmo = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select DateName(Month,StatusUpdateDate) as [Month] , (case  when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe  from tbl_trn_rawdata where ProductName='MO' group by DateName(Month,StatusUpdateDate),[party name],statusbyfe) Tab1 pivot(count(StatusByFE) for StatusByFE in  ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2 order by [Month]");
                    DataTable dtPRI = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select upper(convert(varchar(3),[StatusUpdateDate],0)) as [Month],(case when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProductName='PRI-Fixed Line' and KeyDup='false') Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2");
                    if (dtll.Rows.Count > 0 || dtmo.Rows.Count > 0 || dtPRI.Rows.Count > 0)
                    {
                        ExportExcel(dtll, dtmo, dtPRI, "DashBoard Monthly Work Report (FE Audit Date Wise)");
                        int update = Lo.InsertNormal("insert into tbl_trn_AutoMailSendStatus (Category,MailDate,IsSend) values ('MonthlyReportStatusUpdateDate','" + DateTime.Now.ToString("dd/MMM/yyyy") + "','Y')");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    #endregion
    #region MailCode2"
    protected void SendMailAutoMonthReport1()
    {
        try
        {
            DataTable DTGetStatusSendMail = Lo.RetriveCodeWithContidion("select * from tbl_trn_AutoMailSendStatus where Category='MonthlyReportAllocateDate' and MailDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "'");
            if (DTGetStatusSendMail.Rows.Count == 0)
            {
                DateTime mTime = Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy hh:mm tt"));
                DateTime MailSendTime = Convert.ToDateTime(DateTime.Parse("18:32").ToString("dd/MMM/yyyy hh:mm tt"));
                //  DateTime MailSendTime = Convert.ToDateTime(DateTime.Parse("12:32").ToString("dd/MMM/yyyy hh:mm tt"));
                if (MailSendTime < mTime)
                {
                    DataTable dtll = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select upper(convert(varchar(3),[Allocated Date],0)) as [Month],(case when StatusByFE is null  then 'Pending'  when StatusByFE='' then 'Pending' when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProductName='Leased Line')Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2");
                    DataTable dtmo = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select DateName(Month,[Allocated Date]) as [Month] ,(case when StatusByFE is null then 'Pending' when StatusByFE='' then 'Pending' when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProductName='MO' group by DateName(Month,[Allocated Date]),[party name],statusbyfe) Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2 order by [Month]");
                    DataTable dtPRI = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select upper(convert(varchar(3),[Allocated Date],0)) as [Month],(case when StatusByFE is null  then 'Pending'  when StatusByFE='' then 'Pending' when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProductName='PRI-Fixed Line' and KeyDup='false')Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2");
                    if (dtll.Rows.Count > 0 || dtmo.Rows.Count > 0 || dtPRI.Rows.Count > 0)
                    {
                        ExportExcel(dtll, dtmo, dtPRI, "DashBoard Monthly Work Report (FE Allocation Date Wise)");
                        int update = Lo.InsertNormal("insert into tbl_trn_AutoMailSendStatus (Category,MailDate,IsSend) values ('MonthlyReportAllocateDate','" + DateTime.Now.ToString("dd/MMM/yyyy") + "','Y')");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    #endregion
    #region MailCode3
    protected void FeMonthWiseReport()
    {
        DataTable DTGetStatusSendMail = Lo.RetriveCodeWithContidion("select * from tbl_trn_AutoMailSendStatus where Category='FEAllWorkMonthReport' and MailDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "'");
        if (DTGetStatusSendMail.Rows.Count == 0)
        {
            DateTime mTime = Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy hh:mm tt"));
            DateTime MailSendTime = Convert.ToDateTime(DateTime.Parse("12:00").ToString("dd/MMM/yyyy hh:mm tt"));
            if (MailSendTime < mTime)
            {
                DataTable DTSearch = Lo.RetriveCodeWithContidion("IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp SELECT DISTINCT TOP 100 PERCENT datepart(day,statusupdatedate) as [Day] into #temp FROM tbl_trn_RawData where StatusUpdateDate is not null  and DATENAME(MONTH,StatusUpdateDate)='" + DateTime.Now.ToString("MMMM") + "' order by datepart(day,statusupdatedate) CREATE CLUSTERED INDEX cix_wekno ON #temp([Day] ASC)DECLARE @Pivot_Column [nvarchar](max); DECLARE @Query [nvarchar](max); SELECT @Pivot_Column= COALESCE(@Pivot_Column+',','')+ QUOTENAME([DAY])  FROM (select [day] from #temp)  Tab SELECT @Query='SELECT [FE Name], '+@Pivot_Column+' FROM(select [FE name],[Party Name] as [Total], datepart(day,statusupdatedate) as [Day] FROM tbl_trn_RawData where StatusUpdateDate is not null and ProductName=''Leased Line'' and DATENAME(MONTH,StatusUpdateDate)=''" + DateTime.Now.ToString("MMMM") + "'' group by [FE name],[party name],statusupdatedate) Tab1 PIVOT(count([Total]) FOR [Day] IN ('+@Pivot_Column+')) AS Tab2 ' EXEC  sp_executesql  @Query  DROP TABLE #temp");
                DataTable DTSearchMO = Lo.RetriveCodeWithContidion("IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp SELECT DISTINCT TOP 100 PERCENT datepart(day,statusupdatedate) as [Day] into #temp FROM tbl_trn_RawData where StatusUpdateDate is not null  and DATENAME(MONTH,StatusUpdateDate)='" + DateTime.Now.ToString("MMMM") + "' order by datepart(day,statusupdatedate)CREATE CLUSTERED INDEX cix_wekno ON #temp([Day] ASC)DECLARE @Pivot_Column [nvarchar](max); DECLARE @Query [nvarchar](max); SELECT @Pivot_Column= COALESCE(@Pivot_Column+',','')+ QUOTENAME([DAY])  FROM (select [day] from #temp)  Tab SELECT @Query='SELECT [FE Name], '+@Pivot_Column+' FROM(select [FE name],[Party Name] as [Total], datepart(day,statusupdatedate) as [Day] FROM tbl_trn_RawData where StatusUpdateDate is not null and ProductName=''MO'' and DATENAME(MONTH,StatusUpdateDate)=''" + DateTime.Now.ToString("MMMM") + "''  group by [FE name],[party name],statusupdatedate) Tab1 PIVOT(count([Total]) FOR [Day] IN ('+@Pivot_Column+')) AS Tab2 ' EXEC  sp_executesql  @Query  DROP TABLE #temp");
                DataTable DTSearchPRI = Lo.RetriveCodeWithContidion("IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp SELECT DISTINCT TOP 100 PERCENT datepart(day,statusupdatedate) as [Day] into #temp FROM tbl_trn_RawData where StatusUpdateDate is not null   and DATENAME(MONTH,StatusUpdateDate)='" + DateTime.Now.ToString("MMMM") + "' order by datepart(day,statusupdatedate) CREATE CLUSTERED INDEX cix_wekno ON #temp([Day] ASC)DECLARE @Pivot_Column [nvarchar](max); DECLARE @Query [nvarchar](max); SELECT @Pivot_Column= COALESCE(@Pivot_Column+',','')+ QUOTENAME([DAY])  FROM (select [day] from #temp)  Tab SELECT @Query='SELECT [FE Name], '+@Pivot_Column+' FROM(select [FE name],[Party Name] as [Total], datepart(day,statusupdatedate) as [Day] FROM tbl_trn_RawData where StatusUpdateDate is not null and ProductName=''PRI-Fixed Line'' and KeyDup=''False'' and DATENAME(MONTH,StatusUpdateDate)=''" + DateTime.Now.ToString("MMMM") + "'' group by [FE name],[party name],statusupdatedate) Tab1 PIVOT(count([Total]) FOR [Day] IN ('+@Pivot_Column+')) AS Tab2 ' EXEC  sp_executesql  @Query  DROP TABLE #temp");
                if (DTSearchMO.Rows.Count > 0 || DTSearch.Rows.Count > 0 || DTSearchPRI.Rows.Count > 0)
                {
                    ExportExcel(DTSearch, DTSearchMO, DTSearchPRI, "Automated FE Current Month Day Wise Work Report");
                    int update = Lo.InsertNormal("insert into tbl_trn_AutoMailSendStatus (Category,MailDate,IsSend) values ('FEAllWorkMonthReport','" + DateTime.Now.ToString("dd/MMM/yyyy") + "','Y')");
                }
            }
        }
    }
    #endregion
    #region MailCode4
    protected void FeDayWiseReportCurrentAllCategory()
    {
        DataTable DTGetStatusSendMail = Lo.RetriveCodeWithContidion("select * from tbl_trn_AutoMailSendStatus where Category='FELastDayWorkReport' and MailDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "'");
        if (DTGetStatusSendMail.Rows.Count == 0)
        {
            DateTime mTime = Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy hh:mm tt"));
            DateTime MailSendTime = Convert.ToDateTime(DateTime.Parse("12:10").ToString("dd/MMM/yyyy hh:mm tt"));
            if (MailSendTime < mTime)
            {
                DataTable DtL = Lo.RetriveCodeWithContidion("select [Fe Name],[Positive],[Negative],[Pending] as Revisit from (select [Fe Name],StatusByFE from tbl_trn_rawdata where ProductName='Leased Line' and StatusUpdateDate='" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "')Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending])) as Tab2 order by Tab2.[FE Name]");
                DataTable DtM = Lo.RetriveCodeWithContidion("select [Fe Name],[Positive],[Negative],[Pending] as Revisit from (select [Fe Name],StatusByFE from tbl_trn_rawdata where ProductName='MO' and StatusUpdateDate='" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "' group by [FE Name],[party name],statusbyfe)Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending])) as Tab2 order by Tab2.[FE Name]");
                DataTable DtP = Lo.RetriveCodeWithContidion("select [Fe Name],[Positive],[Negative],[Pending] as Revisit from (select [Fe Name],StatusByFE from tbl_trn_rawdata where ProductName='PRI-Fixed Line' and KeyDup='false' and StatusUpdateDate='" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "')Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending])) as Tab2 order by Tab2.[FE Name]");
                if (DtL.Rows.Count > 0 || DtM.Rows.Count > 0 || DtP.Rows.Count > 0)
                {
                    ExportExcel(DtL, DtM, DtP, "Automated FE Last Day Work Report Status Wise");
                    int update = Lo.InsertNormal("insert into tbl_trn_AutoMailSendStatus (Category,MailDate,IsSend) values ('FELastDayWorkReport','" + DateTime.Now.ToString("dd/MMM/yyyy") + "','Y')");
                }
            }
        }
    }
    #endregion
    #region Mail Send 5
    protected void FeStatusUpdateTimeDetailsLastDay()
    {
        DataTable DTGetStatusSendMail = Lo.RetriveCodeWithContidion("select * from tbl_trn_AutoMailSendStatus where Category='FELastDayWorkInternalMail' and MailDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "'");
        if (DTGetStatusSendMail.Rows.Count == 0)
        {
            DateTime mTime = Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy hh:mm tt"));
            DateTime MailSendTime = Convert.ToDateTime(DateTime.Parse("09:30").ToString("dd/MMM/yyyy hh:mm tt"));
            if (MailSendTime < mTime)
            {

                DataTable DtLeaseLineS = Lo.RetriveCodeWithContidion("select [FE Name],[Party Name],[StatusByFE],StatusUpdateDate,StatusTime,[Subs Del No],[Logical Circuit ID],ProductName from tbl_trn_RawData where StatusUpdateDate='" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "'  and  StatusByFE!='' group by [Party Name],[StatusByFE],StatusUpdateDate,StatusTime,[Subs Del No],[Logical Circuit ID],ProductName,[FE NAme] order by [Fe Name],[StatusTime],[ProductName] asc ");
                if (DtLeaseLineS.Rows.Count > 0)
                {
                    ExportExcel1(DtLeaseLineS, "Automated FE Last Day Visit or Status Update Time Report");
                    int update = Lo.InsertNormal("insert into tbl_trn_AutoMailSendStatus (Category,MailDate,IsSend) values ('FELastDayWorkInternalMail','" + DateTime.Now.ToString("dd/MMM/yyyy") + "','Y')");
                }
            }
        }
    }
    #endregion
    #region MailCode6
    protected void FeDayWiseFeedBack()
    {
        try
        {
            DataTable DTGetStatusSendMail = Lo.RetriveCodeWithContidion("select * from tbl_trn_AutoMailSendStatus where Category='FELastDayWorkReportDetail' and MailDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "'");
            if (DTGetStatusSendMail.Rows.Count == 0)
            {
                DateTime mTime = Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy hh:mm tt"));
                DateTime MailSendTime = Convert.ToDateTime(DateTime.Parse("6:30").ToString("dd/MMM/yyyy hh:mm tt"));
                if (MailSendTime < mTime)
                {
                    DataTable DtLL = Lo.RetriveCodeWithContidion("select  [Logical Circuit ID],[Party Name],[FE Name], StatusByFE,Code, RemarksOfStatus  as Remarks ,StatusUpdateDate,[Customer Name],PersonMetMobileNo from tbl_trn_RawData where ProductName='Leased Line' and StatusUpdateDate='" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "'");
                    DataTable DtPRI = Lo.RetriveCodeWithContidion("select  [Subs Del No],[Party Name],[FE Name], StatusByFE,Code, RemarksOfStatus  as Remarks ,StatusUpdateDate,[Customer Name],PersonMetMobileNo from tbl_trn_RawData where ProductName='PRI-Fixed Line' and KeyDup='false' and StatusUpdateDate='" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "'");
                    // string compname;
                    DataTable dt = new DataTable();
                    //dt.Columns.Add("Party Name", typeof(string));
                    //dt.Columns.Add("Billed Ext Id", typeof(string));
                    //dt.Columns.Add("FE Name", typeof(string));
                    //dt.Columns.Add("StatusByFe", typeof(string));
                    //dt.Columns.Add("Code", typeof(string));
                    //dt.Columns.Add("Remarks", typeof(string));
                    //dt.Columns.Add("StatusUpdateDate", typeof(DateTime));
                    //dt.Columns.Add("PersonMetName", typeof(string));
                    //dt.Columns.Add("PersonMetMobileNo", typeof(string));
                    //DataTable DtFetchcompgropby = Lo.RetriveCodeWithContidion("select count([party name])as [Count] ,[party name] from tbl_trn_RawData where productname='MO' and isclosed='N' and IsActive='Y' and (StatusByFE!='' or StatusByFE is not null) and ExcelVerifiy='2' and StatusUpdateDate='" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "' and [Eligible/Not Eligible]='Eligible' group by [party name]");
                    dt = Lo.RetriveCodeWithContidion("select * from fn_DailyDayWiseFeedBackMo('MO','" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "') ");
                    //if (DtFetchcompgropby.Rows.Count > 0)
                    //{
                    //    for (int i = 0; DtFetchcompgropby.Rows.Count > i; i++)
                    //    {
                    //        compname = DtFetchcompgropby.Rows[i]["Party Name"].ToString();
                    //        DataTable DtAssignJob = Lo.RetriveCodeWithContidion("SELECT  top 1  [Billed Ext Id],[Party Name],[FE Name],StatusByFe,Code,RemarksOfStatus as Remarks,StatusUpdateDate,PersonMetName,PersonMetMobileNo from tbl_trn_RawData where IsActive='Y' and IsClosed !='Y' and ExcelVerifiy='2'  and (StatusByFE!='' or StatusByFE is not null) and [Eligible/Not Eligible]='Eligible' and ProductName='MO' and StatusUpdateDate='" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "' and [Party Name]= '" + compname + "' order by [FE Name] asc");
                    //        foreach (DataRow dr in DtAssignJob.Rows)
                    //        {
                    //            object[] row = dr.ItemArray;
                    //            dt.Rows.Add(row);
                    //        }
                    //    }
                    //}
                    if (DtLL.Rows.Count > 0 || dt.Rows.Count > 0 || DtPRI.Rows.Count > 0)
                    {
                        ExportExcel(DtLL, dt, DtPRI, "Automated FE Day Wise Feedback Report");
                        int update = Lo.InsertNormal("insert into tbl_trn_AutoMailSendStatus (Category,MailDate,IsSend) values ('FELastDayWorkReportDetail','" + DateTime.Now.ToString("dd/MMM/yyyy") + "','Y')");
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion
    #region MailCode9"
    protected void SendMailAutoMonthReportOnlyPositiveNegRevi()
    {
        try
        {

            DataTable DTGetStatusSendMail = Lo.RetriveCodeWithContidion("select * from tbl_trn_AutoMailSendStatus where Category='MonthlyReportStatusUpdateDatePNR' and (MailDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "')");
            if (DTGetStatusSendMail.Rows.Count == 0)
            {
                DateTime mTime = Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy hh:mm tt"));
                DateTime MailSendTime = Convert.ToDateTime(DateTime.Parse("10:32").ToString("dd/MMM/yyyy hh:mm tt"));
                //  DateTime MailSendTime = Convert.ToDateTime(DateTime.Parse("12:32").ToString("dd/MMM/yyyy hh:mm tt"));
                if (MailSendTime < mTime)
                {
                    DataTable dtll = Lo.RetriveCodeWithContidion("select [Month],[Positive],[Negative],[Revisit] from (select DateName(Month,StatusUpdateDate) as [Month],(case when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProductName='Leased Line') Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Revisit])) as Tab2");
                    DataTable dtmo = Lo.RetriveCodeWithContidion("select [Month],[Positive],[Negative],[Revisit] from (select DateName(Month,StatusUpdateDate) as [Month] , (case  when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe  from tbl_trn_rawdata where ProductName='MO' group by DateName(Month,StatusUpdateDate),[party name],statusbyfe)  Tab1 pivot(count(StatusByFE) for StatusByFE in  ([Positive],[Negative],[Revisit]))   as Tab2 order by [Month]");
                    DataTable dtPRI = Lo.RetriveCodeWithContidion("select [Month],[Positive],[Negative],[Revisit] from (select top 100 percent  DateName(Month,StatusUpdateDate) as [Month],(case when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProductName='PRI-Fixed Line' and KeyDUp='false') Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Revisit])) as Tab2");
                    if (dtll.Rows.Count > 0 || dtmo.Rows.Count > 0 || dtPRI.Rows.Count > 0)
                    {
                        ExportExcel(dtll, dtmo, dtPRI, "DashBoard Monthly Work Report (FE Audit Date Wise)");
                        int update = Lo.InsertNormal("insert into tbl_trn_AutoMailSendStatus (Category,MailDate,IsSend) values ('MonthlyReportStatusUpdateDatePNR','" + DateTime.Now.ToString("dd/MMM/yyyy") + "','Y')");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    #endregion
    #region BindExcel or MailCode
    protected void SendMailExcelReport(byte[] bytes, byte[] wali, byte[] PRI, DataTable dtll, DataTable dtmo, DataTable dtPRI, string FileName)
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/EmailForReport.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Message}", FileName);
        SendMail s;
        s = new SendMail();
        s.CreateInvoiceMailForByte1("verification@rcpl.in", "Shyam.Prasad@airtel.com", FileName.ToString(), body, "", bytes, wali, PRI, "verification@rcpl.in", "gagan@rcpl.in");
        // s.CreateInvoiceMailForByte1("verification@rcpl.in", "mohdwali@globalitpoint.com", FileName.ToString(), body, "", bytes, wali, PRI, "", "");
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
    }
    protected void SendMailExcelReport1(byte[] bytes, byte[] wali, byte[] PRI, DataTable dtll, DataTable dtmo, DataTable dtPRI, string FileName)
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/EmailForReport.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Message}", FileName);
        SendMail s;
        s = new SendMail();
        s.CreateInvoiceMailForByte1("verification@rcpl.in", "gagan@rcpl.in", FileName.ToString(), body, "", bytes, wali, PRI, "sales@rcpl.in", "bs1@rcpl.in");
        // s.CreateInvoiceMailForByte1("verification@rcpl.in", "mohdwali@globalitpoint.com", FileName.ToString(), body, "", bytes, wali, PRI, "", "");
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
    }
    protected void ExportExcel(DataTable dtll, DataTable dtmo, DataTable dtPRI, string FileName)
    {
        byte[] wali;
        byte[] bytes;
        byte[] PRI;
        using (XLWorkbook wb = new XLWorkbook())
        {
            dtll.TableName = "Report Leased Line";
            wb.Worksheets.Add(dtll);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                bytes = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        using (XLWorkbook wbn = new XLWorkbook())
        {
            dtmo.TableName = "Report MO";
            wbn.Worksheets.Add(dtmo);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbn.SaveAs(memoryStream);
                wali = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        using (XLWorkbook wbpri = new XLWorkbook())
        {
            dtPRI.TableName = "Report PRI";
            wbpri.Worksheets.Add(dtPRI);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbpri.SaveAs(memoryStream);
                PRI = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        if (FileName == "DashBoard Monthly Work Report (FE Audit Date Wise)" || FileName == "DashBoard Monthly Work Report (FE Allocation Date Wise)" || FileName == "Automated FE Current Month Day Wise Work Report")
        {
            if (dtll != null || dtmo != null || dtPRI != null)
            {
                SendMailExcelReport(bytes, wali, PRI, dtll, dtmo, dtPRI, FileName);
            }
        }
        else
        {
            if (dtll != null || dtmo != null || dtPRI != null)
            {
                SendMailExcelReport1(bytes, wali, PRI, dtll, dtmo, dtPRI, FileName);
            }
        }
    }
    byte[] PRIi;
    byte[] Walie;
    protected void ExportExcel1(DataTable dtll, string FileName)
    {
        byte[] bytes;
        using (XLWorkbook wb = new XLWorkbook())
        {
            dtll.TableName = "Report Leased Line";
            wb.Worksheets.Add(dtll);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                bytes = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        if (dtll != null)
        {
            DataTable dtPRI = new DataTable();
            DataTable dtmo = new DataTable();
            SendMailExcelReport1(bytes, Walie, PRIi, dtll, dtmo, dtPRI, FileName);
        }
    }
    #endregion
}