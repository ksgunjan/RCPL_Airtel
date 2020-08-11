using BuisnessLayer;
using ClosedXML.Excel;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmNegativeWeeklyReport : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLL();
            BindMo();
            BindPRI();
        }
    }
    protected void BindLL()
    {
        DataTable DtLL = Lo.RetriveCodeReport8("" + DateTime.Now.AddDays(-7).ToString("yyyy/MMM/dd") + "", "" + DateTime.Now.AddDays(-1).ToString("yyyy/MMM/dd") + "", "LLReport");
        if (DtLL.Rows.Count > 0)
        {
            gvll.DataSource = DtLL;
            gvll.DataBind();
            gvll.Visible = true;
        }
    }
    protected void BindMo()
    {
        dt = Lo.RetriveFunctionCode6("'MO'", "" + DateTime.Now.AddDays(-7).ToString("yyyy/MMM/dd") + "", "" + DateTime.Now.AddDays(-1).ToString("yyyy/MMM/dd") + "", "'Negative'", "");
        if (dt.Rows.Count > 0)
        {
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
        }
    }
    protected void BindPRI()
    {
        DataTable DtPRI = Lo.RetriveCodeReport8("" + DateTime.Now.AddDays(-7).ToString("yyyy/MMM/dd") + "", "" + DateTime.Now.AddDays(-1).ToString("yyyy/MMM/dd") + "", "PRIReport");
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
            DataTable DtLL = Lo.RetriveCodeReport8("" + DateTime.Now.AddDays(-7).ToString("yyyy/MMM/dd") + "", "" + DateTime.Now.AddDays(-1).ToString("yyyy/MMM/dd") + "", "LLReport");
            BindMo();
            DataTable DtPRI = Lo.RetriveCodeReport8("" + DateTime.Now.AddDays(-7).ToString("yyyy/MMM/dd") + "", "" + DateTime.Now.AddDays(-1).ToString("yyyy/MMM/dd") + "", "PRIReport");
            if (DtLL.Rows.Count > 0 || dt.Rows.Count > 0 || DtPRI.Rows.Count > 0)
            {
                ExportExcel(DtLL, dt, DtPRI, "Automated FE Weekly Negative Report");
                DataTable update = Lo.RetriveCodeReport8("" + DateTime.Now.ToString("yyyy/MMM/dd") + "", "", "Insert");
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
            DtLL.TableName = "Negative Report Leased Line";
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
            dt.TableName = "Negative Report MO";
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
            dtPRI.TableName = "Negative Report PRI";
            wbpri.Worksheets.Add(dtPRI);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbpri.SaveAs(memoryStream);
                PRI = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        SendMailExcelReport(bytes, Wali, PRI, DtLL, dt, dtPRI, "Automated FE Weekly Negative Report");
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
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), Convert.ToInt16(ConfigurationManager.AppSettings["Port"].ToString()), ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        if (result == true && exMsg == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Report send to Shyam Prasad successfully.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('"+ exMsg + "')", true);          
        }
    }

    protected void gvll_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void gvmo_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void gvpri_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
}