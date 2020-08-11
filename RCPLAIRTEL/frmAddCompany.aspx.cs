using BuisnessLayer;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmAddCompany : System.Web.UI.Page
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
    protected void bindgrid()
    {
        DataTable DTGrid = Lo.RetriveCodeLocal(0, "", "", "", "RetriveCompany"); ;
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
                bindgrid();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Company save successfully')", true);

            }
            else
            {
                cleartext();
                bindgrid();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Company save successfully')", true);
                btnsub.Text = "Submit";
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Company not save')", true);
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Fill Company Name')", true);
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
        DataTable DTComp = Lo.RetriveCodeLocal(Convert.ToInt64(hfcomp.Value), "", "", "", "RetriveCompanyById");
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record is deactive please active first for update.')", true);
            }
        }
    }
    protected void gvcomp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "up")
        {
            hfcomp.Value = e.CommandArgument.ToString();
            update();
        }
        else if (e.CommandName == "active")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string CompName = (gvcomp.Rows[rowIndex].FindControl("lbisacti") as Label).Text;
            if (CompName.ToString() == "Active")
            {
                string Update = Lo.RetriveCodeLocalup(Convert.ToInt64(e.CommandArgument.ToString()), "N", "", "", "UCStatus");
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
                string Update = Lo.RetriveCodeLocalup(Convert.ToInt64(e.CommandArgument.ToString()), "Y", "", "", "UCStatus");
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