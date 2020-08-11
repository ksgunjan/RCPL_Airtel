using BuisnessLayer;
using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;

public partial class MasterPage : System.Web.UI.MasterPage
{
    private Logic Lo = new Logic();
    private Common ObjEnc = new Common();
    private string strInterestedArea = "";
    private string strMasterAlloted = "";
    private string sType = "";
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] != null && Session["Name"] != null)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!IsPostBack)
                {
                    try
                    {
                        MenuLogin();
                        //Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                        //SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
                        //int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
                        //Page.ClientScript.RegisterStartupScript(GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Session Expired,Please login again');window.location='Login'", true);
                    }
                }
            }
            else
            {
                Session.Clear();
                Session.Abandon();
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Session Expired,Please login again');window.location='Login'", true);
            }
        }
        catch (Exception)
        {
            Session.Clear();
            Session.Abandon();
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Session Expired,Please login again');window.location='Login'", true);
        }
    }
    protected void lbllogout_Click(object sender, EventArgs e)
    {
        Logoutstatus();
    }
    private void bindMasterMenu(string sType)
    {
        StringBuilder strMasterMenu = new StringBuilder();
        strMasterMenu.Append("<ul class='nav  nav-list'>");
        string[] MCateg = strMasterAlloted.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        string MmCval = "";
        for (int x = 0; x < MCateg.Length; x++)
        {
            MmCval = MCateg[x];
            DataTable dtMArea = Lo.RetriveMasterData(Convert.ToInt64(MmCval), sType, "", 0, "", "", "InterestedAreaMenuId");
            foreach (DataRow row in dtMArea.Rows)
            {
                strMasterMenu.Append("<li class='parent-nav'><a href='#'  title='" + row["Tooltip"].ToString() + "'><i class='fas fa-tachometer-alt'></i><span class='hidden-minibar'>" + row["InterestArea"].ToString() + " </span></a>");
                string[] MCateg1 = dtMArea.Rows[0]["MenuId"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string MmCval1 = "";
                strMasterMenu.Append("<ul class='parent-nav-child' style='display: block;'>");
                for (int j = 0; j < MCateg1.Length; j++)
                {
                    MmCval1 = MCateg1[j];
                    DataTable dtMMenu = Lo.RetriveMasterData(0, "", Session["Type"].ToString(), Convert.ToInt16(MmCval1), "", "", "MenuMain");
                    foreach (DataRow row2 in dtMMenu.Rows)
                    {
                        strMasterMenu.Append("<li class='parent-nav'><a href='" + row2["MenuUrl"].ToString() + "?mu=" + row2["Spanclass"].ToString() + "&id=" + ObjEnc.Encrypt(row["InterestArea"].ToString() + " >> " + row2["MenuName"].ToString()) + "' title=''><i class='fas fa-tachometer-alt'></i><span class='hidden-minibar'>" + row2["MenuName"].ToString() + "</span>");
                        strMasterMenu.Append("<i class='fas fa-angle-down'></i></a>");
                        DataTable SubMmenu = Lo.RetriveMasterData(0, "", Session["Type"].ToString(), Convert.ToInt16(row2["MenuID"].ToString()), "", "", "SubMenu");
                        if (SubMmenu.Rows.Count > 0)
                        {
                            strMasterMenu.Append("<ul class='parent-nav-child'>");
                            foreach (DataRow row1 in SubMmenu.Rows)
                            {
                                strMasterMenu.Append("<li><a href='" + row1["MenuUrl"].ToString() + "?mu=" + row1["Spanclass"].ToString() + "&id=" + ObjEnc.Encrypt(row["InterestArea"].ToString() + " >> " + row2["MenuName"].ToString() + " >> " + row1["MenuName"].ToString()) + "' title=''><i class='far fa-building'></i><span class='hidden-minibar'>" + row1["MenuName"].ToString() + "</span></a></li> ");
                            }
                            strMasterMenu.Append("</ul>");
                        }
                        strMasterMenu.Append("</li>");
                    }
                }
                strMasterMenu.Append("</ul>");
                strMasterMenu.Append("</li>");
            }
        }
        MasterMenu.InnerHtml = strMasterMenu.ToString();
        strMasterMenu.Append("</ul>");
    }
    #region Menu Wise Login
    protected void MenuLogin()
    {
        lbltypelogin.Text = Session["Type"].ToString();
        lblusername.Text = Session["User"].ToString();
        DataTable dtCompany = Lo.RetriveMasterData(0, lbltypelogin.Text, "", 0, "", "", "InterestedArea");
        if (dtCompany.Rows.Count > 0)
        {
            strMasterAlloted = dtCompany.Rows[0]["MasterAllowed"].ToString();
            bindMasterMenu(lbltypelogin.Text);
        }
    }
    #endregion
    protected void Logoutstatus()
    {
        try
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.RedirectToRoute("Login");
        }
        catch (Exception)
        { Response.RedirectToRoute("Login"); }
    }

}
