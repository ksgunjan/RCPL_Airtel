using System;
using System.Data;
using BusinessLayer;
using System.IO;
using System.Configuration;
using ClosedXML.Excel;

public partial class frmDaywiseFeedBackReport : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataTable dt = new DataTable();
    DateTime date;
    string datefinal = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            BindLL(datefinal);
            BindMo(datefinal);
            BindPRI(datefinal);
        }
    }
    protected void BindLL(string datefinal)
    {
        DataTable DtLL = new DataTable();
        if (datefinal == "")
        {
            DtLL = Lo.RetriveCodeWithContidion("select [Logical Circuit Id],[Party Name],[FE Name], StatusByFE,Code, RemarksOfStatus as Remarks ,StatusTime,StatusUpdateDate,[Customer Name],PersonMetMobileNo,[Mobile No],PersonMetEmail from tbl_trn_RawData where ProductName='Leased Line' and StatusUpdateDate='" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "'");
        }
        else
        {
            DtLL = Lo.RetriveCodeWithContidion("select  [Logical Circuit Id],[Party Name],[FE Name], StatusByFE,Code, RemarksOfStatus as Remarks ,StatusTime,StatusUpdateDate,[Customer Name],PersonMetMobileNo,[Mobile No],PersonMetEmail from tbl_trn_RawData where ProductName='Leased Line' and StatusUpdateDate='" + datefinal + "'");
        }
        if (DtLL.Rows.Count > 0)
        {
            gvll.DataSource = DtLL;
            gvll.DataBind();
            gvll.Visible = true;
        }
        else
        {
            gvll.Visible = false;
        }
    }
    protected void BindMo(string datefinal)
    {
        //DataTable DtAssignJob = new DataTable();
        //string compname;
        //dt.Columns.Add("Billed Ext ID", typeof(string));
        //dt.Columns.Add("Party Name", typeof(string));
        //dt.Columns.Add("FE Name", typeof(string));
        //dt.Columns.Add("StatusByFe", typeof(string));
        //dt.Columns.Add("Code", typeof(string));
        //dt.Columns.Add("Remarks", typeof(string));
        //dt.Columns.Add("StatusTime", typeof(string));
        //dt.Columns.Add("StatusUpdateDate", typeof(DateTime));
        //dt.Columns.Add("PersonMetName", typeof(string));
        //dt.Columns.Add("PersonMetMobileNo", typeof(string));
        //dt.Columns.Add("PersonMetEmail", typeof(string));
        //dt.Columns.Add("Coordinator_Contact_Number", typeof(string));
       // DataTable DtFetchcompgropby = new DataTable();
        if (datefinal == "")
        {
            dt = Lo.RetriveCodeWithContidion("select * from fn_DailyDayWiseFeedBackMo('MO','" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "') ");
        }
        else
        {
            dt = Lo.RetriveCodeWithContidion("select * from fn_DailyDayWiseFeedBackMo('MO','" + datefinal + "')");
        }
        //if (DtFetchcompgropby.Rows.Count > 0)
        //{
        //    for (int i = 0; DtFetchcompgropby.Rows.Count > i; i++)
        //    {
        //        compname = DtFetchcompgropby.Rows[i]["Party Name"].ToString();
        //        if (datefinal == "")
        //        {
        //            DtAssignJob = Lo.RetriveCodeWithContidion("SELECT  top 1  [Billed Ext ID],[Party Name],[FE Name],StatusByFe,Code,RemarksOfStatus as Remarks,StatusTime,StatusUpdateDate,PersonMetName,PersonMetMobileNo,PersonMetEmail,Coordinator_Contact_Number from tbl_trn_RawData where IsActive='Y' and IsClosed !='Y' and ExcelVerifiy='2'  and (StatusByFE!='' or StatusByFE is not null) and [Eligible/Not Eligible]='Eligible' and ProductName='MO' and StatusUpdateDate='" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "' and [Party Name]= '" + compname + "' order by [FE Name] asc");
        //        }
        //        else
        //        {
        //            DtAssignJob = Lo.RetriveCodeWithContidion("SELECT  top 1  [Billed Ext ID],[Party Name],[FE Name],StatusByFe,Code,RemarksOfStatus as Remarks,StatusTime,StatusUpdateDate,PersonMetName,PersonMetMobileNo,Coordinator_Contact_Number from tbl_trn_RawData where IsActive='Y' and IsClosed !='Y' and ExcelVerifiy='2'  and (StatusByFE!='' or StatusByFE is not null) and [Eligible/Not Eligible]='Eligible' and ProductName='MO' and StatusUpdateDate='" + datefinal + "' and [Party Name]= '" + compname + "' order by [FE Name] asc");
        //        }

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
    protected void BindPRI(string datefinal)
    {
        DataTable DtPRI = new DataTable();
        if (datefinal == "")
        {
            DtPRI = Lo.RetriveCodeWithContidion("select [Subs Del No], [Party Name],[FE Name], StatusByFE,Code, RemarksOfStatus as Remarks ,StatusTime,StatusUpdateDate,[Customer Name],PersonMetMobileNo,PersonMetEmail,Coordinator_Contact_Number from tbl_trn_RawData where ProductName='PRI-Fixed Line' and StatusUpdateDate='" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "'");
        }
        else
        {
            DtPRI = Lo.RetriveCodeWithContidion("select  [Subs Del No],[Party Name],[FE Name], StatusByFE,Code, RemarksOfStatus as Remarks ,StatusTime,StatusUpdateDate,[Customer Name],PersonMetMobileNo,PersonMetEmail,Coordinator_Contact_Number from tbl_trn_RawData where ProductName='PRI-Fixed Line' and StatusUpdateDate='" + datefinal + "'");
        }
        if (DtPRI.Rows.Count > 0)
        {
            gvpri.DataSource = DtPRI;
            gvpri.DataBind();
            gvpri.Visible = true;
        }
        else
        {
            gvpri.Visible = false;
        }
    }
    protected void btnmail_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable DtLL = new DataTable();
            DataTable DtPRI = new DataTable();
            if (datefinal == "")
            {
                DtLL = Lo.RetriveCodeWithContidion("select  [Logical Circuit Id],[Party Name],[FE Name], StatusByFE,Code, RemarksOfStatus  as Remarks ,StatusTime,StatusUpdateDate,[Customer Name],PersonMetMobileNo,PersonMetEmail,[Mobile No] from tbl_trn_RawData where ProductName='Leased Line' and StatusUpdateDate='" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "'");
            }
            else
            {
                DtLL = Lo.RetriveCodeWithContidion("select  [Logical Circuit Id],[Party Name],[FE Name], StatusByFE,Code, RemarksOfStatus  as Remarks ,StatusTime,StatusUpdateDate,[Customer Name],PersonMetMobileNo,PersonMetEmail,[Mobile No] from tbl_trn_RawData where ProductName='Leased Line' and StatusUpdateDate='" + datefinal + "'");
            }
            BindMo(datefinal);
            if (datefinal == "")
            {
                DtPRI = Lo.RetriveCodeWithContidion("select  [Subs Del No],[Party Name],[FE Name], StatusByFE,Code, RemarksOfStatus  as Remarks ,StatusTime,StatusUpdateDate,[Customer Name],PersonMetMobileNo,PersonMetEmail,Coordinator_Contact_Number from tbl_trn_RawData where ProductName='PRI-Fixed Line' and StatusUpdateDate='" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "'");
            }
            else
            {
                DtPRI = Lo.RetriveCodeWithContidion("select  [Subs Del No],[Party Name],[FE Name], StatusByFE,Code, RemarksOfStatus  as Remarks ,StatusTime,StatusUpdateDate,[Customer Name],PersonMetMobileNo,PersonMetEmail,Coordinator_Contact_Number from tbl_trn_RawData where ProductName='PRI-Fixed Line' and StatusUpdateDate='" + datefinal + "'");
            }
            ExportExcel(DtLL, dt, DtPRI, "Automated FE Day Wise Feedback Report");
            int update = Lo.InsertNormal("insert into tbl_trn_AutoMailSendStatus (Category,MailDate,IsSend) values ('FELastDayWorkReportDetail','" + DateTime.Now.ToString("dd/MMM/yyyy") + "','Y')");
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
        using (XLWorkbook wb1 = new XLWorkbook())
        {
            dt.TableName = "ExcelsheetMoDailyWork";
            wb1.Worksheets.Add(dt);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb1.SaveAs(memoryStream);
                Wali = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        using (XLWorkbook wb = new XLWorkbook())
        {
            DtLL.TableName = "ExcelsheetLeasedLineDailyWork";
            wb.Worksheets.Add(DtLL);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                bytes = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        using (XLWorkbook wbPRI = new XLWorkbook())
        {
            dtPRI.TableName = "ExcelsheetPRIDailyWork";
            wbPRI.Worksheets.Add(dtPRI);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbPRI.SaveAs(memoryStream);
                PRI = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        if (DtLL.Rows.Count > 0 || dt.Rows.Count > 0 || dtPRI.Rows.Count > 0)
        {
            SendMailExcelReport(bytes, Wali, PRI, DtLL, dt, dtPRI, FileName);
        }
    }
    protected void SendMailExcelReport(byte[] bytes, byte[] Wali, byte[] PRI, DataTable DtLL, DataTable dt, DataTable dtPRI, string FileName)
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/EmailForReport.htm")))
        {
            body = reader.ReadToEnd();
        }
        SendMail s;
        s = new SendMail();
        s.CreateInvoiceMailForByte1("verification@rcpl.in", "verification@rcpl.in", FileName.ToString(), body, "", bytes, Wali, PRI, "bs1@rcpl.in", "gagan@rcpl.in");
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        if (result == true && exMsg == "")
        {
            diverror.Visible = true;
            diverror.InnerHtml = "Report send successfully.";
            diverror.Attributes.Add("class", "alert alert-success");
        }
        else
        {
            diverror.Visible = true;
            diverror.InnerHtml = exMsg;
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (txtsearch.Text != "")
        {
            date = Convert.ToDateTime(txtsearch.Text);
            datefinal = date.ToString("dd-MMM-yyyy");
            BindLL(datefinal);
            BindMo(datefinal);
            BindPRI(datefinal);
        }
        else
        {
            date = Convert.ToDateTime(txtsearch.Text);
            datefinal = "";
            BindLL(datefinal);
            BindMo(datefinal);
            BindPRI(datefinal);
        }
    }
}