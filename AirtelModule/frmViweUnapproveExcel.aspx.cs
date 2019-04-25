using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;

public partial class frmViweUnapproveExcel : System.Web.UI.Page
{
    Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
        }
    }
    protected void celartext()
    {
        txtenddate.Text = "";
        txtstartdate.Text = "";
        ddlcasestatus.SelectedValue = "01";
    }
    #region SearchCOde
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (txtenddate.Text != "" && txtstartdate.Text != "")
        {
            SeachResult();
        }
        else
        {
            gvexcel.Visible = false;
            diverror.Visible = true;
            diverror.InnerHtml = "Please select valid allocation date.";
            diverror.Attributes.Add("class", "alert alert-danger");
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
                dr["Column"] = "[Allocated Date]" + " between";
                dr["Value"] = "'" + txtstartdate.Text + "' and '" + txtenddate.Text + "'";
                insert.Rows.Add(dr);
            }
            if (ddlcasestatus.SelectedItem.Text != "Select Case")
            {
                if (ddlcasestatus.SelectedItem.Value == "05")
                {
                }
                else
                {
                    dr = insert.NewRow();
                    dr["Column"] = "[StatusByFE]" + "="; ;
                    dr["Value"] = "'" + ddlcasestatus.SelectedItem.Text + "'";
                    insert.Rows.Add(dr);
                }
            }
            dr = insert.NewRow();
            dr["Column"] = "IsActive=";
            dr["Value"] = "'N' and IsCLosed!='Y'";
            insert.Rows.Add(dr);
            return insert;
        }
        else
        {
            diverror.Visible = true;
            diverror.InnerHtml = "End Date could not be less then start date.";
            diverror.Attributes.Add("class", "alert alert-danger");
            return insert = null;
        }
    }
    protected DataTable BindInsert()
    {
        return Lo.AllSearchCode(this.insert(), "tbl_trn_RawData" + "[Logical Circuit Id]" + "");
    }
    protected void SeachResult()
    {
        try
        {
            DataTable DtSearchResult = this.BindInsert();
            if (DtSearchResult.Rows.Count > 0)
            {
                gvexcel.DataSource = DtSearchResult;
                gvexcel.DataBind();
                gvexcel.Visible = true;
                diverror.Visible = true;
                h4message.InnerHtml = "Click on view to show all detail of record.";
                diverror.InnerHtml = "Total record found:- " + DtSearchResult.Rows.Count.ToString();
                diverror.Attributes.Add("class", "alert alert-success");
            }
            else
            {
                celartext();
                gvexcel.Visible = false;
                diverror.Visible = true;
                diverror.InnerHtml = "No record found. Please select valid allocation date.";
                diverror.Attributes.Add("class", "alert alert-danger");
            }
        }
        catch (Exception ex)
        {
            diverror.InnerHtml = ex.Message.ToString();
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    #endregion
}