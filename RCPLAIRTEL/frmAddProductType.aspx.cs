using BuisnessLayer;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmAddProductType : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private HybridDictionary hysavecomp = new HybridDictionary();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    private int Mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindgrid();
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
                bindgrid();
                cleartext();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('ProductType save successfully')", true);
            }
            else
            {
                bindgrid();
                cleartext();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('ProductType update or save successfully')", true);
                btnsub.Text = "Submit";
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('ProductType not save')", true);
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Fill ProductType Name')", true);
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
    protected void bindgrid()
    {
        DataTable DTGrid = Lo.RetriveCodeLocal(0, "", "", "", "RetriveProductType");
        if (DTGrid.Rows.Count > 0)
        {
            gvcomp.DataSource = DTGrid;
            gvcomp.DataBind();
            gvcomp.Visible = true;
        }
        else
        {
            gvcomp.Visible = false;
        }
    }
    protected void gvcomp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "up")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string CompName = (gvcomp.Rows[rowIndex].FindControl("lbisacti") as Label).Text;
            string ProdTypeName = (gvcomp.Rows[rowIndex].FindControl("lblprodtype") as Label).Text;
            hfprodtype.Value = e.CommandArgument.ToString();
            if (CompName.ToString() == "Y")
            {
                txtprodtypename.Text = ProdTypeName.ToString();
                btnsub.Text = "Update";
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record is deactive please active first for update.')", true);
            }
        }
        else if (e.CommandName == "active")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string CompName = (gvcomp.Rows[rowIndex].FindControl("lbisacti") as Label).Text;
            if (CompName.ToString().Trim() == "Active")
            {
                string Update = Lo.RetriveCodeLocalup(Convert.ToInt64(e.CommandArgument.ToString()), "N", "", "", "UPTStatus");
                if (Update == "true")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Record deactive successfully')", true);
                    bindgrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not update')", true);
                }
            }
            else
            {
                string Update = Lo.RetriveCodeLocalup(Convert.ToInt64(e.CommandArgument.ToString()), "Y", "", "", "UPTStatus");
                if (Update == "true")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Record active successfully')", true);
                    bindgrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not update')", true);

                }
            }
        }
    }
}
