using BuisnessLayer;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmAddProduct : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private Common Co = new Common();
    private HybridDictionary hysavecomp = new HybridDictionary();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    private int Mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
            bindgrid();
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
                bindgrid();
                cleartext();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Product save successfully')", true);
            }
            else
            {
                bindgrid();
                cleartext();
                btnsub.Text = "Submit";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Product update or save successfully')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Product not save')", true);
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Fill Product Name')", true);
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
    protected void Bind()
    {
        DataTable DtBind = Lo.RetriveCodeLocal(0, "", "", "", "RetriveCompany");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlcomp, DtBind, "CompName", "CompID");
        }
    }
    protected void bindgrid()
    {
        DataTable DTGrid = Lo.RetriveCodeLocal(0, "", "", "", "RetriveProduct");
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
            string ProdName = (gvcomp.Rows[rowIndex].FindControl("lblprodname") as Label).Text;
            hfproduct.Value = e.CommandArgument.ToString();
            if (CompName == "Active")
            {
                txtproductname.Text = ProdName.ToString();
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
            if (CompName.ToString() == "Active")
            {
                string Update = Lo.RetriveCodeLocalup(Convert.ToInt64(e.CommandArgument.ToString()), "N", "", "", "UPStatus");
                if (Update == "true")
                {
                    bindgrid();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Record deactive successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not update')", true);
                }
            }
            else
            {
                string Update = Lo.RetriveCodeLocalup(Convert.ToInt64(e.CommandArgument.ToString()), "Y", "", "", "UPStatus");
                if (Update == "true")
                {
                    bindgrid();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Record active successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not update')", true);
                }
            }

        }
    }
}