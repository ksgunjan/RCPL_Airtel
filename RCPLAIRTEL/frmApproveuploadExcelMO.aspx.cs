﻿using BuisnessLayer;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmApproveuploadExcelMO : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private Label lblproduct;
    private Label feName;
    private string compname;
    private string successmessage;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Search();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void Search()
    {
        if (txtdate.Text != "")
        {
            DataTable DtFetchcompgropby = Lo.RetriveFunctionCode("MO", txtdate.Text, "", "", "");
            if (DtFetchcompgropby.Rows.Count > 0)
            {
                gvexcel.DataSource = DtFetchcompgropby;
                gvexcel.DataBind();
                gvexcel.Visible = true;
                divbutton.Visible = true;
                lbltoalrows.Text = "Total Record Found:- " + DtFetchcompgropby.Rows.Count.ToString();
            }
            else
            {
                lbltoalrows.Text = "Total Record Found:- " + "0";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No record found.')", true);
                gvexcel.Visible = false;
                divbutton.Visible = false;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please enter valid allocation date for approve or disapprove')", true);
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
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), Convert.ToInt16(ConfigurationManager.AppSettings["Port"].ToString()), ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        if (result == true && exMsg == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('" + lblproduct.Text + "Row successfully delete also mail send to team leader to upload again excel sheet.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup(' " + exMsg + " ')", true);
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
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Please select all record to delete upload excel')", true);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>$('#myModal').modal('hide');</script>", false);
                    break;
                }
            }
            lbltotalrowselected.Text = "Total Row Selected=" + checkgridlistrow;
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Please select all record to delete upload excel')", true);
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
                    DataTable dtgetuser = Lo.RetriveCodeLocal(0, feName.Text, "", "", "RetriveUserNormal");
                    if (dtgetuser.Rows.Count > 0)
                    {
                        string Uploadexcel = Lo.RetriveCodeLocalup(0, HfIDBilled.Value, txtdate.Text, "", "UExcelVerifyStatusMO");
                        Totalcountsucess++;
                        successmessage = "RowVerify";
                    }
                    else
                    {
                        successmessage = "RowVerify";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('FE Name " + feName + " is not match please check FE Name.Please verify excel again all record not inserted for approve total row inserted for approval is := " + Totalcountsucess + "')", true);
                        break;
                    }
                }
                else
                {
                    successmessage = "RowVerify";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('You are not selected all row.Please verify excel again  total row inserted for approval is := " + Totalcountsucess + "')", true);
                    break;

                }
            }
            if (successmessage == "RowVerify")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Row Approved Successfully.Please refresh again to check')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
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
                        int DeleteAllrow = Lo.DeleteCode(0, HfIDBilled.Value, Convert.ToDateTime(txtdate.Text).ToString("dd/MMM/yyyy"), "", "DeletecodeAppExclMO");
                    }
                }
                SendMailTL();
                Search();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Successfully delete current excel upload record and mail send to Team Leader.')", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Please enter reason for disapprove data')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
    }
    protected void gvexcel_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }

    }
}