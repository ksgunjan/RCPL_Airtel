using BuisnessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmViweUnapproveExcel : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void celartext()
    {
        txtenddate.Text = "";
        txtstartdate.Text = "";
        ddlproduct.SelectedValue = "01";
    }
    #region SearchCOde
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (txtenddate.Text != "" && txtstartdate.Text != "" && ddlproduct.SelectedItem.Text != "Select Product")
        {
            SeachResult();
        }
        else
        {
            gvexcel.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Please select valid allocation date.')", true);
        }
    }
    protected DataTable insert()
    {
        DateTime DateFirst = Convert.ToDateTime(txtstartdate.Text);
        DateTime DateEndDate = Convert.ToDateTime(txtenddate.Text);
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        if (DateEndDate >= DateFirst)
        {
            if (txtstartdate.Text != "" && txtenddate.Text != "")
            {
                dr = insert.NewRow();
                dr["Column"] = "Allocated_Date" + " between ";
                dr["Value"] = "'" + txtstartdate.Text + "' and '" + txtenddate.Text + "'";
                insert.Rows.Add(dr);
            }
            if (ddlproduct.SelectedItem.Text != "Select Product")
            {
                dr = insert.NewRow();
                dr["Column"] = "Product_Name" + "=";
                dr["Value"] = "'" + ddlproduct.SelectedItem.Text + "'";
                insert.Rows.Add(dr);
            }
            return insert;
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('End Date could not be less then start date.')", true);
            return insert = null;
        }
    }
    protected DataTable BindInsert()
    {
        if (ddlproduct.SelectedItem.Text != "MO")
        {
            return Lo.RetriveDataWithOnlyQuery(insert(), "", "BindGrid");
        }
        else
        {
            return Lo.RetriveDataWithOnlyQuery(insert(), "", "BindGridMO");
        }
    }
    protected void SeachResult()
    {
        try
        {
            DataTable DtSearchResult = BindInsert();
            if (DtSearchResult.Rows.Count > 0)
            {
                gvexcel.DataSource = DtSearchResult;
                gvexcel.DataBind();
                lbltot.Text = "Total Record Found:- " + DtSearchResult.Rows.Count.ToString();
                gvexcel.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "SuccessfullPop('Total record found:- " + DtSearchResult.Rows.Count.ToString() + "')", true);
            }
            else
            {
                lbltot.Text = "Total Record Found:- " + DtSearchResult.Rows.Count.ToString();
                celartext();
                gvexcel.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('No record found. Please select valid allocation date.')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('" + ex.Message.ToString() + "')", true);
        }
    }
    #endregion
    protected void gvexcel_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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