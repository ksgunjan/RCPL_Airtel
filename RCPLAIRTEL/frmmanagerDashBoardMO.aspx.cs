using BuisnessLayer;
using System;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

public partial class frmmanagerDashBoardMO : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private DataTable DTCount = new DataTable();
    private Common Co = new Common();
    private DateTime TodayDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy"));
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Name"] != null)
            {
                countlink();
                bindFELastVisit();
                BindMonth();
                FEAllotedJobCountAllocatedMOnth();
                FEAllotedJobCountStatusUpdateMonthDate();
            }
        }
    }
    protected void countlink()
    {
        #region Mobility
        DataTable DtGetCount = Lo.RetriveFunctionCode8(Session["Name"].ToString(), "", "", "", "");
        if (DtGetCount.Rows.Count > 0)
        {
            lbltotfe.Text = DtGetCount.Rows[0]["TotFE"].ToString();
            lbltotpositive.Text = DtGetCount.Rows[0]["TotPositive"].ToString();
            lblrevisit.Text = DtGetCount.Rows[0]["TotPending"].ToString();
            lblnegative.Text = DtGetCount.Rows[0]["TotNegative"].ToString();
            lblpending.Text = DtGetCount.Rows[0]["TotRevisit"].ToString();
            lbltotalcase.Text = DtGetCount.Rows[0]["Total"].ToString();
            lbltotsubdoc.Text = DtGetCount.Rows[0]["TotSubDocument"].ToString();
            lbltotscann.Text = DtGetCount.Rows[0]["TotScannedDocument"].ToString();
            lbltotaltodaysuccesscase.Text = "Total Positive Case Today:- " + DtGetCount.Rows[0]["TotCasePositiv"].ToString();
            lbltodaynegativecase.Text = "Total Negative Case Today:- " + DtGetCount.Rows[0]["TotCaseNegative"].ToString();
            lbltodaypendingcase.Text = "Total Pending Case Today:- " + DtGetCount.Rows[0]["TotCasePending"].ToString();
        }
        #endregion
    }
    protected void bindFELastVisit()
    {
        DataTable DtgetotherdatalistcodeAll = Lo.RetriveCodeManagerDashboard(0, "", "", "", "", "", "", "", Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy")), "FeLastVisitMO");
        if (DtgetotherdatalistcodeAll.Rows.Count > 0)
        {
            dlmo.DataSource = DtgetotherdatalistcodeAll;
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
            DataTable DTGetuserbyID = Lo.RetriveCodeManagerDashboard(0, "", hfid.Text, "", "", "", "", "", Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy")), "FeVisitGetOneRowMO");
            if (DTGetuserbyID.Rows.Count > 0)
            {
                DateTime date = Convert.ToDateTime(DTGetuserbyID.Rows[0]["Status_Update_Date"].ToString());
                DateTime dtTyme = new DateTime();
                DateTime.TryParse(DTGetuserbyID.Rows[0]["Status_Time"].ToString(), out dtTyme);
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
    protected void BindMonth()
    {
        DataTable Dtmonth = Lo.RetriveCodeLocal(0, "", "", "", "Month");
        if (Dtmonth.Rows.Count > 0)
        {
            Co.FillComboBox(ddlmonth, Dtmonth, "MonthName", "MonthName");
            string m = DateTime.Now.ToString("MMMM");
            ddlmonth.SelectedValue = m;
        }
    }
    protected void FEAllotedJobCountAllocatedMOnth()
    {
        DataTable DtAM = Lo.RetriveCodeLocal(0, ddlmonth.SelectedItem.Text, "MO", "", "CountADFE");
        if (DtAM.Rows.Count > 0)
        {
            string[] x = new string[DtAM.Rows.Count];
            int[] y = new int[DtAM.Rows.Count];
            for (int i = 0; i < DtAM.Rows.Count; i++)
            {
                x[i] = DtAM.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(DtAM.Rows[i][1]);
            }
            crtAllocateddatewise.Series[0].Points.DataBindXY(x, y);
            crtAllocateddatewise.Series[0].ChartType = SeriesChartType.StackedColumn;
            crtAllocateddatewise.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            crtAllocateddatewise.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;
            crtAllocateddatewise.Legends[0].Enabled = true;
        }
    }
    protected void FEAllotedJobCountStatusUpdateMonthDate()
    {
        DataTable DtAM = Lo.RetriveCodeLocal(0, ddlmonth.SelectedItem.Text, "MO", "", "CountSUDFE");
        if (DtAM.Rows.Count > 0)
        {
            string[] x = new string[DtAM.Rows.Count];
            int[] y = new int[DtAM.Rows.Count];
            for (int i = 0; i < DtAM.Rows.Count; i++)
            {
                x[i] = DtAM.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(DtAM.Rows[i][1]);
            }
            crtstatusupdatedatewise.Series[0].Points.DataBindXY(x, y);
            crtstatusupdatedatewise.Series[0].ChartType = SeriesChartType.StackedColumn;
            crtstatusupdatedatewise.ChartAreas["ChartArea2"].Area3DStyle.Enable3D = true;
            crtstatusupdatedatewise.ChartAreas["ChartArea2"].AxisX.LabelStyle.Interval = 1;
            crtstatusupdatedatewise.Legends[0].Enabled = true;
        }
    }

    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        FEAllotedJobCountAllocatedMOnth();
        FEAllotedJobCountStatusUpdateMonthDate();
    }
}