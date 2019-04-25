using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.IO;
using System.Configuration;
using ClosedXML.Excel;

public partial class frmNotEligibleDataMail : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Common Co = new Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            BindCategory();
        }
    }
    protected void BindCategory()
    {
        DataTable DtBind = Lo.RetriveBindDDL("[ProductId]", "[ProductName]", "tbl_mst_Product where IsActive='Y'");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlcategory, DtBind, "ProductName", "ProductId");
            ddlcategory.Items.Insert(0, "Select Category");
        }
    }
    protected void BindSubCategory()
    {
        DataTable DtBind = Lo.RetriveBindDDL("[ProductTypeID]", "[ProductTypeName]", "tbl_mst_ProductType where IsActive='Y'");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlsubprod, DtBind, "ProductTypeName", "ProductTypeID");
            ddlsubprod.Items.Insert(0, "Select SubCategory");
        }
    }
    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcategory.SelectedItem.Text == "Select Category")
        {
            ddlsubprod.Enabled = false;
        }
        else
        {
            ddlsubprod.Enabled = true;
            BindSubCategory();
        }
    }
    protected DataTable SearchReocrd()
    {
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        if (ddlcategory.SelectedItem.Value != "Select Category")
        {
            dr = insert.NewRow();
            dr["Column"] = "ProductName" + "=";
            dr["Value"] = "'" + ddlcategory.SelectedItem.Text + "'";
            insert.Rows.Add(dr);
        }
        if (ddlsubprod.SelectedItem.Value != "Select SubCategory")
        {
            dr = insert.NewRow();
            dr["Column"] = "ProdTypeName" + "=";
            dr["Value"] = "'" + ddlsubprod.SelectedItem.Text + "'";
            insert.Rows.Add(dr);
        }
        if (txtallocatedDate.Text != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "[Allocated Date]" + " =";
            dr["Value"] = "'" + Convert.ToDateTime(txtallocatedDate.Text).ToString("dd/MMM/yyyy") + "'";
            insert.Rows.Add(dr);
        }
        dr = insert.NewRow();
        dr["Column"] = "[Eligible/Not Eligible]" + "=";
        dr["Value"] = "'Not Eligible' and ExcelVerifiy='2'";

        insert.Rows.Add(dr);
        return insert;
    }
    protected DataTable BindResult()
    {
        return Lo.AllSearchCodeWithCoulmnName(this.SearchReocrd(), "tbl_trn_RawData", "UniqueID,[Allocated Date],[Logical Circuit ID],[Billed Ext ID],[Subs Del No],[Party Name],ProductName,ProdTypeName,[Eligible/Not Eligible],KeyDup,IsActive,EligibleRemarks");
    }
    protected DataTable BindResultMO()
    {
        return Lo.AllSearchCodeWithCoulmnNameMO(this.SearchReocrd(), "tbl_trn_RawData", "[Billed Ext ID],[Party Name],ProductName");
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
                    gvnoteligible.DataSource = Dt1;
                    gvnoteligible.DataBind();
                    gvnoteligible.Visible = true;
                    dlmo.Visible = false;
                    gvprileasedline.Visible = true;
                    btnsendmail.Visible = true;
                    diverror.Visible = true;
                    lbltotalrowselected.Text = "Total record " + Dt1.Rows.Count;
                    diverror.InnerHtml = "Total record " + Dt1.Rows.Count;
                    diverror.Attributes.Add("class", "alert alert-success");
                }
                else
                {
                    dlmo.Visible = false;
                    gvprileasedline.Visible = false;
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
    protected void InsertRecordDataMO()
    {
        try
        {
            DataTable Dt1 = this.BindResultMO();
            try
            {
                if (Dt1.Rows.Count > 0)
                {
                    datalistMo.DataSource = Dt1;
                    datalistMo.DataBind();
                    datalistMo.Visible = true;
                    diverror.Visible = true;
                    dlmo.Visible = true;
                    gvprileasedline.Visible = false;
                    btnsendmailmo.Visible = true;
                    lbltotalrowselected.Text = "Total record " + Dt1.Rows.Count;
                    diverror.InnerHtml = "Total record " + Dt1.Rows.Count;
                    diverror.Attributes.Add("class", "alert alert-success");
                }
                else
                {
                    dlmo.Visible = false;
                    gvprileasedline.Visible = false;
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
    protected void btnclear_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtallocatedDate.Text != "" && ddlcategory.SelectedItem.Text != "Select Category") //&& ddlprod1.SelectedItem.Value != "0")
            {
                DateTime Startdate = Convert.ToDateTime(txtallocatedDate.Text);
                if (ddlcategory.SelectedItem.Text == "MO")
                {
                    InsertRecordDataMO();
                }
                else
                {
                    InsertRecordData();
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
    protected void cleartext()
    {
        ddlcategory.Text = "Select Category";
        ddlsubprod.Enabled = false;
        txtallocatedDate.Text = "";
        txtremark.Text = "";
        lbltotalrowselected.Text = "";
    }
    protected void datalistMo_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GridView gvexcelmo = (GridView)e.Item.FindControl("gvmo");
                Label hfid = (Label)e.Item.FindControl("lblcompdl") as Label;
                DataTable DtAssignJob = Lo.RetriveCodeWithContidion("SELECT  top 1 UniqueID,[Allocated Date],[Billed Ext ID],[Party Name],ProductName,ProdTypeName,[Eligible/Not Eligible],IsActive,EligibleRemarks from tbl_trn_RawData where  IsClosed !='Y' and [Eligible/Not Eligible]='Not Eligible' and ProductName='MO' and [Party Name]='" + hfid.Text + "' and [Allocated Date]='" + txtallocatedDate.Text + "'");
                if (DtAssignJob.Rows.Count > 0)
                {
                    gvexcelmo.DataSource = DtAssignJob;
                    gvexcelmo.DataBind();
                    gvexcelmo.Visible = true;

                }
                else
                {
                    gvexcelmo.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            diverror.Visible = true;
            diverror.InnerHtml = ex.Message;
            diverror.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void btnsendmail_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
        }
        catch (Exception ex)
        {
            diverror.Visible = true;
            diverror.InnerHtml = "Please select all record to send not eligible mail";
            diverror.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void btnsendmailmo_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
        }
        catch (Exception ex)
        {
            diverror.Visible = true;
            diverror.InnerHtml = "Please select all record to send not eligible mail";
            diverror.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        if (txtremark.Text != "")
        {
            try
            {
                if (ddlcategory.SelectedItem.Text == "MO")
                {
                    DateTime TextBoxDate = Convert.ToDateTime(txtallocatedDate.Text);
                    string FinalstartDate1 = TextBoxDate.ToString("dd/MMM/yyyy");
                    DataTable DtSendMailToClient = Lo.RetriveCodeWithContidion("select [Billed Ext ID],[Party Name],ProductName,EligibleRemarks from tbl_trn_RawData where ProductName='MO' and [Allocated Date]='" + FinalstartDate1 + "'  and IsClosed='N' and [Eligible/Not Eligible]='Not Eligible' group by [Billed Ext ID],[Party Name],ProductName,EligibleRemarks");
                    if (DtSendMailToClient.Rows.Count > 0)
                    {
                        ExportExcelNotEligible(DtSendMailToClient);
                    }
                }
                else if (ddlcategory.SelectedItem.Text == "Leased Line")
                {
                    DateTime TextBoxDate = Convert.ToDateTime(txtallocatedDate.Text);
                    string FinalstartDate1 = TextBoxDate.ToString("dd/MMM/yyyy");
                    DataTable DtSendMailToClient = new DataTable();
                    if (ddlsubprod.SelectedItem.Text != "Select SubCategory")
                    {
                        DtSendMailToClient = Lo.RetriveCodeWithContidion("select [Logical Circuit ID],[Party Name],ProductName,EligibleRemarks from tbl_trn_RawData where ProductName='Leased Line' and ProdTypeName='" + ddlsubprod.SelectedItem.Text + "' and [Allocated Date]='" + FinalstartDate1 + "'   and IsClosed='N' and [Eligible/Not Eligible]='Not Eligible'");
                    }
                    else
                    {
                        DtSendMailToClient = Lo.RetriveCodeWithContidion("select [Logical Circuit ID],[Party Name],ProductName,EligibleRemarks from tbl_trn_RawData where ProductName='Leased Line' and [Allocated Date]='" + FinalstartDate1 + "'  and IsClosed='N' and [Eligible/Not Eligible]='Not Eligible'");
                    }
                    if (DtSendMailToClient.Rows.Count > 0)
                    {
                        ExportExcelNotEligible(DtSendMailToClient);
                    }
                }
                else if (ddlcategory.SelectedItem.Text == "PRI-Fixed Line")
                {
                    DateTime TextBoxDate = Convert.ToDateTime(txtallocatedDate.Text);
                    string FinalstartDate1 = TextBoxDate.ToString("dd/MMM/yyyy");
                    DataTable DtSendMailToClient = new DataTable();
                    if (ddlsubprod.SelectedItem.Text != "Select SubCategory")
                    {
                        DtSendMailToClient = Lo.RetriveCodeWithContidion("select [Subs Del No],[Party Name],ProductName,EligibleRemarks from tbl_trn_RawData where ProductName='PRI-Fixed Line' and ProdTypeName='" + ddlsubprod.SelectedItem.Text + "' and [Allocated Date]='" + FinalstartDate1 + "'  and IsClosed='N' and [Eligible/Not Eligible]='Not Eligible'");
                    }
                    else
                    {
                        DtSendMailToClient = Lo.RetriveCodeWithContidion("select [Subs Del No],[Party Name],ProductName,EligibleRemarks from tbl_trn_RawData where ProductName='PRI-Fixed Line' and [Allocated Date]='" + FinalstartDate1 + "'  and IsClosed='N' and [Eligible/Not Eligible]='Not Eligible'");
                    }
                    if (DtSendMailToClient.Rows.Count > 0)
                    {
                        ExportExcelNotEligible(DtSendMailToClient);
                    }
                }
            }
            catch (Exception ex)
            {
                diverror.InnerHtml = ex.Message.ToString();
                diverror.Attributes.Add("Class", "alert alert-danger");
                diverror.Visible = true;
            }
        }
        else
        {
            diverror.InnerHtml = "Please enter remark.";
            diverror.Attributes.Add("Class", "alert alert-danger");
            diverror.Visible = true;
        }

    }
    #region Send MailMo
    protected void SendMailAirtel(byte[] bytes, DataTable DtEligibleMailSendClient)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/NotEligible.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Remark}", txtremark.Text);
            SendMail s;
            s = new SendMail();
            s.CreateNotEligible("verification@rcpl.in", "Shyam.Prasad@airtel.com", "Airtel Not Eligible Sheet ' " + ddlcategory.SelectedItem.Text + "'", body, ddlcategory.SelectedItem.Text, bytes, "");
            string exMsg = "";
            bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
            if (result == true && exMsg == "")
            {
                diverror.InnerHtml = "Successfully Mail Send.";
                diverror.Attributes.Add("Class", "alert alert-danger");
                diverror.Visible = true;
                cleartext();
                DtEligibleMailSendClient.Clear();
                gvprileasedline.Visible = false;
                dlmo.Visible = false;
            }
            else
            {
                diverror.InnerHtml = exMsg.ToString();
                diverror.Attributes.Add("Class", "alert alert-danger");
                diverror.Visible = true;
                cleartext();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void ExportExcelNotEligible(DataTable DtNotEligibleMO)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(DtNotEligibleMO);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                string path = Server.MapPath("~/App_Data/NotEligible.xlsx");
                File.WriteAllBytes(path, bytes);
                SendMailAirtel(bytes, DtNotEligibleMO);
            }
        }
    }
    #endregion
}