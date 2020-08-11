using BuisnessLayer;
using ClosedXML.Excel;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmDaywiseFeedBackReport : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private DataTable dt = new DataTable();
    private DateTime date;
    private string datefinal = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
            DtLL = Lo.RetriveCodeReport7("" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "", "", "LLReport");
        }
        else
        {
            DtLL = Lo.RetriveCodeReport7("" + datefinal + "", "", "LLReport");
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
        if (datefinal == "")
        {
            dt = Lo.RetriveFunctionCode5("MO", DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy"), "", "", "");
        }
        else
        {
            dt = Lo.RetriveFunctionCode5("MO", datefinal, "", "", "");
        }
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
    protected void BindPRI(string datefinal)
    {
        DataTable DtPRI = new DataTable();
        if (datefinal == "")
        {
            DtPRI = Lo.RetriveCodeReport7("" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "", "", "PRIReport");
        }
        else
        {
            DtPRI = Lo.RetriveCodeReport7("" + datefinal + "", "", "PRIReport");
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
                DtLL = Lo.RetriveCodeReport7("" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "", "", "LLReport");
            }
            else
            {
                DtLL = Lo.RetriveCodeReport7("'" + datefinal + "'", "", "LLReport");
            }
            BindMo(datefinal);
            if (datefinal == "")
            {
                DtPRI = Lo.RetriveCodeReport7("" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "", "", "PRIReport");
            }
            else
            {
                DtPRI = Lo.RetriveCodeReport7("" + datefinal + "", "", "PRIReport");
            }
            ExportExcel(DtLL, dt, DtPRI, "Automated FE Day Wise Feedback Report");
            DataTable update = Lo.RetriveCodeReport7(DateTime.Now.ToString("dd/MMM/yyyy"), "", "Insert");
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
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), Convert.ToInt16(ConfigurationManager.AppSettings["Port"].ToString()), ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        if (result == true && exMsg == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Report send successfully')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + exMsg + "')", true);
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