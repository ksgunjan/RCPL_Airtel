using System;
using System.Data;
using BusinessLayer;
using System.IO;
using System.Configuration;
using ClosedXML.Excel;

public partial class frmMonthWiseFeWorkReport : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Common Co = new Common();
    DataTable DTSearch = new DataTable();
    DataTable DTSearchMO = new DataTable();
    DataTable DTSearchPRI = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lo.verifyconnect();
            bindcurrentmontheport();
            bindcurrentmontheportMO();
        }
    }
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmonth.SelectedItem.Value == "01")
        {

        }
        else
        {

        }
    }
    protected void cleartext()
    {
        ddlmonth.Text = "Select Month";
    }
    protected void bindcurrentmontheport()
    {
        DTSearch = Lo.RetriveCodeWithContidion("IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp SELECT DISTINCT TOP 100 PERCENT datepart(day,statusupdatedate) as [Day] into #temp FROM tbl_trn_RawData where StatusUpdateDate is not null  and DATENAME(MONTH,StatusUpdateDate)='" + DateTime.Now.ToString("MMMM") + "' order by datepart(day,statusupdatedate) CREATE CLUSTERED INDEX cix_wekno ON #temp([Day] ASC)DECLARE @Pivot_Column [nvarchar](max); DECLARE @Query [nvarchar](max); SELECT @Pivot_Column= COALESCE(@Pivot_Column+',','')+ QUOTENAME([DAY])  FROM (select [day] from #temp)  Tab SELECT @Query='SELECT [FE Name], '+@Pivot_Column+' FROM(select [FE name],[Party Name] as [Total], datepart(day,statusupdatedate) as [Day] FROM tbl_trn_RawData where StatusUpdateDate is not null and ProductName=''Leased Line'' and DATENAME(MONTH,StatusUpdateDate)=''" + DateTime.Now.ToString("MMMM") + "'' group by [FE name],[party name],statusupdatedate) Tab1 PIVOT(count([Total]) FOR [Day] IN ('+@Pivot_Column+')) AS Tab2 ' EXEC  sp_executesql  @Query  DROP TABLE #temp");
        if (DTSearch != null)
        {
            gvfedailyreport.DataSource = DTSearch;
            gvfedailyreport.DataBind();
            gvfedailyreport.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void bindcurrentmontheportMO()
    {
        DTSearch = Lo.RetriveCodeWithContidion("IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp SELECT DISTINCT TOP 100 PERCENT datepart(day,statusupdatedate) as [Day] into #temp FROM tbl_trn_RawData where StatusUpdateDate is not null  and DATENAME(MONTH,StatusUpdateDate)='" + DateTime.Now.ToString("MMMM") + "' order by datepart(day,statusupdatedate)CREATE CLUSTERED INDEX cix_wekno ON #temp([Day] ASC)DECLARE @Pivot_Column [nvarchar](max); DECLARE @Query [nvarchar](max); SELECT @Pivot_Column= COALESCE(@Pivot_Column+',','')+ QUOTENAME([DAY])  FROM (select [day] from #temp)  Tab SELECT @Query='SELECT [FE Name], '+@Pivot_Column+' FROM(select [FE name],[Party Name] as [Total], datepart(day,statusupdatedate) as [Day] FROM tbl_trn_RawData where StatusUpdateDate is not null and ProductName=''MO'' and DATENAME(MONTH,StatusUpdateDate)=''" + DateTime.Now.ToString("MMMM") + "''  group by [FE name],[party name],statusupdatedate) Tab1 PIVOT(count([Total]) FOR [Day] IN ('+@Pivot_Column+')) AS Tab2 ' EXEC  sp_executesql  @Query  DROP TABLE #temp");
        if (DTSearch != null)
        {
            gridviewMo.DataSource = DTSearch;
            gridviewMo.DataBind();
            gridviewMo.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void bindcurrentmontheportPRI()
    {
        DTSearch = Lo.RetriveCodeWithContidion("IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp SELECT DISTINCT TOP 100 PERCENT datepart(day,statusupdatedate) as [Day] into #temp FROM tbl_trn_RawData where StatusUpdateDate is not null  and DATENAME(MONTH,StatusUpdateDate)='" + DateTime.Now.ToString("MMMM") + "' order by datepart(day,statusupdatedate) CREATE CLUSTERED INDEX cix_wekno ON #temp([Day] ASC)DECLARE @Pivot_Column [nvarchar](max); DECLARE @Query [nvarchar](max); SELECT @Pivot_Column= COALESCE(@Pivot_Column+',','')+ QUOTENAME([DAY])  FROM (select [day] from #temp)  Tab SELECT @Query='SELECT [FE Name], '+@Pivot_Column+' FROM(select [FE name],[Party Name] as [Total], datepart(day,statusupdatedate) as [Day] FROM tbl_trn_RawData where StatusUpdateDate is not null and ProductName=''PRI-Fixed Line'' and DATENAME(MONTH,StatusUpdateDate)=''" + DateTime.Now.ToString("MMMM") + "'' group by [FE name],[party name],statusupdatedate) Tab1 PIVOT(count([Total]) FOR [Day] IN ('+@Pivot_Column+')) AS Tab2 ' EXEC  sp_executesql  @Query  DROP TABLE #temp");
        if (DTSearch != null)
        {
            gvpri.DataSource = DTSearch;
            gvpri.DataBind();
            gvpri.Visible = true;
            divexcel.Visible = true;
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            DTSearch = Lo.RetriveCodeWithContidion("IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp SELECT DISTINCT TOP 100 PERCENT datepart(day,statusupdatedate) as [Day] into #temp FROM tbl_trn_RawData where StatusUpdateDate is not null  and DATENAME(MONTH,StatusUpdateDate)='" + ddlmonth.SelectedItem.Text + "' order by datepart(day,statusupdatedate) CREATE CLUSTERED INDEX cix_wekno ON #temp([Day] ASC)DECLARE @Pivot_Column [nvarchar](max); DECLARE @Query [nvarchar](max); SELECT @Pivot_Column= COALESCE(@Pivot_Column+',','')+ QUOTENAME([DAY])  FROM (select [day] from #temp)  Tab SELECT @Query='SELECT [FE Name], '+@Pivot_Column+' FROM(select [FE name],[Party Name] as [Total], datepart(day,statusupdatedate) as [Day] FROM tbl_trn_RawData where StatusUpdateDate is not null and ProductName=''Leased Line'' and DATENAME(MONTH,StatusUpdateDate)=''" + ddlmonth.SelectedItem.Text + "'' group by [FE name],[party name],statusupdatedate) Tab1 PIVOT(count([Total]) FOR [Day] IN ('+@Pivot_Column+')) AS Tab2 ' EXEC  sp_executesql  @Query  DROP TABLE #temp");
            if (DTSearch != null)
            {
                gvfedailyreport.DataSource = DTSearch;
                gvfedailyreport.DataBind();
                gvfedailyreport.Visible = true;
                divexcel.Visible = true;
                diverror.Visible = true;
                diverror.InnerHtml = "Successfully fetch result";
                diverror.Attributes.Add("Class", "alert alert-success");
            }
            else
            {
                gvfedailyreport.Visible = false;
                divexcel.Visible = false;
                diverror.Visible = true;
                diverror.InnerHtml = "No Record Found";
                diverror.Attributes.Add("Class", "alert alert-danger");
            }
            DTSearchMO = Lo.RetriveCodeWithContidion("IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp SELECT DISTINCT TOP 100 PERCENT datepart(day,statusupdatedate) as [Day] into #temp FROM tbl_trn_RawData where StatusUpdateDate is not null  and DATENAME(MONTH,StatusUpdateDate)='" + ddlmonth.SelectedItem.Text + "' order by datepart(day,statusupdatedate)CREATE CLUSTERED INDEX cix_wekno ON #temp([Day] ASC)DECLARE @Pivot_Column [nvarchar](max); DECLARE @Query [nvarchar](max); SELECT @Pivot_Column= COALESCE(@Pivot_Column+',','')+ QUOTENAME([DAY])  FROM (select [day] from #temp)  Tab SELECT @Query='SELECT [FE Name], '+@Pivot_Column+' FROM(select [FE name],[Party Name] as [Total], datepart(day,statusupdatedate) as [Day] FROM tbl_trn_RawData where StatusUpdateDate is not null and ProductName=''MO'' and DATENAME(MONTH,StatusUpdateDate)=''" + ddlmonth.SelectedItem.Text + "''  group by [FE name],[party name],statusupdatedate) Tab1 PIVOT(count([Total]) FOR [Day] IN ('+@Pivot_Column+')) AS Tab2 ' EXEC  sp_executesql  @Query  DROP TABLE #temp");
            if (DTSearchMO != null)
            {
                gridviewMo.DataSource = DTSearchMO;
                gridviewMo.DataBind();
                gridviewMo.Visible = true;
                div1.Visible = true;
                diverror.Visible = true;
                diverror.InnerHtml = "Successfully fetch result";
                diverror.Attributes.Add("Class", "alert alert-success");
            }
            else
            {
                gridviewMo.Visible = false;
                div1.Visible = false;
                diverror.Visible = true;
                diverror.InnerHtml = "No Record Found";
                diverror.Attributes.Add("Class", "alert alert-danger");
            }
            DTSearchPRI = Lo.RetriveCodeWithContidion("IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp SELECT DISTINCT TOP 100 PERCENT datepart(day,statusupdatedate) as [Day] into #temp FROM tbl_trn_RawData where StatusUpdateDate is not null  and DATENAME(MONTH,StatusUpdateDate)='" + DateTime.Now.ToString("MMMM") + "' order by datepart(day,statusupdatedate) CREATE CLUSTERED INDEX cix_wekno ON #temp([Day] ASC)DECLARE @Pivot_Column [nvarchar](max); DECLARE @Query [nvarchar](max); SELECT @Pivot_Column= COALESCE(@Pivot_Column+',','')+ QUOTENAME([DAY])  FROM (select [day] from #temp)  Tab SELECT @Query='SELECT [FE Name], '+@Pivot_Column+' FROM(select [FE name],[Party Name] as [Total], datepart(day,statusupdatedate) as [Day] FROM tbl_trn_RawData where StatusUpdateDate is not null and ProductName=''PRI-Fixed Line'' and DATENAME(MONTH,StatusUpdateDate)=''" + DateTime.Now.ToString("MMMM") + "'' group by [FE name],[party name],statusupdatedate) Tab1 PIVOT(count([Total]) FOR [Day] IN ('+@Pivot_Column+')) AS Tab2 ' EXEC  sp_executesql  @Query  DROP TABLE #temp");
            if (DTSearchPRI != null)
            {
                gvpri.DataSource = DTSearchPRI;
                gvpri.DataBind();
                gvpri.Visible = true;
                diverror.Visible = true;
                diverror.InnerHtml = "Successfully fetch result";
                diverror.Attributes.Add("Class", "alert alert-success");
            }
            else
            {
                gvpri.Visible = false;
                diverror.Visible = true;
                diverror.InnerHtml = "No Record Found";
                diverror.Attributes.Add("Class", "alert alert-danger");
            }
        }
        catch (Exception ex)
        {
            div1.Visible = false;
            gridviewMo.Visible = false;
            diverror.Visible = true;
            diverror.InnerHtml = "No Record Found";
            diverror.Attributes.Add("Class", "alert alert-danger");
        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlmonth.SelectedItem.Text != "Select Month")
            {
                DTSearch = Lo.RetriveCodeWithContidion("IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp SELECT DISTINCT TOP 100 PERCENT datepart(day,statusupdatedate) as [Day] into #temp FROM tbl_trn_RawData where StatusUpdateDate is not null  and DATENAME(MONTH,StatusUpdateDate)='" + ddlmonth.SelectedItem.Text + "' order by datepart(day,statusupdatedate) CREATE CLUSTERED INDEX cix_wekno ON #temp([Day] ASC)DECLARE @Pivot_Column [nvarchar](max); DECLARE @Query [nvarchar](max); SELECT @Pivot_Column= COALESCE(@Pivot_Column+',','')+ QUOTENAME([DAY])  FROM (select [day] from #temp)  Tab SELECT @Query='SELECT [FE Name], '+@Pivot_Column+' FROM(select [FE name],[Party Name] as [Total], datepart(day,statusupdatedate) as [Day] FROM tbl_trn_RawData where StatusUpdateDate is not null and ProductName=''Leased Line'' and DATENAME(MONTH,StatusUpdateDate)=''" + ddlmonth.SelectedItem.Text + "'' group by [FE name],[party name],statusupdatedate) Tab1 PIVOT(count([Total]) FOR [Day] IN ('+@Pivot_Column+')) AS Tab2 ' EXEC  sp_executesql  @Query  DROP TABLE #temp");
                DTSearchMO = Lo.RetriveCodeWithContidion("IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp SELECT DISTINCT TOP 100 PERCENT datepart(day,statusupdatedate) as [Day] into #temp FROM tbl_trn_RawData where StatusUpdateDate is not null  and DATENAME(MONTH,StatusUpdateDate)='" + ddlmonth.SelectedItem.Text + "' order by datepart(day,statusupdatedate)CREATE CLUSTERED INDEX cix_wekno ON #temp([Day] ASC)DECLARE @Pivot_Column [nvarchar](max); DECLARE @Query [nvarchar](max); SELECT @Pivot_Column= COALESCE(@Pivot_Column+',','')+ QUOTENAME([DAY])  FROM (select [day] from #temp)  Tab SELECT @Query='SELECT [FE Name], '+@Pivot_Column+' FROM(select [FE name],[Party Name] as [Total], datepart(day,statusupdatedate) as [Day] FROM tbl_trn_RawData where StatusUpdateDate is not null and ProductName=''MO'' and DATENAME(MONTH,StatusUpdateDate)=''" + ddlmonth.SelectedItem.Text + "''  group by [FE name],[party name],statusupdatedate) Tab1 PIVOT(count([Total]) FOR [Day] IN ('+@Pivot_Column+')) AS Tab2 ' EXEC  sp_executesql  @Query  DROP TABLE #temp");
                DTSearchPRI = Lo.RetriveCodeWithContidion("IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp SELECT DISTINCT TOP 100 PERCENT datepart(day,statusupdatedate) as [Day] into #temp FROM tbl_trn_RawData where StatusUpdateDate is not null  and DATENAME(MONTH,StatusUpdateDate)='" + ddlmonth.SelectedItem.Text + "' order by datepart(day,statusupdatedate) CREATE CLUSTERED INDEX cix_wekno ON #temp([Day] ASC)DECLARE @Pivot_Column [nvarchar](max); DECLARE @Query [nvarchar](max); SELECT @Pivot_Column= COALESCE(@Pivot_Column+',','')+ QUOTENAME([DAY])  FROM (select [day] from #temp)  Tab SELECT @Query='SELECT [FE Name], '+@Pivot_Column+' FROM(select [FE name],[Party Name] as [Total], datepart(day,statusupdatedate) as [Day] FROM tbl_trn_RawData where StatusUpdateDate is not null and ProductName=''PRI-Fixed Line'' and DATENAME(MONTH,StatusUpdateDate)=''" + ddlmonth.SelectedItem.Text + "'' group by [FE name],[party name],statusupdatedate) Tab1 PIVOT(count([Total]) FOR [Day] IN ('+@Pivot_Column+')) AS Tab2 ' EXEC  sp_executesql  @Query  DROP TABLE #temp");
                if (DTSearchMO.Rows.Count > 0 || DTSearch.Rows.Count > 0 || DTSearchPRI.Rows.Count > 0)
                {
                    ExportExcel(DTSearch, DTSearchMO, DTSearchPRI,"Automated FE Current Month Day Wise Work Report");
                }
            }
            else
            {
                DTSearch = Lo.RetriveCodeWithContidion("IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp SELECT DISTINCT TOP 100 PERCENT datepart(day,statusupdatedate) as [Day] into #temp FROM tbl_trn_RawData where StatusUpdateDate is not null  and DATENAME(MONTH,StatusUpdateDate)='" + DateTime.Now.ToString("MMMM") + "' order by datepart(day,statusupdatedate) CREATE CLUSTERED INDEX cix_wekno ON #temp([Day] ASC)DECLARE @Pivot_Column [nvarchar](max); DECLARE @Query [nvarchar](max); SELECT @Pivot_Column= COALESCE(@Pivot_Column+',','')+ QUOTENAME([DAY])  FROM (select [day] from #temp)  Tab SELECT @Query='SELECT [FE Name], '+@Pivot_Column+' FROM(select [FE name],[Party Name] as [Total], datepart(day,statusupdatedate) as [Day] FROM tbl_trn_RawData where StatusUpdateDate is not null and ProductName=''Leased Line'' and DATENAME(MONTH,StatusUpdateDate)=''" + DateTime.Now.ToString("MMMM") + "'' group by [FE name],[party name],statusupdatedate) Tab1 PIVOT(count([Total]) FOR [Day] IN ('+@Pivot_Column+')) AS Tab2 ' EXEC  sp_executesql  @Query  DROP TABLE #temp ");
                DTSearchMO = Lo.RetriveCodeWithContidion("IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp SELECT DISTINCT TOP 100 PERCENT datepart(day,statusupdatedate) as [Day] into #temp FROM tbl_trn_RawData where StatusUpdateDate is not null  and DATENAME(MONTH,StatusUpdateDate)='" + DateTime.Now.ToString("MMMM") + "' order by datepart(day,statusupdatedate)CREATE CLUSTERED INDEX cix_wekno ON #temp([Day] ASC)DECLARE @Pivot_Column [nvarchar](max); DECLARE @Query [nvarchar](max); SELECT @Pivot_Column= COALESCE(@Pivot_Column+',','')+ QUOTENAME([DAY])  FROM (select [day] from #temp)  Tab SELECT @Query='SELECT [FE Name], '+@Pivot_Column+' FROM(select [FE name],[Party Name] as [Total], datepart(day,statusupdatedate) as [Day] FROM tbl_trn_RawData where StatusUpdateDate is not null and ProductName=''MO'' and DATENAME(MONTH,StatusUpdateDate)=''" + DateTime.Now.ToString("MMMM") + "''  group by [FE name],[party name],statusupdatedate) Tab1 PIVOT(count([Total]) FOR [Day] IN ('+@Pivot_Column+')) AS Tab2 ' EXEC  sp_executesql  @Query  DROP TABLE #temp");
                DTSearchPRI = Lo.RetriveCodeWithContidion("IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp SELECT DISTINCT TOP 100 PERCENT datepart(day,statusupdatedate) as [Day] into #temp FROM tbl_trn_RawData where StatusUpdateDate is not null  and DATENAME(MONTH,StatusUpdateDate)='" + DateTime.Now.ToString("MMMM") + "' order by datepart(day,statusupdatedate) CREATE CLUSTERED INDEX cix_wekno ON #temp([Day] ASC)DECLARE @Pivot_Column [nvarchar](max); DECLARE @Query [nvarchar](max); SELECT @Pivot_Column= COALESCE(@Pivot_Column+',','')+ QUOTENAME([DAY])  FROM (select [day] from #temp)  Tab SELECT @Query='SELECT [FE Name], '+@Pivot_Column+' FROM(select [FE name],[Party Name] as [Total], datepart(day,statusupdatedate) as [Day] FROM tbl_trn_RawData where StatusUpdateDate is not null and ProductName=''PRI-Fixed Line'' and DATENAME(MONTH,StatusUpdateDate)=''" + DateTime.Now.ToString("MMMM") + "'' group by [FE name],[party name],statusupdatedate) Tab1 PIVOT(count([Total]) FOR [Day] IN ('+@Pivot_Column+')) AS Tab2 ' EXEC  sp_executesql  @Query  DROP TABLE #temp");
                if (DTSearchMO.Rows.Count > 0 || DTSearch.Rows.Count > 0 || DTSearchPRI.Rows.Count > 0)
                {
                    ExportExcel(DTSearch, DTSearchMO, DTSearchPRI, "Automated FE Current Month Day Wise Work Report");
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void ExportExcel(DataTable DtNotEligible, DataTable DtNotEligibleMO, DataTable dtPRI,string FileName)
    {
        byte[] bytes;
        byte[] Wali;
        byte[] PRI;
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(DtNotEligible);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                bytes = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        using (XLWorkbook wb1 = new XLWorkbook())
        {
            wb1.Worksheets.Add(DtNotEligibleMO);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb1.SaveAs(memoryStream);
                Wali = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        using (XLWorkbook wbpri = new XLWorkbook())
        {
            wbpri.Worksheets.Add(dtPRI);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbpri.SaveAs(memoryStream);
                PRI = memoryStream.ToArray();
                memoryStream.Close();
            }
        }
        SendMailExcelReport(bytes, Wali, PRI, DtNotEligible, DtNotEligibleMO, dtPRI);

    }
    protected void SendMailExcelReport(byte[] bytes, byte[] Wali, byte[] PRI, DataTable DtNotEligible, DataTable DtNotEligibleMO, DataTable dtPRI)
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/EmailForReport.htm")))
        {
            body = reader.ReadToEnd();
        }
        SendMail s;
        s = new SendMail();
        s.CreateInvoiceMailForByte1("verification@rcpl.in", "Shyam.Prasad@airtel.com", "Automated FE Current Month Day Wise Work Report", body, "", bytes, Wali, PRI, "verification@rcpl.in,sales@rcpl.in,bs@rcpl.in", "gagan@rcpl.in");
        //  s.CreateInvoiceMailForByte1("verification@rcpl.in", "mohdwali@globalitpoint.com", "FE Daily Work Report", body, "", bytes, Wali, "verification@rcpl.in","");
        string exMsg = "";
        bool result = s.sendMail(ConfigurationManager.AppSettings["MailServer"].ToString(), 25, ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), out exMsg);
        if (result == true && exMsg == "")
        {
            diverror.Visible = true;
            diverror.InnerHtml = "Report send to purushottam successfully.";
            diverror.Attributes.Add("class", "alert alert-success");
        }
        else
        {
            diverror.Visible = true;
            diverror.InnerHtml = exMsg;
            diverror.Attributes.Add("class", "alert alert-danger");
        }
    }
}