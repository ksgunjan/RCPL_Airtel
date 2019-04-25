using System;
using System.Collections.Specialized;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
    public class Logic
    {
        DataSet ds = new DataSet();
        string _Sys = string.Empty;
        string _msg = string.Empty;
        DataTable dt = new DataTable();
        public DataSet RCPL()
        {
            return ds;
        }
        public bool verifyconnect()
        {
            return SqlHelper.Instance.VerifyDataBase();
        }
        #region Login"
        public Int32 VerifyEmployee(HybridDictionary _hyLogin, out DateTime lastLogin)
        {
            return SqlHelper.Instance.VerifyEmployee(_hyLogin, out lastLogin);
        }
        #endregion
        #region SaveCode"
        public string SaveEmp(HybridDictionary hyadvertise, out string _msg, out string _sysMsg)
        {
            return SqlHelper.Instance.SaveEmp(hyadvertise, out _msg, out _sysMsg);
        }
        public string SaveComp(HybridDictionary hyadvertise, out string _msg, out string _sysMsg)
        {
            return SqlHelper.Instance.SaveComp(hyadvertise, out _msg, out _sysMsg);
        }
        public string SaveUserCategory(HybridDictionary hyadvertise, out string _msg, out string _sysMsg)
        {
            return SqlHelper.Instance.SaveUserCategory(hyadvertise, out _msg, out _sysMsg);
        }
        public string SaveEmail(HybridDictionary hyadvertise, out string _msg, out string _sysMsg)
        {
            return SqlHelper.Instance.SaveEmail(hyadvertise, out _msg, out _sysMsg);
        }
        public string SaveProd(HybridDictionary hyadvertise, out string _msg, out string _sysMsg)
        {
            return SqlHelper.Instance.SaveProd(hyadvertise, out _msg, out _sysMsg);
        }
        public string SaveProdType(HybridDictionary hyadvertise, out string _msg, out string _sysMsg)
        {
            return SqlHelper.Instance.SaveProdType(hyadvertise, out _msg, out _sysMsg);
        }
        public string SaveZone(HybridDictionary hyadvertise, out string _msg, out string _sysMsg)
        {
            return SqlHelper.Instance.SaveZone(hyadvertise, out _msg, out _sysMsg);
        }
        public string SaveZoneArea(HybridDictionary hyadvertise, out string _msg, out string _sysMsg)
        {
            return SqlHelper.Instance.SaveZoneArea(hyadvertise, out _msg, out _sysMsg);
        }
        public string SaveZoneAreaMapping(HybridDictionary hyadvertise, out string _msg, out string _sysMsg)
        {
            return SqlHelper.Instance.SaveZoneAreaMapping(hyadvertise, out _msg, out _sysMsg);
        }
        public DataSet CreateExcelConnection(string FilePath, string SheetName, out string text)
        {
            DataSet ds = SqlHelper.Instance.CreateExcelConnection(FilePath, SheetName, out text);
            return ds;
        }
        public string SaveUploadExcelCompany(DataTable DtExcel, string Comp, string product, string producttype)
        {
            return SqlHelper.Instance.SaveUploadExcelCompany(DtExcel, Comp, product, producttype);
        }
        #endregion
        #region RetriveCode
        public DataTable RetriveCodeUserCategory()
        {
            string query = "select UCategoryID,UserCategory,IsActive,case when IsActive='Y' then 'm-lg-r fa fa-times'  when IsActive = 'N' then 'fa fa-check' end as button,case when IsActive='Y' then 'Active'  when IsActive = 'N' then 'Deactive' end as Text from tbl_mst_UserCategory";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeUserCategory(Int64 Id)
        {
            string query = "select UCategoryID,UserCategory,IsActive,case when IsActive='Y' then 'm-lg-r fa fa-times'  when IsActive = 'N' then 'fa fa-check' end as button,case when IsActive='Y' then 'Active'  when IsActive = 'N' then 'Deactive' end as Text from tbl_mst_UserCategory where UCategoryID='" + Id + "'";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeUserEmail(string email)
        {
            string query = "select Email from tbl_mst_Employee where Email='" + email + "'";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeUser(string UserName)
        {
            string query = "SELECT tbl_mst_Employee.EmpID, tbl_mst_Employee.Category, tbl_mst_Employee.Name,tbl_mst_Employee.FEZoneAreaLocation, tbl_mst_Employee.ContactNo, tbl_mst_Employee.Address,tbl_mst_Employee.GovtIDNo, tbl_mst_Employee.ProfilePic, tbl_mst_Employee.IsActive,case when tbl_mst_Employee.IsActive='Y' then 'm-lg-r fa fa-times'  when tbl_mst_Employee.IsActive = 'N' then 'fa fa-check' end as button,case when tbl_mst_Employee.IsActive='Y' then 'Active'  when tbl_mst_Employee.IsActive = 'N' then 'Deactive' end as Text, tbl_trn_Login.UserName, tbl_trn_Login.Password, tbl_trn_Login.LastLoginTime, tbl_trn_Login.LastLogOutTime, tbl_trn_Login.LoginID FROM tbl_mst_Employee LEFT OUTER JOIN tbl_trn_Login ON tbl_mst_Employee.EmpID = tbl_trn_Login.EmpID where tbl_trn_Login.UserName='" + UserName + "'";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeUser(Int64 Id)
        {
            string query = "SELECT tbl_mst_Employee.EmpID, tbl_mst_Employee.Category, tbl_mst_Employee.Name,tbl_mst_Employee.FEZoneAreaLocation, tbl_mst_Employee.ContactNo, tbl_mst_Employee.Address,tbl_mst_Employee.GovtIDNo, tbl_mst_Employee.ProfilePic, tbl_mst_Employee.IsActive,case when tbl_mst_Employee.IsActive='Y' then 'm-lg-r fa fa-times'  when tbl_mst_Employee.IsActive = 'N' then 'fa fa-check' end as button,case when tbl_mst_Employee.IsActive='Y' then 'Active'  when tbl_mst_Employee.IsActive = 'N' then 'Deactive' end as Text, tbl_trn_Login.UserName, tbl_trn_Login.Password, tbl_trn_Login.LastLoginTime, tbl_trn_Login.LastLogOutTime, tbl_trn_Login.LoginID FROM tbl_mst_Employee LEFT OUTER JOIN tbl_trn_Login ON tbl_mst_Employee.EmpID = tbl_trn_Login.EmpID where tbl_trn_Login.EmpID='" + Id + "'";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeUser()
        {
            string query = "SELECT tbl_mst_Employee.EmpID, tbl_mst_Employee.Category,tbl_mst_Employee.FEZoneAreaLocation, tbl_mst_Employee.Name, tbl_mst_Employee.ContactNo, tbl_mst_Employee.Address,tbl_mst_Employee.GovtIDNo, tbl_mst_Employee.ProfilePic, tbl_mst_Employee.IsActive,case when tbl_mst_Employee.IsActive='Y' then 'm-lg-r fa fa-times'  when tbl_mst_Employee.IsActive = 'N' then 'fa fa-check' end as button,case when tbl_mst_Employee.IsActive='Y' then 'Active'  when tbl_mst_Employee.IsActive = 'N' then 'Deactive' end as Text, tbl_trn_Login.UserName, tbl_trn_Login.Password, tbl_trn_Login.LastLoginTime, tbl_trn_Login.LastLogOutTime, tbl_trn_Login.LoginID FROM tbl_mst_Employee LEFT OUTER JOIN tbl_trn_Login ON tbl_mst_Employee.EmpID = tbl_trn_Login.EmpID";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeCompany()
        {
            string query = "select CompId,CompName,CompGSTNo,CompPanNo,IsActive,case when IsActive='Y' then 'm-lg-r fa fa-times'  when IsActive = 'N' then 'fa fa-check' end as button,case when IsActive='Y' then 'Active'  when IsActive = 'N' then 'Deactive' end as Text from tbl_mst_Company";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeCompany(Int64 Id)
        {
            string query = "select CompId,CompName,CompGSTNo,CompPanNo,IsActive,case when IsActive='Y' then 'm-lg-r fa fa-times'  when IsActive = 'N' then 'fa fa-check' end as button,case when IsActive='Y' then 'Active'  when IsActive = 'N' then 'Deactive' end as Text from tbl_mst_Company where CompID='" + Id + "'";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeEmail()
        {
            string query = "select EmailId,Designation,Email,IsActive,case when IsActive='Y' then 'm-lg-r fa fa-times'  when IsActive = 'N' then 'fa fa-check' end as button,case when IsActive='Y' then 'Active'  when IsActive = 'N' then 'Deactive' end as Text from tbl_mst_Email";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeEmail(Int64 Id)
        {
            string query = "select EmailId,Designation,Email,IsActive,case when IsActive='Y' then 'm-lg-r fa fa-times'  when IsActive = 'N' then 'fa fa-check' end as button,case when IsActive='Y' then 'Active'  when IsActive = 'N' then 'Deactive' end as Text from tbl_mst_Email where EmailId='" + Id + "'";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeProduct()
        {
            string query = "select ProductId,ProductName,CompId,CompName,IsActive,case when IsActive='Y' then 'm-lg-r fa fa-times'  when IsActive = 'N' then 'fa fa-check' end as button,case when IsActive='Y' then 'Active'  when IsActive = 'N' then 'Deactive' end as Text from tbl_mst_Product";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeProduct(Int64 Id)
        {
            string query = "select ProductId,ProductName,CompId,CompName,IsActive,case when IsActive='Y' then 'm-lg-r fa fa-times'  when IsActive = 'N' then 'fa fa-check' end as button,case when IsActive='Y' then 'Active'  when IsActive = 'N' then 'Deactive' end as Text from tbl_mst_Product where ProductId='" + Id + "'";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeProductType()
        {
            string query = "select ProductTypeID,ProductTypeName,IsActive,case when IsActive='Y' then 'm-lg-r fa fa-times'  when IsActive = 'N' then 'fa fa-check' end as button,case when IsActive='Y' then 'Active'  when IsActive = 'N' then 'Deactive' end as Text from tbl_mst_ProductType";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeProductType(Int64 Id)
        {
            string query = "select ProductTypeID,ProductTypeName,IsActive,case when IsActive='Y' then 'm-lg-r fa fa-times'  when IsActive = 'N' then 'fa fa-check' end as button,case when IsActive='Y' then 'Active'  when IsActive = 'N' then 'Deactive' end as Text from tbl_mst_ProductType where ProductTypeID='" + Id + "'";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeZone()
        {
            string query = "select ZoneID,ZoneName,IsActive,case when IsActive='Y' then 'm-lg-r fa fa-times'  when IsActive = 'N' then 'fa fa-check' end as button,case when IsActive='Y' then 'Active'  when IsActive = 'N' then 'Deactive' end as Text from tbl_mst_Zone";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeZone(Int64 Id)
        {
            string query = "select ZoneID,ZoneName,IsActive,case when IsActive='Y' then 'm-lg-r fa fa-times'  when IsActive = 'N' then 'fa fa-check' end as button,case when IsActive='Y' then 'Active'  when IsActive = 'N' then 'Deactive' end as Text from tbl_mst_Zone where ZoneID='" + Id + "'";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeZoneArea()
        {
            string query = "select ZoneAreaID,ZoneArea,ZoneAreaPinCode,IsActive,case when IsActive='Y' then 'm-lg-r fa fa-times'  when IsActive = 'N' then 'fa fa-check' end as button,case when IsActive='Y' then 'Active'  when IsActive = 'N' then 'Deactive' end as Text from tbl_mst_ZoneArea";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeZoneArea(Int64 Id)
        {
            string query = "select ZoneAreaID,ZoneArea,ZoneAreaPinCode,IsActive,case when IsActive='Y' then 'm-lg-r fa fa-times'  when IsActive = 'N' then 'fa fa-check' end as button,case when IsActive='Y' then 'Active'  when IsActive = 'N' then 'Deactive' end as Text from tbl_mst_ZoneArea where ZoneAreaID='" + Id + "'";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeZoneAreaMapping()
        {
            string query = "SELECT tbl_mst_ZoneArea.ZoneArea,tbl_trn_ZoneMapping.ZoneAreaCode, tbl_trn_ZoneMapping.ZoneMappingID, tbl_trn_ZoneMapping.ZoneName, tbl_trn_ZoneMapping.ZoneAreaID FROM  tbl_mst_ZoneArea RIGHT OUTER JOIN tbl_trn_ZoneMapping ON tbl_mst_ZoneArea.ZoneAreaID = tbl_trn_ZoneMapping.ZoneAreaID";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeZoneAreaMapping(Int64 Id)
        {
            string query = "SELECT tbl_mst_ZoneArea.ZoneArea,tbl_trn_ZoneMapping.ZoneAreaCode, tbl_trn_ZoneMapping.ZoneMappingID, tbl_trn_ZoneMapping.ZoneName, tbl_trn_ZoneMapping.ZoneAreaID FROM  tbl_mst_ZoneArea RIGHT OUTER JOIN tbl_trn_ZoneMapping ON tbl_mst_ZoneArea.ZoneAreaID = tbl_trn_ZoneMapping.ZoneAreaID where tbl_trn_ZoneMapping.ZoneMappingID='" + Id + "'";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeAllExcelRecord()
        {
            string query = "select AppointmentMailDatetime,CustSamKamMailDatetime,NegativeCustSamKamMailDatetime,PersonMetName,PersonMetEmail,PersonMetDesignation,PersonMetMobileNo,UniqueID,TotalActiveNum,TotalDeactiveNum,TotalNewNum,[Allocated Date],[Account Caf No],IsCall,ExcelVerifiy,[Revisit Time],[Account Manager],[Account Number],[Alt Phone1],[Alt Phone2],Annotation,B2B_Email,B2B_Head_Email,B2B_Head_Name,[Bill Company],[Bill Plan],[Bill UOM],Bill_City,[Billed Act Id],[Billed Ext Id],[Billing Address],[Billing Trig Date],BILLING_BANDWIDTH,BILLING_BANDWIDTH_UOM,Billing_Contact_Number,Billing_Email_Id,ComplainceDate,[Charge Name],[Circuit BandWidth],[Commissioning Date],[Company Name],[Contact Phone1],[Contact Phone2],Coordinator_Contact_Email,Coordinator_Contact_Number,Coordinator_Name,[Cust account No],[Cust Email],[Customer Name],[Customer Segment],[Eligible/Not Eligible],[External id type],[From Site],[FE Name],FEComplianceDate,FX_ACCOUNT_EXTERNAL_ID,[Installation Address],InsertedDate,IsOTPVerified,IsScanned,IsSubmitted,KAM_Contact_Number,KAM_Email,KAM_Name,[Line Item Description],Line_Name,LOB,[LOC Created Date By PM],[LOC Status],[LOC Submiited Date By PM],Location,[Location Secondary],[Logical Circuit Id],[Mkt Code],MappingID,[Mobile OTP],[Mobile No],New_Connection_Type,[Num channel],[Order Type],[Orig Service Start Date],[Overall DD Completion Date],PreMeetingDate,PreMeetingTime,[Party Name],[Phone Nos],Pincode,[PO Number],[POP 1],[POP 2],[Primary Address],[Project Manager],[Product Name],PRODUCT_TYPE,ProductName,ProdTypeName,[Positive/Negative],RM_Contact_Number,RM_Email,RM_NAME,RecievedDate,ReasonOfStatus,RemarksOfStatus,[Revisit PerSon Name],[Revisit Date Time],StatusByFE,StatusTime,StatusUpdateDate,ScannedDate,SubmittedDate,SendNegativeMailtoAll,SentMailAirtel,SentSamMail,SentCustMail,SAM_Contact_Number,SAM_Email,SAM_Name,SAM_TL,SAM_TL_Contact_Number,SAM_TL_EMAIL,[Secondary Address],Segment,[Service City],[Service Name],[Service Start],Status,[Sub Product Name],[Subs Del No],[TL Name],TL_Contact_Number,TL_Email,[To Site],[Unique Company],Unique_Installation_Address,Verification,[Verification Agent],[Verification Code],[Verification Date],[Verification Status],[Verification Type],Vertical,VH_Contact_Number,VH_Email,VH_Name,IsClosed,IsActive from tbl_trn_RawData";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeAllExcelRecordWhereCondition(string whereCondition)
        {
            string query = "SELECT AppointmentMailDatetime,CustSamKamMailDatetime,NegativeCustSamKamMailDatetime,PersonMetName,PersonMetEmail,PersonMetDesignation,PersonMetMobileNo,UniqueID,TotalActiveNum,TotalDeactiveNum,TotalNewNum,[Allocated Date],[Account Caf No],IsCall,[Revisit Time],ExcelVerifiy,[Account Manager],[Account Number],[Alt Phone1],[Alt Phone2],Annotation,B2B_Email,B2B_Head_Email,B2B_Head_Name,[Bill Company],[Bill Plan],[Bill UOM],Bill_City,[Billed Act Id],[Billed Ext Id],[Billing Address],[Billing Trig Date],BILLING_BANDWIDTH,BILLING_BANDWIDTH_UOM,Billing_Contact_Number,Billing_Email_Id,ComplainceDate,[Charge Name],[Circuit BandWidth],[Commissioning Date],[Company Name],[Contact Phone1],[Contact Phone2],Coordinator_Contact_Email,Coordinator_Contact_Number,Coordinator_Name,[Cust account No],[Cust Email],[Customer Name],[Customer Segment],[Eligible/Not Eligible],[External id type],[From Site],[FE Name],FEComplianceDate,FX_ACCOUNT_EXTERNAL_ID,[Installation Address],InsertedDate,IsOTPVerified,IsScanned,IsSubmitted,KAM_Contact_Number,KAM_Email,KAM_Name,[Line Item Description],Line_Name,LOB,[LOC Created Date By PM],[LOC Status],[LOC Submiited Date By PM],Location,[Location Secondary],[Logical Circuit Id],[Mkt Code],MappingID,[Mobile OTP],[Mobile No],New_Connection_Type,[Num channel],[Order Type],[Orig Service Start Date],[Overall DD Completion Date],PreMeetingDate,PreMeetingTime,[Party Name],[Phone Nos],Pincode,[PO Number],[POP 1],[POP 2],[Primary Address],[Project Manager],[Product Name],PRODUCT_TYPE,ProductName,ProdTypeName,[Positive/Negative],RM_Contact_Number,RM_Email,RM_NAME,RecievedDate,ReasonOfStatus,RemarksOfStatus,[Revisit PerSon Name],[Revisit Date Time],StatusByFE,StatusTime,StatusUpdateDate,ScannedDate,SubmittedDate,SendNegativeMailtoAll,SentMailAirtel,SentSamMail,SentCustMail,SAM_Contact_Number,SAM_Email,SAM_Name,SAM_TL,SAM_TL_Contact_Number,SAM_TL_EMAIL,[Secondary Address],Segment,[Service City],[Service Name],[Service Start],Status,[Sub Product Name],[Subs Del No],[TL Name],TL_Contact_Number,TL_Email,[To Site],[Unique Company],Unique_Installation_Address,Verification,[Verification Agent],[Verification Code],[Verification Date],[Verification Status],[Verification Type],Vertical,VH_Contact_Number,VH_Email,VH_Name,IsClosed,IsActive,KeyDup from tbl_trn_RawData where " + whereCondition + "";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeLeasedLineApproveUploadExcel(string whereCondition)
        {
            string query = "SELECT [Allocated Date],ComplainceDate,[Logical Circuit Id],[Customer Name],BILLING_BANDWIDTH,ProductName,ProdTypeName,[FE NAME],[Eligible/Not Eligible],UniqueID from tbl_trn_RawData where " + whereCondition + "";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveCodeWithContidion(string Condition)
        {
            string query = Condition;
            try
            {
                return SqlHelper.Instance.GetDataset(query).Tables[0];
            }
            catch (Exception ex)
            {
                return SqlHelper.Instance.GetDataset(query).Tables[""];
            }
        }
        public DataTable RetriveBindDDL(string column1, string column2, string tablename)
        {
            string query = "select " + column1 + "," + column2 + " from " + tablename + "";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        public DataTable RetriveBindDDL(Int32 whereid)
        {
            string query = "SELECT tbl_mst_Company.CompId,tbl_mst_Company.CompName, tbl_mst_Product.ProductId, tbl_mst_Product.ProductName, tbl_mst_Product.IsActive FROM tbl_mst_Company RIGHT OUTER JOIN tbl_mst_Product ON tbl_mst_Company.CompId = tbl_mst_Product.CompId where tbl_mst_Company.CompId ='" + whereid + "' and tbl_mst_Product.IsActive ='Y'";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
        }
        #endregion
        #region UpdateCode
        public Int32 UpdateLogOutTime(string Username)
        {
            string mquery = "Update tbl_trn_Login set LastLogOutTime = '" + DateTime.Now + "' where UserName = '" + Username + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 UpdateIsActiveCode(string tablename, string value, string ColumnName, Int64 id)
        {
            string mquery = "Update " + tablename + " set IsActive = '" + value + "' where " + ColumnName + " = '" + id + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 UpdateCodeOther(string tablename, string otherquery, string WhereCondition)
        {
            string mquery = "Update " + tablename + " " + otherquery + " where " + WhereCondition + "";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        #endregion
        #region InsertCode
        public int InsertExcelUpload(DataTable insertrec, string tablename, out string _msg, out string _sysMsg)
        {
            for (int i = 0; insertrec.Rows.Count > i; i++)
            {
                try
                {
                    string mquery = "Insert into " + tablename + "" + "(" + insertrec.Rows[i][0].ToString() + ")" + "Values" + "(" + insertrec.Rows[i][1].ToString() + ")";
                    SqlHelper.Instance.ExecuteQuery(mquery);
                }
                catch (Exception ex)
                {
                    _msg = ex.Message + "in row" + " -" + i++ + "-" + insertrec.Columns[i].ColumnName.ToString();
                    _sysMsg = ex.Message;
                    return 0;
                }
            }
            _msg = "row inserted";
            _sysMsg = "row inserted";
            return 1;
        }
        public Int32 InsertNormal(string condition)
        {
            string mquery = condition;
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        #endregion
        #region SearchCode"
        public DataTable AllSearchCode(DataTable dtsearchresult, string tablename)
        {
            string mquery = "select AppointmentMailDatetime,CustSamKamMailDatetime,NegativeCustSamKamMailDatetime,PersonMetName,PersonMetEmail,PersonMetDesignation,PersonMetMobileNo,UniqueID,TotalActiveNum,TotalDeactiveNum,TotalNewNum,[Allocated Date],[Account Caf No],IsCall,ExcelVerifiy,[Revisit Time],[Account Manager],[Account Number],[Alt Phone1],[Alt Phone2],Annotation,B2B_Email,B2B_Head_Email,B2B_Head_Name,[Bill Company],[Bill Plan],[Bill UOM],Bill_City,[Billed Act Id],[Billed Ext Id],[Billing Address],[Billing Trig Date],BILLING_BANDWIDTH,BILLING_BANDWIDTH_UOM,Billing_Contact_Number,Billing_Email_Id,ComplainceDate,[Charge Name],[Circuit BandWidth],[Commissioning Date],[Company Name],[Contact Phone1],[Contact Phone2],Coordinator_Contact_Email,Coordinator_Contact_Number,Coordinator_Name,[Cust account No],[Cust Email],[Customer Name],[Customer Segment],[Eligible/Not Eligible],[External id type],[From Site],[FE Name],FEComplianceDate,FX_ACCOUNT_EXTERNAL_ID,[Installation Address],InsertedDate,IsOTPVerified,IsScanned,IsSubmitted,KAM_Contact_Number,KAM_Email,KAM_Name,[Line Item Description],Line_Name,LOB,[LOC Created Date By PM],[LOC Status],[LOC Submiited Date By PM],Location,[Location Secondary],[Logical Circuit Id],[Mkt Code],MappingID,[Mobile OTP],[Mobile No],New_Connection_Type,[Num channel],[Order Type],[Orig Service Start Date],[Overall DD Completion Date],PreMeetingDate,PreMeetingTime,[Party Name],[Phone Nos],Pincode,[PO Number],[POP 1],[POP 2],[Primary Address],[Project Manager],[Product Name],PRODUCT_TYPE,ProductName,ProdTypeName,[Positive/Negative],RM_Contact_Number,RM_Email,RM_NAME,RecievedDate,ReasonOfStatus,RemarksOfStatus,[Revisit PerSon Name],[Revisit Date Time],StatusByFE,StatusTime,StatusUpdateDate,ScannedDate,SubmittedDate,SendNegativeMailtoAll,SentMailAirtel,SentSamMail,SentCustMail,SAM_Contact_Number,SAM_Email,SAM_Name,SAM_TL,SAM_TL_Contact_Number,SAM_TL_EMAIL,[Secondary Address],Segment,[Service City],[Service Name],[Service Start],Status,[Sub Product Name],[Subs Del No],[TL Name],TL_Contact_Number,TL_Email,[To Site],[Unique Company],Unique_Installation_Address,Verification,[Verification Agent],[Verification Code],[Verification Date],[Verification Status],[Verification Type],Vertical,VH_Contact_Number,VH_Email,VH_Name,IsClosed,IsActive  from " + tablename.Substring(0, 15) + " where '1' = '1'";
            for (int i = 0; i < dtsearchresult.Rows.Count; i++)
            {
                mquery = mquery + " and (" + dtsearchresult.Rows[i][0].ToString() + "" + dtsearchresult.Rows[i][1].ToString() + ")";
            }
            mquery = mquery + "Order by " + tablename.Substring(15) + " asc ";
            return SqlHelper.Instance.GetDataset(mquery).Tables[0];
        }
        public DataTable AllSearchCodeWithCoulmnName(DataTable dtsearchresult, string tablename, string columnname)
        {
            string mquery = "select " + columnname + "  from " + tablename + " where '1' = '1'";
            for (int i = 0; i < dtsearchresult.Rows.Count; i++)
            {
                mquery = mquery + " and (" + dtsearchresult.Rows[i][0].ToString() + "" + dtsearchresult.Rows[i][1].ToString() + ")";
            }
            mquery = mquery + "";
            return SqlHelper.Instance.GetDataset(mquery).Tables[0];
        }
        public DataTable AllSearchCodeExcel(DataTable dtsearchresult, string tablename)
        {
            string mquery = "select [Allocated Date],[Billed Ext Id],[Eligible/Not Eligible],[From Site],[FE Name],[Logical Circuit Id],[Party Name],ProductName,StatusByFE,StatusUpdateDate,[Subs Del No]  from " + tablename.Substring(0, 15) + " where '1' = '1'";
            for (int i = 0; i < dtsearchresult.Rows.Count; i++)
            {
                mquery = mquery + " and (" + dtsearchresult.Rows[i][0].ToString() + "" + dtsearchresult.Rows[i][1].ToString() + ")";
            }
            mquery = mquery + "Order by " + tablename.Substring(15) + " asc ";
            return SqlHelper.Instance.GetDataset(mquery).Tables[0];
        }
        public DataTable AllSearchCodeWithCoulmnNameMO(DataTable dtsearchresult, string tablename, string columnname)
        {
            string mquery = "select " + columnname + "  from " + tablename + " where '1' = '1'";
            for (int i = 0; i < dtsearchresult.Rows.Count; i++)
            {
                mquery = mquery + " and (" + dtsearchresult.Rows[i][0].ToString() + "" + dtsearchresult.Rows[i][1].ToString() + ")";
            }
            mquery = mquery + "Group by [Billed Ext ID],[Party Name],ProductName order by [Party Name] asc";
            return SqlHelper.Instance.GetDataset(mquery).Tables[0];
        }
        #endregion
        #region count code
        public DataTable RetriveAllCount(string tablename, string wherecondition)
        {
            string mquery = "select count([Party Name]) as RCount from " + tablename + " where " + wherecondition + "";
            return SqlHelper.Instance.GetDataset(mquery).Tables[0];
        }
        public DataTable RetriveAllCountFE(string tablename, string wherecondition)
        {
            string mquery = "select count([Name]) as RCount from " + tablename + " where " + wherecondition + "";
            return SqlHelper.Instance.GetDataset(mquery).Tables[0];
        }
        public DataTable RetriveAllCount(string tablename)
        {
            string mquery = "select IsActive,StatusByFE,ProductName,[TL Name],IsSubmitted,IsScanned,IsClosed,StatusUpdateDate,PreMeetingDate,KeyDup from " + tablename + "";
            return SqlHelper.Instance.GetDataset(mquery).Tables[0];
        }
        public DataTable RetriveAllCount1(string tablename, string where)
        {
            string mquery = "select IsActive,StatusByFE,ProductName,[TL Name],[FE Name],[Eligible/Not Eligible],IsSubmitted,IsScanned,IsClosed,StatusUpdateDate,PreMeetingDate from " + tablename + " where ProductName='" + where + "'";
            return SqlHelper.Instance.GetDataset(mquery).Tables[0];
        }
        #endregion
        #region DeleteCode
        public Int32 DeleteCode(string tablename, string wherecondition)
        {
            string mquery = "delete from " + tablename + " where " + wherecondition + "";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        #endregion
    }
}
