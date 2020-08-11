using BuisnessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdateCaseFields : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private Common Co = new Common();
    private string HFRevisitDate;
    private string Email;
    private string Customer;
    private string PartyName;
    private DateTime LastStatusUpdateDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindProductName();
        }
    }
    protected DataTable insert()
    {
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        if (txtsearch.Text != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "[Customer_Name]" + "like ";
            dr["Value"] = "'%" + txtsearch.Text + "%' or [Logical_Circuit_Id] like '%" + txtsearch.Text + "%' or [Subs_Del_No] like '%" + txtsearch.Text + "%' or [Party_Name] like '%" + txtsearch.Text + "%'";
            insert.Rows.Add(dr);
        }
        if (ddlcategory.SelectedItem.Text != "--Select--")
        {
            dr = insert.NewRow();
            dr["Column"] = "Product_Name" + "="; ;
            dr["Value"] = "'" + ddlcategory.SelectedItem.Text + "'";
            insert.Rows.Add(dr);
        }
        dr = insert.NewRow();
        dr["Column"] = "[Eligible_Not_Eligible]=";
        dr["Value"] = "'Eligible' and Is_CLosed!='Y'";
        insert.Rows.Add(dr);
        return insert;
    }
    protected DataTable BindInsert()
    {
        return Lo.AllSearchCode(insert(), ddlcategory.SelectedItem.Text, "");
    }
    protected void SeachResult()
    {
        try
        {
            DataTable DtSearchResult = BindInsert();
            if (DtSearchResult.Rows.Count > 0)
            {
                gvexcel.DataSource = DtSearchResult;
                gvexcel.DataBind();
                gvexcel.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Total Record Found: -" + DtSearchResult.Rows.Count.ToString() + "')", true);

            }
            else
            {
                gvexcel.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No Record Found,Case status updated or please select valid keyword')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void SeachResultMO()
    {
        try
        {
            string compname = "";
            DataTable DtSearchResult = Lo.RetriveCodeRescheduled_APPOINTMENT(txtsearch.Text, txtsearch.Text, "", "", "", "", "RetriveMoCompGroup");
            if (DtSearchResult.Rows.Count > 0)
            {
                compname = DtSearchResult.Rows[0]["Party_Name"].ToString();
                DataTable DtMoFindResult = Lo.RetriveCodeRescheduled_APPOINTMENT(compname, "", "", "", "", "", "RetriveMo");
                if (DtMoFindResult.Rows.Count > 0)
                {
                    gvexcel.DataSource = DtMoFindResult;
                    gvexcel.DataBind();
                    gvexcel.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Total Record Found: -" + DtSearchResult.Rows.Count.ToString() + "')", true);

                }
                else
                {
                    gvexcel.Visible = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No Record Found,Case status updated or please select valid keyword')", true);
                }
            }
            else
            {
                gvexcel.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No Record Found,Case status updated or please select valid keyword')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btnsearchrecord_Click(object sender, EventArgs e)
    {
        if (ddlcategory.SelectedItem.Value != "0" && txtsearch.Text != "")
        {
            if (ddlcategory.SelectedItem.Text != "MO")
            {
                SeachResult();
                uppan.Visible = false;
            }
            else if (ddlcategory.SelectedItem.Text == "MO")
            {
                SeachResultMO();
                uppan.Visible = false;
            }
        }
    }
    protected void gvexcel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "UpdateStatus")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                ViewState["Id"] = e.CommandArgument.ToString();
                ViewState["Party"] = gvexcel.Rows[rowIndex].Cells[1].Text;
                txtemail.Text = gvexcel.Rows[rowIndex].Cells[2].Text;
                txtname.Text = gvexcel.Rows[rowIndex].Cells[3].Text;
                txtmobile.Text = gvexcel.Rows[rowIndex].Cells[4].Text;
                uppan.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
    }
    protected void BindProductName()
    {
        DataTable DtBind = Lo.RetriveCodeLocal(0, "", "", "", "RetriveProduct");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlcategory, DtBind, "ProductName", "ProductId");
            ddlcategory.Items.Insert(0, "Select Product Name");
        }
    }
    protected void btnupdateprescheduledtimedate_Click(object sender, EventArgs e)
    {
        string UpdatePreschDatetime = "";
        try
        {
            if (txtname.Text != "" && txtemail.Text != "" && txtmobile.Text != "")
            {

                if (ddlcategory.SelectedItem.Text != "MO")
                {
                    UpdatePreschDatetime = Lo.UpdateCode(txtemail.Text.Trim(), txtmobile.Text.Trim(), txtname.Text.Trim(), "", "", ViewState["Id"].ToString(), "", "", "", "", "updatefield");
                }
                else if (ddlcategory.SelectedItem.Text == "MO")
                {
                    UpdatePreschDatetime = Lo.UpdateCode(txtemail.Text.Trim(), txtmobile.Text.Trim(), txtname.Text.Trim(), "", "", ViewState["Party"].ToString(), "", "", "", "", "updatefieldmo");
                }
                if (UpdatePreschDatetime == "true")
                {
                    cleartext();
                    string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("field update succussfully");
                    string script = string.Format("alert({0});window.location ='Update-Field';", message);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                }
                else
                {
                    string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("field not update some error occure");
                    string script = string.Format("alert({0});window.location ='Update-Field';", message);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void cleartext()
    {
        txtmobile.Text = "";
        txtsearch.Text = "";
        txtemail.Text = "";
        txtname.Text = "";
        ViewState["Id"] = null;
        ViewState["Party"] = null;
    }
}