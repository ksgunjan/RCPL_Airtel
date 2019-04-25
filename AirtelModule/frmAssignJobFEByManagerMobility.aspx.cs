using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;

public partial class frmAssignJobFEByManagerMobility : System.Web.UI.Page
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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (ddlfe.SelectedItem.Text != "" && ddlfe1.SelectedItem.Text != "")
        {
            try
            {
                foreach (GridViewRow row in gvexcel.Rows)
                {
                    HiddenField HfIDCompName = (HiddenField)row.FindControl("hdnIDGridRawData") as HiddenField;
                    if ((row.FindControl("chkRow") as CheckBox).Checked)
                    {
                        int UpdateNewFEtoLocation = Lo.UpdateCodeOther("tbl_trn_RawData", "Set [FE Name]='" + ddlfe.SelectedItem.Text + "'", "[Party Name]='" + HfIDCompName.Value + "' and [Allocated Date]='" + txtallocationdate.Text + "' and ProductName='MO'");
                    }
                }
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
    protected void bindcomp()
    {
        try
        {
            //string compname;
            //DataTable dt = new DataTable();
            //dt.Columns.Add("Billed Ext Id", typeof(string));
            //dt.Columns.Add("Party Name", typeof(string));
            //dt.Columns.Add("Customer Name", typeof(string));
            //dt.Columns.Add("Cust Email", typeof(string));
            //dt.Columns.Add("ProductName", typeof(string));
            //dt.Columns.Add("ProdTypeName", typeof(string));
            //dt.Columns.Add("FE Name", typeof(string));
            //dt.Columns.Add("Allocated Date", typeof(DateTime));
            //dt.Columns.Add("ComplainceDate", typeof(DateTime));
            //DataTable DtFetchcompgropby = Lo.RetriveCodeWithContidion("select count([party name])as [Count] ,[party name],[Allocated Date],[ProdTypeName] from tbl_trn_RawData where IsClosed='N' and (StatusByFE='Pending' or StatusByFE is null or StatusByFE='') and [Eligible/Not Eligible]='Eligible' and ProductName='MO' and ExcelVerifiy='2'  and [FE Name]='" + ddlfe1.SelectedItem.Text.Trim() + "' group by [party name],[Allocated Date],[ProdTypeName]");
            DataTable DtFetchcompgropby = Lo.RetriveCodeWithContidion("select * from fn_AssignJobFeByManagerMO('MO','" + ddlfe1.SelectedItem.Text + "')");
            if (DtFetchcompgropby.Rows.Count > 0)
            {
                //for (int i = 0; DtFetchcompgropby.Rows.Count > i; i++)
                //{
                //    compname = DtFetchcompgropby.Rows[i]["Party Name"].ToString();
                //    DataTable DtAssignJob = new DataTable();
                //    if (txtbillcity.Text != "")
                //    {
                //        DtAssignJob = Lo.RetriveCodeWithContidion("SELECT  top 1  [Billed Ext Id],[Party Name],[Customer Name],[Cust Email],ProductName,ProdTypeName,[FE Name],[Allocated Date],ComplainceDate from tbl_trn_RawData where IsActive='Y' and IsClosed !='Y' and ExcelVerifiy='2' and (StatusByFE='Pending' or StatusByFE is null or StatusByFE='') and [Eligible/Not Eligible]='Eligible' and ProductName='MO' and [FE Name]='" + ddlfe1.SelectedItem.Text.Trim() + "' and [Party Name]= '" + compname + "' and Bill_City like '%" + txtbillcity.Text + "%'");
                //    }
                //    else
                //    {
                //        DtAssignJob = Lo.RetriveCodeWithContidion("SELECT  top 1  [Billed Ext Id],[Party Name],[Customer Name],[Cust Email],ProductName,ProdTypeName,[FE Name],[Allocated Date],ComplainceDate from tbl_trn_RawData where IsActive='Y' and IsClosed !='Y' and ExcelVerifiy='2' and (StatusByFE='Pending' or StatusByFE is null or StatusByFE='') and [Eligible/Not Eligible]='Eligible' and ProductName='MO' and [FE Name]='" + ddlfe1.SelectedItem.Text.Trim() + "' and [Party Name]= '" + compname + "'");
                //    }
                //    foreach (DataRow dr in DtAssignJob.Rows)
                //    {
                //        object[] row = dr.ItemArray;
                //        dt.Rows.Add(row);
                //    }
                //}
                if (DtFetchcompgropby.Rows.Count > 0)
                {
                    gvexcel.DataSource = DtFetchcompgropby;
                    gvexcel.DataBind();
                    gvexcel.Visible = true;
                    diverror.Visible = true;
                    diverror.InnerHtml = "Total Record Found:- " + DtFetchcompgropby.Rows.Count;
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
        catch (Exception ex)
        {
            diverror.Visible = true;
            diverror.InnerHtml = "No record found to assign FE";
            diverror.Attributes.Add("class", "alert alert-danger");
        }

    }
    #region SearchCode
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (txtallocationdate.Text != "" && ddlfe1.SelectedItem.Text != "Select Field Executive" || txtbillcity.Text != "")
        {
            bindcomp();
        }
        else
        {
            diverror.Visible = true;
            diverror.InnerHtml = "Please select allocated Date or fe name field to search record";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    #endregion
}
