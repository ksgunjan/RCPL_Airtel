using System;
using BusinessLayer;
using System.Data;
using System.Configuration;
using System.IO;

public partial class frmFEDashBoard : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataTable DTCount = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Lo.verifyconnect();
                if (Session["Name"] != null)
                {
                    DataTable GetUser = Lo.RetriveCodeWithContidion("select top 1 ProductName,[FE Name] from tbl_trn_RawData where [FE Name]='" + Session["Name"].ToString() + "'");
                    if (GetUser.Rows.Count > 0)
                    {
                        if (GetUser.Rows[0]["ProductName"].ToString() == "Leased Line")
                        {
                            countlinkLeasedLine();
                            divMobility.Visible = false;
                            divleasedline.Visible = true;
                            divpri.Visible = false;
                        }
                        else if (GetUser.Rows[0]["ProductName"].ToString() == "MO")
                        {
                            countlinkMO();
                            divMobility.Visible = true;
                            divleasedline.Visible = false;
                            divpri.Visible = false;
                        }
                        else if (GetUser.Rows[0]["ProductName"].ToString() == "PRI-Fixed Line")
                        {
                            countPri();
                            divMobility.Visible = false;
                            divleasedline.Visible = false;
                            divpri.Visible = true;
                        }
                    }
                    else
                    {
                        divMobility.Visible = false;
                        divleasedline.Visible = false;
                        divpri.Visible = false;
                    }
                }
            }
            catch (Exception Ex)
            {
            }
        }
    }
    protected void countlinkLeasedLine()
    {
        try
        {
            DTCount = Lo.RetriveAllCount1("tbl_trn_RawData", "Leased Line");
            DataView Dv = new DataView(DTCount);
            Dv.RowFilter = "IsActive='Y' and [FE Name]='" + Session["Name"].ToString() + "' and [Eligible/Not Eligible]='Eligible'";
            divcompany.InnerText = Dv.Count.ToString();

            Dv.RowFilter = "IsActive='Y' and [FE Name]='" + Session["Name"] + "' and StatusByFE is not null and  [Eligible/Not Eligible]='Eligible'";
            divproduct.InnerText = Dv.Count.ToString();

            Dv.RowFilter = "IsActive='Y' and [FE Name]='" + Session["Name"] + "' and StatusByFE is null and [Eligible/Not Eligible]='Eligible'";
            divprotype.InnerText = Dv.Count.ToString();

            Dv.RowFilter = "IsActive='Y' and [FE Name]='" + Session["Name"] + "' and StatusByFE is null and [Eligible/Not Eligible]='Eligible'";
            divrevisit.InnerText = Dv.Count.ToString();

            DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and [FE Name]='" + Session["Name"] + "' and StatusByFE='Pending' and [Eligible/Not Eligible]='Eligible' and ([Revisit Date Time] ='" + DateTime.Now.ToString("dd/MMM/yyyy") + "' or [Revisit Date Time] >'" + DateTime.Now.ToString("dd/MMM/yyyy") + "') and productName='Leased Line'");
            if (DTCount.Rows.Count > 0)
            {
                divrevisit.InnerText = DTCount.Rows[0]["RCount"].ToString();
            }
            DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and [FE Name]='" + Session["Name"] + "' and PreMeetingDate ='" + DateTime.Now.ToString("dd/MMM/yyyy") + "' and [Eligible/Not Eligible]='Eligible' and productName='Leased Line'");
            if (DTCount.Rows.Count > 0)
            {
                divpreschedule.InnerText = DTCount.Rows[0]["RCount"].ToString();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void countlinkMO()
    {
        try
        {
            DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and [FE Name]='" + Session["Name"].ToString() + "' and [Eligible/Not Eligible]='Eligible' and ProductName='MO' group by [Party Name]");
            if (DTCount.Rows.Count > 0)
            {
                divmototal.InnerText = DTCount.Rows.Count.ToString();
            }
            else
            {
                divmototal.InnerText = DTCount.Rows.Count.ToString();
            }
            DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and [FE Name]='" + Session["Name"] + "' and StatusByFE is not null and  [Eligible/Not Eligible]='Eligible' and ProductName='MO' group by [Party Name]");
            if (DTCount.Rows.Count > 0)
            {
                divmovisitdone.InnerText = DTCount.Rows.Count.ToString();
            }
            else
            {
                divmovisitdone.InnerText = DTCount.Rows.Count.ToString();
            }
            DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and [FE Name]='" + Session["Name"] + "' and StatusByFE is null and [Eligible/Not Eligible]='Eligible' and ProductName='MO' group by [Party Name]");
            if (DTCount.Rows.Count > 0)
            {
                divmovisipen.InnerText = DTCount.Rows.Count.ToString();
            }
            else
            {
                divmovisipen.InnerText = DTCount.Rows.Count.ToString();
            }
            DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and [FE Name]='" + Session["Name"] + "' and StatusByFE='Pending' and [Eligible/Not Eligible]='Eligible' and ([Revisit Date Time] ='" + DateTime.Now.ToString("dd/MMM/yyyy") + "' or [Revisit Date Time] >'" + DateTime.Now.ToString("dd/MMM/yyyy") + "') and ProductName='MO' group by [Party Name]");
            if (DTCount.Rows.Count > 0)
            {
                divvisitrevi.InnerText = DTCount.Rows.Count.ToString();
            }
            else
            {
                divvisitrevi.InnerText = DTCount.Rows.Count.ToString();
            }
            DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and [FE Name]='" + Session["Name"] + "' and PreMeetingDate ='" + DateTime.Now.ToString("dd/MMM/yyyy") + "' and [Eligible/Not Eligible]='Eligible' and ProductName='MO' group by [Party Name]");
            if (DTCount.Rows.Count > 0)
            {
                divpresschdulemo.InnerText = DTCount.Rows.Count.ToString();
            }
            else
            {
                divpresschdulemo.InnerText = DTCount.Rows.Count.ToString();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void countPri()
    {
        try
        {
            DTCount = Lo.RetriveAllCount1("tbl_trn_RawData", "PRI-Fixed Line");
            DataView Dv = new DataView(DTCount);
            Dv.RowFilter = "IsActive='Y' and [FE Name]='" + Session["Name"].ToString() + "' and [Eligible/Not Eligible]='Eligible'";
            divtotalpri.InnerText = Dv.Count.ToString();

            Dv.RowFilter = "IsActive='Y' and [FE Name]='" + Session["Name"] + "' and StatusByFE is not null and  [Eligible/Not Eligible]='Eligible'";
            divtotalvisitpri.InnerText = Dv.Count.ToString();

            Dv.RowFilter = "IsActive='Y' and [FE Name]='" + Session["Name"] + "' and StatusByFE is null and [Eligible/Not Eligible]='Eligible'";
            divtotalpendingpri.InnerText = Dv.Count.ToString();

            DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and [FE Name]='" + Session["Name"] + "' and StatusByFE='Pending' and [Eligible/Not Eligible]='Eligible' and ([Revisit Date Time] ='" + DateTime.Now.ToString("dd/MMM/yyyy") + "' or [Revisit Date Time] >'" + DateTime.Now.ToString("dd/MMM/yyyy") + "') and productName='PRI-Fixed Line'");
            if (DTCount.Rows.Count > 0)
            {
                divtotalpenpri.InnerText = DTCount.Rows[0]["RCount"].ToString();
            }
            DTCount = Lo.RetriveAllCount("tbl_trn_RawData", "IsActive='Y' and [FE Name]='" + Session["Name"] + "' and PreMeetingDate ='" + DateTime.Now.ToString("dd/MMM/yyyy") + "' and [Eligible/Not Eligible]='Eligible' and productName='PRI-Fixed Line'");
            if (DTCount.Rows.Count > 0)
            {
                divprepri.InnerText = DTCount.Rows[0]["RCount"].ToString();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void CheckNegativeCaseOrSendMail()
    {
        DataTable DtEmailNeativeCase = Lo.RetriveCodeAllExcelRecordWhereCondition("StatusByFE='Negative' and IsClosed='N' and [Eligible/Not Eligible]='Eligible' and SendNegativeMailtoAll!='5'");
        if (DtEmailNeativeCase.Rows.Count > 0)
        {
            for (int i = 0; DtEmailNeativeCase.Rows.Count > i; i++)
            {
                DateTime LastEmailDate = Convert.ToDateTime(DtEmailNeativeCase.Rows[i]["StatusUpdateDate"]);
                DateTime CheckFInalDate = LastEmailDate.AddDays(5);
                DateTime CheckFInalDate1 = LastEmailDate.AddDays(10);
                DateTime CheckFInalDate2 = LastEmailDate.AddDays(15);
                DateTime CheckFInalDate3 = LastEmailDate.AddDays(18);
                //This code send mail for Five day after negative case found
                if (CheckFInalDate == Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy")) && DtEmailNeativeCase.Rows[i]["StatusByFE"].ToString() == "Negative" && DtEmailNeativeCase.Rows[i]["SendNegativeMailtoAll"].ToString() == "1")
                {
                    string body;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/NegativeCaseMailAfter5Days.htm")))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{ClientName}", DtEmailNeativeCase.Rows[i]["Customer Name"].ToString());
                    SendMail s;
                    s = new SendMail();
                    if ("Leased Line" == DtEmailNeativeCase.Rows[i]["ProductName"].ToString())
                    {
                        if (DtEmailNeativeCase.Rows[i]["Cust Email"].ToString() != "")
                        {
                            s.CreateMail("verification@rcpl.in", DtEmailNeativeCase.Rows[i]["Cust Email"].ToString(), "RE: Airtel " + DtEmailNeativeCase.Rows[i]["ProductName"].ToString() + " Verification  " + DtEmailNeativeCase.Rows[i]["ProdTypeName"].ToString() + " - " + DtEmailNeativeCase.Rows[i]["Party Name"].ToString() + "", body, "Leased Line", "Negative", DtEmailNeativeCase.Rows[i]["SAM_Email"].ToString());

                            string exMsg = "";
                            bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
                            if (result == true && exMsg == "")
                            {
                                Int32 UpdateCaseStatus = Lo.UpdateCodeOther("tbl_trn_RawData", "Set SendNegativeMailtoAll='2'", "UniqueID='" + DtEmailNeativeCase.Rows[i]["UniqueID"].ToString() + "'");
                            }
                        }
                        else
                        {
                            Int32 UpdateCaseStatus = Lo.UpdateCodeOther("tbl_trn_RawData", "Set SendNegativeMailtoAll='2'", "UniqueID='" + DtEmailNeativeCase.Rows[i]["UniqueID"].ToString() + "'");

                        }
                    }
                }
                //This code send mail for 10 day after negative case found
                if (CheckFInalDate1 == Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy")) && DtEmailNeativeCase.Rows[i]["StatusByFE"].ToString() == "Negative" && DtEmailNeativeCase.Rows[i]["SendNegativeMailtoAll"].ToString() == "2")
                {
                    string body;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/NegativeCaseMailAfter5Days.htm")))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{ClientName}", DtEmailNeativeCase.Rows[i]["Customer Name"].ToString());
                    SendMail s;
                    s = new SendMail();
                    if ("Leased Line" == DtEmailNeativeCase.Rows[i]["ProductName"].ToString())
                    {
                        if (DtEmailNeativeCase.Rows[i]["SAM_Email"].ToString() != "")
                        {
                            s.CreateMail("verification@rcpl.in", DtEmailNeativeCase.Rows[i]["SAM_Email"].ToString(), "RE: Airtel " + DtEmailNeativeCase.Rows[i]["ProductName"].ToString() + " Verification  " + DtEmailNeativeCase.Rows[i]["ProdTypeName"].ToString() + " - " + DtEmailNeativeCase.Rows[i]["Party Name"].ToString() + "", body, "Leased Line", "Negative", DtEmailNeativeCase.Rows[i]["SAM_TL_EMAIL"].ToString());

                            string exMsg = "";
                            bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
                            if (result == true && exMsg == "")
                            {
                                Int32 UpdateCaseStatus = Lo.UpdateCodeOther("tbl_trn_RawData", "Set SendNegativeMailtoAll='3'", "UniqueID='" + DtEmailNeativeCase.Rows[i]["UniqueID"].ToString() + "'");
                            }
                        }
                        else
                        {
                            Int32 UpdateCaseStatus = Lo.UpdateCodeOther("tbl_trn_RawData", "Set SendNegativeMailtoAll='3'", "UniqueID='" + DtEmailNeativeCase.Rows[i]["UniqueID"].ToString() + "'");
                        }
                    }
                }
                //This code send mail for 15 day after negative case found
                else if (CheckFInalDate2 == Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy")) && DtEmailNeativeCase.Rows[i]["StatusByFE"].ToString() == "Negative" && DtEmailNeativeCase.Rows[i]["SendNegativeMailtoAll"].ToString() == "3")
                {
                    string body;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/NegativeCaseMailAfter5Days.htm")))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{ClientName}", DtEmailNeativeCase.Rows[i]["Customer Name"].ToString());
                    SendMail s;
                    s = new SendMail();
                    if ("Leased Line" == DtEmailNeativeCase.Rows[i]["ProductName"].ToString())
                    {
                        if (DtEmailNeativeCase.Rows[i]["SAM_Email"].ToString() != "" && DtEmailNeativeCase.Rows[i]["SAM_TL_EMAIL"].ToString() != "" && DtEmailNeativeCase.Rows[i]["Cust Email"].ToString() != "")
                        {
                            s.CreateMail("verification@rcpl.in", DtEmailNeativeCase.Rows[i]["SAM_Email"].ToString() + "," + DtEmailNeativeCase.Rows[i]["SAM_TL_EMAIL"].ToString() + "," + DtEmailNeativeCase.Rows[i]["Cust Email"].ToString(), "RE: Airtel " + DtEmailNeativeCase.Rows[i]["ProductName"].ToString() + " Verification  " + DtEmailNeativeCase.Rows[i]["ProdTypeName"].ToString() + " - " + DtEmailNeativeCase.Rows[i]["Party Name"].ToString() + "", body, "Leased Line", "Negative", "");

                            string exMsg = "";
                            bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
                            if (result == true && exMsg == "")
                            {
                                Int32 UpdateCaseStatus = Lo.UpdateCodeOther("tbl_trn_RawData", "Set SendNegativeMailtoAll='4'", "UniqueID='" + DtEmailNeativeCase.Rows[i]["UniqueID"].ToString() + "'");
                            }
                        }
                        else
                        {
                            Int32 UpdateCaseStatus = Lo.UpdateCodeOther("tbl_trn_RawData", "Set SendNegativeMailtoAll='4'", "UniqueID='" + DtEmailNeativeCase.Rows[i]["UniqueID"].ToString() + "'");
                        }
                    }
                }
                //This code send mail for 18 day after negative case found
                else if (CheckFInalDate3 == Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy")) && DtEmailNeativeCase.Rows[i]["StatusByFE"].ToString() == "Negative" && DtEmailNeativeCase.Rows[i]["SendNegativeMailtoAll"].ToString() == "4")
                {
                    string body;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/NegativeCaseMailAfter5Days.htm")))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{ClientName}", DtEmailNeativeCase.Rows[i]["Customer Name"].ToString());
                    SendMail s;
                    s = new SendMail();
                    if ("Leased Line" == DtEmailNeativeCase.Rows[i]["ProductName"].ToString())
                    {
                        if (DtEmailNeativeCase.Rows[i]["SAM_Email"].ToString() != "" && DtEmailNeativeCase.Rows[i]["SAM_TL_EMAIL"].ToString() != "" && DtEmailNeativeCase.Rows[i]["B2BMailID"].ToString() != "")
                        {
                            s.CreateMail("verification@rcpl.in", DtEmailNeativeCase.Rows[i]["SAM_Email"].ToString() + "," + DtEmailNeativeCase.Rows[i]["SAM_TL_EMAIL"].ToString() + "," + DtEmailNeativeCase.Rows[i]["B2B_Head_Email"].ToString() + "", "RE: Airtel " + DtEmailNeativeCase.Rows[i]["ProductName"].ToString() + " Verification  " + DtEmailNeativeCase.Rows[i]["ProdTypeName"].ToString() + " - " + DtEmailNeativeCase.Rows[i]["Party Name"].ToString() + "", body, "LastMailNegative", "Negative", "");
                            string exMsg = "";
                            bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
                            if (result == true && exMsg == "")
                            {
                                Int32 UpdateCaseStatus = Lo.UpdateCodeOther("tbl_trn_RawData", "Set IsClosed='Y',SendNegativeMailtoAll='5'", "UniqueID='" + DtEmailNeativeCase.Rows[i]["UniqueID"].ToString() + "'");
                            }
                        }
                        else
                        {
                            Int32 UpdateCaseStatus = Lo.UpdateCodeOther("tbl_trn_RawData", "Set IsClosed='Y',SendNegativeMailtoAll='5'", "UniqueID='" + DtEmailNeativeCase.Rows[i]["UniqueID"].ToString() + "'");
                        }
                    }
                }
            }
        }
    }
}