using BuisnessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmAssignJobFEByManagerPRI : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private Common Co = new Common();
    private DataTable dtgrid = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFE1();
        }
    }
    protected void BindFE1()
    {
        DataTable DtBind = Lo.RetriveCodeLocal(0, "Field Executive", "", "", "RetriveUserNormalType");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlfe1, DtBind, "Name", "EmpID");
            ddlfe1.Items.Insert(0, "Select Field Executive");
        }
    }
    protected void BindFE()
    {
        DataTable DtBind = Lo.RetriveCodeLocal(0, "Field Executive", "", "", "RetriveUserNormalType");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlfe, DtBind, "Name", "EmpID");
            ddlfe.Items.Insert(0, "Select Field Executive");
        }
    }
    protected void ddlfe1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlfe1.SelectedItem.Text != "Select Field Executive")
        {
            dtgrid = Lo.RetriveAssignJobFEByManager("PRI-Fixed Line", ddlfe1.SelectedItem.Text.Trim(), "BindGrid");
            if (dtgrid.Rows.Count > 0)
            {
                gvexcel.DataSource = dtgrid;
                gvexcel.DataBind();
                gvexcel.Visible = true;
                lbltotal.Text = "Total Record Found:-" + dtgrid.Rows.Count.ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Total Record Found: - " + dtgrid.Rows.Count + ")", true);
                BindFE();
                divfename.Visible = true;

            }
            else
            {
                lbltotal.Text = "Total Record Found:-" + dtgrid.Rows.Count.ToString();
                gvexcel.Visible = false;
                divfename.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No record found to assign FE')", true);
            }
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (ddlfe.SelectedItem.Text != "" && ddlfe1.SelectedItem.Text != "")
        {
            try
            {
                foreach (GridViewRow row in gvexcel.Rows)
                {
                    HiddenField HfID = (HiddenField)row.FindControl("hdnIDGridRawData") as HiddenField;
                    if ((row.FindControl("chkRow") as CheckBox).Checked)
                    {
                        string UpdateNewFEtoLocation = Lo.UpdateCode(ddlfe.SelectedItem.Text, HfID.Value, "", "", "", "", "", "", "", "", "UpdatePRIAJFBM");
                    }
                }
                string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Successfully FE Assign");
                string script = string.Format("alert({0});window.location ='AssignJob-FeByManager-PRI';", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Select data with FE DropDownList')", true);
        }
    }
}