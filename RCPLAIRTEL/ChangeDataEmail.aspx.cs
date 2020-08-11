using BuisnessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangeDataEmail : System.Web.UI.Page
{
    #region Variable
    private DataTable dtgridrecord = new DataTable();
    private Logic Lo = new Logic();
    private Common Co = new Common();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    private string exMsg = string.Empty;
    private long ExcelRawID = 0;
    private string mprod = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { BindCompProd(); }
    }
    protected void BindCompProd()
    {
        DataTable DtBind = Lo.RetriveCodeLocal(0, "", "", "", "RetriveProduct");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBoxList1(rbCatType, DtBind, "ProductName", "ProductId");
            rbCatType.SelectedIndex = 0;
        }
    }
    protected void BindGrid()
    {
        DataTable DtResult = Lo.RetriveCodeLocal(0, txtsearch.Text, rbCatType.SelectedItem.Text, "", "SearchData");
        if (DtResult.Rows.Count > 0)
        {
            gvdetail.DataSource = DtResult;
            gvdetail.DataBind();
            gvdetail.Visible = true;
        }
        else
        {
            gvdetail.Visible = false;
        }
    }
    protected void gvdetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Upd")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            ExcelRawID = Convert.ToInt64(e.CommandArgument.ToString());
            Label PartyName = gvdetail.Rows[rowIndex].FindControl("lblpartyname") as Label;
            Label AllocatedDate = gvdetail.Rows[rowIndex].FindControl("lblalocdatr") as Label;
            Label ProdType = gvdetail.Rows[rowIndex].FindControl("lblprodtype") as Label;
            DataTable DtGet = Lo.RetriveCodeLocal(Convert.ToInt64(e.CommandArgument.ToString()), "", ProdType.Text, "", "SearchDataSingle");
            txtb2bemail.Text = DtGet.Rows[0]["B2B_Email"].ToString();
            txtb2bheademail.Text = DtGet.Rows[0]["B2B_Head_Email"].ToString();
            txtbillingemail.Text = DtGet.Rows[0]["Billing_Email_Id"].ToString();
            txtcoordinatoremail.Text = DtGet.Rows[0]["Coordinator_Contact_Email"].ToString();
            txtcustemail.Text = DtGet.Rows[0]["Cust_Email"].ToString();
            txtkamemail.Text = DtGet.Rows[0]["KAM_Email"].ToString();
            txtrmemail.Text = DtGet.Rows[0]["RM_Email"].ToString();
            txtsamemail.Text = DtGet.Rows[0]["SAM_Email"].ToString();
            txtsamtlemail.Text = DtGet.Rows[0]["SAM_TL_EMAIL"].ToString();
            txttlemail.Text = DtGet.Rows[0]["TL_Email"].ToString();
            txtvhemail.Text = DtGet.Rows[0]["VH_Email"].ToString();
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
        }
    }
    protected void gvdetail_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (ExcelRawID != 0)
        {
            string rmQuery;
            if (mprod != "MO" && mprod != "")
            {
                rmQuery = "update tbl_mst_LeasedLinePRIData set  set B2B_Email='" + txtb2bemail.Text + "',B2B_Head_Email='" + txtb2bheademail.Text + "',Billing_Email_Id='" + txtbillingemail.Text + "',Coordinator_Contact_Email='" + txtcoordinatoremail.Text + "'"
                + ",Cust_Email = '" + txtcustemail.Text + "',KAM_Email = '" + txtkamemail.Text + "',RM_Email = '" + txtrmemail.Text + "',SAM_Email = '" + txtsamemail.Text + "',SAM_TL_EMAIL = '" + txtsamtlemail.Text + "',TL_Email = '" + txttlemail.Text + "',VH_Email = '" + txtvhemail.Text + "' where ExcelRawID='"+ExcelRawID.ToString()+"'";
            }
            else
            {
                rmQuery = "update tbl_mst_MOData set  set B2B_Email='" + txtb2bemail.Text + "',B2B_Head_Email='" + txtb2bheademail.Text + "',Billing_Email_Id='" + txtbillingemail.Text + "',Coordinator_Contact_Email='" + txtcoordinatoremail.Text + "'"
                + ",Cust_Email = '" + txtcustemail.Text + "',KAM_Email = '" + txtkamemail.Text + "',RM_Email = '" + txtrmemail.Text + "',SAM_Email = '" + txtsamemail.Text + "',SAM_TL_EMAIL = '" + txtsamtlemail.Text + "',TL_Email = '" + txttlemail.Text + "',VH_Email = '" + txtvhemail.Text + "' where Party_Name='" + ExcelRawID.ToString() + "' and Allocated_Date='' and Prod_Type_Name=''";
            }
            int Update = Lo.UpdateCodeSingleonly(rmQuery);
            if (Update != 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Records mail Update Successfully.')", true);
            }
            else
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Records mail not  Updated.')", true); }
        }

    }
}