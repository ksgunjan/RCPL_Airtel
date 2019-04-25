using System;
using BusinessLayer;
using System.Data;
using System.Collections.Specialized;

public partial class frmAddMail : System.Web.UI.Page
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
    protected void SaveEmail()
    {
        if (hfcomp.Value != "")
        {
            hysavecomp["EmailId"] = Convert.ToInt32(hfcomp.Value);
        }
        else
        {
            hysavecomp["EmailId"] = Mid;
        }
        hysavecomp["Designation"] = txtdesignation.Text.Trim();

        hysavecomp["Email"] = txtemail.Text.Trim();
        string str = Lo.SaveEmail(hysavecomp, out _msg, out _sysMsg);
        if (str == "Save")
        {
            if (btnsub.Text != "Update")
            {
                cleartext();
                diverror.InnerHtml = "Email save successfully";
                diverror.Attributes.Add("class", "alert alert-success");
            }
            else
            {
                cleartext();
                diverror.InnerHtml = "Email update or save successfully";
                diverror.Attributes.Add("class", "alert alert-success");
                btnsub.Text = "Submit";
            }
        }
        else
        {
            diverror.InnerHtml = "Email not save";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        if (txtemail.Text != "")
        {
            System.Threading.Thread.Sleep(1000);
            SaveEmail();
        }
        else
        {
            diverror.InnerHtml = "Fill Email ID";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    protected void cleartext()
    {
        txtemail.Text = "";
        txtdesignation.Text = "";
        hfcomp.Value = "";
    }
    protected void update()
    {
        if (Request.QueryString["string"] != null)
        {
            hfcomp.Value = Convert.ToInt64(Request.QueryString["string"]).ToString();
            DataTable DTComp = Lo.RetriveCodeEmail(Convert.ToInt64(hfcomp.Value));
            if (DTComp.Rows.Count > 0)
            {
                if (DTComp.Rows[0]["IsActive"].ToString() == "Y")
                {
                    txtemail.Text = DTComp.Rows[0]["Email"].ToString();
                    txtdesignation.Text = DTComp.Rows[0]["Designation"].ToString();
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