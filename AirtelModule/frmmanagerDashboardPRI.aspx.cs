using System;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using System.Configuration;
using ClosedXML.Excel;

public partial class frmmanagerDashboardPRI : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataTable DTCount = new DataTable();
    DateTime TodayDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy"));
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            if (Session["Name"] != null)
            {
                countlink();
                bindFELastVisit();
                BinndFEAllwordassingincurrentmonthPRI();
                #region Send Mail AutoMode Weekly-NegativeReport or PositiveReport
                //  SendMailNegative1week();
                #endregion
            }
        }
    }
    protected void countlink()
    {
        #region PRI
        DTCount = Lo.RetriveAllCountFE("tbl_mst_Employee", "IsActive='Y' and Category ='field Executive'");
        if (DTCount.Rows.Count > 0)
        {
            divprife.InnerText = DTCount.Rows[0]["RCount"].ToString();
        }
        DTCount = Lo.RetriveAllCount("tbl_trn_RawData");
        if (DTCount.Rows.Count > 0)
        {
            DataView DV = new DataView(DTCount);
            DV.RowFilter = "IsActive='Y' and StatusByFE='Positive' and ProductName='PRI-Fixed Line' and KeyDup='false'";
            divpripostive.InnerText = DV.Count.ToString();
            DV.RowFilter = "IsActive='Y' and StatusByFE='Pending' and ProductName='PRI-Fixed Line' and KeyDup='false'";
            divpripending.InnerText = DV.Count.ToString();
            DV.RowFilter = "IsActive='Y' and StatusByFE='Negative' and ProductName='PRI-Fixed Line' and KeyDup='false'";
            divprinegative.InnerText = DV.Count.ToString();
            DV.RowFilter = "IsActive='Y' and (StatusByFE='' or StatusByFE is null) and ProductName='PRI-Fixed Line' and KeyDup='false'";
            divprinotwork.InnerText = DV.Count.ToString();
            DV.RowFilter = "IsActive='Y' and ProductName='PRI-Fixed Line' and KeyDup='false'";
            divpritotalcasestart.InnerText = DV.Count.ToString();
            DV.RowFilter = "IsActive='Y' and [TL Name]='" + Session["Name"] + "' and IsSubmitted='Y' and ProductName='PRI-Fixed Line' and KeyDup='false'";
            divpritotalsubmit.InnerText = DV.Count.ToString();
            DV.RowFilter = "IsActive='Y' and [TL Name]='" + Session["Name"] + "' and IsScanned='Y' and ProductName='PRI-Fixed Line' and KeyDup='false'";
            divpritotalscaneed.InnerText = DV.Count.ToString();
        }
        DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and IsClosed!='Y' and StatusByFE='Positive' and StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "' and ProductName='PRI-Fixed Line' and KeyDup='false'");
        if (DTCount.Rows.Count > 0)
        {
            lblpripositive.Text = "Total Positive Case Today:- " + DTCount.Rows[0]["RCount"].ToString();
        }
        else
        {
            lblpripositive.Text = "Total Positive Case Today:- " + 0;
        }
        DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and IsClosed!='Y' and StatusByFE='Negative' and StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "' and ProductName='PRI-Fixed Line' and KeyDup='false'");
        if (DTCount.Rows.Count > 0)
        {
            lblprinegative.Text = "Total Negative Case Today:- " + DTCount.Rows[0]["RCount"].ToString();
        }
        else
        {
            lblprinegative.Text = "Total Negative Case Today:- " + 0;
        }
        DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and IsClosed!='Y' and StatusByFE='Pending' and StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "' and ProductName='PRI-Fixed Line' and KeyDup='false'");
        if (DTCount.Rows.Count > 0)
        {
            lblpripending.Text = "Total Pending Case Today:- " + DTCount.Rows[0]["RCount"].ToString();
        }
        else
        {
            lblpripending.Text = "Total Pending Case Today:- " + 0;
        }
        #endregion
    }
    protected void bindFELastVisit()
    {
        DataTable DtgetotherdatalistcodeAll = Lo.RetriveCodeWithContidion("select distinct [FE Name],ProductName from tbl_trn_RawData where StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "' and IsActive='Y'  ");
        if (DtgetotherdatalistcodeAll.Rows.Count > 0)
        {
            DataView DVFE = new DataView(DtgetotherdatalistcodeAll);
            DVFE.RowFilter = "ProductName='PRI-Fixed Line'";
            dlfepri.DataSource = DVFE;
            dlfepri.DataBind();
            div3.Visible = true;
        }
    }
    protected void dlfepri_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView gvfedetaillist = (GridView)e.Item.FindControl("gvfedetaillistpri");
            Label hfid = (Label)e.Item.FindControl("lblname1pri");
            Label lblmssg = (Label)e.Item.FindControl("lblmssgpri");
            Label lbltime = (Label)e.Item.FindControl("lbltimepri");
            Label lbldate = (Label)e.Item.FindControl("lbldatepri");
            DataTable DTGetuserbyID = Lo.RetriveCodeWithContidion("select top 1 [Allocated Date],UniqueID,[From Site],StatusTime,[FE Name],StatusByFE,StatusUpdateDate,[Logical Circuit Id],[Party Name],ProductName,ProdTypeName,BILLING_BANDWIDTH,[Commissioning Date],[Account Manager],FEComplianceDate from tbl_trn_RawData where StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "' and IsActive='Y' and [FE NAME]='" + hfid.Text + "'  and ProductName='PRI-Fixed Line' and KeyDup='false' order by  StatusTime desc ");
            if (DTGetuserbyID.Rows.Count > 0)
            {
                DateTime date = Convert.ToDateTime(DTGetuserbyID.Rows[0]["StatusUpdateDate"].ToString());
                DateTime dtTyme = new DateTime();
                DateTime.TryParse(DTGetuserbyID.Rows[0]["StatusTime"].ToString(), out dtTyme);
                DateTime time = dtTyme;
                lbltime.Text = time.ToString("hh:mm:ss");
                lbldate.Text = date.ToString("dd/MMM/yyyy");
                gvfedetaillist.DataSource = DTGetuserbyID;
                gvfedetaillist.DataBind();
                gvfedetaillist.Visible = true;
                lbldate.Visible = true;
                lbltime.Visible = true;
            }
            else
            {
                lblmssg.Visible = true;
                lblmssg.Text = "No case handle record find today.";
            }
        }
    }
    protected void BinndFEAllwordassingincurrentmonthPRI()
    {
        if (ddlmonthpri.SelectedItem.Value == "01")
        {
            DataTable DtFEWorkassign = Lo.RetriveCodeWithContidion("select [fe name],count([fe name]) as Total,DATENAME(month, GETDATE()) AS 'Month Name' from tbl_trn_RawData where DATENAME(month, [allocated date])=DATENAME(month, GETDATE()) and IsActive='Y' and ProductName='PRI-Fixed Line' and KeyDup='false' group by [fe name] ");
            if (DtFEWorkassign.Rows.Count > 0)
            {
                lblpritotalthismonth.Text = DtFEWorkassign.Rows[0]["Month Name"].ToString();
                string[] x = new string[DtFEWorkassign.Rows.Count];
                int[] y = new int[DtFEWorkassign.Rows.Count];
                for (int i = 0; i < DtFEWorkassign.Rows.Count; i++)
                {
                    x[i] = DtFEWorkassign.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(DtFEWorkassign.Rows[i][1]);
                }
                Chart3.Series[0].Points.DataBindXY(x, y);
                Chart3.Series[0].ChartType = SeriesChartType.Pie;
                Chart3.ChartAreas["ChartArea2"].Area3DStyle.Enable3D = false;
                Chart3.Legends[0].Enabled = true;
            }
            else
            {
                lblpritotalthismonth.Text = DateTime.Now.ToString("MMMM");
            }
        }
        else
        {
            DataTable DtFEWorkassign = Lo.RetriveCodeWithContidion("select [fe name],count([fe name]) as Total,DATENAME(month, GETDATE()) AS 'Month Name' from tbl_trn_RawData where DATENAME(month, [allocated date])='" + ddlmonthpri.SelectedItem.Text + "' and IsActive='Y' and ProductName='PRI-Fixed Line' and KeyDup='false'  group by [fe name]");
            if (DtFEWorkassign.Rows.Count > 0)
            {
                lblpritotalthismonth.Text = ddlmonthpri.SelectedItem.Text;
                string[] x = new string[DtFEWorkassign.Rows.Count];
                int[] y = new int[DtFEWorkassign.Rows.Count];
                for (int i = 0; i < DtFEWorkassign.Rows.Count; i++)
                {
                    x[i] = DtFEWorkassign.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(DtFEWorkassign.Rows[i][1]);
                }
                Chart3.Series[0].Points.DataBindXY(x, y);
                Chart3.Series[0].ChartType = SeriesChartType.Pie;
                Chart3.ChartAreas["ChartArea2"].Area3DStyle.Enable3D = false;
                Chart3.Legends[0].Enabled = true;
            }
            else
            {
                lblpritotalthismonth.Text = ddlmonthpri.SelectedItem.Text;
            }
        }
    }
    protected void ddlmonthpri_SelectedIndexChanged(object sender, EventArgs e)
    {
        BinndFEAllwordassingincurrentmonthPRI();
    }
    //----2 mail big section -----//
    #region MailCode7
    protected void SendMailNegative1week()
    {
        try
        {
            DataTable DTGetStatusSendMail = Lo.RetriveCodeWithContidion("select * from tbl_trn_AutoMailSendStatus where Category='FEWeeklyNegWorkReport' and MailDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "'");
            if (DTGetStatusSendMail.Rows.Count == 0)
            {
                DateTime mTime = Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy hh:mm tt"));
                DateTime MailSendTime = Convert.ToDateTime(DateTime.Parse("6:30").ToString("dd/MMM/yyyy hh:mm tt"));
                if (MailSendTime < mTime)
                {
                    DataTable DtLL = Lo.RetriveCodeWithContidion("select [Logical Circuit ID], [Party Name], StatusByFE,Code, RemarksOfStatus as Remarks ,StatusUpdateDate,PersonMetName,PersonMetMobileNo,PersonMetEmail,StatusTime,CustSamKamMailDatetime,NegativeCustSamKamMailDatetime from tbl_trn_RawData where ProductName='Leased Line' and StatusUpdateDate between '" + DateTime.Now.AddDays(-7).ToString("dd/MMM/yyyy") + "' and '" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "' and StatusByFE='Negative' ");
                    DataTable DtPRI = Lo.RetriveCodeWithContidion("select [Subs Del no], [Party Name], StatusByFE,Code, RemarksOfStatus as Remarks ,StatusUpdateDate,PersonMetName,PersonMetMobileNo,PersonMetEmail,StatusTime,CustSamKamMailDatetime,NegativeCustSamKamMailDatetime from tbl_trn_RawData where ProductName='PRI-Fixed Line' and StatusUpdateDate between '" + DateTime.Now.AddDays(-7).ToString("dd/MMM/yyyy") + "' and '" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "' and StatusByFE='Negative' ");
                    string compname;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Billed Ext ID", typeof(string));
                    dt.Columns.Add("Party Name", typeof(string));
                    dt.Columns.Add("FE Name", typeof(string));
                    dt.Columns.Add("StatusByFe", typeof(string));
                    dt.Columns.Add("Code", typeof(string));
                    dt.Columns.Add("Remarks", typeof(string));
                    dt.Columns.Add("StatusUpdateDate", typeof(DateTime));
                    dt.Columns.Add("PersonMetName", typeof(string));
                    dt.Columns.Add("PersonMetMobileNo", typeof(string));
                    dt.Columns.Add("PersonMetEmail", typeof(string));
                    dt.Columns.Add("StatusTime", typeof(string));
                    dt.Columns.Add("CustSamKamMailDatetime", typeof(string));
                    dt.Columns.Add("NegativeCustSamKamMailDatetime", typeof(string));
                    DataTable DtFetchcompgropby = Lo.RetriveCodeWithContidion("select count([party name])as [Count] ,[party name] from tbl_trn_RawData where productname='MO' and IsActive='Y'   and StatusUpdateDate between '" + DateTime.Now.AddDays(-7).ToString("dd/MMM/yyyy") + "' and '" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "' and StatusByFE='Negative' group by [party name]");
                    if (DtFetchcompgropby.Rows.Count > 0)
                    {
                        for (int i = 0; DtFetchcompgropby.Rows.Count > i; i++)
                        {
                            compname = DtFetchcompgropby.Rows[i]["Party Name"].ToString();
                            DataTable DtAssignJob = Lo.RetriveCodeWithContidion("SELECT  top 1 {Billed Ext ID],[Party Name],[FE Name], StatusByFE,Code, RemarksOfStatus as Remarks ,StatusUpdateDate,[PersonMetName],[PersonMetMobileNo],[PersonMetEmail],StatusTime,CustSamKamMailDatetime,NegativeCustSamKamMailDatetime from tbl_trn_RawData where IsActive='Y' and StatusByFE='Negative' and StatusUpdateDate between '" + DateTime.Now.AddDays(-7).ToString("dd/MMM/yyyy") + "' and '" + DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy") + "' and ProductName='MO' and [Party Name]= '" + compname + "' order by [FE Name] asc");
                            foreach (DataRow dr in DtAssignJob.Rows)
                            {
                                object[] row = dr.ItemArray;
                                dt.Rows.Add(row);
                            }
                        }
                    }
                    if (DtLL.Rows.Count > 0 || dt.Rows.Count > 0 || DtPRI.Rows.Count > 0)
                    {
                        ExportExcel(DtLL, dt, DtPRI, "Automated FE Weekly Negative Report");
                        int update = Lo.InsertNormal("insert into tbl_trn_AutoMailSendStatus (Category,MailDate,IsSend) values ('FEWeeklyNegWorkReport','" + DateTime.Now.ToString("dd/MMM/yyyy") + "','Y')");
                    }
                }
            }
        }
        catch (Exception Ex)
        {
        }
    }
    #endregion
    #region BindExcel or MailCode
    protected void SendMailExcelReport(byte[] bytes, byte[] wali, byte[] PRI, DataTable dtll, DataTable dtmo, DataTable dtPRI, string FileName)
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/EmailForReport.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Message}", FileName);
        SendMail s;
        s = new SendMail();
        s.CreateInvoiceMailForByte1("verification@rcpl.in", "Shyam.Prasad@airtel.com", FileName, body, "", bytes, wali, PRI, "verification@rcpl.in", "gagan@rcpl.in");
        // s.CreateInvoiceMailForByte1("verification@rcpl.in", "mohdwali@globalitpoint.com", FileName, body, "", bytes, wali, PRI, "", "");
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
    }
    protected void ExportExcel(DataTable dtll, DataTable dtmo, DataTable dtPRI, string FileName)
    {
        byte[] wali;
        byte[] bytes;
        byte[] PRI;
        using (XLWorkbook wb = new XLWorkbook())
        {
            dtll.TableName = "Report Leased Line";
            wb.Worksheets.Add(dtll);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                bytes = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        using (XLWorkbook wbn = new XLWorkbook())
        {
            dtmo.TableName = "Report MO";
            wbn.Worksheets.Add(dtmo);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbn.SaveAs(memoryStream);
                wali = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        using (XLWorkbook wbpri = new XLWorkbook())
        {
            dtPRI.TableName = "Report PRI";
            wbpri.Worksheets.Add(dtPRI);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbpri.SaveAs(memoryStream);
                PRI = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        if (dtll != null || dtmo != null || dtPRI != null)
        {
            SendMailExcelReport(bytes, wali, PRI, dtll, dtmo, dtPRI, FileName);
        }
    }
    #endregion
    //end--------//
}