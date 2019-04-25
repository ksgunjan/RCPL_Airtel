using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;

public partial class ViweMasterFEjobHistory : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Common Co = new Common();
    DataTable DtSearch = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Name"] != null)
            {
                Lo.verifyconnect();
               // BindProdType();
                BindFE();
                //InsertRecordData();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
    #region SearchCode
    protected void btnsearchlastjob_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdate1.Text != "" && txtenddate1.Text != "") //&& ddlprod1.SelectedItem.Value != "0")
            {
                DateTime Startdate = Convert.ToDateTime(txtdate1.Text);
                DateTime EndDate = Convert.ToDateTime(txtenddate1.Text);
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
            else
            {
                diverror.Visible = true;
                diverror.InnerHtml = "Please select date";
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
    protected DataTable SearchReocrd()
    {
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        //if (ddlprod1.SelectedItem.Text != "Select Product Type")
        //{
        //    dr = insert.NewRow();
        //    dr["Column"] = "ProdTypeName" + "=";
        //    dr["Value"] = "'" + ddlprod1.SelectedItem.Text + "'";
        //    insert.Rows.Add(dr);
        //}
        if (ddlfe1.SelectedItem.Value != "Select Field Executive")
        {
            dr = insert.NewRow();
            dr["Column"] = "[FE Name]" + "=";
            dr["Value"] = "'" + ddlfe1.SelectedItem.Text + "'";
            insert.Rows.Add(dr);
        }
        if (ddlauditstatus.SelectedValue != "01")
        {
            if (ddlauditstatus.SelectedItem.Value == "02")
            {
            }
            else if (ddlauditstatus.SelectedItem.Value == "03")
            {
                dr = insert.NewRow();
                dr["Column"] = "StatusByFE" + "=";
                dr["Value"] = "'' or StatusByFE is null";
                insert.Rows.Add(dr);
            }
            else
            {
                dr = insert.NewRow();
                dr["Column"] = "StatusByFE" + "=";
                dr["Value"] = "'" + ddlauditstatus.SelectedItem.Value + "'";
                insert.Rows.Add(dr);
            }
        }
        if (txtdate1.Text != "" && txtenddate1.Text != "")
        {
            if (ddlauditstatus.SelectedItem.Value == "02" || ddlauditstatus.SelectedItem.Value == "03")
            {
                dr = insert.NewRow();
                dr["Column"] = "[Allocated Date]" + " between";
                dr["Value"] = " '" + Convert.ToDateTime(txtdate1.Text).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(txtenddate1.Text).ToString("dd/MMM/yyyy") + "'";
                insert.Rows.Add(dr);
            }
            else
            {
                dr = insert.NewRow();
                dr["Column"] = "StatusUpdateDate" + " between";
                dr["Value"] = " '" + Convert.ToDateTime(txtdate1.Text).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(txtenddate1.Text).ToString("dd/MMM/yyyy") + "'";
                insert.Rows.Add(dr);
            }
        }
        dr = insert.NewRow();
        dr["Column"] = "IsActive" + "=";
        dr["Value"] = "'Y'";
        insert.Rows.Add(dr);
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
                    gvfedetaillist.DataSource = Dt1;
                    gvfedetaillist.DataBind();
                    gvfedetaillist.Visible = true;
                    lbltotal.Text = "Total  " + Dt1.Rows.Count + "  record found";
                    divfejobdetail.Visible = true;
                    diverror.Visible = false;
                }
                else
                {
                    gvfedetaillist.Visible = false;
                    divfejobdetail.Visible = false;
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
    //protected void BindProdType()
    //{
    //    DataTable DtBind = Lo.RetriveBindDDL("ProductTypeID", "ProductTypeName", "tbl_mst_ProductType where IsActive='Y'");
    //    if (DtBind.Rows.Count > 0)
    //    {
    //        Co.FillComboBox(ddlprod1, DtBind, "ProductTypeName", "ProductTypeID");
    //        ddlprod1.Items.Insert(0, "Select Product Type");
    //        ddlprod1.Enabled = true;
    //    }
    //}
    protected void BindFE()
    {
        DataTable DtBind = Lo.RetriveBindDDL("EmpID", "Name", "tbl_mst_Employee where IsActive='Y' and Category='Field Executive'");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlfe1, DtBind, "Name", "EmpID");
            ddlfe1.Items.Insert(0, "Select Field Executive");
            ddlfe1.Enabled = true;
        }
    }
    protected void cleartext()
    {
        txtdate1.Text = "";
        ddlauditstatus.SelectedValue = "01";
        ddlfe1.SelectedItem.Value = "01";
        //ddlprod1.SelectedItem.Value = "01";
        ddlsort.SelectedItem.Value = "01";
    }
    #endregion
    //#region Gridview Command Code
    //protected void gvexcel_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        if (e.CommandName == "ViewDetail")
    //        {
    //            DataTable DtSearchView = Lo.RetriveCodeAllExcelRecordWhereCondition("UniqueID =" + Convert.ToInt32(e.CommandArgument.ToString()) + "");
    //            if (DtSearchView.Rows.Count > 0)
    //            {
    //                dluploadexcelrecord.DataSource = DtSearchView;
    //                dluploadexcelrecord.DataBind();
    //                dluploadexcelrecord.Visible = true;
    //                divshow1.Visible = true;
    //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
    //            }
    //            else
    //            {
    //                diverror.InnerHtml = "No record found.";
    //                diverror.Attributes.Add("class", "alert alert-danger");
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        diverror.InnerHtml = ex.Message;
    //        diverror.Attributes.Add("class", "alert alert-danger");
    //    }
    //}
    //protected void gvexcel_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        Label lblcompliancedate = e.Row.FindControl("lblcomp") as Label;
    //        DateTime compdate = Convert.ToDateTime(lblcompliancedate.Text);
    //        DateTime currentdate = Convert.ToDateTime(DateTime.Now);
    //        if (compdate < currentdate)
    //        {
    //            lblcompliancedate.ForeColor = System.Drawing.Color.Red;
    //        }
    //        Label FeStatus = e.Row.FindControl("hfstatusfe") as Label;
    //      //  Label HfRevisitDate = e.Row.FindControl("hfrevisitdate") as Label;
    //        //if (HfRevisitDate.Text != "#")
    //        //{
    //        //    DateTime RevisitDate = Convert.ToDateTime(HfRevisitDate.Text);
    //        //    HFRevisitDate = RevisitDate.ToString("dd/MMM/yyyy");
    //        //}
    //        if (compdate < currentdate)
    //        {
    //            lblcompliancedate.ForeColor = System.Drawing.Color.Red;
    //        }
    //        if (FeStatus.Text == "Pending")
    //        {
    //            GridViewRow grv = e.Row;
    //            //if (HFRevisitDate == DateTime.Now.ToString("dd/MMM/yyyy"))
    //            //{
    //            //    e.Row.BackColor = System.Drawing.Color.Red;
    //            //    e.Row.Visible = true;
    //            //    e.Row.ForeColor = System.Drawing.Color.White;
    //            //}
    //            //else
    //            //{
    //            //    HfRevisitDate.ForeColor = System.Drawing.Color.Red;
    //            //}
    //        }
    //    }
    //}
    //#endregion
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