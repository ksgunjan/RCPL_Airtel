using System;
using BusinessLayer;
using System.Data;
using System.Collections.Specialized;

public partial class frmAddProductType : System.Web.UI.Page
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
        if (hfprodtype.Value != "")
        {
            hysavecomp["ProductTypeID"] = Convert.ToInt32(hfprodtype.Value);
        }
        else
        {
            hysavecomp["ProductTypeID"] = Mid;
        }
        hysavecomp["ProductTypeName"] = txtprodtypename.Text.Trim();
        string str = Lo.SaveProdType(hysavecomp, out _msg, out _sysMsg);
        if (str == "Save")
        {
            if (btnsub.Text != "Update")
            {
                cleartext();
                diverror.InnerHtml = "ProductType save successfully";
                diverror.Attributes.Add("class", "alert alert-success");
            }
            else
            {
                cleartext();
                diverror.InnerHtml = "ProductType update or save successfully";
                diverror.Attributes.Add("class", "alert alert-success");
                btnsub.Text = "Submit";
            }
        }
        else
        {
            diverror.InnerHtml = "ProductType not save";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        if (txtprodtypename.Text != "")
        {
            SaveComp();
        }
        else
        {
            diverror.InnerHtml = "Fill ProductType Name";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    protected void cleartext()
    {
        txtprodtypename.Text = "";
        hfprodtype.Value = "";

    }
    protected void update()
    {
        if (Request.QueryString["string"] != null)
        {
            hfprodtype.Value = Convert.ToInt64(Request.QueryString["string"]).ToString();
            DataTable DTComp = Lo.RetriveCodeProductType(Convert.ToInt64(hfprodtype.Value));
            if (DTComp.Rows.Count > 0)
            {
                if (DTComp.Rows[0]["IsActive"].ToString() == "Y")
                {
                    txtprodtypename.Text = DTComp.Rows[0]["ProductTypeName"].ToString();

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