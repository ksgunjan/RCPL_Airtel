﻿using System;
using System.Web.UI.WebControls;
using System.Data;
using BuisnessLayer;
using System.IO;
using System.Configuration;
using ClosedXML.Excel;
using System.Web.UI;

public partial class frmFEDashBoardReportMonthlySubCatWiseAllocatedDateWise : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Common Co = new Common();
    DataTable DTSearch = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindcurrentmontheport1();
            bindcurrentmontheportmo2();
            bindcurrentmontheportPRI3();
            bindcurrentmontheport4();
            bindcurrentmontheportmo5();
        }
    }
    protected void bindcurrentmontheport1()
    {
        DTSearch = Lo.RetriveCodeReport2("","", "LLReport");
        if (DTSearch.Rows.Count > 0)
        {

            gvfemonthwisereport.DataSource = DTSearch;
            gvfemonthwisereport.DataBind();
            gvfemonthwisereport.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void bindcurrentmontheportmo2()
    {
        DTSearch = Lo.RetriveCodeReport2("","", "MOReport");
        if (DTSearch.Rows.Count > 0)
        {
            gvmo.DataSource = DTSearch;
            gvmo.DataBind();
            gvmo.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void bindcurrentmontheportPRI3()
    {
        DTSearch = Lo.RetriveCodeReport2("","", "PRIReport");
        if (DTSearch.Rows.Count > 0)
        {
            gvprifixedline.DataSource = DTSearch;
            gvprifixedline.DataBind();
            gvprifixedline.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void bindcurrentmontheport4()
    {
        DTSearch = Lo.RetriveCodeReport2("","", "LLRepoType2");
        if (DTSearch.Rows.Count > 0)
        {

            gvllcat2.DataSource = DTSearch;
            gvllcat2.DataBind();
            gvllcat2.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void bindcurrentmontheportmo5()
    {
        DTSearch = Lo.RetriveCodeReport2("","", "MORepoType2");
        if (DTSearch.Rows.Count > 0)
        {
            gvmocat2.DataSource = DTSearch;
            gvmocat2.DataBind();
            gvmocat2.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtll = Lo.RetriveCodeReport2("","", "LLMail");
            DataTable dtmo = Lo.RetriveCodeReport2("","", "MOMail");
            DataTable dtpri = Lo.RetriveCodeReport2("","", "PRIMail");
            if (dtll.Rows.Count > 0 || dtmo.Rows.Count > 0 || dtpri.Rows.Count > 0)
            {
                ExportExcel(dtll, dtmo, dtpri, "DashBoard Monthly Work Report (FE StatusUpdate Date Wise)");
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void ExportExcel(DataTable dtll, DataTable dtmo, DataTable dtpri, string FileName)
    {
        byte[] bytes;
        byte[] Wali;
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
        using (XLWorkbook wb1 = new XLWorkbook())
        {
            wb1.Worksheets.Add(dtmo);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb1.SaveAs(memoryStream);
                Wali = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        using (XLWorkbook wbpri = new XLWorkbook())
        {
            wbpri.Worksheets.Add(dtpri);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbpri.SaveAs(memoryStream);
                pri = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        SendMailExcelReport(bytes, Wali, pri, dtll, dtmo, dtpri, FileName);
    }
    protected void SendMailExcelReport(byte[] bytes, byte[] wali, byte[] pri, DataTable dtll, DataTable dtmo, DataTable dtpri, string FileName)
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/EmailForReport.htm")))
        {
            body = reader.ReadToEnd();
        }
        SendMail s;
        s = new SendMail();
        s.CreateInvoiceMailForByte1("verification@rcpl.in", "Shyam.Prasad@airtel.com", FileName, body, "", bytes, wali, pri, "verification@rcpl.in", "gagan@rcpl.in");
        //s.CreateInvoiceMailForByte1("verification@rcpl.in", "mohdwaliahmad@gmail.com", "Monthly Work Report", body, "", bytes, wali, "verification@rcpl.in");
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), Convert.ToInt16(ConfigurationManager.AppSettings["Port"].ToString()), ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        // bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 587, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        if (result == true && exMsg == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Report send to Shyam Prasad successfully.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + exMsg + "')", true);
        }
    }
    decimal sumFooterValue = 0;
    protected void gvfemonthwisereport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string NotEligible = ((Label)e.Row.FindControl("lblnoteligible")).Text;
            string Negative = ((Label)e.Row.FindControl("lblnegative")).Text;
            string Positive = ((Label)e.Row.FindControl("lblpositive")).Text;
            string Pending = ((Label)e.Row.FindControl("lblpending")).Text;
            string Revisit = ((Label)e.Row.FindControl("lblrevisit")).Text;
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
                ((Label)e.Row.FindControl("lblrevisit")).Text = Revisit.ToString();
            }
            if (NotEligible == "")
            {
                NotEligible = "0";
                ((Label)e.Row.FindControl("lblnoteligible")).Text = NotEligible.ToString();
            }
            if (Pending == "")
            {
                Pending = "0";
                ((Label)e.Row.FindControl("lblpending")).Text = Pending.ToString();
            }
            decimal totalvalue = Convert.ToDecimal(Negative) + Convert.ToDecimal(Positive) + Convert.ToDecimal(Revisit) + Convert.ToDecimal(NotEligible) + Convert.ToDecimal(Pending);
            e.Row.Cells[7].Text = totalvalue.ToString();
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
            string Revisit = ((Label)e.Row.FindControl("lblrevisitmo")).Text;
            string NotEligi = ((Label)e.Row.FindControl("lblnoteligiblemo")).Text;
            string Pending = ((Label)e.Row.FindControl("lblpendingmo")).Text;
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
                ((Label)e.Row.FindControl("lblrevisitmo")).Text = Revisit.ToString();
            }
            if (NotEligi == "")
            {
                NotEligi = "0";
                ((Label)e.Row.FindControl("lblnoteligiblemo")).Text = NotEligi.ToString();
            }
            if (Pending == "")
            {
                Pending = "0";
                ((Label)e.Row.FindControl("lblpendingmo")).Text = Pending.ToString();
            }
            decimal totalvalue = Convert.ToDecimal(Negative) + Convert.ToDecimal(Positive) + Convert.ToDecimal(Revisit) + Convert.ToDecimal(NotEligi) + Convert.ToDecimal(Pending);
            e.Row.Cells[7].Text = totalvalue.ToString();
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
            string NotEligible = ((Label)e.Row.FindControl("lblnoteligiblepri")).Text;
            string Negative = ((Label)e.Row.FindControl("lblnegativepri")).Text;
            string Positive = ((Label)e.Row.FindControl("lblpositivepri")).Text;
            string Pending = ((Label)e.Row.FindControl("lblpendingpri")).Text;
            string Revisit = ((Label)e.Row.FindControl("lblrevisitpri")).Text;
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
                ((Label)e.Row.FindControl("lblrevisit")).Text = Revisit.ToString();
            }
            if (NotEligible == "")
            {
                NotEligible = "0";
                ((Label)e.Row.FindControl("lblnoteligible")).Text = NotEligible.ToString();
            }
            if (Pending == "")
            {
                Pending = "0";
                ((Label)e.Row.FindControl("lblpendingpri")).Text = Pending.ToString();
            }
            decimal totalvalue = Convert.ToDecimal(Negative) + Convert.ToDecimal(Positive) + Convert.ToDecimal(Revisit) + Convert.ToDecimal(NotEligible) + Convert.ToDecimal(Pending);
            e.Row.Cells[7].Text = totalvalue.ToString();
            sumFooterValue += totalvalue;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lbltotalpri");
            lbl.Text = sumFooterValue.ToString();
        }
    }
    protected void gvllcat2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string NotEligible = ((Label)e.Row.FindControl("lblnoteligiblelcat2")).Text;
            string Negative = ((Label)e.Row.FindControl("lblnegativelcat2")).Text;
            string Positive = ((Label)e.Row.FindControl("lblpositivelcat2")).Text;
            string Pending = ((Label)e.Row.FindControl("lblpendinglcat2")).Text;
            string Revisit = ((Label)e.Row.FindControl("lblrevisitlcat2")).Text;
            if (Negative == "")
            {
                Negative = "0";
                ((Label)e.Row.FindControl("lblnegativelcat2")).Text = Negative.ToString();
            }
            if (Positive == "")
            {
                Positive = "0";
                ((Label)e.Row.FindControl("lblpositivelcat2")).Text = Positive.ToString();
            }
            if (Revisit == "")
            {
                Revisit = "0";
                ((Label)e.Row.FindControl("lblrevisitlcat2")).Text = Revisit.ToString();
            }
            if (NotEligible == "")
            {
                NotEligible = "0";
                ((Label)e.Row.FindControl("lblnoteligiblelcat2")).Text = NotEligible.ToString();
            }
            if (Pending == "")
            {
                Pending = "0";
                ((Label)e.Row.FindControl("lblpendinglcat2")).Text = Pending.ToString();
            }
            decimal totalvalue = Convert.ToDecimal(Negative) + Convert.ToDecimal(Positive) + Convert.ToDecimal(Revisit) + Convert.ToDecimal(NotEligible) + Convert.ToDecimal(Pending);
            e.Row.Cells[7].Text = totalvalue.ToString();
            sumFooterValue += totalvalue;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblTotallcat2");
            lbl.Text = sumFooterValue.ToString();
        }
    }
    protected void gvmocat2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Negative = ((Label)e.Row.FindControl("lblnegativemomcat2")).Text;
            string Positive = ((Label)e.Row.FindControl("lblpositivemomcat2")).Text;
            string Revisit = ((Label)e.Row.FindControl("lblrevisitmomcat2")).Text;
            string NotEligi = ((Label)e.Row.FindControl("lblnoteligiblemomcat2")).Text;
            string Pending = ((Label)e.Row.FindControl("lblpendingmomcat2")).Text;
            if (Negative == "")
            {
                Negative = "0";
                ((Label)e.Row.FindControl("lblnegativemomcat2")).Text = Negative.ToString();
            }
            if (Positive == "")
            {
                Positive = "0";
                ((Label)e.Row.FindControl("lblpositivemomcat2")).Text = Positive.ToString();
            }
            if (Revisit == "")
            {
                Revisit = "0";
                ((Label)e.Row.FindControl("lblrevisitmomcat2")).Text = Revisit.ToString();
            }
            if (NotEligi == "")
            {
                NotEligi = "0";
                ((Label)e.Row.FindControl("lblnoteligiblemomcat2")).Text = NotEligi.ToString();
            }
            if (Pending == "")
            {
                Pending = "0";
                ((Label)e.Row.FindControl("lblpendingmomcat2")).Text = Pending.ToString();
            }
            decimal totalvalue = Convert.ToDecimal(Negative) + Convert.ToDecimal(Positive) + Convert.ToDecimal(Revisit) + Convert.ToDecimal(NotEligi) + Convert.ToDecimal(Pending);
            e.Row.Cells[7].Text = totalvalue.ToString();
            sumFooterValue += totalvalue;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblTotalmomcat2");
            lbl.Text = sumFooterValue.ToString();
        }
    }
}