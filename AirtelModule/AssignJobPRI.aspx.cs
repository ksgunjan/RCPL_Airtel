using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

public partial class AssignJobPRI : System.Web.UI.Page
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
            BindAssignJobCompany();
        }
    }
    protected void BindAssignJobCompany()
    {
        string sortExpression = null;
        DataTable DtAssignJob = new DataTable();
        DtAssignJob = Lo.RetriveCodeAllExcelRecordWhereCondition("[FE Name]='" + hfFeName.Value + "' and IsClosed !='Y' and IsActive='Y' and [Eligible/Not Eligible]='Eligible' and (StatusByFE='' or StatusByFE is null or StatusByFE='Pending') and ProductName='PRI-Fixed Line' and KeyDup='false' order by [Party Name] asc");
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
            diverror.InnerHtml = "No job assign ";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void cleartext()
    {
        txtnewaddress.Text = "";
        txtotpnegative.Text = "";
        txtotprevisitcustmer.Text = "";
        txtpersonemetmobilenosuccess.Text = "";
        txtpersonmetemailid.Text = "";
        txtpersonmetemailidsuccess.Text = "";
        txtpersonmetmobno.Text = "";
        txtpersonmetname.Text = "";
        txtpersonmetnamesuccess.Text = "";
        txtremarkcust.Text = "";
        txtrevisitpersonmetname.Text = "";
        txtrevisitpersonmobileno.Text = "";
        txtrevisittimeanddate.Text = "";
        txttimerevisit.Text = "";
        ddlairmana.SelectedValue = "01";
        ddlairper.SelectedValue = "01";
        ddlcondition.SelectedValue = "01";
        ddlsamper.SelectedValue = "01";
        ddltlper.SelectedValue = "01";
        hfcompname.Value = "";
        hfEmailCustomer.Value = "";
        hflocsrid.Value = "";
        // hfNameCustomer.Value = "";
        hfPreVisit = "";
        HFRevisitDate = "";
    }
    protected void GetFeBasicDetail()
    {
        try
        {
            DataTable DtBasicdetilofFE = Lo.RetriveCodeWithContidion("select Name from tbl_mst_Employee where Email='" + Session["LoginEmail"].ToString() + "'");
            if (DtBasicdetilofFE.Rows.Count > 0)
            {
                hfFeName.Value = DtBasicdetilofFE.Rows[0]["Name"].ToString();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #region SortCode"
    private string SortDirection
    {
        get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }
    #endregion
    #endregion
    #region Gridview or DataList Code or Commande Code"
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
                    div2.Visible = false;
                    panmoallpanel.Visible = false;
                    cleartext();
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
                    hfPartyName.Value = (gvexcel.Rows[rowIndex].FindControl("lblcompnamemo") as Label).Text;
                    hflogicalcircuitid.Value = (gvexcel.Rows[rowIndex].FindControl("hfbilledextid") as HiddenField).Value;
                    hflocsrid.Value = e.CommandArgument.ToString();
                    hfEmailCustomer.Value = Email;
                    panmoallpanel.Visible = true;
                    divshow1.Visible = false;
                    div2.Visible = true;
                    pansuccess.Visible = false;
                    pannegativecondition.Visible = false;
                    panrevisitcondition.Visible = false;
                    ddlcondition.Enabled = true;
                    panaddressorcompclose.Visible = false;
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
    #endregion
    #region Select Process By DLL
    protected void ddlcondition_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcondition.SelectedValue == "01")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = false;
        }
        else if (ddlcondition.SelectedValue == "02")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = true;
            divcustomershifted.Visible = true;
            ddlcondition.Enabled = false;
            btnnextnegative.Text = "Submit";
        }
        else if (ddlcondition.SelectedValue == "03")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = false;
            ddlcondition.Enabled = false;
            panaddressorcompclose.Visible = true;

        }
        else if (ddlcondition.SelectedValue == "04")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = true;
            divcustomershifted.Visible = false;
            diventrynotallowd.Visible = false;
            ddlcondition.Enabled = false;
        }
        else if (ddlcondition.SelectedValue == "05")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = true;
            divcustomershifted.Visible = false;
            diventrynotallowd.Visible = true;
            ddlcondition.Enabled = false;
            divnegativeotp.Visible = false;
            diventrynotallowd.Visible = true;
            btnnextnegative.Text = "Submit";
        }
        else if (ddlcondition.SelectedValue == "06")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = true;
            divnegativeotp.Visible = true;
            divcustomershifted.Visible = false;
            diventrynotallowd.Visible = false;
            ddlcondition.Enabled = false;
        }
        else if (ddlcondition.SelectedValue == "07")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = true;
            divcustomershifted.Visible = false;
            diventrynotallowd.Visible = false;
            divnegativeotp.Visible = true;
            ddlcondition.Enabled = false;
        }
        else if (ddlcondition.SelectedValue == "08")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = true;
            divcustomershifted.Visible = false;
            diventrynotallowd.Visible = false;
            ddlcondition.Enabled = false;
            divnegativeotp.Visible = true;
        }
        else if (ddlcondition.SelectedValue == "09")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = true;
            divcustomershifted.Visible = false;
            diventrynotallowd.Visible = false;
            ddlcondition.Enabled = false;
            divnegativeotp.Visible = true;
        }
        else if (ddlcondition.SelectedValue == "10")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = true;
            pannegativecondition.Visible = false;
            divcustomershifted.Visible = false;
            diventrynotallowd.Visible = false;
            ddlcondition.Enabled = false;
        }
        else if (ddlcondition.SelectedValue == "11")
        {
            pansuccess.Visible = true;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = false;
            divcustomershifted.Visible = false;
            diventrynotallowd.Visible = false;
            ddlcondition.Enabled = false;
        }
        else if (ddlcondition.SelectedValue == "12")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = false;
            ddlcondition.Enabled = false;
            panaddressorcompclose.Visible = true;

        }
    }
    #endregion
    #region OTPCode
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
                } while (otp.IndexOf(character) != -1);
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
            string requestUristring = string.Format("http://smsgateway.spicedigital.in/MessagingGateway//MessagePush?username=DefexpoT&password=Def@1234&messageType=text&mobile={0}&senderId=MYRCPL&message=" + "Dear Airtel Customer, Your Airtel verification OTP for the same is '" + hfotp.Value + "' Thank You.", _mobileNo);
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(requestUristring);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
            Int32 UpdateRevisitStatusByFE = Lo.UpdateCodeOther("tbl_trn_RawData", "Set [Mobile OTP]='" + hfotp.Value + "'", "UniqueID='" + hflocsrid.Value + "' and [Eligible/Not Eligible]='Eligible'");
            if (UpdateRevisitStatusByFE != 0)
            {
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
    #region NegativeCaseSubmit"
    protected void btnnextnegative_Click(object sender, EventArgs e)
    {
        if (ddlcondition.SelectedValue == "02")
        {
            if (txtnewaddress.Text != "")
            {
                Int32 UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='SHIFTED', [New Address of Company]='" + txtnewaddress.Text + "',PersonMetMobileNo='" + txtpersonmetmobno.Text + "',PersonMetName='" + txtpersonmetname.Text + "',PersonMetEmail='" + txtpersonmetemailid.Text + "',PersonMetDesignation='" + txtpersonmetdesig.Text + "',ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Address by " + hfFeName.Value + " Met With Mr./Ms. " + txtpersonmetname.Text + " He/She Told This Compay Is Shifted From Here Last , And New Address Is:-NO',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1'", "[Eligible/Not Eligible]='Eligible' and UniqueID='" + hflocsrid.Value + "' and  (StatusByFE='' or StatusByFE is null) and Productname ='PRI-Fixed Line' and KeyDUP='false'");
                if (UpdateNewCompAddress != 0)
                {
                    SendMailNegativeCaseToAirtelOrOtherTeam();
                    cleartext();
                    var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    var script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
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
                divmssgpopup.InnerHtml = "Please fill new address";
                divmssgpopup.Attributes.Add("class", "alert alert-danger");
            }
        }
        else if (ddlcondition.SelectedValue == "05")
        {
            if (ddlsamper.SelectedValue != "01" && ddltlper.SelectedValue != "01" && ddlairmana.SelectedValue != "01" && ddlairper.SelectedValue != "01")
            {
                Int32 UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='ENA', ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Add by " + hfFeName.Value + ". Met With Guard. He Said Entry Not Allowed Without Appointment',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1',IsCall='" + ddlsamper.SelectedItem.Text.Substring(0, 1) + "-" + ddltlper.SelectedItem.Text.Substring(0, 1) + "-" + ddlairper.SelectedItem.Text.Substring(0, 1) + "-" + ddlairmana.SelectedItem.Text.Substring(0, 1) + "'", "[Eligible/Not Eligible]='Eligible' and UniqueID='" + hflocsrid.Value + "' and  (StatusByFE='' or StatusByFE is null) and Productname ='PRI-Fixed Line' and KeyDUP='false'");
                if (UpdateNewCompAddress != 0)
                {
                    SendMailNegativeCaseToAirtelOrOtherTeam();
                    cleartext();
                    var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    var script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
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
                divmssgpopup.InnerHtml = "All field fill mandatory.";
                divmssgpopup.Attributes.Add("class", "alert alert-danger");
            }
        }
        else
        {
            if (txtpersonmetmobno.Text != "")
            {
                GenerateOTP();
                send(txtpersonmetmobno.Text);
                pannegativesecondstep.Visible = true;
                pannegativefirststep.Visible = false;
            }
            else
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = "All field fill mandatory";
                divmssgpopup.Attributes.Add("Class", "alert alert-danger");
            }
        }
    }
    protected void lblresendotpnegative_Click(object sender, EventArgs e)
    {
        GenerateOTP();
        if (ddlnum.SelectedItem.Value != "OTP")
        {
            send(ddlnum.SelectedItem.Text);
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = "OTP resend successfully";
            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
        }
        else if (txtpersonmetmobno.Text != "")
        {
            send(txtpersonmetmobno.Text);
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = "OTP resend successfully";
            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
        }
        else
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = "OTP already send please check checkbox or select any number to send otp again";
            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void btnsubmitnegative_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlcondition.SelectedValue == "04")
            {
                DataTable DTOTPCheck = Lo.RetriveCodeAllExcelRecordWhereCondition("[Mobile OTP]='" + txtotpnegative.Text + "' and UniqueID='" + hflocsrid.Value + "' and [Eligible/Not Eligible]='Eligible' and (StatusByFE ='Pending' or StatusByFE is null or StatusByFE='')");
                if (DTOTPCheck.Rows.Count == 1)
                {
                    if (txtotpnegative.Text == DTOTPCheck.Rows[0]["Mobile OTP"].ToString())
                    {
                        string num;
                        if (ddlnum.SelectedItem.Value != "OTP")
                        {
                            num = ddlnum.SelectedItem.Text;
                        }
                        else
                        {
                            num = txtpersonmetmobno.Text;
                        }
                        Int32 UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='Already Disconnected', ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + "Visit Done At The Given Address " + hfFeName.Value + " Met With Mr./Ms. " + txtpersonmetname.Text + " He/She Told We Already Disconnected The All Airtel Corporate Connection',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1',PersonMetMobileNo='" + num + "',PersonMetEmail='" + txtpersonmetemailid.Text + "',PersonMetDesignation='" + txtpersonmetdesig.Text + "',PersonMetName='" + txtpersonmetname.Text + "',[IsOTPVerified]='Y'", "[Eligible/Not Eligible]='Eligible' and UniqueID='" + hflocsrid.Value + "' and  (StatusByFE='' or StatusByFE is null) and Productname ='PRI-Fixed Line' and KeyDUP='false'");
                        if (UpdateNewCompAddress != 0)
                        {
                            SendMailNegativeCaseToAirtelOrOtherTeam();
                            cleartext();
                            var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                            var script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
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
                        divmssgpopup.InnerHtml = "Invalid OTP";
                        divmssgpopup.Attributes.Add("class", "alert alert-danger");
                    }
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "OTP not generated or Status updated earlier.";
                    divmssgpopup.Attributes.Add("class", "alert alert-danger");
                }
            }
            else if (ddlcondition.SelectedValue == "06" || ddlcondition.SelectedValue == "07" || ddlcondition.SelectedValue == "08" || ddlcondition.SelectedValue == "09")
            {
                Int32 UpdateNewCompAddress;
                DataTable DTOTPCheck = Lo.RetriveCodeAllExcelRecordWhereCondition("[Mobile OTP]='" + txtotpnegative.Text + "' and UniqueID='" + hflocsrid.Value + "' and [Eligible/Not Eligible]='Eligible' and (StatusByFE ='Pending' or StatusByFE is null or StatusByFE='')");
                if (DTOTPCheck.Rows.Count == 1)
                {
                    if (txtotpnegative.Text == DTOTPCheck.Rows[0]["Mobile OTP"].ToString())
                    {
                        string num;
                        if (ddlnum.SelectedItem.Value != "OTP")
                        {
                            num = ddlnum.SelectedItem.Text;
                        }
                        else
                        {
                            num = txtpersonmetmobno.Text;
                        }
                        if (ddlcondition.SelectedValue == "06")
                        {
                            UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='NOT INTERESTED', ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Address by " + hfFeName.Value + " Met With Mr./Ms. " + txtpersonmetname.Text + " He/She Said I am Not Interested To Any Types Of Verification And reason Not Disclosed',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1',PersonMetMobileNo='" + num + "',PersonMetEmail='" + txtpersonmetemailid.Text + "',PersonMetDesignation='" + txtpersonmetdesig.Text + "',PersonMetName='" + txtpersonmetname.Text + "',[IsOTPVerified]='Y'", "[Eligible/Not Eligible]='Eligible' and UniqueID='" + hflocsrid.Value + "' and  (StatusByFE='' or StatusByFE is null) and Productname ='PRI-Fixed Line' and KeyDUP='false'");
                            if (UpdateNewCompAddress != 0)
                            {
                                SendMailNegativeCaseToAirtelOrOtherTeam();
                                cleartext();
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                                var script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                            }
                            else
                            {
                                divmssgpopup.Visible = true;
                                divmssgpopup.InnerHtml = "Status not update some error occure.";
                                divmssgpopup.Attributes.Add("class", "alert alert-danger");
                            }
                        }
                        else if (ddlcondition.SelectedValue == "07")
                        {
                            UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='Not Using', ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Address by " + hfFeName.Value + " Met With Mr./Ms:- " + txtpersonmetname.Text + " He/She Said We Are Not Using This Number',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1',PersonMetMobileNo='" + num + "',PersonMetEmail='" + txtpersonmetemailid.Text + "',PersonMetDesignation='" + txtpersonmetdesig.Text + "',PersonMetName='" + txtpersonmetname.Text + "',[IsOTPVerified]='Y'", "[Eligible/Not Eligible]='Eligible' and UniqueID='" + hflocsrid.Value + "' and  (StatusByFE='' or StatusByFE is null) and Productname ='PRI-Fixed Line' and KeyDUP='false'");
                            if (UpdateNewCompAddress != 0)
                            {
                                SendMailNegativeCaseToAirtelOrOtherTeam();
                                cleartext();
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                                var script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                            }
                            else
                            {
                                divmssgpopup.Visible = true;
                                divmssgpopup.InnerHtml = "Status not update some error occure.";
                                divmssgpopup.Attributes.Add("class", "alert alert-danger");
                            }
                        }
                        else if (ddlcondition.SelectedValue == "08")
                        {
                            UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code=' Already Done', ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + "Visit Done At The Given Add by " + hfFeName.Value + " Met With Mr./Ms. " + txtpersonmetname.Text + "  He/She Told Verification Is Already Done ',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1',PersonMetMobileNo='" + num + "',PersonMetEmail='" + txtpersonmetemailid.Text + "',PersonMetDesignation='" + txtpersonmetdesig.Text + "',PersonMetName='" + txtpersonmetname.Text + "',[IsOTPVerified]='Y'", "[Eligible/Not Eligible]='Eligible' and UniqueID='" + hflocsrid.Value + "' and  (StatusByFE='' or StatusByFE is null) and Productname ='PRI-Fixed Line' and KeyDUP='false'");
                            if (UpdateNewCompAddress != 0)
                            {
                                SendMailNegativeCaseToAirtelOrOtherTeam();
                                cleartext();
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                                var script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                            }
                            else
                            {
                                divmssgpopup.Visible = true;
                                divmssgpopup.InnerHtml = "Status not update some error occure.";
                                divmssgpopup.Attributes.Add("class", "alert alert-danger");
                            }
                        }
                        else if (ddlcondition.SelectedValue == "09")
                        {
                            UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='(NI) RM APPROVEL', ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + "Visit Done At The Given Address " + hfFeName.Value + " Met With Mr./Ms. " + txtpersonmetname.Text + " He./She Said First Co-Ordinate With Our Relashionship Manager He will Said This Procces Is Requered Then I Will Provide You All Documents You Have Required',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1',PersonMetMobileNo='" + num + "',PersonMetEmail='" + txtpersonmetemailid.Text + "',PersonMetDesignation='" + txtpersonmetdesig.Text + "',PersonMetName='" + txtpersonmetname.Text + "',[IsOTPVerified]='Y'", "[Eligible/Not Eligible]='Eligible' and UniqueID='" + hflocsrid.Value + "' and  (StatusByFE='' or StatusByFE is null) and Productname ='PRI-Fixed Line' and KeyDUP='false'");
                            if (UpdateNewCompAddress != 0)
                            {
                                SendMailNegativeCaseToAirtelOrOtherTeam();
                                cleartext();
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                                var script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
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
                    else
                    {
                        divmssgpopup.Visible = true;
                        divmssgpopup.InnerHtml = "Invalid OTP";
                        divmssgpopup.Attributes.Add("class", "alert alert-danger");
                    }
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "OTP not generated.";
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
    #region Revisit Caee
    protected void btngetreviitotp_Click(object sender, EventArgs e)
    {
        if (ddlcondition.SelectedValue == "10")
        {
            if (txtrevisitpersonmobileno.Text != "")
            {
                GenerateOTP();
                send(txtrevisitpersonmobileno.Text);
                revisittimedate.Visible = false;
                DivOTPRevisit.Visible = true;
            }
            else
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = "All field fill mandatory";
                divmssgpopup.Attributes.Add("Class", "alert alert-danger");
            }
        }
    }
    protected void lbresendotprevisit_Click(object sender, EventArgs e)
    {
        GenerateOTP();
        if (ddlnum.SelectedItem.Value != "OTP")
        {
            send(txtrevisitpersonmobileno.Text);
        }
        else
        {
            send(ddlnum.SelectedItem.Text);
        }
        divmssgpopup.Visible = true;
        divmssgpopup.InnerHtml = "OTP resend successfully";
        divmssgpopup.Attributes.Add("Class", "alert alert-danger");
    }
    protected void btnsubmitrevisitstatus_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtrevisittimeanddate.Text != "" && txtrevisitpersonmetname.Text != "" && txtrevisitpersonmobileno.Text != "")
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
                            Int32 UpdateRevisitStatusByFE = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='CUSTOMER NOT AVAILABLE', [Revisit PerSon Name]='" + txtrevisitpersonmetname.Text + "',[Revisit Date Time]='" + DatetimeRevis + "',[Revisit Time]='" + DatetimeRevisTime + "',PersonMetMobileNo='" + txtrevisitpersonmobileno.Text + "',PersonMetEmail='" + txtrevisitpersonmetemail.Text + "',PersonMetDesignation='" + txtrevisitpersonmetdesig.Text + "',[IsOTPVerified]='Y',ReasonOfStatus='Revisit," + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Address by " + hfFeName.Value + " Met With Mr. " + txtrevisitpersonmetname.Text + " He Said Aouthrised Person Not Avavilable At This Time Please Again Visit On ',StatusByFE='Pending',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Pending'", "[Eligible/Not Eligible]='Eligible' and UniqueID='" + hflocsrid.Value + "' and  (StatusByFE='' or StatusByFE is null) and Productname ='PRI-Fixed Line' and KeyDUP='false'");
                            if (UpdateRevisitStatusByFE != 0)
                            {
                                SendMailClientPending();
                                DivOTPRevisit.Visible = false;
                                revisittimedate.Visible = false;
                                cleartext();
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit rescheduled successfully.");
                                var script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                            }
                            else
                            {
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit rescheduled status not updated some error occure.");
                                var script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
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
    #endregion
    #region Success Submit
    protected void btnsuccessotpstep1_Click(object sender, EventArgs e)
    {
        if (txtpersonemetmobilenosuccess.Text != "")
        {
            GenerateOTP();
            if (ddlnump.SelectedItem.Value != "OTP")
            {
                send(ddlnump.SelectedItem.Text);
            }
            else
            {
                send(txtpersonemetmobilenosuccess.Text);
            }
            divsuccessfirst.Visible = false;
            divsuccessoptorsubmit.Visible = true;
        }
        else
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = "All field fill mandatory";
            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void lblresendotppositive_Click(object sender, EventArgs e)
    {
        GenerateOTP();
        if (ddlnump.SelectedItem.Value != "OTP")
        {
            send(txtpersonemetmobilenosuccess.Text);
        }
        else
        {
            send(ddlnump.SelectedItem.Text);
        }
        divmssgpopup.Visible = true;
        divmssgpopup.InnerHtml = "OTP resend successfully";
        divmssgpopup.Attributes.Add("Class", "alert alert-danger");
    }
    protected void btnsuccsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtpersonemetmobilenosuccess.Text != "")
            {
                if (txtoptsuces.Text != "")
                {
                    DataTable DTOTPCheck = Lo.RetriveCodeAllExcelRecordWhereCondition("[Mobile OTP]='" + txtoptsuces.Text + "' and UniqueID='" + hflocsrid.Value + "' and [Eligible/Not Eligible]='Eligible' and (StatusByFE ='Pending' or StatusByFE is null or StatusByFE='')");
                    if (DTOTPCheck.Rows.Count == 1)
                    {
                        if (txtoptsuces.Text == DTOTPCheck.Rows[0]["Mobile OTP"].ToString())
                        {
                            string nums;
                            if (ddlnump.SelectedItem.Value != "OTP")
                            {
                                nums = ddlnump.SelectedItem.Text;
                            }
                            else
                            {
                                nums = txtpersonemetmobilenosuccess.Text;
                            }
                            Int32 UpdateCaseID = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='OK', [Positive/Negative]='Positive',[ReasonOfStatus]='Verification Done',[RemarksOfStatus]='" + DateTime.Now.ToString("dd-MMM") + "MET PERSON:-" + txtpersonmetnamesuccess.Text + " RELATION:-" + txtsuccpersonmetdesig.Text + "  CONFIRMATION:- DONE, REMARKS:-MET PERSON CONFIRMATION DONE ',[StatusByFE]='Positive',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',PersonMetName='" + txtpersonmetnamesuccess.Text + "',PersonMetEmail='" + txtpersonmetemailidsuccess.Text + "',PersonMetDesignation='" + txtsuccpersonmetdesig.Text + "',PersonMetMobileNo='" + txtpersonemetmobilenosuccess.Text + "'", "[Eligible/Not Eligible]='Eligible' and UniqueID='" + hflocsrid.Value + "' and  (StatusByFE='' or StatusByFE is null) and Productname ='PRI-Fixed Line' and KeyDUP='false'");
                            if (UpdateCaseID != 0)
                            {
                                SendMailClientSuccessVerification(hfNameCustomer.Value, hflogicalcircuitid.Value, hfFeName.Value);
                                cleartext();
                                divmssgpopup.Visible = true;
                                divmssgpopup.InnerHtml = "SuccessFully Update Status";
                                divmssgpopup.Attributes.Add("class", "alert alert-success");
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("SuccessFully Update Status");
                                var script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                            }
                            else
                            {
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit rescheduled status not updated some error occure.");
                                var script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
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
    #endregion
    #region SendMailCode
    protected void SendMailClientSuccessVerification(string PartyorCust, string Logicalcircuitid, string FEName)
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/MoPositivePending.htm")))
        {
            body = reader.ReadToEnd();
        }
        if (PartyorCust == "")
        {
            PartyorCust = hfPartyName.Value;
        }
        body = body.Replace("{PartyName}", PartyorCust);
        body = body.Replace("{Logical}", Logicalcircuitid);
        body = body.Replace("{FEName}", FEName);
        body = body.Replace("{Message}", " This is for your information that verification as per DoT regulations for PRI-Fixed Line has been completed successfully.");
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
        DataTable DTRetriveClient = Lo.RetriveCodeWithContidion("select [Subs Del No],[Party Name],ProductName,[From Site],StatusUpdateDate,[Positive/Negative],ReasonOfStatus as Code,ProdTypeName as [Type of Inspection],RM_Name,RM_EMAIL from tbl_trn_RawData where UniqueID='" + hflocsrid.Value + "' and StatusByFE='Negative' and KeyDup='false'");
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
            //  s.CreateMail("verification@rcpl.in", "mohdwali@globalitpoint.com", "Airtel " + DTRetriveClient.Rows[0]["ProductName"].ToString() + " Verification  " + DTRetriveClient.Rows[0]["Type of Inspection"].ToString() + " - " + DTRetriveClient.Rows[0]["Party Name"].ToString() + "", body);

            s.CreateMail("verification@rcpl.in", "verification@rcpl.in", "Airtel " + DTRetriveClient.Rows[0]["ProductName"].ToString() + " Verification  " + DTRetriveClient.Rows[0]["Type of Inspection"].ToString() + " - " + DTRetriveClient.Rows[0]["Party Name"].ToString() + "", body);
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
        body = body.Replace("{Message}", "This for your information that verification as per DoT regulations for PRI-Fixed Line has been resechduled " + txtrevisittimeanddate.Text + ". <br /><br />Thanks for choosing Airtel as your preferred service partner. <br /><br />assuring you our best services always. <br /><br />");
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
    #endregion
    #region OTP Send By DDL"
    protected void sendotp_Click(object sender, EventArgs e)
    {
        if (ddlnum.SelectedItem.Value != "otp")
        {
            GenerateOTP();
            send(ddlnum.SelectedItem.Text);
        }
    }
    protected void chkotpshowddl_CheckedChanged(object sender, EventArgs e)
    {
        if (chkotpshowddl.Checked == true)
        {
            ddlnum.Enabled = true;
            sendotp.Enabled = true;
            lblresendotpnegative.Enabled = true;
        }
        else
        {
            ddlnum.Enabled = false;
            sendotp.Enabled = false;
            lblresendotpnegative.Enabled = false;
        }
    }
    protected void sendotps_Click(object sender, EventArgs e)
    {
        if (ddlnump.SelectedItem.Value != "OTP")
        {
            GenerateOTP();
            send(ddlnump.SelectedItem.Text);
        }
    }
    protected void chkboxposi_CheckedChanged(object sender, EventArgs e)
    {
        if (chkboxposi.Checked == true)
        {
            ddlnump.Enabled = true;
            sendotps.Enabled = true;
            lblresendotppositive.Enabled = true;
        }
        else
        {
            ddlnump.Enabled = false;
            sendotps.Enabled = false;
            lblresendotppositive.Enabled = false;
        }
    }
    #endregion
    #region Dropdown or Search"
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        string sortExpression = null;
        if (txtsearch.Text != "")
        {
            DataTable DtAssignJob = new DataTable();
            DtAssignJob = Lo.RetriveCodeAllExcelRecordWhereCondition(" ([Subs Del No]='" + txtsearch.Text + "' or [Party Name] like '%" + txtsearch.Text + "%') and [FE Name]='" + hfFeName.Value + "' and IsClosed !='Y' and IsActive='Y' and [Eligible/Not Eligible]='Eligible' and (StatusByFE='' or StatusByFE is null or StatusByFE='Pending') and ProductName='PRI-Fixed Line' and KeyDup='false' order by [Party Name] asc");
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
                diverror.InnerHtml = "No job assign ";
                diverror.Attributes.Add("class", "alert alert-danger");
                txtsearch.Text = "";
            }
        }
    }
    #endregion
    protected void btnsubremarksaddressnotfoundorcompclose_Click(object sender, EventArgs e)
    {
        if (ddlcondition.SelectedItem.Value == "03" || ddlcondition.SelectedItem.Value == "12" && txtremarksaddressnotfoundorcompclose.Text != "")
        {
            if (ddlcondition.SelectedItem.Value == "03")
            {
                Int32 UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='COMPANY CLOSED', ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Address by " + hfFeName.Value + ". He/She Said Company Are Parmanetly Closed To Here',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1'", "[Eligible/Not Eligible]='Eligible' and [UniqueID]='" + hflocsrid.Value + "'  and (StatusByFE='' or StatusByFE is null) and Productname ='PRI-Fixed Line' and KeyDUP='false'");
                if (UpdateNewCompAddress != 0)
                {
                    SendMailNegativeCaseToAirtelOrOtherTeam();
                    cleartext();
                    var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    var script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "Status not update some error occure.";
                    divmssgpopup.Attributes.Add("class", "alert alert-danger");
                }
            }
            else if (ddlcondition.SelectedItem.Value == "12")
            {
                Int32 UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='Address Not Found', ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + " MET PERSON NAME:-NO MET PERSON, RELATION:-NO, REMARKS:-Address Not Found Because,CUSTOMER NOT MET',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1'", "[Eligible/Not Eligible]='Eligible' and UniqueID='" + hflocsrid.Value + "' and (StatusByFE='' or StatusByFE is null) and Productname ='PRI-Fixed Line' and KeyDUP='false'");
                if (UpdateNewCompAddress != 0)
                {
                    SendMailNegativeCaseToAirtelOrOtherTeam();
                    cleartext();
                    var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    var script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
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
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please enter reamrks.')", true);
        }
    }
}