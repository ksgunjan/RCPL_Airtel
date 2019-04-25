using System;
using System.Data;
using BusinessLayer;
using System.Web.UI;

public partial class MasterPage2 : System.Web.UI.MasterPage
{
    Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            if (Session["LoginEmail"] != null)
            {
                GetUserDetail();
                getformname();
            }
            else
            {
                Response.RedirectToRoute("CRM-Login");
            }
        }
    }
    protected void GetUserDetail()
    {
        try
        {
            DataTable DtUser = Lo.RetriveCodeUser(Session["LoginEmail"].ToString());
            if (DtUser.Rows.Count > 0)
            {
                if (DtUser.Rows[0]["LastLoginTime"].ToString() != "")
                {
                    DateTime datetime = Convert.ToDateTime(DtUser.Rows[0]["LastLoginTime"].ToString());
                    string finaldatetime = datetime.ToString("dd-MMMM-yyyy, hh:mm");
                    lbloginfrom.Text = "Last Login:- " + finaldatetime;
                }
                name.InnerText = DtUser.Rows[0]["Name"].ToString();
                category.InnerText = DtUser.Rows[0]["Category"].ToString();
                a.Src = "ProfileImage" + "/" + DtUser.Rows[0]["ProfilePic"].ToString();
                if (category.InnerText == "Manager")
                {     
                    Session["Category"] = category.InnerText;
                    menucompany.Visible = false;
                    menuemail.Visible = false;
                    menuproduct.Visible = false;
                    menuzone.Visible = false;
                    menuexceladmin.Visible = false;
                    menuexcelmanager.Visible = true;
                    menuuser.Visible = false;
                    menufe.Visible = false;
                    menumanagerfe.Visible = true;
                    rescheduled.Visible = true;
                    menegardiv.Visible = false;
                    reports.Visible = true;
                    email1.Visible = false;
                    email2.Visible = false;
                    email3.Visible = false;
                    onlymanagerdashboard.Visible = true;
                    homelink.HRef = "~/Manager-DashBoard";
                }
                else if (category.InnerText == "Field Executive")
                {
                    Session["Category"] = category.InnerText;
                    menucompany.Visible = false;
                    menuexceladmin.Visible = false;
                    menuexcelmanager.Visible = false;
                    menuproduct.Visible = false;
                    menuuser.Visible = false;
                    menuzone.Visible = false;
                    menuemail.Visible = false;
                    menumanagerfe.Visible = false;
                    menufefejob.Visible = true;
                    rescheduled.Visible = false;
                    menegardiv.Visible = false;
                    historyferecord.Visible = true;
                    reports.Visible = false;
                    onlymanagerdashboard.Visible = false;
                    homelink.HRef = "~/FE-DashBoard";
                }
                else if (category.InnerText == "Admin")
                {
                    Session["Category"] = category.InnerText;
                    menufefejob.Visible = false;
                    menuexcelmanager.Visible = false;
                    menegardiv.Visible = false;
                    innermenuapproveexcel.Visible = false;
                    email3.Visible = false;
                    menufe.Visible = false;
                    rescheduled.Visible = false;
                    homelink.HRef = "~/CRM-DashBoard";
                }
                else if (category.InnerText == "Manager Sales")
                {
                    Session["Category"] = category.InnerText;
                    menucompany.Visible = false;
                    menuexceladmin.Visible = false;
                    menuexcelmanager.Visible = false;
                    menuproduct.Visible = false;
                    menuuser.Visible = false;
                    menuzone.Visible = false;
                    menuemail.Visible = false;
                    menumanagerfe.Visible = false;
                    menufe.Visible = false;
                    rescheduled.Visible = false;
                    menegardiv.Visible = true;
                    historyferecord.Visible = false;
                    reports.Visible = false;
                    onlymanagerdashboard.Visible = true;
                    homelink.HRef = "~/Manager-DashBoard";
                }
            }
            else
            {
                Response.RedirectToRoute("CRM-Login");
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            //Response.RedirectToRoute("CRM-Login");
        }
    }
    protected void LogOut()
    {
        try
        {
            if (Session["LoginEmail"] != null)
            {
                Int32 id = Lo.UpdateLogOutTime(Session["LoginEmail"].ToString());
                Session.Abandon();
                Session.Clear();
                Response.RedirectToRoute("CRM-Login");
            }
            else
            {
                Session.Abandon();
                Session.Clear();
                Response.RedirectToRoute("CRM-Login");
            }
        }
        catch (Exception ex)
        {
            Response.RedirectToRoute("CRM-Login");
        }
    }
    protected void lbllogout_Click(object sender, EventArgs e)
    {
        LogOut();
    }
    protected void getformname()
    {
        string url = Request.Path.Substring(Request.Path.LastIndexOf("/")).Substring(1);
        divformname.InnerText = url;
    }
    protected void btnbulkmailorsms_Click(object sender, EventArgs e)
    {
        GenerateOTP();
        divotpmobile.Visible = true;
        SendMail s = new SendMail();
        //s.sendSMS("9811020978", hfOTPMobile.Value);
        s.sendSMS("9871804280", hfOTPMobile.Value);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "showPopup();", true);
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
            hfOTPMobile.Value = otp;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void verifyotp_Click(object sender, EventArgs e)
    {

        if (txtotpmobile.Text != "")
        {
            try
            {
                if (hfOTPMobile.Value == txtotpmobile.Text)
                {
                    Response.Redirect("Bulk-Email?pg=" + hfOTPMobile.Value);
                }
                else
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please enter valid otp')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please fill otp')", true);
        }
    }
}
