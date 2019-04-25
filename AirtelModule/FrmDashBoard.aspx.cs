using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Configuration;
using ClosedXML.Excel;


public partial class FrmDashBoard : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataTable DTCount = new DataTable();
    DataTable DtLeasedLine = new DataTable();
    DataTable DtMo = new DataTable();
    DataTable DtMo1 = new DataTable();
    DataTable DtLeasedLine1 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
         //   countlink();
          // BindFEGiveWorkAllLeasedLine();
         //   BindFEGiveWorkAllMo();
        }
    }
    protected void countlink()
    {
        DTCount = Lo.RetriveAllCountFE("tbl_mst_Company", "IsActive='Y'");
        if (DTCount.Rows.Count > 0)
        {
            divcompany.InnerText = DTCount.Rows[0]["RCount"].ToString();
        }
        DTCount = Lo.RetriveAllCountFE("tbl_mst_Employee", "IsActive='Y'");
        if (DTCount.Rows.Count > 0)
        {
            divuser.InnerText = DTCount.Rows[0]["RCount"].ToString();
        }
        DTCount = Lo.RetriveAllCount("tbl_mst_Product", "IsActive='Y'");
        if (DTCount.Rows.Count > 0)
        {
            divproduct.InnerText = DTCount.Rows[0]["RCount"].ToString();
        }
        DTCount = Lo.RetriveAllCount("tbl_mst_ProductType", "IsActive='Y'");
        if (DTCount.Rows.Count > 0)
        {
            divprotype.InnerText = DTCount.Rows[0]["RCount"].ToString();
        }
        DTCount = Lo.RetriveAllCount("tbl_mst_Zone", "IsActive='Y'");
        if (DTCount.Rows.Count > 0)
        {
            divzone.InnerText = DTCount.Rows[0]["RCount"].ToString();
        }
        DTCount = Lo.RetriveAllCount("tbl_mst_ZoneArea", "IsActive='Y'");
        if (DTCount.Rows.Count > 0)
        {
            divzonearea.InnerText = DTCount.Rows[0]["RCount"].ToString();
        }
        DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y'");
        if (DTCount.Rows.Count > 0)
        {
            divrowdata.InnerText = DTCount.Rows[0]["RCount"].ToString();
        }
        DTCount = Lo.RetriveAllCount("tbl_mst_Email", "IsActive='Y'");
        if (DTCount.Rows.Count > 0)
        {
            divemail.InnerText = DTCount.Rows[0]["RCount"].ToString();
        }
    }
    protected void BindFEGiveWorkAllLeasedLine()
    {
        DtLeasedLine = Lo.RetriveCodeWithContidion("select Count([Fe Name])TotalAssignJob,[Fe Name] from tbl_trn_RawData where DateName(Month,[Allocated Date])='" + DateTime.Now.ToString("MMMM") + "' and ProductName='Leased Line' group by [fe name]");
        if (DtLeasedLine.Rows.Count > 0)
        {
            DtLeasedLine.Columns.Add("PendingAudit", typeof(Int64));
            DtLeasedLine.Columns.Add("Positive", typeof(string));
            DtLeasedLine.Columns.Add("Negative", typeof(string));
            DtLeasedLine.Columns.Add("Pending", typeof(string));
            gvdetailleasedline.DataSource = DtLeasedLine;
            gvdetailleasedline.DataBind();
            gvdetailleasedline.Visible = true;
        }
    }
    decimal sum = 0;
    protected void gvdetailleasedline_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblfename = e.Row.FindControl("lblfename") as Label;
            Label lbltotal = e.Row.FindControl("lbltotal") as Label;
            Label lblremaintotal = e.Row.FindControl("lblremaintotal") as Label;
            Label Negative = e.Row.FindControl("lblneg") as Label;
            Label Positive = e.Row.FindControl("lblpositive") as Label;
            Label Revisit = e.Row.FindControl("lblpend") as Label;
            DtLeasedLine1 = Lo.RetriveCodeWithContidion("select [Fe Name],[Positive],[Negative],[Pending] from (select [Fe Name],StatusByFE from tbl_trn_rawdata where ProductName='Leased Line' and DateName(Month,StatusUpdateDate)='" + DateTime.Now.ToString("MMMM") + "' and [FE Name]='" + lblfename.Text + "') Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending])) as Tab2 order by Tab2.[FE Name]");
            if (DtLeasedLine1.Rows.Count > 0)
            {
                Negative.Text = DtLeasedLine1.Rows[0]["Negative"].ToString();
                Positive.Text = DtLeasedLine1.Rows[0]["Positive"].ToString();
                Revisit.Text = DtLeasedLine1.Rows[0]["Pending"].ToString();
                Int64 neg = Convert.ToInt64(Negative.Text);
                Int64 pos = Convert.ToInt64(Positive.Text);
                Int64 rev = Convert.ToInt64(Revisit.Text);
                Int64 FInalTotal = Convert.ToInt64(lbltotal.Text) - Convert.ToInt64(neg + pos + rev);
                lblremaintotal.Text = FInalTotal.ToString();
            }
            else
            {
                Negative.Text = "0";
                Positive.Text = "0";
                Revisit.Text = "0";
                Int64 neg = Convert.ToInt64(Negative.Text);
                Int64 pos = Convert.ToInt64(Positive.Text);
                Int64 rev = Convert.ToInt64(Revisit.Text);
                Int64 FInalTotal = Convert.ToInt64(lbltotal.Text) - Convert.ToInt64(neg + pos + rev);
                lblremaintotal.Text = FInalTotal.ToString();
            }
        }
    }
    protected void BindFEGiveWorkAllMo()
    {
        DataTable DtMo = Lo.RetriveCodeWithContidion("select Count([Fe Name])TotalAssignJob,[Fe Name] from tbl_trn_RawData where DateName(Month,[Allocated Date])='" + DateTime.Now.ToString("MMMM") + "' and ProductName='MO' group by [fe name]");
        if (DtMo.Rows.Count > 0)
        {
            DtMo.Columns.Add("PendingAudit", typeof(Int64));
            DtMo.Columns.Add("Positive", typeof(string));
            DtMo.Columns.Add("Negative", typeof(string));
            DtMo.Columns.Add("Pending", typeof(string));
            gvdetailMo.DataSource = DtMo;
            gvdetailMo.DataBind();
            gvdetailMo.Visible = true;
        }
    }
    protected void gvdetailMo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblfename1 = e.Row.FindControl("lblfename1") as Label;
            Label lbltotal1 = e.Row.FindControl("lbltotal1") as Label;
            Label lblremaintotal1 = e.Row.FindControl("lblremaintotal1") as Label;
            Label Negative1 = e.Row.FindControl("lblneg1") as Label;
            Label Positive1 = e.Row.FindControl("lblpositive1") as Label;
            Label Revisit1 = e.Row.FindControl("lblpend1") as Label;
            DtMo1 = Lo.RetriveCodeWithContidion("select [Fe Name],[Positive],[Negative],[Pending] from (select [Fe Name],StatusByFE from tbl_trn_rawdata where ProductName='MO' and DateName(Month,StatusUpdateDate)='" + DateTime.Now.ToString("MMMM") + "' and [FE Name]='" + lblfename1.Text + "') Tab1 pivot(count(StatusByFE) for StatusByFE in ([Positive],[Negative],[Pending])) as Tab2 order by Tab2.[FE Name]");
            if (DtMo1.Rows.Count > 0)
            {
                Negative1.Text = DtMo1.Rows[0]["Negative"].ToString();
                Positive1.Text = DtMo1.Rows[0]["Positive"].ToString();
                Revisit1.Text = DtMo1.Rows[0]["Pending"].ToString();
                Int64 neg1 = Convert.ToInt64(Negative1.Text);
                Int64 pos1 = Convert.ToInt64(Positive1.Text);
                Int64 rev1 = Convert.ToInt64(Revisit1.Text);
                Int64 FInalTotal1 = Convert.ToInt64(lbltotal1.Text) - Convert.ToInt64(neg1 + pos1 + rev1);
                lblremaintotal1.Text = FInalTotal1.ToString();
            }
            else
            {
                Negative1.Text = "0";
                Positive1.Text = "0";
                Revisit1.Text = "0";
                Int64 neg1 = Convert.ToInt64(Negative1.Text);
                Int64 pos1 = Convert.ToInt64(Positive1.Text);
                Int64 rev1 = Convert.ToInt64(Revisit1.Text);
                Int64 FInalTotal1 = Convert.ToInt64(lbltotal1.Text) - Convert.ToInt64(neg1 + pos1 + rev1);
                lblremaintotal1.Text = FInalTotal1.ToString();
            }
        }
    }
    //#region BindExcel or MailCode
    //protected void SendMailExcelReport(byte[] bytes, byte[] wali, DataTable dtll, DataTable dtmo, string FileName)
    //{
    //    string body;
    //    using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/EmailForReport.htm")))
    //    {
    //        body = reader.ReadToEnd();
    //    }
    //    SendMail s;
    //    s = new SendMail();
    //    // s.CreateInvoiceMailForByte1("verification@rcpl.in", "Shyam.Prasad@airtel.com", "Reports", body, "", bytes, wali, "verification@rcpl.in", "gagan@rcpl.in");
    //    s.CreateInvoiceMailForByte1("noreply.gipinfosystems@gmail.com", "gagan@rcpl.in", FileName, body, "", bytes, wali, "", "");
    //    //  s.CreateInvoiceMailForByte1("verification@rcpl.in", "mohdwali@globalitpoint.com", "Reports", body, "", bytes, wali, "", "");
    //    string exMsg = "";
    //    // bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 465, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);      
    //    //  bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
    //    bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 587, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
    //}
    //protected void ExportExcel(DataTable dtll, DataTable dtmo, string FileName)
    //{
    //    byte[] wali;
    //    byte[] bytes;
    //    using (XLWorkbook wb = new XLWorkbook())
    //    {
    //        wb.Worksheets.Add(dtll);
    //        using (MemoryStream memoryStream = new MemoryStream())
    //        {
    //            wb.SaveAs(memoryStream);
    //            bytes = memoryStream.ToArray();
    //            memoryStream.Close();
    //        }
    //    }
    //    using (XLWorkbook wbn = new XLWorkbook())
    //    {
    //        dtmo.TableName = "Reports";
    //        wbn.Worksheets.Add(dtmo);
    //        using (MemoryStream memoryStream = new MemoryStream())
    //        {
    //            wbn.SaveAs(memoryStream);
    //            wali = memoryStream.ToArray();
    //            memoryStream.Close();
    //        }
    //    }
    //    if (dtll != null || dtmo != null)
    //    {
    //        SendMailExcelReport(bytes, wali, dtll, dtmo, "Fe Alloted Work Report");
    //    }
    //}
    //#endregion
    //protected void btnsendmail_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        BindFEGiveWorkAllLeasedLine();
    //        BindFEGiveWorkAllMo();
    //        ExportExcel(DtLeasedLine, DtMo,"Fe All Work Alloted Report");
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.Message.ToString();
    //    }
    //}
}