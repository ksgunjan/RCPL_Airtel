using BuisnessLayer;
using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAddLoginRegistration : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private Common Co = new Common();
    private HybridDictionary HySave = new HybridDictionary();
    private int Mid = 0;
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUserCategory();
            bindgrid();
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
        string str = Lo.SaveEmp(HySave, out _msg, out _sysMsg);
        if (str == "Save")
        {
            if (btnsub.Text != "Update")
            {
                bindgrid();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Record save successfully')", true);
            }
            else
            {
                bindgrid();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Record update successfully')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not save')", true);
        }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        if (txtname.Text != "" && txtpassword.Text != "")
        {
            SaveLoginRegistration();
            cleartext();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not save.Please fill all mandatory column.')", true);
        }
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
        txtpassword.Text = "";
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Image upload successfully.Click save new user for save record')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Image not upload successfully')", true);
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
            DataTable DtEmail = Lo.RetriveCodeLocal(0, txtemail.Text.Trim(), "", "", "RetriveEmailBtemail");
            if (DtEmail.Rows.Count > 0)
            {
                btnsub.Enabled = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Email already registerd')", true);
            }
            else
            {
                btnsub.Enabled = true;
            }
        }
    }
    protected void BindUserCategory()
    {
        DataTable DtBind = Lo.RetriveCodeLocal(0, "", "", "", "RetriveUType");
        if (DtBind.Rows.Count > 0)
        {
            Co.FillComboBox(ddlcategory, DtBind, "UserCategory", "UCategoryID");
        }
    }
    protected void bindgrid()
    {
        DataTable DTGrid = Lo.RetriveCodeLocal(0, "", "", "", "RetriveUser");
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
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string CompName = (gvcomp.Rows[rowIndex].FindControl("lbisacti") as Label).Text;
            if (CompName.ToString() == "Active")
            {
                ddlcategory.SelectedItem.Text = (gvcomp.Rows[rowIndex].FindControl("lblcat") as Label).Text;
                txtname.Text = (gvcomp.Rows[rowIndex].FindControl("lblname") as Label).Text;
                txtaddress.Text = (gvcomp.Rows[rowIndex].FindControl("lbladd") as Label).Text;
                txtemail.Text = (gvcomp.Rows[rowIndex].FindControl("lblemail") as Label).Text;
                txtgovtidno.Text = (gvcomp.Rows[rowIndex].FindControl("lbisacti") as Label).Text;
                txtmobno.Text = (gvcomp.Rows[rowIndex].FindControl("lblphone") as Label).Text;
                txtpassword.Text = (gvcomp.Rows[rowIndex].FindControl("lblpass") as Label).Text;
                lblpicdetail.Text = (gvcomp.Rows[rowIndex].FindControl("hfimg") as HiddenField).Value;
                btnsub.Text = "Update";
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record is deactive please active first for update.')", true);
            }
        }
        else if (e.CommandName == "active")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string CompName = (gvcomp.Rows[rowIndex].FindControl("lbisacti") as Label).Text;
            if (CompName.ToString() == "Active")
            {
                string Update1 = Lo.RetriveCodeLocalup(Convert.ToInt64(e.CommandArgument.ToString()), "N", "", "", "UUserStatus");
                if (Update1 == "true")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Record deactive successfully')", true);
                    bindgrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not update')", true);
                }
            }
            else
            {
                string Update1 = Lo.RetriveCodeLocalup(Convert.ToInt64(e.CommandArgument.ToString()), "Y", "", "", "UUserStatus");
                if (Update1 == "true")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Record active successfully')", true);
                    bindgrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not update')", true);
                }
            }

        }
    }
    protected void gvcomp_RowCreated(object sender, GridViewRowEventArgs e)
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
}