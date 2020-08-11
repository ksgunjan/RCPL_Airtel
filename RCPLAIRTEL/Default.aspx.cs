using BuisnessLayer;
using System;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Configuration;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    #region "Variables"
    private Logic LO = new Logic();
    private Common Co = new Common();
    private HybridDictionary hyLogin = new HybridDictionary();
    private string _msg = string.Empty;
    private string Defaultpage = string.Empty;
    private string _sysMsg = string.Empty;
    private string notvalidate = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    #region "Login Code"
    public static bool IsValidEmailId(string InputEmail)
    {
        string pattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$";
        Match match = Regex.Match(InputEmail.Trim(), pattern, RegexOptions.IgnoreCase);
        if (match.Success)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValidEmailId(txtUserName.Text) == true)
            {
                hyLogin["UserName"] = Co.RSQandSQLInjection(txtUserName.Text.Trim() + "'", "hard" + "'");
                hyLogin["Password"] = Co.RSQandSQLInjection(txtPwd.Text.Trim() + "'", "hard" + "'");
                string _EmpId = LO.VerifyEmployee(hyLogin, out _msg, out Defaultpage);
                if (_EmpId != "0" && _EmpId != "1" && _msg != "")
                {
                    Session["Type"] = _msg;
                    Session["User"] = txtUserName.Text;
                    Session["Name"] = _EmpId;
                    Response.RedirectToRoute(Defaultpage);
                }
                else
                {
                    txtPwd.Text = "";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Invalid Login. Either user id or password or both are incorrect.')", true);
                }

            }
            else
            {
                txtPwd.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Invalid email format.')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
    }
    #endregion
    #region ReturnUrl Long"
    public string Resturl(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }
    #endregion
   
}