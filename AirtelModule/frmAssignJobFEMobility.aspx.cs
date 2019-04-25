using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using System.Configuration;
using System.IO;
using System.Net;
using ClosedXML.Excel;



public partial class frmAssignJobFEMobility : System.Web.UI.Page
{
    #region variable
    Logic Lo = new Logic();
    Common Co = new Common();
    string Email;
    string HFRevisitDate;
    string hfPreVisit;
    PagedDataSource pgsource = new PagedDataSource();
    int firstindex, lastindex;
    #endregion
    #region Other Basic Function
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            GetFeBasicDetail();
            BindAssignJobCompany();
        }
    }
    protected void BindAssignJobCompany()
    {
        DataTable DtFetchcompgropby = new DataTable();
        if (ddlprodbycat.SelectedItem.Value == "0")
        {
            DtFetchcompgropby = Lo.RetriveCodeWithContidion("select count([party name])as [Count] ,[party name],[Allocated Date],[ProdTypeName] from tbl_trn_RawData where productname='MO' and [FE Name]='" + Session["Name"] + "' and (StatusbyFE is null or StatusbyFE='' ) and isactive='Y' and isclosed='N' and [Eligible/Not Eligible]='Eligible' group by [party name],[Allocated Date],[ProdTypeName]  order by [Allocated Date] desc");
        }
        else if (ddlprodbycat.SelectedItem.Value == "MO-Periodic")
        {
            DtFetchcompgropby = Lo.RetriveCodeWithContidion("select count([party name])as [Count] ,[party name],[Allocated Date],[ProdTypeName] from tbl_trn_RawData where productname='MO' and [FE Name]='" + Session["Name"] + "' and (StatusbyFE is null or StatusbyFE='' ) and isactive='Y' and isclosed='N' and [Eligible/Not Eligible]='Eligible' and ProdTypeName='MO-Periodic' group by [party name],[Allocated Date],[ProdTypeName]  order by [Allocated Date] desc");
        }
        else if (ddlprodbycat.SelectedItem.Value == "MO-Quarterly")
        {
            DtFetchcompgropby = Lo.RetriveCodeWithContidion("select count([party name])as [Count] ,[party name],[Allocated Date],[ProdTypeName] from tbl_trn_RawData where productname='MO' and [FE Name]='" + Session["Name"] + "' and (StatusbyFE is null or StatusbyFE='' ) and isactive='Y' and isclosed='N' and [Eligible/Not Eligible]='Eligible' and ProdTypeName='MO-Quarterly' group by [party name],[Allocated Date],[ProdTypeName]  order by [Allocated Date] desc");
        }
        if (DtFetchcompgropby.Rows.Count > 0)
        {
            //dlcomp.DataSource = DtFetchcompgropby;
            //dlcomp.DataBind();
            //dlcomp.Visible = true;
            //totalcount.InnerText = "Total company for audit :- " + DtFetchcompgropby.Rows.Count.ToString();
            //diverror.Visible = false;
            //diverror.InnerHtml = "";

            DataTable dtads = DtFetchcompgropby;
            pgsource.DataSource = dtads.DefaultView;
            pgsource.AllowPaging = true;
            pgsource.PageSize = 25;
            pgsource.CurrentPageIndex = pagingCurrentPage;
            ViewState["totpage"] = pgsource.PageCount;
            lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
            lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
            lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
            lnkbtnPgFirst.Enabled = !pgsource.IsFirstPage;
            lnkbtnPgLast.Enabled = !pgsource.IsLastPage;
            pgsource.DataSource = dtads.DefaultView;
            dlcomp.DataSource = pgsource;
            dlcomp.DataBind();
            DataListPagingMethod();
            dlcomp.Visible = true;
            totalcount.InnerText = "Total company for audit :- " + DtFetchcompgropby.Rows.Count.ToString();
            diverror.Visible = false;
            diverror.InnerHtml = "";


        }
        else
        {
            dlcomp.Visible = false;
            diverror.Visible = true;
            diverror.InnerHtml = "No job assign";
            diverror.Attributes.Add("class", "alert alert-danger");
            totalcount.InnerText = "Toal company for audit :- 0";
        }
    }
    protected void cleartext()
    {
        txtactivenuber.Text = "";
        txtdeactivenumber.Text = "";
        txtnewaddress.Text = "";
        txtnewnumber.Text = "";
        txtoptsuces.Text = "";
        txtotpnegative.Text = "";
        txtotprevisitcustmer.Text = "";
        txtpersonemetmobilenosuccess.Text = "";
        txtpersonmetdesig.Text = "";
        txtpersonmetemailid.Text = "";
        txtpersonmetemailidsuccess.Text = "";
        txtpersonmetmobno.Text = "";
        txtpersonmetname.Text = "";
        txtpersonmetnamesuccess.Text = "";
        txtremarkcust.Text = "";
        txtrevisitpersonmetdesig.Text = "";
        txtrevisitpersonmetemail.Text = "";
        txtrevisitpersonmetname.Text = "";
        txtrevisitpersonmobileno.Text = "";
        txtrevisittimeanddate.Text = "";
        txtsearch.Text = "";
        txtsuccpersonmetdesig.Text = "";
        txttimerevisit.Text = "";
        ddlairmana.SelectedValue = "01";
        ddlairper.SelectedValue = "01";
        ddlcondition.SelectedIndex = 0;
        ddlnum.SelectedIndex = 0;
        ddlnump.SelectedIndex = 0;
        ddlprodbycat.SelectedIndex = 0;
        ddlsamper.SelectedValue = "01";
        ddltlper.SelectedValue = "01";
        hfAllocationDate.Value = "";
        hfbilledextid1.Value = "";
        hfcompname.Value = "";
        hfcustomerorpartyname.Value = "";
        hfEmailCustomer.Value = "";
        // hfFeName.Value = "";
        hflocsrid.Value = "";
        hfotp.Value = "";
        hfPreVisit = "";
        HFRevisitDate = "";
    }
    protected void GetFeBasicDetail()
    {
        try
        {
            DataTable DtBasicdetilofFE = Lo.RetriveCodeWithContidion("select Name from tbl_mst_Employee where Email='" + Session["LoginEmail"].ToString() + "'");
            if (DtBasicdetilofFE.Rows.Count > 0)
            {
                hfFeName.Value = DtBasicdetilofFE.Rows[0]["Name"].ToString();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #endregion
    #region Gridview or DataList Code or Commande Code"
    protected void gvexcel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewDetail")
            {
                DataTable DtSearchView = Lo.RetriveCodeAllExcelRecordWhereCondition("UniqueID =" + Convert.ToInt32(e.CommandArgument.ToString()) + "");
                if (DtSearchView.Rows.Count > 0)
                {
                    dluploadexcelrecord.DataSource = DtSearchView;
                    dluploadexcelrecord.DataBind();
                    dluploadexcelrecord.Visible = true;
                    divshow1.Visible = true;
                    div2.Visible = false;
                    panviewnum.Visible = false;
                    panmoallpanel.Visible = false;
                    cleartext();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                }
                else
                {
                    diverror.InnerHtml = "No record found.";
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
            }
            else if (e.CommandName == "UpdateStatus")
            {
                cleartext();
                GridViewRow gvexcel = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvexcel.RowIndex;
                string lblcompliancedate = (gvexcel.FindControl("lblcomp") as Label).Text;
                string stattusbyfe = (gvexcel.FindControl("hfstatusfe") as HiddenField).Value;

                if (stattusbyfe == "" || stattusbyfe == null || stattusbyfe == "Pending")
                {
                    hfcompname.Value = (gvexcel.FindControl("lblcompnamemo") as Label).Text;
                    Email = (gvexcel.FindControl("hfemail") as HiddenField).Value;
                    hfcustomerorpartyname.Value = (gvexcel.FindControl("Customer") as HiddenField).Value;
                    hfbilledextid1.Value = (gvexcel.FindControl("hfbilledextid") as HiddenField).Value;
                    hfAllocationDate.Value = (gvexcel.FindControl("hfGridAllcationDate") as HiddenField).Value;
                    hflocsrid.Value = e.CommandArgument.ToString();
                    hfEmailCustomer.Value = Email;
                    panmoallpanel.Visible = true;
                    divshow1.Visible = false;
                    div2.Visible = true;
                    panviewnum.Visible = false;
                    pansuccess.Visible = false;
                    pannegativecondition.Visible = false;
                    panrevisitcondition.Visible = false;
                    panaddressorcompclose.Visible = false;
                    ddlcondition.Enabled = true;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                }
                else
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = "Status already update please refresh page for see updated list.";
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
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
            HiddenField FeStatus = e.Row.FindControl("hfstatusfe") as HiddenField;
            HiddenField HfRevisitDate = e.Row.FindControl("hfrevisitdate") as HiddenField;
            HiddenField hfPremmetingdate = e.Row.FindControl("hfPreMeetingDate") as HiddenField;
            if (HfRevisitDate.Value != "")
            {
                DateTime RevisitDate = Convert.ToDateTime(HfRevisitDate.Value);
                HFRevisitDate = RevisitDate.ToString("dd/MMM/yyyy");
            }
            if (hfPremmetingdate.Value != "")
            {
                DateTime PreVisit = Convert.ToDateTime(hfPremmetingdate.Value);
                hfPreVisit = PreVisit.ToString("dd/MMM/yyyy");
            }
            //error come if fecompliance date will not be enter or null
            DateTime compdate = Convert.ToDateTime(lblcompliancedate.Text);
            DateTime currentdate = Convert.ToDateTime(DateTime.Now);
            if (compdate <= currentdate.AddDays(-2))
            {
                lblcompliancedate.ForeColor = System.Drawing.Color.DarkRed;
            }
            if (FeStatus.Value == "Pending")
            {
                GridViewRow grv = e.Row;
                if (Convert.ToDateTime(HFRevisitDate) >= Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy")))
                {
                    if (HFRevisitDate == DateTime.Now.ToString("dd/MMM/yyyy"))
                    {
                        e.Row.BackColor = System.Drawing.Color.Red;
                        e.Row.Visible = true;
                        e.Row.ForeColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        e.Row.Visible = false;
                    }
                }

            }
            if (hfPreVisit != "")
            {
                GridViewRow grv = e.Row;
                if (hfPreVisit == DateTime.Now.ToString("dd/MMM/yyyy"))
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.Visible = true;
                    e.Row.ForeColor = System.Drawing.Color.White;
                }
            }
        }
    }
    protected void dlcomp_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GridView gvexcel = (GridView)e.Item.FindControl("gvexcel");
                Label hfid = (Label)e.Item.FindControl("lblcmpaname") as Label;

                DataTable DtAssignJob = Lo.RetriveCodeWithContidion("SELECT top 1 UniqueID,[Billed Ext ID],StatusByFE,[Cust Email],[Customer Name],[Revisit Date Time],PreMeetingDate,[Allocated Date],[Party Name],[Billing Address],PRODUCT_TYPE,Coordinator_Name,Coordinator_Contact_number,AppointmentMailDatetime,FEComplianceDate,ProdTypeName  from tbl_trn_RawData where [FE Name]='" + Session["Name"] + "' and IsClosed !='Y' and [Eligible/Not Eligible]='Eligible' and (StatusByFE='' or StatusByFE is null or StatusByFE='Pending') and ProductName='MO' and [Party Name]='" + hfid.Text + "'   order by [Allocated Date],[Party Name] desc");
                if (DtAssignJob.Rows.Count > 0)
                {
                    gvexcel.DataSource = DtAssignJob;
                    gvexcel.DataBind();
                    gvexcel.Visible = true;
                }
                else
                {
                    gvexcel.Visible = false;
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
    protected void dlcomp_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "Viewnum")
        {
            dlcomp.SelectedIndex = e.Item.ItemIndex;
            string lblacdate = ((Label)dlcomp.SelectedItem.FindControl("hfallocateddate")).Text;
            DataTable DtGetCompNum = Lo.RetriveCodeWithContidion("select [Mobile No] from tbl_trn_RawData where ProductName='MO' and (StatusByFE='' or StatusByFE is null) and [Party Name]='" + e.CommandArgument.ToString() + "' and [Allocated Date]='" + lblacdate.ToString() + "'");
            if (DtGetCompNum.Rows.Count > 0)
            {
                gvnum.DataSource = DtGetCompNum;
                gvnum.DataBind();
                divshow1.Visible = false;
                panviewnum.Visible = true;
                div2.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
            }
        }
    }
    #endregion
    #region Select Process By DLL
    protected void ddlcondition_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcondition.SelectedValue == "01")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = false;
        }
        else if (ddlcondition.SelectedValue == "02")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = true;
            pannegativefirststep.Visible = true;
            divcustomershifted.Visible = true;
            ddlcondition.Enabled = false;
            btnnextnegative.Text = "Submit";
        }
        else if (ddlcondition.SelectedValue == "03")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            ddlcondition.Enabled = false;
            pannegativecondition.Visible = true;
            pannegativefirststep.Visible = false;
            pannegativesecondstep.Visible = false;
            panaddressorcompclose.Visible = true;
        }
        else if (ddlcondition.SelectedValue == "04")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = true;
            divcustomershifted.Visible = false;
            diventrynotallowd.Visible = false;
            ddlcondition.Enabled = false;
        }
        else if (ddlcondition.SelectedValue == "05")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = true;
            divcustomershifted.Visible = false;
            diventrynotallowd.Visible = true;
            ddlcondition.Enabled = false;
            divnegativeotp.Visible = false;
            diventrynotallowd.Visible = true;
            btnnextnegative.Text = "Submit";
        }
        else if (ddlcondition.SelectedValue == "06")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = true;
            divnegativeotp.Visible = true;
            divcustomershifted.Visible = false;
            diventrynotallowd.Visible = false;
            ddlcondition.Enabled = false;
        }
        else if (ddlcondition.SelectedValue == "07")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = true;
            divcustomershifted.Visible = false;
            diventrynotallowd.Visible = false;
            divnegativeotp.Visible = true;
            ddlcondition.Enabled = false;
        }
        else if (ddlcondition.SelectedValue == "08")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = true;
            divcustomershifted.Visible = false;
            diventrynotallowd.Visible = false;
            ddlcondition.Enabled = false;
            divnegativeotp.Visible = true;
        }
        else if (ddlcondition.SelectedValue == "09")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = true;
            divcustomershifted.Visible = false;
            diventrynotallowd.Visible = false;
            ddlcondition.Enabled = false;
            divnegativeotp.Visible = true;
        }
        else if (ddlcondition.SelectedValue == "10")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = true;
            pannegativecondition.Visible = false;
            divcustomershifted.Visible = false;
            diventrynotallowd.Visible = false;
            ddlcondition.Enabled = false;
        }
        else if (ddlcondition.SelectedValue == "11")
        {
            DataTable DtFetchcompgropby = Lo.RetriveCodeWithContidion("select count([party name])as [Count] ,[party name],[Allocated Date] from tbl_trn_RawData where productname='MO' and [FE Name]='" + Session["Name"] + "' and (StatusbyFE is null or StatusbyFE='' ) and [Party Name]='" + hfcompname.Value + "' and isactive='Y' and isclosed='N' and [Eligible/Not Eligible]='Eligible' and [Allocated Date]='" + hfAllocationDate.Value + "' group by [party name],[Allocated Date]");
            if (DtFetchcompgropby.Rows.Count > 0)
            {
                totalnumbercount.InnerText = "Total Number As Per Data " + DtFetchcompgropby.Rows[0]["Count"].ToString();
            }
            pansuccess.Visible = true;
            panrevisitcondition.Visible = false;
            pannegativecondition.Visible = false;
            divcustomershifted.Visible = false;
            diventrynotallowd.Visible = false;
            ddlcondition.Enabled = false;
        }
        else if (ddlcondition.SelectedValue == "12")
        {
            pansuccess.Visible = false;
            panrevisitcondition.Visible = false;
            ddlcondition.Enabled = false;
            pannegativecondition.Visible = true;
            pannegativefirststep.Visible = false;
            pannegativesecondstep.Visible = false;
            panaddressorcompclose.Visible = true;
        }
    }
    #endregion
    #region OTPCode
    protected void GenerateOTP()
    {
        try
        {
            string numbers = "1234567890";
            string characters = numbers;
            int length = int.Parse("6");
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            hfotp.Value = otp;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    private void send(string _mobileNo)
    {
        try
        {
            string requestUristring = string.Format("http://smsgateway.spicedigital.in/MessagingGateway//MessagePush?username=DefexpoT&password=Def@1234&messageType=text&mobile={0}&senderId=MYRCPL&message=" + "Dear Airtel Customer, Your Airtel verification OTP for the same is '" + hfotp.Value + "' Thank You.", _mobileNo);
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(requestUristring);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
            Int32 UpdateRevisitStatusByFE = Lo.UpdateCodeOther("tbl_trn_RawData", "Set [Mobile OTP]='" + hfotp.Value + "'", "[Party Name]='" + hfcompname.Value + "' and [Eligible/Not Eligible]='Eligible' and [Allocated Date]='" + hfAllocationDate.Value + "'");
            if (UpdateRevisitStatusByFE != 0)
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = ("Enter OTP for submit status.");
                divmssgpopup.Attributes.Add("Class", "alert alert-warning");
            }
            else
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = ("OTP not send to user some error occure.");
                divmssgpopup.Attributes.Add("Class", "alert alert-danger");
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    #endregion
    #region NegativeCaseSubmit"
    protected void btnnextnegative_Click(object sender, EventArgs e)
    {
        if (ddlcondition.SelectedValue == "02")
        {
            if (txtnewaddress.Text != "")
            {
                Int32 UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='SHIFTED', [New Address of Company]='" + txtnewaddress.Text + "',PersonMetMobileNo='" + txtpersonmetmobno.Text + "',PersonMetName='" + txtpersonmetname.Text + "',PersonMetEmail='" + txtpersonmetemailid.Text + "',PersonMetDesignation='" + txtpersonmetdesig.Text + "',ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Address by " + hfFeName.Value + " Met With Mr./Ms. " + txtpersonmetname.Text + " He/She Told This Compay Is Shifted From Here Last , And New Address Is:-NO ,',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1'", "[Eligible/Not Eligible]='Eligible' and [Party Name]='" + hfcompname.Value + "' and (StatusByFE='' or StatusByFE is null) and Productname ='MO' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                if (UpdateNewCompAddress != 0)
                {
                    DataTable DtNegative = Lo.RetriveCodeWithContidion("select [Account Number],[Party Name] as [Company Name],[Billing Address],[Coordinator_Name] as [Coordinator Name],[Coordinator_Contact_Email] as [Coordinator Email],[Coordinator_Contact_Number] as [Coordinator Contact],[RM_Contact_Number] as [RM No],[RM_Email] as [RM Email],[RM_NAME] as [RM Name],[ProductName] as Product,DateName(Month,[Allocated Date]) as [Month] ,[StatusByFE],[ReasonOfStatus],[Allocated Date] as [Date] from tbl_trn_RawData where Productname ='MO' and StatusByFE='Negative' and [Party Name]='" + hfcompname.Value + "' and [Eligible/Not Eligible]='Eligible' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                    if (DtNegative.Rows.Count > 0)
                    {
                        ExportExcel(DtNegative);
                    }
                    divmssgpopup.Visible = true;
                    cleartext();
                    var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    var script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "Status not update some error occure.";
                    divmssgpopup.Attributes.Add("class", "alert alert-danger");
                }
            }
            else
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = "Please fill new address";
                divmssgpopup.Attributes.Add("class", "alert alert-danger");
            }
        }
        else if (ddlcondition.SelectedValue == "05")
        {
            if (ddlsamper.SelectedValue != "01" && ddltlper.SelectedValue != "01" && ddlairmana.SelectedValue != "01" && ddlairper.SelectedValue != "01")
            {
                Int32 UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='ENA', ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Add by " + hfFeName.Value + " Met With Guard. He Said Entry Not Allowed Without Appointment',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1',IsCall='" + ddlsamper.SelectedItem.Text.Substring(0, 1) + "-" + ddltlper.SelectedItem.Text.Substring(0, 1) + "-" + ddlairper.SelectedItem.Text.Substring(0, 1) + "-" + ddlairmana.SelectedItem.Text.Substring(0, 1) + "'", "[Eligible/Not Eligible]='Eligible' and [Party Name]='" + hfcompname.Value + "' and (StatusByFE='' or StatusByFE is null) and Productname ='MO' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                if (UpdateNewCompAddress != 0)
                {
                    DataTable DtNegative = Lo.RetriveCodeWithContidion("select [Account Number],[Party Name] as [Company Name],[Billing Address],[Coordinator_Name] as [Coordinator Name],[Coordinator_Contact_Email] as [Coordinator Email],[Coordinator_Contact_Number] as [Coordinator Contact],[RM_Contact_Number] as [RM No],[RM_Email] as [RM Email],[RM_NAME] as [RM Name],[ProductName] as Product,DateName(Month,[Allocated Date]) as [Month] ,[StatusByFE],[ReasonOfStatus],[Allocated Date] as [Date] from tbl_trn_RawData where Productname ='MO' and StatusByFE='Negative' and [Party Name]='" + hfcompname.Value + "' and [Eligible/Not Eligible]='Eligible' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                    if (DtNegative.Rows.Count > 0)
                    {
                        ExportExcel(DtNegative);
                    }
                    divmssgpopup.Visible = true;
                    cleartext();
                    var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    var script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "Status not update some error occure.";
                    divmssgpopup.Attributes.Add("class", "alert alert-danger");
                }
            }
            else
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = "All field fill mandatory.";
                divmssgpopup.Attributes.Add("class", "alert alert-danger");
            }
        }
        else
        {
            if (txtpersonmetmobno.Text != "")
            {
                GenerateOTP();
                send(txtpersonmetmobno.Text);
                pannegativesecondstep.Visible = true;
                pannegativefirststep.Visible = false;
            }
            else
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = "All field fill mandatory";
                divmssgpopup.Attributes.Add("Class", "alert alert-danger");
            }
        }
    }
    protected void lblresendotpnegative_Click(object sender, EventArgs e)
    {
        GenerateOTP();
        if (ddlnum.SelectedItem.Value != "OTP")
        {
            send(txtpersonmetmobno.Text);
        }
        else
        {
            send(ddlnum.SelectedItem.Text);
        }
        divmssgpopup.Visible = true;
        divmssgpopup.InnerHtml = "OTP resend successfully";
        divmssgpopup.Attributes.Add("Class", "alert alert-danger");
    }
    protected void btnsubmitnegative_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlcondition.SelectedValue == "04")
            {
                DataTable DTOTPCheck = Lo.RetriveCodeAllExcelRecordWhereCondition("[Mobile OTP]='" + txtotpnegative.Text + "' and UniqueID='" + hflocsrid.Value + "' and [Eligible/Not Eligible]='Eligible' and (StatusByFE ='Pending' or StatusByFE is null or StatusByFE='') and [Allocated Date]='" + hfAllocationDate.Value + "'");
                if (DTOTPCheck.Rows.Count == 1)
                {
                    if (txtotpnegative.Text == DTOTPCheck.Rows[0]["Mobile OTP"].ToString())
                    {
                        string num;
                        if (ddlnum.SelectedItem.Value != "OTP")
                        {
                            num = ddlnum.SelectedItem.Text;
                        }
                        else
                        {
                            num = txtpersonmetmobno.Text;
                        }
                        Int32 UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='Already Disconnected', ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + "Visit Done At The Given Address by " + hfFeName.Value + " Met With Mr./Ms. " + txtpersonmetname.Text + " He/She Told We Already Disconnected The All Airtel Corporate Connection',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1',PersonMetMobileNo='" + num + "',PersonMetEmail='" + txtpersonmetemailid.Text + "',PersonMetDesignation='" + txtpersonmetdesig.Text + "',PersonMetName='" + txtpersonmetname.Text + "',[IsOTPVerified]='Y'", "[Eligible/Not Eligible]='Eligible' and [Party Name]='" + hfcompname.Value + "' and (StatusByFE='' or StatusByFE is null) and Productname ='MO' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                        if (UpdateNewCompAddress != 0)
                        {
                            DataTable DtNegative = Lo.RetriveCodeWithContidion("select [Account Number],[Party Name] as [Company Name],[Billing Address],[Coordinator_Name] as [Coordinator Name],[Coordinator_Contact_Email] as [Coordinator Email],[Coordinator_Contact_Number] as [Coordinator Contact],[RM_Contact_Number] as [RM No],[RM_Email] as [RM Email],[RM_NAME] as [RM Name],[ProductName] as Product,DateName(Month,[Allocated Date]) as [Month] ,[StatusByFE],[ReasonOfStatus],[Allocated Date] as [Date] from tbl_trn_RawData where Productname ='MO' and StatusByFE='Negative' and [Party Name]='" + hfcompname.Value + "' and [Eligible/Not Eligible]='Eligible' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                            if (DtNegative.Rows.Count > 0)
                            {
                                ExportExcel(DtNegative);
                            }
                            divmssgpopup.Visible = true;
                            cleartext();
                            var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                            var script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                        }
                        else
                        {
                            divmssgpopup.Visible = true;
                            divmssgpopup.InnerHtml = "Status not update some error occure.";
                            divmssgpopup.Attributes.Add("class", "alert alert-danger");
                        }
                    }
                    else
                    {
                        divmssgpopup.Visible = true;
                        divmssgpopup.InnerHtml = "Invalid OTP";
                        divmssgpopup.Attributes.Add("class", "alert alert-danger");
                    }
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "OTP not generated or Status updated earlier.";
                    divmssgpopup.Attributes.Add("class", "alert alert-danger");
                }
            }
            else if (ddlcondition.SelectedValue == "06" || ddlcondition.SelectedValue == "07" || ddlcondition.SelectedValue == "08" || ddlcondition.SelectedValue == "09")
            {
                Int32 UpdateNewCompAddress;
                DataTable DTOTPCheck = Lo.RetriveCodeAllExcelRecordWhereCondition("[Mobile OTP]='" + txtotpnegative.Text + "' and UniqueID='" + hflocsrid.Value + "' and [Eligible/Not Eligible]='Eligible' and (StatusByFE ='Pending' or StatusByFE is null or StatusByFE='') and [Allocated Date]='" + hfAllocationDate.Value + "'");
                if (DTOTPCheck.Rows.Count == 1)
                {
                    if (txtotpnegative.Text == DTOTPCheck.Rows[0]["Mobile OTP"].ToString())
                    {
                        string num;
                        if (ddlnum.SelectedItem.Value != "OTP")
                        {
                            num = ddlnum.SelectedItem.Text;
                        }
                        else
                        {
                            num = txtpersonmetmobno.Text;
                        }
                        if (ddlcondition.SelectedValue == "06")
                        {
                            UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='NOT INTERESTED', ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Address by " + hfFeName.Value + " Met With Mr./Ms. " + txtpersonmetname.Text + " He/She Said I am Not Interested To Any Types Of Verification And reason Not Disclosed',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1',PersonMetMobileNo='" + num + "',PersonMetEmail='" + txtpersonmetemailid.Text + "',PersonMetDesignation='" + txtpersonmetdesig.Text + "',PersonMetName='" + txtpersonmetname.Text + "',[IsOTPVerified]='Y'", "[Eligible/Not Eligible]='Eligible' and [Party Name]='" + hfcompname.Value + "' and (StatusByFE='' or StatusByFE is null) and Productname ='MO' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                            if (UpdateNewCompAddress != 0)
                            {
                                DataTable DtNegative = Lo.RetriveCodeWithContidion("select [Account Number],[Party Name] as [Company Name],[Billing Address],[Coordinator_Name] as [Coordinator Name],[Coordinator_Contact_Email] as [Coordinator Email],[Coordinator_Contact_Number] as [Coordinator Contact],[RM_Contact_Number] as [RM No],[RM_Email] as [RM Email],[RM_NAME] as [RM Name],[ProductName] as Product,DateName(Month,[Allocated Date]) as [Month] ,[StatusByFE],[ReasonOfStatus],[Allocated Date] as [Date] from tbl_trn_RawData where Productname ='MO' and StatusByFE='Negative' and [Party Name]='" + hfcompname.Value + "' and [Eligible/Not Eligible]='Eligible' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                                if (DtNegative.Rows.Count > 0)
                                {
                                    ExportExcel(DtNegative);
                                }
                                divmssgpopup.Visible = true;
                                cleartext();
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                                var script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                            }
                            else
                            {
                                divmssgpopup.Visible = true;
                                divmssgpopup.InnerHtml = "Status not update some error occure.";
                                divmssgpopup.Attributes.Add("class", "alert alert-danger");
                            }
                        }
                        else if (ddlcondition.SelectedValue == "07")
                        {
                            UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='Not Using', ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Address by " + hfFeName.Value + " Met With Mr./Ms:- " + txtpersonmetname.Text + " He/She Said We Are Not Using This Number',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1',PersonMetMobileNo='" + num + "',PersonMetEmail='" + txtpersonmetemailid.Text + "',PersonMetDesignation='" + txtpersonmetdesig.Text + "',PersonMetName='" + txtpersonmetname.Text + "',[IsOTPVerified]='Y'", "[Eligible/Not Eligible]='Eligible' and [Party Name]='" + hfcompname.Value + "' and (StatusByFE='' or StatusByFE is null) and Productname ='MO' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                            if (UpdateNewCompAddress != 0)
                            {
                                DataTable DtNegative = Lo.RetriveCodeWithContidion("select [Account Number],[Party Name] as [Company Name],[Billing Address],[Coordinator_Name] as [Coordinator Name],[Coordinator_Contact_Email] as [Coordinator Email],[Coordinator_Contact_Number] as [Coordinator Contact],[RM_Contact_Number] as [RM No],[RM_Email] as [RM Email],[RM_NAME] as [RM Name],[ProductName] as Product,DateName(Month,[Allocated Date]) as [Month] ,[StatusByFE],[ReasonOfStatus],[Allocated Date] as [Date] from tbl_trn_RawData where Productname ='MO' and StatusByFE='Negative' and [Party Name]='" + hfcompname.Value + "' and [Eligible/Not Eligible]='Eligible' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                                if (DtNegative.Rows.Count > 0)
                                {
                                    ExportExcel(DtNegative);
                                }
                                divmssgpopup.Visible = true;
                                cleartext();
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                                var script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                            }
                            else
                            {
                                divmssgpopup.Visible = true;
                                divmssgpopup.InnerHtml = "Status not update some error occure.";
                                divmssgpopup.Attributes.Add("class", "alert alert-danger");
                            }
                        }
                        else if (ddlcondition.SelectedValue == "08")
                        {
                            UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code=' Already Done', ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + "Visit Done At The Given Add by " + hfFeName.Value + " Met With Mr./Ms. " + txtpersonmetname.Text + "  He/She Told Verification Is Already Done ',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1',PersonMetMobileNo='" + num + "',PersonMetEmail='" + txtpersonmetemailid.Text + "',PersonMetDesignation='" + txtpersonmetdesig.Text + "',PersonMetName='" + txtpersonmetname.Text + "',[IsOTPVerified]='Y'", "[Eligible/Not Eligible]='Eligible' and [Party Name]='" + hfcompname.Value + "' and (StatusByFE='' or StatusByFE is null) and Productname ='MO' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                            if (UpdateNewCompAddress != 0)
                            {
                                DataTable DtNegative = Lo.RetriveCodeWithContidion("select [Account Number],[Party Name] as [Company Name],[Billing Address],[Coordinator_Name] as [Coordinator Name],[Coordinator_Contact_Email] as [Coordinator Email],[Coordinator_Contact_Number] as [Coordinator Contact],[RM_Contact_Number] as [RM No],[RM_Email] as [RM Email],[RM_NAME] as [RM Name],[ProductName] as Product,DateName(Month,[Allocated Date]) as [Month] ,[StatusByFE],[ReasonOfStatus],[Allocated Date] as [Date] from tbl_trn_RawData where Productname ='MO' and StatusByFE='Negative' and [Party Name]='" + hfcompname.Value + "' and [Eligible/Not Eligible]='Eligible' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                                if (DtNegative.Rows.Count > 0)
                                {
                                    ExportExcel(DtNegative);
                                }
                                divmssgpopup.Visible = true;
                                cleartext();
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                                var script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                            }
                            else
                            {
                                divmssgpopup.Visible = true;
                                divmssgpopup.InnerHtml = "Status not update some error occure.";
                                divmssgpopup.Attributes.Add("class", "alert alert-danger");
                            }
                        }
                        else if (ddlcondition.SelectedValue == "09")
                        {
                            UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='(NI) RM APPROVEL', ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + "Visit Done At The Given Address by " + hfFeName.Value + " Met With Mr./Ms. " + txtpersonmetname.Text + " He./She Said First Co-Ordinate With Our Relashionship Manager He will Said This Procces Is Requered Then I Will Provide You All Documents You Have Required',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1',PersonMetMobileNo='" + num + "',PersonMetEmail='" + txtpersonmetemailid.Text + "',PersonMetDesignation='" + txtpersonmetdesig.Text + "',PersonMetName='" + txtpersonmetname.Text + "',[IsOTPVerified]='Y'", "[Eligible/Not Eligible]='Eligible' and [Party Name]='" + hfcompname.Value + "' and (StatusByFE='' or StatusByFE is null) and Productname ='MO' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                            if (UpdateNewCompAddress != 0)
                            {
                                DataTable DtNegative = Lo.RetriveCodeWithContidion("select [Account Number],[Party Name] as [Company Name],[Billing Address],[Coordinator_Name] as [Coordinator Name],[Coordinator_Contact_Email] as [Coordinator Email],[Coordinator_Contact_Number] as [Coordinator Contact],[RM_Contact_Number] as [RM No],[RM_Email] as [RM Email],[RM_NAME] as [RM Name],[ProductName] as Product,DateName(Month,[Allocated Date]) as [Month] ,[StatusByFE],[ReasonOfStatus],[Allocated Date] as [Date] from tbl_trn_RawData where Productname ='MO' and StatusByFE='Negative' and [Party Name]='" + hfcompname.Value + "' and [Eligible/Not Eligible]='Eligible' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                                if (DtNegative.Rows.Count > 0)
                                {
                                    ExportExcel(DtNegative);
                                }
                                divmssgpopup.Visible = true;
                                cleartext();
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                                var script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                            }
                            else
                            {
                                divmssgpopup.Visible = true;
                                divmssgpopup.InnerHtml = "Status not update some error occure.";
                                divmssgpopup.Attributes.Add("class", "alert alert-danger");
                            }
                        }
                    }
                    else
                    {
                        divmssgpopup.Visible = true;
                        divmssgpopup.InnerHtml = "Invalid OTP";
                        divmssgpopup.Attributes.Add("class", "alert alert-danger");
                    }
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "OTP not generated.";
                    divmssgpopup.Attributes.Add("class", "alert alert-danger");
                }
            }
        }
        catch (Exception ex)
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = ex.Message;
            divmssgpopup.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnsubremarksaddressnotfoundorcompclose_Click(object sender, EventArgs e)
    {
        if (ddlcondition.SelectedItem.Value == "03" || ddlcondition.SelectedItem.Value == "12" && txtremarksaddressnotfoundorcompclose.Text != "")
        {
            if (ddlcondition.SelectedItem.Value == "03")
            {
                Int32 UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='COMPANY CLOSED', ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Address by " + hfFeName.Value + ". He/She Said Company Are Parmanetly Closed To Here',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1'", "[Eligible/Not Eligible]='Eligible' and [Party Name]='" + hfcompname.Value + "' and (StatusByFE='' or StatusByFE is null) and Productname ='MO' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                if (UpdateNewCompAddress != 0)
                {
                    DataTable DtNegative = Lo.RetriveCodeWithContidion("select [Account Number],[Party Name] as [Company Name],[Billing Address],[Coordinator_Name] as [Coordinator Name],[Coordinator_Contact_Email] as [Coordinator Email],[Coordinator_Contact_Number] as [Coordinator Contact],[RM_Contact_Number] as [RM No],[RM_Email] as [RM Email],[RM_NAME] as [RM Name],[ProductName] as Product,DateName(Month,[Allocated Date]) as [Month] ,[StatusByFE],[ReasonOfStatus],[Allocated Date] as [Date] from tbl_trn_RawData where Productname ='MO' and StatusByFE='Negative' and [Party Name]='" + hfcompname.Value + "' and [Eligible/Not Eligible]='Eligible' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                    if (DtNegative.Rows.Count > 0)
                    {
                        ExportExcel(DtNegative);
                    }
                    divmssgpopup.Visible = true;
                    cleartext();
                    var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    var script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "Status not update some error occure.";
                    divmssgpopup.Attributes.Add("class", "alert alert-danger");
                }
            }
            else if (ddlcondition.SelectedItem.Value == "12")
            {
                Int32 UpdateNewCompAddress = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='Address Not Found', ReasonOfStatus='" + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + " MET PERSON NAME:-NO MET PERSON, RELATION:-NO, REMARKS:-Address Not Found Because,CUSTOMER NOT MET',StatusByFE='Negative',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Negative',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',SendNegativeMailtoAll='1'", "[Eligible/Not Eligible]='Eligible' and [Party Name]='" + hfcompname.Value + "' and (StatusByFE='' or StatusByFE is null) and Productname ='MO' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                if (UpdateNewCompAddress != 0)
                {
                    DataTable DtNegative = Lo.RetriveCodeWithContidion("select [Account Number],[Party Name] as [Company Name],[Billing Address],[Coordinator_Name] as [Coordinator Name],[Coordinator_Contact_Email] as [Coordinator Email],[Coordinator_Contact_Number] as [Coordinator Contact],[RM_Contact_Number] as [RM No],[RM_Email] as [RM Email],[RM_NAME] as [RM Name],[ProductName] as Product,DateName(Month,[Allocated Date]) as [Month] ,[StatusByFE],[ReasonOfStatus],[Allocated Date] as [Date] from tbl_trn_RawData where Productname ='MO' and StatusByFE='Negative' and [Party Name]='" + hfcompname.Value + "' and [Eligible/Not Eligible]='Eligible' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                    if (DtNegative.Rows.Count > 0)
                    {
                        ExportExcel(DtNegative);
                    }
                    divmssgpopup.Visible = true;
                    cleartext();
                    var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit completed");
                    var script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = "Status not update some error occure.";
                    divmssgpopup.Attributes.Add("class", "alert alert-danger");
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please enter reamrks.')", true);
        }
    }
    #endregion
    #region Revisit Caee
    protected void btngetreviitotp_Click(object sender, EventArgs e)
    {
        if (ddlcondition.SelectedValue == "10")
        {
            if (txtrevisitpersonmobileno.Text != "")
            {
                GenerateOTP();
                send(txtrevisitpersonmobileno.Text);
                revisittimedate.Visible = false;
                DivOTPRevisit.Visible = true;
            }
            else
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = "All field fill mandatory";
                divmssgpopup.Attributes.Add("Class", "alert alert-danger");
            }
        }
    }
    protected void lbresendotprevisit_Click(object sender, EventArgs e)
    {
        GenerateOTP();
        if (ddlnum.SelectedItem.Value != "OTP")
        {
            send(txtrevisitpersonmobileno.Text);
        }
        else
        {
            send(ddlnum.SelectedItem.Text);
        }
        divmssgpopup.Visible = true;
        divmssgpopup.InnerHtml = "OTP resend successfully";
        divmssgpopup.Attributes.Add("Class", "alert alert-danger");
    }
    protected void btnsubmitrevisitstatus_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtrevisittimeanddate.Text != "" && txtrevisitpersonmetname.Text != "" && txtrevisitpersonmobileno.Text != "")
            {
                if (txtotprevisitcustmer.Text != "")
                {
                    DataTable DTOTPCheck = Lo.RetriveCodeAllExcelRecordWhereCondition("[Mobile OTP]='" + txtotprevisitcustmer.Text + "' and UniqueID='" + hflocsrid.Value + "' and [Eligible/Not Eligible]='Eligible' and (StatusByFE ='Pending' or StatusByFE is null or StatusByFE='') and [Allocated Date]='" + hfAllocationDate.Value + "'");
                    if (DTOTPCheck.Rows.Count == 1)
                    {
                        if (txtotprevisitcustmer.Text == DTOTPCheck.Rows[0]["Mobile OTP"].ToString())
                        {
                            DateTime Datetimerevisit = Convert.ToDateTime(txtrevisittimeanddate.Text);
                            string DatetimeRevis = Datetimerevisit.ToString("dd/MMM/yyyy");
                            DateTime DatetimerevisitTIme = Convert.ToDateTime(txttimerevisit.Text);
                            string DatetimeRevisTime = DatetimerevisitTIme.ToString("HH:mm:ss");
                            Int32 UpdateRevisitStatusByFE = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='CUSTOMER NOT AVAILABLE',[PersonMetName]='" + txtrevisitpersonmetname.Text + "', [Revisit PerSon Name]='" + txtrevisitpersonmetname.Text + "',[Revisit Date Time]='" + DatetimeRevis + "',[Revisit Time]='" + DatetimeRevisTime + "',PersonMetMobileNo='" + txtrevisitpersonmobileno.Text + "',PersonMetEmail='" + txtrevisitpersonmetemail.Text + "',PersonMetDesignation='" + txtrevisitpersonmetdesig.Text + "',[IsOTPVerified]='Y',ReasonOfStatus='Revisit," + ddlcondition.SelectedItem.Text + "',RemarksOfStatus='" + DateTime.Now.ToString("dd-MMM") + " Visit Done At The Given Address by " + hfFeName.Value + " Met With Mr. " + txtrevisitpersonmetname.Text + " He Said Aouthrised Person Not Avavilable At This Time Please Again Visit On ',StatusByFE='Pending',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',[Positive/Negative]='Pending'", "[Eligible/Not Eligible]='Eligible' and [Party Name]='" + hfcompname.Value + "' and (StatusByFE='' or StatusByFE is null) and Productname ='MO' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                            if (UpdateRevisitStatusByFE != 0)
                            {
                                SendMailClientPending();
                                DivOTPRevisit.Visible = false;
                                revisittimedate.Visible = false;
                                divmssgpopup.Visible = true;
                                cleartext();
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit rescheduled successfully.");
                                var script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                            }
                            else
                            {
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit rescheduled status not updated some error occure.");
                                var script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                            }
                        }
                        else
                        {
                            divmssgpopup.Visible = true;
                            divmssgpopup.InnerHtml = ("Invalid OTP");
                            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
                        }
                    }
                    else
                    {
                        divmssgpopup.Visible = true;
                        divmssgpopup.InnerHtml = ("Invalid OTP");
                        divmssgpopup.Attributes.Add("Class", "alert alert-danger");
                    }
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = ("Enter six digit OTP.");
                    divmssgpopup.Attributes.Add("Class", "alert alert-danger");
                }

            }
            else
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = ("Please enter customer detail.");
                divmssgpopup.Attributes.Add("Class", "alert alert-danger");
            }
        }
        catch (Exception ex)
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = ex.Message;
            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
        }

    }
    #endregion
    #region Success Submit
    protected void btnsuccessotpstep1_Click(object sender, EventArgs e)
    {
        if (txtpersonemetmobilenosuccess.Text != "")
        {
            GenerateOTP();
            if (ddlnump.SelectedItem.Value != "OTP")
            {
                send(ddlnump.SelectedItem.Text);
            }
            else
            {
                send(txtpersonemetmobilenosuccess.Text);
            }
            divsuccessfirst.Visible = false;
            divsuccessoptorsubmit.Visible = true;
        }
        else
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = "All field fill mandatory";
            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void lblresendotppositive_Click(object sender, EventArgs e)
    {
        GenerateOTP();
        if (ddlnump.SelectedItem.Value != "OTP")
        {
            send(txtpersonemetmobilenosuccess.Text);
        }
        else
        {
            send(ddlnump.SelectedItem.Text);
        }
        divmssgpopup.Visible = true;
        divmssgpopup.InnerHtml = "OTP resend successfully";
        divmssgpopup.Attributes.Add("Class", "alert alert-danger");
    }
    protected void btnsuccsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtactivenuber.Text != "" && txtdeactivenumber.Text != "" && txtnewnumber.Text != "" && txtpersonemetmobilenosuccess.Text != "")
            {
                if (txtoptsuces.Text != "")
                {
                    DataTable DTOTPCheck = Lo.RetriveCodeAllExcelRecordWhereCondition("[Mobile OTP]='" + txtoptsuces.Text + "' and UniqueID='" + hflocsrid.Value + "' and [Eligible/Not Eligible]='Eligible' and (StatusByFE ='Pending' or StatusByFE is null or StatusByFE='') and [Allocated Date]='" + hfAllocationDate.Value + "'");
                    if (DTOTPCheck.Rows.Count == 1)
                    {
                        if (txtoptsuces.Text == DTOTPCheck.Rows[0]["Mobile OTP"].ToString())
                        {
                            string nums;
                            if (ddlnump.SelectedItem.Value != "OTP")
                            {
                                nums = ddlnump.SelectedItem.Text;
                            }
                            else
                            {
                                nums = txtpersonemetmobilenosuccess.Text;
                            }
                            Int32 UpdateCaseID = Lo.UpdateCodeOther("tbl_trn_RawData", "Set Code='OK', [Positive/Negative]='Positive',[ReasonOfStatus]='Verification Done',[RemarksOfStatus]='" + DateTime.Now.ToString("dd-MMM") + "MET PERSON:-" + txtpersonmetnamesuccess.Text + " RELATION:-" + txtsuccpersonmetdesig.Text + "  CONFIRMATION:- DONE, REMARKS:-MET PERSON CONFIRMATION DONE ',[StatusByFE]='Positive',StatusTime='" + DateTime.Now.ToString("HH:mm:ss") + "',StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "',TotalActiveNum='" + txtactivenuber.Text + "',TotalDeactiveNum='" + txtdeactivenumber.Text + "',TotalNewNum='" + txtnewnumber.Text + "',PersonMetName='" + txtpersonmetnamesuccess.Text + "',PersonMetEmail='" + txtpersonmetemailidsuccess.Text + "',PersonMetDesignation='" + txtsuccpersonmetdesig.Text + "',PersonMetMobileNo='" + txtpersonemetmobilenosuccess.Text + "'", "[Eligible/Not Eligible]='Eligible' and [Party Name]='" + hfcompname.Value + "' and (StatusByFE='' or StatusByFE is null) and Productname ='MO' and [Allocated Date]='" + hfAllocationDate.Value + "'");
                            if (UpdateCaseID != 0)
                            {
                                SendMailClientSuccessVerification(hfbilledextid1.Value, hfFeName.Value, hfcustomerorpartyname.Value);
                                cleartext();
                                divmssgpopup.Visible = true;
                                divmssgpopup.InnerHtml = "SuccessFully Update Status";
                                divmssgpopup.Attributes.Add("class", "alert alert-success");
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("SuccessFully Update Status");
                                var script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                            }
                            else
                            {
                                var message = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize("Audit rescheduled status not updated some error occure.");
                                var script = string.Format("alert({0});window.location ='Fe-JobMobility';", message);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                            }
                        }
                        else
                        {
                            divmssgpopup.Visible = true;
                            divmssgpopup.InnerHtml = ("Invalid OTP");
                            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
                        }
                    }
                    else
                    {
                        divmssgpopup.Visible = true;
                        divmssgpopup.InnerHtml = ("Invalid OTP");
                        divmssgpopup.Attributes.Add("Class", "alert alert-danger");
                    }
                }
                else
                {
                    divmssgpopup.Visible = true;
                    divmssgpopup.InnerHtml = ("Enter six digit OTP.");
                    divmssgpopup.Attributes.Add("Class", "alert alert-danger");
                }

            }
            else
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = ("Please enter customer detail.");
                divmssgpopup.Attributes.Add("Class", "alert alert-danger");
            }
        }
        catch (Exception ex)
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = ex.Message;
            divmssgpopup.Attributes.Add("Class", "alert alert-danger");
        }
    }
    #endregion
    #region SendMailCode
    protected void ExportExcel(DataTable DtNotEligible)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(DtNotEligible);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                SendMailNegativeCaseToAirtelOrOtherTeam(bytes, DtNotEligible);
            }
        }
    }
    protected void SendMailClientSuccessVerification(string Billedextid, string FeName, string UserOrCompName)
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/MoPositivePending.htm")))
        {
            body = reader.ReadToEnd();
        }
        if (UserOrCompName != "")
        {
            UserOrCompName = hfcustomerorpartyname.Value;
        }
        else
        {
            UserOrCompName = hfcompname.Value;
        }
        body = body.Replace("{PartyName}", UserOrCompName);
        body = body.Replace("{Logical}", Billedextid);
        body = body.Replace("{FEName}", FeName);
        body = body.Replace("{Message}", " This is for your information that verification as per DoT regulations for MO-Periodic has been completed successfully.");
        SendMail s;
        s = new SendMail();
        s.CreateMail("verification@rcpl.in", "verification@rcpl.in", "Airtel Periodic Verification Mobile Success / " + hfcompname.Value + "", body);
        //s.CreateMail("verification@rcpl.in", "mohdwali@globalitpoint.com", "Airtel Periodic Verification Mobile Success / " + hfcompname.Value + "", body);
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        if (result == true && exMsg == "")
        {

            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = "Status update or e-mail send successfully.";
            divmssgpopup.Attributes.Add("class", "alert alert-success");
        }
        else
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = exMsg;
            divmssgpopup.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void SendMailClientPending()
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/SuccessorFailVerification.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Message}", " This for your information that verification as per DoT regulations for MO-Periodic has been resechduled " + txtrevisittimeanddate.Text + ". <br /><br />Thanks for choosing Airtel as your preferred service partner. <br /><br />assuring you our best services always. <br /><br />");
        SendMail s;
        s = new SendMail();
        s.CreateMail("verification@rcpl.in", "verification@rcpl.in", "Airtel Periodic Verification Mobile Revisit /" + hfcompname.Value + "", body);
        //s.CreateMail("verification@rcpl.in", "mohdwali@globalitpoint.com", "Airtel Periodic Verification Mobile Revisit /" + hfcompname.Value + "", body);
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        if (result == true && exMsg == "")
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = "Status update or e-mail send successfully.";
            divmssgpopup.Attributes.Add("class", "alert alert-success");
        }
        else
        {
            divmssgpopup.Visible = true;
            divmssgpopup.InnerHtml = exMsg;
            divmssgpopup.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void SendMailNegativeCaseToAirtelOrOtherTeam(byte[] bytes, DataTable DtNotEligible)
    {
        if (DtNotEligible.Rows.Count > 0)
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/MoNegativeMail.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{ClientName}", DtNotEligible.Rows[0]["Company Name"].ToString());
            SendMail s;
            s = new SendMail();
            s.CreateInvoiceMailForByte("verification@rcpl.in", "verification@rcpl.in", "Airtel Periodic Verification Mobile Negative/" + DtNotEligible.Rows[0]["Company Name"].ToString() + "", body, "MO-Periodic", bytes, "");
            //s.CreateInvoiceMailForByte("verification@rcpl.in", "mohdwali@globalitpoint.com", "Airtel Periodic Verification Mobile Negative/" + DtNotEligible.Rows[0]["Company Name"].ToString() + "", body, "MO-Periodic", bytes, "");
            string exMsg = "";
            bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
            if (result == true && exMsg == "")
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = "Status update or e-mail send successfully.";
                divmssgpopup.Attributes.Add("class", "alert alert-success");
            }
            else
            {
                divmssgpopup.Visible = true;
                divmssgpopup.InnerHtml = exMsg;
                divmssgpopup.Attributes.Add("class", "alert alert-danger");
            }
        }
    }
    #endregion
    #region OTP Send By DDL"
    protected void sendotp_Click(object sender, EventArgs e)
    {
        if (ddlnum.SelectedItem.Value != "otp")
        {
            GenerateOTP();
            send(ddlnum.SelectedItem.Text);
        }
    }
    protected void chkotpshowddl_CheckedChanged(object sender, EventArgs e)
    {
        if (chkotpshowddl.Checked == true)
        {
            ddlnum.Enabled = true;
            sendotp.Enabled = true;
            lblresendotpnegative.Enabled = true;
        }
        else
        {
            ddlnum.Enabled = false;
            sendotp.Enabled = false;
            lblresendotpnegative.Enabled = false;
        }
    }
    protected void sendotps_Click(object sender, EventArgs e)
    {
        if (ddlnump.SelectedItem.Value != "OTP")
        {
            GenerateOTP();
            send(ddlnump.SelectedItem.Text);
        }
    }
    protected void chkboxposi_CheckedChanged(object sender, EventArgs e)
    {
        if (chkboxposi.Checked == true)
        {
            ddlnump.Enabled = true;
            sendotps.Enabled = true;
            lblresendotppositive.Enabled = true;
        }
        else
        {
            ddlnump.Enabled = false;
            sendotps.Enabled = false;
            lblresendotppositive.Enabled = false;
        }
    }
    #endregion
    #region Dropdown or Search"
    protected void ddlprodbycat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAssignJobCompany();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (txtsearch.Text != "")
        {
            DataTable DtFetchcompgropby = new DataTable();
            if (ddlprodbycat.SelectedItem.Value == "0")
            {
                DtFetchcompgropby = Lo.RetriveCodeWithContidion("select count([party name])as [Count] ,[party name],[Allocated Date],[ProdTypeName] from tbl_trn_RawData where ([Billed Ext ID]='" + txtsearch.Text + "' or [Party Name] like '%" + txtsearch.Text + "%') and  productname='MO' and [FE Name]='" + Session["Name"] + "' and (StatusbyFE is null or StatusbyFE='' ) and isactive='Y' and isclosed='N' and [Eligible/Not Eligible]='Eligible' group by [party name],[Allocated Date],[ProdTypeName]");
            }
            else if (ddlprodbycat.SelectedItem.Value == "MO-Periodic")
            {
                DtFetchcompgropby = Lo.RetriveCodeWithContidion("select count([party name])as [Count] ,[party name],[Allocated Date],[ProdTypeName] from tbl_trn_RawData where ([Billed Ext ID]='" + txtsearch.Text + "' or [Party Name] like '%" + txtsearch.Text + "%') and productname='MO' and [FE Name]='" + Session["Name"] + "' and (StatusbyFE is null or StatusbyFE='' ) and isactive='Y' and isclosed='N' and [Eligible/Not Eligible]='Eligible' and ProdTypeName='MO-Periodic' group by [party name],[Allocated Date],[ProdTypeName]");
            }
            else if (ddlprodbycat.SelectedItem.Value == "MO-Quarterly")
            {
                DtFetchcompgropby = Lo.RetriveCodeWithContidion("select count([party name])as [Count] ,[party name],[Allocated Date],[ProdTypeName] from tbl_trn_RawData where ([Billed Ext ID]='" + txtsearch.Text + "' or [Party Name] like '%" + txtsearch.Text + "%') and productname='MO' and [FE Name]='" + Session["Name"] + "' and (StatusbyFE is null or StatusbyFE='' ) and isactive='Y' and isclosed='N' and [Eligible/Not Eligible]='Eligible' and ProdTypeName='MO-Quarterly' group by [party name],[Allocated Date],[ProdTypeName]");
            }
            if (DtFetchcompgropby.Rows.Count > 0)
            {
                dlcomp.DataSource = DtFetchcompgropby;
                dlcomp.DataBind();
                dlcomp.Visible = true;
                totalcount.InnerText = "Total company for audit :- " + DtFetchcompgropby.Rows.Count.ToString();
                diverror.Visible = false;
                diverror.InnerHtml = "";
                txtsearch.Text = "";
            }
            else
            {
                dlcomp.Visible = false;
                diverror.Visible = true;
                diverror.InnerHtml = "No job assign";
                diverror.Attributes.Add("class", "alert alert-danger");
                totalcount.InnerText = "Toal company for audit :- 0";
                txtsearch.Text = "";
            }
        }
    }
    #endregion
    #region Page Index Code"
    protected void lnkbtnPgFirst_Click(object sender, EventArgs e)
    {
        pagingCurrentPage = 0;
        BindAssignJobCompany();
    }
    protected void lnkbtnPgPrevious_Click(object sender, EventArgs e)
    {
        pagingCurrentPage -= 1;
        BindAssignJobCompany();
    }
    protected void lnkbtnPgNext_Click(object sender, EventArgs e)
    {
        pagingCurrentPage += 1;
        BindAssignJobCompany();
    }
    protected void lnkbtnPgLast_Click(object sender, EventArgs e)
    {
        pagingCurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
        BindAssignJobCompany();
    }
    protected void DataListPaging_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.Equals("Newpage"))
        {
            pagingCurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BindAssignJobCompany();
        }
    }
    protected void DataListPaging_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        LinkButton lnkPage = (LinkButton)e.Item.FindControl("Pagingbtn");
        if (lnkPage.CommandArgument.ToString() == pagingCurrentPage.ToString())
        {
            lnkPage.Enabled = false;
        }
    }
    private int pagingCurrentPage
    {
        get
        {
            if (ViewState["pagingCurrentPage"] == null)
            {
                return 0;
            }
            else
            {
                return ((int)ViewState["pagingCurrentPage"]);
            }
        }
        set
        {
            ViewState["pagingCurrentPage"] = value;
        }
    }
    private void DataListPagingMethod()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PageIndex");
        dt.Columns.Add("PageText");
        firstindex = pagingCurrentPage - 25;
        if (pagingCurrentPage > 25)
        {
            lastindex = pagingCurrentPage + 25;
        }
        else
        {
            lastindex = 24;
        }
        if (lastindex > Convert.ToInt32(ViewState["totpage"]))
        {
            lastindex = Convert.ToInt32(ViewState["totpage"]);
            firstindex = lastindex - 24;
        }
        if (firstindex < 0)
        {
            firstindex = 0;
        }
        for (int i = firstindex; i < lastindex; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = i;
            dr[1] = i + 1;
            dt.Rows.Add(dr);
        }
        DataListPaging.DataSource = dt;
        DataListPaging.DataBind();
    }
    #endregion
}