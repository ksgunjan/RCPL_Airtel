using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;

public partial class frmUpdateStatusSubmitManager : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Common Co = new Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            BindProdType();
        }
    }
    protected void gvexcel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewDetail")
            {
                DataTable DtSearchView = Lo.RetriveCodeAllExcelRecordWhereCondition("UniqueID =" + Convert.ToInt32(e.CommandArgument.ToString()) + "");
                if (DtSearchView.Rows.Count > 0)
                {
                    dluploadexcelrecord.DataSource = DtSearchView;
                    dluploadexcelrecord.DataBind();
                    dluploadexcelrecord.Visible = true;
                    divshow1.Visible = true;
                    divupdatebyfe.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                }
                else
                {
                    diverror.InnerHtml = "No record found.";
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
            }
            else if (e.CommandName == "UpdateStatus")
            {
                divshow1.Visible = false;
                divupdatebyfe.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
            }
        }
        catch (Exception ex)
        {
            diverror.InnerHtml = ex.Message;
            diverror.Attributes.Add("class", "alert alert-danger");
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
            diverror.Visible = true;
            diverror.InnerHtml = ex.Message;
            diverror.Attributes.Add("class", "alert alert-danger");
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
            dr["Column"] = "RecievedDate between ";
            dr["Value"] = "'" + date + "' and '" + date1 + "'";
            insert.Rows.Add(dr);
        }
        if (ddlprodtype.SelectedItem.Text != "Select Product Type")
        {
            dr = insert.NewRow();
            dr["Column"] = "ProdTypeName" + "=";
            dr["Value"] = "'" + ddlprodtype.SelectedItem.Text + "'";
            insert.Rows.Add(dr);
        }
        dr = insert.NewRow();
        dr["Column"] = "[Eligible/Not Eligible]" + "=";
        dr["Value"] = "'Eligible'";
        insert.Rows.Add(dr);

        dr = insert.NewRow();
        dr["Column"] = "StatusByFE" + "=";
        dr["Value"] = "'Positive' or StatusByFE='Negative' or StatusByFE is null or StatusByFE='' ";
        insert.Rows.Add(dr);


        return insert;
    }
    protected DataTable BindResult()
    {
        return Lo.AllSearchCode(this.SearchReocrd(), "tbl_trn_RawData" + "[Logical Circuit Id]" + "");
    }
    protected void InsertRecordData()
    {
        try
        {
            DataTable Dt1 = this.BindResult();
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
                    diverror.Visible = true;
                    diverror.InnerHtml = "No record found";
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
            }
            catch (Exception ex)
            {
                diverror.Visible = true;
                diverror.InnerHtml = ex.Message;
                diverror.Attributes.Add("class", "alert alert-danger");
            }
        }
        catch (Exception ex)
        {
            diverror.Visible = true;
            diverror.InnerHtml = ex.Message;
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void BindProdType()
    {
        DataTable DtBind = Lo.RetriveBindDDL("ProductTypeID", "ProductTypeName", "tbl_mst_ProductType where IsActive='Y'");
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
                        int UpdateStatus = Lo.UpdateCodeOther("tbl_trn_RawData", "Set IsScanned='" + ddlstatus.SelectedItem.Value + "',ScannedDate='" + IsScannedDate + "',IsSubmitted='" + ddlissubmited.SelectedItem.Value + "',SubmittedDate='" + IsSubmittedDate + "'", "[LOC Sr No]='" + HfLocValue.Value + "'");
                        if (UpdateStatus != 0)
                        {
                            cleartext();
                            var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Status Update Successfully");
                            var script = string.Format("alert({0});window.location ='UpdateStatus-ByManager';", message);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                        }
                        else
                        {
                            var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Status not Update");
                            var script = string.Format("alert({0});window.location ='UpdateStatus-ByManager';", message);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ex.Message);
                var script = string.Format("alert({0});window.location ='UpdateStatus-ByManager';", message);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
            }
        }
        else
        {
            var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("All fields fill mandotory");
            var script = string.Format("alert({0});window.location ='UpdateStatus-ByManager';", message);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
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