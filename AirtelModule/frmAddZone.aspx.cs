using System;
using BusinessLayer;
using System.Data;
using System.Collections.Specialized;

public partial class frmAddZone : System.Web.UI.Page
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
        if (hfzone.Value != "")
        {
            hysavecomp["ZoneID"] = Convert.ToInt32(hfzone.Value);
        }
        else
        {
            hysavecomp["ZoneID"] = Mid;
        }
        hysavecomp["ZoneName"] = txtzonename.Text.Trim();
        string str = Lo.SaveZone(hysavecomp, out _msg, out _sysMsg);
        if (str == "Save")
        {
            if (btnsub.Text != "Update")
            {
                cleartext();
                diverror.InnerHtml = "Zone save successfully";
                diverror.Attributes.Add("class", "alert alert-success");
            }
            else
            {
                cleartext();
                diverror.InnerHtml = "Zone update or save successfully";
                diverror.Attributes.Add("class", "alert alert-success");
                btnsub.Text = "Submit";
            }
        }
        else
        {
            diverror.InnerHtml = "Zone not save";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        if (txtzonename.Text != "")
        {
            System.Threading.Thread.Sleep(1000);
            SaveComp();
        }
        else
        {
            diverror.InnerHtml = "Fill Zone Name";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    protected void cleartext()
    {
        txtzonename.Text = "";
        hfzone.Value = "";
    }
    protected void update()
    {
        if (Request.QueryString["string"] != null)
        {
            hfzone.Value = Convert.ToInt64(Request.QueryString["string"]).ToString();
            DataTable DTComp = Lo.RetriveCodeZone(Convert.ToInt64(hfzone.Value));
            if (DTComp.Rows.Count > 0)
            {
                if (DTComp.Rows[0]["IsActive"].ToString() == "Y")
                {
                    txtzonename.Text = DTComp.Rows[0]["ZoneName"].ToString();
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