using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using System.IO;
using System.Configuration;

public partial class RescheduleClientVisit : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Common Co = new Common();
    string HFRevisitDate;
    string Email;
    string Customer;
    string PartyName;
    DateTime LastStatusUpdateDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
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
            dr["Column"] = "[Customer Name]" + "like ";
            dr["Value"] = "'%" + txtsearch.Text + "%' or [Logical Circuit Id] like '%" + txtsearch.Text + "%' or [Subs Del No] like '%" + txtsearch.Text + "%' or [Party Name] like '%" + txtsearch.Text + "%'";
            insert.Rows.Add(dr);
        }
        if (ddlprductname.SelectedItem.Text != "--Select--")
        {
            dr = insert.NewRow();
            dr["Column"] = "ProductName" + "="; ;
            dr["Value"] = "'" + ddlprductname.SelectedItem.Text + "'";
            insert.Rows.Add(dr);
        }
        dr = insert.NewRow();
        dr["Column"] = "[Eligible/Not Eligible]=";
        dr["Value"] = "'Eligible' and IsActive='Y' and IsCLosed!='Y' and (StatusByFE is null or StatusByFE='' or StatusByFE='Negative')  and (PreMeetingDate is null or PreMeetingDate='')";
        insert.Rows.Add(dr);
        return insert;
    }
    protected DataTable BindInsert()
    {
        return Lo.AllSearchCode(this.insert(), "tbl_trn_RawDataProductName");
    }
    protected void SeachResult()
    {
        try
        {
            DataTable DtSearchResult = this.BindInsert();
            if (DtSearchResult.Rows.Count > 0)
            {
                gvexcel.DataSource = DtSearchResult;
                gvexcel.DataBind();
                gvexcel.Visible = true;
                diverror.Visible = true;
                diverror.InnerHtml = "Total Record Found:-" + DtSearchResult.Rows.Count.ToString();
                diverror.Attributes.Add("Class", "alert alert-success");
            }
            else
            {
                gvexcel.Visible = false;
                diverror.Visible = true;
                diverror.InnerHtml = "No Record Found,Case status updated or please select valid keyword";
                diverror.Attributes.Add("Class", "alert alert-danger");
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
            DataTable DtSearchResult = Lo.RetriveCodeWithContidion("Select [Party Name],[Fe Name],[Allocated Date] from tbl_trn_RawData where ([Party Name]='" + txtsearch.Text + "' or [Billed Ext Id]='" + txtsearch.Text + "') and ProductName='MO' and [Eligible/Not Eligible]='Eligible' and IsActive='Y' and IsCLosed!='Y' and (StatusByFE is null or StatusByFE='' or StatusByFE='Negative') Group by [Party Name],[Allocated Date],[Fe Name]");
            if (DtSearchResult.Rows.Count > 0)
            {
                //for (int i = 0; DtSearchResult.Rows.Count > i; i++)
                //{
                    compname = DtSearchResult.Rows[0]["Party Name"].ToString();
               // }
                DataTable DtMoFindResult = Lo.RetriveCodeWithContidion("Select top 1 * from tbl_trn_RawData where ProductName='MO' and (StatusByFE is null or StatusByFE='' or StatusByFE='Negative')  and [Party Name] = '" + compname + "'");
                if (DtMoFindResult.Rows.Count > 0)
                {
                    gvexcel.DataSource = DtMoFindResult;
                    gvexcel.DataBind();
                    gvexcel.Visible = true;
                    diverror.Visible = true;
                    diverror.InnerHtml = "Total Record Found:-" + DtSearchResult.Rows.Count.ToString();
                    diverror.Attributes.Add("Class", "alert alert-success");
                }
                else
                {
                    gvexcel.Visible = false;
                    diverror.Visible = true;
                    diverror.InnerHtml = "No Record Found,Case status updated or please select valid keyword";
                    diverror.Attributes.Add("Class", "alert alert-danger");
                }
            }
            else
            {
                gvexcel.Visible = false;
                diverror.Visible = true;
                diverror.InnerHtml = "No Record Found,Case status updated or please select valid keyword";
                diverror.Attributes.Add("Class", "alert alert-danger");
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
                diverror.Visible = false;
                diverror.InnerHtml = "";
                divupdatebyfe.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
            }
        }
        catch (Exception ex)
        {
            diverror.InnerHtml = ex.Message;
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void gvexcel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblcompliancedate = e.Row.FindControl("lblcomp") as Label;
            Label lbllogcirid = e.Row.FindControl("lbllogcirid") as Label;
            Label lblbillextid = e.Row.FindControl("lblbillextid") as Label;
            Label lblsubs = e.Row.FindControl("lblsubs") as Label;
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
            if (ddlprductname.SelectedItem.Text == "MO")
            {
                lblbillextid.Visible = true;
                lbllogcirid.Visible = false;
                lblsubs.Visible = false;
            }
            else if (ddlprductname.SelectedItem.Text == "PRI-Fixed Line")
            {
                lblbillextid.Visible = false;
                lbllogcirid.Visible = false;
                lblsubs.Visible = true;
            }
            else if (ddlprductname.SelectedItem.Text == "Leased Line")
            {
                lblbillextid.Visible = false;
                lbllogcirid.Visible = true;
                lblsubs.Visible = false;
            }
        }
    }
    #endregion
    #region BindDataDropDownCOde"
    protected void BindProductName()
    {
        DataTable DtBind = Lo.RetriveBindDDL("ProductId", "ProductName", "tbl_mst_Product  where IsActive='Y'");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlprductname, DtBind, "ProductName", "ProductId");
            ddlprductname.Items.Insert(0, "Select Product Name");
        }
    }
    protected void BindFE()
    {
        DataTable DtBind = Lo.RetriveBindDDL("EmpID", "Name", "tbl_mst_Employee where Category='Field Executive' and IsActive='Y'");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlselectfe, DtBind, "Name", "EmpID");
            ddlselectfe.Items.Insert(0, "Select Field Executive");
        }
    }
    #endregion
    #region UpdateStatusCode
    Int32 UpdatePreschDatetime;
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
                    UpdatePreschDatetime = Lo.UpdateCodeOther("tbl_trn_RawData", "Set [Verification Date]='" + hfupdatedate.Value + "', PreMeetingDate='" + FinalDtPrescheduledDate + "',PreMeetingTime='" + TPreSchTime + "',[FE Name]='" + ddlselectfe.SelectedItem.Text + "',StatusByFE=''", "UniqueID='" + hflocsrid.Value + "'and [Eligible/Not Eligible]='Eligible' and IsClosed!='Y' and (StatusByFE is null or StatusByFE='' or StatusByFE='Negative') and StatusUpdateDate='" + hfupdatedate.Value + "'");
                }
                else if (ddlprductname.SelectedItem.Text == "MO")
                {
                    UpdatePreschDatetime = Lo.UpdateCodeOther("tbl_trn_RawData", "Set [Verification Date]='" + hfupdatedate.Value + "',  PreMeetingDate='" + FinalDtPrescheduledDate + "',PreMeetingTime='" + TPreSchTime + "',[FE Name]='" + ddlselectfe.SelectedItem.Text + "',StatusByFE=''", "[Party Name]='" + hfPartyNameDisplay.Value + "'and [Eligible/Not Eligible]='Eligible' and IsClosed!='Y' and (StatusByFE is null or StatusByFE='' or StatusByFE='Negative') and StatusUpdateDate='" + hfupdatedate.Value + "'");
                }
                if (UpdatePreschDatetime != 0)
                {
                    // prescheduledmail();
                    cleartext();
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "Prescheduled assign to fe succussfully";
                    divmssgpopup.Attributes.Add("Class", "alert alert-success");
                    var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Prescheduled assign to fe succussfully");
                    var script = string.Format("alert({0});window.location ='Assign-Reschedule-Client-Visit-FE';", message);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "Prescheduled not assign some error occure";
                    divmssgpopup.Attributes.Add("Class", "alert alert-danger");
                    var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Prescheduled not assign some error occure");
                    var script = string.Format("alert({0});window.location ='Assign-Reschedule-Client-Visit-FE';", message);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
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
    protected void prescheduledmail()
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/SuccessorFailVerification.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Message}", " This for your information that verification as per DoT regulations for Leased Line has been resechduled date and time is " + txtdate.Text + "-" + txttime.Text + ". <br /><br />Thanks for choosing Airtel as your preferred service partner. <br /><br />assuring you our best services always. <br /><br />");
        SendMail s;
        s = new SendMail();

        s.CreateMail("verification@rcpl.in", hfEmailCustomer.Value, "Airtel pre schedule status email!!", body, "Preschedule");
        // s.CreateMail("mohdwali@globalitpoint.com", "mohdwali@globalitpoint.com", "Airtel!!", body);
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
    }
    protected void cleartext()
    {
        txttime.Text = "";
        txtsearch.Text = "";
        txtdate.Text = "";
        //ddllocation.SelectedValue = "0";
        //ddlprductname.SelectedValue = "0";
        //ddlselectfe.SelectedValue = "0";
    }
    #endregion
}
