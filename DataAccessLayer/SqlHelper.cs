using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.OleDb;

namespace DataAccessLayer
{
    public class SqlHelper
    {
        # region "Variables"
        private static SqlHelper instance;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        public SqlDatabase db;
        # endregion
        # region "Constructor"
        public SqlHelper()
        {
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
            DbCommand dbCommand = db.GetSqlStringCommand(Query);
            dbCommand.CommandTimeout = 120;
            ds = db.ExecuteDataSet(dbCommand);
            return ds;
        }
        public DataTable GetExecuteData(string Query)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(Query);
            // dbCommand.CommandTimeout = 120;
            dt = db.ExecuteDataSet(dbCommand).Tables[0];
            return dt;
        }
        public Int32 ExecuteQuery(string mQuery)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(mQuery);
            return Convert.ToInt32(db.ExecuteNonQuery(dbCommand));
        }
        public DataTable GetDataTable(string Query)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand(Query);
                return db.ExecuteDataSet(dbcommand).Tables[0];
            }
            catch (Exception ex)
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
                // db = new SqlDatabase(@"data source=167.114.173.232;Initial Catalog =Gip_RCPLCRM;User Id=sa;Password=DuG39yjR;Pooling=false; Connection Timeout=360");
                db = new SqlDatabase(@"data source=103.73.189.114;Initial Catalog =Gip_RCPLCRM;User Id=sa;Password=mXy<wxh3:Mh@U;Pooling=false; Connection Timeout=360");
               // db = new SqlDatabase(@"data source=DESKTOP-11TFGRC\MOHAMMADWALI;Initial Catalog =Gip_RCPLCRM;User Id=sa;Password=wali;Pooling=false; Connection Timeout=120");
                db.CreateConnection().Open();
                db.CreateConnection().Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region "Login"
        public Int32 VerifyEmployee(HybridDictionary _hyLogin, out DateTime LastLoginTime)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_verify_Login");
                db.AddInParameter(_dbCmd, "@UserName", DbType.String, _hyLogin["UserName"]);
                db.AddInParameter(_dbCmd, "@Password", DbType.String, _hyLogin["Password"]);
                db.AddOutParameter(_dbCmd, "@EmpId", DbType.Int32, 0);
                db.AddOutParameter(_dbCmd, "@LastLoginTime", DbType.DateTime, 0);
                db.ExecuteNonQuery(_dbCmd);
                LastLoginTime = Convert.ToDateTime(db.GetParameterValue(_dbCmd, "@LastLoginTime"));
                Int32 _empId = Convert.ToInt32(db.GetParameterValue(_dbCmd, "@EmpID"));
                return _empId;
            }
            catch (SqlException ex)
            {
                LastLoginTime = DateTime.Now;
                return 0;
            }
            catch (Exception ex)
            {
                LastLoginTime = DateTime.Now;
                return 0;
            }
        }
        #endregion
        #region "Save Code"
        public string SaveEmp(HybridDictionary hysave, out string _msg, out string _sysMsg)
        {
            using (DbConnection dbcon = db.CreateConnection())
            {
                dbcon.Open();
                DbTransaction dbTran = dbcon.BeginTransaction();
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
                    db.AddInParameter(dbcom, "@ProfilePic", DbType.String, hysave["ProfilePic"]);
                    db.AddInParameter(dbcom, "@FEZoneAreaLocation", DbType.String, hysave["FEZoneAreaLocation"]);
                    db.AddOutParameter(dbcom, "@ReturnID", DbType.Int64, 0);
                    db.ExecuteNonQuery(dbcom, dbTran);

                    Int64 ID = Convert.ToInt64(db.GetParameterValue(dbcom, "@ReturnID"));

                    DbCommand dbcom1 = db.GetStoredProcCommand("sp_mst_Login");
                    db.AddInParameter(dbcom1, "@LoginID", DbType.Int64, hysave["LoginID"]);
                    db.AddInParameter(dbcom1, "@EmpID", DbType.Int64, ID);
                    db.AddInParameter(dbcom1, "@UserName", DbType.String, hysave["Email"]);
                    db.AddInParameter(dbcom1, "@Password", DbType.String, hysave["Password"]);
                    db.ExecuteNonQuery(dbcom1, dbTran);
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return "Save";

                }
                catch (SqlException ex)
                {
                    dbTran.Rollback();
                    _msg = " DataBase Error";
                    _sysMsg = ex.Message;
                    return "-1";

                }
                finally
                {
                    dbcon.Close();
                }
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
        public string SaveZone(HybridDictionary _hyLogin, out string _msg, out string _sysMsg)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_mst_Zone");
                db.AddInParameter(_dbCmd, "@ZoneID", DbType.Int64, _hyLogin["ZoneID"]);
                db.AddInParameter(_dbCmd, "@ZoneName", DbType.String, _hyLogin["ZoneName"]);
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
        public string SaveZoneArea(HybridDictionary _hyLogin, out string _msg, out string _sysMsg)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_mst_ZoneArea");
                db.AddInParameter(_dbCmd, "@ZoneAreaID", DbType.Int64, _hyLogin["ZoneAreaID"]);
                db.AddInParameter(_dbCmd, "@ZoneArea", DbType.String, _hyLogin["ZoneArea"]);
                db.AddInParameter(_dbCmd, "@ZoneAreaPinCode", DbType.String, _hyLogin["ZoneAreaPinCode"]);
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
        public string SaveZoneAreaMapping(HybridDictionary _hyLogin, out string _msg, out string _sysMsg)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_mst_ZoneMapping");
                db.AddInParameter(_dbCmd, "@ZoneMappingID", DbType.Int64, _hyLogin["ZoneMappingID"]);
                db.AddInParameter(_dbCmd, "@ZoneName", DbType.String, _hyLogin["ZoneName"]);
                db.AddInParameter(_dbCmd, "@ZoneAreaCode", DbType.String, _hyLogin["ZoneAreaCode"]);
                db.AddInParameter(_dbCmd, "@ZoneAreaID", DbType.Int64, _hyLogin["ZoneAreaID"]);
                db.AddInParameter(_dbCmd, "@ZoneAreaName", DbType.String, _hyLogin["ZoneAreaName"]);
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
                //Connection = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;data source=" + FilePath + ";Extended Properties=Excel 8.0;")
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
                Int32 errorRow = -1;
                try
                {
                    string mEntryID = "";
                    for (int a = 0; a < dtExcel.Rows.Count; a++)
                    {
                        errorRow = a;

                        //  if (product == "MO")
                        // {
                         DbCommand cmd = db.GetStoredProcCommand("sp_mst_trnRawData");
                        //}
                        //else if (product == "Leased Line")
                        //{
                        //    cmd = db.GetStoredProcCommand("sp_mst_trnRawDataLeasedLine");
                        //}
                        //else if (product == "PRI-Fixed Line")
                        //{
                        //    cmd = db.GetStoredProcCommand("sp_mst_trnRawDataPRI");
                        //}
                        db.AddInParameter(cmd, "@UniqueID", DbType.Int64, "-1");

                        db.AddInParameter(cmd, "@Allocated_Date", DbType.Date, DateTime.Parse(dtExcel.Rows[a]["Allocated Date"].ToString()));
                        db.AddInParameter(cmd, "@Account_Caf_No", DbType.String, dtExcel.Rows[a]["Account Caf No"].ToString());
                        db.AddInParameter(cmd, "@Account_Manager", DbType.String, dtExcel.Rows[a]["Account Manager"].ToString());
                        db.AddInParameter(cmd, "@Account_Number", DbType.String, dtExcel.Rows[a]["Account Number"].ToString());
                        db.AddInParameter(cmd, "@Alt_Phone1", DbType.String, dtExcel.Rows[a]["Alt Phone1"].ToString());
                        db.AddInParameter(cmd, "@Alt_Phone2", DbType.String, dtExcel.Rows[a]["Alt Phone2"].ToString());
                        db.AddInParameter(cmd, "@Annotation", DbType.String, dtExcel.Rows[a]["Annotation"].ToString());
                        db.AddInParameter(cmd, "@B2B_Email", DbType.String, dtExcel.Rows[a]["B2B_Email"].ToString());
                        db.AddInParameter(cmd, "@B2B_Head_Email", DbType.String, dtExcel.Rows[a]["B2B_Head_Email"].ToString());
                        db.AddInParameter(cmd, "@B2B_Head_Name", DbType.String, dtExcel.Rows[a]["B2B_Head_Name"].ToString());
                        db.AddInParameter(cmd, "@Bill_Company", DbType.String, dtExcel.Rows[a]["Bill Company"].ToString());
                        db.AddInParameter(cmd, "@Bill_Plan", DbType.String, dtExcel.Rows[a]["Bill Plan"].ToString());
                        db.AddInParameter(cmd, "@Bill_UOM", DbType.String, dtExcel.Rows[a]["Bill UOM"].ToString());
                        db.AddInParameter(cmd, "@Bill_City", DbType.String, dtExcel.Rows[a]["Bill_City"].ToString());
                        db.AddInParameter(cmd, "@Billed_Act_Id", DbType.String, dtExcel.Rows[a]["Billed Act Id"].ToString());
                        db.AddInParameter(cmd, "@Billed_Ext_Id", DbType.String, dtExcel.Rows[a]["Billed Ext Id"].ToString());
                        db.AddInParameter(cmd, "@Billing_Address", DbType.String, dtExcel.Rows[a]["Billing Address"].ToString());
                        if (dtExcel.Rows[a]["Billing Trig Date"].ToString() != "")
                        {
                            db.AddInParameter(cmd, "@Billing_Trig_Date", DbType.Date, DateTime.Parse(dtExcel.Rows[a]["Billing Trig Date"].ToString()));
                        }
                        else
                        {
                            db.AddInParameter(cmd, "@Billing_Trig_Date", DbType.Date, DateTime.Parse("1900-01-01"));
                        }
                        db.AddInParameter(cmd, "@BILLING_BANDWIDTH", DbType.String, dtExcel.Rows[a]["BILLING_BANDWIDTH"].ToString());
                        db.AddInParameter(cmd, "@BILLING_BANDWIDTH_UOM", DbType.String, dtExcel.Rows[a]["BILLING_BANDWIDTH_UOM"].ToString());
                        db.AddInParameter(cmd, "@Billing_Contact_Number", DbType.String, (dtExcel.Rows[a]["Billing_Contact_Number"].ToString()));
                        db.AddInParameter(cmd, "@Billing_Email_Id", DbType.String, "");
                        db.AddInParameter(cmd, "@ComplainceDate", DbType.Date, DateTime.Parse(dtExcel.Rows[a]["ComplainceDate"].ToString()));
                        db.AddInParameter(cmd, "@Charge_Name", DbType.String, (dtExcel.Rows[a]["Charge Name"].ToString()));
                        db.AddInParameter(cmd, "@Circuit_BandWidth", DbType.String, (dtExcel.Rows[a]["Circuit BandWidth"].ToString()));
                        if (dtExcel.Rows[a]["Commissioning Date"].ToString() != "")
                        {
                            db.AddInParameter(cmd, "@Commissioning_Date", DbType.Date, DateTime.Parse(dtExcel.Rows[a]["Commissioning Date"].ToString()));
                        }
                        else
                        {
                            db.AddInParameter(cmd, "@Commissioning_Date", DbType.Date, DateTime.Parse("1900-01-01"));
                        }
                        db.AddInParameter(cmd, "@Company_Name", DbType.String, comp);
                        db.AddInParameter(cmd, "@Contact_Phone1", DbType.String, (dtExcel.Rows[a]["Contact Phone1"].ToString()));
                        db.AddInParameter(cmd, "@Contact_Phone2", DbType.String, (dtExcel.Rows[a]["Contact Phone2"].ToString()));
                        db.AddInParameter(cmd, "@Coordinator_Contact_Email", DbType.String, dtExcel.Rows[a]["Coordinator_Contact_Email"].ToString());
                        db.AddInParameter(cmd, "@Coordinator_Contact_Number", DbType.String, (dtExcel.Rows[a]["Coordinator_Contact_Number"].ToString()));
                        db.AddInParameter(cmd, "@Coordinator_Name", DbType.String, (dtExcel.Rows[a]["Coordinator_Name"].ToString()));
                        db.AddInParameter(cmd, "@Cust_account_No", DbType.String, (dtExcel.Rows[a]["Cust account No"].ToString()));
                        db.AddInParameter(cmd, "@Cust_Email", DbType.String, (dtExcel.Rows[a]["Cust Email"].ToString()));
                        db.AddInParameter(cmd, "@Customer_Name", DbType.String, (dtExcel.Rows[a]["Customer Name"].ToString()));
                        db.AddInParameter(cmd, "@Customer_Segment", DbType.String, dtExcel.Rows[a]["Customer Segment"].ToString());
                        db.AddInParameter(cmd, "@Eligible_Not_Eligible", DbType.String, (dtExcel.Rows[a]["Eligible/Not Eligible"].ToString()));
                        db.AddInParameter(cmd, "@External_id_type", DbType.String, (dtExcel.Rows[a]["External id type"].ToString()));
                        db.AddInParameter(cmd, "@From_Site", DbType.String, (dtExcel.Rows[a]["From Site"].ToString()));
                        db.AddInParameter(cmd, "@FE_Name", DbType.String, (dtExcel.Rows[a]["FE Name"].ToString()));
                        db.AddInParameter(cmd, "@FX_ACCOUNT_EXTERNAL_ID", DbType.String, (dtExcel.Rows[a]["FX_ACCOUNT_EXTERNAL_ID"].ToString()));
                        db.AddInParameter(cmd, "@Installation_Address", DbType.String, (dtExcel.Rows[a]["Installation Address"].ToString()));
                        db.AddInParameter(cmd, "@InsertedDate", DbType.Date, DateTime.Now.ToString("dd-MMM-yyyy"));
                        db.AddInParameter(cmd, "@KAM_Contact_Number", DbType.String, (dtExcel.Rows[a]["KAM_Contact_Number"].ToString()));
                        db.AddInParameter(cmd, "@KAM_Email", DbType.String, (dtExcel.Rows[a]["KAM_Email"].ToString()));
                        db.AddInParameter(cmd, "@KAM_Name", DbType.String, (dtExcel.Rows[a]["KAM_Name"].ToString()));
                        db.AddInParameter(cmd, "@Line_Item_Description", DbType.String, (dtExcel.Rows[a]["Line Item Description"].ToString()));
                        db.AddInParameter(cmd, "@Line_Name", DbType.String, (dtExcel.Rows[a]["Line_Name"].ToString()));
                        db.AddInParameter(cmd, "@LOB", DbType.String, (dtExcel.Rows[a]["LOB"].ToString()));
                        if (dtExcel.Rows[a]["LOC Created Date By PM"].ToString() != "")
                        {
                            db.AddInParameter(cmd, "@LOC_Created_Date_By_PM", DbType.Date, DateTime.Parse(dtExcel.Rows[a]["LOC Created Date By PM"].ToString()));
                        }
                        else
                        {
                            db.AddInParameter(cmd, "@LOC_Created_Date_By_PM", DbType.Date, DateTime.Parse("1900-01-01"));
                        }
                        db.AddInParameter(cmd, "@LOC_Status", DbType.String, (dtExcel.Rows[a]["LOC Status"].ToString()));
                        if (dtExcel.Rows[a]["LOC Submiited Date By PM"].ToString() != "")
                        {
                            db.AddInParameter(cmd, "@LOC_Submiited_Date_By_PM", DbType.Date, DateTime.Parse(dtExcel.Rows[a]["LOC Submiited Date By PM"].ToString()));
                        }
                        else
                        {
                            db.AddInParameter(cmd, "@LOC_Submiited_Date_By_PM", DbType.Date, DateTime.Parse("1900-01-01"));
                        }
                        db.AddInParameter(cmd, "@Location", DbType.String, (dtExcel.Rows[a]["Location"].ToString()));
                        db.AddInParameter(cmd, "@Location_Secondary", DbType.String, (dtExcel.Rows[a]["Location Secondary"].ToString()));
                        db.AddInParameter(cmd, "@Logical_Circuit_Id", DbType.String, (dtExcel.Rows[a]["Logical Circuit Id"].ToString()));
                        db.AddInParameter(cmd, "@Mkt_Code", DbType.String, (dtExcel.Rows[a]["Mkt Code"].ToString()));
                        db.AddInParameter(cmd, "@Mobile_No", DbType.String, (dtExcel.Rows[a]["Mobile No"].ToString()));
                        db.AddInParameter(cmd, "@New_Connection_Type", DbType.String, dtExcel.Rows[a]["New_Connection_Type"].ToString());
                        db.AddInParameter(cmd, "@Num_channel", DbType.String, dtExcel.Rows[a]["Num channel"].ToString());
                        db.AddInParameter(cmd, "@Order_Type", DbType.String, dtExcel.Rows[a]["Order Type"].ToString());
                        if (dtExcel.Rows[a]["Orig Service Start Date"].ToString() != "")
                        {
                            db.AddInParameter(cmd, "@Orig_Service_Start_Date", DbType.Date, DateTime.Parse(dtExcel.Rows[a]["Orig Service Start Date"].ToString()));
                        }
                        else
                        {
                            db.AddInParameter(cmd, "@Orig_Service_Start_Date", DbType.Date, DateTime.Parse("1900-01-01"));
                        }
                        if (dtExcel.Rows[a]["Overall DD Completion Date"].ToString() != "")
                        {
                            db.AddInParameter(cmd, "@Overall_DD_Completion_Date", DbType.Date, DateTime.Parse(dtExcel.Rows[a]["Overall DD Completion Date"].ToString()));
                        }
                        else
                        {
                            db.AddInParameter(cmd, "@Overall_DD_Completion_Date", DbType.Date, DateTime.Parse("1900-01-01"));
                        }
                        db.AddInParameter(cmd, "@Party_Name", DbType.String, dtExcel.Rows[a]["Party Name"].ToString());
                        db.AddInParameter(cmd, "@Phone_Nos", DbType.String, dtExcel.Rows[a]["Phone Nos"].ToString());
                        db.AddInParameter(cmd, "@Pincode", DbType.String, dtExcel.Rows[a]["Pincode"].ToString());
                        db.AddInParameter(cmd, "@PO_Number", DbType.String, dtExcel.Rows[a]["PO Number"].ToString());
                        db.AddInParameter(cmd, "@POP_1", DbType.String, dtExcel.Rows[a]["POP 1"].ToString());
                        db.AddInParameter(cmd, "@POP_2", DbType.String, dtExcel.Rows[a]["POP 2"].ToString());
                        db.AddInParameter(cmd, "@Primary_Address", DbType.String, dtExcel.Rows[a]["Primary Address"].ToString());
                        db.AddInParameter(cmd, "@Project_Manager", DbType.String, dtExcel.Rows[a]["Project Manager"].ToString());
                        db.AddInParameter(cmd, "@Product_Name", DbType.String, (dtExcel.Rows[a]["Product Name"].ToString()));
                        db.AddInParameter(cmd, "@PRODUCT_TYPE", DbType.String, (dtExcel.Rows[a]["PRODUCT_TYPE"].ToString()));
                        db.AddInParameter(cmd, "@ProductName", DbType.String, product);
                        db.AddInParameter(cmd, "@ProdTypeName", DbType.String, producttype);
                        db.AddInParameter(cmd, "@RM_Contact_Number", DbType.String, (dtExcel.Rows[a]["RM_Contact_Number"].ToString()));
                        db.AddInParameter(cmd, "@RM_Email", DbType.String, (dtExcel.Rows[a]["RM_Email"].ToString()));
                        db.AddInParameter(cmd, "@RM_NAME", DbType.String, (dtExcel.Rows[a]["RM_NAME"].ToString()));
                        if (dtExcel.Rows[a]["RecievedDate"].ToString() != "")
                        {
                            db.AddInParameter(cmd, "@RecievedDate", DbType.Date, DateTime.Parse(dtExcel.Rows[a]["RecievedDate"].ToString()));
                        }
                        else
                        {
                            db.AddInParameter(cmd, "@RecievedDate", DbType.Date, DateTime.Parse("1900-01-01"));
                        }
                        db.AddInParameter(cmd, "@SAM_Contact_Number", DbType.String, (dtExcel.Rows[a]["SAM_Contact_Number"].ToString()));
                        db.AddInParameter(cmd, "@SAM_Email", DbType.String, (dtExcel.Rows[a]["SAM_Email"].ToString()));
                        db.AddInParameter(cmd, "@SAM_Name", DbType.String, (dtExcel.Rows[a]["SAM_Name"].ToString()));
                        db.AddInParameter(cmd, "@SAM_TL", DbType.String, (dtExcel.Rows[a]["SAM_TL"].ToString()));
                        db.AddInParameter(cmd, "@SAM_TL_Contact_Number", DbType.String, (dtExcel.Rows[a]["SAM_TL_Contact_Number"].ToString()));
                        db.AddInParameter(cmd, "@SAM_TL_EMAIL", DbType.String, (dtExcel.Rows[a]["SAM_TL_EMAIL"].ToString()));
                        db.AddInParameter(cmd, "@Secondary_Address", DbType.String, (dtExcel.Rows[a]["Secondary Address"].ToString()));
                        db.AddInParameter(cmd, "@Segment", DbType.String, (dtExcel.Rows[a]["Segment"].ToString()));
                        db.AddInParameter(cmd, "@Service_City", DbType.String, (dtExcel.Rows[a]["Service City"].ToString()));
                        db.AddInParameter(cmd, "@Service_Name", DbType.String, (dtExcel.Rows[a]["Service Name"].ToString()));
                        db.AddInParameter(cmd, "@Service_Start", DbType.String, (dtExcel.Rows[a]["Service Start"].ToString()));
                        db.AddInParameter(cmd, "@Status", DbType.String, (dtExcel.Rows[a]["Status"].ToString()));
                        db.AddInParameter(cmd, "@Sub_Product_Name", DbType.String, (dtExcel.Rows[a]["Sub Product Name"].ToString()));
                        db.AddInParameter(cmd, "@Subs_Del_No", DbType.String, dtExcel.Rows[a]["Subs Del No"].ToString());
                        db.AddInParameter(cmd, "@TL_Name", DbType.String, dtExcel.Rows[a]["TL Name"].ToString());
                        db.AddInParameter(cmd, "@TL_Contact_Number", DbType.String, dtExcel.Rows[a]["TL_Contact_Number"].ToString());
                        db.AddInParameter(cmd, "@TL_Email", DbType.String, dtExcel.Rows[a]["TL_Email"].ToString());
                        db.AddInParameter(cmd, "@To_Site", DbType.String, dtExcel.Rows[a]["To Site"].ToString());
                        db.AddInParameter(cmd, "@Unique_Company", DbType.String, dtExcel.Rows[a]["Unique Company"].ToString());
                        db.AddInParameter(cmd, "@Unique_Installation_Address", DbType.String, dtExcel.Rows[a]["Unique_Installation_Address"].ToString());
                        db.AddInParameter(cmd, "@Verification", DbType.String, dtExcel.Rows[a]["Verification"].ToString());
                        db.AddInParameter(cmd, "@Verification_Agent", DbType.String, dtExcel.Rows[a]["Verification Agent"].ToString());
                        db.AddInParameter(cmd, "@Verification_Code", DbType.String, dtExcel.Rows[a]["Verification Code"].ToString());
                        if (dtExcel.Rows[a]["Verification Date"].ToString() != "")
                        {
                            db.AddInParameter(cmd, "@Verification_Date", DbType.Date, DateTime.Parse(dtExcel.Rows[a]["Verification Date"].ToString()));
                        }
                        else
                        {
                            db.AddInParameter(cmd, "@Verification_Date", DbType.Date, DateTime.Parse("1900-01-01"));
                        }
                        db.AddInParameter(cmd, "@Verification_Status", DbType.String, dtExcel.Rows[a]["Verification Status"].ToString());
                        db.AddInParameter(cmd, "@Verification_Type", DbType.String, dtExcel.Rows[a]["Verification Type"].ToString());
                        db.AddInParameter(cmd, "@Vertical", DbType.String, dtExcel.Rows[a]["Vertical"].ToString());
                        db.AddInParameter(cmd, "@VH_Contact_Number", DbType.String, (dtExcel.Rows[a]["VH_Contact_Number"].ToString()));
                        db.AddInParameter(cmd, "@VH_Email", DbType.String, (dtExcel.Rows[a]["VH_Email"].ToString()));
                        db.AddInParameter(cmd, "@VH_Name", DbType.String, (dtExcel.Rows[a]["VH_Name"].ToString()));
                        db.AddInParameter(cmd, "@KeyDup", DbType.String, (dtExcel.Rows[a]["Key_Dup"].ToString()));
                        db.AddInParameter(cmd, "@EligibleRemarks", DbType.String, (dtExcel.Rows[a]["EligibleRemark"].ToString()));
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
    }
}
