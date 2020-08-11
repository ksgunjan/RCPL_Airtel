using BuisnessLayer;
using System;
using System.Data;

public partial class frmFEDashBoard : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private DataTable DTCount = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                #region check file
                if (Session["Name"] != null)
                {
                    DataTable DtCount = Lo.RetriveFunctionCode4(Session["Name"].ToString(), DateTime.Now.ToString("dd/MMM/yyyy"), "", "", "");
                    lbltotclient.Text = DtCount.Rows[0]["TotalClientLL"].ToString();
                    lbltotalvisitdone.Text = DtCount.Rows[0]["TotVisitDoneLL"].ToString();
                    lbltotalpendingvisit.Text = DtCount.Rows[0]["TotPenVisitLL"].ToString();
                    lbltotalrevisit.Text = DtCount.Rows[0]["TotRevisitLL"].ToString();
                    lbltotalpresche.Text = DtCount.Rows[0]["TotPreSchLL"].ToString();
                    lbltotalclientmo.Text = DtCount.Rows[0]["TotalClientMO"].ToString();
                    lbltotalvisitidonemo.Text = DtCount.Rows[0]["TotVisitDoneMO"].ToString();
                    lblpenvisitmo.Text = DtCount.Rows[0]["TotPenVisitMO"].ToString();
                    lblrevisitmo.Text = DtCount.Rows[0]["TotRevisitMO"].ToString();
                    lblpreschmo.Text = DtCount.Rows[0]["TotPreSchMO"].ToString();
                    lbltotalclientpri.Text = DtCount.Rows[0]["TotalClientPRI"].ToString();
                    lbltotalvisitdonepri.Text = DtCount.Rows[0]["TotVisitDonePRI"].ToString();
                    lblpendingvisitpri.Text = DtCount.Rows[0]["TotPenVisitPRI"].ToString();
                    lblpendrevisitpri.Text = DtCount.Rows[0]["TotRevisitPRI"].ToString();
                    lblpreschepri.Text = DtCount.Rows[0]["TotPreSchPRI"].ToString();
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}