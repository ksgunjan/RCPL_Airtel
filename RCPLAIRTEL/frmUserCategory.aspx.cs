using BuisnessLayer;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmUserCategory : System.Web.UI.Page
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
                bindgrid();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('UserCategory save successfully')", true);
                cleartext();
            }
            else
            {
                bindgrid();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('UserCategory update or save successfully')", true);
                cleartext();
                btnsub.Text = "Submit";
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('UserCategory not save')", true);
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Fill category Name')", true);
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
    protected void bindgrid()
    {
        DataTable DTGrid = Lo.RetriveCodeLocal(0, "", "", "", "RetriveUType");
        if (DTGrid.Rows.Count > 0)
        {
            gvusercategory.DataSource = DTGrid;
            gvusercategory.DataBind();
            gvusercategory.Visible = true;
        }
        else
        {
            gvusercategory.Visible = false;
        }
    }
    protected void gvusercategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "up")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string CompName = (gvusercategory.Rows[rowIndex].FindControl("lbisacti") as Label).Text;
            if (CompName.ToString().Trim() == "Active")
            {
                txtusercategory.Text = (gvusercategory.Rows[rowIndex].FindControl("lblcat") as Label).Text;
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
            string CompName = (gvusercategory.Rows[rowIndex].FindControl("lbisacti") as Label).Text;

            if (CompName.ToString().Trim() == "Active")
            {
                string Update = Lo.RetriveCodeLocalup(Convert.ToInt64(e.CommandArgument.ToString()), "N", "", "", "UUStatus");
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
                string Update = Lo.RetriveCodeLocalup(Convert.ToInt64(e.CommandArgument.ToString()), "Y", "", "", "UUStatus");
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
