using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using TIV.Core.DatabaseAccess;
using System.Collections;
using MainProgram.bus;
using MainProgram.model;


namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class PurchaseInOrderDetails : ITableModel
    {
        private SortedDictionary<int, PurchaseInOrderDetailsTable> m_tableDataList = new SortedDictionary<int, PurchaseInOrderDetailsTable>();

        static private PurchaseInOrderDetails m_instance = null;

        private PurchaseInOrderDetails()
        {
            load();
        }

        static public PurchaseInOrderDetails getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new PurchaseInOrderDetails();
            }

            return m_instance;
        }

        public void insert(ArrayList purchaseOrderDetailsRecords)
        {
            if(purchaseOrderDetailsRecords.Count <= 0)
            {
                return ;
            }
            else
            {
                try
                {
                    for (int i = 0; i < purchaseOrderDetailsRecords.Count; i++)
                    {
                        PurchaseInOrderDetailsTable record = (PurchaseInOrderDetailsTable)purchaseOrderDetailsRecords[i];

                        if (i == 0 && checkBillIsExist(record.billNumber))
                        {
                            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
                            delete(record.billNumber);
                        }

                        string insert = "INSERT INTO [dbo].[PURCHASE_IN_ORDER_DETAILS]([ROW_NUMBER],[MATERIEL_ID],[BILL_NUMBER], [CONTRACT_MATERIEL_NAME]";
                        insert += ",[PRICE],[VALUE],[TRANSPORTATION_COST],[OTHER_COST])VALUES(";

                        insert += "'" + record.rowNumber + "',";
                        insert += record.materielID + ",";
                        insert += "'" + record.billNumber + "',";
                        insert += "'" + record.contractMaterielName + "',";

                        insert += record.price + ",";
                        insert += record.value + ",";
                        insert += record.costApportionments + ",";
                        insert += record.noCostApportionments;
                        insert += ")";

                        try
                        {
                            DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);
                        }
                        catch (Exception error)
                        {
                            MessageBoxExtend.messageWarning(error.Message);
                            return;
                        }
                    }

                    load();
                }
                catch (Exception)
                {
 
                }
            }
        }

        public void delete(string billNumber)
        {
            string delete = "DELETE FROM PURCHASE_IN_ORDER_DETAILS WHERE BILL_NUMBER = '" + billNumber + "'";

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

        public void updateInStorageValue(string billNumber, string rowNumber, string actualValue)
        {
            string update = "UPDATE [dbo].[PURCHASE_IN_ORDER_DETAILS] SET ";

            update += "IS_IN_STORAGE = 1, ";
            update += "ACTUAL_VALUE = " + Convert.ToInt32(actualValue.ToString());
            update += " WHERE BILL_NUMBER = '" + billNumber + "' AND ROW_NUMBER = '" + rowNumber + "'";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        private void load()
        {
            string sql = "SELECT [PKEY],[ROW_NUMBER],[MATERIEL_ID],[BILL_NUMBER],[CONTRACT_MATERIEL_NAME],[PRICE],[VALUE]";
            sql += ",[TRANSPORTATION_COST],[OTHER_COST] FROM [dbo].[PURCHASE_IN_ORDER_DETAILS] ORDER BY PKEY";

            m_tableDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    PurchaseInOrderDetailsTable record = new PurchaseInOrderDetailsTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.rowNumber = DbDataConvert.ToString(row["ROW_NUMBER"]);
                    record.materielID = DbDataConvert.ToInt32(row["MATERIEL_ID"]);
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);
                    record.contractMaterielName = DbDataConvert.ToString(row["CONTRACT_MATERIEL_NAME"]);

                    // 根据物料ID得到物料相关信息
                    MaterielTable materiel = Materiel.getInctance().getMaterielInfoFromPkey(record.materielID);
                    record.materielName = materiel.name;
                    record.materielModel = materiel.model;
                    record.parameter = materiel.materielParameter;
                    record.brand = materiel.brand;
                    record.materielUnitPurchase = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", materiel.unitPurchase);

                    record.price = DbDataConvert.ToDouble(row["PRICE"]);
                    record.value = DbDataConvert.ToDouble(row["VALUE"]);
                    record.sumMoney = record.price * record.value;
                    record.costApportionments = DbDataConvert.ToDouble(row["TRANSPORTATION_COST"]);
                    record.noCostApportionments = DbDataConvert.ToDouble(row["OTHER_COST"]);
                    record.totalMoney = record.sumMoney + record.costApportionments + record.noCostApportionments;

                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        public double getPurchaseValueFromBillNumber(string billNumber, int materielID)
        {
            double value = 0;
            SortedDictionary<int, PurchaseInOrderDetailsTable> list = new SortedDictionary<int, PurchaseInOrderDetailsTable>();

            foreach (KeyValuePair<int, PurchaseInOrderDetailsTable> index in m_tableDataList)
            {
                if (index.Value.billNumber == billNumber && index.Value.materielID == materielID)
                {
                    value += index.Value.value;
                }
            }

            return value;
        }

        public SortedDictionary<int, PurchaseInOrderDetailsTable> getAllPurchaseInOrderInfo()
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public SortedDictionary<int, PurchaseInOrderDetailsTable> getPurchaseInfoFromBillNumber(string billNumber)
        {
            SortedDictionary<int, PurchaseInOrderDetailsTable> list = new SortedDictionary<int, PurchaseInOrderDetailsTable>();

            foreach (KeyValuePair<int, PurchaseInOrderDetailsTable> index in m_tableDataList)
            {
                if (index.Value.billNumber == billNumber)
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public bool checkBillIsExist(string billNumber)
        {
            bool isRet = false;

            foreach (KeyValuePair<int, PurchaseInOrderDetailsTable> index in m_tableDataList)
            {
                PurchaseInOrderDetailsTable record = new PurchaseInOrderDetailsTable();

                if (index.Value.billNumber == billNumber)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }

        public SortedDictionary<int, PurchaseInOrderDetailsTable> getPurchaseInOrderCountInfo(string startDate, string endDate, bool isAllBill = false)
        {
            SortedDictionary<int, PurchaseInOrderDetailsTable> dataList = new SortedDictionary<int, PurchaseInOrderDetailsTable>();
            string querySQL = "SELECT [MATERIEL_ID], SUM([PRICE]*[VALUE]+[TRANSPORTATION_COST]+[OTHER_COST]) AS TOTAL_MONEY ";
            querySQL += "FROM [PURCHASE_IN_ORDER_DETAILS] WHERE BILL_NUMBER IN(";

            querySQL += "SELECT BILL_NUMBER FROM [dbo].[PURCHASE_IN_ORDER] WHERE ";
            querySQL += "TRADING_DATE >= '" + startDate + "' AND TRADING_DATE <= '" + endDate + "' AND IS_RED_BILL = 0 ";

            if (!isAllBill)
            {
                querySQL += " AND IS_REVIEW = 1";
            }

            querySQL += ") GROUP BY [MATERIEL_ID]";
            querySQL += " ORDER BY TOTAL_MONEY DESC";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, querySQL))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    PurchaseInOrderDetailsTable record = new PurchaseInOrderDetailsTable();

                    record.materielID = DbDataConvert.ToInt32(row["MATERIEL_ID"]);

                    MaterielTable materiel = Materiel.getInctance().getMaterielInfoFromPkey(record.materielID);
                    record.materielName = materiel.name;
                    record.materielModel = materiel.model;

                    record.totalMoney = DbDataConvert.ToDouble(row["TOTAL_MONEY"]);

                    dataList.Add(dataList.Count, record);
                }
            }
            return dataList;
        }

        public void refreshRecord()
        {
            load();
        }

        // 根据物料ID得到历史交易信息
        public SortedDictionary<int, PurchaseInOrderTable> getPurchasePriceCountInfo(int materielID, string startDate, string endDate)
        {
            string query = "SELECT A2.BILL_NUMBER, A2.SUPPLIER_ID, A3.[NAME], A2.TRADING_DATE, A1.[VALUE], A1.PRICE";
            query += " FROM PURCHASE_IN_ORDER_DETAILS A1, PURCHASE_IN_ORDER A2, BASE_SUPPLIER_LIST A3";
            query += " WHERE A1.MATERIEL_ID = " + materielID;
            query += " AND A1.BILL_NUMBER = A2.BILL_NUMBER AND A2.IS_REVIEW = 1 AND A2.SUPPLIER_ID = A3.PKEY";
            query += " AND A2.TRADING_DATE >= '" + startDate + "'";
            query += " AND A2.TRADING_DATE <= '" + endDate + "'";

            SortedDictionary<int, PurchaseInOrderTable> dataList = new SortedDictionary<int, PurchaseInOrderTable>();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    PurchaseInOrderTable record = new PurchaseInOrderTable();

                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);
                    record.supplierId = DbDataConvert.ToInt32(row["SUPPLIER_ID"]);
                    record.supplierName = DbDataConvert.ToString(row["NAME"]);
                    record.tradingDate = DbDataConvert.ToDateTime(row["TRADING_DATE"]).ToString("yyyy-MM-dd");

                    record.totalMoney = DbDataConvert.ToString(row["PRICE"]);
                    record.sumValue = DbDataConvert.ToString(row["VALUE"]);

                    dataList.Add(dataList.Count, record);
                }
            }

            return dataList;
        }
    }

    public class PurchaseInOrderDetailsTable
    {
        public int pkey { get; set; }
        public string rowNumber { get; set; }
        public string billNumber { get; set; }

        public int materielID { get; set; }
        public string materielName { get; set; }
        public string contractMaterielName { get; set; }
        public string materielModel { get; set; }
        public string parameter { get; set; }
        public string brand { get; set; }
        public string materielUnitPurchase { get; set; }

        public double price { get; set; }
        public double value { get; set; }
        public double sumMoney { get; set; }
        
        // 应计成本费用
        public double costApportionments { get; set; }

        // 非成本费用
        public double noCostApportionments { get; set; }
        public double totalMoney { get; set; }
    }
}