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
    public class MaterielOutOrderDetails : ITableModel
    {
        private SortedDictionary<int, MaterielOutOrderDetailsTable> m_tableDataList = new SortedDictionary<int, MaterielOutOrderDetailsTable>();

        static private MaterielOutOrderDetails m_instance = null;

        private MaterielOutOrderDetails()
        {
            load();
        }

        static public MaterielOutOrderDetails getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new MaterielOutOrderDetails();
            }

            return m_instance;
        }

        public void insert(ArrayList orderDetailsRecords)
        {
            if (orderDetailsRecords.Count <= 0)
            {
                return ;
            }
            else
            {
                try
                {
                    for (int i = 0; i < orderDetailsRecords.Count; i++)
                    {
                        MaterielOutOrderDetailsTable record = (MaterielOutOrderDetailsTable)orderDetailsRecords[i];

                        if (i == 0 && checkBillIsExist(record.billNumber))
                        {
                            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
                            delete(record.billNumber);
                        }

                        string insert = "INSERT INTO [dbo].[WAREHOUSE_MANAGEMENT_OUT_DETAILS]([ROW_NUMBER],[MATERIEL_ID],[BILL_NUMBER]";
                        insert += ",[PRICE],[VALUE],[NOTE])VALUES(";

                        insert += "'" + record.rowNumber + "',";
                        insert += record.materielID + ",";
                        insert += "'" + record.billNumber + "',";
                        insert += record.price + ",";
                        insert += record.value + ",";
                        insert += "'" + record.note + "'";
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
            string delete = "DELETE FROM WAREHOUSE_MANAGEMENT_OUT_DETAILS WHERE BILL_NUMBER = '" + billNumber + "'";

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

        private void load()
        {
            m_tableDataList.Clear();

            string querySQL = "SELECT A1.[PKEY],A1.[ROW_NUMBER],A1.[MATERIEL_ID], A1.[BILL_NUMBER],A1.[PRICE],A1.[VALUE],A1.[NOTE] ";
            querySQL += " FROM [WAREHOUSE_MANAGEMENT_OUT_DETAILS] A1, WAREHOUSE_MANAGEMENT_OUT A2";
            querySQL += " WHERE A1.BILL_NUMBER = A2.BILL_NUMBER";
            querySQL += " ORDER BY A1.[PKEY]";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, querySQL))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    MaterielOutOrderDetailsTable record = new MaterielOutOrderDetailsTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.rowNumber = DbDataConvert.ToString(row["ROW_NUMBER"]);
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);

                    record.materielID = DbDataConvert.ToInt32(row["MATERIEL_ID"]);

                    // 根据物料ID得到物料相关信息
                    MaterielTable materiel = Materiel.getInctance().getMaterielInfoFromPkey(record.materielID);
                    record.materielName = materiel.name;
                    record.materielModel = materiel.model;
                    record.materielUnitSale = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", materiel.unitSale);

                    record.price = DbDataConvert.ToDouble(row["PRICE"]);
                    record.value = DbDataConvert.ToDouble(row["VALUE"]);
                    record.sumMoney = record.price * record.value;

                    record.note = DbDataConvert.ToString(row["NOTE"]);

                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        public SortedDictionary<int, MaterielOutOrderDetailsTable> getAllMaterielOutOrderInfo()
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public SortedDictionary<int, MaterielOutOrderDetailsTable> getMaterielOutInfoFromBillNumber(string billNumber)
        {
            SortedDictionary<int, MaterielOutOrderDetailsTable> list = new SortedDictionary<int, MaterielOutOrderDetailsTable>();

            foreach (KeyValuePair<int, MaterielOutOrderDetailsTable> index in m_tableDataList)
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

            foreach (KeyValuePair<int, MaterielOutOrderDetailsTable> index in m_tableDataList)
            {
                MaterielOutOrderDetailsTable record = new MaterielOutOrderDetailsTable();

                if (index.Value.billNumber == billNumber)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }

        public SortedDictionary<int, MaterielOutOrderDetailsTable> getMaterielOutOrderCountInfo(string startDate, string endDate, bool isAllBill = false)
        {
            SortedDictionary<int, MaterielOutOrderDetailsTable> dataList = new SortedDictionary<int, MaterielOutOrderDetailsTable>();
            string querySQL = "SELECT A1.[PKEY],A1.[ROW_NUMBER],A1.[MATERIEL_ID], A1.[BILL_NUMBER],A1.[PRICE],A1.[VALUE],A1.[NOTE] ";
            querySQL += "FROM [WAREHOUSE_MANAGEMENT_OUT_DETAILS] A1, WAREHOUSE_MANAGEMENT_OUT A2";
            querySQL += " WHERE A1.BILL_NUMBER = A2.BILL_NUMBER";
            querySQL += " AND A2.TRADING_DATE >= '" + startDate + "' AND A2.TRADING_DATE <= '" + endDate + "' AND IS_RED_BILL = 0 ";

            if (!isAllBill)
            {
                querySQL += " AND A2.IS_REVIEW = 1";
            }

            querySQL += " ORDER BY A1.[PKEY]";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, querySQL))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    MaterielOutOrderDetailsTable record = new MaterielOutOrderDetailsTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.rowNumber = DbDataConvert.ToString(row["ROW_NUMBER"]);
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);

                    record.materielID = DbDataConvert.ToInt32(row["MATERIEL_ID"]);

                    // 根据物料ID得到物料相关信息
                    MaterielTable materiel = Materiel.getInctance().getMaterielInfoFromPkey(record.materielID);
                    record.materielName = materiel.name;
                    record.materielModel = materiel.model;
                    record.materielUnitSale = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", materiel.unitSale);

                    record.price = DbDataConvert.ToDouble(row["PRICE"]);
                    record.value = DbDataConvert.ToDouble(row["VALUE"]);
                    record.sumMoney = record.price * record.value;

                    record.billNumber = DbDataConvert.ToString(row["NOTE"]);

                    dataList.Add(dataList.Count, record);
                }
            }

            return dataList;
        }

        public void refreshRecord()
        {
            load();
        }

        // xx阶段xx产品领料金额统计
        public SortedDictionary<int, MaterielOutOrderDetailsTable> getMaterielOutOrderCountInfo2(string startDate, string endDate, bool isAllBill = false)
        {
            SortedDictionary<int, MaterielOutOrderDetailsTable> dataList = new SortedDictionary<int, MaterielOutOrderDetailsTable>();
            string querySQL = "SELECT [MATERIEL_ID], SUM([PRICE]*[VALUE]) AS TOTAL_MONEY ";
            querySQL += "FROM [WAREHOUSE_MANAGEMENT_OUT_DETAILS] WHERE BILL_NUMBER IN(";

            querySQL += "SELECT BILL_NUMBER FROM [dbo].[WAREHOUSE_MANAGEMENT_OUT] WHERE ";
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
                    MaterielOutOrderDetailsTable record = new MaterielOutOrderDetailsTable();

                    record.materielID = DbDataConvert.ToInt32(row["MATERIEL_ID"]);

                    MaterielTable materiel = Materiel.getInctance().getMaterielInfoFromPkey(record.materielID);
                    record.materielName = materiel.name;
                    record.materielModel = materiel.model;

                    record.sumMoney = DbDataConvert.ToDouble(row["TOTAL_MONEY"]);

                    dataList.Add(dataList.Count, record);
                }
            }

            return dataList;
        }
    }

    public class MaterielOutOrderDetailsTable
    {
        public int pkey { get; set; }
        public string rowNumber { get; set; }
        public string billNumber { get; set; }

        public int materielID { get; set; }
        public string materielName { get; set; }
        public string materielModel { get; set; }
        public string materielUnitSale { get; set; }

        public double price { get; set; }
        public double value { get; set; }
        public double sumMoney { get; set; }
        public string note { get; set; }
    }
}