using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.IO;

/// <summary>
/// Summary description for SendMail
/// </summary>
public class SendMail
{
    public SendMail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    SmtpClient mailClient = new SmtpClient();
    MailMessage Email = new MailMessage();
    public void CreateMail(string sender, string receipt, String subject, String body)
    {
        Email = new MailMessage(sender, receipt);
        Email.IsBodyHtml = true;
        Email.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
        Email.Subject = subject;
        Email.Body = body;
    }
    public void CreateMail(string sender, string receipt, String subject, String body, string EmailType)
    {

        Email = new MailMessage(sender, receipt);
        Email.IsBodyHtml = true;
        if (EmailType == "Leased Line")
        {
            Email.CC.Add("verification@rcpl.in");//team leader leaseline rcpl
            // Email.CC.Add("bs1@rcpl.in");//process owner 
        }
        else if (EmailType == "PR1-Fixed Line")
        {
            Email.CC.Add("verification@rcpl.in");//team leader pri rcpl
            //   Email.CC.Add("bs1@rcpl.in");//process owner
        }
        else if (EmailType == "Customer")
        {
            Email.CC.Add("Shyam.Prasad@airtel.com");//Airtel Manager
            Email.CC.Add("NCRULinks.Verification@airtel.com");//Lease Line Airtel ID
            Email.CC.Add("verification@rcpl.in");//Process Owner RCPL
        }
        else if (EmailType == "Positive")
        {
            Email.CC.Add("Shyam.Prasad@airtel.com");//Airtel Manager
            Email.CC.Add("verification@rcpl.in");//Process Owner RCPL
            Email.CC.Add("NCRULinks.Verification@airtel.com");
        }
        else if (EmailType == "Pending")
        {
            Email.CC.Add("verification@rcpl.in");//Process Owner RCPL
        }
        else if (EmailType == "Preschedule")
        {
            Email.CC.Add("verification@rcpl.in");//Process Owner RCPL
        }
        else if (EmailType == "ExcelVerify")
        {
            Email.CC.Add("gagan@rcpl.in");//Process Owner RCPL
        }
        Email.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
        Email.Subject = subject;
        Email.Body = body;
    }
    public void CreateMail(string sender, string receipt, String subject, String body, string EmailType, string CaseStatus, string BCCID)
    {
        if (CaseStatus == "Negative")
        {
            Email = new MailMessage(sender, receipt);
            Email.IsBodyHtml = true;
            if (EmailType == "Leased Line")
            {
                Email.CC.Add("Shyam.Prasad@airtel.com");//Airtel Manager Assistant Manager Acquisition Experience
                Email.CC.Add("verification@rcpl.in");//process owner
                Email.CC.Add("NCRULinks.Verification@airtel.com");
                if (BCCID != "")
                {
                    Email.CC.Add(BCCID);//Sam Mail id
                }
            }
            else if (EmailType == "PR1-Fixed Line")
            {
                Email.CC.Add("verification@rcpl.in");//team leader pri rcpl
                // Email.CC.Add("bs1@rcpl.in");//process owner
            }
            else if (EmailType == "Customer")
            {
                //  Email.CC.Add("sales1@rcpl.in");//Airtel Manager
                Email.CC.Add("NCRULinks.Verification@airtel.com");//Lease Line Airtel ID
                Email.CC.Add("verification@rcpl.in");//Process Owner RCPL
            }
            else if (EmailType == "LastMailNegative")
            {
                Email.CC.Add("shikha1.gupta@airtel.com,");
                Email.CC.Add("devender.jain@airtel.com");

            }
            Email.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
            Email.Subject = subject;
            Email.Body = body;
        }

    }
    public bool sendMail(string host, Int32 port, string username, string password, out string exMsg)
    {
        // for your information host is "smtp.rcpl.in" , port is 25, 587( tried both), username is "auto@rcpl.in"  pwd is "Mda*KTI8"

        NetworkCredential basicAuthenticationInfo;
        mailClient = new System.Net.Mail.SmtpClient(host, port);
        basicAuthenticationInfo = new System.Net.NetworkCredential(username, password);
        //'Put your own, or your ISPs, mail server name on this next line
        mailClient.EnableSsl = false;
        // mailClient.EnableSsl = true;
        mailClient.UseDefaultCredentials = false;
        mailClient.Credentials = basicAuthenticationInfo;
        try
        {
            mailClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            mailClient.Send(Email);
            exMsg = "";
            return true;
        }
        catch (SmtpFailedRecipientException e)
        {
            exMsg = e.Message;
            return false;
        }
    }
    public void CreateInvoiceMail(string s, string receipt, String subject, String body, string path)
    {
        Email = new MailMessage(s, receipt);
        Email.IsBodyHtml = true;
        Email.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
        Email.Subject = subject;
        Email.Body = body;
        if (path != "")
        {
            Email.Attachments.Add(new Attachment(path));
        }
    }
    public void CreateInvoiceMailForByte(string s, string receipt, String subject, String body, string Prodtype, byte[] bytes, string ccid)
    {
        Email = new MailMessage(s, receipt);
        Email.IsBodyHtml = true;
        if (Prodtype == "Leased Line" && ccid != "")
        {
            Email.CC.Add(ccid);//process owner or team leade lease line
        }
        else if (Prodtype == "PR1-Fixed Line" && ccid != "")
        {
            Email.CC.Add(ccid);//Mail to process owner or team leader pri
        }
        else if (Prodtype == "MO-Periodic")
        {
            // Email.Attachments.Add(new Attachment(ccid));
        }
        else if (Prodtype == "" && ccid != "")
        {
            Email.CC.Add(ccid);//Mail to process owner or team leader pri
        }
        Email.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
        Email.Subject = subject;
        Email.Body = body;
        if (Prodtype == "Leased Line")
        {
            if (bytes.Length > 0)
            {
                Email.Attachments.Add(new Attachment(new MemoryStream(bytes), "LeaseLineNegativeCase.xlsx"));
            }
        }
        if (Prodtype == "PR1-Fixed Line")
        {
            if (bytes.Length > 0)
            {
                Email.Attachments.Add(new Attachment(new MemoryStream(bytes), "PRINegativeCase.xlsx"));
            }
        }
        if (Prodtype == "MO-Periodic")
        {
            if (bytes.Length > 0)
            {
                Email.Attachments.Add(new Attachment(new MemoryStream(bytes), "NegativeCase.xlsx"));
            }
        }
        if (Prodtype == "")
        {
            if (bytes.Length > 0)
            {
                Email.Attachments.Add(new Attachment(new MemoryStream(bytes), "DailyWork-ExcelReport-FEWork.xlsx"));
            }
        }
    }
    public void CreateInvoiceMailForByte1(string s, string receipt, String subject, String body, string Prodtype, byte[] bytes, byte[] wali, byte[] pri, string ccid, string BCCID)
    {
        Email = new MailMessage(s, receipt);
        Email.IsBodyHtml = true;
        if (Prodtype == "" && ccid != "")
        {
            Email.CC.Add(ccid);
        }
        Email.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
        Email.Subject = subject;
        Email.Body = body;
        if (Prodtype == "")
        {
            if (subject == "Automated FE Last Day Visit or Status Update Time Report")
            {
                if (bytes.Length > 0)
                {
                    Email.Attachments.Add(new Attachment(new MemoryStream(bytes), subject + ".xlsx"));
                }
            }
            else
            {
                if (bytes.Length > 0)
                {
                    Email.Attachments.Add(new Attachment(new MemoryStream(bytes), subject + " Leased Line.xlsx"));
                }
            }
            if (subject == "Automated FE Last Day Visit or Status Update Time Report")
            {
            }
            else
            {
                if (wali.Length > 0)
                {
                    Email.Attachments.Add(new Attachment(new MemoryStream(wali), subject + " MO.xlsx"));
                }
            }
            if (subject == "Automated FE Last Day Visit or Status Update Time Report")
            {
            }
            else
            {
                if (pri.Length > 0)
                {
                    Email.Attachments.Add(new Attachment(new MemoryStream(pri), subject + " PRI.xlsx"));
                }
            }
        }
        if (BCCID != "")
        {
            Email.CC.Add(BCCID);
        }
    }
    public void CreateNotEligible(string s, string receipt, String subject, String body, string Prodtype, byte[] bytes, string ccid)
    {
        Email = new MailMessage(s, receipt);
        Email.IsBodyHtml = true;
        if (Prodtype == "Leased Line" && ccid != "")
        {
            Email.CC.Add(ccid);//process owner or team leade lease line
        }
        else if (Prodtype == "PR1-Fixed Line" && ccid != "")
        {
            Email.CC.Add(ccid);//Mail to process owner or team leader pri
        }
        else if (Prodtype == "MO")
        {
            // Email.Attachments.Add(new Attachment(ccid));
        }
        else if (Prodtype == "" && ccid != "")
        {
            Email.CC.Add(ccid);//Mail to process owner or team leader pri
        }
        Email.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
        Email.Subject = subject;
        Email.Body = body;
        if (Prodtype == "Leased Line")
        {
            if (bytes.Length > 0)
            {
                Email.Attachments.Add(new Attachment(new MemoryStream(bytes), "Not Eligible Leased Line.xlsx"));
            }
        }
        if (Prodtype == "PR1-Fixed Line")
        {
            if (bytes.Length > 0)
            {
                Email.Attachments.Add(new Attachment(new MemoryStream(bytes), "Not Eligible PRI-Fixed Line.xlsx"));
            }
        }
        if (Prodtype == "MO")
        {
            if (bytes.Length > 0)
            {
                Email.Attachments.Add(new Attachment(new MemoryStream(bytes), "Not Eligible MO.xlsx"));
            }
        }
    }

