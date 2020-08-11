using BuisnessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmHistoryFERecord : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private Common Co = new Common();
    private string HFRevisitDate;
    private DataTable DtSearch = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Name"] != null)
            {
                BindProdType();
                InsertRecordData();
            }
            else
            {
                Response.Redirect("Login");
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('End date could not less start date')", true);
                }
            }
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
        dr = insert.NewRow();
        dr["Column"] = "[Eligible_Not_Eligible]" + "=";
        dr["Value"] = "'Eligible'";
        insert.Rows.Add(dr);
        if (ddlprod.SelectedItem.Text != "Select Product Type")
        {
            dr = insert.NewRow();
            dr["Column"] = "Prod_Type_Name" + "=";
            dr["Value"] = "'" + ddlprod.SelectedItem.Text + "'";
            insert.Rows.Add(dr);
        }
        if (Session["Name"].ToString() != null)
        {
            dr = insert.NewRow();
            dr["Column"] = "[FE_Name]" + "=";
            dr["Value"] = "'" + Session["Name"].ToString() + "'";
            insert.Rows.Add(dr);
        }
        if (rbcheck.SelectedItem.Value == "Positive")
        {
            dr = insert.NewRow();
            dr["Column"] = "Status_By_FE" + "=";
            dr["Value"] = "'Positive'";
            insert.Rows.Add(dr);
        }
        else if (rbcheck.SelectedItem.Value == "Negative")
        {
            dr = insert.NewRow();
            dr["Column"] = "Status_By_FE" + "=";
            dr["Value"] = "'Negative'";
            insert.Rows.Add(dr);
        }
        else if (rbcheck.SelectedItem.Value == "Pending")
        {
            dr = insert.NewRow();
            dr["Column"] = "Status_By_FE" + "=";
            dr["Value"] = "'Pending'";
            insert.Rows.Add(dr);
        }
        else if (rbcheck.SelectedItem.Value == "All")
        {
            dr = insert.NewRow();
            dr["Column"] = "Status_By_FE" + "=";
            dr["Value"] = "'Pending' or Status_By_FE='Positive' or Status_By_FE='Negative'";
            insert.Rows.Add(dr);
        }
        if (txtdate.Text != "" && txtenddate.Text != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "[Status_Update_Date]" + " between";
            dr["Value"] = "'" + Convert.ToDateTime(txtdate.Text).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(txtenddate.Text).ToString("dd/MMM/yyyy") + "'";
            insert.Rows.Add(dr);
        }
        return insert;
    }
    protected DataTable BindResult()
    {
        return Lo.RetriveFEJobHistroy(SearchReocrd(), "", ddlprod.SelectedItem.Text, "", "", "SearchCode");
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
                    lbltotal.Text = "Total Row:- " + Dt1.Rows.Count.ToString();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop(Total  " + Dt1.Rows.Count + "  record found')", true);
                }
                else
                {
                    lbltotal.Text = "Total Row:- " + Dt1.Rows.Count.ToString();
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
    #endregion
    #region OtherCode
    protected void BindProdType()
    {
        DataTable DtBind = Lo.RetriveCodeLocal(0, "", "", "", "RetriveProductType");
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
    protected void gvexcel_RowCreated(object sender, GridViewRowEventArgs e)
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