using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class frmAssignJobFEByManager : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Common Co = new Common();
    DataTable dtgrid = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            BindFE1();
        }
    }
    protected void BindFE1()
    {
        DataTable DtBind = Lo.RetriveBindDDL("EmpID", "Name", "tbl_mst_Employee where Category='Field Executive' and IsActive='Y'");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlfe1, DtBind, "Name", "EmpID");
            ddlfe1.Items.Insert(0, "Select Field Executive");
        }
    }
    protected void BindFE()
    {
        DataTable DtBind = Lo.RetriveBindDDL("EmpID", "Name", "tbl_mst_Employee where Category='Field Executive' and IsActive='Y'");
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
            dtgrid = Lo.RetriveCodeAllExcelRecordWhereCondition("IsClosed='N' and (StatusByFE='Pending' or StatusByFE is null or StatusByFE='') and [Eligible/Not Eligible]='Eligible' and ProductName='Leased Line' and [FE Name]='" + ddlfe1.SelectedItem.Text.Trim() + "'");
            if (dtgrid.Rows.Count > 0)
            {
                gvexcel.DataSource = dtgrid;
                gvexcel.DataBind();
                gvexcel.Visible = true;
                diverror.Visible = true;
                diverror.InnerHtml = "Total Record Found:- " + dtgrid.Rows.Count;
                diverror.Attributes.Add("class", "alert alert-success");
                BindFE();
                divfename.Visible = true;

            }
            else
            {
                gvexcel.Visible = false;
                divfename.Visible = false;
                diverror.Visible = true;
                diverror.InnerHtml = "No record found to assign FE";
                diverror.Attributes.Add("class", "alert alert-danger");
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
                        int UpdateNewFEtoLocation = Lo.UpdateCodeOther("tbl_trn_RawData", "Set [FE Name]='" + ddlfe.SelectedItem.Text + "'", "[Logical Circuit Id]='" + HfID.Value + "' and ProductName='Leased Line'");
                    }
                }
                ddlfe1_SelectedIndexChanged(sender, e);
                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Successfully FE Assign");
                var script = string.Format("alert({0});window.location ='Manager-Assign-FE-Job';", message);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
            }
            catch (Exception ex)
            {
                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ex.Message);
                var script = string.Format("alert({0});window.location ='Manager-Assign-FE-Job';", message);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
            }
        }
        else
        {
            var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Select data with FE DropDownList");
            var script = string.Format("alert({0});window.location ='Manager-Assign-FE-Job';", message);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
        }
    }
}