using System;
using System.Web.UI;
using System.Data;
using BusinessLayer;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Text;
using ClosedXML.Excel;

public partial class frmmastersendmail : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Common Co = new Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
        }
    }
   
    protected void btnsubmitorsend_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlauditcase.SelectedItem.Value != "01")
            {
                InsertRecordData();
            }
            else
            {
                diverror.Visible = true;
                diverror.InnerHtml = "Please select auditcase and email process.";
                diverror.Attributes.Add("class", "alert alert-danger");
            }
        }
        catch (Exception ex)
        {
            diverror.Visible = true;
            diverror.InnerHtml = ex.Message;
            diverror.Attributes.Add("class", "alert alert-danger");
        }

    }
    protected DataTable SearchReocrd()
    {
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        if (ddlauditcase.SelectedItem.Value != "Select Audit Case")
        {
            if (ddlauditcase.SelectedItem.Value == "Negative" || ddlauditcase.SelectedItem.Value == "Pending")
            {
                dr = insert.NewRow();
                dr["Column"] = "StatusByFE" + "=";
                dr["Value"] = "'" + ddlauditcase.SelectedItem.Value + "'";
                insert.Rows.Add(dr);
            }
            if (ddlauditcase.SelectedItem.Value == "03")
            {
                dr = insert.NewRow();
                dr["Column"] = "StatusByFE" + "=";
                dr["Value"] = "'' or StatusByFE is null";
                insert.Rows.Add(dr);
            }
        }
        if (txtsearch.Text != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "[Logical Circuit Id]" + "=";
            dr["Value"] = "'" + txtsearch.Text + "' or [Customer Name]='" + txtsearch.Text + "'";
            insert.Rows.Add(dr);
        }

        dr = insert.NewRow();
        dr["Column"] = "SendNegativeMailtoAll" + "!=";
        dr["Value"] = "'5'";
        insert.Rows.Add(dr);

        return insert;
    }
    protected DataTable BindResult()
    {
        return Lo.AllSearchCode(this.SearchReocrd(), "tbl_trn_RawData" + "[Logical Circuit Id]" + "");
    }
    protected void InsertRecordData()
    {
        try
        {
            DataTable Dt1 = this.BindResult();
            try
            {
                if (Dt1.Rows.Count > 0)
                {
                    gvexcel.DataSource = Dt1;
                    gvexcel.Visible = true;
                    gvexcel.DataBind();
                    divgrid.Visible = true;
                    lbltotal.Text = "Total  " + Dt1.Rows.Count + "  record found";
                    diverror.Visible = false;
                }
                else
                {
                    divgrid.Visible = false;
                    gvexcel.Visible = false;
                    diverror.Visible = true;
                    diverror.InnerHtml = "No record found";
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
            }
            catch (Exception ex)
            {
                diverror.Visible = true;
                diverror.InnerHtml = ex.Message;
                diverror.Attributes.Add("class", "alert alert-danger");
            }
        }
        catch (Exception ex)
        {
            diverror.Visible = true;
            diverror.InnerHtml = ex.Message;
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnsendmailview_Click(object sender, EventArgs e)
    {
        try
        {
            int totalcount = 0;
            foreach (GridViewRow row in gvexcel.Rows)
            {
                if ((row.FindControl("chkRow") as CheckBox).Checked)
                {
                    totalcount++;
                    divmssgpopup.Visible = true;
                }
            }
            divshow1.Visible = false;
            divsentmailclient.Visible = true;
            divmssgpopup.Visible = false;
            if (ddlauditcase.SelectedItem.Text == "Negative")
            {
                ddlendmailprocess.Items.FindByValue("Both").Enabled = false;
            }
            else if (ddlauditcase.SelectedItem.Value == "Pending")
            {
                ddlendmailprocess.Items.FindByValue("Airtel").Enabled = false;
                ddlendmailprocess.Items.FindByValue("Both").Enabled = false;
            }
            else if (ddlauditcase.SelectedItem.Value == "03")
            {
                ddlendmailprocess.Items.FindByValue("Airtel").Enabled = false;
                ddlendmailprocess.Items.FindByValue("Client").Enabled = true;
            }
            BindEmails();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
            lbltotalselctedrow.Text = "Total Select Rows is :-" + totalcount;
        }
        catch (Exception ex)
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = ex.Message;
            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void gvexcel_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
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
                    divshow1.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                }
            }
        }
        catch (Exception ex)
        {
            diverror.InnerHtml = ex.Message;
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnsend_Click(object sender, EventArgs e)
    {
        if (lbltotalselctedrow.Text != "0" && ddlendmailprocess.SelectedItem.Value != "01")
        {
            if (ddlauditcase.SelectedItem.Text == "Negative" && ddlendmailprocess.SelectedItem.Text == "Airtel")
            {
                DateTime current = Convert.ToDateTime(DateTime.Now);
                string FinalCurrentDate = current.ToString("dd/MMM/yyyy");
                DataTable DtRetriveUploadDate = Lo.RetriveCodeWithContidion("select [Logical Circuit Id],[Customer Name],ProductName,BILLING_BANDWIDTH,[Customer Segment],[Eligible/Not Eligible] from tbl_trn_RawData where [Eligible/Not Eligible]='Not Eligible' and IsClosed !='Y' and IsActive='Y'");
                if (DtRetriveUploadDate.Rows.Count > 0)
                {
                    ExportExcel(DtRetriveUploadDate);
                    int updatecodeairtel = Lo.UpdateCodeOther("tbl_trn_RawData", "Set SentSamMail='Y'", "[Eligible/Not Eligible]='Not Eligible' and IsClosed !='Y'");
                }
            }
            else if (ddlauditcase.SelectedItem.Text == "Negative" && ddlendmailprocess.SelectedItem.Text == "Client")
            {
                DataTable DtEmailNeativeCase = Lo.RetriveCodeAllExcelRecordWhereCondition("StatusByFE='Negative' and IsClosed='N' and [Eligible/Not Eligible]='Eligible' and SendNegativeMailtoAll!='5'");
                if (DtEmailNeativeCase.Rows.Count > 0)
                {
                    SendMailNegativeCustomer(DtEmailNeativeCase);
                }
            }
            else if (ddlauditcase.SelectedItem.Value == "Pending" && ddlendmailprocess.SelectedItem.Text == "Client")
            {
                SendMailClientPending();
            }
            else if (ddlauditcase.SelectedItem.Value == "03" && ddlendmailprocess.SelectedItem.Text == "Client")
            { //DataTable DtSendMailToClient = Lo.RetriveCodeWithContidion("select [Logical Circuit Id] as [Circuit ID],[FE Name],[Party Name],ProductName as Product,BILLING_BANDWIDTH as [B/W(KBPS)], [From Site] as [Installation Address],ProdTypeName as [Type of Inspection],[Cust Email] as CustomerMailID from tbl_trn_RawData where [Eligible/Not Eligible]='Eligible' and IsClosed !='Y' and IsActive='Y' and UniqueID='" + HfID.Value + "'");
                //if (DtSendMailToClient.Rows.Count > 0)
                //{
                //    SendMailToClient(DtSendMailToClient);
                //}
            }
            else if (ddlauditcase.SelectedItem.Value == "03" && ddlendmailprocess.SelectedItem.Text == "Airtel Not Eligible")
            { //DateTime current = Convert.ToDateTime(DateTime.Now);
                //string FinalCurrentDate = current.ToString("dd/MMM/yyyy");
                //DataTable DtRetriveUploadDate = Lo.RetriveCodeWithContidion("select [Logical Circuit Id],[Customer Name],ProductName,BILLING_BANDWIDTH,[Customer Segment],[Eligible/Not Eligible] from tbl_trn_RawData where [Eligible/Not Eligible]='Not Eligible' and IsClosed !='Y' and IsActive='Y'");
                //if (DtRetriveUploadDate.Rows.Count > 0)
                //{
                //    ExportExcel(DtRetriveUploadDate);
                //    int updatecodeairtel = Lo.UpdateCodeOther("tbl_trn_RawData", "Set SentSamMail='Y'", "[Eligible/Not Eligible]='Not Eligible' and IsClosed !='Y'");
                //}
            }
            else if (ddlauditcase.SelectedItem.Value == "03" && ddlendmailprocess.SelectedItem.Value == "Both")
            { //DataTable DtSendMailToClient = Lo.RetriveCodeWithContidion("select [Logical Circuit Id] as [Circuit ID],[FE Name],[Party Name],ProductName as Product,BILLING_BANDWIDTH as [B/W(KBPS)], [From Site] as [Installation Address],ProdTypeName as [Type of Inspection],[Cust Email] as CustomerMailID from tbl_trn_RawData where [Eligible/Not Eligible]='Eligible' and IsClosed !='Y' and IsActive='Y' and UniqueID='" + HfID.Value + "'");
                //if (DtSendMailToClient.Rows.Count > 0)
                //{
                //    SendMailToClient(DtSendMailToClient);
                //}
                //DateTime current = Convert.ToDateTime(DateTime.Now);
                //string FinalCurrentDate = current.ToString("dd/MMM/yyyy");
                //DataTable DtRetriveUploadDate = Lo.RetriveCodeWithContidion("select [Logical Circuit Id],[Customer Name],ProductName,BILLING_BANDWIDTH,[Customer Segment],[Eligible/Not Eligible] from tbl_trn_RawData where [Eligible/Not Eligible]='Not Eligible' and IsClosed !='Y' and IsActive='Y'");
                //if (DtRetriveUploadDate.Rows.Count > 0)
                //{
                //    ExportExcel(DtRetriveUploadDate);
                //    int updatecodeairtel = Lo.UpdateCodeOther("tbl_trn_RawData", "Set SentSamMail='Y'", "[Eligible/Not Eligible]='Not Eligible' and IsClosed !='Y'");
                //}
            }
        }
        else
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = "Please select any grid row and also select mail process";
            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void BindEmails()
    {
        DataTable DtBind = Lo.RetriveBindDDL("Designation", "Email", "tbl_mst_Email where IsActive='Y'");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBoxList(chkreceiver, DtBind, "Email", "Designation");
            Co.FillComboBoxList(chkccrecemail, DtBind, "Email", "Designation");

            foreach (ListItem item in chkccrecemail.Items)
            {
                //item.Attributes.Add("class", "badge");
                item.Text = "<span class=\"btn btn-sm btn-round btn-info radius-25 ml10\">" + item.Text + "</span>";
                item.Value = item.Value;
                //chklstFSOptions.Items.Add(item);
            }
            foreach (ListItem item in chkreceiver.Items)
            {
                //item.Attributes.Add("class", "badge");
                item.Text = "<span class=\"btn btn-sm btn-round btn-info radius-25 ml10\">" + item.Text + "</span>";
                item.Value = item.Value;
                //chklstFSOptions.Items.Add(item);
            }
        }
    }
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
        }
        else
        {
            diverror.Visible = true;
            diverror.InnerHtml = exMsg;
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void SendMailNegativeCustomer(DataTable DtEmailNeativeCase)
    {
        for (int i = 0; DtEmailNeativeCase.Rows.Count > i; i++)
        {
            if (DtEmailNeativeCase.Rows[i]["StatusByFE"].ToString() == "Negative" && DtEmailNeativeCase.Rows[i]["SendNegativeMailtoAll"].ToString() != "5")
            {
                string body;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/NegativeCaseMailAfter5Days.htm")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{ClientName}", DtEmailNeativeCase.Rows[i]["Customer Name"].ToString());
                SendMail s;
                s = new SendMail();
                if ("Leased Line" == DtEmailNeativeCase.Rows[i]["ProductName"].ToString())
                {
                    if (DtEmailNeativeCase.Rows[i]["Cust Email"].ToString() != "")
                    {
                        s.CreateMail("verification@rcpl.in", DtEmailNeativeCase.Rows[i]["Cust Email"].ToString(), "RE: Airtel " + DtEmailNeativeCase.Rows[i]["ProductName"].ToString() + " Verification  " + DtEmailNeativeCase.Rows[i]["ProdTypeName"].ToString() + " - " + DtEmailNeativeCase.Rows[i]["Party Name"].ToString() + "", body, "Leased Line", "Negative", DtEmailNeativeCase.Rows[i]["SAM_Email"].ToString());

                        string exMsg = "";
                        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
                        if (result == true && exMsg == "")
                        {
                            Int32 UpdateCaseStatus = Lo.UpdateCodeOther("tbl_trn_RawData", "Set SendNegativeMailtoAll='2'", "UniqueID='" + DtEmailNeativeCase.Rows[i]["UniqueID"].ToString() + "'");
                        }
                    }
                    else
                    {
                        Int32 UpdateCaseStatus = Lo.UpdateCodeOther("tbl_trn_RawData", "Set SendNegativeMailtoAll='2'", "UniqueID='" + DtEmailNeativeCase.Rows[i]["UniqueID"].ToString() + "'");

                    }
                }
            }
        }
    }
    protected void SendMailClientPending()
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/SuccessorFailVerification.htm")))
        {
            body = reader.ReadToEnd();
        }
        //  body = body.Replace("{Message}", " This for your information that verification as per DoT regulations for Leased Line has been resechduled " + txtrevisittimeanddate.Text + ". <br /><br />Thanks for choosing Airtel as your preferred service partner. <br /><br />assuring you our best services always. <br /><br />");
        SendMail s;
        s = new SendMail();
        //if (txtemailsecondperson.Text == hfEmailCustomer.Value)
        //{
        //    s.CreateMail("verification@rcpl.in", hfEmailCustomer.Value + "," + txtemailsecondperson.Text, "Airtel verification status email!!", body, "Pending");
        //}
        //else
        //{
        //    s.CreateMail("verification@rcpl.in", hfEmailCustomer.Value, "Airtel verification status email!!", body, "Pending");
        //}
        // s.CreateMail("mohdwali@globalitpoint.com", "mohdwali@globalitpoint.com", "Airtel!!", body);
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        if (result == true && exMsg == "")
        {

            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = "Status update or e-mail send successfully.";
            divmssgpopup.Attributes.Add("class", "alert alert-success");
        }
        else
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = exMsg;
            divmssgpopup.Attributes.Add("class", "alert alert-danger");
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
   
}