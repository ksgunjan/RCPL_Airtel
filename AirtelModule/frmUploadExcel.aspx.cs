using System;
using BusinessLayer;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web.UI;
using System.Data.OleDb;

public partial class frmUploadExcel : System.Web.UI.Page
{
    #region Variable
    // DataTable Exceldt;
    DataTable dtgridrecord = new DataTable();
    DataTable Dt = new DataTable();
    Logic Lo = new Logic();
    Common Co = new Common();
    string _msg = string.Empty;
    string _sysMsg = string.Empty;
    string exMsg = string.Empty;
    Int32 countselect;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            BindCompany();
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
    }
    #region DropDownList Code"
    protected void BindCompany()
    {

        DataTable DtBind = Lo.RetriveBindDDL("CompId", "CompName", "tbl_mst_Company where IsActive='Y'");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlcomp, DtBind, "CompName", "CompId");
            ddlcomp.Items.Insert(0, "Select Company");
            if (ddlcomp.SelectedItem.Text == "Select Company")
            {
                ddlproduct.Enabled = false;
                ddlproducttype.Enabled = false;
            }
        }

    }
    protected void BindCompProd()
    {
        if (ddlcomp.SelectedItem.Text != "Select Company")
        {
            DataTable DtBind = Lo.RetriveBindDDL(Convert.ToInt32(ddlcomp.SelectedItem.Value));
            if (DtBind.Rows.Count > 0)
            {
                Co.FillComboBox(ddlproduct, DtBind, "ProductName", "ProductId");
                ddlproduct.Items.Insert(0, "Select Product");
                BindProdType();
                ddlproduct.Enabled = true;
                ddlproducttype.Enabled = true;
            }
        }
        else
        {
            ddlproduct.Enabled = false;
            ddlproducttype.Enabled = false;
        }
    }
    protected void BindProdType()
    {
        DataTable DtBind = Lo.RetriveBindDDL("ProductTypeID", "ProductTypeName", "tbl_mst_ProductType where IsActive='Y'");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlproducttype, DtBind, "ProductTypeName", "ProductTypeID");
            ddlproducttype.Items.Insert(0, "Select Product Type");
        }
    }
    protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCompProd();
        ddlproducttype.Enabled = true;
    }
    #endregion
    #region Upload Excel Code New
    string str;
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
                        diverror.Visible = true;
                        diverror.InnerHtml = "Please select Excel file.";
                        diverror.Attributes.Add("class", "alert alert-danger");
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
                            diverror.Visible = true;
                            diverror.InnerHtml = ErrText;
                            diverror.Attributes.Add("class", "alert alert-danger");
                        }
                        if (dtExcelRate.Rows.Count < 1)
                        {
                            diverror.Visible = true;
                            diverror.InnerHtml = "No data imported from excel file !!";
                            diverror.Attributes.Add("class", "alert alert-danger");
                            return;
                        }
                        else
                        {
                            try
                            {
                                var rowsCount = Convert.ToInt32(dtExcelRate.Rows.Count);
                                // var recordToStartFetchFrom = 0;
                                // const int chunkSize = 2500;
                                // while (recordToStartFetchFrom <= rowsCount)
                                // {
                                // var diff = rowsCount - recordToStartFetchFrom;
                                //  int internalChunkSize = diff < 50000 ? diff : chunkSize;
                                try
                                {
                                    //  int sum = Convert.ToInt32(recordToStartFetchFrom + internalChunkSize);
                                    lblRowCount.InnerHtml = "Rows Processed:- " + rowsCount;//+ " Rows going to inserted:- "; //+ sum;
                                    //  lblRowCount.Attributes.Add("Class", "alert alert-warning");
                                    //   recordToStartFetchFrom += chunkSize;
                                    str = Lo.SaveUploadExcelCompany(dtExcelRate, ddlcomp.SelectedItem.Text, ddlproduct.SelectedItem.Text, ddlproducttype.SelectedItem.Text);
                                    if (str == "Save")
                                    {
                                        diverror.Visible = true;
                                        diverror.InnerHtml = "Data imported successfully from excel file !!\n\nTotal Rows - " + dtExcelRate.Rows.Count.ToString() + ".";
                                        diverror.Attributes.Add("class", "alert alert-success");
                                        dtExcelRate.Dispose();
                                        dtExcelRate = null;
                                        dtExcelRate = new DataTable();
                                        chklist.ClearSelection();
                                    }
                                    else
                                    {
                                        diverror.Visible = true;
                                        diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + str + "";
                                        diverror.Attributes.Add("class", "alert alert-danger");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    diverror.Visible = true;
                                    diverror.InnerHtml = "Please review your excel we have found error in excel row. Technical Error:- " + ex.Message.ToString(); //+ recordToStartFetchFrom + internalChunkSize + "";
                                    diverror.Attributes.Add("Class", "alert alert-danger");
                                    // break;
                                }
                            }
                            //if (str == "Save")
                            //{
                            //    diverror.Visible = true;
                            //    diverror.InnerHtml = "Data imported successfully from excel file !!\n\nTotal Rows - " + dtExcelRate.Rows.Count.ToString() + ".";
                            //    diverror.Attributes.Add("class", "alert alert-success");
                            //    dtExcelRate.Dispose();
                            //    dtExcelRate = null;
                            //    dtExcelRate = new DataTable();
                            //    chklist.ClearSelection();
                            //}
                            //else
                            //{
                            //    diverror.Visible = true;
                            //    diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + str + "";
                            //    diverror.Attributes.Add("class", "alert alert-danger");
                            //}
                            // }
                            catch (Exception ex)
                            {
                                diverror.Visible = true;
                                diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + ex.Message + "";
                                diverror.Attributes.Add("class", "alert alert-danger");
                            }
                        }
                    }
                }
                else
                {
                    chklist.ClearSelection();
                    diverror.InnerHtml = "Please select all checkbox to upload excel.";
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
            }
            else
            {
                diverror.InnerHtml = "Please select checkbox to upload excel.";
                diverror.Attributes.Add("class", "alert alert-danger");
            }
        }
        else
        {
            diverror.InnerHtml = "Please select company product or product type with excel file.";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    #endregion
    #region OLD Upload Excel Code
    //protected void btnexcel_Click(object sender, EventArgs e)
    //{
    //    if (ddlcomp.SelectedItem.Text != "Select Company" && ddlproduct.SelectedItem.Text != "Select Product" && ddlproducttype.SelectedItem.Text != "Select Product Type")
    //    {
    //        if (chklist.SelectedValue != "0")
    //        {
    //            for (int x = 0; x < chklist.Items.Count; x++)
    //            {
    //                if (chklist.Items[x].Selected)
    //                {
    //                    countselect = x + 1;
    //                }
    //            }
    //            if (countselect == 10)
    //            {
    //                ImportAirtelData(fuexcel.FileName);
    //            }
    //            else
    //            {
    //                chklist.ClearSelection();
    //                diverror.InnerHtml = "Please select all checkbox to upload excel.";
    //                diverror.Attributes.Add("class", "alert alert-danger");
    //            }
    //        }
    //        else
    //        {
    //            diverror.InnerHtml = "Please select checkbox to upload excel.";
    //            diverror.Attributes.Add("class", "alert alert-danger");
    //        }
    //    }
    //    else
    //    {
    //        diverror.InnerHtml = "Please select company product or product type with excel file.";
    //        diverror.Attributes.Add("class", "alert alert-danger");
    //    }
    //}
    //protected DataTable InsertReocrd(DataTable dtChunk)
    //{
    //    dtgridrecord = dtChunk;
    //    DataTable dtsearchfilter = new DataTable();
    //    if (dtgridrecord.Rows.Count > 0)
    //    {
    //        try
    //        {
    //            string FinalDateAllocDate = "";
    //            string FinalDateBillingTrigDate = "";
    //            string FinalDateCommDate = "";
    //            string FinalComplainceDate = "";
    //            string FinalDateLocCreDate = "";
    //            string FinalDateLocSubByPM = "";
    //            string FinalDateOrigDate = "";
    //            string FinalDateODDCompDate = "";
    //            string FinalRecieveDate = "";
    //            string FinalDateVerifictionDate = "";
    //            string FinalServiceStart = "";
    //            dtsearchfilter.Columns.Add(new DataColumn("Name", typeof(string)));
    //            dtsearchfilter.Columns.Add(new DataColumn("Value", typeof(string)));
    //            for (int i = 0; dtgridrecord.Rows.Count > i; i++)
    //            {
    //                if (dtgridrecord.Rows[i]["Allocated Date"].ToString() != "")
    //                {
    //                    DateTime DateAllocDate = Convert.ToDateTime(dtgridrecord.Rows[i]["Allocated Date"].ToString());
    //                    FinalDateAllocDate = DateAllocDate.ToString("dd/MMM/yyyy");
    //                }
    //                else
    //                {
    //                    FinalDateAllocDate = "";
    //                }
    //                if (dtgridrecord.Rows[i]["Billing Trig Date"].ToString() != "")
    //                {
    //                    DateTime DateBillingTrigDate = Convert.ToDateTime(dtgridrecord.Rows[i]["Billing Trig Date"].ToString());
    //                    FinalDateBillingTrigDate = DateBillingTrigDate.ToString("dd/MMM/yyyy");
    //                }
    //                else
    //                {
    //                    FinalDateBillingTrigDate = "";
    //                }
    //                if (dtgridrecord.Rows[i]["ComplainceDate"].ToString() != "")
    //                {
    //                    DateTime DateComplainceDate = Convert.ToDateTime(dtgridrecord.Rows[i]["ComplainceDate"].ToString());
    //                    FinalComplainceDate = DateComplainceDate.ToString("dd/MMM/yyyy");
    //                }
    //                else
    //                {
    //                    FinalComplainceDate = "";
    //                }
    //                if (dtgridrecord.Rows[i]["Commissioning Date"].ToString() != "")
    //                {
    //                    DateTime DateCommDate = Convert.ToDateTime(dtgridrecord.Rows[i]["Commissioning Date"].ToString());
    //                    FinalDateCommDate = DateCommDate.ToString("dd/MMM/yyyy");
    //                }
    //                else
    //                {
    //                    FinalDateCommDate = "";
    //                }
    //                if (dtgridrecord.Rows[i]["LOC Created Date By PM"].ToString() != "")
    //                {
    //                    DateTime DateLocCreDate = Convert.ToDateTime(dtgridrecord.Rows[i]["LOC Created Date By PM"].ToString());
    //                    FinalDateLocCreDate = DateLocCreDate.ToString("dd/MMM/yyyy");
    //                }
    //                else
    //                {
    //                    FinalDateLocCreDate = "";
    //                }
    //                if (dtgridrecord.Rows[i]["LOC Submiited Date By PM"].ToString() != "")
    //                {
    //                    DateTime DateLocSubByPM = Convert.ToDateTime(dtgridrecord.Rows[i]["LOC Submiited Date By PM"].ToString());
    //                    FinalDateLocSubByPM = DateLocSubByPM.ToString("dd/MMM/yyyy");
    //                }
    //                else
    //                {
    //                    FinalDateLocSubByPM = "";
    //                }
    //                if (dtgridrecord.Rows[i]["Orig Service Start Date"].ToString() != "")
    //                {
    //                    DateTime DateOrigServiceStartDate = Convert.ToDateTime(dtgridrecord.Rows[i]["Orig Service Start Date"].ToString());
    //                    FinalDateOrigDate = DateOrigServiceStartDate.ToString("dd/MMM/yyyy");
    //                }
    //                else
    //                {
    //                    FinalDateOrigDate = "";
    //                }
    //                if (dtgridrecord.Rows[i]["Overall DD Completion Date"].ToString() != "")
    //                {
    //                    DateTime DateODDCompDate = Convert.ToDateTime(dtgridrecord.Rows[i]["Overall DD Completion Date"].ToString());
    //                    FinalDateODDCompDate = DateODDCompDate.ToString("dd/MMM/yyyy");
    //                }
    //                else
    //                {
    //                    FinalDateODDCompDate = "";
    //                }
    //                if (dtgridrecord.Rows[i]["RecievedDate"].ToString() != "")
    //                {
    //                    DateTime DateRecievedDate = Convert.ToDateTime(dtgridrecord.Rows[i]["RecievedDate"].ToString());
    //                    FinalRecieveDate = DateRecievedDate.ToString("dd/MMM/yyyy");
    //                }
    //                else
    //                {
    //                    FinalRecieveDate = "";
    //                }
    //                if (dtgridrecord.Rows[i]["Service Start"].ToString() != "")
    //                {
    //                    DateTime DateServiceStart = Convert.ToDateTime(dtgridrecord.Rows[i]["Service Start"].ToString());
    //                    FinalServiceStart = DateServiceStart.ToString("dd/MMM/yyyy");
    //                }
    //                else
    //                {
    //                    FinalServiceStart = "";
    //                }
    //                if (dtgridrecord.Rows[i]["Verification Date"].ToString() != "")
    //                {
    //                    DateTime DateVerification = Convert.ToDateTime(dtgridrecord.Rows[i]["Verification Date"].ToString());
    //                    FinalDateVerifictionDate = DateVerification.ToString("dd/MMM/yyyy");
    //                }
    //                else
    //                {
    //                    FinalDateVerifictionDate = "";
    //                }
    //                #region Hide
    //                DataRow dr;
    //                dr = dtsearchfilter.NewRow();
    //                dr["Name"] = "[Account Caf No]," + "[Account Manager]," + "[Account Number]," + "[Alt Phone1]," + "[Alt Phone2]," + "Annotation,"
    //                 + "B2B_Email," + "B2B_Head_Email," + "B2B_Head_Name," + "[Bill Company]," + "[Bill Plan]," + "[Bill UOM]," + "Bill_City," + "[Billed Act Id],"
    //                    + "[Billed Ext Id]," + "[Billing Address]," + "BILLING_BANDWIDTH," + "BILLING_BANDWIDTH_UOM," + "Billing_Contact_Number," + "Billing_Email_Id," + "[Charge Name],"
    //                    + "[Circuit BandWidth]," + "[Company Name]," + "[Contact Phone1]," + "[Contact Phone2]," + "Coordinator_Contact_Email," + "Coordinator_Contact_Number," + "Coordinator_Name,"
    //                    + "[Cust account No]," + "[Cust Email]," + "[Customer Name]," + "[Customer Segment]," + "[Eligible/Not Eligible]," + "[External id type]," + "[From Site]," + "[FE Name],"
    //                    + "FX_ACCOUNT_EXTERNAL_ID," + "[Installation Address]," + "KAM_Contact_Number,"
    //                    + "KAM_Email," + "KAM_Name," + "[Line Item Description]," + "Line_Name," + "LOB,"
    //                    + "[LOC Status]," + "Location," + "[Location Secondary]," + "[Logical Circuit Id]," + "[Mkt Code]," + "MappingID,"
    //                    + "[Mobile No]," + "New_Connection_Type," + "[Num channel]," + "[Order Type]," + "[Party Name]," + "[Phone Nos],"
    //                    + "Pincode," + "[PO Number]," + "[POP 1]," + "[POP 2]," + "[Primary Address]," + "[Project Manager]," + "[Product Name],"
    //                    + "PRODUCT_TYPE," + "ProductName," + "ProdTypeName," + "RM_Contact_Number," + "RM_Email," + "RM_NAME,"
    //                    + "SAM_Contact_Number," + "SAM_Email," + "SAM_Name," + "SAM_TL," + "SAM_TL_Contact_Number," + "SAM_TL_EMAIL," + "[Secondary Address]," + "Segment," + "[Service City],"
    //                    + "[Service Name]," + "[Service Start]," + "Status," + "[Sub Product Name]," + "[Subs Del No]," + "[TL Name]," + "TL_Contact_Number," + "TL_Email,"
    //                    + "[To Site]," + "[Unique Company]," + "Unique_Installation_Address," + "Verification," + "[Verification Agent]," + "[Verification Code],"
    //                    + "[Verification Status]," + "[Verification Type]," + "Vertical," + "VH_Contact_Number," + "VH_Email," + "VH_Name,"
    //                    + "[Allocated Date]," + "[Billing Trig Date]," + "ComplainceDate," + "[Commissioning Date]," + "InsertedDate," + "[LOC Created Date By PM],"
    //                    + "[LOC Submiited Date By PM]," + "[Orig Service Start Date]," + "[Overall DD Completion Date]," + "RecievedDate," + "[Verification Date]";
    //                dr["Value"] = "'" + dtgridrecord.Rows[i]["Account Caf No"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Account Manager"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Account Number"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Alt Phone1"].ToString() + "',"
    //                    + "'" + dtgridrecord.Rows[i]["Alt Phone2"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Annotation"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["B2B_Email"].ToString() + "',"
    //                                   + "'" + dtgridrecord.Rows[i]["B2B_Head_Email"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["B2B_Head_Name"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Bill Company"].ToString() + "',"
    //                                   + "'" + dtgridrecord.Rows[i]["Bill Plan"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Bill UOM"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Bill_City"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Billed Act Id"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["Billed Ext Id"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Billing Address"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["BILLING_BANDWIDTH"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["BILLING_BANDWIDTH_UOM"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["Billing_Contact_Number"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Billing_Email_Id"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Charge Name"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Circuit BandWidth"].ToString() + "',"
    //                                  + "'" + ddlcomp.SelectedItem.Text + "'," + "'" + dtgridrecord.Rows[i]["Contact Phone1"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Contact Phone2"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Coordinator_Contact_Email"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["Coordinator_Contact_Number"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Coordinator_Name"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Cust account No"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Cust Email"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["Customer Name"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Customer Segment"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Eligible/Not Eligible"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["External id type"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["From Site"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["FE Name"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["FX_ACCOUNT_EXTERNAL_ID"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Installation Address"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["KAM_Contact_Number"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["KAM_Email"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["KAM_Name"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Line Item Description"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Line_Name"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["LOB"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["LOC Status"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Location"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Location Secondary"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["Logical Circuit Id"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Mkt Code"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["MappingID"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Mobile No"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["New_Connection_Type"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Num channel"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Order Type"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Party Name"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["Phone Nos"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Pincode"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["PO Number"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["POP 1"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["POP 2"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["Primary Address"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Project Manager"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["Product Name"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["PRODUCT_TYPE"].ToString() + "'," + "'" + ddlproduct.SelectedItem.Text + "'," + "'" + ddlproducttype.SelectedItem.Text + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["RM_Contact_Number"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["RM_Email"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["RM_NAME"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["SAM_Contact_Number"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["SAM_Email"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["SAM_Name"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["SAM_TL"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["SAM_TL_Contact_Number"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["SAM_TL_EMAIL"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Secondary Address"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Segment"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Service City"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["Service Name"].ToString() + "'," + "'" + FinalServiceStart + "'," + "'" + dtgridrecord.Rows[i]["Status"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Sub Product Name"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["Subs Del No"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["TL Name"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["TL_Contact_Number"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["TL_Email"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["To Site"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Unique Company"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Unique_Installation_Address"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["Verification"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Verification Agent"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Verification Code"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Verification Status"].ToString() + "',"
    //                                  + "'" + dtgridrecord.Rows[i]["Verification Type"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Vertical"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["VH_Contact_Number"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["VH_Email"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["VH_Name"].ToString() + "',"
    //                                  + "'" + FinalDateAllocDate + "'," + "'" + FinalDateBillingTrigDate + "'," + "'" + FinalComplainceDate + "'," + "'" + FinalDateCommDate + "'," + "'" + DateTime.Now.ToString("dd/MMM/yyyy") + "'," + "'" + FinalDateLocCreDate + "'," + "'" + FinalDateLocSubByPM + "'," + "'" + FinalDateOrigDate + "'," + "'" + FinalDateODDCompDate + "',"
    //                                  + "'" + FinalRecieveDate + "'," + "'" + FinalDateVerifictionDate + "'";
    //                dtsearchfilter.Rows.Add(dr);
    //                #endregion
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            diverror.Visible = true;
    //            diverror.InnerHtml = ex.Message.ToString();
    //            diverror.Attributes.Add("class", "alert alert-danger");
    //        }
    //    }
    //    else
    //    {
    //        diverror.Visible = true;
    //        diverror.InnerHtml = "Server error";
    //        diverror.Attributes.Add("class", "alert alert-danger");
    //    }
    //    return dtsearchfilter;
    //}
    //protected void InsertRecordData(DataTable DtInnerChunk)
    //{
    //    try
    //    {
    //        int Dt1 = Lo.InsertExcelUpload(this.InsertReocrd(DtInnerChunk), "tbl_trn_RawData", out _msg, out _sysMsg);
    //        if (Dt1 < 2500)
    //        {
    //            if (Dt1 != 0 && _msg == "row inserted")
    //            {
    //                diverror.InnerHtml = "Excel inserted succssfully, After admin approval it will display and allow to FE.";
    //                diverror.Attributes.Add("class", "alert alert-success");
    //            }
    //            else
    //            {
    //                diverror.Visible = true;
    //                diverror.InnerHtml = "Error found in excel" + " " + _msg;
    //                diverror.Attributes.Add("class", "alert alert-danger");
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        diverror.Visible = true;
    //        diverror.InnerHtml = ex.InnerException.Message;
    //        diverror.Attributes.Add("class", "alert alert-danger");
    //    }
    //}
    //private void ImportAirtelData(string excelfilename)
    //{
    //    string excelConnString = "";
    //    string strFileType = Path.GetExtension(fuexcel.FileName).ToLower();
    //    string path = Server.MapPath("~/App_Data/") + fuexcel.PostedFile.FileName;
    //    fuexcel.SaveAs(path);
    //    if (strFileType.Trim() == ".xls")
    //    {
    //        excelConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2;\"";
    //    }
    //    else if (strFileType.Trim() == ".xlsx")
    //    {
    //        excelConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=2;\"";
    //    }
    //    using (var cnn = new OleDbConnection(excelConnString))
    //    {
    //        cnn.Open();
    //        const string countQuery = "SELECT COUNT(*) FROM [Sheet1$]";
    //        using (var cmd = new OleDbCommand(countQuery, cnn))
    //        {
    //            using (var reader = cmd.ExecuteReader())
    //            {
    //                if (reader == null) return;
    //                reader.Read();
    //                var rowsCount = ((int)reader[0]);
    //                const string query = "SELECT * FROM [Sheet1$]";
    //                using (var odp = new OleDbDataAdapter(query, cnn))
    //                {
    //                    try
    //                    {
    //                        var detailTable = new DataTable();
    //                        var recordToStartFetchFrom = 0;
    //                        const int chunkSize = 2500;
    //                        while (recordToStartFetchFrom <= rowsCount)
    //                        {
    //                            var diff = rowsCount - recordToStartFetchFrom;
    //                            int internalChunkSize = diff < 2500 ? diff : chunkSize;
    //                            odp.Fill(recordToStartFetchFrom, internalChunkSize, detailTable);
    //                            try
    //                            {
    //                                int sum = Convert.ToInt32(recordToStartFetchFrom + internalChunkSize);
    //                                lblRowCount.InnerHtml = "Rows Processed:- " + rowsCount + " Rows going to inserted:- " + sum;
    //                                lblRowCount.Attributes.Add("Class", "alert alert-warning");
    //                                recordToStartFetchFrom += chunkSize;
    //                                InsertRecordData(detailTable);
    //                            }
    //                            catch (Exception ex)
    //                            {
    //                                break;
    //                                diverror.Visible = true;
    //                                diverror.InnerHtml = "Please review your excel we have found error in excel row" + recordToStartFetchFrom + internalChunkSize + "Record before this excel sheet already inserted please delete row in excel and upload again.";
    //                                diverror.Attributes.Add("Class", "alert alert-danger");
    //                            }
    //                            detailTable.Dispose();
    //                            detailTable = null;
    //                            detailTable = new DataTable();
    //                        }
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        diverror.Visible = true;
    //                        diverror.InnerHtml = ex.Message.ToString();
    //                        diverror.Attributes.Add("Class", "alert alert-danger");
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
    #endregion
}

