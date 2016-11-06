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
    public class SupplierPriceSheet : ITableModel
    {
        static private SupplierPriceSheet m_instance = null;

        private SupplierPriceSheet()
        {
        }

        static public SupplierPriceSheet getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new SupplierPriceSheet();
            }

            return m_instance;
        }

        public void insert(SupplierPriceSheetTable record)
        {
            string insert = "INSERT INTO [dbo].[PURCHASE_SUPPLIER_PRICE_SHEET]([SUPPLIER_ID],[MATERIEL_ID],[ORNM_FROM_VALUE],";
            insert += "[ORNM_TO_VALUE],[PRICE],[NOTE]) VALUES (";

            insert += record.supplierId+ ",";
            insert += record.matetielId + ",";
            insert += "'" + record.ORNMFromValue + "',";
            insert += "'" + record.ORNMToValue + "',";
            insert += record.pirce + ",";
            insert += "'" + record.note + "'";
            insert += ")";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);

                MessageBoxExtend.messageOK("数据保存成功");
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public void delete(int pkey)
        {
            deleteRecord("PURCHASE_SUPPLIER_PRICE_SHEET", pkey);
        }

        public void update(int pkey, SupplierPriceSheetTable record)
        {
            string update = "UPDATE [dbo].[PURCHASE_SUPPLIER_PRICE_SHEET] SET ";

            update += "[SUPPLIER_ID] = " + record.supplierId+ ",";
            update += "[MATERIEL_ID] = " + record.matetielId + ",";
            update += "[ORNM_FROM_VALUE] = '" + record.ORNMFromValue + "',";
            update += "[ORNM_TO_VALUE] = '" + record.ORNMToValue + "',";
            update += "[PRICE] = " + record.pirce + ",";
            update += "[NOTE] = '" + record.note + "'";
            update += " WHERE PKEY = " + Convert.ToString(pkey);

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                MessageBoxExtend.messageOK("数据修改成功");
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public SortedDictionary<int, SupplierPriceSheetTable> getAllSupplierPriceSheetInfo(int matetielID)
        {
            string sql = "SELECT [PKEY],[SUPPLIER_ID],[MATERIEL_ID] ,[ORNM_FROM_VALUE],[ORNM_TO_VALUE],[PRICE],[DATE],[NOTE] ";
            sql += "FROM [dbo].[PURCHASE_SUPPLIER_PRICE_SHEET]  WHERE MATERIEL_ID = " +  matetielID + " ORDER BY PKEY";

            SortedDictionary<int, SupplierPriceSheetTable> tableDataList = new SortedDictionary<int, SupplierPriceSheetTable>();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    SupplierPriceSheetTable record = new SupplierPriceSheetTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.supplierId = DbDataConvert.ToInt32(row["SUPPLIER_ID"]);

                    if (record.supplierId != -1)
                    {
                        SupplierTable supplier = Supplier.getInctance().getSupplierInfoFromPkey(record.supplierId);
                        record.supplierName = supplier.name;
                        record.contact = supplier.contact;
                        record.tel = supplier.tel;
                    }
                    record.matetielId = DbDataConvert.ToInt32(row["MATERIEL_ID"]);
                    record.ORNMFromValue = DbDataConvert.ToString(row["ORNM_FROM_VALUE"]);
                    record.ORNMToValue = DbDataConvert.ToString(row["ORNM_TO_VALUE"]);
                    record.pirce = DbDataConvert.ToString(row["PRICE"]);
                    record.date = DbDataConvert.ToDateTime(row["DATE"]).ToString("yyyy-MM-dd");
                    record.note = DbDataConvert.ToString(row["NOTE"]);

                    tableDataList.Add(tableDataList.Count, record);
                }
            }

            return tableDataList;
        }

        public SupplierPriceSheetTable getSupplierPriceSheetInfoFromPkey(int pkey)
        {
            string sql = "SELECT A1.PKEY, A1.SUPPLIER_ID, A2.[NAME], A1.MATERIEL_ID, A1.ORNM_FROM_VALUE, A1.ORNM_TO_VALUE, ";
            sql += "A1.PRICE, A1.DATE, A2.CONTACT, A2.TEL, A1.NOTE FROM [dbo].[PURCHASE_SUPPLIER_PRICE_SHEET] A1, ";
            sql += "[BASE_SUPPLIER_LIST] A2 WHERE A1.SUPPLIER_ID = A2.PKEY AND  A1.PKEY = " + pkey;

            SupplierPriceSheetTable record = new SupplierPriceSheetTable();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.supplierId = DbDataConvert.ToInt32(row["SUPPLIER_ID"]);
                    record.supplierName = DbDataConvert.ToString(row["NAME"]);
                    record.matetielId = DbDataConvert.ToInt32(row["MATERIEL_ID"]);
                    record.ORNMFromValue = DbDataConvert.ToString(row["ORNM_FROM_VALUE"]);
                    record.ORNMToValue = DbDataConvert.ToString(row["ORNM_TO_VALUE"]);
                    record.pirce = DbDataConvert.ToString(row["PRICE"]);
                    record.date = DbDataConvert.ToDateTime(row["DATE"]).ToString("yyyy-MM-dd");
                    record.contact = DbDataConvert.ToString(row["CONTACT"]);
                    record.tel = DbDataConvert.ToString(row["TEL"]);
                    record.note = DbDataConvert.ToString(row["NOTE"]);
                }
            }

            return record;
        }
    }

    public class SupplierPriceSheetTable
    {
        public int pkey { get; set; }
        public int supplierId { get; set; }
        public int matetielId { get; set; }
        public string date { get; set; }
        public string supplierName { get; set; }
        public string ORNMFromValue { get; set; }
        public string ORNMToValue { get; set; }
        public string pirce { get; set; }
        public string contact { get; set; }
        public string tel { get; set; }
        public string note { get; set; }
    }
}