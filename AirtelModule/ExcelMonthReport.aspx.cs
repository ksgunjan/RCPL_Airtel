using System;
using BusinessLayer;
using System.Data;
using ClosedXML.Excel;
using System.IO;

public partial class ExcelMonthReport : System.Web.UI.Page
{
    Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        ddlproduct.Text = "All";
        txtdateend.Text = "";
        txtdatestart.Text = "";
    }
    #region SearchCode
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdatestart.Text != "" && txtdateend.Text != "") //&& ddlprod1.SelectedItem.Value != "0")
            {
                DateTime Startdate = Convert.ToDateTime(txtdatestart.Text);
                DateTime EndDate = Convert.ToDateTime(txtdateend.Text);
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
        if (ddlproduct.SelectedItem.Value != "All")
        {
            dr = insert.NewRow();
            dr["Column"] = "ProductName" + "=";
            dr["Value"] = "'" + ddlproduct.SelectedItem.Text + "'";
            insert.Rows.Add(dr);
        }
        if (txtdatestart.Text != "" && txtdateend.Text != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "StatusUpdateDate" + " between";
            dr["Value"] = " '" + Convert.ToDateTime(txtdatestart.Text).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(txtdateend.Text).ToString("dd/MMM/yyyy") + "'";
            insert.Rows.Add(dr);
        }
        dr = insert.NewRow();
        dr["Column"] = "IsActive" + "=";
        dr["Value"] = "'Y'";

        insert.Rows.Add(dr);
        return insert;
    }
    protected DataTable BindResult()
    {
        return Lo.AllSearchCodeExcel(this.SearchReocrd(), "tbl_trn_RawData" + "StatusUpdateDate" + "");
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
                    int[] iColumns = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                    RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
                    objExport.ExportDetails(Dt1, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Month wise excel detail.xls");
                    diverror.Visible = true;
                    diverror.InnerHtml = "Total record download in excel" + Dt1.Rows.Count;
                    diverror.Attributes.Add("class", "alert alert-success");
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
    #endregion
}