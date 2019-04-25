using System;
using System.Data;
using BusinessLayer;
using System.IO;
using System.Configuration;
using ClosedXML.Excel;

public partial class frmPositiveWeeklyReport : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            BindLL();
            BindMo();
            BindPRI();
        }
    }
    protected void BindLL()
    {
        DataTable DtLL = Lo.RetriveCodeWithContidion("select [Logical Circuit ID],[Party Name],StatusByFE,Code,RemarksOfStatus as Remarks,StatusUpdateDate,PersonMetName,PersonMetMobileNo,PersonMetEmail,StatusTime,AppointmentMailDatetime,CustSamKamMailDatetime from tbl_trn_RawData where ProductName='Leased Line' and StatusUpdateDate between '" + DateTime.Now.AddDays(-7).ToString("dd/MMM/yyyy") + "' and '" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "' and StatusByFE='Positive' ");
        if (DtLL.Rows.Count > 0)
        {
            gvll.DataSource = DtLL;
            gvll.DataBind();
            gvll.Visible = true;
        }
    }
    protected void BindMo()
    {
        //string compname;
        //dt.Columns.Add("Billed Ext ID", typeof(string));
        //dt.Columns.Add("Party Name", typeof(string));
        //dt.Columns.Add("FE Name", typeof(string));
        //dt.Columns.Add("StatusByFe", typeof(string));
        //dt.Columns.Add("Code", typeof(string));
        //dt.Columns.Add("Remarks", typeof(string));
        //dt.Columns.Add("StatusUpdateDate", typeof(DateTime));
        //dt.Columns.Add("PersonMetName", typeof(string));
        //dt.Columns.Add("PersonMetMobileNo", typeof(string));
        //dt.Columns.Add("PersonMetEmail", typeof(string));
        //dt.Columns.Add("StatusTime", typeof(string));
        //dt.Columns.Add("AppointmentMailDatetime", typeof(string));
        //dt.Columns.Add("CustSamKamMailDatetime", typeof(string));
        //DataTable DtFetchcompgropby = Lo.RetriveCodeWithContidion("select count([party name])as [Count] ,[party name] from tbl_trn_RawData where productname='MO' and IsActive='Y'   and StatusUpdateDate between '" + DateTime.Now.AddDays(-7).ToString("dd/MMM/yyyy") + "' and '" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "' and StatusByFE='Positive' group by [party name]");
         dt = Lo.RetriveCodeWithContidion("select * from [fn_WeeklyNegativeReportMO]('MO','" + DateTime.Now.AddDays(-7).ToString("dd/MMM/yyyy") + "','" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "','Positive')");
        //if (DtFetchcompgropby.Rows.Count > 0)
        //{
        //    for (int i = 0; DtFetchcompgropby.Rows.Count > i; i++)
        //    {
        //        compname = DtFetchcompgropby.Rows[i]["Party Name"].ToString();
        //        DataTable DtAssignJob = Lo.RetriveCodeWithContidion("SELECT  top 1 [Billed Ext ID],[Party Name],[FE Name], StatusByFE,Code, RemarksOfStatus as Remarks ,StatusUpdateDate,[PersonMetName],[PersonMetMobileNo],[PersonMetEmail],StatusTime,AppointmentMailDatetime,CustSamKamMailDatetime from tbl_trn_RawData where IsActive='Y' and StatusByFE='Positive' and StatusUpdateDate between '" + DateTime.Now.AddDays(-7).ToString("dd/MMM/yyyy") + "' and '" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "' and ProductName='MO' and [Party Name]= '" + compname + "' order by [FE Name] asc");
        //        foreach (DataRow dr in DtAssignJob.Rows)
        //        {
        //            object[] row = dr.ItemArray;
        //            dt.Rows.Add(row);
        //        }
        //    }
            if (dt.Rows.Count > 0)
            {
                gvmo.DataSource = dt;
                gvmo.DataBind();
                gvmo.Visible = true;
            }
            else
            {
                gvmo.Visible = false;
            }
        //}
    }
    protected void BindPRI()
    {
        DataTable DtPRI = Lo.RetriveCodeWithContidion("select [Subs Del No],[Party Name], StatusByFE,Code, RemarksOfStatus as Remarks ,StatusUpdateDate,PersonMetName,PersonMetMobileNo,PersonMetEmail,StatusTime,AppointmentMailDatetime,CustSamKamMailDatetime from tbl_trn_RawData where ProductName='PRI-Fixed Line' and StatusUpdateDate between '" + DateTime.Now.AddDays(-7).ToString("dd/MMM/yyyy") + "' and '" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "' and StatusByFE='Positive' ");
        if (DtPRI.Rows.Count > 0)
        {
            gvpri.DataSource = DtPRI;
            gvpri.DataBind();
            gvpri.Visible = true;
        }
    }
    protected void btnmail_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable DtLL = Lo.RetriveCodeWithContidion("select  [Logical Circuit ID],[Party Name],[FE Name], StatusByFE,Code, RemarksOfStatus as Remarks ,StatusUpdateDate,PersonMetName,PersonMetMobileNo,PersonMetEmail,StatusTime,AppointmentMailDatetime,CustSamKamMailDatetime from tbl_trn_RawData where StatusByFE='Positive' and ProductName='Leased Line' and StatusUpdateDate between '" + DateTime.Now.AddDays(-7).ToString("dd/MMM/yyyy") + "' and '" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "' ");
            BindMo();
            DataTable DtPRI = Lo.RetriveCodeWithContidion("select  [Subs Del No],[Party Name],[FE Name], StatusByFE,Code, RemarksOfStatus as Remarks ,StatusUpdateDate,PersonMetName,PersonMetMobileNo,PersonMetEmail,StatusTime,AppointmentMailDatetime,CustSamKamMailDatetime from tbl_trn_RawData where StatusByFE='Positive' and ProductName='PRI-Fixed Line' and StatusUpdateDate between '" + DateTime.Now.AddDays(-7).ToString("dd/MMM/yyyy") + "' and '" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "' ");
            if (DtLL.Rows.Count > 0 || dt.Rows.Count > 0 || DtPRI.Rows.Count > 0)
            {
                ExportExcel(DtLL, dt, DtPRI, "Automated FE Weekly Positive Report");
                int update = Lo.InsertNormal("insert into tbl_trn_AutoMailSendStatus (Category,MailDate,IsSend) values ('FEWeeklyPositiveWorkReport','" + DateTime.Now.ToString("dd/MMM/yyyy") + "','Y')");

            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void ExportExcel(DataTable DtLL, DataTable dt, DataTable dtPRI, string FileName)
    {
        byte[] bytes;
        byte[] Wali;
        byte[] PRI;
        using (XLWorkbook wb = new XLWorkbook())
        {
            DtLL.TableName = "Leased Line Work Positive Weekly Report";
            wb.Worksheets.Add(DtLL);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                bytes = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        using (XLWorkbook wb1 = new XLWorkbook())
        {
            dt.TableName = "MO Work Positive Weekly Report";
            wb1.Worksheets.Add(dt);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb1.SaveAs(memoryStream);
                Wali = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        using (XLWorkbook wbpri = new XLWorkbook())
        {
            dtPRI.TableName = "PRI Work Positive Weekly Report";
            wbpri.Worksheets.Add(dtPRI);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbpri.SaveAs(memoryStream);
                PRI = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        SendMailExcelReport(bytes, Wali, PRI, DtLL, dt, dtPRI, "Automated FE Weekly Positive Report");
    }
    protected void SendMailExcelReport(byte[] bytes, byte[] Wali, byte[] PRI, DataTable DtNotEligible, DataTable dt, DataTable dtPRI, string FileName)
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/EmailForReport.htm")))
        {
            body = reader.ReadToEnd();
        }
        SendMail s;
        s = new SendMail();
        s.CreateInvoiceMailForByte1("verification@rcpl.in", "Shyam.Prasad@airtel.com", FileName, body, "", bytes, Wali, PRI, "verification@rcpl.in", "Gagan@rcpl.in");
        // s.CreateInvoiceMailForByte1("verification@rcpl.in", "mohdwali@globalitpoint.com", FileName, body, "", bytes, Wali, PRI, "", "Gagan@rcpl.in");
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        if (result == true && exMsg == "")
        {
            diverror.Visible = true;
            diverror.InnerHtml = "Report send to purushottam successfully.";
            diverror.Attributes.Add("class", "alert alert-success");
        }
        else
        {
            diverror.Visible = true;
            diverror.InnerHtml = exMsg;
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
}