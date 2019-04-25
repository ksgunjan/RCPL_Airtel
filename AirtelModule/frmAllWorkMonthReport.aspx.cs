using System;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using System.IO;
using System.Configuration;
using ClosedXML.Excel;

public partial class frmAllWorkMonthReport : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Common Co = new Common();
    DataTable DTSearch = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            bindcurrentmontheport();
            bindcurrentmontheportmo();
            bindcurrentmontheportpri();
        }
    }
    protected void bindcurrentmontheport()
    {
        DTSearch = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select upper(convert(varchar(3),[StatusUpdateDate],0)) as [Month],(case when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProductName='Leased Line') Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2");
        if (DTSearch.Rows.Count > 0)
        {
            gvfemonthwisereport.DataSource = DTSearch;
            gvfemonthwisereport.DataBind();
            gvfemonthwisereport.Visible = true;
            diverror.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void bindcurrentmontheportmo()
    {
        DTSearch = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select DateName(Month,StatusUpdateDate) as [Month] , (case  when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe  from tbl_trn_rawdata where ProductName='MO' group by DateName(Month,StatusUpdateDate),[party name],statusbyfe) Tab1 pivot(count(StatusByFE) for StatusByFE in  ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2 order by [Month]");
        if (DTSearch.Rows.Count > 0)
        {

            gvmo.DataSource = DTSearch;
            gvmo.DataBind();
            gvmo.Visible = true;
            diverror.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void bindcurrentmontheportpri()
    {
        DTSearch = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select upper(convert(varchar(3),[StatusUpdateDate],0)) as [Month],(case when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProductName='PRI-Fixed Line' and KeyDup='false') Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2");
        if (DTSearch.Rows.Count > 0)
        {
            gvprifixedline.DataSource = DTSearch;
            gvprifixedline.DataBind();
            gvprifixedline.Visible = true;
            diverror.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtll = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select upper(convert(varchar(3),[StatusUpdateDate],0)) as [Month],(case  when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProductName='Leased Line') Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2");
            DataTable dtmo = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select DateName(Month,StatusUpdateDate) as [Month] , (case when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe  from tbl_trn_rawdata where ProductName='MO' group by DateName(Month,StatusUpdateDate),[party name],statusbyfe) Tab1 pivot(count(StatusByFE) for StatusByFE in  ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2 order by [Month]");
            DataTable dtPRI = Lo.RetriveCodeWithContidion("select [Month],[Not Eligible],[Positive],[Negative],[Pending],[Revisit] from (select upper(convert(varchar(3),[StatusUpdateDate],0)) as [Month],(case  when StatusByFE='Pending' then 'Revisit' else StatusByFE end) as statusbyfe from tbl_trn_rawdata where ProductName='PRI-Fixed Line' and KeyDup='false') Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending],[Not Eligible],[Revisit])) as Tab2");
            ExportExcel(dtll, dtmo, dtPRI, "DashBoard Monthly Work Report (FE Audit Date Wise)");
            int update = Lo.InsertNormal("insert into tbl_trn_AutoMailSendStatus (Category,MailDate,IsSend) values ('MonthlyReportStatusUpdateDate','" + DateTime.Now.ToString("dd/MMM/yyyy") + "','Y')");
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
    decimal sumFooterValue = 0;
    protected void gvfemonthwisereport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Negative = ((Label)e.Row.FindControl("lblnegative")).Text;
            string Positive = ((Label)e.Row.FindControl("lblpositive")).Text;
            //  string Pending = ((Label)e.Row.FindControl("lblpending")).Text;
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
            //if (Pending == "")
            //{
            //    Pending = "0";
            //    ((Label)e.Row.FindControl("lblpending")).Text = Pending.ToString();
            //}
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
            //  string Pending = ((Label)e.Row.FindControl("lblpendingmo")).Text;
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
            //if (Pending == "")
            //{
            //    Pending = "0";
            //    ((Label)e.Row.FindControl("lblrevistmo")).Text = Pending.ToString();
            //}
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
            //  string Pending = ((Label)e.Row.FindControl("lblpositivepri")).Text;
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
            //if (Pending == "")
            //{
            //    Pending = "0";
            //    ((Label)e.Row.FindControl("lblpending")).Text = Pending.ToString();
            //}
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