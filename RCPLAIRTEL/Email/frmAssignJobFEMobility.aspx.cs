using BuisnessLayer;
using ClosedXML.Excel;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmAssignJobFEMobility : System.Web.UI.Page
{
    #region variable
    private Logic Lo = new Logic();
    private Common Co = new Common();
    private string Email;
    private string HFRevisitDate;
    private string hfPreVisit;
    private PagedDataSource pgsource = new PagedDataSource();
    private int firstindex, lastindex;
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
        DataTable DtFetchcompgropby = new DataTable();
        if (ddlprodbycat.SelectedItem.Value == "0")
        {
            DtFetchcompgropby = Lo.RetriveCodeMOFE(0, "MO", "", DateTime.Now, hfFeName.Value, "", "", "", "", "BindGridOnlyMO");
        }
        else
        {
            DtFetchcompgropby = Lo.RetriveCodeMOFE(0, "MO", ddlprodbycat.SelectedItem.Text, DateTime.Now, hfFeName.Value, "", "", "", "", "BindGridOnlyMO");
        }
        if (DtFetchcompgropby.Rows.Count > 0)
        {
            dlcomp.DataSource = DtFetchcompgropby;
            dlcomp.DataBind();
            dlcomp.Visible = true;
            totalcount.InnerText = "Total company for audit :- " + DtFetchcompgropby.Rows.Count.ToString();
        }
        else
        {
            dlcomp.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No job assign')", true);
            totalcount.InnerText = "Toal company for audit :- 0";
        }
    }
    protected void cleartext()
    {
        txtactivenuber.Text = "";
        txtdeactivenumber.Text = "";
        txtnewaddress.Text = "";
        txtnewnumber.Text = "";
        txtoptsuces.Text = "";
        txtotpnegative.Text = "";
        txtotprevisitcustmer.Text = "";
        txtpersonemetmobilenosuccess.Text = "";
        txtpersonmetdesig.Text = "";
        txtpersonmetemailid.Text = "";
        txtpersonmetemailidsuccess.Text = "";
        txtpersonmetmobno.Text = "";
        txtpersonmetname.Text = "";
        txtpersonmetnamesuccess.Text = "";
        txtremarkcust.Text = "";
        txtrevisitpersonmetdesig.Text = "";
        txtrevisitpersonmetemail.Text = "";
        txtrevisitpersonmetname.Text = "";
        txtrevisitpersonmobileno.Text = "";
        txtrevisittimeanddate.Text = "";
        txtsuccpersonmetdesig.Text = "";
        txttimerevisit.Text = "";
        ddlairmana.SelectedValue = "01";
        ddlairper.SelectedValue = "01";
        ddlcondition.SelectedIndex = 0;
        ddlnum.SelectedIndex = 0;
        ddlnump.SelectedIndex = 0;
        ddlprodbycat.SelectedIndex = 0;
        ddlsamper.SelectedValue = "01";
        ddltlper.SelectedValue = "01";
        hfAllocationDate.Value = "";
        hfbilledextid1.Value = "";
        hfcompname.Value = "";
        hfcustomerorpartyname.Value = "";
        hfEmailCustomer.Value = "";
        // hfFeName.Value = "";
        hflocsrid.Value = "";
        hfotp.Value = "";
        hfPreVisit = "";
        HFRevisitDate = "";
    }
    #endregion
    #region Gridview or DataList Code or Commande Code"
    protected void gvexcel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewDetail")
            {
                DataTable DtSearchView = Lo.RetriveCodeMOFE(Convert.ToInt64(e.CommandArgument.ToString()), "", "", DateTime.Now, "", "", "", "", "", "ViewDetail");
                if (DtSearchView.Rows.Count > 0)
                {
                    dluploadexcelrecord.DataSource = DtSearchView;
                    dluploadexcelrecord.DataBind();
                    dluploadexcelrecord.Visible = true;
                    divshow1.Visible = true;
                    div2.Visible = false;
                    panviewnum.Visible = false;
                    panmoallpanel.Visible = false;
                    cleartext();
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No record found.')", true);
                }
            }
            else if (e.CommandName == "UpdateStatus")
            {
                cleartext();
                GridViewRow gvexcel = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvexcel.RowIndex;
                string lblcompliancedate = (gvexcel.FindControl("lblcomp") as Label).Text;
                string stattusbyfe = (gvexcel.FindControl("hfstatusfe") as HiddenField).Value;

                if (stattusbyfe == "" || stattusbyfe == null || stattusbyfe == "Pending")
                {
                    hfcompname.Value = (gvexcel.FindControl("lblcompnamemo") as Label).Text;
                    Email = (gvexcel.FindControl("hfemail") as HiddenField).Value;
                    hfcustomerorpartyname.Value = (gvexcel.FindControl("Customer") as HiddenField).Value;
                    hfbilledextid1.Value = (gvexcel.FindControl("hfbilledextid") as HiddenField).Value;
                    hfAllocationDate.Value = (gvexcel.FindControl("hfGridAllcationDate") as HiddenField).Value;
                    hflocsrid.Value = e.CommandArgument.ToString();
                    hfEmailCustomer.Value = Email;
                    panmoallpanel.Visible = true;
                    divshow1.Visible = false;
                    div2.Visible = true;
                    panviewnum.Visible = false;
                    pansuccess.Visible = false;
                    pannegativecondition.Visible = false;
                    panrevisitcondition.Visible = false;
                    panaddressorcompclose.Visible = false;
                    ddlcondition.Enabled = true;
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup(" + ex.Message + ")", true);
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
    protected void gvnum_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void dlcomp_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GridView gvexcel = (GridView)e.Item.FindControl("gvexcel");
                Label hfid = (Label)e.Item.FindControl("lblcmpaname") as Label;
                DataTable DtAssignJob = Lo.RetriveCodeMOFE(0, "MO", "", DateTime.Now, hfFeName.Value, hfid.Text, "", "", "", "ViewDetailBindExcel");
                if (DtAssignJob.Rows.Count > 0)
                {
                    gvexcel.DataSource = DtAssignJob;
                    gvexcel.DataBind();
                    gvexcel.Visible = true;
                }
                else
                {
                    gvexcel.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
    }
    protected void dlcomp_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "Viewnum")
        {
            dlcomp.SelectedIndex = e.Item.ItemIndex;
            string lblacdate = ((Label)dlcomp.SelectedItem.FindControl("hfallocateddate")).Text;
            DataTable DtGetCompNum = Lo.RetriveCodeMOFE(0, "", "", DateTime.Now, e.CommandArgument.ToString(), lblacdate.ToString(), "", "", "", "MobNo");
            if (DtGetCompNum.Rows.Count > 0)
            {
                gvnum.DataSource = DtGetCompNum;
                gvnum.DataBind();
                divshow1.Visible = false;
                panviewnum.Visible = true;
                div2.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
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
            pannegativefirststep.Visible = true;
            divcustomershifted.Visible = true;
            ddlcondition.Enabled = false;
            btnnextnegative.Text = "Submit";
        }
        else if (ddlcondition.SelectedValue == "03")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            ddlcondition.Enabled = false;
            pannegativecondition.Visible = true;
            pannegativefirststep.Visible = false;
            pannegativesecondstep.Visible = false;
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
            DataTable DtFetchcompgropby = Lo.RetriveCodeMOFE(0, "", "", DateTime.Now, hfFeName.Value, hfcompname.Value, hfAllocationDate.Value, "", "", "CountNum");
            if (DtFetchcompgropby.Rows.Count > 0)
            {
                totalnumbercount.InnerText = "Total Number As Per Data " + DtFetchcompgropby.Rows[0]["Count"].ToString();
            }
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
            ddlcondition.Enabled = false;
            pannegativecondition.Visible = true;
            pannegativefirststep.Visible = false;
            pannegativesecondstep.Visible = false;
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
            int UpdateRevisitStatusByFE = Lo.UpdateCodeFEAssignMO(0, "", "", DateTime.Now, hfotp.Value, hfcompname.Value, hfAllocationDate.Value, "", "", "", "", "", "", "", "UpdateOTP"); if (UpdateRevisitStatusByFE != 0)
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
    #region NegativeCaseSubmit"
    protected void btnnextnegative_Click(object sender, EventArgs e)
    {
        if (ddlcondition.SelectedValue == "02")
        {
            if (txtnewaddress.Text != "")
            {
                int UpdateNewCompAddress = Lo.UpdateCodeFEAssignMO(0, "", "", DateTime.Now, txtnewaddress.Text, txtpersonmetmobno.Text, txtpersonmetname.Text, txtpersonmetemailid.Text, txtpersonmetdesig.Text,
                    ddlcondition.SelectedItem.Text, hfFeName.Value, txtpersonmetname.Text, hfcompname.Value, hfAllocationDate.Value, "Update11");
                if (UpdateNewCompAddress != 0)
                {
                    DataTable DtNegative = Lo.RetriveCodeMOFE(0, "", "", DateTime.Now, hfcompname.Value, hfAllocationDate.Value, "", "", "", "NegMail");
                    if (DtNegative.Rows.Count > 0)
                    {
                        ExportExcel(DtNegative);
                    }
                    divmssgpopup.Visible = true;
                    cleartext();
                    string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    string script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
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
                int UpdateNewCompAddress = Lo.UpdateCodeFEAssignMO(0, "", "", Convert.ToDateTime(hfAllocationDate.Value), ddlcondition.SelectedItem.Text, hfFeName.Value, ddlsamper.SelectedItem.Text.Substring(0, 1) + "-" + ddltlper.SelectedItem.Text.Substring(0, 1) + "-" + ddlairper.SelectedItem.Text.Substring(0, 1) + "-" + ddlairmana.SelectedItem.Text.Substring(0, 1), hfcompname.Value, hfAllocationDate.Value, "", "", "", "", "", "Update10");
                if (UpdateNewCompAddress != 0)
                {
                    DataTable DtNegative = Lo.RetriveCodeMOFE(0, "", "", DateTime.Now, hfcompname.Value, hfAllocationDate.Value, "", "", "", "NegMail");
                    if (DtNegative.Rows.Count > 0)
                    {
                        ExportExcel(DtNegative);
                    }
                    divmssgpopup.Visible = true;
                    cleartext();
                    string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    string script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
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
                // send("", "", "email");
                send(txtpersonmetmobno.Text, "", "Mobile");
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
            //send("", "", "email");
            send(txtpersonmetmobno.Text, "", "Mobile");
        }
        else
        {
            // send("", "", "email");
            send(ddlnum.SelectedItem.Text, "", "Mobile");
        }
        divmssgpopup.Visible = true;
        divmssgpopup.InnerHtml = "OTP resend successfully";
        divmssgpopup.Attributes.Add("Class", "alert alert-danger");
    }
    protected void btnsubmitnegative_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlcondition.SelectedValue == "04" || ddlcondition.SelectedValue == "05")
            {
                DataTable DTOTPCheck = Lo.RetriveCodeMOFE(0, "", "", DateTime.Now, txtotpnegative.Text, hflocsrid.Value, hfAllocationDate.Value, "", "", "GetMobotp");
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
                        int UpdateNewCompAddress = Lo.UpdateCodeFEAssignMO(0, "", "", DateTime.Now, ddlcondition.SelectedItem.Text, hfFeName.Value, txtpersonmetname.Text, num, txtpersonmetemailid.Text, txtpersonmetdesig.Text, txtpersonmetname.Text, hfcompname.Value, hfAllocationDate.Value, "", "Update9");
                        if (UpdateNewCompAddress != 0)
                        {
                            DataTable DtNegative = Lo.RetriveCodeMOFE(0, "", "", DateTime.Now, hfcompname.Value, hfAllocationDate.Value, "", "", "", "NegMail");
                            if (DtNegative.Rows.Count > 0)
                            {
                                ExportExcel(DtNegative);
                            }
                            divmssgpopup.Visible = true;
                            cleartext();
                            string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                            string script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
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
                DataTable DTOTPCheck = Lo.RetriveCodeMOFE(0, "", "", DateTime.Now, txtotpnegative.Text, hflocsrid.Value, hfAllocationDate.Value, "", "", "GetMobotp");
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
                            UpdateNewCompAddress = Lo.UpdateCodeFEAssignMO(0, "", "", DateTime.Now, ddlcondition.SelectedItem.Text, hfFeName.Value, txtpersonmetname.Text, num, txtpersonmetemailid.Text, txtpersonmetdesig.Text, txtpersonmetname.Text, hfcompname.Value, hfAllocationDate.Value, "", "Update8");
                            if (UpdateNewCompAddress != 0)
                            {
                                DataTable DtNegative = Lo.RetriveCodeMOFE(0, "", "", DateTime.Now, hfcompname.Value, hfAllocationDate.Value, "", "", "", "NegMail");
                                if (DtNegative.Rows.Count > 0)
                                {
                                    ExportExcel(DtNegative);
                                }
                                cleartext();
                                divmssgpopup.Visible = true;
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                                string script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
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
                            UpdateNewCompAddress = Lo.UpdateCodeFEAssignMO(0, "", "", DateTime.Now, ddlcondition.SelectedItem.Text, hfFeName.Value, txtpersonmetname.Text, num, txtpersonmetemailid.Text, txtpersonmetdesig.Text, txtpersonmetname.Text, hfcompname.Value, hfAllocationDate.Value, "", "Update7");
                            if (UpdateNewCompAddress != 0)
                            {
                                DataTable DtNegative = Lo.RetriveCodeMOFE(0, "", "", DateTime.Now, hfcompname.Value, hfAllocationDate.Value, "", "", "", "NegMail");
                                if (DtNegative.Rows.Count > 0)
                                {
                                    ExportExcel(DtNegative);
                                }
                                divmssgpopup.Visible = true;
                                cleartext();
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                                string script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
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
                            UpdateNewCompAddress = Lo.UpdateCodeFEAssignMO(0, "", "", DateTime.Now, ddlcondition.SelectedItem.Text, hfFeName.Value, txtpersonmetname.Text, num, txtpersonmetemailid.Text, txtpersonmetdesig.Text, txtpersonmetname.Text,
                                hfcompname.Value, hfAllocationDate.Value, "", "Update6");
                            if (UpdateNewCompAddress != 0)
                            {
                                DataTable DtNegative = Lo.RetriveCodeMOFE(0, "", "", DateTime.Now, hfcompname.Value, hfAllocationDate.Value, "", "", "", "NegMail");
                                if (DtNegative.Rows.Count > 0)
                                {
                                    ExportExcel(DtNegative);
                                }
                                divmssgpopup.Visible = true;
                                cleartext();
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                                string script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
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

                            UpdateNewCompAddress = Lo.UpdateCodeFEAssignMO(0, "", "", DateTime.Now, ddlcondition.SelectedItem.Text, hfFeName.Value, txtpersonmetname.Text, num, txtpersonmetemailid.Text, txtpersonmetdesig.Text,
                            txtpersonmetname.Text, hfcompname.Value, hfAllocationDate.Value, "", "Update5");
                            if (UpdateNewCompAddress != 0)
                            {
                                DataTable DtNegative = Lo.RetriveCodeMOFE(0, "", "", DateTime.Now, hfcompname.Value, hfAllocationDate.Value, "", "", "", "NegMail");
                                if (DtNegative.Rows.Count > 0)
                                {
                                    ExportExcel(DtNegative);
                                }
                                divmssgpopup.Visible = true;
                                cleartext();
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                                string script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
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
                    divmssgpopup.InnerHtml = "Otp row error / Case close some error in redirect to location.Please refresh page.";
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
    protected void btnsubremarksaddressnotfoundorcompclose_Click(object sender, EventArgs e)
    {
        if (ddlcondition.SelectedItem.Value == "03" || ddlcondition.SelectedItem.Value == "12" && txtremarksaddressnotfoundorcompclose.Text != "")
        {
            if (ddlcondition.SelectedItem.Value == "03")
            {

                int UpdateNewCompAddress = Lo.UpdateCodeFEAssignMO(0, "", "", DateTime.Now, ddlcondition.SelectedItem.Text, hfFeName.Value, hfcompname.Value, hfAllocationDate.Value, "", "", "", "", "", "", "Update4");
                if (UpdateNewCompAddress != 0)
                {
                    DataTable DtNegative = Lo.RetriveCodeMOFE(0, "", "", DateTime.Now, hfcompname.Value, hfAllocationDate.Value, "", "", "", "NegMail");
                    if (DtNegative.Rows.Count > 0)
                    {
                        ExportExcel(DtNegative);
                    }
                    divmssgpopup.Visible = true;
                    cleartext();
                    string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    string script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
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

                int UpdateNewCompAddress = Lo.UpdateCodeFEAssignMO(0, "", "", DateTime.Now, ddlcondition.SelectedItem.Text, hfcompname.Value, hfAllocationDate.Value, "", "", "", "", "", "", "", "Update3");
                if (UpdateNewCompAddress != 0)
                {
                    DataTable DtNegative = Lo.RetriveCodeMOFE(0, "", "", DateTime.Now, hfcompname.Value, hfAllocationDate.Value, "", "", "", "NegMail");
                    if (DtNegative.Rows.Count > 0)
                    {
                        ExportExcel(DtNegative);
                    }
                    divmssgpopup.Visible = true;
                    cleartext();
                    string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    string script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
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
    #region Revisit Caee
    protected void btngetreviitotp_Click(object sender, EventArgs e)
    {
        if (ddlcondition.SelectedValue == "10")
        {
            if (txtrevisitpersonmobileno.Text != "")
            {
                GenerateOTP();
                //send("", "", "email");
                send(txtrevisitpersonmobileno.Text, "", "Mobile");
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
            send(ddlnum.SelectedItem.Text, "", "Mobile");
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

                    DataTable DTOTPCheck = Lo.RetriveCodeMOFE(0, "", "", DateTime.Now, txtotprevisitcustmer.Text, hflocsrid.Value, hfAllocationDate.Value, "", "", "GetMobotp");
                    if (DTOTPCheck.Rows.Count == 1)
                    {
                        if (txtotprevisitcustmer.Text == DTOTPCheck.Rows[0]["Mobile_OTP"].ToString())
                        {
                            DateTime Datetimerevisit = Convert.ToDateTime(txtrevisittimeanddate.Text);
                            string DatetimeRevis = Datetimerevisit.ToString("dd/MMM/yyyy");
                            DateTime DatetimerevisitTIme = Convert.ToDateTime(txttimerevisit.Text);
                            string DatetimeRevisTime = DatetimerevisitTIme.ToString("HH:mm:ss");
                            int UpdateRevisitStatusByFE = Lo.UpdateCodeFEAssignMO(Convert.ToInt64(0), "", txtrevisitpersonmetname.Text, Convert.ToDateTime(hfAllocationDate.Value),
                                txtrevisitpersonmetname.Text, txtrevisitpersonmetname.Text, DatetimeRevis, DatetimeRevisTime, txtrevisitpersonmobileno.Text, txtrevisitpersonmetemail.Text,
                                txtrevisitpersonmetdesig.Text, ddlcondition.SelectedItem.Text, hfFeName.Value, hfcompname.Value, "Update2");
                            if (UpdateRevisitStatusByFE != 0)
                            {
                                SendMailClientPending();
                                DivOTPRevisit.Visible = false;
                                revisittimedate.Visible = false;
                                divmssgpopup.Visible = true;
                                cleartext();
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit rescheduled successfully.");
                                string script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                            }
                            else
                            {
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit rescheduled status not updated some error occure.");
                                string script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
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
                //send("", "", "email");
                send(ddlnump.SelectedItem.Text, "", "Mobile");
            }
            else
            {
                // send("", "", "email");
                send(txtpersonemetmobilenosuccess.Text, "", "Mobile");
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
            send(txtpersonemetmobilenosuccess.Text, "", "Mobile");
        }
        else
        {
            //  send("", "", "email");
            send(ddlnump.SelectedItem.Text, "", "Mobile");
        }
        divmssgpopup.Visible = true;
        divmssgpopup.InnerHtml = "OTP resend successfully";
        divmssgpopup.Attributes.Add("Class", "alert alert-danger");
    }
    protected void btnsuccsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtactivenuber.Text != "" && txtdeactivenumber.Text != "" && txtnewnumber.Text != "" && txtpersonemetmobilenosuccess.Text != "")
            {
                if (txtoptsuces.Text != "")
                {
                    DataTable DTOTPCheck = Lo.RetriveCodeMOFE(0, "", "", DateTime.Now, txtoptsuces.Text, hflocsrid.Value, hfAllocationDate.Value, "", "", "GetMobotp");
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
                            int UpdateCaseID = Lo.UpdateCodeFEAssignMO(0, "", "", Convert.ToDateTime(hfAllocationDate.Value), txtpersonmetnamesuccess.Text, txtsuccpersonmetdesig.Text, txtactivenuber.Text, txtdeactivenumber.Text, txtnewnumber.Text, txtpersonmetnamesuccess.Text, txtpersonmetemailidsuccess.Text, txtsuccpersonmetdesig.Text, txtpersonemetmobilenosuccess.Text, hfcompname.Value, "Update1");
                            if (UpdateCaseID != 0)
                            {
                                SendMailClientSuccessVerification(hfbilledextid1.Value, hfFeName.Value, hfcustomerorpartyname.Value);
                                cleartext();
                                divmssgpopup.Visible = true;
                                divmssgpopup.InnerHtml = "SuccessFully Update Status";
                                divmssgpopup.Attributes.Add("class", "alert alert-success");
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("SuccessFully Update Status");
                                string script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                            }
                            else
                            {
                                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit rescheduled status not updated some error occure.");
                                string script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
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
    protected void ExportExcel(DataTable DtNotEligible)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            DtNotEligible.TableName = DtNotEligible.Rows[0]["Company_Name"].ToString().Substring(0, 5);
            wb.Worksheets.Add(DtNotEligible);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                SendMailNegativeCaseToAirtelOrOtherTeam(bytes, DtNotEligible);
            }
        }
    }
    protected void SendMailClientSuccessVerification(string Billedextid, string FeName, string UserOrCompName)
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/MoPositivePending.htm")))
        {
            body = reader.ReadToEnd();
        }
        if (UserOrCompName != "")
        {
            UserOrCompName = hfcustomerorpartyname.Value;
        }
        else
        {
            UserOrCompName = hfcompname.Value;
        }
        body = body.Replace("{PartyName}", UserOrCompName);
        body = body.Replace("{Logical}", Billedextid);
        body = body.Replace("{FEName}", FeName);
        body = body.Replace("{Message}", " This is for your information that verification as per DoT regulations for MO-Periodic has been completed successfully.");
        SendMail s;
        s = new SendMail();
        s.CreateMail("verification@rcpl.in", "verification@rcpl.in", "Airtel Periodic Verification Mobile Success / " + hfcompname.Value + "", body);
        // s.CreateMail("verification@rcpl.in", "mohdwali@globalitpoint.com", "Airtel Periodic Verification Mobile Success / " + hfcompname.Value + "", body);
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
    protected void SendMailClientPending()
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/SuccessorFailVerification.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Message}", " This for your information that verification as per DoT regulations for MO-Periodic has been resechduled " + txtrevisittimeanddate.Text + ". <br /><br />Thanks for choosing Airtel as your preferred service partner. <br /><br />assuring you our best services always. <br /><br />");
        SendMail s;
        s = new SendMail();
        s.CreateMail("verification@rcpl.in", "verification@rcpl.in", "Airtel Periodic Verification Mobile Revisit /" + hfcompname.Value + "", body);
        //  s.CreateMail("verification@rcpl.in", "mohdwali@globalitpoint.com", "Airtel Periodic Verification Mobile Revisit /" + hfcompname.Value + "", body);
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
    protected void SendMailNegativeCaseToAirtelOrOtherTeam(byte[] bytes, DataTable DtNotEligible)
    {
        if (DtNotEligible.Rows.Count > 0)
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/MoNegativeMail.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{ClientName}", DtNotEligible.Rows[0]["Company_Name"].ToString());
            SendMail s;
            s = new SendMail();
            s.CreateInvoiceMailForByte("verification@rcpl.in", "verification@rcpl.in", "Airtel Periodic Verification Mobile Negative/" + DtNotEligible.Rows[0]["Company_Name"].ToString() + "", body, "MO-Periodic", bytes, "");
            //  s.CreateInvoiceMailForByte("verification@rcpl.in", "mohdwali@globalitpoint.com", "Airtel Periodic Verification Mobile Negative/" + DtNotEligible.Rows[0]["Company_Name"].ToString() + "", body, "MO-Periodic", bytes, "");
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
    #endregion
    #region OTP Send By DDL"
    protected void sendotp_Click(object sender, EventArgs e)
    {
        if (ddlnum.SelectedItem.Value != "otp")
        {
            GenerateOTP();
            // send("", "", "Mobile");
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
            send(ddlnump.SelectedItem.Text, "", "Mobile");
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
    protected void ddlprodbycat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAssignJobCompany();
    }

    #endregion
}