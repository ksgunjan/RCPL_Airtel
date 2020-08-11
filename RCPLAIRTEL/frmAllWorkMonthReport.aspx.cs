using BuisnessLayer;
using ClosedXML.Excel;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmAllWorkMonthReport : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private Common Co = new Common();
    private DataTable DTSearch = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindcurrentmontheport();
            bindcurrentmontheportmo();
            bindcurrentmontheportpri();
        }
    }
    protected void bindcurrentmontheport()
    {
        DTSearch = Lo.RetriveCodeReport3("", "", "LLReport");
        if (DTSearch.Rows.Count > 0)
        {
            gvfemonthwisereport.DataSource = DTSearch;
            gvfemonthwisereport.DataBind();
            gvfemonthwisereport.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void bindcurrentmontheportmo()
    {
        DTSearch = Lo.RetriveCodeReport3("", "", "MOReport");
        if (DTSearch.Rows.Count > 0)
        {

            gvmo.DataSource = DTSearch;
            gvmo.DataBind();
            gvmo.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void bindcurrentmontheportpri()
    {
        DTSearch = Lo.RetriveCodeReport3("", "", "PRIReport");
        if (DTSearch.Rows.Count > 0)
        {
            gvprifixedline.DataSource = DTSearch;
            gvprifixedline.DataBind();
            gvprifixedline.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtll = Lo.RetriveCodeReport3("", "", "LLReport");
            DataTable dtmo = Lo.RetriveCodeReport3("", "", "MOReport");
            DataTable dtPRI = Lo.RetriveCodeReport3("", "", "PRIReport");
            ExportExcel(dtll, dtmo, dtPRI, "DashBoard Monthly Work Report (FE Audit Date Wise)");
            DataTable update = Lo.RetriveCodeReport3(DateTime.Now.ToString("dd/MMM/yyyy"), "", "UpdateStatus");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void ExportExcel(DataTable dtll, DataTable dtmo, DataTable dtPRI, string FileName)
    {
        byte[] wali;
        byte[] bytes;
        byte[] pri;
        using (XLWorkbook wb = new XLWorkbook())
        {
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
            wbn.Worksheets.Add(dtmo);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbn.SaveAs(memoryStream);
                wali = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        using (XLWorkbook wbp = new XLWorkbook())
        {
            wbp.Worksheets.Add(dtPRI);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbp.SaveAs(memoryStream);
                pri = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        if (dtll.Rows.Count > 0 || dtmo.Rows.Count > 0 || dtPRI.Rows.Count > 0)
        {
            SendMailExcelReport(bytes, wali, pri, dtll, dtmo, dtPRI, FileName);
        }
    }
    protected void SendMailExcelReport(byte[] bytes, byte[] wali, byte[] pri, DataTable dtll, DataTable dtmo, DataTable dtPRI, string FileName)
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/EmailForReport.htm")))
        {
            body = reader.ReadToEnd();
        }
        SendMail s;
        s = new SendMail();
        s.CreateInvoiceMailForByte1("verification@rcpl.in", "Shyam.Prasad@airtel.com", FileName.ToString(), body, "", bytes, wali, pri, "verification@rcpl.in", "gagan@rcpl.in");
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

    private decimal sumFooterValue = 0;
    protected void gvfemonthwisereport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Negative = ((Label)e.Row.FindControl("lblnegative")).Text;
            string Positive = ((Label)e.Row.FindControl("lblpositive")).Text;
            string NotEligi = ((Label)e.Row.FindControl("lblnoteligible")).Text;
            string Revisit = ((Label)e.Row.FindControl("lblrevist")).Text;

            if (Negative == "0" && Positive == "0" && NotEligi == "0" && Revisit == "0")
            {
                e.Row.Visible = false;
            }
            else
            {
                e.Row.Visible = true;
            }

            if (Negative == "")
            {
                Negative = "0";
                ((Label)e.Row.FindControl("lblnegative")).Text = Negative.ToString();
            }
            if (Positive == "")
            {
                Positive = "0";
                ((Label)e.Row.FindControl("lblpositive")).Text = Positive.ToString();
            }
            if (Revisit == "")
            {
                Revisit = "0";
                ((Label)e.Row.FindControl("lblrevist")).Text = Revisit.ToString();
            }
            if (NotEligi == "")
            {
                NotEligi = "0";
                ((Label)e.Row.FindControl("lblnoteligible")).Text = Revisit.ToString();
            }
            decimal totalvalue = Convert.ToDecimal(Negative) + Convert.ToDecimal(Positive) + Convert.ToDecimal(Revisit) + Convert.ToDecimal(NotEligi);
            e.Row.Cells[6].Text = totalvalue.ToString();
            sumFooterValue += totalvalue;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblTotal");
            lbl.Text = sumFooterValue.ToString();
        }
    }
    protected void gvmo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Negative = ((Label)e.Row.FindControl("lblnegativemo")).Text;
            string Positive = ((Label)e.Row.FindControl("lblpositivemo")).Text;
            string NotEligi = ((Label)e.Row.FindControl("lblnoteligiblemo")).Text;
            string Revisit = ((Label)e.Row.FindControl("lblrevistmo")).Text;

            if (Negative == "0" && Positive == "0" && NotEligi == "0" && Revisit == "0")
            {
                e.Row.Visible = false;
            }
            else
            {
                e.Row.Visible = true;
            }

            if (Negative == "")
            {
                Negative = "0";
                ((Label)e.Row.FindControl("lblnegativemo")).Text = Negative.ToString();
            }
            if (Positive == "")
            {
                Positive = "0";
                ((Label)e.Row.FindControl("lblpositivemo")).Text = Positive.ToString();
            }
            if (Revisit == "")
            {
                Revisit = "0";
                ((Label)e.Row.FindControl("lblrevistmo")).Text = Revisit.ToString();
            }
            if (NotEligi == "")
            {
                NotEligi = "0";
                ((Label)e.Row.FindControl("lblnoteligiblemo")).Text = NotEligi.ToString();
            }
            decimal totalvalue = Convert.ToDecimal(Negative) + Convert.ToDecimal(Positive) + Convert.ToDecimal(Revisit) + Convert.ToDecimal(NotEligi);
            e.Row.Cells[6].Text = totalvalue.ToString();
            sumFooterValue += totalvalue;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblTotalmo");
            lbl.Text = sumFooterValue.ToString();
        }
    }
    protected void gvprifixedline_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Negative = ((Label)e.Row.FindControl("lblnegativepri")).Text;
            string Positive = ((Label)e.Row.FindControl("lblpositivepri")).Text;
            string NotEligi = ((Label)e.Row.FindControl("lblnoteligiblepri")).Text;
            string Revisit = ((Label)e.Row.FindControl("lblrevistpri")).Text;
            if (Negative == "0" && Positive == "0" && NotEligi == "0" && Revisit == "0")
            {
                e.Row.Visible = false;
            }
            else
            {
                e.Row.Visible = true;
            }

            if (Negative == "")
            {
                Negative = "0";
                ((Label)e.Row.FindControl("lblnegativepri")).Text = Negative.ToString();
            }
            if (Positive == "")
            {
                Positive = "0";
                ((Label)e.Row.FindControl("lblpositivepri")).Text = Positive.ToString();
            }
            if (Revisit == "")
            {
                Revisit = "0";
                ((Label)e.Row.FindControl("lblrevistpri")).Text = Revisit.ToString();
            }
            if (NotEligi == "")
            {
                NotEligi = "0";
                ((Label)e.Row.FindControl("lblnoteligiblepri")).Text = Revisit.ToString();
            }
            decimal totalvalue = Convert.ToDecimal(Negative) + Convert.ToDecimal(Positive) + Convert.ToDecimal(Revisit) + Convert.ToDecimal(NotEligi);
            e.Row.Cells[6].Text = totalvalue.ToString();
            sumFooterValue += totalvalue;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lbltotalpri");
            lbl.Text = sumFooterValue.ToString();
        }
    }
}