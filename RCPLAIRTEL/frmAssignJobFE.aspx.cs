using BuisnessLayer;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmAssignJobFE : System.Web.UI.Page
{

    #region variable
    private Logic Lo = new Logic();
    private Common Co = new Common();
    private string Email;
    private string HFRevisitDate;
    private string hfPreVisit;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hffename.Value = Session["Name"].ToString();
            BindAssignJob();
        }
    }
    protected void BindAssignJob(string sortExpression = null)
    {
        DataTable DtAssignJob = new DataTable();
        if (ddlprodbycat.SelectedItem.Text == "All")
        {
            DtAssignJob = Lo.RetriveCodeLLFE(0, "Leased Line", "", DateTime.Now, hffename.Value, "", "", "", "", "BindGridFENameLL");
        }
        else if (ddlprodbycat.SelectedItem.Text != "All")
        {
            DtAssignJob = Lo.RetriveCodeLLFE(0, "Leased Line", ddlprodbycat.SelectedItem.Value, DateTime.Now, hffename.Value, "", "", "", "", "BindGridFENameLLF");
        }
        if (DtAssignJob.Rows.Count > 0)
        {
            gvexcel.DataSource = DtAssignJob;
            gvexcel.DataBind();
            lblcounttotal.Text = "Total Assign job- " + DtAssignJob.Rows.Count.ToString();
            gvexcel.Visible = true;
        }
        else
        {
            lblcounttotal.Text = "Total Assign job- " + DtAssignJob.Rows.Count.ToString();
            gvexcel.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No job assign " + ddlprodbycat.SelectedItem.Text + "')", true);
        }
    }
    protected void ddlprodbycat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAssignJob();
    }
    protected void gvexcel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblcompliancedate = e.Row.FindControl("lblcomp") as Label;
            HiddenField FeStatus = e.Row.FindControl("hfstatusfe") as HiddenField;
            HiddenField HfRevisitDate = e.Row.FindControl("hfrevisitdate") as HiddenField;
            HiddenField hfPremmetingdate = e.Row.FindControl("hfPreMeetingDate") as HiddenField;
            if (HfRevisitDate.Value != "")
            {
                DateTime RevisitDate = Convert.ToDateTime(HfRevisitDate.Value);
                HFRevisitDate = RevisitDate.ToString("dd/MMM/yyyy");
            }
            if (hfPremmetingdate.Value != "")
            {
                DateTime PreVisit = Convert.ToDateTime(hfPremmetingdate.Value);
                hfPreVisit = PreVisit.ToString("dd/MMM/yyyy");
            }
            //error come if fecompliance date will not be enter or null
            DateTime compdate = Convert.ToDateTime(lblcompliancedate.Text);
            DateTime currentdate = Convert.ToDateTime(DateTime.Now);
            if (compdate <= currentdate.AddDays(-2))
            {
                lblcompliancedate.ForeColor = System.Drawing.Color.DarkRed;
            }
            if (FeStatus.Value == "Pending")
            {
                GridViewRow grv = e.Row;
                if (Convert.ToDateTime(HFRevisitDate) >= Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy")))
                {
                    if (HFRevisitDate == DateTime.Now.ToString("dd/MMM/yyyy"))
                    {
                        e.Row.Attributes.Add("Class", "pendingrow");
                    }
                    else
                    {
                        e.Row.Visible = false;
                    }
                }
                else
                {
                    e.Row.Attributes.Add("Class", "pendingrow");
                }
            }
            if (hfPreVisit != "")
            {
                GridViewRow grv = e.Row;
                if (hfPreVisit == DateTime.Now.ToString("dd/MMM/yyyy"))
                {
                    e.Row.Attributes.Add("Class", "resmanage-user");
                }
            }
        }
    }
    protected void gvexcel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewDetail")
            {
                DataTable DtSearchView = Lo.RetriveCodeLLFE(0, "Leased Line", "", DateTime.Now, e.CommandArgument.ToString(), "", "", "", "", "ViewDetail");
                if (DtSearchView.Rows.Count > 0)
                {
                    dluploadexcelrecord.DataSource = DtSearchView;
                    dluploadexcelrecord.DataBind();
                    dluploadexcelrecord.Visible = true;
                    divshow1.Visible = true;
                    divupdatebyfe.Visible = false;
                    ddlselectcase.Enabled = true;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No record found.')", true);
                }
            }
            else if (e.CommandName == "UpdateStatus")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                string lblcompliancedate = (gvexcel.Rows[rowIndex].FindControl("lblcomp") as Label).Text;
                string stattusbyfe = (gvexcel.Rows[rowIndex].FindControl("hfstatusfe") as HiddenField).Value;
                if (stattusbyfe == "" || stattusbyfe == null || stattusbyfe == "Pending")
                {
                    cleartext();
                    Email = (gvexcel.Rows[rowIndex].FindControl("hfemail") as HiddenField).Value;
                    hfNameCustomer.Value = (gvexcel.Rows[rowIndex].FindControl("Customer") as HiddenField).Value;
                    hfPartyName.Value = (gvexcel.Rows[rowIndex].FindControl("lblpartyname") as Label).Text;
                    hflogicalcircuitid.Value = (gvexcel.Rows[rowIndex].FindControl("logiclacircuitid") as Label).Text;
                    hfexcelrawid.Value = e.CommandArgument.ToString();
                    hfEmailCustomer.Value = Email;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Status already update please refresh page for see updated list.')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
    }
    protected void gvexcel_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void cleartext()
    {
        txtsecondpersonname.Text = "";
        txtsecondpersondesig.Text = "";
        txtcompshiftpersonmetname.Text = "";
        txtcompshiftpersonmetdesig.Text = "";
        txtcompshiftpersonmetemail.Text = "";
        txtemailsecondperson.Text = "";
        txtnumbersecondperson.Text = "";
        txtotprevisitcustmer.Text = "";
        txtrevisitpersonmetname.Text = "";
        txtrevisitpersonmobileno.Text = "";
        txtrevisittimeanddate.Text = "";
        hfEmailCustomer.Value = "";
        //  hffename.Value = "";
        HFRevisitDate = "";
        txtnewcompanyaddress.Text = "";
        ddlstamp.SelectedValue = "01";
        ddlselectcase.SelectedValue = "01";
        ddlrevisitrequired.SelectedValue = "01";
        ddlemailregardstamp.SelectedValue = "01";
        ddlsamper.SelectedValue = "01";
        ddltlper.SelectedValue = "01";
        ddlairmana.SelectedValue = "01";
        ddlairper.SelectedValue = "01";
        txtremarkupdate.Text = "";
        ddlselectcase.Enabled = true;
        txtreasoneremarkauditcustomer.Text = "";
        hfNameCustomer.Value = "";
        hflogicalcircuitid.Value = "";
        //Code Pick From Gridview Command
        divmssgpopup.Visible = false;
        divmssgpopup.InnerHtml = "";
        divshow1.Visible = false;
        divupdatebyfe.Visible = true;
        Divrevisit.Visible = false;
        panstatuscase1.Visible = false;
        panelstatuscase2.Visible = false;
        ddlselectcase.Enabled = true;
        divEntrynotallowed.Visible = false;
        divcustomerdeniedforaudit.Visible = false;
        DivCompanyshifted.Visible = false;
        hfotp.Value = "";
    }
    #region Code of FE What status Giving by FE it is depend on this code with mail Code"
    protected void ddlselectcase_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlselectcase.SelectedItem.Text != "--Select Case--")
        {
            if (ddlselectcase.SelectedItem.Value == "02")
            {
                DivCompanyshifted.Visible = true;
                divaddforcompshift.Visible = true;
                panstatuscase1.Visible = false;
                Divrevisit.Visible = false;
                divemailifotherempverify.Visible = false;
                panelstatuscase2.Visible = false;
                ddlselectcase.Enabled = false;
            }
            else if (ddlselectcase.SelectedItem.Value == "04")
            {
                DivCompanyshifted.Visible = true;
                divaddforcompshift.Visible = false;
                panstatuscase1.Visible = false;
                Divrevisit.Visible = false;
                divemailifotherempverify.Visible = false;
                panelstatuscase2.Visible = false;
                ddlselectcase.Enabled = false;

            }
            else if (ddlselectcase.SelectedItem.Value == "05")
            {
                panstatuscase1.Visible = true;
                DivCompanyshifted.Visible = false;
                Divrevisit.Visible = false;
                divemailifotherempverify.Visible = false;
                panelstatuscase2.Visible = false;
                ddlselectcase.Enabled = false;

            }
            else if (ddlselectcase.SelectedItem.Value == "06")
            {
                Divrevisit.Visible = true;
                DivCompanyshifted.Visible = false;
                panstatuscase1.Visible = false;
                divemailifotherempverify.Visible = false;
                panelstatuscase2.Visible = false;
                ddlselectcase.Enabled = false;
            }
            else if (ddlselectcase.SelectedItem.Value == "07")
            {
                Divrevisit.Visible = false;
                DivCompanyshifted.Visible = false;
                panstatuscase1.Visible = false;
                divemailifotherempverify.Visible = false;
                panelstatuscase2.Visible = false;
                divEntrynotallowed.Visible = true;
                ddlselectcase.Enabled = false;
            }
            else if (ddlselectcase.SelectedItem.Value == "08")
            {
                Divrevisit.Visible = false;
                DivCompanyshifted.Visible = false;
                panstatuscase1.Visible = false;
                divemailifotherempverify.Visible = false;
                panelstatuscase2.Visible = false;
                divEntrynotallowed.Visible = false;
                ddlselectcase.Enabled = false;
                divcustomerdeniedforaudit.Visible = true;
            }
            else if (ddlselectcase.SelectedItem.Value == "03")
            {
                Divrevisit.Visible = false;
                DivCompanyshifted.Visible = false;
                panstatuscase1.Visible = false;
                divemailifotherempverify.Visible = false;
                panelstatuscase2.Visible = false;
                divEntrynotallowed.Visible = false;
                ddlselectcase.Enabled = false;
                panaddressorcompclose.Visible = true;
                divcustomerdeniedforaudit.Visible = false;
                btnsubmit.Visible = true;
            }
            else if (ddlselectcase.SelectedItem.Value == "09")
            {
                Divrevisit.Visible = false;
                DivCompanyshifted.Visible = false;
                panstatuscase1.Visible = false;
                divemailifotherempverify.Visible = false;
                panelstatuscase2.Visible = false;
                divEntrynotallowed.Visible = false;
                ddlselectcase.Enabled = false;
                divcustomerdeniedforaudit.Visible = false;
                panaddressorcompclose.Visible = true;
                btnsubmit.Visible = true;
            }
        }
        else
        {
            Divrevisit.Visible = false;
            panstatuscase1.Visible = false;
            panelstatuscase2.Visible = false;
            divemailifotherempverify.Visible = false;
            divmssgpopup.Visible = true;
            DivCompanyshifted.Visible = false;
            cleartext();
            divmssgpopup.InnerHtml = "Select any case for update status.";
            divmssgpopup.Attributes.Add("class", "alert alert-danger");
        }
    }
    #endregion
    #region Success Audit
    protected void ddlstamp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstamp.SelectedItem.Text != "--Select Status--")
        {
            if (ddlstamp.SelectedItem.Value == "02")
            {
                divemailifotherempverify.Visible = true;
                btnsubmit.Visible = true;
                panelstatuscase2.Visible = false;
            }
            else
            {
                panelstatuscase2.Visible = true;
                panstatuscase1.Visible = false;
                divemailifotherempverify.Visible = false;
            }
        }
        else
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = "Select any case for update status.";
            divmssgpopup.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void ddlemailregardstamp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlemailregardstamp.SelectedItem.Text != "--Select Status--")
        {
            if (ddlemailregardstamp.SelectedItem.Value == "02")
            {
                divemailifotherempverify.Visible = true;
                btnsubmit.Visible = true;
                panelstatuscase2.Visible = false;
                panstatuscase1.Visible = false;
            }
            else
            {
                divemailifotherempverify.Visible = true;
                btnsubmit.Visible = true;
                panelstatuscase2.Visible = false;
                panstatuscase1.Visible = false;
            }
        }
        else
        {
            divemailifotherempverify.Visible = false;
            btnsubmit.Visible = false;
            panelstatuscase2.Visible = false;
            panstatuscase1.Visible = false;
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = "Select any case for update status.";
            divmssgpopup.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlemailregardstamp.SelectedItem.Value == "03")
            {
                int UpdateCaseID = Lo.UpdateCodeFEAssignLL(Convert.ToInt64(hfexcelrawid.Value), "", "", Convert.ToDateTime(DateTime.Now), txtsecondpersonname.Text, txtsecondpersondesig.Text, txtsecondpersonname.Text, txtsecondpersondesig.Text, "", "", "", "", "", "", "Update1");
                if (UpdateCaseID != 0)
                {
                    int UpdateEmailorNumber = Lo.UpdateCodeFEAssignLL(Convert.ToInt64(hfexcelrawid.Value), "", "", Convert.ToDateTime(DateTime.Now), txtnumbersecondperson.Text, txtemailsecondperson.Text, "", "", "", "", "", "", "", "", "Update1_1");
                    SendMailNegativeCaseToAirtelOrOtherTeam();
                    panelstatuscase2.Visible = false;
                    panstatuscase1.Visible = false;
                    cleartext();
                    divmssgpopup.Visible = false;
                    string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    string script = string.Format("alert({0});window.location ='FE-Job';", message);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                }
            }
            else if (ddlselectcase.SelectedItem.Value == "03")
            {
                try
                {
                    int UpdateCaseID = Lo.UpdateCodeFEAssignLL(Convert.ToInt64(hfexcelrawid.Value), "", "", Convert.ToDateTime(DateTime.Now), ddlselectcase.SelectedItem.Text, hffename.Value, "", "", "", "", "", "", "", "", "Update2");
                    if (UpdateCaseID != 0)
                    {
                        SendMailNegativeCaseToAirtelOrOtherTeam();
                        cleartext();
                        divmssgpopup.Visible = false;
                        string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                        string script = string.Format("alert({0});window.location ='FE-Job';", message);
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                    }
                }
                catch (Exception ex)
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = ex.Message;
                    divmssgpopup.Attributes.Add("class", "alert alert-danger");
                }
            }
            else if (ddlselectcase.SelectedItem.Value == "09")
            {
                try
                {
                    int UpdateCaseID = Lo.UpdateCodeFEAssignLL(Convert.ToInt64(hfexcelrawid.Value), "", "", Convert.ToDateTime(DateTime.Now), ddlselectcase.SelectedItem.Text, hffename.Value, "", "", "", "", "", "", "", "", "Update3");
                    if (UpdateCaseID != 0)
                    {
                        SendMailNegativeCaseToAirtelOrOtherTeam();
                        cleartext();
                        divmssgpopup.Visible = false;
                        string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                        string script = string.Format("alert({0});window.location ='FE-Job';", message);
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                    }
                }
                catch (Exception ex)
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = ex.Message;
                    divmssgpopup.Attributes.Add("class", "alert alert-danger");
                }

            }
            else
            {
                if (txtemailsecondperson.Text != "" && txtnumbersecondperson.Text != "")
                {
                    if (hfEmailCustomer.Value != "" && hfEmailCustomer.Value == txtemailsecondperson.Text)
                    {

                        int UpdateCaseID = Lo.UpdateCodeFEAssignLL(Convert.ToInt64(hfexcelrawid.Value), "", "", Convert.ToDateTime(DateTime.Now), txtsecondpersonname.Text, txtsecondpersondesig.Text, "", "", "", "", "", "", "", "", "Update4");
                        if (UpdateCaseID != 0)
                        {
                            int UpdateEmailorNumber = Lo.UpdateCodeFEAssignLL(Convert.ToInt64(hfexcelrawid.Value), "", "", Convert.ToDateTime(DateTime.Now), txtnumbersecondperson.Text, hfEmailCustomer.Value, txtsecondpersonname.Text, txtsecondpersondesig.Text, "", "", "", "", "", "", "Update4_1");
                            SendMailClientSuccessVerification(hfNameCustomer.Value, hflogicalcircuitid.Value, hffename.Value);
                            cleartext();
                            BindAssignJob();
                            ddlselectcase.Enabled = false;
                            divmssgpopup.Visible = true;
                            divmssgpopup.InnerHtml = "SuccessFully Update Status";
                            divmssgpopup.Attributes.Add("class", "alert alert-success");
                            string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("SuccessFully Update Status");
                            string script = string.Format("alert({0});window.location ='FE-Job';", message);
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                        }
                    }
                    else
                    {
                        int UpdateCaseID = Lo.UpdateCodeFEAssignLL(Convert.ToInt64(hfexcelrawid.Value), "", "", Convert.ToDateTime(DateTime.Now), txtsecondpersonname.Text, txtsecondpersondesig.Text, "", "", "", "", "", "", "", "", "Update5");
                        if (UpdateCaseID != 0)
                        {
                            int UpdateEmailorNumber = Lo.UpdateCodeFEAssignLL(Convert.ToInt64(hfexcelrawid.Value), "", "", Convert.ToDateTime(DateTime.Now), txtnumbersecondperson.Text, txtemailsecondperson.Text, txtsecondpersonname.Text, txtsecondpersondesig.Text, "", "", "", "", "", "", "Update5_1");
                            SendMailClientSuccessVerification(hfNameCustomer.Value, hflogicalcircuitid.Value, hffename.Value);
                            cleartext();
                            ddlselectcase.Enabled = false;
                            divmssgpopup.Visible = true;
                            divmssgpopup.InnerHtml = "SuccessFully Update Status";
                            divmssgpopup.Attributes.Add("class", "alert alert-success");
                            string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("SuccessFully Update Status");
                            string script = string.Format("alert({0});window.location ='FE-Job';", message);
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                        }
                    }
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "Status not update Contact person Email or Phone no mandatory.";
                    divmssgpopup.Attributes.Add("class", "alert alert-danger");
                }
            }
        }
        catch (Exception ex)
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = ex.Message;
            divmssgpopup.Attributes.Add("class", "alert alert-danger");
        }
    }
    #endregion
    #region Revisit Case Code"
    protected void ddlrevisitrequired_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrevisitrequired.SelectedItem.Value == "01")
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = ("Please select any one option to update revisit status.");
            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
            revisittimedate.Visible = false;
        }
        else
        {
            divmssgpopup.Visible = false;
            revisittimedate.Visible = true;
        }
    }
    protected void btnsubmitrevisitstatus_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlrevisitrequired.SelectedItem.Value != "01" && txtrevisittimeanddate.Text != "" && txtrevisitpersonmetname.Text != "" && txtrevisitpersonmobileno.Text != "")
            {
                if (txtotprevisitcustmer.Text != "")
                {
                    DataTable DTOTPCheck = Lo.RetriveCodeLLFE(Convert.ToInt64(hfexcelrawid.Value), "", "", DateTime.Now, txtotprevisitcustmer.Text, "", "", "", "", "GetOTP");
                    if (DTOTPCheck.Rows.Count == 1)
                    {
                        if (txtotprevisitcustmer.Text == DTOTPCheck.Rows[0]["Mobile_OTP"].ToString())
                        {
                            DateTime Datetimerevisit = Convert.ToDateTime(txtrevisittimeanddate.Text);
                            string DatetimeRevis = Datetimerevisit.ToString("dd/MMM/yyyy");
                            DateTime DatetimerevisitTIme = Convert.ToDateTime(txttimerevisit.Text);
                            string DatetimeRevisTime = DatetimerevisitTIme.ToString("HH:mm:ss");
                            int UpdateCaseID = Lo.UpdateCodeFEAssignLL(Convert.ToInt64(hfexcelrawid.Value), "", "", Convert.ToDateTime(DatetimeRevis), txtrevisitpersonmetname.Text, DatetimeRevisTime, txtrevisitpersonmobileno.Text, ddlrevisitrequired.SelectedItem.Text, hffename.Value, txtrevisitpersonmetname.Text, "", "", "", "", "Update6");
                            if (UpdateCaseID != 0)
                            {
                                // SendMailClientPending();
                                DivOTPRevisit.Visible = false;
                                Divrevisit.Visible = false;
                                revisittimedate.Visible = false;
                                divmssgpopup.Visible = false;
                                cleartext();
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit rescheduled successfully.");
                                string script = string.Format("alert({0});window.location ='FE-Job';", message);
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                            }
                            else
                            {
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit rescheduled status not updated some error occure.");
                                string script = string.Format("alert({0});window.location ='FE-Job';", message);
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                            }
                        }
                        else
                        {
                            divmssgpopup.Visible = true;
                            divmssgpopup.InnerHtml = ("Invalid OTP");
                            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
                        }
                    }
                    else
                    {
                        divmssgpopup.Visible = true;
                        divmssgpopup.InnerHtml = ("Invalid OTP");
                        divmssgpopup.Attributes.Add("Class", "alert alert-danger");
                    }
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = ("Enter six digit OTP.");
                    divmssgpopup.Attributes.Add("Class", "alert alert-danger");
                }

            }
            else
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = ("Please enter customer detail.");
                divmssgpopup.Attributes.Add("Class", "alert alert-danger");
            }
        }
        catch (Exception ex)
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = ex.Message;
            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void btngetreviitotp_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlrevisitrequired.SelectedItem.Value != "01" && txtrevisittimeanddate.Text != "" && txtrevisitpersonmetname.Text != "" && txtrevisitpersonmobileno.Text != "")
            {
                GenerateOTP();
                //send("", "", "email");
                send(txtrevisitpersonmobileno.Text, "", "Mobile");
            }
            else
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = ("Please enter customer detail.");
                divmssgpopup.Attributes.Add("Class", "alert alert-danger");
            }
        }
        catch (Exception ex)
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = ex.Message.ToString();
            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void lbresendotp_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlrevisitrequired.SelectedItem.Value != "01" && txtrevisittimeanddate.Text != "" && txtrevisitpersonmetname.Text != "" && txtrevisitpersonmobileno.Text != "")
            {
                GenerateOTP();
               // send("", "", "email");
                 send(txtrevisitpersonmobileno.Text,"","Mobile");
            }
            else
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = ("Please enter customer detail.");
                divmssgpopup.Attributes.Add("Class", "alert alert-danger");
            }
        }
        catch (Exception ex)
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = ex.Message.ToString();
            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void GenerateOTP()
    {
        try
        {
            string numbers = "1234567890";
            string characters = numbers;
            int length = int.Parse("6");
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                }
                while (otp.IndexOf(character) != -1);
                otp += character;
            }
            hfotp.Value = otp;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    private void send(string _mobileNo, string email, string type)
    {
        try
        {
            if (type == "Mobile")
            {
                string requestUristring = string.Format("http://smsgateway.spicedigital.in/MessagingGateway//MessagePush?username=DefexpoT&password=Def@1234&messageType=text&mobile={0}&senderId=myrcpl&message=" + "Dear Airtel Customer, Your Airtel verification has been rescheduled OTP for the same is '" + hfotp.Value + "' Thank You.", _mobileNo);
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(requestUristring);
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
            }
            else if (type == "email")
            {
                sendMailOTP();
            }
            int UpdateRevisitStatusByFE = Lo.UpdateCodeFEAssignLL(Convert.ToInt64(hfexcelrawid.Value), "", "", Convert.ToDateTime(DateTime.Now), hfotp.Value, "", "", "", "", "", "", "", "", "", "Update6_1");
            if (UpdateRevisitStatusByFE != 0)
            {
                ddlrevisitrequired.Enabled = false;
                DivOTPRevisit.Visible = true;
                revisittimedate.Visible = false;
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = ("Enter OTP for submit status.");
                divmssgpopup.Attributes.Add("Class", "alert alert-warning");
            }
            else
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = ("OTP not send to user some error occure.");
                divmssgpopup.Attributes.Add("Class", "alert alert-danger");
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    private void sendMailOTP()
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/EmailOTP.html")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{OTP}", hfotp.Value);
        SendMail s;
        s = new SendMail();
        s.CreateMail("verification@rcpl.in", "verification@rcpl.in", "Airtel verification otp email!!", body);
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), Convert.ToInt16(ConfigurationManager.AppSettings["Port"].ToString()), ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        if (result == true && exMsg == "")
        {

            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = "OTP e-mail send successfully.";
            divmssgpopup.Attributes.Add("class", "alert alert-success");
        }
        else
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = exMsg;
            divmssgpopup.Attributes.Add("class", "alert alert-danger");
        }
    }
    #endregion
    #region "IF Company Shifted"
    protected void btnaddaddress_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlselectcase.SelectedItem.Value == "02")
            {
                if (txtnewcompanyaddress.Text != "")
                {
                    int UpdateNewCompAddress = Lo.UpdateCodeFEAssignLL(Convert.ToInt64(hfexcelrawid.Value), "", "", Convert.ToDateTime(DateTime.Now), txtcompshiftpersonmetname.Text, txtcompshiftpersonmetemail.Text, txtcompshiftpersonmetdesig.Text, txtnewcompanyaddress.Text, ddlselectcase.SelectedItem.Text, hffename.Value, txtcompshiftpersonmetname.Text, "", "", "", "Update7");
                    if (UpdateNewCompAddress != 0)
                    {
                        SendMailNegativeCaseToAirtelOrOtherTeam();
                        divmssgpopup.Visible = false;
                        cleartext();
                        string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                        string script = string.Format("alert({0});window.location ='FE-Job';", message);
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                    }
                    else
                    {
                        divmssgpopup.Visible = true;
                        divmssgpopup.InnerHtml = "Status not update some error occure.";
                        divmssgpopup.Attributes.Add("class", "alert alert-danger");
                    }
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "All field fill mandatory";
                    divmssgpopup.Attributes.Add("class", "alert alert-danger");
                }
            }
            else if (ddlselectcase.SelectedItem.Value == "04")
            {
                int UpdateNewCompAddress = Lo.UpdateCodeFEAssignLL(Convert.ToInt64(hfexcelrawid.Value), "", "", Convert.ToDateTime(DateTime.Now), txtcompshiftpersonmetname.Text, txtcompshiftpersonmetemail.Text, txtcompshiftpersonmetdesig.Text, ddlselectcase.SelectedItem.Text, hffename.Value, txtcompshiftpersonmetname.Text, "", "", "", "", "Update8");
                if (UpdateNewCompAddress != 0)
                {
                    SendMailNegativeCaseToAirtelOrOtherTeam();
                    divmssgpopup.Visible = false;
                    cleartext();
                    string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    string script = string.Format("alert({0});window.location ='FE-Job';", message);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "Status not update some error occure.";
                    divmssgpopup.Attributes.Add("class", "alert alert-danger");
                }
            }
        }
        catch (Exception ex)
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = ex.Message;
            divmssgpopup.Attributes.Add("class", "alert alert-danger");
        }
    }
    #endregion
    #region "EntryNotAllowed"
    protected void btnentrynotallwo_Click(object sender, EventArgs e)
    {
        if (txtremarkupdate.Text != "" && ddlsamper.SelectedValue != "01" && ddltlper.SelectedValue != "01" && ddlairmana.SelectedValue != "01" && ddlairper.SelectedValue != "01")
        {
            try
            {
                int UpdateCaseID = Lo.UpdateCodeFEAssignLL(Convert.ToInt64(hfexcelrawid.Value), "", "", Convert.ToDateTime(DateTime.Now), txtremarkupdate.Text, hffename.Value, ddlsamper.SelectedItem.Text.Substring(0, 1) + "-" + ddltlper.SelectedItem.Text.Substring(0, 1) + "-" + ddlairper.SelectedItem.Text.Substring(0, 1) + "-" + ddlairmana.SelectedItem.Text.Substring(0, 1), "", "", "", "", "", "", "", "Update9");
                if (UpdateCaseID != 0)
                {
                    SendMailNegativeCaseToAirtelOrOtherTeam();
                    cleartext();
                    divmssgpopup.Visible = false;
                    string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    string script = string.Format("alert({0});window.location ='FE-Job';", message);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "Audit not completed some error occure.";
                    divmssgpopup.Attributes.Add("class", "alert alert-danger");
                }
            }
            catch (Exception ex)
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = "Audit not completed some error occure. Technical Error:-" + ex.Message.ToString() + "";
                divmssgpopup.Attributes.Add("class", "alert alert-danger");
            }
        }
        else
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = "All field fill mandatory.";
            divmssgpopup.Attributes.Add("class", "alert alert-danger");
        }
    }
    #endregion
    #region Customer Denied Audit"
    protected void btnsubcustdeniedaudit_Click(object sender, EventArgs e)
    {
        if (txtreasoneremarkauditcustomer.Text != "")
        {
            int UpdateCaseID = Lo.UpdateCodeFEAssignLL(Convert.ToInt64(hfexcelrawid.Value), "", "", Convert.ToDateTime(DateTime.Now), txtcustomerdeniedmetname.Text, txtcustomerdeniedmetemail.Text, txtcustomerdeniedmetdesi.Text, txtreasoneremarkauditcustomer.Text, hffename.Value, txtcustomerdeniedmetname.Text, "", "", "", "", "Update10");
            if (UpdateCaseID != 0)
            {
                SendMailNegativeCaseToAirtelOrOtherTeam();
                cleartext();
                divmssgpopup.Visible = false;
                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                string script = string.Format("alert({0});window.location ='FE-Job';", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
            else
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = "Audit not completed some error occure.";
                divmssgpopup.Attributes.Add("class", "alert alert-danger");
            }
        }
        else
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = "All field fill mandatory.";
            divmssgpopup.Attributes.Add("class", "alert alert-danger");
        }
    }
    #endregion
    #region SendMailCode
    protected void SendMailClientSuccessVerification(string PartyorCust, string Logicalcircuitid, string FEName)
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/MoPositivePending.htm")))
        {
            body = reader.ReadToEnd();
        }
        if (PartyorCust != "")
        {
            PartyorCust = hfNameCustomer.Value;
        }
        else
        {
            PartyorCust = hfPartyName.Value;
        }
        body = body.Replace("{PartyName}", PartyorCust);
        body = body.Replace("{Logical}", Logicalcircuitid);
        body = body.Replace("{FEName}", FEName);
        body = body.Replace("{Message}", " This is for your information that verification as per DoT regulations for Leased Line has been completed successfully.");
        SendMail s;
        s = new SendMail();
        s.CreateMail("verification@rcpl.in", "verification@rcpl.in", "Airtel verification status email!!", body);
        //s.CreateMail("verification@rcpl.in", "mohdwali@globalitpoint.com", "Airtel verification status email!!", body);
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), Convert.ToInt16(ConfigurationManager.AppSettings["Port"].ToString()), ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
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
    protected void SendMailNegativeCaseToAirtelOrOtherTeam()
    {
        DataTable DTRetriveClient = Lo.RetriveCodeLLFE(Convert.ToInt64(hfexcelrawid.Value), "", "", DateTime.Now, "", "", "", "", "", "GetEmail");
        if (DTRetriveClient.Rows.Count > 0)
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/NegativeCaseMailToAirtel.htm")))
            {
                body = reader.ReadToEnd();
            }
            StringBuilder html = new StringBuilder();
            html.Append("<table border = '1'>");
            html.Append("<tr>");
            foreach (DataColumn column in DTRetriveClient.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");
            {
                html.Append("<tr>");
                foreach (DataColumn column in DTRetriveClient.Columns)
                {
                    html.Append("<td>");
                    html.Append(DTRetriveClient.Rows[0][column.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("</tr>");
            }
            html.Append("</table>");
            body = body.Replace("{dt}", html.ToString());
            body = body.Replace("{ClientName}", DTRetriveClient.Rows[0]["Party_Name"].ToString());
            SendMail s;
            s = new SendMail();
            s.CreateMail("verification@rcpl.in", "verification@rcpl.in", "Airtel " + DTRetriveClient.Rows[0]["Product_Name"].ToString() + " Verification  " + DTRetriveClient.Rows[0]["Type_of_Inspection"].ToString() + " - " + DTRetriveClient.Rows[0]["Party_Name"].ToString() + "", body);
            // s.CreateMail("verification@rcpl.in", "mohdwali@globalitpoint.com", "Airtel " + DTRetriveClient.Rows[0]["Product_Name"].ToString() + " Verification  " + DTRetriveClient.Rows[0]["Type_of_Inspection"].ToString() + " - " + DTRetriveClient.Rows[0]["Party_Name"].ToString() + "", body);
            string exMsg = "";
            bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), Convert.ToInt16(ConfigurationManager.AppSettings["Port"].ToString()), ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
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
    }
    protected void SendMailClientPending()
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/SuccessorFailVerification.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Message}", "This for your information that verification as per DoT regulations for Leased Line has been resechduled " + txtrevisittimeanddate.Text + ". <br /><br />Thanks for choosing Airtel as your preferred service partner. <br /><br />assuring you our best services always. <br /><br />");
        SendMail s;
        s = new SendMail();
        s.CreateMail("verification@rcpl.in", "verification@rcpl.in", "Airtel verification status email!!", body);
        //s.CreateMail("verification@rcpl.in", "mohdwali@globalitpoint.com", "Airtel verification status email!!", body);
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), Convert.ToInt16(ConfigurationManager.AppSettings["Port"].ToString()), ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
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
    #endregion
}