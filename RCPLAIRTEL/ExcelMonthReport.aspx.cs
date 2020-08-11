using BuisnessLayer;
using System;
using System.Data;
using System.Web.UI;

public partial class ExcelMonthReport : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        ddlproduct.SelectedValue = "1";
        txtdateend.Text = "";
        txtdatestart.Text = "";
    }
    #region SearchCode
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdatestart.Text != "" && txtdateend.Text != "")
            {
                DateTime Startdate = Convert.ToDateTime(txtdatestart.Text);
                DateTime EndDate = Convert.ToDateTime(txtdateend.Text);
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
        if (ddlproduct.SelectedItem.Text != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "Product_Name" + "=";
            dr["Value"] = "'" + ddlproduct.SelectedItem.Text + "'";
            insert.Rows.Add(dr);
        }
        if (txtdatestart.Text != "" && txtdateend.Text != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "Status_Update_Date" + " between";
            dr["Value"] = " '" + Convert.ToDateTime(txtdatestart.Text).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(txtdateend.Text).ToString("dd/MMM/yyyy") + "'";
            insert.Rows.Add(dr);
        }
        return insert;
    }
    protected DataTable BindResult()
    {
        return Lo.RetriveCodeRawDataGrid(SearchReocrd(), 0, ddlproduct.SelectedItem.Text, "", DateTime.Now, "", DateTime.Now, "", "", "", "DownExcel");
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
                    if (ddlproduct.SelectedItem.Text == "MO")
                    {
                        int[] iColumns = { 2, 35, 70, 12, 21, 24, 17, 28, 19, 45, 46, 47, 67, 68, 69, 73, 74, 75, 25, 3, 62, 63, 64, 22, 25, 53, 49, 48 };
                        RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
                        objExport.ExportDetails(Dt1, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Month wise excel detail.xls");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Total record download in excel" + Dt1.Rows.Count + "')", true);
                    }
                    else
                    {
                        int[] iColumns = { 2, 35, 70, 12, 21, 24, 17, 28, 19, 45, 46, 47, 67, 68, 69, 73, 74, 75, 25, 3, 62, 63, 64, 22, 25, 53, 49, 48 };
                        RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
                        objExport.ExportDetails(Dt1, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Month wise excel detail.xls");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Total record download in excel" + Dt1.Rows.Count + "')", true);
                    }
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
    #endregion
}