using BuisnessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViweMasterFEjobHistory : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private Common Co = new Common();
    private DataTable DtSearch = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Name"] != null)
            {
                BindFE();
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
            if (txtdate1.Text != "" && txtenddate1.Text != "")
            {
                DateTime Startdate = Convert.ToDateTime(txtdate1.Text);
                DateTime EndDate = Convert.ToDateTime(txtenddate1.Text);
                if (Startdate <= EndDate)
                {
                    InsertRecordData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('End date could not less start date')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Please select date')", true);
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

        if (ddlfe1.SelectedItem.Value != "Select Field Executive")
        {
            dr = insert.NewRow();
            dr["Column"] = "[FE_Name]" + "=";
            dr["Value"] = "'" + ddlfe1.SelectedItem.Text + "'";
            insert.Rows.Add(dr);
        }
        dr = insert.NewRow();
        dr["Column"] = "Product_Name" + "=";
        dr["Value"] = "'" + ddlproduct.SelectedItem.Text + "'";
        insert.Rows.Add(dr);
        if (ddlauditstatus.SelectedValue != "01")
        {
            if (ddlauditstatus.SelectedItem.Value == "02")
            {
            }
            else if (ddlauditstatus.SelectedItem.Value == "03")
            {
                dr = insert.NewRow();
                dr["Column"] = "Status_By_FE" + "=";
                dr["Value"] = "'' or StatusByFE is null";
                insert.Rows.Add(dr);
            }
            else
            {
                dr = insert.NewRow();
                dr["Column"] = "Status_By_FE" + "=";
                dr["Value"] = "'" + ddlauditstatus.SelectedItem.Value + "'";
                insert.Rows.Add(dr);
            }
        }
        if (txtdate1.Text != "" && txtenddate1.Text != "")
        {
            if (ddlauditstatus.SelectedItem.Value == "02" || ddlauditstatus.SelectedItem.Value == "03")
            {
                dr = insert.NewRow();
                dr["Column"] = "[Allocated_Date]" + " between";
                dr["Value"] = " '" + Convert.ToDateTime(txtdate1.Text).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(txtenddate1.Text).ToString("dd/MMM/yyyy") + "'";
                insert.Rows.Add(dr);
            }
            else
            {
                dr = insert.NewRow();
                dr["Column"] = "Status_Update_Date" + " between";
                dr["Value"] = " '" + Convert.ToDateTime(txtdate1.Text).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(txtenddate1.Text).ToString("dd/MMM/yyyy") + "'";
                insert.Rows.Add(dr);
            }
        }
        dr = insert.NewRow();
        dr["Column"] = "Is_Active" + "=";
        dr["Value"] = "'Y'";
        insert.Rows.Add(dr);
        return insert;
    }
    protected DataTable BindResult()
    {
        return Lo.MasterJobHistoryFE(SearchReocrd(), ddlproduct.SelectedItem.Value, "", "BindGrid");
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
                    gvfedetaillist.DataSource = Dt1;
                    gvfedetaillist.DataBind();
                    gvfedetaillist.Visible = true;
                    lbltotal.Text = "Total  " + Dt1.Rows.Count + "  record found";
                  
                }
                else
                {
                    gvfedetaillist.Visible = false;
                   
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
    protected void BindFE()
    {
        DataTable DtBind = Lo.RetriveCodeLocal(0, "Field Executive", "", "", "RetriveUserNormalType");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlfe1, DtBind, "Name", "EmpID");
            ddlfe1.Items.Insert(0, "Select Field Executive");
        }
    }
    protected void cleartext()
    {
        txtdate1.Text = "";
        ddlauditstatus.SelectedValue = "01";
        ddlfe1.SelectedItem.Value = "01";
       
    }
    #endregion
    protected void gvfedetaillist_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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