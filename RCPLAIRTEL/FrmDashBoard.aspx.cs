using BuisnessLayer;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class FrmDashBoard : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private DataTable DTCount = new DataTable();
    private DataTable DtLeasedLine = new DataTable();
    private DataTable DtMo = new DataTable();
    private DataTable DtMo1 = new DataTable();
    private DataTable DtLeasedLine1 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //countlink();
            //BindFEGiveWorkAllLeasedLine();
            //BindFEGiveWorkAllPRI();
        }
    }
    protected void countlink()
    {

        DataTable DTCount = Lo.RetriveCountAdminDashboard("Count", "", "");
        if (DTCount.Rows.Count > 0)
        {
            lbltotoalcomp.Text = DTCount.Rows[0]["TotComp"].ToString();
            lbltotalproduct.Text = DTCount.Rows[0]["TotProduct"].ToString();
            lblprodtype.Text = DTCount.Rows[0]["TotProductType"].ToString();
            lblrowdatall.Text = DTCount.Rows[0]["TotRawDataLL"].ToString();
            lbltotalmo.Text = DTCount.Rows[0]["TotRawDataMO"].ToString();
            lblpritotal.Text = DTCount.Rows[0]["TotRawDataPRI"].ToString();
            lbltotaluser.Text = DTCount.Rows[0]["TotEmployee"].ToString();
            lbltotalemail.Text = DTCount.Rows[0]["TotEmail"].ToString();
        }
    }
    protected void BindFEGiveWorkAllLeasedLine()
    {
        DtLeasedLine = Lo.RetriveAdminFeCount("", "Leased Line", DateTime.Now.ToString("MMMM"), "FENameorCount");
        if (DtLeasedLine.Rows.Count > 0)
        {
            DtLeasedLine.Columns.Add("PendingAudit", typeof(long));
            DtLeasedLine.Columns.Add("Positive", typeof(string));
            DtLeasedLine.Columns.Add("Negative", typeof(string));
            DtLeasedLine.Columns.Add("Pending", typeof(string));
            gvdetailleasedline.DataSource = DtLeasedLine;
            gvdetailleasedline.DataBind();
            gvdetailleasedline.Visible = true;
        }
    }

    private decimal sum = 0;
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
            DtLeasedLine1 = Lo.RetriveAdminFeCount(lblfename.Text, "Leased Line", DateTime.Now.ToString("MMMM"), "FEStates");
            if (DtLeasedLine1.Rows.Count > 0)
            {
                Negative.Text = DtLeasedLine1.Rows[0]["Negative"].ToString();
                Positive.Text = DtLeasedLine1.Rows[0]["Positive"].ToString();
                Revisit.Text = DtLeasedLine1.Rows[0]["Pending"].ToString();
                long neg = Convert.ToInt64(Negative.Text);
                long pos = Convert.ToInt64(Positive.Text);
                long rev = Convert.ToInt64(Revisit.Text);
                long FInalTotal = Convert.ToInt64(lbltotal.Text) - Convert.ToInt64(neg + pos + rev);
                lblremaintotal.Text = FInalTotal.ToString();
            }
            else
            {
                Negative.Text = "0";
                Positive.Text = "0";
                Revisit.Text = "0";
                long neg = Convert.ToInt64(Negative.Text);
                long pos = Convert.ToInt64(Positive.Text);
                long rev = Convert.ToInt64(Revisit.Text);
                long FInalTotal = Convert.ToInt64(lbltotal.Text) - Convert.ToInt64(neg + pos + rev);
                lblremaintotal.Text = FInalTotal.ToString();
            }
        }
    }
    protected void BindFEGiveWorkAllPRI()
    {
        DataTable DtMo = Lo.RetriveAdminFeCount("", "PRI-Fixed Line", DateTime.Now.ToString("MMMM"), "FENameorCount");
        if (DtMo.Rows.Count > 0)
        {
            DtMo.Columns.Add("PendingAudit", typeof(long));
            DtMo.Columns.Add("Positive", typeof(string));
            DtMo.Columns.Add("Negative", typeof(string));
            DtMo.Columns.Add("Pending", typeof(string));
            gvdetailpri.DataSource = DtMo;
            gvdetailpri.DataBind();
            gvdetailpri.Visible = true;
        }
    }
    protected void gvdetailpri_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblfename1 = e.Row.FindControl("lblfename1") as Label;
            Label lbltotal1 = e.Row.FindControl("lbltotal1") as Label;
            Label lblremaintotal1 = e.Row.FindControl("lblremaintotal1") as Label;
            Label Negative1 = e.Row.FindControl("lblneg1") as Label;
            Label Positive1 = e.Row.FindControl("lblpositive1") as Label;
            Label Revisit1 = e.Row.FindControl("lblpend1") as Label;
            DtMo1 = Lo.RetriveAdminFeCount(lblfename1.Text,"PRI-Fixed Line",DateTime.Now.ToString("MMM"), "FEStatesPRI");
            if (DtMo1.Rows.Count > 0)
            {
                Negative1.Text = DtMo1.Rows[0]["Negative"].ToString();
                Positive1.Text = DtMo1.Rows[0]["Positive"].ToString();
                Revisit1.Text = DtMo1.Rows[0]["Pending"].ToString();
                long neg1 = Convert.ToInt64(Negative1.Text);
                long pos1 = Convert.ToInt64(Positive1.Text);
                long rev1 = Convert.ToInt64(Revisit1.Text);
                long FInalTotal1 = Convert.ToInt64(lbltotal1.Text) - Convert.ToInt64(neg1 + pos1 + rev1);
                lblremaintotal1.Text = FInalTotal1.ToString();
            }
            else
            {
                Negative1.Text = "0";
                Positive1.Text = "0";
                Revisit1.Text = "0";
                long neg1 = Convert.ToInt64(Negative1.Text);
                long pos1 = Convert.ToInt64(Positive1.Text);
                long rev1 = Convert.ToInt64(Revisit1.Text);
                long FInalTotal1 = Convert.ToInt64(lbltotal1.Text) - Convert.ToInt64(neg1 + pos1 + rev1);
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
    //        ExportExcel(DtLeasedLine, DtMo, "Fe All Work Alloted Report");
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.Message.ToString();
    //    }
    //}

    //protected void gvpri_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //}
    protected void gvdetailleasedline_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gvdetailpri_RowCreated(object sender, GridViewRowEventArgs e)
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