using BuisnessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmViewUploadExcel : System.Web.UI.Page
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
        ddlproduct.SelectedIndex = -1;
    }
    protected void gvexcel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewDetail")
            {
                DataTable DtSearchView = Lo.RetriveCodeRawDataPopup(Convert.ToInt64(e.CommandArgument.ToString()), "", "", DateTime.Now, "", DateTime.Now, "", "", "", "ViewRecordById");
                if (DtSearchView.Rows.Count > 0)
                {
                    dluploadexcelrecord.DataSource = DtSearchView;
                    dluploadexcelrecord.DataBind();
                    dluploadexcelrecord.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No record found.')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Please select valid allocation date and product name.')", true);
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
                dr["Column"] = "Allocated_Date" + " between";
                dr["Value"] = "'" + txtstartdate.Text + "' and '" + txtenddate.Text + "'";
                insert.Rows.Add(dr);
            }
            if (ddlproduct.SelectedItem.Text != "Select Product")
            {
                dr = insert.NewRow();
                dr["Column"] = "Product_Name" + " =";
                dr["Value"] = "'" + ddlproduct.SelectedItem.Text + "'";
                insert.Rows.Add(dr);
            }
            dr = insert.NewRow();
            dr["Column"] = "Is_CLosed";
            dr["Value"] = "!='Y'";
            insert.Rows.Add(dr);
            return insert;
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('End Date could not be less then start date.')", true);
            return insert = null;
        }
    }
    protected DataTable BindInsert()
    {
        if (ddlproduct.SelectedItem.Text == "MO")
        { return Lo.RetriveCodeRawDataGrid(insert(), 0, ddlproduct.SelectedItem.Text, "", DateTime.Now, "", DateTime.Now, "", "N", "", "BindGridMO"); }
        else
        {
            return Lo.RetriveCodeRawDataGrid(insert(), 0, ddlproduct.SelectedItem.Text, "", DateTime.Now, "", DateTime.Now, "", "N", "", "BindGrid");
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
                gvexcel.Visible = true;
                lbltotal.Text = "Total Record Found:- " + DtSearchResult.Rows.Count.ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Total record found:- " + DtSearchResult.Rows.Count.ToString() + "')", true);
            }
            else
            {
                gvexcel.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No record found. Please select valid allocation date.')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message.ToString() + "')", true);
        }
    }
    #endregion
}