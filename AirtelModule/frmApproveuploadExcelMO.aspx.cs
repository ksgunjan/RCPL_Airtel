using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using System.IO;
using System.Configuration;
using System.Text;
using ClosedXML.Excel;

public partial class frmApproveuploadExcelMO : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Label lblproduct;
    Label feName;
    string compname;
    string successmessage;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }
    protected void Search()
    {
        if (txtdate.Text != "")
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add("Allocated Date", typeof(DateTime));
            //dt.Columns.Add("ComplainceDate", typeof(DateTime));
            //dt.Columns.Add("Billed Ext Id", typeof(string));
            //dt.Columns.Add("Party Name", typeof(string));
            //dt.Columns.Add("Customer Segment", typeof(string));
            //dt.Columns.Add("ProductName", typeof(string));
            //dt.Columns.Add("ProdTypeName", typeof(string));
            //dt.Columns.Add("FE Name", typeof(string));
            //dt.Columns.Add("UniqueID", typeof(Int32));
            // DataTable DtFetchcompgropby = Lo.RetriveCodeWithContidion("select count([party name])as [Count] ,[party name] from tbl_trn_RawData where productname='MO' and isclosed='N' and IsActive='N' and ExcelVerifiy='1' and [Eligible/Not Eligible]='Eligible' group by [party name]");
            DataTable DtFetchcompgropby = Lo.RetriveCodeWithContidion("select * from dbo.fn_RecordForApproveForMailMO('MO','" + txtdate.Text + "') order by Party_Name asc");
            if (DtFetchcompgropby.Rows.Count > 0)
            {
                // for (int i = 0; DtFetchcompgropby.Rows.Count > i; i++)
                // {
                //   compname = DtFetchcompgropby.Rows[i]["Party Name"].ToString();
                // DataTable DtAssignJob = Lo.RetriveCodeWithContidion("SELECT  top 1  [Allocated Date],ComplainceDate,[Billed Ext Id],[Party Name],[Customer Segment],ProductName,[ProdTypeName],[FE Name],UniqueID  from tbl_trn_RawData where IsActive='N' and IsClosed !='Y' and ExcelVerifiy='1'  and [Eligible/Not Eligible]='Eligible' and ProductName='MO' and [Party Name]= '" + compname + "' order by [FE Name] asc");
                //foreach (DataRow dr in DtAssignJob.Rows)
                //{
                //  object[] row = dr.ItemArray;
                // dt.Rows.Add(row);
                //}
                //}
                // if (dt.Rows.Count > 0)
                //{
                gvexcel.DataSource = DtFetchcompgropby;
                gvexcel.DataBind();
                gvexcel.Visible = true;
                divbutton.Visible = true;
                diverror.Visible = true;
                diverror.InnerHtml = "Total Record Found:- " + DtFetchcompgropby.Rows.Count.ToString();
                diverror.Attributes.Add("Class", "alert alert-success");
                //}
                // else
                // {
                //   gvexcel.Visible = false;
                // divbutton.Visible = false;
                //}
            }
            else
            {
                diverror.Visible = true;
                diverror.InnerHtml = "No Record Found";
                diverror.Attributes.Add("class", "alert alert-danger");
                gvexcel.Visible = false;
                divbutton.Visible = false;
            }
        }
        else
        {
            diverror.Visible = true;
            diverror.InnerHtml = "Please enter valid allocation date for approve or disapprove";
            diverror.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void SendMailTL()
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/SuccessorFailVerification.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Message}", "Excel Sheet Upload on Date <b>" + txtdate.Text + "</b> has been delete from server due to this reasone <b>" + txtreasone.Text + "</b>. Please verify or upload again. ");
        SendMail s;
        s = new SendMail();
        s.CreateMail("verification@rcpl.in", "verification@rcpl.in", "Airtel Upload Sheet " + txtdate.Text + "", body, "ExcelVerify");
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        if (result == true && exMsg == "")
        {
            diverror.Visible = true;
            diverror.InnerHtml = lblproduct.Text + "Row successfully delete also mail send to team leader to upload again excel sheet.";
            diverror.Attributes.Add("Class", "alert alert-success");
        }
        else
        {
            diverror.Visible = true;
            diverror.InnerHtml = exMsg;
            diverror.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void btndisapprove_Click(object sender, EventArgs e)
    {
        try
        {
            int checkgridlistrow = 0;
            foreach (GridViewRow row in gvexcel.Rows)
            {
                if ((row.FindControl("chkRow") as CheckBox).Checked)
                {
                    checkgridlistrow++;
                    diverrormssg.Visible = true;
                    divshowdetail.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                }
                else
                {

                    diverror.Visible = true;
                    divshowdetail.Visible = false;
                    diverror.InnerHtml = "Please select all record to delete upload excel";
                    diverror.Attributes.Add("Class", "alert alert-danger");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('hide');</script>", false);
                    break;
                }
            }
            lbltotalrowselected.Text = "Total Row Selected=" + checkgridlistrow;
        }
        catch (Exception ex)
        {
            diverror.Visible = true;
            diverror.InnerHtml = "Please select all record to delete upload excel";
            diverror.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void btnapprove_Click(object sender, EventArgs e)
    {
        try
        {
            int Totalcountsucess = 0;
            foreach (GridViewRow row in gvexcel.Rows)
            {
                HiddenField HfID = (HiddenField)row.FindControl("hdnIDGridRawData") as HiddenField;
                HiddenField HfIDBilled = (HiddenField)row.FindControl("hfbilledextendedid") as HiddenField;
                feName = (Label)row.FindControl("lblfename") as Label;
                lblproduct = (Label)row.FindControl("lblproduct") as Label;
                if ((row.FindControl("chkRow") as CheckBox).Checked)
                {
                    DataTable dtgetuser = Lo.RetriveCodeWithContidion("select EmpID,Category,Name from  tbl_mst_Employee where Name ='" + feName.Text + "'");
                    if (dtgetuser.Rows.Count > 0)
                    {
                        int Uploadexcel = Lo.UpdateCodeOther("tbl_trn_RawData", "Set ExcelVerifiy='2'", "[Party Name]='" + HfIDBilled.Value + "' and [Allocated Date]='" + txtdate.Text + "'");
                        Totalcountsucess++;
                        successmessage = "RowVerify";
                    }
                    else
                    {
                        diverror.Visible = true;
                        diverror.InnerHtml = "FE Name " + feName + " is not match please check FE Name.Please verify excel again";
                        diverror.Attributes.Add("Class", "alert alert-danger");
                        successmessage = "";
                        break;
                    }
                }
                else
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = "You are not selected all row.Please verify excel again all record not inserted for approve total row inserted for approval is := " + Totalcountsucess + "";
                    diverror.Attributes.Add("Class", "alert alert-danger");
                    successmessage = "";
                    break;

                }
            }
            if (successmessage == "RowVerify")
            {
                Search();
            }
        }
        catch (Exception ex)
        {
            diverror.Visible = true;
            diverror.InnerHtml = ex.Message;
            diverror.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void gvexcel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDetail")
        {
            DataTable DtSearchView = Lo.RetriveCodeAllExcelRecordWhereCondition("UniqueID =" + Convert.ToInt32(e.CommandArgument.ToString()) + " and ProductName='MO'");
            if (DtSearchView.Rows.Count > 0)
            {
                dluploadexcelrecord.DataSource = DtSearchView;
                dluploadexcelrecord.DataBind();
                dluploadexcelrecord.Visible = true;
                divshowdetail.Visible = true;
                diverror.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
            }
        }
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtreasone.Text != "" && lbltotalrowselected.Text != "")
            {
                foreach (GridViewRow row in gvexcel.Rows)
                {
                    HiddenField HfID = (HiddenField)row.FindControl("hdnIDGridRawData") as HiddenField;
                    HiddenField HfIDBilled = (HiddenField)row.FindControl("hfbilledextendedid") as HiddenField;
                    lblproduct = (Label)row.FindControl("lblproduct") as Label;
                    if ((row.FindControl("chkRow") as CheckBox).Checked)
                    {
                        Int32 DeleteAllrow = Lo.DeleteCode("tbl_trn_RawData", "[Party Name]='" + HfIDBilled.Value + "' and  [Allocated Date]='" + Convert.ToDateTime(txtdate.Text).ToString("dd/MMM/yyyy") + "' and ProductName='MO' and isActive='N'");
                    }
                }
                SendMailTL();
                Search();
                popuperror.Visible = true;
                popuperror.InnerHtml = "Successfully delete current excel upload record and mail send to Team Leader.";
                popuperror.Attributes.Add("Class", "alert alert-success");
            }
            else
            {
                popuperror.Visible = true;
                popuperror.InnerHtml = "Please enter reason for disapprove data";
                popuperror.Attributes.Add("Class", "alert alert-danger");
            }
        }
        catch (Exception ex)
        {
            popuperror.Visible = true;
            popuperror.InnerHtml = ex.Message + "Record successfully delete.";
            popuperror.Attributes.Add("Class", "alert alert-danger");
        }
    }
}