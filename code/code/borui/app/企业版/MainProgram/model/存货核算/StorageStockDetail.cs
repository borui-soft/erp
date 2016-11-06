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
    public class StorageStockDetail : ITableModel
    {
        private SortedDictionary<int, StorageStockDetailTable> m_tableDataList = new SortedDictionary<int, StorageStockDetailTable>();

        static private StorageStockDetail m_instance = null;

        private StorageStockDetail()
        {
        }

        static public StorageStockDetail getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new StorageStockDetail();
            }

            return m_instance;
        }

        public void insert(StorageStockDetailTable record)
        {
            string insert = "INSERT INTO [dbo].[STORAGE_STOCK_DETAIL]([MATERIEL_ID],[TRADING_DATE],[BILL_NUMBER],[THINGS_TYPE]";
            insert += ",[IS_IN],[VALUE],[PRICE],[STORAGE_VALUE],[STORAGE_PRICE]) VALUES(";

            insert += record.materielID + ",";
            insert += "'" + record.tradingDate + "',";
            insert += "'" + record.billNumber + "',";
            insert += "'" + record.thingsType + "',";
            insert += record.isIn + ",";
            insert += record.value + ",";
            insert += record.price + ",";
            insert += record.storageValue + ",";
            insert += record.storagePrice;

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

        private void load(string sql)
        {
            m_tableDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    StorageStockDetailTable record = new StorageStockDetailTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.materielID = DbDataConvert.ToInt32(row["MATERIEL_ID"]);
                    MaterielTable materiel = Materiel.getInctance().getMaterielInfoFromPkey(record.materielID);
                    record.materielName = materiel.name;
                    record.model = materiel.model;

                    record.tradingDate = DbDataConvert.ToDateTime(row["TRADING_DATE"]).ToString("yyyy-MM-dd");
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);

                    record.thingsType = DbDataConvert.ToString(row["THINGS_TYPE"]);
                    record.isIn = DbDataConvert.ToInt32(row["IS_IN"]);

                    record.value = DbDataConvert.ToDouble(row["VALUE"]);
                    record.price = DbDataConvert.ToDouble(row["PRICE"]);
                    record.sumMoney = record.value * record.price;

                    record.storageValue = DbDataConvert.ToDouble(row["STORAGE_VALUE"]);
                    record.storagePrice = DbDataConvert.ToDouble(row["STORAGE_PRICE"]);
                    record.storageMoney = record.storageValue * record.storagePrice;

                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        public void refreshRecord()
        {
            //load();
        }

        public SortedDictionary<int, StorageStockDetailTable> getAllStorageStockDetailInfo(string startDate = "", string endDate = "")
        {
            string sql = "SELECT [PKEY],[MATERIEL_ID],[TRADING_DATE],[BILL_NUMBER],[THINGS_TYPE],[IS_IN],[VALUE],[PRICE],";
            sql += "[STORAGE_VALUE],[STORAGE_PRICE] FROM [dbo].[STORAGE_STOCK_DETAIL]";

            if (startDate.Length > 0 && endDate.Length > 0)
            {
                 sql += "WHERE TRADING_DATE >= '" + startDate + "' AND TRADING_DATE <='" + endDate + "'";
            }

            sql += " ORDER BY PKEY";

            load(sql);

            return m_tableDataList;
        }

        public SortedDictionary<int, StorageStockDetailTable> getMaterielStorageStockDetailInfo(int materielID, string startDate = "", string endDate = "")
        {
            string sql = "SELECT [PKEY],[MATERIEL_ID],[TRADING_DATE],[BILL_NUMBER],[THINGS_TYPE],[IS_IN],[VALUE],[PRICE],";
            sql += "[STORAGE_VALUE],[STORAGE_PRICE] FROM [dbo].[STORAGE_STOCK_DETAIL] WHERE MATERIEL_ID = " + materielID;

            if (startDate.Length > 0 && endDate.Length > 0)
            {
                sql += " AND TRADING_DATE >= '" + startDate + "' AND TRADING_DATE <='" + endDate + "'";
            }

            sql += " ORDER BY PKEY";

            load(sql);

            return m_tableDataList;
        }

        public SortedDictionary<int, StorageStockDetailTable> getMaterielStorageStockDetailHistoryInfo(string endDate)
        {
            SortedDictionary<int, StorageStockDetailTable> tableDataList = new SortedDictionary<int, StorageStockDetailTable>();

            string sql = "SELECT [PKEY],[MATERIEL_ID],[TRADING_DATE],[STORAGE_VALUE],[STORAGE_PRICE] FROM [dbo].[STORAGE_STOCK_DETAIL] WHERE TRADING_DATE <= '";
            sql += endDate + "' ORDER BY PKEY DESC";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    StorageStockDetailTable record = new StorageStockDetailTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.materielID = DbDataConvert.ToInt32(row["MATERIEL_ID"]);
                    record.tradingDate = DbDataConvert.ToDateTime(row["TRADING_DATE"]).ToString("yyyy-MM-dd");
                    record.storageValue = DbDataConvert.ToDouble(row["STORAGE_VALUE"]);
                    record.storagePrice = DbDataConvert.ToDouble(row["STORAGE_PRICE"]);

                    tableDataList.Add(tableDataList.Count, record);
                }
            }

            return tableDataList;
        }

        // 得到指定日期之前的最后一条记录的结存相关信息。被用于存货核算界面存货明细账界面，得到xx物料期初余额
        public StorageStockDetailTable getMaterielStorageStockDetailInfo(int materielID, string startDate)
        {
            StorageStockDetailTable record = new StorageStockDetailTable();

            string sql = "SELECT [PKEY],[MATERIEL_ID],[TRADING_DATE],[BILL_NUMBER],[THINGS_TYPE],[IS_IN],[VALUE],[PRICE],";
            sql += "[STORAGE_VALUE],[STORAGE_PRICE] FROM [dbo].[STORAGE_STOCK_DETAIL] WHERE MATERIEL_ID = " + materielID;
            sql += " AND TRADING_DATE < '" + startDate + "' ORDER BY PKEY DESC";

            load(sql);
            
            if (m_tableDataList.Count > 0)
            {
                record = m_tableDataList[0];
            }
            //else
            //{
            //    // 此处需要修改代码，当在STORAGE_STOCK_DETAIL没查相应物料的记录时，需要根据物料ID，去库存表中查到相应的记录
            //    InitMaterielTable data = InitMateriel.getInctance().getMaterielInfoFromPkey(materielID);
            //    record.storageValue = data.value;
            //    record.storagePrice = data.price;
            //    record.storageMoney = data.value * data.price;
            //}

            return record;
        }

        public ArrayList getMaterielAgeValue(int materielID, double currentStorageValue)
        {
            ArrayList list = new ArrayList();

            string value1 = "0", value31 = "0", value61 = "0";
            double value91 = 0;

            string query1 = "SELECT SUM(VALUE) AS SUM_VALUE FROM STORAGE_STOCK_DETAIL WHERE THINGS_TYPE = '采购入库' AND MATERIEL_ID = " + Convert.ToString(materielID);
            query1 += " AND (GETDATE()- TRADING_DATE >= 0 AND GETDATE()- TRADING_DATE <= 30)";

            string query2 = "SELECT SUM(VALUE) AS SUM_VALUE FROM STORAGE_STOCK_DETAIL WHERE THINGS_TYPE = '采购入库' AND MATERIEL_ID = " + Convert.ToString(materielID);
            query2 += " AND (GETDATE()- TRADING_DATE >= 31 AND GETDATE()- TRADING_DATE <= 60)";

            string query3 = "SELECT SUM(VALUE) AS SUM_VALUE FROM STORAGE_STOCK_DETAIL WHERE THINGS_TYPE = '采购入库' AND MATERIEL_ID = " + Convert.ToString(materielID);
            query3 += " AND (GETDATE()- TRADING_DATE >= 61 AND GETDATE()- TRADING_DATE <= 90)";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query1))
            {
                if (dataTable.Rows.Count > 0)
                {
                    value1 = DbDataConvert.ToString(dataTable.Rows[0][0]);
                }
            }

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query2))
            {
                if (dataTable.Rows.Count > 0)
                {
                    value31 = DbDataConvert.ToString(dataTable.Rows[0][0]);
                }
            }

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query3))
            {
                if (dataTable.Rows.Count > 0)
                {
                    value61 = DbDataConvert.ToString(dataTable.Rows[0][0]);
                }
            }

            if (value1.Length == 0)
            {
                value1 = "0";
            }

            if (value31.Length == 0)
            {
                value31 = "0";
            }

            if (value61.Length == 0)
            {
                value61 = "0";
            }


            value91 = currentStorageValue - Convert.ToDouble(value1.ToString()) - Convert.ToDouble(value31.ToString()) - Convert.ToDouble(value61.ToString());

            list.Add(value1);
            list.Add(value31);
            list.Add(value61);
            list.Add(value91);

            return list;
        }
    }

    public class StorageStockDetailTable
    {
        public int pkey { get; set; }

        public int materielID { get; set; }
        public string materielName { get; set; }
        public string model { get; set; }

        public string tradingDate { get; set; }
        public string billNumber { get; set; }

        // 事物类型
        public string thingsType { get; set; }
        public int isIn { get; set; }

        // 本次交易数量和单价
        public double price { get; set; }
        public double value { get; set; }
        public double sumMoney { get; set; }

        // 库存数量和单价
        public double storagePrice { get; set; }
        public double storageValue { get; set; }
        public double storageMoney { get; set; }
    }
}