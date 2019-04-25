using System;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using System.IO;
using System.Configuration;
using ClosedXML.Excel;

public partial class frmFEDashBoardReportMonthlySubCatWise : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Common Co = new Common();
    DataTable DTSearch = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            bindcurrentmontheport1();
            bindcurrentmontheportmo2();
            bindcurrentmontheportPRI3();
            bindcurrentmontheport4();
            bindcurrentmontheportmo5();
        }
    }
    protected void bindcurrentmontheport1()
    {
        DTSearch = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select Datename(Month,[StatusUpdateDate]) as [Month],(case when StatusByFE is null  then 'Pending'  when StatusByFE='' then 'Pending' when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProdTypeName='Leased Line-Initial' and StatusUpdateDate!='')Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2");
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
        DTSearch = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select Datename(Month,[StatusUpdateDate]) as [Month] ,(case when StatusByFE is null then 'Pending' when StatusByFE='' then 'Pending' when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProdTypeName='MO-Periodic' and StatusUpdateDate!='' group by DateName(Month,[StatusUpdateDate]),[party name],statusbyfe) Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2 order by [Month]");
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
        DTSearch = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select Datename(Month,[StatusUpdateDate]) as [Month],(case when StatusByFE is null  then 'Pending'  when StatusByFE='' then 'Pending' when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProdTypeName='PRI-Fixed Line' and StatusUpdateDate!='' and KeyDup='false')Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2");
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
        DTSearch = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select Datename(Month,[StatusUpdateDate]) as [Month],(case when StatusByFE is null  then 'Pending'  when StatusByFE='' then 'Pending' when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProdTypeName='Leased Line-Periodic' and StatusUpdateDate!='')Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2");
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
        DTSearch = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select Datename(Month,[StatusUpdateDate]) as [Month] ,(case when StatusByFE is null then 'Pending' when StatusByFE='' then 'Pending' when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProdTypeName='MO-Quarterly' and StatusUpdateDate!='' group by DateName(Month,[StatusUpdateDate]),[party name],statusbyfe) Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2 order by [Month]");
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
            DataTable dtll = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select Datename(Month,[StatusUpdateDate]) as [Month],(case when StatusByFE is null then 'Pending'  when StatusByFE='' then 'Pending' when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProdTypeName='Leased Line-Initial')Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2");
            DataTable dtmo = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select DateName(Month,[Allocated Date]) as [Month] ,(case when StatusByFE is null then 'Pending' when StatusByFE='' then 'Pending' when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProdTypeName='MO-Periodic') group by DateName(Month,[Allocated Date]),[party name],statusbyfe) Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2 order by [Month]");
            DataTable dtpri = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select Datename(Month,[StatusUpdateDate]) as [Month],(case when StatusByFE is null then 'Pending'  when StatusByFE='' then 'Pending' when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProdTypeName='PRI-Fixed Line' and KeyDup='false')Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2");
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
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        // bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 587, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
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