using BuisnessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RescheduleClientVisit : System.Web.UI.Page
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
            BindFE();
        }
    }
    #region SearchCOde
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (ddlprductname.SelectedItem.Value != "0" && txtsearch.Text != "")
        {
            if (ddlprductname.SelectedItem.Text != "MO")
            {
                SeachResult();
            }
            else if (ddlprductname.SelectedItem.Text == "MO")
            {
                SeachResultMO();
            }
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
        if (ddlprductname.SelectedItem.Text != "--Select--")
        {
            dr = insert.NewRow();
            dr["Column"] = "Product_Name" + "="; ;
            dr["Value"] = "'" + ddlprductname.SelectedItem.Text + "'";
            insert.Rows.Add(dr);
        }
        dr = insert.NewRow();
        dr["Column"] = "[Eligible_Not_Eligible]=";
        dr["Value"] = "'Eligible' and Is_CLosed!='Y' and (Status_By_FE='Pending' or Status_By_FE='Negative')  and (Pre_Meeting_Date is null or Pre_Meeting_Date='')";
        insert.Rows.Add(dr);
        return insert;
    }
    protected DataTable BindInsert()
    {
        return Lo.AllSearchCode(insert(), ddlprductname.SelectedItem.Text,"");
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
                DataTable DtMoFindResult = Lo.RetriveCodeRescheduled_APPOINTMENT(compname, "", "", "", "", "",  "RetriveMo");
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
    #endregion
    #region Gridview Code or Commande Code"
    protected void gvexcel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "UpdateStatus")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                string lblcompliancedate = (gvexcel.Rows[rowIndex].FindControl("lblcomp") as Label).Text;
                Email = (gvexcel.Rows[rowIndex].FindControl("hfemail") as HiddenField).Value;
                Customer = (gvexcel.Rows[rowIndex].FindControl("Customer") as HiddenField).Value;
                PartyName = (gvexcel.Rows[rowIndex].FindControl("hfpartyname") as HiddenField).Value;
                LastStatusUpdateDate = Convert.ToDateTime((gvexcel.Rows[rowIndex].FindControl("hflaststatusupdatedate") as HiddenField).Value);
                hfupdatedate.Value = LastStatusUpdateDate.ToString("dd-MMM-yyyy");
                hfPartyNameDisplay.Value = PartyName.ToString();
                hflocsrid.Value = e.CommandArgument.ToString();
                hfEmailCustomer.Value = Email;
                hfNameCustomer.Value = Customer;
                divupdatebyfe.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
    }
    protected void gvexcel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblcompliancedate = e.Row.FindControl("lblcomp") as Label;
            HiddenField FeStatus = e.Row.FindControl("hfstatusfe") as HiddenField;
            HiddenField HfRevisitDate = e.Row.FindControl("hfrevisitdate") as HiddenField;
            if (HfRevisitDate.Value != "")
            {
                DateTime RevisitDate = Convert.ToDateTime(HfRevisitDate.Value);
                HFRevisitDate = RevisitDate.ToString("dd/MMM/yyyy");
            }
            DateTime compdate = Convert.ToDateTime(lblcompliancedate.Text);
            DateTime currentdate = Convert.ToDateTime(DateTime.Now);
            if (compdate < currentdate)
            {
                lblcompliancedate.ForeColor = System.Drawing.Color.Red;
            }
            if (FeStatus.Value == "Pending")
            {
                GridViewRow grv = e.Row;
                if (HFRevisitDate == DateTime.Now.ToString("dd/MMM/yyyy"))
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.Visible = true;
                    e.Row.ForeColor = System.Drawing.Color.White;
                }
            }
        }
    }
    #endregion
    #region BindDataDropDownCOde"
    protected void BindProductName()
    {
        DataTable DtBind = Lo.RetriveCodeLocal(0, "", "", "", "RetriveProduct");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlprductname, DtBind, "ProductName", "ProductId");
            ddlprductname.Items.Insert(0, "Select Product Name");
        }
    }
    protected void BindFE()
    {
        DataTable DtBind = Lo.RetriveCodeLocal(0, "Field Executive", "", "", "RetriveUserNormalType");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlselectfe, DtBind, "Name", "EmpID");
            ddlselectfe.Items.Insert(0, "Select Field Executive");
        }
    }
    #endregion
    #region UpdateStatusCode
    private string UpdatePreschDatetime;
    protected void btnupdateprescheduledtimedate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlselectfe.SelectedItem.Text != "Select Field Executive" && txtdate.Text != "" && txttime.Text != "")
            {
                DateTime DtPrescheduledDate = Convert.ToDateTime(txtdate.Text);
                string FinalDtPrescheduledDate = DtPrescheduledDate.ToString("dd/MMM/yyyy");
                DateTime TPreSchTime = Convert.ToDateTime(txttime.Text);
                string FindTpreSchTime = TPreSchTime.ToString("hh:mm:ss");
                if (ddlprductname.SelectedItem.Text != "MO")
                {
                    UpdatePreschDatetime = Lo.UpdateCode(hfupdatedate.Value, FinalDtPrescheduledDate, FindTpreSchTime, ddlselectfe.SelectedItem.Text, hflocsrid.Value, hfupdatedate.Value, "", "", "", "", "PRILLUpdate");
                }
                else if (ddlprductname.SelectedItem.Text == "MO")
                {
                    UpdatePreschDatetime = Lo.UpdateCode(hfupdatedate.Value, FinalDtPrescheduledDate, FindTpreSchTime, ddlselectfe.SelectedItem.Text, hfPartyNameDisplay.Value, hfupdatedate.Value, "", "", "", "", "MOUpdate");
                }
                if (UpdatePreschDatetime == "true")
                {
                    cleartext();
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "Prescheduled assign to fe succussfully";
                    divmssgpopup.Attributes.Add("Class", "alert alert-success");
                    string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Prescheduled assign to fe succussfully");
                    string script = string.Format("alert({0});window.location ='Assign-Reschedule-Client-Visit-FE';", message);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "Prescheduled not assign some error occure";
                    divmssgpopup.Attributes.Add("Class", "alert alert-danger");
                    string message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Prescheduled not assign some error occure");
                    string script = string.Format("alert({0});window.location ='Assign-Reschedule-Client-Visit-FE';", message);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                }
            }
        }
        catch (Exception ex)
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = ex.Message.ToString();
            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void cleartext()
    {
        txttime.Text = "";
        txtsearch.Text = "";
        txtdate.Text = "";
    }
    #endregion
}