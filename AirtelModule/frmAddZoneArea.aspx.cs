using System;
using BusinessLayer;
using System.Data;
using System.Collections.Specialized;

public partial class frmAddZoneArea : System.Web.UI.Page
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
        if (hfzoneareaid.Value != "")
        {
            hysavecomp["ZoneAreaID"] = Convert.ToInt32(hfzoneareaid.Value);
        }
        else
        {
            hysavecomp["ZoneAreaID"] = Mid;
        }
        hysavecomp["ZoneArea"] = txtzoneareaname.Text.Trim();
        hysavecomp["ZoneAreaPinCode"] = txtzoneareapincode.Text.Trim();
        string str = Lo.SaveZoneArea(hysavecomp, out _msg, out _sysMsg);
        if (str == "Save")
        {
            if (btnsub.Text != "Update")
            {
                cleartext();
                diverror.InnerHtml = "ZoneArea save successfully";
                diverror.Attributes.Add("class", "alert alert-success");
            }
            else
            {
                cleartext();
                diverror.InnerHtml = "ZoneArea update or save successfully";
                diverror.Attributes.Add("class", "alert alert-success");
                btnsub.Text = "Submit";
            }
        }
        else
        {
            diverror.InnerHtml = "ZoneArea not save";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        if (txtzoneareaname.Text != "")
        {
            System.Threading.Thread.Sleep(1000);
            SaveComp();
        }
        else
        {
            diverror.InnerHtml = "Fill ZoneArea Name";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    protected void cleartext()
    {
        txtzoneareaname.Text = "";
        hfzoneareaid.Value = "";
        txtzoneareapincode.Text = "";
    }
    protected void update()
    {
        if (Request.QueryString["string"] != null)
        {
            hfzoneareaid.Value = Convert.ToInt64(Request.QueryString["string"]).ToString();
            DataTable DTComp = Lo.RetriveCodeZoneArea(Convert.ToInt64(hfzoneareaid.Value));
            if (DTComp.Rows.Count > 0)
            {
                if (DTComp.Rows[0]["IsActive"].ToString() == "Y")
                {
                    txtzoneareaname.Text = DTComp.Rows[0]["ZoneArea"].ToString();
                    txtzoneareapincode.Text = DTComp.Rows[0]["ZoneAreaPinCode"].ToString();
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