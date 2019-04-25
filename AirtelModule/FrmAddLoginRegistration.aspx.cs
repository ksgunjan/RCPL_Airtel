using System;
using BusinessLayer;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
public partial class FrmAddLoginRegistration : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Common Co = new Common();
    HybridDictionary HySave = new HybridDictionary();
    Int32 Mid = 0;
    string _msg = string.Empty;
    string _sysMsg = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            BindUserCategory();
            update();
        }
    }
    protected void SaveLoginRegistration()
    {
        if (hfEmpId.Value == "")
        {
            HySave["EmpID"] = Mid;
        }
        else
        {
            HySave["EmpID"] = Convert.ToInt32(hfEmpId.Value);
        }
        HySave["Category"] = ddlcategory.SelectedItem.Text.Trim();
        HySave["Name"] = txtname.Text.Trim();
        HySave["Email"] = txtemail.Text.Trim();
        HySave["ContactNo"] = txtmobno.Text.Trim();
        HySave["Address"] = txtaddress.Text.Trim();
        HySave["GovtIDNo"] = txtgovtidno.Text.Trim();
        HySave["Password"] = txtpassword.Text;
        HySave["ProfilePic"] = lblpicdetail.Text;
        if (ddlcategory.SelectedItem.Text == "Field Executive")
        {
            if (ddllocation.SelectedItem.Text != "Select Zone Area")
            {
                HySave["FEZoneAreaLocation"] = ddllocation.SelectedItem.Text.Substring(0, ddllocation.SelectedItem.Text.LastIndexOf("+"));
            }
            else
            {
                HySave["FEZoneAreaLocation"] = "";
            }
        }
        else
        {
            HySave["FEZoneAreaLocation"] = "";
        }
        string str = Lo.SaveEmp(HySave, out _msg, out _sysMsg);
        if (str == "Save")
        {
            if (btnsub.Text != "Update")
            {
                divErr.Visible = true;
                divErr.InnerHtml = "Record save successfully";
                divErr.Attributes.Add("class", "alert alert-success");
            }
            else
            {
                divErr.Visible = true;
                divErr.InnerHtml = "Record update successfully";
                divErr.Attributes.Add("class", "alert alert-success");
            }
        }
        else
        {
            divErr.Visible = true;
            divErr.InnerHtml = "Record not save";
            divErr.Attributes.Add("class", "alert alert-danger");
        }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        if (txtname.Text != "" && txtpassword.Text != "")
        {
            //if (IsValidEmailId(txtemail.Text.Trim()) == true)
            //{
            SaveLoginRegistration();
            cleartext();
            // }
            //else
            //{
            //    divErr.Visible = true;
            //    divErr.InnerHtml = "Email not in a valid format";
            //    divErr.Attributes.Add("class", "alert alert-danger");
            //}
        }
        else
        {
            divErr.Visible = true;
            divErr.InnerHtml = "Record not save.Please fill all mandatory column.";
            divErr.Attributes.Add("class", "alert alert-danger");
        }
    }
    public static bool IsValidEmailId(string InputEmail)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(InputEmail);
        if (match.Success)
            return true;
        else
            return false;
    }
    protected void cleartext()
    {
        txtaddress.Text = "";
        txtemail.Text = "";
        txtgovtidno.Text = "";
        txtmobno.Text = "";
        txtname.Text = "";
        lblpicdetail.Text = "";
        hfEmpId.Value = "";
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    public void Photographupload()
    {
        try
        {
            if (fupic.HasFile != false)
            {
                string mFileName = Path.GetFileName(fupic.PostedFile.FileName);
                string SPhotoName = txtname.Text + DateTime.Now.ToString("hhmmss") + mFileName;
                fupic.PostedFile.SaveAs(Server.MapPath("ProfileImage/Temp/") + SPhotoName);
                System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath("ProfileImage/Temp/") + SPhotoName);
                System.Drawing.Image thumbnail = image.GetThumbnailImage(250, 250, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
                thumbnail.Save(Server.MapPath("ProfileImage") + "\\" + SPhotoName);
                lblpicdetail.Text = SPhotoName;
                divErr.Visible = true;
                divErr.InnerHtml = "Image upload successfully.Click save new user for save record";
                divErr.Attributes.Add("class", "alert alert-success");
            }
            else
            {
                divErr.Visible = true;
                divErr.InnerHtml = "Image not upload successfully";
                divErr.Attributes.Add("class", "alert alert-danger");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    public bool ThumbnailCallback()
    {
        return true;
    }
    protected void btnuploadimage_Click(object sender, EventArgs e)
    {
        Photographupload();
    }
    protected void txtemail_TextChanged(object sender, EventArgs e)
    {
        if (txtemail.Text != "")
        {
            DataTable DtEmail = Lo.RetriveCodeUserEmail(txtemail.Text.Trim());
            if (DtEmail.Rows.Count > 0)
            {
                btnsub.Enabled = false;
                divErr.Visible = true;
                divErr.InnerHtml = "Email already registerd";
                divErr.Attributes.Add("class", "alert alert-danger");
            }
            else
            {
                divErr.Visible = true;
                divErr.Visible = false;
                btnsub.Enabled = true;
            }
        }
    }
    protected void update()
    {
        if (Request.QueryString["string"] != null)
        {

            hfEmpId.Value = Convert.ToInt64(Request.QueryString["string"]).ToString();
            DataTable DTComp = Lo.RetriveCodeUser(Convert.ToInt64(hfEmpId.Value));
            if (DTComp.Rows.Count > 0)
            {
                if (DTComp.Rows[0]["IsActive"].ToString().Trim() == "Y")
                {
                    txtname.Text = DTComp.Rows[0]["Name"].ToString();
                    txtaddress.Text = DTComp.Rows[0]["Address"].ToString();
                    txtemail.Text = DTComp.Rows[0]["UserName"].ToString();
                    txtgovtidno.Text = DTComp.Rows[0]["GovtIDNo"].ToString();
                    txtmobno.Text = DTComp.Rows[0]["ContactNo"].ToString();
                    txtpassword.Text = DTComp.Rows[0]["Password"].ToString();
                    lblpicdetail.Text = DTComp.Rows[0]["ProfilePic"].ToString();
                    btnsub.Text = "Update";
                }
                else
                {
                    divErr.Visible = true;
                    divErr.InnerHtml = "Record is deactive please active first for update.";
                    divErr.Attributes.Add("class", "alert alert-danger");
                }
            }
        }
    }
    protected void BindUserCategory()
    {
        DataTable DtBind = Lo.RetriveBindDDL("UCategoryID", "UserCategory", "tbl_mst_UserCategory where IsActive='Y'");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlcategory, DtBind, "UserCategory", "UCategoryID");
        }
    }
    protected void BindZoneLocationFE()
    {
        DataTable DtBind = Lo.RetriveBindDDL("ZoneMappingID", "(c.ZoneAreaCode+' + '+c.ZoneAreaName) AS ZoneAreaCode", "tbl_trn_ZoneMapping c where IsActive='Y'");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddllocation, DtBind, "ZoneAreaCode", "ZoneMappingID");
            ddllocation.Items.Insert(0, "Select Zone Area");
        }
    }
    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcategory.SelectedItem.Text == "Field Executive")
        {
            BindZoneLocationFE();
            divlocfe.Visible = true;
        }
        else
        {
            divlocfe.Visible = false;
        }
    }




}