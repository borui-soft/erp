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
    public class SaleOutOrderDetails : ITableModel
    {
        private SortedDictionary<int, SaleOutOrderDetailsTable> m_tableDataList = new SortedDictionary<int, SaleOutOrderDetailsTable>();

        static private SaleOutOrderDetails m_instance = null;

        private SaleOutOrderDetails()
        {
            load();
        }

        static public SaleOutOrderDetails getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new SaleOutOrderDetails();
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
                        SaleOutOrderDetailsTable record = (SaleOutOrderDetailsTable)purchaseOrderDetailsRecords[i];

                        if (i == 0 && checkBillIsExist(record.billNumber))
                        {
                            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
                            delete(record.billNumber);
                        }

                        string insert = "INSERT INTO [dbo].[SALE_OUT_ORDER_DETAILS]([ROW_NUMBER],[MATERIEL_ID],[BILL_NUMBER]";
                        insert += ",[PRICE],[VALUE],[TRANSPORTATION_COST],[OTHER_COST])VALUES(";

                        insert += "'" + record.rowNumber + "',";
                        insert += record.materielID + ",";
                        insert += "'" + record.billNumber + "',";
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
            string delete = "DELETE FROM SALE_OUT_ORDER_DETAILS WHERE BILL_NUMBER = '" + billNumber + "'";

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
            string update = "UPDATE [dbo].[SALE_OUT_ORDER_DETAILS] SET ";

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
            sql += ",[TRANSPORTATION_COST],[OTHER_COST] FROM [dbo].[SALE_OUT_ORDER_DETAILS] ORDER BY PKEY";

            m_tableDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    SaleOutOrderDetailsTable record = new SaleOutOrderDetailsTable();

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
                    record.costApportionments = DbDataConvert.ToDouble(row["TRANSPORTATION_COST"]);
                    record.noCostApportionments = DbDataConvert.ToDouble(row["OTHER_COST"]);
                    record.totalMoney = record.sumMoney + record.costApportionments + record.noCostApportionments;

                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        public SortedDictionary<int, SaleOutOrderDetailsTable> getAllSaleOutOrderInfo()
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public SortedDictionary<int, SaleOutOrderDetailsTable> getSaleOutInfoFromBillNumber(string billNumber)
        {
            SortedDictionary<int, SaleOutOrderDetailsTable> list = new SortedDictionary<int, SaleOutOrderDetailsTable>();

            foreach (KeyValuePair<int, SaleOutOrderDetailsTable> index in m_tableDataList)
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

            foreach (KeyValuePair<int, SaleOutOrderDetailsTable> index in m_tableDataList)
            {
                SaleOutOrderDetailsTable record = new SaleOutOrderDetailsTable();

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

        // xx阶段xx产品销售金额统计
        public SortedDictionary<int, SaleOutOrderDetailsTable> getSaleOutOrderCountInfo(string startDate, string endDate, bool isAllBill = false)
        {
            SortedDictionary<int, SaleOutOrderDetailsTable> dataList = new SortedDictionary<int, SaleOutOrderDetailsTable>();
            string querySQL = "SELECT [MATERIEL_ID], SUM([PRICE]*[VALUE]+[TRANSPORTATION_COST]+[OTHER_COST]) AS TOTAL_MONEY ";
            querySQL += "FROM [SALE_OUT_ORDER_DETAILS] WHERE BILL_NUMBER IN(";

            querySQL += "SELECT BILL_NUMBER FROM [dbo].[SALE_OUT_ORDER] WHERE ";
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
                    SaleOutOrderDetailsTable record = new SaleOutOrderDetailsTable();

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

        // xx阶段xx产品，销售成本，毛利润汇总信息(存货核算->销售成本汇总和销售毛利润汇总报表用)
        public SortedDictionary<int, SaleProfitCounInfoStruct> getSaleOutOrderCountInfo2(string startDate, string endDate, bool isAllBill = false)
        {
            SortedDictionary<int, SaleProfitCounInfoStruct> dataList = new SortedDictionary<int, SaleProfitCounInfoStruct>();
            string querySQL = "SELECT [MATERIEL_ID], SUM([PRICE]*[VALUE]+[TRANSPORTATION_COST]+[OTHER_COST]) AS TOTAL_MONEY ";
            querySQL += "FROM [SALE_OUT_ORDER_DETAILS] WHERE BILL_NUMBER IN(";

            querySQL += "SELECT BILL_NUMBER FROM [dbo].[SALE_OUT_ORDER] WHERE ";
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
                    SaleProfitCounInfoStruct record = new SaleProfitCounInfoStruct();

                    record.materielID = DbDataConvert.ToInt32(row["MATERIEL_ID"]);

                    MaterielTable materiel = Materiel.getInctance().getMaterielInfoFromPkey(record.materielID);
                    record.materielName = materiel.name;
                    record.materielUnitSale = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", materiel.unitSale);

                    record.saleSum = DbDataConvert.ToDouble(row["TOTAL_MONEY"]);

                    // 得到xx阶段xx产品总销量
                    record.valueSum = getSumValueFromStage(record.materielID, startDate, endDate, isAllBill);

                    // 总成本 = 总交易数量 * 库存加权单价
                    InitMaterielTable temp = InitMateriel.getInctance().getMaterielInfoFromMaterielID(record.materielID);
                    double priceCost = temp.price; 
                    record.costSum = record.valueSum * priceCost;

                    // 总毛利润
                    record.profitSum = record.saleSum - record.costSum;

                    dataList.Add(dataList.Count, record);
                }
            }
            return dataList;
        }

        // xx阶段xx产品，销售成本，毛利润明细信息(存货核算->销售成本明细和销售毛利润明细报表用)
        public SortedDictionary<int, SaleProfitCounInfoStruct> getSaleOutOrderDetailsInfo(string startDate, string endDate, bool isAllBill = false)
        {
            SortedDictionary<int, SaleProfitCounInfoStruct> dataList = new SortedDictionary<int, SaleProfitCounInfoStruct>();
            string querySQL = "SELECT [PKEY], [MATERIEL_ID], [BILL_NUMBER], [PRICE], [VALUE], [PRICE]*[VALUE]+[TRANSPORTATION_COST]+[OTHER_COST] AS TOTAL_SALE_MONEY ";
            querySQL += "FROM [SALE_OUT_ORDER_DETAILS] WHERE BILL_NUMBER IN(";

            querySQL += "SELECT BILL_NUMBER FROM [dbo].[SALE_OUT_ORDER] WHERE ";
            querySQL += "TRADING_DATE >= '" + startDate + "' AND TRADING_DATE <= '" + endDate + "' AND IS_RED_BILL = 0 ";

            if (!isAllBill)
            {
                querySQL += " AND IS_REVIEW = 1";
            }

            querySQL += ") ORDER BY PKEY";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, querySQL))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    SaleProfitCounInfoStruct record = new SaleProfitCounInfoStruct();

                    record.materielID = DbDataConvert.ToInt32(row["MATERIEL_ID"]);
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);

                    MaterielTable materiel = Materiel.getInctance().getMaterielInfoFromPkey(record.materielID);
                    record.materielName = materiel.name;
                    record.materielUnitSale = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", materiel.unitSale);

                    record.priceSale = DbDataConvert.ToDouble(row["PRICE"]);
                    record.value = DbDataConvert.ToDouble(row["VALUE"]);
                    record.saleSum = DbDataConvert.ToDouble(row["TOTAL_SALE_MONEY"]);



                    // 成本价 = 交易数量 * 库存加权单价
                    InitMaterielTable temp = InitMateriel.getInctance().getMaterielInfoFromMaterielID(record.materielID);
                    record.priceCost = temp.price;
                    record.costSum = record.value * record.priceCost;

                    // 总毛利润
                    record.profitSum = record.saleSum - record.costSum;

                    dataList.Add(dataList.Count, record);
                }
            }
            return dataList;
        }

        // xx阶段xx产品销售总数量
        public double getSumValueFromStage(int matetielID, string startDate, string endDate, bool isAllBill = false)
        {
            string querySQL = "SELECT SUM([VALUE]) AS TOTAL_VALUE ";
            querySQL += "FROM [SALE_OUT_ORDER_DETAILS] WHERE [MATERIEL_ID] = " + matetielID + "AND BILL_NUMBER IN(";

            querySQL += "SELECT BILL_NUMBER FROM [dbo].[SALE_OUT_ORDER] WHERE ";
            querySQL += "TRADING_DATE >= '" + startDate + "' AND TRADING_DATE <= '" + endDate + "' AND IS_RED_BILL = 0";

            if (!isAllBill)
            {
                querySQL += " AND IS_REVIEW = 1";
            }

            querySQL += ") GROUP BY [MATERIEL_ID]";

            DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, querySQL);
            double sumValue = DbDataConvert.ToDouble(dataTable.Rows[0][0]);

            return sumValue;
        }

        // 根据物料ID得到历史交易信息
        public SortedDictionary<int, SaleOutOrderTable> getSalePriceCountInfo(int materielID, string startDate, string endDate)
        {
            string query = "SELECT A2.BILL_NUMBER, A2.CUSTOMER_ID, A3.[NAME], A2.TRADING_DATE, A1.[VALUE], A1.PRICE";
            query += " FROM SALE_OUT_ORDER_DETAILS A1, SALE_OUT_ORDER A2, BASE_SUPPLIER_LIST A3";
            query += " WHERE A1.MATERIEL_ID = " + materielID;
            query += " AND A1.BILL_NUMBER = A2.BILL_NUMBER AND A2.IS_REVIEW = 1 AND A2.CUSTOMER_ID = A3.PKEY";
            query += " AND A2.TRADING_DATE >= '" + startDate + "'";
            query += " AND A2.TRADING_DATE <= '" + endDate + "'";

            SortedDictionary<int, SaleOutOrderTable> dataList = new SortedDictionary<int, SaleOutOrderTable>();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, query))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    SaleOutOrderTable record = new SaleOutOrderTable();

                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);
                    record.customerId = DbDataConvert.ToInt32(row["CUSTOMER_ID"]);
                    record.customerName = DbDataConvert.ToString(row["NAME"]);
                    record.tradingDate = DbDataConvert.ToDateTime(row["TRADING_DATE"]).ToString("yyyy-MM-dd");

                    record.totalMoney = DbDataConvert.ToString(row["PRICE"]);
                    record.sumValue = DbDataConvert.ToString(row["VALUE"]);

                    dataList.Add(dataList.Count, record);
                }
            }

            return dataList;
        }
    }

    public class SaleOutOrderDetailsTable
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

        // 应计成本费用
        public double costApportionments { get; set; }

        // 非成本费用
        public double noCostApportionments { get; set; }
        public double totalMoney { get; set; }
    }

    public class SaleProfitCounInfoStruct
    {
        public int materielID { get; set; }
        public string materielName { get; set; }
        public string materielUnitSale { get; set; }
        public string billNumber { get; set; }
        
        // 销售利润明细账相关字段
        public double priceCost { get; set; }
        public double priceSale { get; set; }
        public double value { get; set; }

        // 销售利润统计信息相关
        public double costSum { get; set; }
        public double saleSum { get; set; }
        public double valueSum { get; set; }
        public double profitSum { get; set; }
    }
}