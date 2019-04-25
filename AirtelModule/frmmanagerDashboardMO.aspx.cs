using System;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using System.Configuration;
using ClosedXML.Excel;

public partial class frmmanagerDashboardMO : System.Web.UI.Page
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
               BinndFEAllwordassingincurrentmonthmo();
            }
        }
    }
    protected void countlink()
    {
        #region Mobility
        DTCount = Lo.RetriveAllCountFE("tbl_mst_Employee", "IsActive='Y' and Category ='field Executive'");
        if (DTCount.Rows.Count > 0)
        {
            div2mo.InnerText = DTCount.Rows[0]["RCount"].ToString();
        }
        DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and StatusByFE='Positive' and ProductName='MO' group by [Party name]");
        if (DTCount.Rows.Count > 0)
        {
            div3mo.InnerText = DTCount.Rows.Count.ToString();
        }
        DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and StatusByFE='Pending' and ProductName='MO' group by [Party name]");
        if (DTCount.Rows.Count > 0)
        {
            div4mo.InnerText = DTCount.Rows.Count.ToString();
        }
        DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and StatusByFE='Negative' and ProductName='MO' group by [Party name]");
        if (DTCount.Rows.Count > 0)
        {
            Div5mo.InnerText = DTCount.Rows.Count.ToString();
        }
        DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and (StatusByFE='' or StatusByFE is null) and ProductName='MO' group by [Party name]");
        if (DTCount.Rows.Count > 0)
        {
            div6mo.InnerText = DTCount.Rows.Count.ToString();
        }
        DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and ProductName='MO' group by [Party name]");
        if (DTCount.Rows.Count > 0)
        {
            div7mo.InnerText = DTCount.Rows.Count.ToString();
        }
        DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and [TL Name]='" + Session["Name"] + "' and IsSubmitted='Y' and ProductName='MO'");
        if (DTCount.Rows.Count > 0)
        {
            div8mo.InnerText = DTCount.Rows[0]["RCount"].ToString();
        }
        DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and [TL Name]='" + Session["Name"] + "' and IsScanned='Y' and ProductName='MO'");
        if (DTCount.Rows.Count > 0)
        {
            div9mo.InnerText = DTCount.Rows[0]["RCount"].ToString();
        }
        //current day work count
        DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and IsClosed!='Y' and ProductName='MO' and StatusByFE='Positive' and StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "' group by [Party Name]");
        if (DTCount.Rows.Count > 0)
        {
            lblsuccessmo.Text = "Total Positive Case Today:- " + DTCount.Rows.Count.ToString();
        }
        else
        {
            lblsuccessmo.Text = "Total Positive Case Today:- " + 0;
        }
        DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and IsClosed!='Y' and ProductName='MO' and StatusByFE='Negative' and StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "' group by [Party Name]");
        if (DTCount.Rows.Count > 0)
        {
            lblnegmo.Text = "Total Negative Case Today:- " + DTCount.Rows.Count.ToString();
        }
        else
        {
            lblnegmo.Text = "Total Negative Case Today:- " + 0;
        }
        DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and IsClosed!='Y' and ProductName='MO' and StatusByFE='Pending' and StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "' group by [Party Name]");
        if (DTCount.Rows.Count > 0)
        {
            lblpesmo.Text = "Total Pending Case Today:- " + DTCount.Rows.Count.ToString();
        }
        else
        {
            lblpesmo.Text = "Total Pending Case Today:- " + 0;
        }
        #endregion
    }
    protected void bindFELastVisit()
    {
        DataTable DtgetotherdatalistcodeAll = Lo.RetriveCodeWithContidion("select distinct [FE Name],ProductName from tbl_trn_RawData where StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "' and IsActive='Y'  ");
        if (DtgetotherdatalistcodeAll.Rows.Count > 0)
        {
            DataView DVFE = new DataView(DtgetotherdatalistcodeAll);
            DVFE.RowFilter = "ProductName='MO'";
            dlmo.DataSource = DVFE;
            dlmo.DataBind();
            divfedetailmo.Visible = true;
        }
    }
    protected void dlmo_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView gvfedetaillistmo = (GridView)e.Item.FindControl("gvfedetaillistmo");
            Label hfid = (Label)e.Item.FindControl("lblnamemo");
            Label lblmssg = (Label)e.Item.FindControl("lblmssgmo");
            Label lbltime = (Label)e.Item.FindControl("lbltimemo");
            Label lbldate = (Label)e.Item.FindControl("lbldatemo");
            DataTable DTGetuserbyID = Lo.RetriveCodeWithContidion("select top 1 [Allocated Date],[Billed Ext Id],UniqueID,[From Site],StatusTime,[FE Name],StatusByFE,StatusUpdateDate,[Logical Circuit Id],[Party Name],ProductName,ProdTypeName,[Commissioning Date],[Account Manager],FEComplianceDate from tbl_trn_RawData where StatusUpdateDate='" + DateTime.Now.ToString("dd/MMM/yyyy") + "' and IsActive='Y' and [FE NAME]='" + hfid.Text + "' and ProductName='MO' order by  StatusTime desc ");
            if (DTGetuserbyID.Rows.Count > 0)
            {
                DateTime date = Convert.ToDateTime(DTGetuserbyID.Rows[0]["StatusUpdateDate"].ToString());
                DateTime dtTyme = new DateTime();
                DateTime.TryParse(DTGetuserbyID.Rows[0]["StatusTime"].ToString(), out dtTyme);
                DateTime time = dtTyme;
                lbltime.Text = time.ToString("hh:mm:ss");
                lbldate.Text = date.ToString("dd/MMM/yyyy");
                gvfedetaillistmo.DataSource = DTGetuserbyID;
                gvfedetaillistmo.DataBind();
                gvfedetaillistmo.Visible = true;
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
    protected void BinndFEAllwordassingincurrentmonthmo()
    {
        if (ddlmonth2.SelectedItem.Value == "01")
        {
            DataTable DtFEWorkassign = Lo.RetriveCodeWithContidion("select [fe name],count([fe name]) as Total,DATENAME(month, GETDATE()) AS 'Month Name' from tbl_trn_RawData where DATENAME(month, [allocated date])=DATENAME(month, GETDATE()) and IsActive='Y' and ProductName='MO' group by [fe name] ");
            if (DtFEWorkassign.Rows.Count > 0)
            {
                lblmonth2.Text = DtFEWorkassign.Rows[0]["Month Name"].ToString();
                string[] x = new string[DtFEWorkassign.Rows.Count];
                int[] y = new int[DtFEWorkassign.Rows.Count];
                for (int i = 0; i < DtFEWorkassign.Rows.Count; i++)
                {
                    x[i] = DtFEWorkassign.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(DtFEWorkassign.Rows[i][1]);
                }
                Chart2.Series[0].Points.DataBindXY(x, y);
                Chart2.Series[0].ChartType = SeriesChartType.Pie;
                Chart2.ChartAreas["ChartArea2"].Area3DStyle.Enable3D = false;
                Chart2.Legends[0].Enabled = true;
            }
            else
            {
                lblmonth2.Text = DateTime.Now.ToString("MMMM");
            }
        }
        else
        {
            DataTable DtFEWorkassign = Lo.RetriveCodeWithContidion("select [fe name],count([fe name]) as Total,DATENAME(month, GETDATE()) AS 'Month Name' from tbl_trn_RawData where DATENAME(month, [allocated date])='" + ddlmonth2.SelectedItem.Text + "' and IsActive='Y' and ProductName='MO'  group by [fe name] ");
            if (DtFEWorkassign.Rows.Count > 0)
            {
                lblmonth2.Text = ddlmonth2.SelectedItem.Text;
                string[] x = new string[DtFEWorkassign.Rows.Count];
                int[] y = new int[DtFEWorkassign.Rows.Count];
                for (int i = 0; i < DtFEWorkassign.Rows.Count; i++)
                {
                    x[i] = DtFEWorkassign.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(DtFEWorkassign.Rows[i][1]);
                }
                Chart2.Series[0].Points.DataBindXY(x, y);
                Chart2.Series[0].ChartType = SeriesChartType.Pie;
                Chart2.ChartAreas["ChartArea2"].Area3DStyle.Enable3D = false;
                Chart2.Legends[0].Enabled = true;
            }
            else
            {
                lblmonth2.Text = ddlmonth2.SelectedItem.Text;
            }
        }
    }
    protected void ddlmonth2_SelectedIndexChanged(object sender, EventArgs e)
    {
        BinndFEAllwordassingincurrentmonthmo();
    }
}