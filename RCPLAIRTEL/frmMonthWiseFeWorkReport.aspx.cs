using BuisnessLayer;
using ClosedXML.Excel;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmMonthWiseFeWorkReport : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private Common Co = new Common();
    private DataTable DTSearch = new DataTable();
    private DataTable DTSearchMO = new DataTable();
    private DataTable DTSearchPRI = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindcurrentmontheport();
            bindcurrentmontheportMO();
            bindcurrentmontheportPRI();
        }
    }

    protected void cleartext()
    {
        ddlmonth.Text = "Select Month";
    }
    protected void bindcurrentmontheport()
    {
        DTSearch = Lo.RetriveCodeReport5("", "", "LLReport");
        if (DTSearch.Rows.Count > 0)
        {
            gvfedailyreport.DataSource = DTSearch;
            gvfedailyreport.DataBind();
            gvfedailyreport.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void bindcurrentmontheportMO()
    {
        DTSearch = Lo.RetriveCodeReport5("", "", "MOReport");
        if (DTSearch.Rows.Count > 0)
        {
            gridviewMo.DataSource = DTSearch;
            gridviewMo.DataBind();
            gridviewMo.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void bindcurrentmontheportPRI()
    {
        DTSearch = Lo.RetriveCodeReport5("", "", "PRIData");
        if (DTSearch.Rows.Count > 0)
        {
            gvpri.DataSource = DTSearch;
            gvpri.DataBind();
            gvpri.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlmonth.SelectedItem.Text != "Select Month")
            {
                DTSearch = Lo.RetriveCodeReport5(ddlmonth.SelectedItem.Text, "'" + ddlmonth.SelectedItem.Text + "'", "LLRepSerch");
                if (DTSearch.Rows.Count > 0)
                {
                    gvfedailyreport.DataSource = DTSearch;
                    gvfedailyreport.DataBind();
                    gvfedailyreport.Visible = true;
                    divexcel.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Successfully fetch result')", true);
                }
                else
                {
                    gvfedailyreport.Visible = false;
                    divexcel.Visible = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No Record Found')", true);
                }
                DTSearchMO = Lo.RetriveCodeReport5(ddlmonth.SelectedItem.Text, "'" + ddlmonth.SelectedItem.Text + "'", "MORepSerch");
                if (DTSearch.Rows.Count > 0)
                {
                    gridviewMo.DataSource = DTSearchMO;
                    gridviewMo.DataBind();
                    gridviewMo.Visible = true;
                    div1.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Successfully fetch result')", true);
                }
                else
                {
                    gridviewMo.Visible = false;
                    div1.Visible = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No Record Found')", true);
                }
                DTSearchPRI = Lo.RetriveCodeReport5(ddlmonth.SelectedItem.Text, "'" + ddlmonth.SelectedItem.Text + "'", "PRIRepSerch");
                if (DTSearch.Rows.Count > 0)
                {
                    gvpri.DataSource = DTSearchPRI;
                    gvpri.DataBind();
                    gvpri.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Successfully fetch result')", true);
                }
                else
                {
                    gvpri.Visible = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No Record Found')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Select Month')", true);
            }
        }
        catch (Exception)
        {
            div1.Visible = false;
            gridviewMo.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No Record Found')", true);
        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlmonth.SelectedItem.Text != "Select Month")
            {
                DTSearch = Lo.RetriveCodeReport5(ddlmonth.SelectedItem.Text, "", "LLRepSerch");
                DTSearchMO = Lo.RetriveCodeReport5(ddlmonth.SelectedItem.Text, "", "MORepSerch");
                DTSearchPRI = Lo.RetriveCodeReport5(ddlmonth.SelectedItem.Text, "", "PRIRepSerch");
                if (DTSearchMO.Rows.Count > 0 || DTSearch.Rows.Count > 0 || DTSearchPRI.Rows.Count > 0)
                {
                    ExportExcel(DTSearch, DTSearchMO, DTSearchPRI, "Automated FE Current Month Day Wise Work Report");
                }
            }
            else
            {
                DTSearch = Lo.RetriveCodeReport5("", "", "LLReport");
                DTSearchMO = Lo.RetriveCodeReport5("", "", "MOReport");
                DTSearchPRI = Lo.RetriveCodeReport5("", "", "PRIData");
                if (DTSearchMO.Rows.Count > 0 || DTSearch.Rows.Count > 0 || DTSearchPRI.Rows.Count > 0)
                {
                    ExportExcel(DTSearch, DTSearchMO, DTSearchPRI, "Automated FE Current Month Day Wise Work Report");
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void ExportExcel(DataTable DtNotEligible, DataTable DtNotEligibleMO, DataTable dtPRI, string FileName)
    {
        byte[] bytes;
        byte[] Wali;
        byte[] PRI;
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(DtNotEligible);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                bytes = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        using (XLWorkbook wb1 = new XLWorkbook())
        {
            wb1.Worksheets.Add(DtNotEligibleMO);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb1.SaveAs(memoryStream);
                Wali = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        using (XLWorkbook wbpri = new XLWorkbook())
        {
            wbpri.Worksheets.Add(dtPRI);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbpri.SaveAs(memoryStream);
                PRI = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        SendMailExcelReport(bytes, Wali, PRI, DtNotEligible, DtNotEligibleMO, dtPRI);

    }
    protected void SendMailExcelReport(byte[] bytes, byte[] Wali, byte[] PRI, DataTable DtNotEligible, DataTable DtNotEligibleMO, DataTable dtPRI)
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/EmailForReport.htm")))
        {
            body = reader.ReadToEnd();
        }
        SendMail s;
        s = new SendMail();
        s.CreateInvoiceMailForByte1("verification@rcpl.in", "Shyam.Prasad@airtel.com", "Automated FE Current Month Day Wise Work Report", body, "", bytes, Wali, PRI, "verification@rcpl.in,sales@rcpl.in,bs@rcpl.in", "gagan@rcpl.in");
        //  s.CreateInvoiceMailForByte1("verification@rcpl.in", "mohdwali@globalitpoint.com", "FE Daily Work Report", body, "", bytes, Wali, "verification@rcpl.in","");
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), Convert.ToInt16(ConfigurationManager.AppSettings["Port"].ToString()), ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        if (result == true && exMsg == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Report send to Shyam Prasad successfully.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + exMsg + "')", true);

        }
    }
    protected void gvfedailyreport_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gridviewMo_RowCreated(object sender, GridViewRowEventArgs e)
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