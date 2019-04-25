﻿using System;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using System.Text;
public partial class frmviewUser : System.Web.UI.Page
{
    Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            bindgrid();
        }
    }
    protected void bindgrid()
    {
        DataTable DTGrid = Lo.RetriveCodeUser();
        if (DTGrid.Rows.Count > 0)
        {
            gvcomp.DataSource = DTGrid;
            gvcomp.DataBind();
            gvcomp.Visible = true;
        }
        else
        {
            gvcomp.Visible = false;
        }
    }
    protected void gvcomp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "up")
        {
            Response.Redirect("FrmAddLoginRegistration.aspx?string=" + Convert.ToInt64(e.CommandArgument.ToString()) + "&other" + Resturl(142));
        }
        else if (e.CommandName == "active")
        {
            DataTable DtActive = Lo.RetriveCodeUser(Convert.ToInt64(e.CommandArgument.ToString()));
            if (DtActive.Rows.Count > 0)
            {
                if (DtActive.Rows[0]["IsActive"].ToString().Trim() == "Y")
                {
                    Int32 Update = Lo.UpdateIsActiveCode("tbl_trn_Login", "N", "EmpID", Convert.ToInt64(e.CommandArgument.ToString()));
                    Int32 Update1 = Lo.UpdateIsActiveCode("tbl_mst_Employee", "N", "EmpID", Convert.ToInt64(e.CommandArgument.ToString()));
                    if (Update != 0)
                    {

                        diverror.InnerHtml = "Record deactive successfully";
                        diverror.Attributes.Add("class", "alert alert-success");
                        bindgrid();
                    }
                    else
                    {
                        diverror.InnerHtml = "Record not update";
                        diverror.Attributes.Add("class", "alert alert-danger");
                    }
                }
                else
                {
                    Int32 Update = Lo.UpdateIsActiveCode("tbl_trn_Login", "Y", "EmpID", Convert.ToInt64(e.CommandArgument.ToString()));
                    Int32 Update1 = Lo.UpdateIsActiveCode("tbl_mst_Employee", "Y", "EmpID", Convert.ToInt64(e.CommandArgument.ToString()));
                    if (Update != 0)
                    {
                        diverror.InnerHtml = "Record active successfully";
                        diverror.Attributes.Add("class", "alert alert-success");
                        bindgrid();
                    }
                    else
                    {
                        diverror.InnerHtml = "Record not update";
                        diverror.Attributes.Add("class", "alert alert-danger");
                    }
                }
            }
        }
    }
    public string Resturl(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }
}