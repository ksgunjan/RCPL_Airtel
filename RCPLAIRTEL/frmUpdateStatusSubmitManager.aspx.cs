using BuisnessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmUpdateStatusSubmitManager : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private Common Co = new Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindProdType();
        }
    }
    protected void gvexcel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "UpdateStatus")
            {
                divupdatebyfe.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        try
        {
            InsertRecordData();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
    }
    protected DataTable SearchReocrd()
    {
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        string date = "";

        if (txtdate.Text != "" && txtenddate.Text != "")
        {
            DateTime datetime = Convert.ToDateTime(txtdate.Text);
            date = datetime.ToString("dd/MMM/yyyy");
            DateTime datenew = Convert.ToDateTime(txtenddate.Text);
            string date1 = datenew.ToString("dd/MMM/yyyy");

            dr = insert.NewRow();
            dr["Column"] = "Allocated_Date between ";
            dr["Value"] = "'" + date + "' and '" + date1 + "'";
            insert.Rows.Add(dr);
        }
        if (ddlprodtype.SelectedItem.Text != "Select Product Type")
        {
            dr = insert.NewRow();
            dr["Column"] = "Prod_Type_Name" + "=";
            dr["Value"] = "'" + ddlprodtype.SelectedItem.Text + "'";
            insert.Rows.Add(dr);
        }
        dr = insert.NewRow();
        dr["Column"] = "[Eligible_Not_Eligible]" + "=";
        dr["Value"] = "'Eligible'";
        insert.Rows.Add(dr);

        dr = insert.NewRow();
        dr["Column"] = "Status_By_FE" + "=";
        dr["Value"] = "'Positive' or StatusByFE='Negative' or StatusByFE is null or StatusByFE='' ";
        insert.Rows.Add(dr);


        return insert;
    }
    protected DataTable BindResult()
    {
        if (ddlprodtype.SelectedItem.Text == "Leased Line-Initial" || ddlprodtype.SelectedItem.Text == "Leased Line-Periodic")
        {
            return Lo.MasterJobHistoryFE(SearchReocrd(), "Leased Line", "", "BindGridMM");
        }
        else
        {
            return Lo.MasterJobHistoryFE(SearchReocrd(), "PRI-Fixed Line", "", "BindGridMM");
        }
    }
    protected void InsertRecordData()
    {
        try
        {
            DataTable Dt1 = BindResult();
            try
            {
                if (Dt1.Rows.Count > 0)
                {
                    gvexcel.DataSource = Dt1;
                    gvexcel.Visible = true;
                    gvexcel.DataBind();
                    ViewState["GridViewData"] = Dt1;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No record found')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
    }
    protected void BindProdType()
    {
        DataTable DtBind = Lo.RetriveCodeLocal(0, "", "", "", "RetriveProductType");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlprodtype, DtBind, "ProductTypeName", "ProductTypeID");
            ddlprodtype.Items.Insert(0, "Select Product Type");
            ddlprodtype.Enabled = true;
        }
    }
    protected void btnsubmitstatus_Click(object sender, EventArgs e)
    {
        if (txtscanneddate.Text != "" && txtsubmiteddate.Text != "" && ddlissubmited.SelectedItem.Text != "Select Submitted Date" && ddlstatus.SelectedItem.Text != "Select Status")
        {
            try
            {
                DataTable DtChckGrid = (DataTable)ViewState["GridViewData"];
                for (int i = 0; DtChckGrid.Rows.Count > i; i++)
                {
                    CheckBox ChkSelect = (CheckBox)gvexcel.Rows[i].FindControl("chkRow");
                    HiddenField HfLocValue = (HiddenField)gvexcel.Rows[i].FindControl("hfLocID");
                    if (ChkSelect.Checked == true)
                    {
                        DateTime ScannedDate = Convert.ToDateTime(txtscanneddate.Text);
                        string IsScannedDate = ScannedDate.ToString("dd/MMM/yyyy");
                        DateTime SubmittedDate = Convert.ToDateTime(txtsubmiteddate.Text);
                        string IsSubmittedDate = ScannedDate.ToString("dd/MMM/yyyy");
                        string UpdateStatus = Lo.UpdateCode(ddlstatus.SelectedItem.Value, IsScannedDate, ddlissubmited.SelectedItem.Value, IsSubmittedDate, "", "", "", "", "", "", "Updatestatusmanager");
                        if (UpdateStatus == "true")
                        {
                            cleartext();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Status Update Successfully')", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Status not Update')", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('All fields fill mandotory')", true);
        }
    }
    protected void cleartext()
    {
        ddlissubmited.Text = "Select Submitted Date";
        ddlprodtype.Text = "Select Product Type";
        ddlstatus.Text = "Select Status";
        txtsubmiteddate.Text = "";
        txtscanneddate.Text = "";
    }
}