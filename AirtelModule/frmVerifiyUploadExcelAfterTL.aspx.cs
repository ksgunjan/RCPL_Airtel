using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.IO;
using System.Configuration;

public partial class frmVerifiyUploadExcelAfterTL : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Label lblproduct;
    HiddenField HfID;
    Label feName;
    Label ProductName;
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
            DataTable DtVerifyData = Lo.RetriveCodeAllExcelRecordWhereCondition("IsActive='N' and IsClosed='N' and [Allocated Date]='" + txtdate.Text + "' and ExcelVerifiy='1' and ProductName ='Leased Line'");
            if (DtVerifyData.Rows.Count > 0)
            {
                
                gvexcel.DataSource = DtVerifyData;
                gvexcel.DataBind();
                gvexcel.Visible = true;
                divbutton.Visible = true;
                lbltoalrows.Text = "Total Record Found:- " + DtVerifyData.Rows.Count.ToString();
            }
            else
            {
                gvexcel.Visible = false;
                diverror.Visible = true;
                diverror.InnerHtml = "No record found.";
                diverror.Attributes.Add("Class", "alert alert-danger");
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
        body = body.Replace("{Message}", "Excel Sheet Upload on Date <b>" + txtdate.Text + "</b> has been delete from server due to this reasone <b>" + txtreasone.Text + "</b>.<br> Please verify or upload again. ");
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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                }
                else
                {

                    diverror.Visible = true;
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
                        if (lblproduct.Text != "MO")
                        {
                            int Uploadexcel = Lo.UpdateCodeOther("tbl_trn_RawData", "Set ExcelVerifiy='2'", "[Logical Circuit Id]='" + HfID.Value + "'");
                        }
                        Totalcountsucess++;
                    }
                    else
                    {
                        diverror.Visible = true;
                        diverror.InnerHtml = "FE Name is not match please check FE Name.Please verify excel again all record not inserted for approve total row inserted for approval is := " + Totalcountsucess + "";
                        diverror.Attributes.Add("Class", "alert alert-danger");
                        break;
                    }
                }
                else
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = "You are not selected all row.Please verify excel again  total row inserted for approval is := " + Totalcountsucess + "";
                    diverror.Attributes.Add("Class", "alert alert-danger");
                    break;
                }
            }
            Search();
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
            DataTable DtSearchView = Lo.RetriveCodeAllExcelRecordWhereCondition("UniqueID =" + Convert.ToInt32(e.CommandArgument.ToString()) + " and ProductName !='MO'");
            if (DtSearchView.Rows.Count > 0)
            {
                dluploadexcelrecord.DataSource = DtSearchView;
                dluploadexcelrecord.DataBind();
                dluploadexcelrecord.Visible = true;
                divshowdetail.Visible = true;
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
                        if (lblproduct.Text != "MO")
                        {
                            Int32 DeleteAllrow = Lo.DeleteCode("tbl_trn_RawData", "[Logical Circuit Id]='" + HfID.Value + "' and  [Allocated Date]='" + Convert.ToDateTime(txtdate.Text).ToString("dd/MMM/yyyy") + "'");
                        }
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