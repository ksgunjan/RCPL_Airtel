using System;
using BusinessLayer;
using System.Data;
using System.Collections.Specialized;

public partial class frmAddZoneMapping : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Common Co = new Common();
    HybridDictionary hysavecomp = new HybridDictionary();
    string _msg = string.Empty;
    string _sysMsg = string.Empty;
    Int32 Mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            BindZone();
            BindZoneArea();
            update();
        }
    }
    protected void SaveComp()
    {
        if (hfzonemapping.Value != "")
        {
            hysavecomp["ZoneMappingID"] = Convert.ToInt32(hfzonemapping.Value);
        }
        else
        {
            hysavecomp["ZoneMappingID"] = Mid;
        }
        hysavecomp["ZoneName"] = ddlzonename.SelectedItem.Text.Trim();
        hysavecomp["ZoneAreaCode"] = "Z- " + ddlzoneareaname.SelectedItem.Text.Substring(0, ddlzoneareaname.SelectedItem.Text.LastIndexOf("+")) + " -" + ddlzoneareaname.SelectedItem.Value.Trim();
        hysavecomp["ZoneAreaID"] = ddlzoneareaname.SelectedItem.Value;
        hysavecomp["ZoneAreaName"] = ddlzoneareaname.SelectedItem.Text.Substring(8);
        string str = Lo.SaveZoneAreaMapping(hysavecomp, out _msg, out _sysMsg);
        if (str == "Save")
        {
            if (btnsub.Text != "Update")
            {
                cleartext();
                diverror.InnerHtml = "Zone-Map save successfully";
                diverror.Attributes.Add("class", "alert alert-success");
            }
            else
            {
                cleartext();
                diverror.InnerHtml = "Zone-Map update or save successfully";
                diverror.Attributes.Add("class", "alert alert-success");
                btnsub.Text = "Submit";
            }
        }
        else
        {
            diverror.InnerHtml = "Zone-Map not save";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        if (ddlzonename.Text != "" && ddlzoneareaname.Text != "")
        {
            System.Threading.Thread.Sleep(1000);
            SaveComp();
        }
        else
        {
            diverror.InnerHtml = "Fill Product Name";
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    protected void cleartext()
    {
        BindZoneArea();
        BindZone();
        hfzonemapping.Value = "";
    }
    protected void update()
    {
        if (Request.QueryString["string"] != null)
        {
            hfzonemapping.Value = Convert.ToInt64(Request.QueryString["string"]).ToString();
            DataTable DTComp = Lo.RetriveCodeZoneAreaMapping(Convert.ToInt64(hfzonemapping.Value));
            if (DTComp.Rows.Count > 0)
            {
                ddlzonename.SelectedItem.Value = DTComp.Rows[0]["ZoneName"].ToString();
                ddlzonename.SelectedItem.Text = DTComp.Rows[0]["ZoneName"].ToString();
                ddlzoneareaname.SelectedItem.Value = DTComp.Rows[0]["ZoneAreaID"].ToString();
                string zonearea = DTComp.Rows[0]["ZoneAreaCode"].ToString().Substring(0, DTComp.Rows[0]["ZoneAreaCode"].ToString().LastIndexOf("-"));
                string againzonearea = zonearea.Substring(2);
                ddlzoneareaname.SelectedItem.Text = againzonearea + " + " + DTComp.Rows[0]["ZoneArea"].ToString();
                btnsub.Text = "Update";
            }
        }
    }
    protected void BindZoneArea()
    {
        DataTable DtBind = Lo.RetriveBindDDL("ZoneAreaID", "(c.ZoneAreaPinCode+' + '+c.ZoneArea) AS ZoneAreaWithPinCode", "tbl_mst_ZoneArea c where IsActive='Y'");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlzoneareaname, DtBind, "ZoneAreaWithPinCode", "ZoneAreaID");
        }
    }
    protected void BindZone()
    {
        DataTable DtBind = Lo.RetriveBindDDL("ZoneID", "ZoneName", "tbl_mst_Zone where IsActive='Y'");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlzonename, DtBind, "ZoneName", "ZoneID");
        }
    }
}