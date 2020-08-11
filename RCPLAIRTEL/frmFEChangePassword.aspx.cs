using BuisnessLayer;
using System;
using System.Data;
using System.Web.UI;

public partial class frmFEChangePassword : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtoldpassword.Text != "" && txtnewpassword.Text != "")
            {
                DataTable DtGetOldPassword = Lo.RetriveCodeLocal(0, txtemail.Text, "", "", "RetriveUserBYEmail");
                if (DtGetOldPassword.Rows.Count > 0)
                {
                    if (txtoldpassword.Text == DtGetOldPassword.Rows[0]["Password"].ToString())
                    {
                        DataTable UpdatePassword = Lo.RetriveCodeLocal(0, txtnewpassword.Text, txtemail.Text, "", "updatepassword");
                        Session.Clear();
                        Session.Abandon();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Password update successfully please relogin in your account with new password.')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Password not match with old password.')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('We did not identify you as our FE please enter valid email or old password.')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
    }
    protected void cleartext()
    {
        txtemail.Text = "";
        txtnewpassword.Text = "";
        txtoldpassword.Text = "";
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        cleartext();
    }
}