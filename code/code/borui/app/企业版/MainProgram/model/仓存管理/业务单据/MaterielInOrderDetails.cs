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
    public class MaterielInOrderDetails : ITableModel
    {
        private SortedDictionary<int, MaterielInOrderDetailsTable> m_tableDataList = new SortedDictionary<int, MaterielInOrderDetailsTable>();

        static private MaterielInOrderDetails m_instance = null;

        private MaterielInOrderDetails()
        {
            load();
        }

        static public MaterielInOrderDetails getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new MaterielInOrderDetails();
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
                        MaterielInOrderDetailsTable record = (MaterielInOrderDetailsTable)orderDetailsRecords[i];

                        if (i == 0 && checkBillIsExist(record.billNumber))
                        {
                            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
                            delete(record.billNumber);
                        }

                        string insert = "INSERT INTO [dbo].[WAREHOUSE_MANAGEMENT_IN_DETAILS]([ROW_NUMBER],[MATERIEL_ID],[BILL_NUMBER]";
                        insert += ",[PRICE],[VALUE],[MAKE_NUM],[NOTE])VALUES(";

                        insert += "'" + record.rowNumber + "',";
                        insert += record.materielID + ",";
                        insert += "'" + record.billNumber + "',";
                        insert += record.price + ",";
                        insert += record.value + ",";
                        insert += "'" + record.makeNum + "',";
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
            string delete = "DELETE FROM WAREHOUSE_MANAGEMENT_IN_DETAILS WHERE BILL_NUMBER = '" + billNumber + "'";

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

            string querySQL = "SELECT A1.[PKEY],A1.[ROW_NUMBER],A1.[MATERIEL_ID], A1.[BILL_NUMBER],A1.[PRICE],A1.[VALUE],A1.[NOTE],A1.[MAKE_NUM] ";
            querySQL += " FROM [WAREHOUSE_MANAGEMENT_IN_DETAILS] A1, WAREHOUSE_MANAGEMENT_IN A2";
            querySQL += " WHERE A1.BILL_NUMBER = A2.BILL_NUMBER";
            querySQL += " ORDER BY A1.[PKEY]";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, querySQL))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    MaterielInOrderDetailsTable record = new MaterielInOrderDetailsTable();

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
                    record.makeNum = DbDataConvert.ToString(row["MAKE_NUM"]);

                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        public SortedDictionary<int, MaterielInOrderDetailsTable> getAllMaterielInOrderInfo()
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public SortedDictionary<int, MaterielInOrderDetailsTable> getMaterielInInfoFromBillNumber(string billNumber)
        {
            SortedDictionary<int, MaterielInOrderDetailsTable> list = new SortedDictionary<int, MaterielInOrderDetailsTable>();

            foreach (KeyValuePair<int, MaterielInOrderDetailsTable> index in m_tableDataList)
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

            foreach (KeyValuePair<int, MaterielInOrderDetailsTable> index in m_tableDataList)
            {
                MaterielInOrderDetailsTable record = new MaterielInOrderDetailsTable();

                if (index.Value.billNumber == billNumber)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }

        public void refreshRecord()
        {
            load();
        }

        public SortedDictionary<int, MaterielInOrderDetailsTable> getMaterielInOrderCountInfo(string startDate, string endDate, bool isAllBill = false)
        {
            SortedDictionary<int, MaterielInOrderDetailsTable> dataList = new SortedDictionary<int, MaterielInOrderDetailsTable>();
            string querySQL = "SELECT A1.[PKEY],A1.[ROW_NUMBER],A1.[MATERIEL_ID], A1.[BILL_NUMBER],A1.[PRICE],A1.[VALUE],A1.[NOTE] ";
            querySQL += "FROM [WAREHOUSE_MANAGEMENT_IN_DETAILS] A1, WAREHOUSE_MANAGEMENT_IN A2";
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
                    MaterielInOrderDetailsTable record = new MaterielInOrderDetailsTable();

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
    }

    public class MaterielInOrderDetailsTable
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

        // 生产编号
        public string makeNum { get; set; }
    }
}