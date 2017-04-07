using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using TIV.Core.DatabaseAccess;
using MainProgram.bus;
using MainProgram.model;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class MaterielOutOrder : ITableModel
    {
        private bool m_isRedBill = false;
        private SortedDictionary<int, MaterielOutOrderTable> m_tableDataList = new SortedDictionary<int, MaterielOutOrderTable>();

        static private MaterielOutOrder m_instance = null;

        private MaterielOutOrder()
        {
            load();
        }

        public void refreshRecord()
        {
            load();
        }

        static public MaterielOutOrder getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new MaterielOutOrder();
            }

            return m_instance;
        }

        public void insert(MaterielOutOrderTable record, bool isDisplayMessageBox = true)
        {
            MaterielOutOrderTable oldRecord = new MaterielOutOrderTable();

            string insert = "INSERT INTO [dbo].[WAREHOUSE_MANAGEMENT_OUT]([DEPARTMENT_ID],[TRADING_DATE],[BILL_NUMBER],";
            insert += "[PROJECT_NO],[MAKE_NO],[EXCHANGES_UNIT],[SUM_VALUE],[SUM_MONEY],[MAKE_ORDER_STAFF],[STAFF_SAVE_ID],[MATERIEL_STAFF],[IS_RED_BILL]) VALUES(";

            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
            if (checkBillIsExist(record.billNumber))
            {
                delete(record.billNumber);
            }

            insert += record.departmentID + ",";
            insert += "'" + record.tradingDate + "',";
            insert += "'" + record.billNumber + "',";

            insert += "'" + record.projectNo + "',";
            insert += "'" + record.makeNo + "',";

            insert += "'" + record.exchangesUnit + "',";
            insert += "'" + record.sumValue + "',";
            insert += "'" + record.sumMoney + "',";
            insert += record.makeOrderStaff + ",";
            insert += record.staffSaveId + ",";
            insert += record.materielOutStaffId + ",";
           
            // 红字蓝字标示
            insert += record.isRedBill ;
            insert += ")";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);

                if (isDisplayMessageBox)
                {
                    MessageBoxExtend.messageOK("数据保存成功");
                }

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }

            writeOperatorLog(304, OperatorLogType.Add, record.billNumber);
        }

        public void delete(string billNumber)
        {
            string delete = "DELETE FROM WAREHOUSE_MANAGEMENT_OUT WHERE BILL_NUMBER = '" + billNumber + "'"; 

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, delete);

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public void billReview(string billNumber, bool isRedBill = false)
        {
            m_isRedBill = isRedBill;

            writeOperatorLog(304, OperatorLogType.Review, billNumber + "开始");

            if (updateMaterielData(billNumber))
            {
                // 更新单据审核标志，把审核标志置为1
                string update = "UPDATE [dbo].[WAREHOUSE_MANAGEMENT_OUT] SET ";

                update += "[ORDERR_REVIEW] = " + DbPublic.getInctance().getCurrentLoginUserID() + ",";
                update += "REVIEW_DATE = '" + DateTime.Now.ToString("yyyyMMdd") + "', IS_REVIEW = 1";
                update += " WHERE BILL_NUMBER = '" + billNumber + "'";

                try
                {
                    DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                    MessageBoxExtend.messageOK("单据[" + billNumber + "]审核成功");

                    load();
                }
                catch (Exception error)
                {
                    MessageBoxExtend.messageWarning(error.Message);
                    return;
                }
            }

            writeOperatorLog(304, OperatorLogType.Review, billNumber + "结束");
        }

        private bool updateMaterielData(string billNumber)
        {
            bool isRet = true;

            SortedDictionary<int, MaterielOutOrderDetailsTable> dataList =
                MaterielOutOrderDetails.getInctance().getMaterielOutInfoFromBillNumber(billNumber);

            if (!m_isRedBill)
            {
                // 首先检查物料库存余额是否大于本次出库单交易额，如果小于，则提示用户，但是审核失败
                foreach (KeyValuePair<int, MaterielOutOrderDetailsTable> index in dataList)
                {
                    MaterielOutOrderDetailsTable record = index.Value;

                    InitMaterielTable materielRecord = new InitMaterielTable();
                    materielRecord.materielID = record.materielID;
                    materielRecord.value = record.value;

                    InitMaterielTable materielStorageData = InitMateriel.getInctance().getMaterielInfoFromMaterielID(record.materielID);

                    if (materielStorageData.value < materielRecord.value)
                    {
                        isRet = false;
                        MessageBoxExtend.messageWarning("物料：[" + materielRecord.materielID + "]库存数量小于当前交易数量,单据审核失败");
                        break;
                    }
                }
            }

            if (isRet)
            {
                foreach (KeyValuePair<int, MaterielOutOrderDetailsTable> index in dataList)
                {
                    MaterielOutOrderDetailsTable record = index.Value;

                    #region 更新库存汇总表(INIT_STORAGE_STOCK)

                    // 交易完毕后库存数量
                    double tradingStocksValue = record.value;

                    // 交易后库存数量等于当前仓库库存数减去本次交易数
                    InitMaterielTable materielStorageData = InitMateriel.getInctance().getMaterielInfoFromMaterielID(record.materielID);
                    if (m_isRedBill)
                    {
                        // 如果是销售退货，库存数量应该增加
                        tradingStocksValue = materielStorageData.value + tradingStocksValue;
                    }
                    else
                    {
                        tradingStocksValue = materielStorageData.value - tradingStocksValue;
                    }

                    InitMateriel.getInctance().materielOutStorage(record.materielID, tradingStocksValue);

                    #endregion

                    #region 更新存货明细账表(STORAGE_STOCK_DETAIL) 2012-1-16 00:56

                    StorageStockDetailTable storageStockDetailRecord = new StorageStockDetailTable();
                    storageStockDetailRecord.materielID = record.materielID;
                    storageStockDetailRecord.tradingDate = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                    storageStockDetailRecord.billNumber = billNumber;
                    storageStockDetailRecord.thingsType = "生产领料";
                    storageStockDetailRecord.isIn = 0;

                    // 本次交易数量和单价
                    if (m_isRedBill)
                    {
                        storageStockDetailRecord.value = record.value * -1;
                    }
                    else
                    {
                        storageStockDetailRecord.value = record.value;
                    }
                    storageStockDetailRecord.price = record.price;

                    // 交易完毕后数量和单价
                    storageStockDetailRecord.storageValue = tradingStocksValue;
                    storageStockDetailRecord.storagePrice = materielStorageData.price;

                    StorageStockDetail.getInctance().insert(storageStockDetailRecord);
                    #endregion
                }
            }

            return isRet;
        }

        private void load()
        {
            string sql = "SELECT [PKEY],[DEPARTMENT_ID],[TRADING_DATE],[BILL_NUMBER],[PROJECT_NO],[MAKE_NO],[EXCHANGES_UNIT],[SUM_VALUE],[SUM_MONEY],";
            sql += "[MAKE_ORDER_STAFF],[MATERIEL_STAFF],[ORDERR_REVIEW],[REVIEW_DATE],[IS_REVIEW],[STAFF_SAVE_ID],[IS_RED_BILL] ";
            sql += "FROM [dbo].[WAREHOUSE_MANAGEMENT_OUT] ORDER BY PKEY";

            m_tableDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    MaterielOutOrderTable record = new MaterielOutOrderTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.departmentID = DbDataConvert.ToInt32(row["DEPARTMENT_ID"]);
                    record.departmentName = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_DEPARTMENT_LIST", record.departmentID);
                    record.tradingDate = DbDataConvert.ToDateTime(row["TRADING_DATE"]).ToString("yyyy-MM-dd");
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);

                    record.projectNo = DbDataConvert.ToString(row["PROJECT_NO"]);
                    record.makeNo = DbDataConvert.ToString(row["MAKE_NO"]);
                    record.exchangesUnit = DbDataConvert.ToString(row["EXCHANGES_UNIT"]);

                    record.sumValue = DbDataConvert.ToString(row["SUM_VALUE"]);
                    record.sumMoney = DbDataConvert.ToString(row["SUM_MONEY"]);

                    record.staffSaveId = DbDataConvert.ToInt32(row["STAFF_SAVE_ID"]);
                    record.staffSaveName = Staff.getInctance().getStaffNameFromPkey(record.staffSaveId);

                    record.materielOutStaffId = DbDataConvert.ToInt32(row["MATERIEL_STAFF"]);
                    record.materielOutStaffName = Staff.getInctance().getStaffNameFromPkey(record.materielOutStaffId);

                    record.makeOrderStaff = DbDataConvert.ToInt32(row["MAKE_ORDER_STAFF"]);
                    record.makeOrderStaffName = Staff.getInctance().getStaffNameFromPkey(record.makeOrderStaff);

                    record.isReview = DbDataConvert.ToString(row["IS_REVIEW"]);
                    if (DbDataConvert.ToString(row["ORDERR_REVIEW"]).Length > 0)
                    {
                        record.orderrReview = DbDataConvert.ToInt32(row["ORDERR_REVIEW"]);
                        record.orderrReviewName = Staff.getInctance().getStaffNameFromPkey(record.orderrReview);
                        record.reviewDate = DbDataConvert.ToDateTime(row["REVIEW_DATE"]).ToString("yyyy-MM-dd");
                    }

                    record.isRedBill = DbDataConvert.ToInt32(row["IS_RED_BILL"]);
                    
                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        public SortedDictionary<int, MaterielOutOrderTable> getAllMaterielOutOrderInfo()
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public SortedDictionary<int, MaterielOutOrderTable> getAllPurchaseOrderInfoFromProjectNum(string projectNum)
        {
            SortedDictionary<int, MaterielOutOrderTable> list = new SortedDictionary<int, MaterielOutOrderTable>();

            foreach (KeyValuePair<int, MaterielOutOrderTable> index in m_tableDataList)
            {
                MaterielOutOrderTable record = new MaterielOutOrderTable();
                record = index.Value;

                if (index.Value.projectNo == projectNum)
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public SortedDictionary<int, MaterielOutOrderTable> getAllReviewMaterielOutOrderInfo()
        {
            SortedDictionary<int, MaterielOutOrderTable> list = new SortedDictionary<int, MaterielOutOrderTable>();

            foreach (KeyValuePair<int, MaterielOutOrderTable> index in m_tableDataList)
            {
                MaterielOutOrderTable record = new MaterielOutOrderTable();
                record = index.Value;

                if (index.Value.isReview == "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public SortedDictionary<int, MaterielOutOrderTable> getAllNotReviewMaterielOutOrderInfo()
        {
            SortedDictionary<int, MaterielOutOrderTable> list = new SortedDictionary<int, MaterielOutOrderTable>();

            foreach (KeyValuePair<int, MaterielOutOrderTable> index in m_tableDataList)
            {
                MaterielOutOrderTable record = new MaterielOutOrderTable();
                record = index.Value;

                if (index.Value.isReview != "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public MaterielOutOrderTable getMaterielOutOrderInfoFromBillNumber(string billNumber)
        {
            MaterielOutOrderTable record = new MaterielOutOrderTable();

            foreach (KeyValuePair<int, MaterielOutOrderTable> index in m_tableDataList)
            {
                if (index.Value.billNumber == billNumber)
                {
                    record = index.Value;
                    break;
                }
            }

            return record;
        }

        public bool checkBillIsExist(string billNumber)
        {
            bool isRet = false;

            foreach (KeyValuePair<int, MaterielOutOrderTable> index in m_tableDataList)
            {
                MaterielOutOrderTable record = new MaterielOutOrderTable();

                if (index.Value.billNumber == billNumber)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }

        public bool checkBillIsReview(string billNumber)
        {
            bool isRet = false;

            foreach (KeyValuePair<int, MaterielOutOrderTable> index in m_tableDataList)
            {
                MaterielOutOrderTable record = new MaterielOutOrderTable();

                if (index.Value.billNumber == billNumber)
                {
                    if (index.Value.isReview == "1")
                    {
                        isRet = true;
                    }
                    break;
                }
            }

            return isRet;
        }
    }

    public class MaterielOutOrderTable
    {
        public int pkey { get; set; }
        public int departmentID { get; set; }
        public string departmentName { get; set; }
        public string tradingDate { get; set; }
        public string billNumber { get; set; }

        // 2017-1-15 新增项目编号和生产编码
        public string projectNo { get; set; }
        public string makeNo { get; set; }

        public string exchangesUnit { get; set; }

        public string sumValue { get; set; }
        public string sumMoney { get; set; }

        // 保管员
        public int staffSaveId { get; set; }
        public string staffSaveName { get; set; }

        // 领料人id和姓名
        public int materielOutStaffId { get; set; }
        public string materielOutStaffName { get; set; }

        // 制单人
        public int makeOrderStaff { get; set; }
        public string makeOrderStaffName { get; set; }

        // 审核人
        public int orderrReview { get; set; }
        public string orderrReviewName { get; set; }
        public string reviewDate { get; set; }
        public string isReview { get; set; }

        // 红字蓝字单据标示
        public int isRedBill { get; set; }
    }
}