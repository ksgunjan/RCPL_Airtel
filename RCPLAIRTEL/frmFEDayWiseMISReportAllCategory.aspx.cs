using System;
using System.Web.UI.WebControls;
using System.Data;
using BuisnessLayer;
using System.IO;
using System.Configuration;
using ClosedXML.Excel;
using System.Web.UI;

public partial class frmFEDayWiseMISReportAllCategory : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Common Co = new Common();
    DateTime Date;
    String finaldate;
    decimal sumFooterValue = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetMoYesterdayReport();
            GetLLYesterdayReport();
            GetPRIYesterdayReport();
        }
    }
    #region Function
    protected void GetLLYesterdayReport()
    {
        if (txtdate.Text != "")
        {
            Date = Convert.ToDateTime(txtdate.Text);
            finaldate = Date.ToString("dd/MMM/yyyy");
        }
        else
        {
            Date = Convert.ToDateTime(DateTime.Now.AddDays(-1));
            finaldate = Date.ToString("dd/MMM/yyyy");
        }
        DataTable Dtll = Lo.RetriveCodeReport6(finaldate,"", "LLReport");
        if (Dtll.Rows.Count > 0)
        {
            gvll.DataSource = Dtll;
            gvll.DataBind();
            gvll.Visible = true;
        }
    }
    protected void GetMoYesterdayReport()
    {

        if (txtdate.Text != "")
        {
            Date = Convert.ToDateTime(txtdate.Text);
            finaldate = Date.ToString("dd/MMM/yyyy");
        }
        else
        {
            Date = Convert.ToDateTime(DateTime.Now.AddDays(-1));
            finaldate = Date.ToString("dd/MMM/yyyy");
        }
        DataTable DtMo = Lo.RetriveCodeReport6(finaldate, "", "MOReport");
        if (DtMo.Rows.Count > 0)
        {
            gvmo.DataSource = DtMo;
            gvmo.DataBind();
            gvmo.Visible = true;
        }
    }
    protected void GetPRIYesterdayReport()
    {
        if (txtdate.Text != "")
        {
            Date = Convert.ToDateTime(txtdate.Text);
            finaldate = Date.ToString("dd/MMM/yyyy");
        }
        else
        {
            Date = Convert.ToDateTime(DateTime.Now.AddDays(-1));
            finaldate = Date.ToString("dd/MMM/yyyy");
        }
        DataTable DtPRI = Lo.RetriveCodeReport6(finaldate, "", "PRIReport");
        if (DtPRI.Rows.Count > 0)
        {
            gvpri.DataSource = DtPRI;
            gvpri.DataBind();
            gvpri.Visible = true;
        }
    }
    #endregion
    #region Search
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (txtdate.Text != "")
        {
            GetLLYesterdayReport();
            GetMoYesterdayReport();
            GetPRIYesterdayReport();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Please select date')", true);
        }
    }
    #endregion
    #region GridItemBound
    protected void gvll_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Negative = ((Label)e.Row.FindControl("lblnegative")).Text;
            string Positive = ((Label)e.Row.FindControl("lblpositive")).Text;
            string Revisit = ((Label)e.Row.FindControl("lblpending")).Text;
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
                ((Label)e.Row.FindControl("lblpending")).Text = Revisit.ToString();
            }
            decimal totalvalue = Convert.ToDecimal(Negative) + Convert.ToDecimal(Positive) + Convert.ToDecimal(Revisit);
            e.Row.Cells[5].Text = totalvalue.ToString();
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
            string Revisit = ((Label)e.Row.FindControl("lblpendingmo")).Text;
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
                ((Label)e.Row.FindControl("lblpendingmo")).Text = Revisit.ToString();
            }
            decimal totalvalue = Convert.ToDecimal(Negative) + Convert.ToDecimal(Positive) + Convert.ToDecimal(Revisit);
            e.Row.Cells[5].Text = totalvalue.ToString();
            sumFooterValue += totalvalue;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblTotalmo");
            lbl.Text = sumFooterValue.ToString();
        }

    }
    protected void gvpri_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Negative = ((Label)e.Row.FindControl("lblnegativepri")).Text;
            string Positive = ((Label)e.Row.FindControl("lblpositivepri")).Text;
            string Revisit = ((Label)e.Row.FindControl("lblpendingpri")).Text;
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
                ((Label)e.Row.FindControl("lblpendingpri")).Text = Revisit.ToString();
            }
            decimal totalvalue = Convert.ToDecimal(Negative) + Convert.ToDecimal(Positive) + Convert.ToDecimal(Revisit);
            e.Row.Cells[5].Text = totalvalue.ToString();
            sumFooterValue += totalvalue;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblTotalpri");
            lbl.Text = sumFooterValue.ToString();
        }
    }
    #endregion
    #region SendMailOrExcelCode
    protected void btngetexcelsendmail_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdate.Text != "")
            {
                Date = Convert.ToDateTime(txtdate.Text);
                finaldate = Date.ToString("dd/MMM/yyyy");
            }
            else
            {
                Date = Convert.ToDateTime(DateTime.Now.AddDays(-1));
                finaldate = Date.ToString("dd/MMM/yyyy");
            }
            DataTable DtL = Lo.RetriveCodeReport6(finaldate, "", "LLReport");
            DataTable DtM = Lo.RetriveCodeReport6(finaldate, "", "MOReport");
            DataTable DtP = Lo.RetriveCodeReport6(finaldate, "", "PRIReport");
            if (DtL.Rows.Count > 0 || DtM.Rows.Count > 0 || DtP.Rows.Count > 0)
            {
                ExportExcel(DtL, DtM, DtP);
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void ExportExcel(DataTable LL, DataTable Mo, DataTable PR)
    {
        byte[] bytes;
        byte[] Wali;
        byte[] Pri;
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(LL);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                bytes = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        using (XLWorkbook wb1 = new XLWorkbook())
        {
            wb1.Worksheets.Add(Mo);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb1.SaveAs(memoryStream);
                Wali = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        using (XLWorkbook wbpri = new XLWorkbook())
        {
            wbpri.Worksheets.Add(PR);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbpri.SaveAs(memoryStream);
                Pri = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        SendMailExcelReport(bytes, Wali, Pri, LL, Mo, PR);
    }
    protected void SendMailExcelReport(byte[] bytes, byte[] Wali, byte[] Pri, DataTable LM, DataTable MM, DataTable PR)
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/EmailForReport.htm")))
        {
            body = reader.ReadToEnd();
        }
        SendMail s;
        s = new SendMail();
        s.CreateInvoiceMailForByte1("verification@rcpl.in", "verification@rcpl.in", "Automated FE Daily Work Report", body, "", bytes, Wali, Pri, "bs1@rcpl.in", "gagan@rcpl.in");
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), Convert.ToInt16(ConfigurationManager.AppSettings["Port"].ToString()), ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        if (result == true && exMsg == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Report send successfully.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('"+ exMsg + "')", true);
        }
    }
    #endregion
}