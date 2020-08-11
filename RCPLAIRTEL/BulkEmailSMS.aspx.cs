using BuisnessLayer;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BulkEmailSMS : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private Common Co = new Common();
    private DataTable DtData = new DataTable();
    private DataTable DtMOData = new DataTable();
    public Label mEmail;
    public Label mSms;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            check();
        }
    }
    protected void rblisttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        check();
    }
    protected void check()
    {
        if (rblisttype.SelectedItem.Value == "1")
        {
            //  if (rbsendtype.SelectedValue == "both")
            // {
            DtData = Lo.RetriveSPCode1("LPBoth");
            // }
            if (DtData.Rows.Count > 0)
            {
                gvdetail.DataSource = DtData;
                gvdetail.DataBind();
                divsendsmsoremail.Visible = true;
            }
            else
            {
                divsendsmsoremail.Visible = false;
            }
        }
        else if (rblisttype.SelectedItem.Value == "2")
        {
            // if (rbsendtype.SelectedValue == "both")
            // {
            DtMOData = Lo.RetriveSPCode1("MOBoth");
            //  }
            if (DtMOData.Rows.Count > 0)
            {
                gvdetail.DataSource = DtMOData;
                gvdetail.DataBind();
                divsendsmsoremail.Visible = true;
            }
            else
            {
                divsendsmsoremail.Visible = false;
            }
        }
    }
    protected void btnsendsmsoremail_Click(object sender, EventArgs e)
    {
        if (rbsendtype.SelectedValue == "sms")
        {
            attach.Visible = false;
            txtsms.Visible = true;
            txtmsg.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
        }
        else if (rbsendtype.SelectedValue == "email")
        {
            attach.Visible = true;
            txtsms.Visible = false;
            txtmsg.Visible = true;
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
        }
        else
        {
            attach.Visible = true;
            txtmsg.Visible = true;
            txtsms.Visible = true;
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvdetail.Rows)
        {
            mEmail = (Label)row.FindControl("lblemail") as Label;
            mSms = (Label)row.FindControl("lblmob") as Label;
            if ((row.FindControl("chkRow") as CheckBox).Checked)
            {
                if (rbsendtype.SelectedItem.Value == "both")
                {
                    if (mEmail.Text != "")
                    {
                        try
                        {
                            string body;
                            using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/BulkEmail.html")))
                            {
                                body = reader.ReadToEnd();
                            }
                            body = body.Replace("{Message}", txtmsg.Text);
                            SendMail s;
                            s = new SendMail();
                            if (fufile.HasFile != false)
                            {
                                s.CreateInvoiceMail("verification@rcpl.in", mEmail.Text, txtSubject.Text, body, Server.MapPath(fufile.FileName));
                            }
                            else
                            {
                                s.CreateMail("verification@rcpl.in", "mohdwali@globalitpoint.com", txtSubject.Text, body);
                            }
                            string exMsg = "";
                            bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), Convert.ToInt16(ConfigurationManager.AppSettings["Port"].ToString()), ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);

                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }
                    }
                    if (mSms.Text != "")
                    {
                        try
                        {
                            string requestUristring = string.Format("http://smsgateway.spicedigital.in/MessagingGateway//MessagePush?username=DefexpoT&password=Def@1234&messageType=text&mobile={0}&senderId=myrcpl&message=" + txtsms.Text, mSms.Text);
                            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(requestUristring);
                            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                            string responseString = respStreamReader.ReadToEnd();
                            respStreamReader.Close();
                            myResp.Close();
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }
                    }

                }
                else if (rbsendtype.SelectedItem.Value == "email")
                {
                    if (mEmail.Text != "")
                    {
                        try
                        {
                            string body;
                            using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/BulkEmail.html")))
                            {
                                body = reader.ReadToEnd();
                            }
                            body = body.Replace("{Message}", txtmsg.Text);
                            SendMail s;
                            s = new SendMail();
                            if (fufile.HasFile != false)
                            {
                                s.CreateInvoiceMail("verification@rcpl.in", mEmail.Text, txtSubject.Text, body, Server.MapPath(fufile.FileName));
                            }
                            else
                            {
                                s.CreateMail("verification@rcpl.in", "mohdwali@globalitpoint.com", txtSubject.Text, body);
                            }
                            string exMsg = "";
                            bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), Convert.ToInt16(ConfigurationManager.AppSettings["Port"].ToString()), ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }
                    }
                }
                else if (rbsendtype.SelectedItem.Value == "sms")
                {
                    try
                    {
                        if (mSms.Text != "")
                        {
                            string requestUristring = string.Format("http://smsgateway.spicedigital.in/MessagingGateway//MessagePush?username=DefexpoT&password=Def@1234&messageType=text&mobile={0}&senderId=myrcpl&message=" + txtsms.Text, mSms.Text);
                            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(requestUristring);
                            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                            string responseString = respStreamReader.ReadToEnd();
                            respStreamReader.Close();
                            myResp.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                }
            }
        }
    }
    protected void gvdetail_RowCreated(object sender, GridViewRowEventArgs e)
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