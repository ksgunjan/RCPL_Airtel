using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using System.IO;
using System.Configuration;
using System.Text;
using ClosedXML.Excel;

public partial class frmApproveuploadExcel : System.Web.UI.Page
{
    Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
        }
    }
    #region Search or ApproveCode"
    protected void GetAllRecordExcelSheetCurrent()
    {
        try
        {
            DateTime TextBoxDate = Convert.ToDateTime(txtstartdate.Text);
            string FinalstartDate = TextBoxDate.ToString("dd/MMM/yyyy");
            DataTable DtExcel = Lo.RetriveCodeAllExcelRecordWhereCondition("[Allocated Date] = '" + FinalstartDate + "' and IsActive='N' and IsClosed !='Y' and ExcelVerifiy='2'");
            if (DtExcel.Rows.Count > 0)
            {
                gvexcel.DataSource = DtExcel;
                gvexcel.DataBind();
                gvexcel.Visible = true;
                h4message.InnerHtml = "Click on view to show all detail of record <br/> For approve records select on grid checkbox and click on approve button.";
                diverror.InnerHtml = "Total Record Found:- " + DtExcel.Rows.Count.ToString();
                diverror.Attributes.Add("class", "alert alert-success");
                diverror.Visible = true;
                btnapproveuploadexcel.Enabled = true;
                btnapproveuploadexcel.Visible = true;
            }
            else
            {
                gvexcel.Visible = false;
                diverror.Visible = true;
                diverror.InnerHtml = "No record found.";
                diverror.Attributes.Add("class", "alert alert-danger");
            }
        }
        catch (Exception ex)
        {
            diverror.InnerHtml = ex.Message.ToString();
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (txtstartdate.Text != "")
        {
            GetAllRecordExcelSheetCurrent();
        }
        else
        {
            gvexcel.Visible = false;
            diverror.InnerHtml = "Please select date.";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void gvexcel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewDetail")
            {
                DataTable DtSearchView = Lo.RetriveCodeAllExcelRecordWhereCondition("UniqueID =" + Convert.ToInt32(e.CommandArgument.ToString()) + "");
                if (DtSearchView.Rows.Count > 0)
                {
                    dluploadexcelrecord.DataSource = DtSearchView;
                    dluploadexcelrecord.DataBind();
                    dluploadexcelrecord.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                }
                else
                {
                    diverror.InnerHtml = "No record found.";
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
            }
        }
        catch (Exception ex)
        {
            diverror.InnerHtml = ex.Message;
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnapproveuploadexcel_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in gvexcel.Rows)
            {
                HiddenField HfID = (HiddenField)row.FindControl("hdnIDGridRawData") as HiddenField;
                Label lblcompdate = (Label)row.FindControl("lblcompdate") as Label;
                if ((row.FindControl("chkRow") as CheckBox).Checked)
                {
                    DateTime Datecompliancefe = Convert.ToDateTime(lblcompdate.Text);
                    string FinalConverteddate = Datecompliancefe.AddDays(-7).ToString("dd/MMM/yyyy");
                    //Code for send mail to client and update fe compliance date
                    int UpdateApproveStatus = Lo.UpdateCodeOther("tbl_trn_RawData", "Set IsActive='Y',FEComplianceDate='" + FinalConverteddate + "'", "UniqueID='" + HfID.Value + "' and IsClosed !='Y'");
                    //DataTable DtSendMailToClient = Lo.RetriveCodeWithContidion("select [Logical Circuit Id] as [Circuit ID],[FE Name],[Party Name],ProductName as Product,BILLING_BANDWIDTH as [B/W(KBPS)], [From Site] as [Installation Address],ProdTypeName as [Type of Inspection],[Cust Email] as CustomerMailID from tbl_trn_RawData where [Eligible/Not Eligible]='Eligible' and IsClosed !='Y' and IsActive='Y' and UniqueID='" + HfID.Value + "'");
                    //if (DtSendMailToClient.Rows.Count > 0)
                    //{
                    //    SendMailToClient(DtSendMailToClient);
                    //}
                }
            }
            //code for negative case mail send to airtel team.
            //DateTime current = Convert.ToDateTime(DateTime.Now);
            //string FinalCurrentDate = current.ToString("dd/MMM/yyyy");
            //DataTable DtRetriveUploadDate = Lo.RetriveCodeWithContidion("select [Logical Circuit Id],[Customer Name],ProductName,BILLING_BANDWIDTH,[Customer Segment],[Eligible/Not Eligible] from tbl_trn_RawData where [Eligible/Not Eligible]='Not Eligible' and IsClosed !='Y' and IsActive='Y'");
            //if (DtRetriveUploadDate.Rows.Count > 0)
            //{
            //    ExportExcel(DtRetriveUploadDate);
            //    int updatecodeairtel = Lo.UpdateCodeOther("tbl_trn_RawData", "Set SentSamMail='Y'", "[Eligible/Not Eligible]='Not Eligible' and IsClosed !='Y'");
            //}
            //else
            //{
            //    diverror.Visible = true;
            //    diverror.InnerHtml = "Excel successfully assign to FE <br/> Mail send to airtel team or customer successfully.";
            //    diverror.Attributes.Add("class", "alert alert-success");
            //    gvexcel.Visible = false;
            //    btnapproveuploadexcel.Enabled = false;
            //}
            diverror.Visible = true;
            diverror.InnerHtml = "Excel successfully assign to FE.";// <br/> Mail send to airtel team or customer successfully.
            diverror.Attributes.Add("class", "alert alert-success");
            gvexcel.Visible = false;
            btnapproveuploadexcel.Enabled = false;
            btnapproveuploadexcel.Visible = false;
        }
        catch (Exception ex)
        {
            diverror.Visible = true;
            diverror.InnerHtml = ex.Message;
            diverror.Attributes.Add("class", "alert alert-danger");
            gvexcel.Visible = false;
        }
    }
    #endregion
    #region SendMailCode"
    // this is send mail option to airtel base on data status is not eligible data
    protected void ExportExcel(DataTable DtNotEligible)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(DtNotEligible);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                SendMailAirtel(bytes, DtNotEligible);
            }
        }
    }
    protected void SendMailAirtel(byte[] bytes, DataTable DtNotEligible)
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/AirtelMail.htm")))
        {
            body = reader.ReadToEnd();
        }
        SendMail s;
        s = new SendMail();
        if (DtNotEligible.Rows[0]["ProductName"].ToString() == "Leased Line")
        {
            // s.CreateInvoiceMailForByte("gagan@rcpl.in", "mohdwaliahmad@gmail.com", "Airtel!!", body, DtNotEligible.Rows[0]["ProductName"].ToString(), bytes, "gunjan.vatuk@globalitpoint.com");
            s.CreateInvoiceMailForByte("verification@rcpl.in", "Shyam.Prasad@airtel.com", "Airtel!!", body, DtNotEligible.Rows[0]["ProductName"].ToString(), bytes, "verification@rcpl.in");
        }
        if (DtNotEligible.Rows[0]["ProductName"].ToString() == "PR1-Fixed Line")
        {
            s.CreateInvoiceMailForByte("verification@rcpl.in", "Shyam.Prasad@airtel.com", "Airtel!!", body, DtNotEligible.Rows[0]["ProductName"].ToString(), bytes, "verification@rcpl.in");
        }
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        if (result == true && exMsg == "")
        {
            diverror.Visible = true;
            diverror.InnerHtml = "Excel successfully assign to FE <br/> Mail send to Airtel team or customer successfully.";
            diverror.Attributes.Add("class", "alert alert-success");
            gvexcel.Visible = false;
            btnapproveuploadexcel.Enabled = false;
        }
        else
        {
            diverror.Visible = true;
            diverror.InnerHtml = exMsg;
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void SendMailToClient(DataTable DtEligibleMailSendClient)
    {
        if (DtEligibleMailSendClient.Rows.Count > 0)
        {
            if (DtEligibleMailSendClient.Rows[0]["CustomerMailID"].ToString().Trim() != "")
            {
                string body;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/ClientMail.htm")))
                {
                    body = reader.ReadToEnd();
                }
                StringBuilder html = new StringBuilder();
                html.Append("<table border = '1'>");
                html.Append("<tr>");
                foreach (DataColumn column in DtEligibleMailSendClient.Columns)
                {
                    html.Append("<th>");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in DtEligibleMailSendClient.Columns)
                    {
                        html.Append("<td>");
                        html.Append(DtEligibleMailSendClient.Rows[0][column.ColumnName]);
                        html.Append("</td>");
                    }
                    html.Append("</tr>");
                }
                html.Append("</table>");
                body = body.Replace("{dt}", html.ToString());
                SendMail s;
                s = new SendMail();
                s.CreateMail("verification@rcpl.in", DtEligibleMailSendClient.Rows[0]["CustomerMailID"].ToString(), "Airtel " + DtEligibleMailSendClient.Rows[0]["Product"] + " Verification " + DtEligibleMailSendClient.Rows[0]["Type of Inspection"].ToString() + " - Month " + Convert.ToDateTime(DateTime.Now).ToString("MMM -dd-") + "DoT Audit -" + DtEligibleMailSendClient.Rows[0]["Party Name"].ToString() + " ", body, "Customer");
                string exMsg = "";
                bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
                if (result == true && exMsg == "")
                {
                    //update status of send mail or not
                    int updatecodeairtel = Lo.UpdateCodeOther("tbl_trn_RawData", "Set SentCustMail='Y'", "[Eligible/Not Eligible]='Eligible' and IsClosed !='Y' and [Logical Circuit Id]='" + DtEligibleMailSendClient.Rows[0]["Circuit ID"].ToString() + "' ");
                }
                else
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = exMsg;
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
            }
        }
    }
    #endregion
}