using System;
using BusinessLayer;
using System.Data;
using System.Collections.Specialized;

public partial class frmAddProduct : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Common Co = new Common();
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
            Bind();
        }
    }
    protected void SaveComp()
    {
        if (hfproduct.Value != "")
        {
            hysavecomp["ProductId"] = Convert.ToInt32(hfproduct.Value);
        }
        else
        {
            hysavecomp["ProductId"] = Mid;
        }
        hysavecomp["ProductName"] = txtproductname.Text.Trim();
        hysavecomp["CompId"] = ddlcomp.SelectedItem.Value;
        hysavecomp["CompName"] = ddlcomp.SelectedItem.Text.Trim();
        string str = Lo.SaveProd(hysavecomp, out _msg, out _sysMsg);
        if (str == "Save")
        {
            if (btnsub.Text != "Update")
            {
                cleartext();
                diverror.InnerHtml = "Product save successfully";
                diverror.Attributes.Add("class", "alert alert-success");
            }
            else
            {
                cleartext();
                diverror.InnerHtml = "Product update or save successfully";
                diverror.Attributes.Add("class", "alert alert-success");
                btnsub.Text = "Submit";
            }
        }
        else
        {
            diverror.InnerHtml = "Product not save";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        if (txtproductname.Text != "")
        {
            System.Threading.Thread.Sleep(1000);
            SaveComp();
        }
        else
        {
            diverror.InnerHtml = "Fill Product Name";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    protected void cleartext()
    {
        txtproductname.Text = "";
        hfproduct.Value = "";
    }
    protected void update()
    {
        if (Request.QueryString["string"] != null)
        {
            hfproduct.Value = Convert.ToInt64(Request.QueryString["string"]).ToString();
            DataTable DTComp = Lo.RetriveCodeProduct(Convert.ToInt64(hfproduct.Value));
            if (DTComp.Rows.Count > 0)
            {
                if (DTComp.Rows[0]["IsActive"].ToString().Trim() == "Y")
                {
                    txtproductname.Text = DTComp.Rows[0]["ProductName"].ToString();
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
    protected void Bind()
    {
        DataTable DtBind = Lo.RetriveBindDDL("CompID", "CompName", "tbl_mst_Company where IsActive='Y'");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlcomp, DtBind, "CompName", "CompID");
        }
    }
}