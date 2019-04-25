using System;
using System.Web.UI.WebControls;
using System.Data;
using  BusinessLayer;
using System.Text;

public partial class frmviewZoneMapping : System.Web.UI.Page
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
        DataTable DTGrid = Lo.RetriveCodeZoneAreaMapping();
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
            Response.Redirect("frmAddZoneMapping.aspx?string=" + Convert.ToInt64(e.CommandArgument.ToString()) + "&other" + Resturl(142));
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