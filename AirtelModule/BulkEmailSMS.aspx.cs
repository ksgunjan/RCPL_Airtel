using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using BusinessLayer;

public partial class BulkEmailSMS : System.Web.UI.Page
{
    Logic Lo = new Logic();
    SendMail s = new SendMail();
    //  Common Co = new Common();
    string exMsg = "";
    Label Email;
    Label Mobile;
    Label ContactPerson;
    string Attachment1;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["pg"] != null)
            {
                Lo.verifyconnect();
                bindrblist();
            }
            else
            {
                Response.Redirect("../Default.aspx", false);

            }
        }

    }

    private void bindrblist()
    {
        DataTable DtData = Lo.RetriveCodeWithContidion("select * from fn_BulkSMSorEmail('','1')");
        if (DtData.Rows.Count > 0)
        {
            gvdetail.DataSource = DtData;
            gvdetail.DataBind();
            divsendsmsoremail.Visible = true;
            btnsendsmsoremail.Visible = true;
            button.Visible = true;
        }
    }

    protected void rbsendtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (rbsendtype.SelectedItem.Value == "email")
        //{
        DataTable DtData = Lo.RetriveCodeWithContidion("select * from fn_BulkSMSorEmail('','1')");
        if (DtData.Rows.Count > 0)
        {
            gvdetail.DataSource = DtData;
            gvdetail.DataBind();
            divsendsmsoremail.Visible = true;
            btnsendsmsoremail.Visible = true;
            button.Visible = true;
        }
        //}
        //else if (rbsendtype.SelectedItem.Value == "sms")
        //{
        //    DataTable DtData = Lo.RetriveCodeWithContidion("select * from fn_BulkSMSorEmail('','2')");
        //    if (DtData.Rows.Count > 0)
        //    {
        //        gvdetail.DataSource = DtData;
        //        gvdetail.DataBind();
        //        divsendsmsoremail.Visible = true;
        //        btnsendsmsoremail.Visible = true;
        //        button.Visible = true;
        //    }
        //}
        else
        {
            btnsendsmsoremail.Visible = false;
            divsendsmsoremail.Visible = false;
            button.Visible = false;
        }

    }

    protected void btnsendsmsoremail_Click(object sender, EventArgs e)
    {
        #region Airtel
        //if (rbsendtype.SelectedValue == "email")
        //{
        try
        {
            foreach (GridViewRow gr in gvdetail.Rows)
            {
                CheckBox cb = (CheckBox)gr.FindControl("chkRow");
                if (cb.Checked)
                {
                    subject.Visible = true;
                    attach.Visible = true;
                    txtsms.Visible = false;
                    txtmsg.Visible = true;
                    btnSend.Text = "Send Mail";
                    txtSubject.Text = "";
                    txtmsg.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "showPopup();", true);
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        //}
        //else if (rbsendtype.SelectedValue == "sms")
        //{
        //    try
        //    {
        //        foreach (GridViewRow gr in gvdetail.Rows)
        //        {
        //            CheckBox cb = (CheckBox)gr.FindControl("chkRow");
        //            if (cb.Checked)
        //            {
        //                subject.Visible = false;
        //                attach.Visible = false;
        //                txtmsg.Visible = false;
        //                txtsms.Visible = true;
        //                btnSend.Text = "Send SMS";
        //                txtsms.Text = "";
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "showPopup();", true);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //    }
        //}
        // else
        //{
        //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please select Email Or SMS.')", true);
        //}
        #endregion
    }
    protected void chkHeader_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gr in gvdetail.Rows)
            {
                CheckBox cb = (CheckBox)gr.FindControl("chkRow");
                if (((CheckBox)sender).Checked)
                {
                    cb.Checked = true;
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (fufile.FileName != string.Empty)
        {
            if (fufile.PostedFile.ContentLength > 200000)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Attachment file size is too large');", true);
                return;
            }
            string sUpFileName = Path.GetFileName(fufile.PostedFile.FileName);
            fufile.PostedFile.SaveAs(Server.MapPath(("Attachment/") + sUpFileName));
            sUpFileName = "Attachment/" + sUpFileName;
            Attachment1 = sUpFileName;
        }
        foreach (GridViewRow row in gvdetail.Rows)
        {
            Email = (Label)row.FindControl("lblemail") as Label;
            Mobile = (Label)row.FindControl("lblmob") as Label;
            ContactPerson = (Label)row.FindControl("lblcontactperson") as Label;
            if ((row.FindControl("chkRow") as CheckBox).Checked)
            {
                try
                {
                    if (Email.Text != "" && btnSend.Text == "Send Mail" && txtSubject.Text != "")
                    {
                        string body;
                        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/message.txt")))
                        {
                            body = reader.ReadToEnd();
                        }
                        body = body.Replace("{Message}", txtmsg.Text);
                        //body = body.Replace("{CompanyName}", ContactPerson.Text);
                        SendMail s = new SendMail();
                        //  s.CreateMail(ConfigurationManager.AppSettings["mailSupport"].ToString(), Email.Text, txtSubject.Text, body);
                        s.CreateInvoiceMail(ConfigurationManager.AppSettings["mailSupport"].ToString(), Email.Text, txtSubject.Text, body, Server.MapPath(Attachment1));
                        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
                        if (result == true)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Email Send Successfully.')", true);
                        }
                    }
                    else if (btnSend.Text == "Send SMS" && Mobile.Text != "")
                    {
                        SendSMS(Mobile.Text);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Thankyou, your SMS Query  will be send successfully')", true);
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }

    }
    public void SendSMS(string mMob)
    {
        int mStartPos = (mMob.Length - 10);
        mMob = mMob.Substring(mStartPos);
        string _msg = txtsms.Text.Trim();

        string requestUristring = string.Format("http://smsgateway.spicedigital.in/MessagingGateway/MessagePush?username=DefexpoT&password=Def@1234&messageType=text&mobile={0}&senderId=MYRCPL&message=" + _msg, mMob);
        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(requestUristring);
        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
        string responseString = respStreamReader.ReadToEnd();
        respStreamReader.Close();
        myResp.Close();
    }
}