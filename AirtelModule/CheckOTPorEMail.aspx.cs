using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

public partial class CheckOTPorEMail : System.Web.UI.Page
{
    string OTP;
    Logic Lo = new Logic();
    Common Co = new Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          //  Lo.verifyconnect();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (otp.Text != "")
        {
            send(otp.Text);
        }
        else if (email.Text != "")
        {
            sendemail();
        }
        else
        {
        }

    }
    protected void sendemail()
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/SuccessorFailVerification.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Message}", " Demo Email For Testing Mail server is working or not<br /><br />");
        SendMail s;
        s = new SendMail();
        s.CreateMail("verification@rcpl.in", email.Text, "Airtel verification status email!!", body);
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        if (result == true && exMsg == "")
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Mail Send')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Mail  not Send')", true);
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
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            OTP = otp;
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
            GenerateOTP();
            string requestUristring = string.Format("http://smsgateway.spicedigital.in/MessagingGateway//MessagePush?username=DefexpoT&password=Def@1234&messageType=text&mobile={0}&senderId=MYRCPL&message=" + "Dear Airtel Customer, Your Airtel verification has been rescheduled OTP for the same is '" + OTP + "' Thank You.", _mobileNo);
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(requestUristring);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('sms Send')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('sms not Send')", true);
            ex.Message.ToString();
        }
    }
}