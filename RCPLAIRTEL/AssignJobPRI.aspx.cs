using BuisnessLayer;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AssignJobPRI : System.Web.UI.Page
{
    #region variable
    private Logic Lo = new Logic();
    private Common Co = new Common();
    private string Email;
    private string HFRevisitDate;
    private string hfPreVisit;
    #endregion
    #region Other Basic Function
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfFeName.Value = Session["Name"].ToString();
            BindAssignJobCompany();
        }
    }
    protected void BindAssignJobCompany()
    {
        DataTable DtAssignJob = new DataTable();
        DtAssignJob = Lo.RetriveCodePRIFE(0, "PRI-Fixed Line", "", DateTime.Now, hfFeName.Value, "", "", "", "", "BindGridFENamePRI");
        if (DtAssignJob.Rows.Count > 0)
        {
            gvexcel.DataSource = DtAssignJob;
            gvexcel.DataBind();
            totalcount.Text = "Total Assign job- " + DtAssignJob.Rows.Count.ToString();
            gvexcel.Visible = true;
        }
        else
        {
            totalcount.Text = "Total Assign job- " + DtAssignJob.Rows.Count.ToString();
            gvexcel.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No job assign')", true);
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
        hfPreVisit = "";
        HFRevisitDate = "";
    }
    #endregion
    #region Gridview or DataList Code or Commande Code"
    protected void gvexcel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViDe")
            {
                DataTable DtSearchView = Lo.RetriveCodePRIFE(0, "PRI-Fixed Line", "", DateTime.Now, e.CommandArgument.ToString(), "", "", "", "", "ViewDetail");
                if (DtSearchView.Rows.Count > 0)
                {
                    dluploadexcelrecord.DataSource = DtSearchView;
                    dluploadexcelrecord.DataBind();
                    dluploadexcelrecord.Visible = true;
                    divshow1.Visible = true;
                    div2.Visible = false;
                    panmoallpanel.Visible = false;
                    cleartext();
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No record found.')", true);
                }
            }
            else if (e.CommandName == "UpSt")
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
                    e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.Visible = true;
                    e.Row.ForeColor = System.Drawing.Color.White;
                }
            }
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
    private void send(string _mobileNo, string email, string type)
    {
        try
        {
            if (type == "Mobile")
            {
                string requestUristring = string.Format("http://smsgateway.spicedigital.in/MessagingGateway//MessagePush?username=DefexpoT&password=Def@1234&messageType=text&mobile={0}&senderId=myrcpl&message=" + "Dear Airtel Customer, Your Airtel verification OTP for the same is '" + hfotp.Value + "' Thank You.", _mobileNo);
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
            int UpdateRevisitStatusByFE = Lo.UpdateCodeFEAssignPRI(Convert.ToInt64(hflocsrid.Value), "", "", Convert.ToDateTime(DateTime.Now), hfotp.Value, "", "", "", "", "", "", "", "", "", "Update6_1");
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
    #region ddlupdatekey
    protected void btnsubremarksaddressnotfoundorcompclose_Click(object sender, EventArgs e)
    {
        if (ddlcondition.SelectedItem.Value == "03" || ddlcondition.SelectedItem.Value == "12" && txtremarksaddressnotfoundorcompclose.Text != "")
        {
            if (ddlcondition.SelectedItem.Value == "03")
            {
                int UpdateNewCompAddress = Lo.UpdateCodeFEAssignPRI(Convert.ToInt64(hflocsrid.Value), "", "", DateTime.Now, ddlcondition.SelectedItem.Text, hfFeName.Value, "", "", "", "", "", "", "", "", "Update1");
                if (UpdateNewCompAddress != 0)
                {
                    SendMailNegativeCaseToAirtelOrOtherTeam();
                    cleartext();
                    string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    string script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
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
                int UpdateNewCompAddress = Lo.UpdateCodeFEAssignPRI(Convert.ToInt64(hflocsrid.Value), "", "", DateTime.Now, ddlcondition.SelectedItem.Text, "", "", "", "", "", "", "", "", "", "Update1_1");
                if (UpdateNewCompAddress != 0)
                {
                    SendMailNegativeCaseToAirtelOrOtherTeam();
                    cleartext();
                    string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    string script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
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
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please enter reamrks.')", true);
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
                int UpdateNewCompAddress = Lo.UpdateCodeFEAssignPRI(Convert.ToInt64(hflocsrid.Value), "", "", DateTime.Now, txtpersonmetname.Text, txtpersonmetemailid.Text, txtpersonmetdesig.Text, txtnewaddress.Text, ddlcondition.SelectedItem.Text, hfFeName.Value, txtpersonmetname.Text, txtpersonmetmobno.Text, "", "", "Update2");
                if (UpdateNewCompAddress != 0)
                {
                    SendMailNegativeCaseToAirtelOrOtherTeam();
                    cleartext();
                    string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    string script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
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
                divmssgpopup.InnerHtml = "Please fill new address";
                divmssgpopup.Attributes.Add("class", "alert alert-danger");
            }
        }
        else if (ddlcondition.SelectedValue == "05")
        {
            if (ddlsamper.SelectedValue != "01" && ddltlper.SelectedValue != "01" && ddlairmana.SelectedValue != "01" && ddlairper.SelectedValue != "01")
            {
                int UpdateNewCompAddress = Lo.UpdateCodeFEAssignPRI(Convert.ToInt64(hflocsrid.Value), "", "", DateTime.Now, ddlcondition.SelectedItem.Text, hfFeName.Value, ddlsamper.SelectedItem.Text.Substring(0, 1) + "-" + ddltlper.SelectedItem.Text.Substring(0, 1) + "-" + ddlairper.SelectedItem.Text.Substring(0, 1) + "-" + ddlairmana.SelectedItem.Text.Substring(0, 1), "", "", "", "", "", "", "", "Update3");
                if (UpdateNewCompAddress != 0)
                {
                    SendMailNegativeCaseToAirtelOrOtherTeam();
                    cleartext();
                    string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    string script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
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
                divmssgpopup.InnerHtml = "All field fill mandatory.";
                divmssgpopup.Attributes.Add("class", "alert alert-danger");
            }
        }
        else
        {
            if (txtpersonmetmobno.Text != "")
            {
                GenerateOTP();
                send(txtpersonmetmobno.Text, "", "Mobile");
               // send("", "","email");
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
            send(ddlnum.SelectedItem.Text, "", "Mobile");
          //  send("", "", "email");
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = "OTP resend successfully";
            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
        }
        else if (txtpersonmetmobno.Text != "")
        {
             send(txtpersonmetmobno.Text,"","Mobile");
           // send("", "", "email");
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
                DataTable DTOTPCheck = Lo.RetriveCodePRIFE(Convert.ToInt64(hflocsrid.Value), "", "", DateTime.Now, txtotpnegative.Text, "", "", "", "", "GetOTP");
                if (DTOTPCheck.Rows.Count == 1)
                {
                    if (txtotpnegative.Text == DTOTPCheck.Rows[0]["Mobile_OTP"].ToString())
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
                        int UpdateNewCompAddress = Lo.UpdateCodeFEAssignPRI(Convert.ToInt64(hflocsrid.Value), "", "", DateTime.Now, txtpersonmetname.Text, txtpersonmetemailid.Text, txtpersonmetdesig.Text, ddlcondition.SelectedItem.Text, hfFeName.Value, txtpersonmetname.Text, num, "", "", "", "Update4");
                        if (UpdateNewCompAddress != 0)
                        {
                            SendMailNegativeCaseToAirtelOrOtherTeam();
                            cleartext();
                            string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                            string script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
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
                int UpdateNewCompAddress;
                DataTable DTOTPCheck = Lo.RetriveCodePRIFE(Convert.ToInt64(hflocsrid.Value), "", "", DateTime.Now, txtotpnegative.Text, "", "", "", "", "GetOTP");
                if (DTOTPCheck.Rows.Count == 1)
                {
                    if (txtotpnegative.Text == DTOTPCheck.Rows[0]["Mobile_OTP"].ToString())
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
                            UpdateNewCompAddress = Lo.UpdateCodeFEAssignPRI(Convert.ToInt64(hflocsrid.Value), "", "", DateTime.Now, txtpersonmetname.Text, txtpersonmetemailid.Text, txtpersonmetdesig.Text, ddlcondition.SelectedItem.Text, hfFeName.Value, txtpersonmetname.Text, num, "", "", "", "Update4_1");
                            if (UpdateNewCompAddress != 0)
                            {
                                SendMailNegativeCaseToAirtelOrOtherTeam();
                                cleartext();
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                                string script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
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
                            UpdateNewCompAddress = Lo.UpdateCodeFEAssignPRI(Convert.ToInt64(hflocsrid.Value), "", "", DateTime.Now, ddlcondition.SelectedItem.Text, hfFeName.Value, txtpersonmetname.Text, num, txtpersonmetemailid.Text, txtpersonmetdesig.Text, txtpersonmetname.Text, "", "", "", "Update4_2");
                            if (UpdateNewCompAddress != 0)
                            {
                                SendMailNegativeCaseToAirtelOrOtherTeam();
                                cleartext();
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                                string script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
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
                            UpdateNewCompAddress = Lo.UpdateCodeFEAssignPRI(Convert.ToInt64(hflocsrid.Value), "", "", DateTime.Now, ddlcondition.SelectedItem.Text, hfFeName.Value, txtpersonmetname.Text, num, txtpersonmetemailid.Text, txtpersonmetdesig.Text, txtpersonmetname.Text, "", "", "", "Update4_3");
                            if (UpdateNewCompAddress != 0)
                            {
                                SendMailNegativeCaseToAirtelOrOtherTeam();
                                cleartext();
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                                string script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
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
                            UpdateNewCompAddress = Lo.UpdateCodeFEAssignPRI(Convert.ToInt64(hflocsrid.Value), "", "", DateTime.Now, ddlcondition.SelectedItem.Text, hfFeName.Value, txtpersonmetname.Text, num, txtpersonmetemailid.Text, txtpersonmetdesig.Text, txtpersonmetname.Text, "", "", "", "Update4_4");
                            if (UpdateNewCompAddress != 0)
                            {
                                SendMailNegativeCaseToAirtelOrOtherTeam();
                                cleartext();
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                                string script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
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
               // send("", "", "email");
                 send(txtrevisitpersonmobileno.Text,"","Mobile");
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
           // send("", "", "email");
            send(txtrevisitpersonmobileno.Text, "", "Mobile");
        }
        else
        {
           // send("", "", "email");
             send(ddlnum.SelectedItem.Text,"","Mobile");
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
                    DataTable DTOTPCheck = Lo.RetriveCodePRIFE(Convert.ToInt64(hflocsrid.Value), "", "", DateTime.Now, txtotprevisitcustmer.Text, "", "", "", "", "GetOTP");
                    if (DTOTPCheck.Rows.Count == 1)
                    {
                        if (txtotprevisitcustmer.Text == DTOTPCheck.Rows[0]["Mobile_OTP"].ToString())
                        {
                            DateTime Datetimerevisit = Convert.ToDateTime(txtrevisittimeanddate.Text);
                            string DatetimeRevis = Datetimerevisit.ToString("dd/MMM/yyyy");
                            DateTime DatetimerevisitTIme = Convert.ToDateTime(txttimerevisit.Text);
                            string DatetimeRevisTime = DatetimerevisitTIme.ToString("HH:mm:ss");
                            int UpdateRevisitStatusByFE = Lo.UpdateCodeFEAssignPRI(Convert.ToInt64(hflocsrid.Value), "", "", Convert.ToDateTime(DatetimeRevis), txtrevisitpersonmetname.Text, DatetimeRevisTime, txtrevisitpersonmobileno.Text, txtrevisitpersonmetemail.Text, txtrevisitpersonmetdesig.Text, ddlcondition.SelectedItem.Text, hfFeName.Value, txtrevisitpersonmetname.Text, "", "", "Update5");
                            if (UpdateRevisitStatusByFE != 0)
                            {
                                SendMailClientPending();
                                DivOTPRevisit.Visible = false;
                                revisittimedate.Visible = false;
                                cleartext();
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit rescheduled successfully.");
                                string script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                            }
                            else
                            {
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit rescheduled status not updated some error occure.");
                                string script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
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
    #endregion
    #region Success Submit
    protected void btnsuccessotpstep1_Click(object sender, EventArgs e)
    {
        if (txtpersonemetmobilenosuccess.Text != "")
        {
            GenerateOTP();
            if (ddlnump.SelectedItem.Value != "OTP")
            {
               // send("", "", "email");
                send(ddlnump.SelectedItem.Text, "", "Mobile");
            }
            else
            {
               // send("", "", "email");
                   send(txtpersonemetmobilenosuccess.Text,"","Mobile");
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
           // send("", "", "email");
             send(txtpersonemetmobilenosuccess.Text,"","Mobile");
        }
        else
        {
           // send("", "", "email");
             send(ddlnump.SelectedItem.Text,"","Mobile");
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
                    DataTable DTOTPCheck = Lo.RetriveCodePRIFE(Convert.ToInt64(hflocsrid.Value), "", "", DateTime.Now, txtoptsuces.Text, "", "", "", "", "GetOTP");
                    if (DTOTPCheck.Rows.Count == 1)
                    {
                        if (txtoptsuces.Text == DTOTPCheck.Rows[0]["Mobile_OTP"].ToString())
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
                            int UpdateCaseID = Lo.UpdateCodeFEAssignPRI(Convert.ToInt64(hflocsrid.Value), "", "", DateTime.Now, txtpersonmetnamesuccess.Text, txtsuccpersonmetdesig.Text, txtpersonmetnamesuccess.Text, txtpersonmetemailidsuccess.Text, txtsuccpersonmetdesig.Text, txtpersonemetmobilenosuccess.Text, "", "", "", "", "Update6");
                            if (UpdateCaseID != 0)
                            {
                                SendMailClientSuccessVerification(hfNameCustomer.Value, hflogicalcircuitid.Value, hfFeName.Value);
                                cleartext();
                                divmssgpopup.Visible = true;
                                divmssgpopup.InnerHtml = "SuccessFully Update Status";
                                divmssgpopup.Attributes.Add("class", "alert alert-success");
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("SuccessFully Update Status");
                                string script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                            }
                            else
                            {
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit rescheduled status not updated some error occure.");
                                string script = string.Format("alert({0});window.location ='Fe-JobPRI';", message);
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
        DataTable DTRetriveClient = Lo.RetriveCodePRIFE(Convert.ToInt64(hflocsrid.Value), "", "", DateTime.Now, "", "", "", "", "", "GetEmail");
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
            // s.CreateMail("verification@rcpl.in", "mohdwali@globalitpoint.com", "Airtel " + DTRetriveClient.Rows[0]["ProductName"].ToString() + " Verification  " + DTRetriveClient.Rows[0]["Type of Inspection"].ToString() + " - " + DTRetriveClient.Rows[0]["Party Name"].ToString() + "", body);
            s.CreateMail("verification@rcpl.in", "verification@rcpl.in", "Airtel " + DTRetriveClient.Rows[0]["ProductName"].ToString() + " Verification  " + DTRetriveClient.Rows[0]["TypeofInspection"].ToString() + " - " + DTRetriveClient.Rows[0]["PartyName"].ToString() + "", body);
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
        body = body.Replace("{Message}", "This for your information that verification as per DoT regulations for PRI-Fixed Line has been resechduled " + txtrevisittimeanddate.Text + ". <br /><br />Thanks for choosing Airtel as your preferred service partner. <br /><br />assuring you our best services always. <br /><br />");
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
    #region OTP Send By DDL"
    protected void sendotp_Click(object sender, EventArgs e)
    {
        if (ddlnum.SelectedItem.Value != "otp")
        {
            GenerateOTP();
           // send("", "", "email");
            send(ddlnum.SelectedItem.Text, "", "Mobile");
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
           // send("", "", "email");
            send(ddlnump.SelectedItem.Text,"","Mobile");
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
}