using BuisnessLayer;
using System;
using System.Data;
using System.Web.UI;

public partial class frmUploadExcel : System.Web.UI.Page
{
    #region Variable
    private DataTable dtgridrecord = new DataTable();
    private DataTable Dt = new DataTable();
    private Logic Lo = new Logic();
    private Common Co = new Common();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    private string exMsg = string.Empty;
    private int countselect;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCompany();
            BindCompProd();
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
    }
    #region DropDownList Code"
    protected void BindCompany()
    {
        DataTable DtBind = Lo.RetriveCodeLocal(0, "", "", "", "RetriveCompany");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlcomp, DtBind, "CompName", "CompId");
            ddlcomp.Items.Insert(0, "Select Company");
        }
    }
    protected void BindCompProd()
    {
        DataTable DtBind = Lo.RetriveCodeLocal(0, "", "", "", "RetriveProduct");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlproduct, DtBind, "ProductName", "ProductId");
            ddlproduct.Items.Insert(0, "Select Product");
            BindProdType();
            ddlproduct.Enabled = true;
            ddlproducttype.Enabled = true;
        }
    }
    protected void BindProdType()
    {
        DataTable DtBind = Lo.RetriveCodeLocal(0, "", "", "", "RetriveProductType");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlproducttype, DtBind, "ProductTypeName", "ProductTypeID");
            ddlproducttype.Items.Insert(0, "Select Product Type");
        }
    }
    #endregion
    #region Upload Excel Code New
    private string str;
    protected void btnexcel_Click(object sender, EventArgs e)
    {
        if (ddlcomp.SelectedItem.Text != "Select Company" && ddlproduct.SelectedItem.Text != "Select Product" && ddlproducttype.SelectedItem.Text != "Select Product Type")
        {
            if (chklist.SelectedValue != "0")
            {
                for (int x = 0; x < chklist.Items.Count; x++)
                {
                    if (chklist.Items[x].Selected)
                    {
                        countselect = x + 1;
                    }
                }
                if (countselect == 10)
                {
                    if (fuexcel.FileName == null && fuexcel.FileName == "")
                    {
                        msgpopup.InnerHtml = "Please select Excel file.";
                        msgpopup.Attributes.Add("class", "alert alert-danger");
                        return;
                    }
                    else
                    {
                        string ErrText = "";
                        DataTable dtExcelRate = new DataTable();
                        try
                        {
                            string path = Server.MapPath("~/App_Data/") + fuexcel.PostedFile.FileName;
                            fuexcel.SaveAs(path);
                            dtExcelRate = Lo.CreateExcelConnection(path, "Sheet1", out ErrText).Tables[0];
                        }
                        catch (Exception ex)
                        {
                            msgpopup.InnerHtml = ex.Message;
                            msgpopup.Attributes.Add("class", "alert alert-danger");
                        }
                        if (dtExcelRate.Rows.Count < 1)
                        {
                            msgpopup.InnerHtml = "No data imported from excel file !!" + ErrText.ToString();
                            msgpopup.Attributes.Add("class", "alert alert-danger");
                            return;
                        }
                        else
                        {
                            try
                            {
                                int rowsCount = Convert.ToInt32(dtExcelRate.Rows.Count);
                                try
                                {
                                    lblRowCount.InnerHtml = "Rows Processed:- " + rowsCount;
                                    str = Lo.SaveUploadExcelCompany(dtExcelRate, ddlcomp.SelectedItem.Text, ddlproduct.SelectedItem.Text, ddlproducttype.SelectedItem.Text);
                                    if (str == "Save")
                                    {
                                        msgpopup.InnerHtml = "Data imported successfully from excel file !!\n\nTotal Rows - " + dtExcelRate.Rows.Count.ToString();
                                        msgpopup.Attributes.Add("class", "alert alert-success");
                                        dtExcelRate.Dispose();
                                        dtExcelRate = null;
                                        dtExcelRate = new DataTable();
                                        chklist.ClearSelection();                                        
                                    }
                                    else
                                    {
                                        msgpopup.InnerHtml = "User Error: Error in excel data. Technical Error: " + str;
                                        msgpopup.Attributes.Add("class", "alert alert-danger");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    msgpopup.InnerHtml = "Please review your excel we have found error in excel row. Technical Error:- " + ex.Message.ToString();
                                    msgpopup.Attributes.Add("class", "alert alert-danger");
                                }
                            }
                            catch (Exception ex)
                            {
                                msgpopup.InnerHtml = "User Error: Error in excel data. Technical Error: " + ex.Message;
                                msgpopup.Attributes.Add("class", "alert alert-danger");
                            }
                        }
                    }
                }
                else
                {
                    chklist.ClearSelection();
                    msgpopup.InnerHtml = "Please select all checkbox to upload excel.";
                    msgpopup.Attributes.Add("class", "alert alert-danger");
                }
            }
            else
            {
                msgpopup.InnerHtml = "Please select checkbox to upload excel.";
                msgpopup.Attributes.Add("class", "alert alert-danger");
            }
        }
        else
        {
            msgpopup.InnerHtml = "Please select company product or product type with excel file.";
            msgpopup.Attributes.Add("class", "alert alert-danger");
        }
    }
    #endregion
}