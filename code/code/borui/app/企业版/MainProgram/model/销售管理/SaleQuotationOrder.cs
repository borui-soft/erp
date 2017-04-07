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
    public class SaleQuotationOrder : ITableModel
    {
        static private SaleQuotationOrder m_instance = null;
        private SortedDictionary<int, SaleQuotationOrderTable> m_tableDataList = new SortedDictionary<int, SaleQuotationOrderTable>();

        private SaleQuotationOrder()
        {
        }

        static public SaleQuotationOrder getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new SaleQuotationOrder();
            }

            return m_instance;
        }

        public void refreshRecord()
        {
            load();
        }

        private void load()
        {
            string sql = "SELECT [PKEY],[CUSTOMER_NAME],[DATE] ,[BILL_NUMBER],[CONTENT],[NOTE],[SALEMAN_NAME],";
            sql += "[MAKE_ORDER_NAME],[CONTACT],[TEL] FROM [dbo].[SALE_QUOTATION_ORDER] ORDER BY PKEY";

            m_tableDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    SaleQuotationOrderTable record = new SaleQuotationOrderTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.customerName = DbDataConvert.ToString(row["CUSTOMER_NAME"]);
                    record.date = DbDataConvert.ToDateTime(row["DATE"]).ToString("yyyy-MM-dd");
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);
                    record.content = DbDataConvert.ToString(row["CONTENT"]);
                    record.note = DbDataConvert.ToString(row["NOTE"]);
                    record.salemanName = DbDataConvert.ToString(row["SALEMAN_NAME"]);
                    record.makeOrderName = DbDataConvert.ToString(row["MAKE_ORDER_NAME"]);
                    record.contact = DbDataConvert.ToString(row["CONTACT"]);
                    record.tel = DbDataConvert.ToString(row["TEL"]);

                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        public void insert(SaleQuotationOrderTable record)
        {
            string insert = "INSERT INTO [dbo].[SALE_QUOTATION_ORDER]([CUSTOMER_NAME],[BILL_NUMBER],[CONTENT]";
            insert += ",[NOTE],[SALEMAN_NAME],[MAKE_ORDER_NAME],[CONTACT],[TEL])VALUES (";

            insert += "'" + record.customerName + "',";
            insert += "'" + record.billNumber + "',";
            insert += "'" + record.content + "',";
            insert += "'" + record.note + "',";
            insert += "'" + record.salemanName + "',";
            insert += "'" + record.makeOrderName + "',";
            insert += "'" + record.contact + "',";
            insert += "'" + record.tel + "'";
            insert += ")";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);

                MessageBoxExtend.messageOK("数据保存成功");

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }

            writeOperatorLog(201, OperatorLogType.Add, record.billNumber);
        }

        public void delete(int pkey)
        {
            deleteRecord("SALE_QUOTATION_ORDER", pkey);
            load();
        }

        public void update(string billNumber, SaleQuotationOrderTable record)
        {
            string update = "UPDATE [dbo].[SALE_QUOTATION_ORDER] SET ";

            update += "[CUSTOMER_NAME] = '" + record.customerName + "',";
            update += "[DATE] = '" + record.date + "',";
            update += "[BILL_NUMBER] = '" + record.billNumber + "',";
            update += "[CONTENT] = '" + record.content + "',";
            update += "[NOTE] = '" + record.note + "',";
            update += "[SALEMAN_NAME] = '" + record.salemanName + "',";
            update += "[MAKE_ORDER_NAME] = '" + record.makeOrderName + "',";
            update += "[CONTACT] = '" + record.contact + "',";
            update += "[TEL] = '" + record.tel + "' ";
            update += " WHERE BILL_NUMBER = '" + billNumber + "'";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                MessageBoxExtend.messageOK("数据修改成功");
                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public SortedDictionary<int, SaleQuotationOrderTable> getAllSaleQuotationOrderInfo()
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public SaleQuotationOrderTable getSaleQuotationOrderInfoFromBillNumber(string billNumber)
        {
            SaleQuotationOrderTable record = new SaleQuotationOrderTable();

            foreach (KeyValuePair<int, SaleQuotationOrderTable> index in m_tableDataList)
            {
                if (index.Value.billNumber == billNumber)
                {
                    record = index.Value;
                    break;
                }
            }

            return record;
        }
    }

    public class SaleQuotationOrderTable
    {
        public int pkey { get; set; }
        public string customerName { get; set; }
        public string date { get; set; }
        public string billNumber { get; set; }
        public string content { get; set; }
        public string note { get; set; }
        public string salemanName { get; set; }
        public string makeOrderName { get; set; }
        public string contact { get; set; }
        public string tel { get; set; }
    }
}