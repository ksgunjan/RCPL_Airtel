using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;


public partial class frmAssignJobFE : System.Web.UI.Page
{
    #region variable
    Logic Lo = new Logic();
    Common Co = new Common();
    string Email;
    string HFRevisitDate;
    string hfPreVisit;
    #endregion
    #region Other Basic Function
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            GetFeBasicDetail();
            BindAssignJob();
        }
    }
    protected void BindAssignJob(string sortExpression = null)
    {
        DataTable DtAssignJob = new DataTable();
        if (ddlprodbycat.SelectedItem.Value == "0")
        {
            DtAssignJob = Lo.RetriveCodeAllExcelRecordWhereCondition("[FE Name]='" + hffename.Value + "' and IsClosed !='Y' and IsActive='Y' and [Eligible/Not Eligible]='Eligible' and (StatusByFE='' or StatusByFE is null or StatusByFE='Pending') and ProductName='Leased Line' order by [Party Name] asc");
        }
        else if (ddlprodbycat.SelectedItem.Value == "Leased Line-Initial")
        {
            DtAssignJob = Lo.RetriveCodeAllExcelRecordWhereCondition("[FE Name]='" + hffename.Value + "' and IsClosed !='Y' and IsActive='Y' and [Eligible/Not Eligible]='Eligible' and (StatusByFE='' or StatusByFE is null or StatusByFE='Pending') and ProdTypeName='Leased Line-Initial' order by [Party Name] asc");
        }
        else if (ddlprodbycat.SelectedItem.Value == "Leased Line-Periodic")
        {
            DtAssignJob = Lo.RetriveCodeAllExcelRecordWhereCondition("[FE Name]='" + hffename.Value + "' and IsClosed !='Y' and IsActive='Y' and [Eligible/Not Eligible]='Eligible' and (StatusByFE='' or StatusByFE is null or StatusByFE='Pending') and ProdTypeName='Leased Line-Periodic' order by [Party Name] asc");
        }
        if (DtAssignJob.Rows.Count > 0)
        {
            if (sortExpression != null)
            {
                DataView dv = DtAssignJob.AsDataView();
                this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                dv.Sort = sortExpression + " " + this.SortDirection;
                gvexcel.DataSource = dv;
            }
            else
            {
                gvexcel.DataSource = DtAssignJob;
            }
            gvexcel.DataBind();
            lblcounttotal.Text = "Total Assign job- " + DtAssignJob.Rows.Count.ToString();
            gvexcel.Visible = true;
            diverror.Visible = false;
            diverror.InnerHtml = "";
        }
        else
        {
            lblcounttotal.Text = "Total Assign job- " + DtAssignJob.Rows.Count.ToString();
            gvexcel.Visible = false;
            diverror.Visible = true;
            diverror.InnerHtml = "No job assign " + ddlprodbycat.SelectedItem.Text;
            diverror.Attributes.Add("class", "alert alert-danger");
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
        hflocsrid.Value = "";
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
        diverror.Visible = false;
        diverror.InnerHtml = "";
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
    protected void GetFeBasicDetail()
    {
        try
        {
            DataTable DtBasicdetilofFE = Lo.RetriveCodeWithContidion("select Name from tbl_mst_Employee where Email='" + Session["LoginEmail"].ToString() + "'");
            if (DtBasicdetilofFE.Rows.Count > 0)
            {
                hffename.Value = DtBasicdetilofFE.Rows[0]["Name"].ToString();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #endregion
    #region SortCode"
    private string SortDirection
    {
        get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }
    #endregion
    #region Gridview Code or Commande Code"
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
                    divshow1.Visible = true;
                    divupdatebyfe.Visible = false;
                    ddlselectcase.Enabled = true;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                }
                else
                {
                    diverror.InnerHtml = "No record found.";
                    diverror.Attributes.Add("class", "alert alert-danger");
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
                    hflocsrid.Value = e.CommandArgument.ToString();
                    hfEmailCustomer.Value = Email;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                }
                else
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = "Status already update please refresh page for see updated list.";
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
                        e.Row.BackColor = System.Drawing.Color.Red;
                        e.Row.Visible = true;
                        e.Row.ForeColor = System.Drawing.Color.White;
                    }
                    else
                    {

                        e.Row.Visible = false;
                    }
                }

            }
            if (hfPreVisit != "")
            {
                GridViewRow grv = e.Row;
                if (hfPreVisit == DateTime.Now.ToString("dd/MMM/yyyy"))
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.Visible = true;
                    e.Row.ForeColor = System.Drawing.Color.White;
                }
            }
        }
    }
    protected void OnSorting(object sender, GridViewSortEventArgs e)
    {
        this.BindAssignJob(e.SortExpression);
    }
    #endregion
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
                Int32 UpdateCaseID = Lo.UpdateCodeOther("tbl_trn_RawData", "Set [Positive/Negative]='Negative',Code='No Stamp and No Email',[ReasonOfStatus]='No Stamp and No Email',[RemarksOfStatus]='Date:-" + DateTime.Now.ToString("dd-MMM") + ",,Met Person:-" + txtsecondpersonname.Text + ",Relation:-" + txtsecondpersondesig.Text + ",CONFIRMATION:Company did not put stamp and also did not email',[StatusByFE]='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1'", "[Eligible/Not Eligible]='Eligible' and [UniqueID]='" + hflocsrid.Value + "'");
                if (UpdateCaseID != 0)
                {
                    Int32 UpdateEmailorNumber = Lo.UpdateCodeOther("tbl_trn_RawData", "Set PersonMetMobileNo='" + txtnumbersecondperson.Text + "',PersonMetEmail='" + txtemailsecondperson.Text + "',PersonMetName='" + txtsecondpersonname.Text + "',PersonMetDesignation='" + txtsecondpersondesig.Text + "'", "[Eligible/Not Eligible]='Eligible' and [UniqueID]='" + hflocsrid.Value + "'");
                    SendMailNegativeCaseToAirtelOrOtherTeam();
                    panelstatuscase2.Visible = false;
                    panstatuscase1.Visible = false;
                    cleartext();
                    divmssgpopup.Visible = false;
                    var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    var script = string.Format("alert({0});window.location ='FE-Job';", message);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                }
            }
            else if (ddlselectcase.SelectedItem.Value == "03")
            {
                try
                {
                    Int32 UpdateCaseID = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='COMPANY CLOSED',[Positive/Negative]='Negative',[ReasonOfStatus]='" + ddlselectcase.SelectedItem.Text + "',[RemarksOfStatus]='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Address by " + hffename.Value + ". Company Are Parmanetly Closed To Here',[StatusByFE]='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1'", "[Eligible/Not Eligible]='Eligible' and [UniqueID]='" + hflocsrid.Value + "'");
                    if (UpdateCaseID != 0)
                    {
                        SendMailNegativeCaseToAirtelOrOtherTeam();
                        cleartext();
                        divmssgpopup.Visible = false;
                        var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                        var script = string.Format("alert({0});window.location ='FE-Job';", message);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
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
                    Int32 UpdateCaseID = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='ADDRESS NOT FOUND',[Positive/Negative]='Negative',[ReasonOfStatus]='" + ddlselectcase.SelectedItem.Text + "',[RemarksOfStatus]='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Address by " + hffename.Value + ". Company Address is not found.',[StatusByFE]='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1'", "[Eligible/Not Eligible]='Eligible' and [UniqueID]='" + hflocsrid.Value + "'");
                    if (UpdateCaseID != 0)
                    {
                        SendMailNegativeCaseToAirtelOrOtherTeam();
                        cleartext();
                        divmssgpopup.Visible = false;
                        var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                        var script = string.Format("alert({0});window.location ='FE-Job';", message);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
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
                        Int32 UpdateCaseID = Lo.UpdateCodeOther("tbl_trn_RawData", "Set [Positive/Negative]='Positive',[ReasonOfStatus]='Verification Done',Code='OK',[RemarksOfStatus]='Date:-" + DateTime.Now.ToString("dd-MMM") + ",Met Person:-" + txtsecondpersonname.Text + ",Relation:-" + txtsecondpersondesig.Text + ",CONFIRMATION:DONE',[StatusByFE]='Positive',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "'", "[Eligible/Not Eligible]='Eligible' and [UniqueID]='" + hflocsrid.Value + "'");
                        if (UpdateCaseID != 0)
                        {
                            Int32 UpdateEmailorNumber = Lo.UpdateCodeOther("tbl_trn_RawData", "Set PersonMetMobileNo='" + txtnumbersecondperson.Text + "',PersonMetEmail='" + hfEmailCustomer.Value + "',PersonMetName='" + txtsecondpersonname.Text + "',PersonMetDesignation='" + txtsecondpersondesig.Text + "'", "[Eligible/Not Eligible]='Eligible' and [UniqueID]='" + hflocsrid.Value + "'");
                            SendMailClientSuccessVerification(hfNameCustomer.Value, hflogicalcircuitid.Value, hffename.Value);
                            cleartext();
                            BindAssignJob();
                            ddlselectcase.Enabled = false;
                            divmssgpopup.Visible = true;
                            divmssgpopup.InnerHtml = "SuccessFully Update Status";
                            divmssgpopup.Attributes.Add("class", "alert alert-success");
                            var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("SuccessFully Update Status");
                            var script = string.Format("alert({0});window.location ='FE-Job';", message);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                        }
                    }
                    else
                    {
                        Int32 UpdateCaseID = Lo.UpdateCodeOther("tbl_trn_RawData", "Set [Positive/Negative]='Positive',[ReasonOfStatus]='Verification Done',Code='OK',[RemarksOfStatus]='Date:-" + DateTime.Now.ToString("dd-MMM") + ",Met Person:-" + txtsecondpersonname.Text + ",Relation:-" + txtsecondpersondesig.Text + ",CONFIRMATION:DONE',[StatusByFE]='Positive',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "'", "[Eligible/Not Eligible]='Eligible' and [UniqueID]='" + hflocsrid.Value + "'");
                        if (UpdateCaseID != 0)
                        {
                            Int32 UpdateEmailorNumber = Lo.UpdateCodeOther("tbl_trn_RawData", "Set PersonMetMobileNo='" + txtnumbersecondperson.Text + "',PersonMetEmail='" + txtemailsecondperson.Text + "',PersonMetName='" + txtsecondpersonname.Text + "',PersonMetDesignation='" + txtsecondpersondesig.Text + "'", "[Eligible/Not Eligible]='Eligible' and [UniqueID]='" + hflocsrid.Value + "'");
                            SendMailClientSuccessVerification(hfNameCustomer.Value, hflogicalcircuitid.Value, hffename.Value);
                            cleartext();
                            ddlselectcase.Enabled = false;
                            divmssgpopup.Visible = true;
                            divmssgpopup.InnerHtml = "SuccessFully Update Status";
                            divmssgpopup.Attributes.Add("class", "alert alert-success");
                            var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("SuccessFully Update Status");
                            var script = string.Format("alert({0});window.location ='FE-Job';", message);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
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
                    DataTable DTOTPCheck = Lo.RetriveCodeAllExcelRecordWhereCondition("[Mobile OTP]='" + txtotprevisitcustmer.Text + "' and UniqueID='" + hflocsrid.Value + "' and [Eligible/Not Eligible]='Eligible' and (StatusByFE ='Pending' or StatusByFE is null or StatusByFE='')");
                    if (DTOTPCheck.Rows.Count == 1)
                    {
                        if (txtotprevisitcustmer.Text == DTOTPCheck.Rows[0]["Mobile OTP"].ToString())
                        {
                            DateTime Datetimerevisit = Convert.ToDateTime(txtrevisittimeanddate.Text);
                            string DatetimeRevis = Datetimerevisit.ToString("dd/MMM/yyyy");
                            DateTime DatetimerevisitTIme = Convert.ToDateTime(txttimerevisit.Text);
                            string DatetimeRevisTime = DatetimerevisitTIme.ToString("HH:mm:ss");
                            Int32 UpdateRevisitStatusByFE = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='Revisit Required' ,[Revisit PerSon Name]='" + txtrevisitpersonmetname.Text + "',[Revisit Date Time]='" + DatetimeRevis + "',[Revisit Time]='" + DatetimeRevisTime + "',PersonMetMobileNo='" + txtrevisitpersonmobileno.Text + "',[IsOTPVerified]='Y',ReasonOfStatus='Revisit," + ddlrevisitrequired.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + "Visit Done At The Given Address by " + hffename.Value + " Met With Mr. " + txtrevisitpersonmetname.Text + "  He Said Aouthrised Person Not Avavilable At This Time Please Again Visit On',StatusByFE='Pending',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Pending'", "UniqueID='" + hflocsrid.Value + "' and [Eligible/Not Eligible]='Eligible'");
                            if (UpdateRevisitStatusByFE != 0)
                            {
                                // SendMailClientPending();
                                DivOTPRevisit.Visible = false;
                                Divrevisit.Visible = false;
                                revisittimedate.Visible = false;
                                divmssgpopup.Visible = false;
                                cleartext();
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit rescheduled successfully.");
                                var script = string.Format("alert({0});window.location ='FE-Job';", message);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                            }
                            else
                            {
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit rescheduled status not updated some error occure.");
                                var script = string.Format("alert({0});window.location ='FE-Job';", message);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
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
                send(txtrevisitpersonmobileno.Text);
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
                send(txtrevisitpersonmobileno.Text);
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
    private void send(string _mobileNo)
    {
        try
        {
            string requestUristring = string.Format("http://smsgateway.spicedigital.in/MessagingGateway//MessagePush?username=DefexpoT&password=Def@1234&messageType=text&mobile={0}&senderId=MYRCPL&message=" + "Dear Airtel Customer, Your Airtel verification has been rescheduled OTP for the same is '" + hfotp.Value + "' Thank You.", _mobileNo);
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(requestUristring);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
            Int32 UpdateRevisitStatusByFE = Lo.UpdateCodeOther("tbl_trn_RawData", "Set [Mobile OTP]='" + hfotp.Value + "'", "UniqueID='" + hflocsrid.Value + "' and [Eligible/Not Eligible]='Eligible'");
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
                    Int32 UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='Shifted',PersonMetName='" + txtcompshiftpersonmetname.Text + "',PersonMetEmail='" + txtcompshiftpersonmetemail.Text + "',PersonMetDesignation='" + txtcompshiftpersonmetdesig.Text + "',[New Address of Company]='" + txtnewcompanyaddress.Text + "',ReasonOfStatus='" + ddlselectcase.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Address by " + hffename.Value + " Met With Mr./Ms. " + txtcompshiftpersonmetname.Text + " He/She Told This Compay Is Shifted From Here Last , And New Address Is:-NO',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1'", "UniqueID='" + hflocsrid.Value + "' and [Eligible/Not Eligible]='Eligible'");
                    if (UpdateNewCompAddress != 0)
                    {
                        SendMailNegativeCaseToAirtelOrOtherTeam();
                        divmssgpopup.Visible = false;
                        cleartext();
                        var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                        var script = string.Format("alert({0});window.location ='FE-Job';", message);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
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
                Int32 UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='Already Disconnected',PersonMetName='" + txtcompshiftpersonmetname.Text + "',PersonMetEmail='" + txtcompshiftpersonmetemail.Text + "',PersonMetDesignation='" + txtcompshiftpersonmetdesig.Text + "',ReasonOfStatus='" + ddlselectcase.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Address by " + hffename.Value + " Met With Mr./Ms. " + txtcompshiftpersonmetname.Text + " He/She Told We Already Disconnected The All Airtel Corporate Connection',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1'", "UniqueID='" + hflocsrid.Value + "' and [Eligible/Not Eligible]='Eligible'");
                if (UpdateNewCompAddress != 0)
                {
                    SendMailNegativeCaseToAirtelOrOtherTeam();
                    divmssgpopup.Visible = false;
                    cleartext();
                    var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    var script = string.Format("alert({0});window.location ='FE-Job';", message);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
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
                Int32 UpdateCaseID = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='ENA', [Positive/Negative]='Negative',[ReasonOfStatus]='" + txtremarkupdate.Text + "',[RemarksOfStatus]='" + DateTime.Now.ToString("dd-MMM") + "Visit Done At The Given Add by " + hffename.Value + " Met With Guard. He Said Entry Not Allowed Without Appointment ',[StatusByFE]='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1',IsCall='" + ddlsamper.SelectedItem.Text.Substring(0, 1) + "-" + ddltlper.SelectedItem.Text.Substring(0, 1) + "-" + ddlairper.SelectedItem.Text.Substring(0, 1) + "-" + ddlairmana.SelectedItem.Text.Substring(0, 1) + "'", "[Eligible/Not Eligible]='Eligible' and [UniqueID]='" + hflocsrid.Value + "'");
                if (UpdateCaseID != 0)
                {
                    SendMailNegativeCaseToAirtelOrOtherTeam();
                    cleartext();
                    divmssgpopup.Visible = false;
                    var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    var script = string.Format("alert({0});window.location ='FE-Job';", message);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
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
            Int32 UpdateCaseID = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='NOT INTERESTED', PersonMetName='" + txtcustomerdeniedmetname.Text + "',PersonMetEmail='" + txtcustomerdeniedmetemail.Text + "',PersonMetDesignation='" + txtcustomerdeniedmetdesi.Text + "',[Positive/Negative]='Negative',[ReasonOfStatus]='" + txtreasoneremarkauditcustomer.Text + "',[RemarksOfStatus]='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Address by " + hffename.Value + " Met With Mr./Ms. " + txtcustomerdeniedmetname.Text + " He/She Said I am Not Interested To Any Types Of Verification And reason Not Disclosed',[StatusByFE]='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1'", "[Eligible/Not Eligible]='Eligible' and [UniqueID]='" + hflocsrid.Value + "'");
            if (UpdateCaseID != 0)
            {
                SendMailNegativeCaseToAirtelOrOtherTeam();
                cleartext();
                divmssgpopup.Visible = false;
                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                var script = string.Format("alert({0});window.location ='FE-Job';", message);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
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
        // s.CreateMail("verification@rcpl.in", "mohdwali@globalitpoint.com", "Airtel verification status email!!", body);
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
    protected void SendMailNegativeCaseToAirtelOrOtherTeam()
    {
        DataTable DTRetriveClient = Lo.RetriveCodeWithContidion("select [Logical Circuit Id],BILLING_BANDWIDTH,[Party Name],ProductName,[From Site],CONVERT(date,StatusUpdateDate,103) as StatusUpdateDate,[Positive/Negative],ReasonOfStatus as Code,ProdTypeName as [Type of Inspection],SAM_Name,SAM_TL_EMAIL from tbl_trn_RawData where UniqueID='" + hflocsrid.Value + "' and StatusByFE='Negative'");
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
            body = body.Replace("{ClientName}", DTRetriveClient.Rows[0]["Party Name"].ToString());
            SendMail s;
            s = new SendMail();
            s.CreateMail("verification@rcpl.in", "verification@rcpl.in", "Airtel " + DTRetriveClient.Rows[0]["ProductName"].ToString() + " Verification  " + DTRetriveClient.Rows[0]["Type of Inspection"].ToString() + " - " + DTRetriveClient.Rows[0]["Party Name"].ToString() + "", body);
            // s.CreateMail("verification@rcpl.in", "mohdwali@globalitpoint.com", "Airtel " + DTRetriveClient.Rows[0]["ProductName"].ToString() + " Verification  " + DTRetriveClient.Rows[0]["Type of Inspection"].ToString() + " - " + DTRetriveClient.Rows[0]["Party Name"].ToString() + "", body);
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
        //  s.CreateMail("verification@rcpl.in", "mohdwali@globalitpoint.com", "Airtel verification status email!!", body);
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
    #endregion
    protected void ddlprodbycat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAssignJob();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        string sortExpression = null;
        if (txtsearch.Text != "")
        {
            DataTable DtAssignJob = new DataTable();
            if (ddlprodbycat.SelectedItem.Value == "0")
            {
                DtAssignJob = Lo.RetriveCodeAllExcelRecordWhereCondition(" ([Logical Circuit ID]='" + txtsearch.Text + "' or [Party Name] like '%" + txtsearch.Text + "%') and [FE Name]='" + hffename.Value + "' and IsClosed !='Y' and IsActive='Y' and [Eligible/Not Eligible]='Eligible' and (StatusByFE='' or StatusByFE is null or StatusByFE='Pending') and ProductName='Leased Line' order by [Party Name] asc");
            }
            else if (ddlprodbycat.SelectedItem.Value == "Leased Line-Initial")
            {
                DtAssignJob = Lo.RetriveCodeAllExcelRecordWhereCondition(" ([Logical Circuit ID]='" + txtsearch.Text + "' or [Party Name]='" + txtsearch.Text + "') and [FE Name]='" + hffename.Value + "' and IsClosed !='Y' and IsActive='Y' and [Eligible/Not Eligible]='Eligible' and (StatusByFE='' or StatusByFE is null or StatusByFE='Pending') and ProdTypeName='Leased Line-Initial' order by [Party Name] asc");
            }
            else if (ddlprodbycat.SelectedItem.Value == "Leased Line-Periodic")
            {
                DtAssignJob = Lo.RetriveCodeAllExcelRecordWhereCondition("([Logical Circuit ID]='" + txtsearch.Text + "' or [Party Name] like '%" + txtsearch.Text + "%') and [FE Name]='" + hffename.Value + "' and IsClosed !='Y' and IsActive='Y' and [Eligible/Not Eligible]='Eligible' and (StatusByFE='' or StatusByFE is null or StatusByFE='Pending') and ProdTypeName='Leased Line-Periodic' order by [Party Name] asc");
            }
            if (DtAssignJob.Rows.Count > 0)
            {
                if (sortExpression != null)
                {
                    DataView dv = DtAssignJob.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gvexcel.DataSource = dv;
                }
                else
                {
                    gvexcel.DataSource = DtAssignJob;
                }
                gvexcel.DataBind();
                lblcounttotal.Text = "Total Assign job- " + DtAssignJob.Rows.Count.ToString();
                gvexcel.Visible = true;
                diverror.Visible = false;
                diverror.InnerHtml = "";
                txtsearch.Text = "";
            }
            else
            {
                lblcounttotal.Text = "Total Assign job- " + DtAssignJob.Rows.Count.ToString();
                gvexcel.Visible = false;
                diverror.Visible = true;
                diverror.InnerHtml = "No job assign " + ddlprodbycat.SelectedItem.Text;
                diverror.Attributes.Add("class", "alert alert-danger");
                txtsearch.Text = "";
            }
        }
    }
}