using System;
using BusinessLayer;
using System.Data;
using System.Collections.Specialized;
public partial class frmUserCategory : System.Web.UI.Page
{
    Logic Lo = new Logic();
    HybridDictionary hysavecomp = new HybridDictionary();
    string _msg = string.Empty;
    string _sysMsg = string.Empty;
    Int32 Mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            update();
        }
    }
    protected void SaveComp()
    {
        if (hfcomp.Value != "")
        {
            hysavecomp["UCategoryID"] = Convert.ToInt32(hfcomp.Value);
        }
        else
        {
            hysavecomp["UCategoryID"] = Mid;
        }
        hysavecomp["UserCategory"] = txtusercategory.Text.Trim();
        string str = Lo.SaveUserCategory(hysavecomp, out _msg, out _sysMsg);
        if (str == "Save")
        {
            if (btnsub.Text != "Update")
            {
                cleartext();
                diverror.InnerHtml = "UserCategory save successfully";
                diverror.Attributes.Add("class", "alert alert-success");
            }
            else
            {
                cleartext();
                diverror.InnerHtml = "UserCategory update or save successfully";
                diverror.Attributes.Add("class", "alert alert-success");
                btnsub.Text = "Submit";
            }
        }
        else
        {
            diverror.InnerHtml = "UserCategory not save";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        if (txtusercategory.Text != "")
        {
            System.Threading.Thread.Sleep(1000);
            SaveComp();
        }
        else
        {
            diverror.InnerHtml = "Fill Company Name";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    protected void cleartext()
    {
        txtusercategory.Text = "";
        hfcomp.Value = "";
    }
    protected void update()
    {
        if (Request.QueryString["string"] != null)
        {
            hfcomp.Value = Convert.ToInt64(Request.QueryString["string"]).ToString();
            DataTable DTComp = Lo.RetriveCodeUserCategory(Convert.ToInt64(hfcomp.Value));
            if (DTComp.Rows.Count > 0)
            {
                if (DTComp.Rows[0]["IsActive"].ToString() == "Y")
                {
                    txtusercategory.Text = DTComp.Rows[0]["UserCategory"].ToString();
                    btnsub.Text = "Update";
                }
                else
                {
                    diverror.InnerHtml = "Record is deactive please active first for update.";
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
            }
        }
    }
}