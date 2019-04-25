using System;
using BusinessLayer;
using System.Data;
using System.Data.OleDb;

public partial class UploadPreScheduleExcel : System.Web.UI.Page
{
    #region Variable
    DataTable Exceldt;
    DataTable dtgridrecord = new DataTable();
    DataTable Dt = new DataTable();
    Logic Lo = new Logic();
    Common Co = new Common();
    string _msg = string.Empty;
    string _sysMsg = string.Empty;
    string exMsg = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            BindCompany();
            gvexcelpanel.Visible = false;
        }
    }
    #region Upload Excel Code
    protected void btnexcel_Click(object sender, EventArgs e)
    {
        if (ddlcomp.SelectedItem.Text != "Select Company" && ddlproduct.SelectedItem.Text != "Select Product" && ddlproducttype.SelectedItem.Text != "Select Product Type")
        {
            ImportDataFromExcel(fuexcel.FileName);
        }
        else
        {
            diverror.InnerHtml = "Please select company product or product type with excel file.";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    public void ImportDataFromExcel(string excelFilePath)
    {
        try
        {
            if (fuexcel.FileName != "")
            {
                DataSet ds = new DataSet();
                string path = Server.MapPath("~/App_Data/") + fuexcel.FileName;
                fuexcel.SaveAs(path);
                string a = fuexcel.PostedFile.FileName;
                OleDbConnection con_excel = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 8.0;");
                con_excel.Open();
                DataTable Sheets = con_excel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow dr in Sheets.Rows)
                {
                    string sht = dr[2].ToString().Replace("'", "");
                    OleDbDataAdapter adpt = new OleDbDataAdapter("select * from [" + sht + "]", con_excel);
                    adpt.Fill(ds, "tbl_trn_RowData");
                }
                con_excel.Close();
                //  OleDbDataAdapter adpt = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", con_excel);
                Exceldt = ds.Tables["tbl_trn_RowData"];
                if (Exceldt.Rows.Count > 0)
                {
                    ViewState["wali"] = Exceldt;
                    gvexcel.DataSource = Exceldt;
                    gvexcelpanel.Visible = true;
                    gvexcel.DataBind();
                    lblpathname.Text = "Total Record Found In Excel Sheet= " + Exceldt.Rows.Count.ToString();
                    diverror.Visible = false;
                }
                else
                {
                    gvexcelpanel.Visible = false;
                    diverror.Visible = true;
                    diverror.InnerHtml = "sorry we did not find any row in excel.";
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
            }
            else
            {
                gvexcelpanel.Visible = false;
                diverror.Visible = true;
                diverror.InnerHtml = "Please insert excel file";
                diverror.Attributes.Add("class", "alert alert-danger");
            }
        }
        catch (Exception ex)
        {
            gvexcelpanel.Visible = false;
            diverror.Visible = true;
            diverror.InnerHtml = ex.Message.ToString();
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected DataTable InsertReocrd()
    {
        dtgridrecord = (DataTable)ViewState["wali"];
        DataTable dtsearchfilter = new DataTable();
        if (dtgridrecord.Rows.Count > 0)
        {
            string FinalDateCopeApprove = "";
            string FinalDateCustomePODate = "";
            string FinalDateOrderEntryDate = "";
            string FinalDatePMApproDate = "";
            string FinalDateAccoManRecDate = "";
            string FinalDateNioDate = "";
            string FinalDateCusRFSDate = "";
            string FinalDateCommDate = "";
            string FinalDateODDCompDate = "";
            string FinalDateLocCreDate = "";
            string FinalDateLocSignoff = "";
            string FinalDateLocSubByPM = "";
            string FinalDateLocRecPM = "";
            string FinalDateLocsubdatetobill = "";
            string FinalDateBillingTrigDate = "";
            string FinalDateInvoiceDate = "";
            string FinalDateAllocDate = "";
            string FinalDateDate = "";
            string FinalDateSamMailDate = "";
            string FinalDateSamRevertDate = "";
            string FinalRecieveDate = "";
            string FinalComplainceDate = "";
            //string FinalSubmittedDate = "";

            dtsearchfilter.Columns.Add(new DataColumn("Name", typeof(string)));
            dtsearchfilter.Columns.Add(new DataColumn("Value", typeof(string)));
            for (int i = 0; dtgridrecord.Rows.Count > i; i++)
            {
                if (dtgridrecord.Rows[i]["COPC APPROVED DATE"].ToString() != "")
                {
                    DateTime DateCopeApprove = Convert.ToDateTime(dtgridrecord.Rows[i]["COPC APPROVED DATE"].ToString());
                    FinalDateCopeApprove = DateCopeApprove.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["Customer PODate"].ToString() != "")
                {
                    DateTime DateCustomePODate = Convert.ToDateTime(dtgridrecord.Rows[i]["Customer PODate"].ToString());
                    FinalDateCustomePODate = DateCustomePODate.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["Order Entry Date"].ToString() != "")
                {
                    DateTime DateOrderEntryDate = Convert.ToDateTime(dtgridrecord.Rows[i]["Order Entry Date"].ToString());
                    FinalDateOrderEntryDate = DateOrderEntryDate.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["PM APPROVAL DATE"].ToString() != "")
                {
                    DateTime DatePMApproDate = Convert.ToDateTime(dtgridrecord.Rows[i]["PM APPROVAL DATE"].ToString());
                    FinalDatePMApproDate = DatePMApproDate.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["ACCOUNT MANAGER RECEIVE DATE"].ToString() != "")
                {
                    DateTime DateAccoManRecDate = Convert.ToDateTime(dtgridrecord.Rows[i]["ACCOUNT MANAGER RECEIVE DATE"].ToString());
                    FinalDateAccoManRecDate = DateAccoManRecDate.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["NIO DATE"].ToString() != "")
                {
                    DateTime DateNioDate = Convert.ToDateTime(dtgridrecord.Rows[i]["NIO DATE"].ToString());
                    FinalDateNioDate = DateNioDate.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["CUSTOMER RFS DATE"].ToString() != "")
                {
                    DateTime DateCusRFSDate = Convert.ToDateTime(dtgridrecord.Rows[i]["CUSTOMER RFS DATE"].ToString());
                    FinalDateCusRFSDate = DateCusRFSDate.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["Commissioning Date"].ToString() != "")
                {
                    DateTime DateCommDate = Convert.ToDateTime(dtgridrecord.Rows[i]["Commissioning Date"].ToString());
                    FinalDateCommDate = DateCommDate.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["Overall DD Completion Date"].ToString() != "")
                {
                    DateTime DateODDCompDate = Convert.ToDateTime(dtgridrecord.Rows[i]["Overall DD Completion Date"].ToString());
                    FinalDateODDCompDate = DateODDCompDate.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["LOC Created Date By PM"].ToString() != "")
                {
                    DateTime DateLocCreDate = Convert.ToDateTime(dtgridrecord.Rows[i]["LOC Created Date By PM"].ToString());
                    FinalDateLocCreDate = DateLocCreDate.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["LOC SignOffDate"].ToString() != "")
                {
                    DateTime DateLocSignoff = Convert.ToDateTime(dtgridrecord.Rows[i]["LOC SignOffDate"].ToString());
                    FinalDateLocSignoff = DateLocSignoff.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["LOC Submiited Date By PM"].ToString() != "")
                {
                    DateTime DateLocSubByPM = Convert.ToDateTime(dtgridrecord.Rows[i]["LOC Submiited Date By PM"].ToString());
                    FinalDateLocSubByPM = DateLocSubByPM.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["LOC Received date at PMO"].ToString() != "")
                {
                    DateTime DateLocRecPM = Convert.ToDateTime(dtgridrecord.Rows[i]["LOC Received date at PMO"].ToString());
                    FinalDateLocRecPM = DateLocRecPM.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["LOC Submitted date to Billing"].ToString() != "")
                {
                    DateTime DateLocsubdatetobill = Convert.ToDateTime(dtgridrecord.Rows[i]["LOC Submitted date to Billing"].ToString());
                    FinalDateLocsubdatetobill = DateLocsubdatetobill.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["Billing Trigger Date"].ToString() != "")
                {
                    DateTime DateBillingTrigDate = Convert.ToDateTime(dtgridrecord.Rows[i]["Billing Trigger Date"].ToString());
                    FinalDateBillingTrigDate = DateBillingTrigDate.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["Invoice Date"].ToString() != "")
                {
                    DateTime DateInvoiceDate = Convert.ToDateTime(dtgridrecord.Rows[i]["Invoice Date"].ToString());
                    FinalDateInvoiceDate = DateInvoiceDate.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["Allocated Date"].ToString() != "")
                {
                    DateTime DateAllocDate = Convert.ToDateTime(dtgridrecord.Rows[i]["Allocated Date"].ToString());
                    FinalDateAllocDate = DateAllocDate.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["Date"].ToString() != "")
                {
                    DateTime DateDate = Convert.ToDateTime(dtgridrecord.Rows[i]["Date"].ToString());
                    FinalDateDate = DateDate.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["SAM Mail Date"].ToString() != "")
                {
                    DateTime DateSamMailDate = Convert.ToDateTime(dtgridrecord.Rows[i]["SAM Mail Date"].ToString());
                    FinalDateSamMailDate = DateSamMailDate.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["SAM Revert Date"].ToString() != "")
                {
                    DateTime DateSamRevertDate = Convert.ToDateTime(dtgridrecord.Rows[i]["SAM Revert Date"].ToString());
                    FinalDateSamRevertDate = DateSamRevertDate.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["RecievedDate"].ToString() != "")
                {
                    DateTime DateRecievedDate = Convert.ToDateTime(dtgridrecord.Rows[i]["RecievedDate"].ToString());
                    FinalRecieveDate = DateRecievedDate.ToString("dd/MMM/yyyy");
                }
                if (dtgridrecord.Rows[i]["ComplainceDate"].ToString() != "")
                {
                    DateTime DateComplainceDate = Convert.ToDateTime(dtgridrecord.Rows[i]["ComplainceDate"].ToString());
                    FinalComplainceDate = DateComplainceDate.ToString("dd/MMM/yyyy");
                }
                //if (dtgridrecord.Rows[i]["SubmittedDate"].ToString() != "")
                //{
                //    DateTime DateSubmittedDate = Convert.ToDateTime(dtgridrecord.Rows[i]["SubmittedDate"].ToString());
                //    FinalSubmittedDate = DateSubmittedDate.ToString("dd/MMM/yyyy");
                //}
                DataRow dr;
                dr = dtsearchfilter.NewRow();
                dr["Name"] = "[LOC Sr No]," + "[LOC Status]," + "[Customer Name]," + "[CRM ORDER ID]," + "[COPC APPROVED DATE]," + "[Logical CircuitId]," + "[Dup]," + "[Product Name]," + "[Sub Product Name]," + "[From Site]," +
                    "[To Site]," + "[Order Type]," + "[Circuit BandWidth]," + "[Line Item Amount]," + "[Project Manager]," + "[PM OracleId]," + "[Account Manager]," + "[Zone]," + "[Region]," + "[Vertical]," + "[Account Category]," +
                    "[Customer PO Number]," + "[Customer PODate]," + "[Order Entry Date]," + "[PM APPROVAL DATE]," + "[ACCOUNT MANAGER RECEIVE DATE]," + "[NIO DATE]," + "[CUSTOMER RFS DATE]," + "[Order Category]," + "[Order Reporting Status]," +
                    "[Line Item Description]," + "[Order Line Name]," + "[Order Line Type]," + "[Charge Name]," + "[PK_CHARGES_ID]," + "[Billing Entity]," + "[Contract Period]," + "[Cyclic NonCyclic]," + "[Billing Bandwidth]," + "[Bill UOM]," +
                     "[Currency]," + "[RA ORDER NUMBER]," + "[Commissioning Date]," + "[Overall DD Completion Date]," + "[LOC Created Date By PM]," + "[LOC SignOffDate]," + "[LOC Submiited Date By PM]," + "[LOC Received date at PMO]," + "[LOC Submitted date to Billing]," + "[Billing Trigger Date]," + "[Invoice Date]," +
                    "[Invoice No]," + "[Invoice Amount]," + "[Customer Segment]," + "[Project Region]," + "[Annualized (CWN Value)]," + "[Annualized (CWN Value) in INR Mn]," + "[Gain/Loss]," + "[LOC Delay Reason-Commissioning]," + "[Final ACD]," + "[Final CND]," + "[Party City]," +
                    "[Party Region]," + "[Service Segment]," + "[Media]," + "[OnNet/OffNet]," + "[Eligible/Not Eligible]," + "[Month]," + "[Zone1]," + "[Location]," + "[FOS Name]," + "[Allocated Date]," + "[FE Name]," +
                    "[Positive/Negative]," + "[Code]," + "[Remarks]," + "[Date]," + "[Cust Name]," + "[Cust No]," + "[Cust Mail ID]," + "[SAM Name]," + "[SAM Mail ID]," + "[SAM No]," + "[SAM TL Name]," +
                    "[SAM TL Email]," + "[SAM TL Contact]," + "[SAM Mails Status]," + "[SAM Mail Date]," + "[SAM Revert]," + "[SAM Revert Date]," + "[RecievedDate]," + "[ComplainceDate]," + "[ProductName]," + "[ProdTypeName]," + "[MappingID]," + "IsActive='Y'";
                dr["Value"] = "'" + dtgridrecord.Rows[i]["LOC Sr No"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["LOC Status"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Customer Name"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["CRM ORDER ID"].ToString() + "'," + "'" + FinalDateCopeApprove + "',"
                    + "'" + dtgridrecord.Rows[i]["Logical CircuitId"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Dup"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Product Name"].ToString() + "',"
                     + "'" + dtgridrecord.Rows[i]["Sub Product Name"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["From Site"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["To Site"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Order Type"].ToString() + "'," +
                    "'" + dtgridrecord.Rows[i]["Circuit BandWidth"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Line Item Amount"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Project Manager"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["PM OracleId"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Account Manager"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Zone"].ToString() + "'," +
                    "'" + dtgridrecord.Rows[i]["Region"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Vertical"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Account Category"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Customer PO Number"].ToString() + "'," + "'" + FinalDateCustomePODate + "'," + "'" + FinalDateOrderEntryDate + "'," +
                    "'" + FinalDatePMApproDate + "'," + "'" + FinalDateAccoManRecDate + "'," + "'" + FinalDateNioDate + "'," + "'" + FinalDateCusRFSDate + "'," + "'" + dtgridrecord.Rows[i]["Order Category"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Order Reporting Status"].ToString() + "'," +
                    "'" + dtgridrecord.Rows[i]["Line Item Description"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Order Line Name"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Order Line Type"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Charge Name"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["PK_CHARGES_ID"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Billing Entity"].ToString() + "'," +
                    "'" + dtgridrecord.Rows[i]["Contract Period"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Cyclic NonCyclic"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Billing Bandwidth"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Bill UOM"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Currency"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["RA ORDER NUMBER"].ToString() + "'," +
                    "'" + FinalDateCommDate + "'," + "'" + FinalDateODDCompDate + "'," + "'" + FinalDateLocCreDate + "'," + "'" + FinalDateLocSignoff + "'," + "'" + FinalDateLocSubByPM + "'," + "'" + FinalDateLocRecPM + "'," +
                    "'" + FinalDateLocsubdatetobill + "'," + "'" + FinalDateBillingTrigDate + "'," + "'" + FinalDateInvoiceDate + "'," + "'" + dtgridrecord.Rows[i]["Invoice No"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Invoice Amount"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Customer Segment"].ToString() + "'," +
                    "'" + dtgridrecord.Rows[i]["Project Region"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Annualized (CWN Value)"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Annualized (CWN Value) in INR Mn"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Gain/Loss"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["LOC Delay Reason-Commissioning"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Final ACD"].ToString() + "'," +
                    "'" + dtgridrecord.Rows[i]["Final CND"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Party City"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Party Region"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Service Segment"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Media"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["OnNet/OffNet"].ToString() + "'," +
                    "'" + dtgridrecord.Rows[i]["Eligible/Not Eligible"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Month"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Zone1"].ToString() + "'," + "'" + ddlfe.SelectedItem.Text.Substring(0, ddlfe.SelectedItem.Text.LastIndexOf("+")) + "'," + "'" + dtgridrecord.Rows[i]["FOS Name"].ToString() + "'," + "'" + FinalDateAllocDate + "'," +
                    "'" + ddlfe.SelectedItem.Value + "'," + "'" + dtgridrecord.Rows[i]["Positive/Negative"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Code"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Remarks"].ToString() + "'," + "'" + FinalDateDate + "'," + "'" + dtgridrecord.Rows[i]["Cust Name"].ToString() + "'," +
                    "'" + dtgridrecord.Rows[i]["Cust No"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["Cust Mail ID"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["SAM Name"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["SAM Mail ID"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["SAM No"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["SAM TL Name"].ToString() + "'," +
                    "'" + dtgridrecord.Rows[i]["SAM TL Email"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["SAM TL Contact"].ToString() + "'," + "'" + dtgridrecord.Rows[i]["SAM Mails Status"].ToString() + "'," + "'" + FinalDateSamMailDate + "',"
                    + "'" + dtgridrecord.Rows[i]["SAM Revert"].ToString() + "'," + "'" + FinalDateSamRevertDate + "'," + "'" + FinalRecieveDate + "'," + "'" + FinalComplainceDate + "'," + "'" + ddlproduct.SelectedItem.Text + "'," + "'" + ddlproducttype.SelectedItem.Text + "'," + "'" + ddlfe.SelectedItem.Text.Substring(0, ddlfe.SelectedItem.Text.LastIndexOf("+")) + "','Y'";
                dtsearchfilter.Rows.Add(dr);
            }
        }
        else
        {
            diverror.Visible = true;
            diverror.InnerHtml = "Server error";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
        return dtsearchfilter;
    }
    protected int BindResult()
    {
        return Lo.InsertExcelUpload(this.InsertReocrd(), "tbl_trn_RawData", out _msg, out _sysMsg);
    }
    protected void InsertRecordData()
    {
        try
        {
            int Dt1 = this.BindResult();
            try
            {
                if (Dt1 != 0 && _msg == "row inserted")
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = "Excel inserted succssfully and Record assign to FE";
                    diverror.Attributes.Add("class", "alert alert-success");
                    dtgridrecord.Clear();
                }
                else
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = "Error found in excel" + " " + _msg;
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
    protected void btnfinalexcelupload_Click(object sender, EventArgs e)
    {
        try
        {
            InsertRecordData();
        }
        catch (Exception ex)
        {
            diverror.Visible = true;
            diverror.InnerHtml = ex.Message;
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    #endregion
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
                ddlfe.Enabled = false;
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
                ddlproduct.Enabled = true;
            }
        }
        else
        {
            ddlproduct.Enabled = false;
        }
    }
    protected void BindProdType()
    {
        if (ddlproduct.SelectedItem.Text != "Select Product")
        {
            DataTable DtBind = Lo.RetriveBindDDL("ProductTypeID", "ProductTypeName", "tbl_mst_ProductType where IsActive='Y'");
            if (DtBind.Rows.Count > 0)
            {
                Co.FillComboBox(ddlproducttype, DtBind, "ProductTypeName", "ProductTypeID");
                ddlproducttype.Items.Insert(0, "Select Product Type");
                ddlproducttype.Enabled = true;
            }
        }
        else
        {
            ddlproducttype.Enabled = false;
        }
    }
    protected void BindFE()
    {
        if (ddlfe.SelectedItem.Text != "Select Field Executive")
        {
            DataTable DtBind = Lo.RetriveBindDDL("Name", "(c.FEZoneAreaLocation+' + '+c.Name) AS ZoneAreaWithPinCode", "tbl_mst_Employee where IsActive='Y'");
            if (DtBind.Rows.Count > 0)
            {
                Co.FillComboBox(ddlfe, DtBind, "ZoneAreaWithPinCode", "Name");
                ddlfe.Items.Insert(0, "Select Field Executive");
                ddlfe.Enabled = true;
            }
        }
        else
        {
            ddlfe.Enabled = false;
        }
    }
    protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCompProd();
    }
    protected void ddlproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProdType();
    }
    protected void ddlproducttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindFE();
    }
    #endregion

}