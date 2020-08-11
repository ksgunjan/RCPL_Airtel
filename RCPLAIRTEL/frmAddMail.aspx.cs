using BuisnessLayer;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmAddMail : System.Web.UI.Page
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
                bindgrid();
                cleartext();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Email save successfully')", true);
            }
            else
            {
                bindgrid();
                cleartext();
                btnsub.Text = "Submit";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Email update or save successfully')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Email not save')", true);
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Fill Email ID')", true);
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
            DataTable DTComp = Lo.RetriveCodeLocal(Convert.ToInt64(hfcomp.Value), "", "", "", "RetriveEmailById");
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record is deactive please active first for update.')", true);
                }
            }
        }
    }
    protected void bindgrid()
    {
        DataTable DTGrid = Lo.RetriveCodeLocal(0, "", "", "", "RetriveEmail");
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
                string Update = Lo.RetriveCodeLocalup(Convert.ToInt64(e.CommandArgument.ToString()), "N", "", "", "UEStatus");
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
                string Update = Lo.RetriveCodeLocalup(Convert.ToInt64(e.CommandArgument.ToString()), "Y", "", "", "UEStatus");
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
    protected void gvcomp_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
}