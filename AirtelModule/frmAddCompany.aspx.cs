using System;
using BusinessLayer;
using System.Data;
using System.Collections.Specialized;

public partial class frmAddCompany : System.Web.UI.Page
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
            hysavecomp["CompId"] = Convert.ToInt32(hfcomp.Value);
        }
        else
        {
            hysavecomp["CompId"] = Mid;
        }
        hysavecomp["CompName"] = txtcompname.Text.Trim();
        hysavecomp["CompGSTNo"] = txtgstno.Text.Trim();
        hysavecomp["CompPanNo"] = txtpanno.Text.Trim();
        string str = Lo.SaveComp(hysavecomp, out _msg, out _sysMsg);
        if (str == "Save")
        {
            if (btnsub.Text != "Update")
            {
                cleartext();
                diverror.InnerHtml = "Company save successfully";
                diverror.Attributes.Add("class", "alert alert-success");
            }
            else
            {
                cleartext();
                diverror.InnerHtml = "Company update or save successfully";
                diverror.Attributes.Add("class", "alert alert-success");
                btnsub.Text = "Submit";
            }
        }
        else
        {
            diverror.InnerHtml = "Company not save";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        if (txtcompname.Text != "")
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
        txtcompname.Text = "";
        txtgstno.Text = "";
        txtpanno.Text = "";
        hfcomp.Value = "";
    }
    protected void update()
    {
        if (Request.QueryString["string"] != null)
        {
            hfcomp.Value = Convert.ToInt64(Request.QueryString["string"]).ToString();
            DataTable DTComp = Lo.RetriveCodeCompany(Convert.ToInt64(hfcomp.Value));
            if (DTComp.Rows.Count > 0)
            {
                if (DTComp.Rows[0]["IsActive"].ToString() == "Y")
                {
                    txtcompname.Text = DTComp.Rows[0]["CompName"].ToString();
                    txtgstno.Text = DTComp.Rows[0]["CompGstNo"].ToString();
                    txtpanno.Text = DTComp.Rows[0]["CompPanNo"].ToString();
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