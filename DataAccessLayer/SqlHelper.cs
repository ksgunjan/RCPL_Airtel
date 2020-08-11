using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class SqlHelper
    {
        #region "Variables"
        private static SqlHelper instance;
        private readonly DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private Database db;
        private readonly string constring = string.Empty;
        # endregion
        # region "Constructor"
        private SqlHelper()
        {
            constring = ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString;
            db = new SqlDatabase(constring);
        }
        # endregion
        # region "static"
        public static SqlHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SqlHelper();
                }
                return instance;
            }
        }
        # endregion
        # region "Methods"
        public DataSet GetDataset(string Query)
        {
            DataSet dataset = new DataSet();
            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlDataAdapter sda = new SqlDataAdapter(Query, connection);               
                sda.Fill(dataset);
                return dataset;
            }
        }
        public DataSet GetDataset(SqlCommand command)
        {
            DataSet dataset = new DataSet();
            using (SqlConnection connection = new SqlConnection(constring))
            {
                command.Connection = connection;
                command.CommandTimeout = 21600;
                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(dataset);
                return dataset;
            }
        }
        public DataTable GetExecuteData(string Query)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(Query);
            dbCommand.CommandTimeout=0;
            dt = db.ExecuteDataSet(dbCommand).Tables[0];
            return dt;
        }
        public int ExecuteQuery(string mQuery)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(mQuery);
            
            return Convert.ToInt32(db.ExecuteNonQuery(dbCommand));


        }
        public string NoExecuteQuery(string mQuery)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(mQuery);
            
            return Convert.ToString(db.ExecuteNonQuery(dbCommand));
        }
        public DataTable GetDataTable(string Query)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand(Query);
                
                return db.ExecuteDataSet(dbcommand).Tables[0];
            }
            catch (Exception)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }
        # endregion
        #region "connection String"
        public bool VerifyDataBase()
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region Login Code
        public string VerifyEmployee(HybridDictionary hyLogin, out string _msg, out string Defaultpage)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_verify_UserLogin");
               
                db.AddInParameter(_dbCmd, "@UserName", DbType.String, hyLogin["UserName"]);
                db.AddInParameter(_dbCmd, "@Password", DbType.String, hyLogin["Password"]);
                db.AddOutParameter(_dbCmd, "@Name", DbType.String, 50);
                db.AddOutParameter(_dbCmd, "@LType", DbType.String, 50);
                db.AddOutParameter(_dbCmd, "@Defaultpage", DbType.String, 50);
                db.ExecuteNonQuery(_dbCmd);
                string Name = db.GetParameterValue(_dbCmd, "@Name").ToString();
                Defaultpage = db.GetParameterValue(_dbCmd, "@Defaultpage").ToString();
                _msg = db.GetParameterValue(_dbCmd, "@LType").ToString();
                return Name;
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                _msg = "0";
                Defaultpage = "0";
                return "";
            }
        }
        #endregion
        #region Menu Code
        public DataTable RetriveMasterData(long Companyid, string strRefNo, string strRole, int MenuId, string strMenuUrl, string strInterestedAreaFlag, string strCriteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetDataOnCriteria");
                    db.AddInParameter(cmd, "@CompanyID", DbType.Int64, Companyid);
                    db.AddInParameter(cmd, "@RefNo", DbType.String, strRefNo);
                    db.AddInParameter(cmd, "@Role", DbType.String, strRole);
                    db.AddInParameter(cmd, "@MenuId", DbType.Int16, MenuId);
                    db.AddInParameter(cmd, "@MenuUrl", DbType.String, strMenuUrl);
                    db.AddInParameter(cmd, "@InterestedAreaFlag", DbType.String, strInterestedAreaFlag);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, strCriteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }

                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion
        #region Retrive code
        public DataTable RetriveAdminFeCount(string FeName, string Product, string Month, string strCriteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_AdminDashboardCountFEWork");
                    
                    db.AddInParameter(cmd, "@FeName", DbType.String, FeName);
                    db.AddInParameter(cmd, "@ProductName", DbType.String, Product);
                    db.AddInParameter(cmd, "@Month", DbType.String, Month);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, strCriteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveCodeLocal(long Id, string value1, string value2, string value3, string strCriteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_RetriveCodeNormal");
                    
                    db.AddInParameter(cmd, "@strId", DbType.Int64, Id);
                    db.AddInParameter(cmd, "@strValue1", DbType.String, value1);
                    db.AddInParameter(cmd, "@strValue2", DbType.String, value2);
                    db.AddInParameter(cmd, "@strValue3", DbType.String, value3);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, strCriteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveCodeRawDataPopup(long ExcelRawId, string ProductName, string ProductType, DateTime AllocatedDate, string Status, DateTime StatusUpdateDate, string Value1, string Value2, string Query, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetResultFromRawDataTable");
                    
                    db.AddInParameter(cmd, "@ExcelRawID", DbType.Int64, ExcelRawId);
                    db.AddInParameter(cmd, "@ProductName", DbType.String, ProductName);
                    db.AddInParameter(cmd, "@ProductType", DbType.String, ProductType);
                    db.AddInParameter(cmd, "@AllocatedDate", DbType.Date, AllocatedDate);
                    db.AddInParameter(cmd, "@Status", DbType.String, Status);
                    db.AddInParameter(cmd, "@StatusUpdateDate", DbType.String, StatusUpdateDate);
                    db.AddInParameter(cmd, "@Value1", DbType.String, Value1);
                    db.AddInParameter(cmd, "@Value2", DbType.String, Value2);
                    db.AddInParameter(cmd, "@mQuery", DbType.String, Query);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveDataWithOnlyQuery(string Query, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetResultFromRawDataTableQueryOnly");
                    
                    db.AddInParameter(cmd, "@mQuery", DbType.String, Query);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable mdt = new DataTable();
                    if (dr != null)
                    {
                        mdt.Load(dr);
                    }
                    return mdt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveCodeManagerDashboard(long ExcelRawId, string strValue1, string strValue2, string strValue3, string strValue4, string strValue5, string strValue6, string strValue7, DateTime strValue8, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_RetriveCodeDashboardManagerLeasedLine");
                    
                    db.AddInParameter(cmd, "@strId", DbType.Int64, ExcelRawId);
                    db.AddInParameter(cmd, "@strValue1", DbType.String, strValue1);
                    db.AddInParameter(cmd, "@strValue2", DbType.String, strValue2);
                    db.AddInParameter(cmd, "@strValue3", DbType.String, strValue3);
                    db.AddInParameter(cmd, "@strValue4", DbType.String, strValue4);
                    db.AddInParameter(cmd, "@strValue5", DbType.String, strValue5);
                    db.AddInParameter(cmd, "@strValue6", DbType.String, strValue6);
                    db.AddInParameter(cmd, "@strValue7", DbType.String, strValue7);
                    db.AddInParameter(cmd, "@strValue8", DbType.Date, strValue8);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveCodeRescheduled_APPOINTMENT(string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_mst_updateCodeRescheduled_APPOINTMENT");
                    
                    db.AddInParameter(cmd, "@Value1", DbType.String, Value1);
                    db.AddInParameter(cmd, "@Value2", DbType.String, Value2);
                    db.AddInParameter(cmd, "@Value3", DbType.String, Value3);
                    db.AddInParameter(cmd, "@Value4", DbType.String, Value4);
                    db.AddInParameter(cmd, "@Value5", DbType.String, Value5);
                    db.AddInParameter(cmd, "@Value6", DbType.String, Value6);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveAssignJobFEByManager(string Value1, string Value2, string Value3)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_RP_AssignJobFeByManager");
                    
                    db.AddInParameter(cmd, "@ProductName", DbType.String, Value1);
                    db.AddInParameter(cmd, "@String", DbType.String, Value2);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Value3);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable MasterJobHistoryFE(string Value1, string Value2, string Value3)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_MasterFEJobHistory");
                    
                    db.AddInParameter(cmd, "@ProductName", DbType.String, Value1);
                    db.AddInParameter(cmd, "@mQuery", DbType.String, Value2);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Value3);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveFEJobHistroy(string Value1, string Value2, string Value3, string Value4, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_mst_FEJobHistory");
                    
                    db.AddInParameter(cmd, "@Value1", DbType.String, Value1);
                    db.AddInParameter(cmd, "@Value2", DbType.String, Value2);
                    db.AddInParameter(cmd, "@Value3", DbType.String, Value3);
                    db.AddInParameter(cmd, "@Value4", DbType.String, Value4);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion
        #region "Save Code"
        public string SaveEmp(HybridDictionary hysave, out string _msg, out string _sysMsg)
        {
            try
            {
                DbCommand dbcom = db.GetStoredProcCommand("sp_mst_Emp");
                db.AddInParameter(dbcom, "@EmpID", DbType.Int64, hysave["EmpID"]);
                db.AddInParameter(dbcom, "@Category", DbType.String, hysave["Category"]);
                db.AddInParameter(dbcom, "@Name", DbType.String, hysave["Name"]);
                db.AddInParameter(dbcom, "@Email", DbType.String, hysave["Email"]);
                db.AddInParameter(dbcom, "@ContactNo", DbType.Int64, hysave["ContactNo"]);
                db.AddInParameter(dbcom, "@Address", DbType.String, hysave["Address"]);
                db.AddInParameter(dbcom, "@GovtIDNo", DbType.String, hysave["GovtIDNo"]);
                db.AddInParameter(dbcom, "@Password", DbType.String, hysave["Password"]);
                db.AddInParameter(dbcom, "@ProfilePic", DbType.String, hysave["ProfilePic"]);
                db.AddOutParameter(dbcom, "@ReturnID", DbType.Int64, 0);
                db.ExecuteNonQuery(dbcom);
                _msg = "Save";
                _sysMsg = "Save";
                return "Save";
            }
            catch (SqlException ex)
            {
                _msg = "DataBase Error";
                _sysMsg = ex.Message;
                return "-1";
            }
            catch (Exception ex)
            {
                _msg = "Server error";
                _sysMsg = ex.Message;
                return "-1";
            }
        }
        public string SaveComp(HybridDictionary _hyLogin, out string _msg, out string _sysMsg)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_mst_Company");
                db.AddInParameter(_dbCmd, "@CompId", DbType.Int64, _hyLogin["CompId"]);
                db.AddInParameter(_dbCmd, "@CompName", DbType.String, _hyLogin["CompName"]);
                db.AddInParameter(_dbCmd, "@CompGSTNo", DbType.String, _hyLogin["CompGSTNo"]);
                db.AddInParameter(_dbCmd, "@CompPanNo", DbType.String, _hyLogin["CompPanNo"]);
                db.ExecuteNonQuery(_dbCmd);
                _msg = "Save";
                _sysMsg = "Save";
                return "Save";
            }
            catch (SqlException ex)
            {
                _msg = "DataBase Error";
                _sysMsg = ex.Message;
                return "-1";
            }
            catch (Exception ex)
            {
                _msg = "Server error";
                _sysMsg = ex.Message;
                return "-1";
            }
        }
        public string SaveUserCategory(HybridDictionary _hyLogin, out string _msg, out string _sysMsg)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_mst_UserCategory");
                db.AddInParameter(_dbCmd, "@UCategoryID", DbType.Int64, _hyLogin["UCategoryID"]);
                db.AddInParameter(_dbCmd, "@UserCategory", DbType.String, _hyLogin["UserCategory"]);
                db.ExecuteNonQuery(_dbCmd);
                _msg = "Save";
                _sysMsg = "Save";
                return "Save";
            }
            catch (SqlException ex)
            {
                _msg = "DataBase Error";
                _sysMsg = ex.Message;
                return "-1";
            }
            catch (Exception ex)
            {
                _msg = "Server error";
                _sysMsg = ex.Message;
                return "-1";
            }
        }
        public string SaveEmail(HybridDictionary _hyLogin, out string _msg, out string _sysMsg)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_mst_Email");
                db.AddInParameter(_dbCmd, "@EmailId", DbType.Int64, _hyLogin["EmailId"]);
                db.AddInParameter(_dbCmd, "@Email", DbType.String, _hyLogin["Email"]);
                db.AddInParameter(_dbCmd, "@Designation", DbType.String, _hyLogin["Designation"]);
                db.ExecuteNonQuery(_dbCmd);
                _msg = "Save";
                _sysMsg = "Save";
                return "Save";
            }
            catch (SqlException ex)
            {
                _msg = "DataBase Error";
                _sysMsg = ex.Message;
                return "-1";
            }
            catch (Exception ex)
            {
                _msg = "Server error";
                _sysMsg = ex.Message;
                return "-1";
            }
        }
        public string SaveProd(HybridDictionary _hyLogin, out string _msg, out string _sysMsg)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_mst_Product");
                db.AddInParameter(_dbCmd, "@ProductId", DbType.Int64, _hyLogin["ProductId"]);
                db.AddInParameter(_dbCmd, "@ProductName", DbType.String, _hyLogin["ProductName"]);
                db.AddInParameter(_dbCmd, "@CompId", DbType.Int64, _hyLogin["CompId"]);
                db.AddInParameter(_dbCmd, "@CompName", DbType.String, _hyLogin["CompName"]);
                db.ExecuteNonQuery(_dbCmd);
                _msg = "Save";
                _sysMsg = "Save";
                return "Save";
            }
            catch (SqlException ex)
            {
                _msg = "DataBase Error";
                _sysMsg = ex.Message;
                return "-1";
            }
            catch (Exception ex)
            {
                _msg = "Server error";
                _sysMsg = ex.Message;
                return "-1";
            }
        }
        public string SaveProdType(HybridDictionary _hyLogin, out string _msg, out string _sysMsg)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_mst_ProductType");
                db.AddInParameter(_dbCmd, "@ProductTypeID", DbType.Int64, _hyLogin["ProductTypeID"]);
                db.AddInParameter(_dbCmd, "@ProductTypeName", DbType.String, _hyLogin["ProductTypeName"]);
                db.ExecuteNonQuery(_dbCmd);
                _msg = "Save";
                _sysMsg = "Save";
                return "Save";
            }
            catch (SqlException ex)
            {
                _msg = "DataBase Error";
                _sysMsg = ex.Message;
                return "-1";
            }
            catch (Exception ex)
            {
                _msg = "Server error";
                _sysMsg = ex.Message;
                return "-1";
            }
        }



        #endregion
        #region Upload Excel Code
        public DataSet CreateExcelConnection(string FilePath, string SheetName, out string text)
        {
            try
            {
                OleDbConnection Connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\";");
                string mQuery = "select * from [" + SheetName + "$]";
                OleDbCommand cmd = new OleDbCommand(mQuery, Connection);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                Connection.Open();
                da.Fill(ds, "ex");
                Connection.Close();
                text = "success";
                return ds;
            }
            catch (Exception ex)
            {
                DataSet ds = new DataSet();
                text = ex.Message;
                return ds;
            }
        }
        public string SaveUploadExcelCompany(DataTable dtExcel, string comp, string product, string producttype)
        {

            using (DbConnection Connection = db.CreateConnection())
            {
                Connection.Open();
                DbTransaction Transaction = Connection.BeginTransaction();
                int errorRow = -1;
                try
                {
                    string mEntryID = "";
                    for (int a = 0; a < dtExcel.Rows.Count; a++)
                    {
                        errorRow = a;
                        DbCommand cmd = db.GetStoredProcCommand("sp_mst_InsertExcelRawData");
                        db.AddInParameter(cmd, "@ExcelRawID", DbType.Int64, "-1");
                        db.AddInParameter(cmd, "@Allocated_Date", DbType.Date, DateTime.Parse(dtExcel.Rows[a]["Allocated_Date"].ToString()));
                        db.AddInParameter(cmd, "@Account_Manager", DbType.String, dtExcel.Rows[a]["Account_Manager"].ToString());
                        db.AddInParameter(cmd, "@Account_Number", DbType.String, dtExcel.Rows[a]["Account_Number"].ToString());
                        db.AddInParameter(cmd, "@B2B_Email", DbType.String, dtExcel.Rows[a]["B2B_Email"].ToString());
                        db.AddInParameter(cmd, "@B2B_Head_Email", DbType.String, dtExcel.Rows[a]["B2B_Head_Email"].ToString());
                        db.AddInParameter(cmd, "@B2B_Head_Name", DbType.String, dtExcel.Rows[a]["B2B_Head_Name"].ToString());
                        db.AddInParameter(cmd, "@Bill_Company", DbType.String, dtExcel.Rows[a]["Bill_Company"].ToString());
                        db.AddInParameter(cmd, "@Bill_Plan", DbType.String, dtExcel.Rows[a]["Bill_Plan"].ToString());
                        db.AddInParameter(cmd, "@Bill_City", DbType.String, dtExcel.Rows[a]["Bill_City"].ToString());
                        db.AddInParameter(cmd, "@Billed_Act_Id", DbType.String, dtExcel.Rows[a]["Billed_Act_Id"].ToString());
                        db.AddInParameter(cmd, "@Billed_Ext_Id", DbType.String, dtExcel.Rows[a]["Billed_Ext_Id"].ToString());
                        db.AddInParameter(cmd, "@Billing_Address", DbType.String, dtExcel.Rows[a]["Billing_Address"].ToString());
                        if (dtExcel.Rows[a]["Billing_Trig_Date"].ToString() != "")
                        {
                            db.AddInParameter(cmd, "@Billing_Trig_Date", DbType.Date, DateTime.Parse(dtExcel.Rows[a]["Billing_Trig_Date"].ToString()));
                        }
                        else
                        {
                            db.AddInParameter(cmd, "@Billing_Trig_Date", DbType.Date, null);
                        }
                        db.AddInParameter(cmd, "@BILLING_BANDWIDTH", DbType.String, dtExcel.Rows[a]["BILLING_BANDWIDTH"].ToString());
                        db.AddInParameter(cmd, "@Billing_Contact_Number", DbType.String, (dtExcel.Rows[a]["Billing_Contact_Number"].ToString()));
                        db.AddInParameter(cmd, "@Billing_Email_Id", DbType.String, dtExcel.Rows[a]["Billing_Email_Id"].ToString());
                        db.AddInParameter(cmd, "@Complaince_Date", DbType.Date, DateTime.Parse(dtExcel.Rows[a]["Complaince_Date"].ToString()));
                        db.AddInParameter(cmd, "@Company_Name", DbType.String, comp);
                        db.AddInParameter(cmd, "@Coordinator_Contact_Email", DbType.String, dtExcel.Rows[a]["Coordinator_Contact_Email"].ToString());
                        db.AddInParameter(cmd, "@Coordinator_Contact_Number", DbType.String, (dtExcel.Rows[a]["Coordinator_Contact_Number"].ToString()));
                        db.AddInParameter(cmd, "@Coordinator_Name", DbType.String, (dtExcel.Rows[a]["Coordinator_Name"].ToString()));
                        db.AddInParameter(cmd, "@Cust_account_No", DbType.String, (dtExcel.Rows[a]["Cust_account_No"].ToString()));
                        db.AddInParameter(cmd, "@Cust_Email", DbType.String, (dtExcel.Rows[a]["Cust_Email"].ToString()));
                        db.AddInParameter(cmd, "@Customer_Name", DbType.String, (dtExcel.Rows[a]["Customer_Name"].ToString()));
                        db.AddInParameter(cmd, "@Customer_Segment", DbType.String, dtExcel.Rows[a]["Customer_Segment"].ToString());
                        db.AddInParameter(cmd, "@Eligible_Not_Eligible", DbType.String, (dtExcel.Rows[a]["Eligible_Not_Eligible"].ToString()));
                        db.AddInParameter(cmd, "@From_Site", DbType.String, (dtExcel.Rows[a]["From_Site"].ToString()));
                        db.AddInParameter(cmd, "@FE_Name", DbType.String, (dtExcel.Rows[a]["FE_Name"].ToString()));
                        db.AddInParameter(cmd, "@Inserted_Date", DbType.Date, DateTime.Now.ToString("dd-MMM-yyyy"));
                        db.AddInParameter(cmd, "@KAM_Contact_Number", DbType.String, (dtExcel.Rows[a]["KAM_Contact_Number"].ToString()));
                        db.AddInParameter(cmd, "@KAM_Email", DbType.String, (dtExcel.Rows[a]["KAM_Email"].ToString()));
                        db.AddInParameter(cmd, "@KAM_Name", DbType.String, (dtExcel.Rows[a]["KAM_Name"].ToString()));
                        db.AddInParameter(cmd, "@Logical_Circuit_Id", DbType.String, (dtExcel.Rows[a]["Logical_Circuit_Id"].ToString()));
                        db.AddInParameter(cmd, "@Mobile_No", DbType.String, (dtExcel.Rows[a]["Mobile_No"].ToString()));
                        db.AddInParameter(cmd, "@Party_Name", DbType.String, dtExcel.Rows[a]["Party_Name"].ToString());
                        db.AddInParameter(cmd, "@PRODUCT_TYPE", DbType.String, (dtExcel.Rows[a]["PRODUCT_TYPE"].ToString()));
                        db.AddInParameter(cmd, "@Product_Name", DbType.String, product);
                        db.AddInParameter(cmd, "@Prod_Type_Name", DbType.String, producttype);
                        db.AddInParameter(cmd, "@RM_Contact_Number", DbType.String, (dtExcel.Rows[a]["RM_Contact_Number"].ToString()));
                        db.AddInParameter(cmd, "@RM_Email", DbType.String, (dtExcel.Rows[a]["RM_Email"].ToString()));
                        db.AddInParameter(cmd, "@RM_NAME", DbType.String, (dtExcel.Rows[a]["RM_NAME"].ToString()));
                        db.AddInParameter(cmd, "@SAM_Contact_Number", DbType.String, (dtExcel.Rows[a]["SAM_Contact_Number"].ToString()));
                        db.AddInParameter(cmd, "@SAM_Email", DbType.String, (dtExcel.Rows[a]["SAM_Email"].ToString()));
                        db.AddInParameter(cmd, "@SAM_Name", DbType.String, (dtExcel.Rows[a]["SAM_Name"].ToString()));
                        db.AddInParameter(cmd, "@SAM_TL", DbType.String, (dtExcel.Rows[a]["SAM_TL"].ToString()));
                        db.AddInParameter(cmd, "@SAM_TL_Contact_Number", DbType.String, (dtExcel.Rows[a]["SAM_TL_Contact_Number"].ToString()));
                        db.AddInParameter(cmd, "@SAM_TL_EMAIL", DbType.String, (dtExcel.Rows[a]["SAM_TL_EMAIL"].ToString()));
                        db.AddInParameter(cmd, "@Segment", DbType.String, (dtExcel.Rows[a]["Segment"].ToString()));
                        db.AddInParameter(cmd, "@Subs_Del_No", DbType.String, dtExcel.Rows[a]["Subs_Del_No"].ToString());
                        db.AddInParameter(cmd, "@TL_Name", DbType.String, dtExcel.Rows[a]["TL_Name"].ToString());
                        db.AddInParameter(cmd, "@TL_Contact_Number", DbType.String, dtExcel.Rows[a]["TL_Contact_Number"].ToString());
                        db.AddInParameter(cmd, "@TL_Email", DbType.String, dtExcel.Rows[a]["TL_Email"].ToString());
                        db.AddInParameter(cmd, "@VH_Contact_Number", DbType.String, (dtExcel.Rows[a]["VH_Contact_Number"].ToString()));
                        db.AddInParameter(cmd, "@VH_Email", DbType.String, (dtExcel.Rows[a]["VH_Email"].ToString()));
                        db.AddInParameter(cmd, "@VH_Name", DbType.String, (dtExcel.Rows[a]["VH_Name"].ToString()));
                        db.AddInParameter(cmd, "@Key_Dup", DbType.String, (dtExcel.Rows[a]["Key_Dup"].ToString()));
                        db.AddOutParameter(cmd, "@ReturnID", DbType.String, 50);
                        db.ExecuteNonQuery(cmd, Transaction);
                        mEntryID = db.GetParameterValue(cmd, "@ReturnID").ToString();
                    }
                    Transaction.Commit();
                    return mEntryID = "Save";
                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    return ex.Message + "Error Found in Excel" + errorRow;
                }
                finally
                {
                    Connection.Close();
                }
            }
        }
        #endregion
        #region UpdateCode
        public string RetriveCodeLocalup(long Id, string value1, string value2, string value3, string strCriteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_RetriveCodeNormal");
                    
                    db.AddInParameter(cmd, "@strId", DbType.Int64, Id);
                    db.AddInParameter(cmd, "@strValue1", DbType.String, value1);
                    db.AddInParameter(cmd, "@strValue2", DbType.String, value2);
                    db.AddInParameter(cmd, "@strValue3", DbType.String, value3);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, strCriteria);
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "true";
                }
                catch (Exception)
                {
                    dbTran.Rollback();
                    return "-1";
                }
            }
        }
        public string UpdateCode(string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8, string Value9, string Value10, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_mst_updateCodeRescheduled_APPOINTMENT");
                    db.AddInParameter(cmd, "@Value1", DbType.String, Value1);
                    db.AddInParameter(cmd, "@Value2", DbType.String, Value2);
                    db.AddInParameter(cmd, "@Value3", DbType.String, Value3);
                    db.AddInParameter(cmd, "@Value4", DbType.String, Value4);
                    db.AddInParameter(cmd, "@Value5", DbType.String, Value5);
                    db.AddInParameter(cmd, "@Value6", DbType.String, Value6);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "true";
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    dbTran.Rollback();
                    return "-1";
                }
            }
        }

        public int UpdateCodeFEAssignLL(long ExcelRawID, string ProductName, string Producttypename, DateTime allocateddate, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8, string Value9, string Value10, string Criteria)
        {
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("sp_Update_LeasedLineFE");
                db.AddInParameter(cmd, "@ExcelRawID", DbType.Int64, ExcelRawID);
                db.AddInParameter(cmd, "@ProductName", DbType.String, ProductName);
                db.AddInParameter(cmd, "@ProductType", DbType.String, Producttypename);
                db.AddInParameter(cmd, "@AllocatedDate", DbType.Date, allocateddate);
                db.AddInParameter(cmd, "@Value1", DbType.String, Value1);
                db.AddInParameter(cmd, "@Value2", DbType.String, Value2);
                db.AddInParameter(cmd, "@Value3", DbType.String, Value3);
                db.AddInParameter(cmd, "@Value4", DbType.String, Value4);
                db.AddInParameter(cmd, "@Value5", DbType.String, Value5);
                db.AddInParameter(cmd, "@Value6", DbType.String, Value6);
                db.AddInParameter(cmd, "@Vlaue7", DbType.String, Value7);
                db.AddInParameter(cmd, "@Value8", DbType.String, Value8);
                db.AddInParameter(cmd, "@Value9", DbType.String, Value9);
                db.AddInParameter(cmd, "@Value10", DbType.String, Value10);
                db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                db.ExecuteNonQuery(cmd);
                return 1;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return 0;
            }
        }

        public int UpdateCodeFEAssignPRI(long ExcelRawID, string ProductName, string Producttypename, DateTime allocateddate, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8, string Value9, string Value10, string Criteria)
        {
            try
            {

                DbCommand cmd = db.GetStoredProcCommand("sp_Update_PRIFE");
                db.AddInParameter(cmd, "@ExcelRawID", DbType.Int64, ExcelRawID);
                db.AddInParameter(cmd, "@ProductName", DbType.String, ProductName);
                db.AddInParameter(cmd, "@ProductType", DbType.String, Producttypename);
                db.AddInParameter(cmd, "@AllocatedDate", DbType.Date, allocateddate);
                db.AddInParameter(cmd, "@Value1", DbType.String, Value1);
                db.AddInParameter(cmd, "@Value2", DbType.String, Value2);
                db.AddInParameter(cmd, "@Value3", DbType.String, Value3);
                db.AddInParameter(cmd, "@Value4", DbType.String, Value4);
                db.AddInParameter(cmd, "@Value5", DbType.String, Value5);
                db.AddInParameter(cmd, "@Value6", DbType.String, Value6);
                db.AddInParameter(cmd, "@Vlaue7", DbType.String, Value7);
                db.AddInParameter(cmd, "@Value8", DbType.String, Value8);
                db.AddInParameter(cmd, "@Value9", DbType.String, Value9);
                db.AddInParameter(cmd, "@@Value10", DbType.String, Value10);
                db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                db.ExecuteNonQuery(cmd);
                return 1;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return 0;
            }
        }
        public int UpdateCodeFEAssignMO(long ExcelRawID, string ProductName, string Producttypename, DateTime allocateddate, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8, string Value9, string Value10, string Criteria)
        {
            try
            {

                DbCommand cmd = db.GetStoredProcCommand("sp_Update_MOFE");
                db.AddInParameter(cmd, "@ExcelRawID", DbType.Int64, ExcelRawID);
                db.AddInParameter(cmd, "@ProductName", DbType.String, ProductName);
                db.AddInParameter(cmd, "@ProductType", DbType.String, Producttypename);
                db.AddInParameter(cmd, "@AllocatedDate", DbType.Date, allocateddate);
                db.AddInParameter(cmd, "@Value1", DbType.String, Value1);
                db.AddInParameter(cmd, "@Value2", DbType.String, Value2);
                db.AddInParameter(cmd, "@Value3", DbType.String, Value3);
                db.AddInParameter(cmd, "@Value4", DbType.String, Value4);
                db.AddInParameter(cmd, "@Value5", DbType.String, Value5);
                db.AddInParameter(cmd, "@Value6", DbType.String, Value6);
                db.AddInParameter(cmd, "@Vlaue7", DbType.String, Value7);
                db.AddInParameter(cmd, "@Value8", DbType.String, Value8);
                db.AddInParameter(cmd, "@Value9", DbType.String, Value9);
                db.AddInParameter(cmd, "@@Value10", DbType.String, Value10);
                db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                db.ExecuteNonQuery(cmd);
                return 1;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return 0;
            }
        }

        #endregion
        #region delete code
        public int DeleteCode(long Id, string value1, string value2, string value3, string strCriteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_RetriveCodeNormal");
                    db.AddInParameter(cmd, "@strId", DbType.Int64, Id);
                    db.AddInParameter(cmd, "@strValue1", DbType.String, value1);
                    db.AddInParameter(cmd, "@strValue2", DbType.String, value2);
                    db.AddInParameter(cmd, "@strValue3", DbType.String, value3);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, strCriteria);
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return 1;
                }
                catch (Exception)
                {
                    dbTran.Rollback();
                    return 0;
                }
            }
        }
        #endregion
        #region RetriveCode Reports
        public DataTable RetriveCodeReport1(string Value1, string Value2, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_mst_Reports1");
                    
                    db.AddInParameter(cmd, "@Value1", DbType.String, Value1);
                    db.AddInParameter(cmd, "@Value2", DbType.String, Value2);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveCodeReport2(string Value1, string Value2, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_mst_Reports2");
                    
                    db.AddInParameter(cmd, "@Value1", DbType.String, Value1);
                    db.AddInParameter(cmd, "@Value2", DbType.String, Value2);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveCodeReport3(string Value1, string Value2, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_mst_Reports3");
                    
                    db.AddInParameter(cmd, "@Value1", DbType.String, Value1);
                    db.AddInParameter(cmd, "@Value2", DbType.String, Value2);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveCodeReport5(string Value1, string Value2, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_mst_Reports5");
                    
                    db.AddInParameter(cmd, "@Value1", DbType.String, Value1);
                    db.AddInParameter(cmd, "@Value2", DbType.String, Value2);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveCodeReport6(string Value1, string Value2, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_mst_Reports6");
                    
                    db.AddInParameter(cmd, "@Value1", DbType.String, Value1);
                    db.AddInParameter(cmd, "@Value2", DbType.String, Value2);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveCodeReport7(string Value1, string Value2, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_mst_Reports7");
                    
                    db.AddInParameter(cmd, "@Value1", DbType.Date, Convert.ToDateTime(Value1));
                    db.AddInParameter(cmd, "@Value2", DbType.String, Value2);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveCodeReport8(string Value1, string Value2, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_mst_Reports8");
                    
                    db.AddInParameter(cmd, "@Value1", DbType.Date, Value1);
                    db.AddInParameter(cmd, "@Value2", DbType.Date, Value2);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveCodeReport9(string Value1, string Value2, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_mst_Reports9");
                    
                    db.AddInParameter(cmd, "@Value1", DbType.Date, Value1);
                    db.AddInParameter(cmd, "@Value2", DbType.Date, Value2);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion
        #region All Code FE
        public DataTable RetriveCodeLLFE(long ExcelRawId, string ProductName, string ProductType, DateTime AllocatedDate, string Value1, string Value2, string Value3, string Value4, string Value5, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_LeasedLineFE");
                    
                    db.AddInParameter(cmd, "@ExcelRawID", DbType.Int64, ExcelRawId);
                    db.AddInParameter(cmd, "@ProductName", DbType.String, ProductName);
                    db.AddInParameter(cmd, "@ProductType", DbType.String, ProductType);
                    db.AddInParameter(cmd, "@AllocatedDate", DbType.Date, AllocatedDate);
                    db.AddInParameter(cmd, "@Value1", DbType.String, Value1);
                    db.AddInParameter(cmd, "@Vlaue2", DbType.String, Value2);
                    db.AddInParameter(cmd, "@Value3", DbType.String, Value3);
                    db.AddInParameter(cmd, "@Value4", DbType.String, Value4);
                    db.AddInParameter(cmd, "@Value5", DbType.String, Value5);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveCodePRIFE(long ExcelRawId, string ProductName, string ProductType, DateTime AllocatedDate, string Value1, string Value2, string Value3, string Value4, string Value5, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_PRIFE");
                    
                    db.AddInParameter(cmd, "@ExcelRawID", DbType.Int64, ExcelRawId);
                    db.AddInParameter(cmd, "@ProductName", DbType.String, ProductName);
                    db.AddInParameter(cmd, "@ProductType", DbType.String, ProductType);
                    db.AddInParameter(cmd, "@AllocatedDate", DbType.Date, AllocatedDate);
                    db.AddInParameter(cmd, "@Value1", DbType.String, Value1);
                    db.AddInParameter(cmd, "@Vlaue2", DbType.String, Value2);
                    db.AddInParameter(cmd, "@Value3", DbType.String, Value3);
                    db.AddInParameter(cmd, "@Value4", DbType.String, Value4);
                    db.AddInParameter(cmd, "@Value5", DbType.String, Value5);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveCodeMOFE(long ExcelRawId, string ProductName, string ProductType, DateTime AllocatedDate, string Value1, string Value2, string Value3, string Value4, string Value5, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_MOFE");
                    
                    db.AddInParameter(cmd, "@ExcelRawID", DbType.Int64, ExcelRawId);
                    db.AddInParameter(cmd, "@ProductName", DbType.String, ProductName);
                    db.AddInParameter(cmd, "@ProductType", DbType.String, ProductType);
                    db.AddInParameter(cmd, "@AllocatedDate", DbType.Date, AllocatedDate);
                    db.AddInParameter(cmd, "@Value1", DbType.String, Value1);
                    db.AddInParameter(cmd, "@Vlaue2", DbType.String, Value2);
                    db.AddInParameter(cmd, "@Value3", DbType.String, Value3);
                    db.AddInParameter(cmd, "@Value4", DbType.String, Value4);
                    db.AddInParameter(cmd, "@Value5", DbType.String, Value5);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveSPCode1(string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_BulkSMSORMAIL");
                    
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion
    }
}