    public void sendSMS(string _mobileNo, string _mOTP)
    {
        string StrMobile = _mobileNo;
        //string aMob = "8287354325";
        string aMob = "9811326074";
        string strMsg = "Hello," + _mOTP + " is your OTP for sending Bulk Email to Airtel client.";
        string requestUristring = string.Format("http://smsgateway.spicedigital.in/MessagingGateway/MessagePush?username=DefexpoT&password=Def@1234&messageType=text&mobile={0}&senderId=MYRCPL&message=" + strMsg, StrMobile);
        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(requestUristring);
        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
        string responseString = respStreamReader.ReadToEnd();
        respStreamReader.Close();
        myResp.Close();

        string requestUristring1 = string.Format("http://smsgateway.spicedigital.in/MessagingGateway/MessagePush?username=DefexpoT&password=Def@1234&messageType=text&mobile={0}&senderId=MYRCPL&message=" + strMsg, aMob);
        HttpWebRequest myReq1 = (HttpWebRequest)WebRequest.Create(requestUristring1);
        HttpWebResponse myResp1 = (HttpWebResponse)myReq1.GetResponse();
        System.IO.StreamReader respStreamReader1 = new System.IO.StreamReader(myResp1.GetResponseStream());
        string responseString1 = respStreamReader1.ReadToEnd();
        respStreamReader1.Close();
        myResp1.Close();


    }
}
