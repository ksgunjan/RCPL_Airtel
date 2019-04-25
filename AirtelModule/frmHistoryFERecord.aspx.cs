using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;

public partial class frmHistoryFERecord : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Common Co = new Common();
    string HFRevisitDate;
    DataTable DtSearch = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Name"] != null)
            {
                Lo.verifyconnect();
                BindProdType();
                InsertRecordData();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
    #region SearchCode
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdate.Text != "" && txtenddate.Text != "")
            {
                DateTime Startdate = Convert.ToDateTime(txtdate.Text);
                DateTime EndDate = Convert.ToDateTime(txtenddate.Text);
                if (Startdate <= EndDate)
                {
                    InsertRecordData();
                }
                else
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = "End date could not less start date";
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
            }
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
        string date = "";
        DataRow dr;
        dr = insert.NewRow();
        dr["Column"] = "[Eligible/Not Eligible]" + "=";
        dr["Value"] = "'Eligible'";
        insert.Rows.Add(dr);
        if (ddlprod.SelectedItem.Text != "Select Product Type")
        {
            dr = insert.NewRow();
            dr["Column"] = "ProdTypeName" + "=";
            dr["Value"] = "'" + ddlprod.SelectedItem.Text + "'";
            insert.Rows.Add(dr);
        }
        if (Session["Name"].ToString() != null)
        {
            dr = insert.NewRow();
            dr["Column"] = "[FE Name]" + "=";
            dr["Value"] = "'" + Session["Name"].ToString() + "'";
            insert.Rows.Add(dr);
        }
        if (rbcheck.SelectedItem.Value == "Positive")
        {
            dr = insert.NewRow();
            dr["Column"] = "StatusByFE" + "=";
            dr["Value"] = "'Positive'";
            insert.Rows.Add(dr);
        }
        else if (rbcheck.SelectedItem.Value == "Negative")
        {
            dr = insert.NewRow();
            dr["Column"] = "StatusByFE" + "=";
            dr["Value"] = "'Negative'";
            insert.Rows.Add(dr);
        }
        else if (rbcheck.SelectedItem.Value == "Pending")
        {
            dr = insert.NewRow();
            dr["Column"] = "StatusByFE" + "=";
            dr["Value"] = "'Pending'";
            insert.Rows.Add(dr);
        }
        else if (rbcheck.SelectedItem.Value == "All")
        {
            dr = insert.NewRow();
            dr["Column"] = "StatusByFE" + "=";
            dr["Value"] = "'Pending' or StatusByFE='Positive' or StatusByFE='Negative'";
            insert.Rows.Add(dr);
        }
        if (txtdate.Text != "" && txtenddate.Text != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "[StatusUpdateDate]" + " between";
            dr["Value"] = "'" + Convert.ToDateTime(txtdate.Text).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(txtenddate.Text).ToString("dd/MMM/yyyy") + "'";
            insert.Rows.Add(dr);
        }
        return insert;
    }
    protected DataTable BindResult()
    {
        if (ddlsort.SelectedItem.Value != "01")
        {
            return Lo.AllSearchCode(this.SearchReocrd(), "tbl_trn_RawData" + ddlsort.SelectedValue + "");
        }
        else
        {
            return Lo.AllSearchCode(this.SearchReocrd(), "tbl_trn_RawData" + "[Logical Circuit Id]" + "");
        }

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
                    diverror.Visible = true;
                    diverror.InnerHtml = "Total  " + Dt1.Rows.Count + "  record found";
                    diverror.Attributes.Add("class", "alert alert-success");
                }
                else
                {
                    gvexcel.Visible = false;
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
    #endregion
    #region OtherCode
    protected void BindProdType()
    {
        DataTable DtBind = Lo.RetriveBindDDL("ProductTypeID", "ProductTypeName", "tbl_mst_ProductType where IsActive='Y'");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlprod, DtBind, "ProductTypeName", "ProductTypeID");
            ddlprod.Items.Insert(0, "Select Product Type");
            ddlprod.Enabled = true;
        }
    }
    protected void cleartext()
    {
        ddlprod.SelectedValue = "01";
        txtdate.Text = "";
        txtenddate.Text = "";
    }
    #endregion
    #region Gridview Command Code
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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                }
                else
                {
                    diverror.InnerHtml = "No record found.";
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
            }
        }
        catch (Exception ex)
        {
            diverror.InnerHtml = ex.Message;
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void gvexcel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblcompliancedate = e.Row.FindControl("lblcomp") as Label;
            DateTime compdate = Convert.ToDateTime(lblcompliancedate.Text);
            DateTime currentdate = Convert.ToDateTime(DateTime.Now);
            if (compdate < currentdate)
            {
                lblcompliancedate.ForeColor = System.Drawing.Color.Red;
            }
            Label FeStatus = e.Row.FindControl("hfstatusfe") as Label;
            Label HfRevisitDate = e.Row.FindControl("hfrevisitdate") as Label;
            if (HfRevisitDate.Text != "#")
            {
                DateTime RevisitDate = Convert.ToDateTime(HfRevisitDate.Text);
                HFRevisitDate = RevisitDate.ToString("dd/MMM/yyyy");
            }
            if (compdate < currentdate)
            {
                lblcompliancedate.ForeColor = System.Drawing.Color.Red;
            }
            if (FeStatus.Text == "Pending")
            {
                GridViewRow grv = e.Row;
                if (HFRevisitDate == DateTime.Now.ToString("dd/MMM/yyyy"))
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.Visible = true;
                    e.Row.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    HfRevisitDate.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
    #endregion
    #region SortCmmand
    protected void ddlsort_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsort.SelectedValue != "01")
        {
            InsertRecordData();
        }
        else
        {
            InsertRecordData();
        }
    }
    #endregion
}