using System;
using System.Web.UI;
using BusinessLayer;
using System.Data;

public partial class frmFEChangePassword : System.Web.UI.Page
{
    Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
        }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtoldpassword.Text != "" && txtnewpassword.Text != "")
            {
                DataTable DtGetOldPassword = Lo.RetriveCodeWithContidion("select EmpID,UserName,Password from tbl_trn_Login where UserName='" + txtemail.Text + "'");
                if (DtGetOldPassword.Rows.Count > 0)
                {
                    if (txtoldpassword.Text == DtGetOldPassword.Rows[0]["Password"].ToString())
                    {
                        Int32 UpdatePassword = Lo.UpdateCodeOther("tbl_trn_Login", "set Password='" + txtnewpassword.Text + "'", "UserName='" + txtemail.Text + "'");
                        if (UpdatePassword != 0)
                        {
                            Session.Clear();
                            Session.Abandon();
                            var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Password update successfully please relogin in your account with new password.");
                            var script = string.Format("alert({0});window.location ='CRM-Login';", message);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                        }
                        else
                        {
                            diverror.Visible = true;
                            diverror.InnerHtml = ("Password not update.");
                            diverror.Attributes.Add("Class", "alert alert-danger");
                        }
                    }
                    else
                    {
                        diverror.Visible = true;
                        diverror.InnerHtml = ("Password not match with old password.");
                        diverror.Attributes.Add("Class", "alert alert-danger");
                    }
                }
                else
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = ("We did not identify you as our FE please enter valid email or old password.");
                    diverror.Attributes.Add("Class", "alert alert-danger");
                }
            }
        }
        catch (Exception ex)
        {
            diverror.Visible = true;
            diverror.InnerHtml = ex.Message;
            diverror.Attributes.Add("Class", "alert alert-danger");
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