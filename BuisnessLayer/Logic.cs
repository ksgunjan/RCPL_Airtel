using DataAccessLayer;
using System;
using System.Collections.Specialized;
using System.Data;
namespace BuisnessLayer
{
    public class Logic
    {
        #region "Variables"
        private readonly DataTable dt = new DataTable();
        private readonly DataSet ds = new DataSet();
        #endregion
        public DataSet RCPLAIRTEL()
        {
            return ds;
        }
        #region Login"
        public string VerifyEmployee(HybridDictionary hyLogin, out string _msg, out string Defaultpage)
        {
            return SqlHelper.Instance.VerifyEmployee(hyLogin, out _msg, out Defaultpage);
        }
        #endregion
        #region Retrive Code Menu
        public DataTable RetriveMasterData(long Companyid, string strRefNo, string strRole, int MenuId, string strMenuUrl, string strInterestedAreaFlag, string strCriteria)
        {
            return SqlHelper.Instance.RetriveMasterData(Companyid, strRefNo, strRole, MenuId, strMenuUrl, strInterestedAreaFlag, strCriteria);
        }
        public DataTable RetriveCodeLocal(long Id, string value1, string value2, string value3, string strCriteria)
        {
            return SqlHelper.Instance.RetriveCodeLocal(Id, value1, value2, value3, strCriteria);
        }
        public string RetriveCodeLocalup(long Id, string value1, string value2, string value3, string strCriteria)
        {
            return SqlHelper.Instance.RetriveCodeLocalup(Id, value1, value2, value3, strCriteria);
        }
        public DataTable RetriveAdminFeCount(string FeName, string Product, string Month, string strCriteria)
        {
            return SqlHelper.Instance.RetriveAdminFeCount(FeName, Product, Month, strCriteria);
        }
        #endregion
        #region All Count Code
        public DataTable RetriveCountAdminDashboard(string value1, string value2, string value3)
        {
            return SqlHelper.Instance.GetExecuteData("select * from fn_CountOfAllRecords('" + value1 + "','" + value2 + "','" + value3 + "')");
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
        #region RetriveCode RawData
        public DataTable RetriveCodeRawDataPopup(long ExcelRawId, string ProductName, string ProductType, DateTime AllocatedDate, string Status, DateTime StatusUpdateDate, string Value1, string Value2, string mQuery, string Criteria)
        {
            return SqlHelper.Instance.RetriveCodeRawDataPopup(ExcelRawId, ProductName, ProductType, AllocatedDate, Status, StatusUpdateDate, Value1, Value2, mQuery, Criteria);
        }
        public DataTable RetriveCodeRawDataGridSimple(long ExcelRawId, string ProductName, string ProductType, DateTime AllocatedDate, string Status, DateTime StatusUpdateDate, string Value1, string Value2, string mQuery, string Criteria)
        {
            return SqlHelper.Instance.RetriveCodeRawDataPopup(ExcelRawId, ProductName, ProductType, AllocatedDate, Status, StatusUpdateDate, Value1, Value2, mQuery, Criteria);
        }
        public DataTable RetriveCodeRawDataGrid(DataTable dtsearchresult, long ExcelRawId, string ProductName, string ProductType, DateTime AllocatedDate, string Status, DateTime StatusUpdateDate, string Value1, string Value2, string mQuery, string Criteria)
        {
            string mRetriveDtQuery = "";
            if (ProductName == "MO")
            {
                mRetriveDtQuery = "select ExcelRawID,Account_Number,Allocated_Date,B2B_Email,B2B_Head_Email,B2B_Head_Name,Bill_Company,Bill_Plan,Bill_City,Billed_Act_Id,Billed_Ext_Id as UniqueID,Billing_Address" +
              ",Billing_Trig_Date,Billing_Contact_Number ,Complaince_Date,Company_Name,Coordinator_Contact_Email,Coordinator_Contact_Number,Coordinator_Name ,Cust_Email," +
              "Customer_Name,Code,Eligible_Not_Eligible ,From_Site,FE_Name,FE_Compliance_Date,Inserted_Date ,Is_OTP_Verified,Is_Scanned,Is_Submitted," +
              "Mobile_OTP,Mobile_No,Pre_Meeting_Date,Pre_Meeting_Time,Party_Name,Primary_Address,PRODUCT_TYPE,Product_Name,Prod_Type_Name,Positive_Negative,Person_Met_Name," +
              "Person_Met_Email,Person_Met_Designation,Person_Met_Mobile_No,RM_Contact_Number,RM_Email,RM_NAME,Reason_Of_Status,Remarks_Of_Status,Revisit_PerSon_Name,Revisit_Date_Time," +
              "Revisit_Time,Status_By_FE,Status_Time,Status_Update_Date,Scanned_Date,Submitted_Date,Send_Negative_Mail_to_All,Sent_Mail_Airtel,Sent_Sam_Mail,Sent_Cust_Mail,Appointment_Mail_Datetime," +
              "Cust_Sam_Kam_Mail_Datetime,Negative_Cust_Sam_Kam_Mail_Datetime ,Segment,Subs_Del_No,TL_Name,TL_Contact_Number,TL_Email ,Total_Active_Num,Total_Deactive_Num,Total_New_Num,VH_Contact_Number,VH_Email," +
              "VH_Name,Is_Closed,Is_Active,Is_Call,Excel_Verifiy,Key_Dup from tbl_mst_MOData where 1=1 and Is_Active='Y'";
            }
            else
            {
                mRetriveDtQuery = "select ExcelRawID,Account_Number,Allocated_Date,Account_Manager,Account_Number,B2B_Email,B2B_Head_Email,B2B_Head_Name,Bill_Company,Bill_City,Billed_Act_Id,Billed_Ext_Id,Billing_Address" +
                  ",Billing_Trig_Date,BILLING_BANDWIDTH,Billing_Contact_Number,Complaince_Date,Company_Name,Coordinator_Contact_Email,Coordinator_Contact_Number,Coordinator_Name" +
              ",Coordinator_Contact_Email,Coordinator_Contact_Number,Coordinator_Name,Cust_account_No,Cust_Email,Customer_Name,Customer_Segment,Code,Eligible_Not_Eligible" +
              ",From_Site,FE_Name,FE_Compliance_Date,Inserted_Date,Is_OTP_Verified,Is_Scanned,Is_Submitted,KAM_Contact_Number,KAM_Email,KAM_Name,Logical_Circuit_Id as UniqueID,Mobile_OTP,Pre_Meeting_Date" +
              ",Pre_Meeting_Time,Party_Name,Primary_Address,PRODUCT_TYPE,Product_Name,Prod_Type_Name,Positive_Negative,Person_Met_Name,Person_Met_Email,Person_Met_Designation," +
              "Person_Met_Mobile_No,RM_Contact_Number,RM_Email,RM_NAME,Reason_Of_Status,Remarks_Of_Status,Revisit_PerSon_Name,Revisit_Date_Time,Revisit_Time,Status_By_FE,Status_Time," +
              "Status_Update_Date,Scanned_Date,Submitted_Date,Send_Negative_Mail_to_All,Sent_Mail_Airtel,Sent_Sam_Mail,Sent_Cust_Mail,Appointment_Mail_Datetime,Cust_Sam_Kam_Mail_Datetime" +
              ",Negative_Cust_Sam_Kam_Mail_Datetime,SAM_Contact_Number ,SAM_Email,SAM_Name,SAM_TL,SAM_TL_Contact_Number,SAM_TL_EMAIL,Segment,Subs_Del_No,TL_Name,TL_Contact_Number,TL_Email" +
              ",VH_Contact_Number,VH_Email,VH_Name,Is_Closed ,Is_Active,Is_Call,Excel_Verifiy,Key_Dup from tbl_mst_LeasedLinePRIData where 1=1 and  Is_Active='Y' ";
            }
            for (int i = 0; i < dtsearchresult.Rows.Count; i++)
            {
                mRetriveDtQuery = mRetriveDtQuery + " and (" + dtsearchresult.Rows[i][0].ToString() + "" + dtsearchresult.Rows[i][1].ToString() + ")";
            }
            mQuery = mRetriveDtQuery.ToString();
            return SqlHelper.Instance.GetDataset(mQuery).Tables[0];
            //  return SqlHelper.Instance.RetriveCodeRawDataPopup(ExcelRawId, ProductName, ProductType, AllocatedDate, Status, StatusUpdateDate, Value1, Value2, mQuery, Criteria);
        }
        public DataTable RetriveCodeRawDataGrid1(DataTable dtsearchresult, long ExcelRawId, string ProductName, string ProductType, DateTime AllocatedDate, string Status, DateTime StatusUpdateDate, string Value1, string Value2, string mQuery, string Criteria)
        {
            string mRetriveDtQuery1 = "";
            for (int i = 0; i < dtsearchresult.Rows.Count; i++)
            {
                mRetriveDtQuery1 = mRetriveDtQuery1 + " and (" + dtsearchresult.Rows[i][0].ToString() + "" + dtsearchresult.Rows[i][1].ToString() + ")";
            }
            mQuery = mRetriveDtQuery1.ToString();
            return SqlHelper.Instance.RetriveCodeRawDataPopup(ExcelRawId, ProductName, ProductType, AllocatedDate, Status, StatusUpdateDate, Value1, Value2, "'N' and Is_CLosed!='Y'" + mQuery, Criteria);
        }
        public DataTable RetriveDataWithOnlyQuery(DataTable dtsearchresult, string smQuery, string Criteria)
        {
            string mRetriveDtQuery2 = "";
            if (Criteria == "BindGrid")
            {
                mRetriveDtQuery2 = "select ExcelRawID,Allocated_Date,Account_Manager,Account_Number,B2B_Email,B2B_Head_Email,B2B_Head_Name,Bill_Company,Bill_City,Billed_Act_Id,Billed_Ext_Id,Billing_Address" +
                   ",Billing_Trig_Date,BILLING_BANDWIDTH,Billing_Contact_Number,Complaince_Date,Company_Name,Coordinator_Contact_Email,Coordinator_Contact_Number,Coordinator_Name" +
               ",Coordinator_Contact_Email,Coordinator_Contact_Number,Coordinator_Name,Cust_account_No,Cust_Email,Customer_Name,Customer_Segment,Code,Eligible_Not_Eligible" +
               ",From_Site,FE_Name,FE_Compliance_Date,Inserted_Date,Is_OTP_Verified,Is_Scanned,Is_Submitted,KAM_Contact_Number,KAM_Email,KAM_Name,Logical_Circuit_Id as UniqueID,Mobile_OTP,Pre_Meeting_Date" +
               ",Pre_Meeting_Time,Party_Name,Primary_Address,PRODUCT_TYPE,Product_Name,Prod_Type_Name,Positive_Negative,Person_Met_Name,Person_Met_Email,Person_Met_Designation," +
               "Person_Met_Mobile_No,RM_Contact_Number,RM_Email,RM_NAME,Reason_Of_Status,Remarks_Of_Status,Revisit_PerSon_Name,Revisit_Date_Time,Revisit_Time,Status_By_FE,Status_Time," +
               "Status_Update_Date,Scanned_Date,Submitted_Date,Send_Negative_Mail_to_All,Sent_Mail_Airtel,Sent_Sam_Mail,Sent_Cust_Mail,Appointment_Mail_Datetime,Cust_Sam_Kam_Mail_Datetime" +
               ",Negative_Cust_Sam_Kam_Mail_Datetime,SAM_Contact_Number ,SAM_Email,SAM_Name,SAM_TL,SAM_TL_Contact_Number,SAM_TL_EMAIL,Segment,Subs_Del_No,TL_Name,TL_Contact_Number,TL_Email" +
               ",VH_Contact_Number,VH_Email,VH_Name,Is_Closed ,Is_Active,Is_Call,Excel_Verifiy,Key_Dup from tbl_mst_LeasedLinePRIData where 1=1 and Is_Active = 'N' and Is_Closed='N'";
            }
            else
            {
                mRetriveDtQuery2 = "select ExcelRawID, Allocated_Date, B2B_Email, B2B_Head_Email, B2B_Head_Name, Bill_Company, Bill_Plan, Bill_City, Billed_Act_Id, Billed_Ext_Id as UniqueID,Billing_Address" +
            ",Billing_Trig_Date,Billing_Contact_Number ,Complaince_Date,Company_Name,Coordinator_Contact_Email,Coordinator_Contact_Number,Coordinator_Name ,Cust_Email," +
            "Customer_Name,Code,Eligible_Not_Eligible ,From_Site,FE_Name,FE_Compliance_Date,Inserted_Date ,Is_OTP_Verified,Is_Scanned,Is_Submitted," +
            "Mobile_OTP,Mobile_No,Pre_Meeting_Date,Pre_Meeting_Time,Party_Name,Primary_Address,PRODUCT_TYPE,Product_Name,Prod_Type_Name,Positive_Negative,Person_Met_Name," +
            "Person_Met_Email,Person_Met_Designation,Person_Met_Mobile_No,RM_Contact_Number,RM_Email,RM_NAME,Reason_Of_Status,Remarks_Of_Status,Revisit_PerSon_Name,Revisit_Date_Time," +
            "Revisit_Time,Status_By_FE,Status_Time,Status_Update_Date,Scanned_Date,Submitted_Date,Send_Negative_Mail_to_All,Sent_Mail_Airtel,Sent_Sam_Mail,Sent_Cust_Mail,Appointment_Mail_Datetime," +
            "Cust_Sam_Kam_Mail_Datetime,Negative_Cust_Sam_Kam_Mail_Datetime ,Segment,Subs_Del_No,TL_Name,TL_Contact_Number,TL_Email ,Total_Active_Num,Total_Deactive_Num,Total_New_Num,VH_Contact_Number,VH_Email," +
            "VH_Name,Is_Closed,Is_Active,Is_Call,Excel_Verifiy,Key_Dup from tbl_mst_MOData where 1=1 and Is_Active = 'N' and Is_Closed='N'";
            }
            for (int i = 0; i < dtsearchresult.Rows.Count; i++)
            {
                mRetriveDtQuery2 = mRetriveDtQuery2 + " and (" + dtsearchresult.Rows[i][0].ToString() + "" + dtsearchresult.Rows[i][1].ToString() + ")";
            }
            smQuery = mRetriveDtQuery2.ToString();
            return SqlHelper.Instance.GetDataset(smQuery).Tables[0];
            //  return SqlHelper.Instance.RetriveDataWithOnlyQuery("'N' and Is_CLosed!='Y' " + smQuery, Criteria);
        }
        public DataTable RetriveCodeRescheduled_APPOINTMENT(string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Criteria)
        {
            return SqlHelper.Instance.RetriveCodeRescheduled_APPOINTMENT(Value1, Value2, Value3, Value4, Value5, Value6, Criteria);
        }
        public DataTable AllSearchCode(DataTable dtsearchresult, string Value1, string Value2)
        {
            string lmquery = "";
            if (Value1 == "Leased Line")
            {
                lmquery = " select ExcelRawID, Allocated_Date, Account_Manager, Account_Number, B2B_Email, B2B_Head_Email, B2B_Head_Name, Bill_Company, Bill_City, Billed_Act_Id, Billing_Address" +
               " , Billing_Trig_Date, BILLING_BANDWIDTH, Billing_Contact_Number, Complaince_Date, Company_Name, Coordinator_Contact_Email, Coordinator_Contact_Number, Coordinator_Name" +
            ", Coordinator_Contact_Email, Coordinator_Contact_Number, Coordinator_Name, Cust_account_No, Cust_Email, Customer_Name, Customer_Segment, Code, Eligible_Not_Eligible" +
            ", From_Site, FE_Name, FE_Compliance_Date, Inserted_Date, Is_OTP_Verified, Is_Scanned, Is_Submitted, KAM_Contact_Number, KAM_Email, KAM_Name, Logical_Circuit_Id as UniqueEID,Mobile_OTP,Pre_Meeting_Date" +
            ",Pre_Meeting_Time,Party_Name,Primary_Address,PRODUCT_TYPE,Product_Name,Prod_Type_Name,Positive_Negative,Person_Met_Name,Person_Met_Email,Person_Met_Designation," +
            "Person_Met_Mobile_No,RM_Contact_Number,RM_Email,RM_NAME,Reason_Of_Status,Remarks_Of_Status,Revisit_PerSon_Name,Revisit_Date_Time,Revisit_Time,Status_By_FE,Status_Time," +
            "Status_Update_Date,Scanned_Date,Submitted_Date,Send_Negative_Mail_to_All,Sent_Mail_Airtel,Sent_Sam_Mail,Sent_Cust_Mail,Appointment_Mail_Datetime,Cust_Sam_Kam_Mail_Datetime" +
            ",Negative_Cust_Sam_Kam_Mail_Datetime,SAM_Contact_Number ,SAM_Email,SAM_Name,SAM_TL,SAM_TL_Contact_Number,SAM_TL_EMAIL,Segment,Subs_Del_No,TL_Name,TL_Contact_Number,TL_Email" +
            ",VH_Contact_Number,VH_Email,VH_Name,Is_Closed ,Is_Active,Is_Call,Excel_Verifiy,Key_Dup from tbl_mst_LeasedLinePRIData where 1=1 ";
            }
            else
            {
                lmquery = "select ExcelRawID,Allocated_Date,Account_Manager,Account_Number,B2B_Email,B2B_Head_Email,B2B_Head_Name,Bill_Company,Bill_City,Billed_Act_Id,Billing_Address" +
                ",Billing_Trig_Date,BILLING_BANDWIDTH,Billing_Contact_Number,Complaince_Date,Company_Name,Coordinator_Contact_Email,Coordinator_Contact_Number,Coordinator_Name" +
            ",Coordinator_Contact_Email,Coordinator_Contact_Number,Coordinator_Name,Cust_account_No,Cust_Email,Customer_Name,Customer_Segment,Code,Eligible_Not_Eligible" +
            ",From_Site,FE_Name,FE_Compliance_Date,Inserted_Date,Is_OTP_Verified,Is_Scanned,Is_Submitted,KAM_Contact_Number,KAM_Email,KAM_Name,Logical_Circuit_Id,Mobile_OTP,Pre_Meeting_Date" +
            ",Pre_Meeting_Time,Party_Name,Primary_Address,PRODUCT_TYPE,Product_Name,Prod_Type_Name,Positive_Negative,Person_Met_Name,Person_Met_Email,Person_Met_Designation," +
            "Person_Met_Mobile_No,RM_Contact_Number,RM_Email,RM_NAME,Reason_Of_Status,Remarks_Of_Status,Revisit_PerSon_Name,Revisit_Date_Time,Revisit_Time,Status_By_FE,Status_Time," +
            "Status_Update_Date,Scanned_Date,Submitted_Date,Send_Negative_Mail_to_All,Sent_Mail_Airtel,Sent_Sam_Mail,Sent_Cust_Mail,Appointment_Mail_Datetime,Cust_Sam_Kam_Mail_Datetime" +
            ",Negative_Cust_Sam_Kam_Mail_Datetime,SAM_Contact_Number ,SAM_Email,SAM_Name,SAM_TL,SAM_TL_Contact_Number,SAM_TL_EMAIL,Segment,Subs_Del_No as UniqueEID,TL_Name,TL_Contact_Number,TL_Email" +
            ",VH_Contact_Number,VH_Email,VH_Name,Is_Closed ,Is_Active,Is_Call,Excel_Verifiy,Key_Dup from tbl_mst_LeasedLinePRIData where 1=1 ";
            }
            for (int i = 0; i < dtsearchresult.Rows.Count; i++)
            {
                lmquery = lmquery + " and (" + dtsearchresult.Rows[i][0].ToString() + "" + dtsearchresult.Rows[i][1].ToString() + ")";
            }
            Value2 = lmquery.ToString();
            return SqlHelper.Instance.GetDataset(Value2).Tables[0];
            // return SqlHelper.Instance.RetriveCodeRescheduled_APPOINTMENT('Y' + Value1, Value2, "", "", "", "", "SearchCode");
        }
        public DataTable MasterJobHistoryFE(DataTable dtsearchresult, string Value1, string Value2, string Value3)
        {
            string mquery = "";
            if (Value1 == "Leased Line")
            {
                mquery = "select ExcelRawID,Allocated_Date,Party_Name,Product_Name,Prod_Type_Name,BILLING_BANDWIDTH,Account_Manager,FE_Compliance_Date,Status_Update_Date,Status_Time,Status_By_FE," +
                        "Logical_Circuit_Id as UniqueID from tbl_mst_LeasedLinePRIData where 1=1 ";
            }
            else if (Value1 == "PRI-Fixed Line")
            {
                mquery = "select ExcelRawID,Allocated_Date,Party_Name,Product_Name,Prod_Type_Name,BILLING_BANDWIDTH,Account_Manager,FE_Compliance_Date,Status_Update_Date,Status_Time,Status_By_FE," +
                "Subs_Del_No as UniqueID from tbl_mst_LeasedLinePRIData where 1=1 ";
            }
            else if (Value1 == "MO")
            {
                mquery = "select ExcelRawID, Allocated_Date, Party_Name, Product_Name, Prod_Type_Name, BILLING_BANDWIDTH, Account_Manager, FE_Compliance_Date, Status_Update_Date, Status_Time, Status_By_FE," +
                     "Billed_Ext_Id as UniqueID from tbl_mst_MOData where 1=1 ";
            }
            for (int i = 0; i < dtsearchresult.Rows.Count; i++)
            {
                mquery = mquery + " and (" + dtsearchresult.Rows[i][0].ToString() + "" + dtsearchresult.Rows[i][1].ToString() + ")";
            }
            Value2 = mquery.ToString();
            return SqlHelper.Instance.GetDataset(Value2).Tables[0];
            //  return SqlHelper.Instance.MasterJobHistoryFE(Value1, Value2, "BindGrid");
        }
        public DataTable RetriveAssignJobFEByManager(string Value1, string Value2, string Value3)
        {
            return SqlHelper.Instance.RetriveAssignJobFEByManager(Value1, Value2, Value3);
        }
        public DataTable RetriveFEJobHistroy(DataTable dtsearchresult, string Value1, string Value2, string Value3, string Value4, string Criteria)
        {
            string mquery = "";
            if (Value2 == "MO-Periodic" || Value2 == "MO-Quarterly" || Value2 == "MO-COCP" || Value2 == "MO_Prepaid")
            {
                mquery = "select ExcelRawID,Allocated_Date,B2B_Email,B2B_Head_Email,B2B_Head_Name,Bill_Company,Bill_Plan,Bill_City,Billed_Act_Id,Billed_Ext_Id as UniqueEID,Billing_Address" +
           " , Billing_Trig_Date, Billing_Contact_Number, Complaince_Date, Company_Name, Coordinator_Contact_Email, Coordinator_Contact_Number, Coordinator_Name, Cust_Email," +
           " Customer_Name, Code, Eligible_Not_Eligible, From_Site, FE_Name, FE_Compliance_Date, Inserted_Date, Is_OTP_Verified, Is_Scanned, Is_Submitted," +
            "Mobile_OTP, Mobile_No, Pre_Meeting_Date, Pre_Meeting_Time, Party_Name, Primary_Address, PRODUCT_TYPE, Product_Name, Prod_Type_Name, Positive_Negative, Person_Met_Name," +
           " Person_Met_Email, Person_Met_Designation, Person_Met_Mobile_No, RM_Contact_Number, RM_Email, RM_NAME, Reason_Of_Status, Remarks_Of_Status, Revisit_PerSon_Name, Revisit_Date_Time," +
           " Revisit_Time, Status_By_FE, Status_Time, Status_Update_Date, Scanned_Date, Submitted_Date, Send_Negative_Mail_to_All, Sent_Mail_Airtel, Sent_Sam_Mail, Sent_Cust_Mail, Appointment_Mail_Datetime," +
           " Cust_Sam_Kam_Mail_Datetime, Negative_Cust_Sam_Kam_Mail_Datetime, Segment, Subs_Del_No, TL_Name, TL_Contact_Number, TL_Email, Total_Active_Num, Total_Deactive_Num, Total_New_Num, VH_Contact_Number, VH_Email," +
           " VH_Name, Is_Closed, Is_Active, Is_Call, Excel_Verifiy, Key_Dup from tbl_mst_MOData where 1=1 and Is_Closed='N' ";
            }
            else
            {
                mquery = " select ExcelRawID, Allocated_Date, Account_Manager, Account_Number, B2B_Email, B2B_Head_Email, B2B_Head_Name, Bill_Company, Bill_City, Billed_Act_Id, Billing_Address" +
                 " , Billing_Trig_Date, BILLING_BANDWIDTH, Billing_Contact_Number, Complaince_Date, Company_Name, Coordinator_Contact_Email, Coordinator_Contact_Number, Coordinator_Name" +
             " , Coordinator_Contact_Email, Coordinator_Contact_Number, Coordinator_Name, Cust_account_No, Cust_Email, Customer_Name, Customer_Segment, Code, Eligible_Not_Eligible" +
            "  , From_Site, FE_Name, FE_Compliance_Date, Inserted_Date, Is_OTP_Verified, Is_Scanned, Is_Submitted, KAM_Contact_Number, KAM_Email, KAM_Name, Logical_Circuit_Id as UniqueEID,Mobile_OTP,Pre_Meeting_Date" +
            ",Pre_Meeting_Time,Party_Name,Primary_Address,PRODUCT_TYPE,Product_Name,Prod_Type_Name,Positive_Negative,Person_Met_Name,Person_Met_Email,Person_Met_Designation," +
            "Person_Met_Mobile_No,RM_Contact_Number,RM_Email,RM_NAME,Reason_Of_Status,Remarks_Of_Status,Revisit_PerSon_Name,Revisit_Date_Time,Revisit_Time,Status_By_FE,Status_Time," +
            "Status_Update_Date,Scanned_Date,Submitted_Date,Send_Negative_Mail_to_All,Sent_Mail_Airtel,Sent_Sam_Mail,Sent_Cust_Mail,Appointment_Mail_Datetime,Cust_Sam_Kam_Mail_Datetime" +
            ",Negative_Cust_Sam_Kam_Mail_Datetime,SAM_Contact_Number ,SAM_Email,SAM_Name,SAM_TL,SAM_TL_Contact_Number,SAM_TL_EMAIL,Segment,Subs_Del_No,TL_Name,TL_Contact_Number,TL_Email" +
            ",VH_Contact_Number,VH_Email,VH_Name,Is_Closed ,Is_Active,Is_Call,Excel_Verifiy,Key_Dup from tbl_mst_LeasedLinePRIData where 1=1 and Is_Closed='N'";
            }
            for (int i = 0; i < dtsearchresult.Rows.Count; i++)
            {
                mquery = mquery + " and (" + dtsearchresult.Rows[i][0].ToString() + "" + dtsearchresult.Rows[i][1].ToString() + ")";
            }
            Value1 = mquery.ToString();
            return SqlHelper.Instance.GetDataset(Value1).Tables[0];
            // return SqlHelper.Instance.RetriveFEJobHistroy("'N'" + Value1, Value2, Value3, Value4, "SearchCode");
        }
        #endregion
        #region Deletecode
        public int DeleteCode(long Id, string value1, string value2, string value3, string strCriteria)
        {
            return SqlHelper.Instance.DeleteCode(Id, value1, value2, value3, strCriteria);
        }
        #endregion
        #region RetriveFunction
        public DataTable RetriveFunctionCode(string strValue1, string strValue2, string strValue3, string strValue4, string strValue5)
        {
            string mQuery = "select * from dbo.fn_RecordForApproveForMailMO('" + strValue1 + "','" + strValue2 + "') order by Party_Name asc";
            return SqlHelper.Instance.GetExecuteData(mQuery);
        }
        public DataTable RetriveFunctionCode1(string strValue1, string strValue2, string strValue3, string strValue4, string strValue5)
        {
            string mQuery = "select * from fn_RecordForSendMailBulk('" + strValue1 + "') order by Email asc";
            return SqlHelper.Instance.GetExecuteData(mQuery);
        }
        public DataTable RetriveFunctionCode2(string strValue1, string strValue2, string strValue3, string strValue4, string strValue5)
        {
            string mQuery = "select * from fn_CountOfLeasedLineDashboardManager('Count','" + strValue1 + "','')";
            return SqlHelper.Instance.GetExecuteData(mQuery);
        }
        public DataTable RetriveFunctionCode8(string strValue1, string strValue2, string strValue3, string strValue4, string strValue5)
        {
            string mQuery = "select * from fn_CountOfMODashboardManager('Count','" + strValue1 + "','')";
            return SqlHelper.Instance.GetExecuteData(mQuery);
        }
        public DataTable RetriveFunctionCode3(string strValue1, string strValue2, string strValue3, string strValue4, string strValue5)
        {
            string mQuery = "select * from fn_CountOfPRIDashboardManager('Count','" + strValue1 + "','')";
            return SqlHelper.Instance.GetExecuteData(mQuery);
        }
        public DataTable RetriveFunctionCode4(string strValue1, string strValue2, string strValue3, string strValue4, string strValue5)
        {
            string mQuery = "select * from fn_CountOfFEDashboardLeasedLineMOPRI('Count','" + strValue1 + "','" + strValue2 + "')";
            return SqlHelper.Instance.GetExecuteData(mQuery);
        }

        public DataTable RetriveFunctionCode5(string strValue1, string strValue2, string strValue3, string strValue4, string strValue5)
        {
            string mQuery = "select* from fn_DailyDayWiseFeedBackMo('" + strValue1 + "','" + strValue2 + "')";
            return SqlHelper.Instance.GetExecuteData(mQuery);
        }
        public DataTable RetriveFunctionCode6(string strValue1, string strValue2, string strValue3, string strValue4, string strValue5)
        {
            string mQuery = "select * from [fn_WeeklyNegativeReportMO](" + strValue1 + ",'" + strValue2 + "','" + strValue3 + "'," + strValue4 + ")";
            return SqlHelper.Instance.GetExecuteData(mQuery);
        }

        public DataTable RetriveFunctionCode7(string strValue1, string strValue2, string strValue3, string strValue4, string strValue5)
        {
            string mQuery = "select * from fn_AssignJobFeByManagerMO('" + strValue1 + "','" + strValue2 + "')";
            return SqlHelper.Instance.GetExecuteData(mQuery);
        }
        public DataTable RetriveSPCode1(string Criteria)
        {
            return SqlHelper.Instance.RetriveSPCode1(Criteria);
        }



        #endregion
        #region Retrive Code Dashboard LL Manager
        public DataTable RetriveCodeManagerDashboard(long ExcelRawId, string strValue1, string strValue2, string strValue3, string strValue4, string strValue5, string strValue6, string strValue7, DateTime strValue8, string Criteria)
        {
            return SqlHelper.Instance.RetriveCodeManagerDashboard(ExcelRawId, strValue1, strValue2, strValue3, strValue4, strValue5, strValue6, strValue7, strValue8, Criteria);
        }

        #endregion
        #region Reports Code
        public DataTable RetriveCodeReport1(string Value1, string Value2, string Criteria)
        {
            return SqlHelper.Instance.RetriveCodeReport1(Value1, Value2, Criteria);
        }
        public DataTable RetriveCodeReport2(string Value1, string Value2, string Criteria)
        {
            return SqlHelper.Instance.RetriveCodeReport2(Value1, Value2, Criteria);
        }
        public DataTable RetriveCodeReport3(string Value1, string Value2, string Criteria)
        {
            return SqlHelper.Instance.RetriveCodeReport3(Value1, Value2, Criteria);
        }
        public DataTable RetriveCodeReport5(string Value1, string Value2, string Criteria)
        {
            return SqlHelper.Instance.RetriveCodeReport5(Value1, Value2, Criteria);
        }
        public DataTable RetriveCodeReport6(string Value1, string Value2, string Criteria)
        {
            return SqlHelper.Instance.RetriveCodeReport6(Value1, Value2, Criteria);
        }
        public DataTable RetriveCodeReport7(string Value1, string Value2, string Criteria)
        {
            return SqlHelper.Instance.RetriveCodeReport7(Value1, Value2, Criteria);
        }
        public DataTable RetriveCodeReport8(string Value1, string Value2, string Criteria)
        {
            return SqlHelper.Instance.RetriveCodeReport8(Value1, Value2, Criteria);
        }
        public DataTable RetriveCodeReport9(string Value1, string Value2, string Criteria)
        {
            return SqlHelper.Instance.RetriveCodeReport9(Value1, Value2, Criteria);
        }
        #endregion
        #region UpdateCode
        public string UpdateCode(string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8, string Value9, string Value10, string Criteria)
        {
            return SqlHelper.Instance.UpdateCode(Value1, Value2, Value3, Value4, Value5, Value6, Value7, Value8, Value9, Value10, Criteria);
        }

        public int UpdateCodeSingleonly(string Query)
        {
            return SqlHelper.Instance.ExecuteQuery(Query);
        }

        #endregion
        #region All FE Code LL
        public DataTable RetriveCodeLLFE(long ExcelRawId, string ProductName, string ProductType, DateTime AllocatedDate, string Value1, string Value2, string Value3, string Value4, string Value5, string Criteria)
        {
            return SqlHelper.Instance.RetriveCodeLLFE(ExcelRawId, ProductName, ProductType, AllocatedDate, Value1, Value2, Value3, Value4, Value5, Criteria);
        }
        public DataTable RetriveCodePRIFE(long ExcelRawId, string ProductName, string ProductType, DateTime AllocatedDate, string Value1, string Value2, string Value3, string Value4, string Value5, string Criteria)
        {
            return SqlHelper.Instance.RetriveCodePRIFE(ExcelRawId, ProductName, ProductType, AllocatedDate, Value1, Value2, Value3, Value4, Value5, Criteria);
        }
        public DataTable RetriveCodeMOFE(long ExcelRawId, string ProductName, string ProductType, DateTime AllocatedDate, string Value1, string Value2, string Value3, string Value4, string Value5, string Criteria)
        {
            return SqlHelper.Instance.RetriveCodeMOFE(ExcelRawId, ProductName, ProductType, AllocatedDate, Value1, Value2, Value3, Value4, Value5, Criteria);
        }

        public int UpdateCodeFEAssignLL(long ExcelRawID, string ProductName, string Producttypename, DateTime allocateddate, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8, string Value9, string Value10, string Criteria)
        {
            return SqlHelper.Instance.UpdateCodeFEAssignLL(ExcelRawID, ProductName, Producttypename, allocateddate, Value1, Value2, Value3, Value4, Value5, Value6, Value7, Value8, Value9, Value10, Criteria);
        }

        public int UpdateCodeFEAssignPRI(long ExcelRawID, string ProductName, string Producttypename, DateTime allocateddate, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8, string Value9, string Value10, string Criteria)
        {
            return SqlHelper.Instance.UpdateCodeFEAssignPRI(ExcelRawID, ProductName, Producttypename, allocateddate, Value1, Value2, Value3, Value4, Value5, Value6, Value7, Value8, Value9, Value10, Criteria);
        }
        public int UpdateCodeFEAssignMO(long ExcelRawID, string ProductName, string Producttypename, DateTime allocateddate, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8, string Value9, string Value10, string Criteria)
        {
            return SqlHelper.Instance.UpdateCodeFEAssignMO(ExcelRawID, ProductName, Producttypename, allocateddate, Value1, Value2, Value3, Value4, Value5, Value6, Value7, Value8, Value9, Value10, Criteria);
        }
        #endregion
    }
}
