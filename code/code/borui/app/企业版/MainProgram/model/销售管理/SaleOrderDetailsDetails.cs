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
    public class SaleOrderDetails : ITableModel
    {
        private SortedDictionary<int, SaleOrderDetailsTable> m_tableDataList = new SortedDictionary<int, SaleOrderDetailsTable>();

        static private SaleOrderDetails m_instance = null;

        private SaleOrderDetails()
        {
            load();
        }

        static public SaleOrderDetails getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new SaleOrderDetails();
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
                        SaleOrderDetailsTable record = (SaleOrderDetailsTable)purchaseOrderDetailsRecords[i];

                        if (i == 0 && checkBillIsExist(record.billNumber))
                        {
                            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
                            delete(record.billNumber);
                        }

                        string insert = "INSERT INTO [ERP].[dbo].[SALE_ORDER_DETAILS]([ROW_NUMBER],[MATERIEL_ID],[BILL_NUMBER]";
                        insert += ",[PRICE],[VALUE],[TRANSPORTATION_COST],[OTHER_COST])VALUES(";

                        insert += "'" + record.rowNumber + "',";
                        insert += record.materielID + ",";
                        insert += "'" + record.billNumber + "',";
                        insert += record.price + ",";
                        insert += record.value + ",";
                        insert += record.transportationCost + ",";
                        insert += record.otherCost;
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
            string delete = "DELETE FROM SALE_ORDER_DETAILS WHERE BILL_NUMBER = '" + billNumber + "'";

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

        public void updateOutStorageValue(string billNumber, string rowNumber, string actualValue)
        {
            string update = "UPDATE [dbo].[SALE_ORDER_DETAILS] SET ";

            update += "IS_OUT_STORAGE = 1, ";
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
            string sql = "SELECT [PKEY],[ROW_NUMBER],[MATERIEL_ID],[BILL_NUMBER],[PRICE],[VALUE]";
            sql += ",[TRANSPORTATION_COST],[OTHER_COST] FROM [dbo].[SALE_ORDER_DETAILS] ORDER BY PKEY";

            m_tableDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    SaleOrderDetailsTable record = new SaleOrderDetailsTable();

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
                    record.transportationCost = DbDataConvert.ToDouble(row["TRANSPORTATION_COST"]);
                    record.otherCost = DbDataConvert.ToDouble(row["OTHER_COST"]);
                    record.totalMoney = record.sumMoney + record.transportationCost + record.otherCost;

                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        public SortedDictionary<int, SaleOrderDetailsTable> getAllSaleOrderInfo()
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public SortedDictionary<int, SaleOrderDetailsTable> getSaleInfoFromBillNumber(string billNumber)
        {
            SortedDictionary<int, SaleOrderDetailsTable> list = new SortedDictionary<int, SaleOrderDetailsTable>();

            foreach (KeyValuePair<int, SaleOrderDetailsTable> index in m_tableDataList)
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

            foreach (KeyValuePair<int, SaleOrderDetailsTable> index in m_tableDataList)
            {
                SaleOrderDetailsTable record = new SaleOrderDetailsTable();

                if (index.Value.billNumber == billNumber)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }
    }

    public class SaleOrderDetailsTable
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
        public double transportationCost { get; set; }
        public double otherCost { get; set; }
        public double totalMoney { get; set; }
    }
}