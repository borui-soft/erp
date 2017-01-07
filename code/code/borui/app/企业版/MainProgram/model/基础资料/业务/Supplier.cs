using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using TIV.Core.DatabaseAccess;
using MainProgram.bus;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class Supplier : ITableModel
    {
        private SortedDictionary<int, SupplierTable> m_noForbidSupplierList = new SortedDictionary<int, SupplierTable>();
        private SortedDictionary<int, SupplierTable> m_forbidSupplierList = new SortedDictionary<int, SupplierTable>();

        static private Supplier m_instance = null;

        private Supplier()
        {
            load();
        }

        static public Supplier getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new Supplier();
            }

            return m_instance;
        }

        public void insert(SupplierTable supplier)
        {
            string insert = "INSERT INTO [dbo].[BASE_SUPPLIER_LIST] ([SUPPLIER_TYPE],[NAME],[NAME_SHORT],[MNEMONIC_CODE],[AREA],";
            insert += "[CREDIT],[VAT_RATE],[CONTACT],[TEL],[FAX],[MOBILE_PHONE],[E-MAIL],[HOME_PAGE],[COMPANY_ADDRESS],[ZIP_CODE],";
            insert += "[BANK_NAME],[BANK_ACCOUNT],[TAX_ACCOUNT],[NOTE],[IS_FORBID]) VALUES(";

            insert += supplier.supplierType + ",";
            insert += "'" + supplier.name + "',";
            insert += "'" + supplier.nameShort + "',";
            insert += "'" + supplier.mnemonicCode + "',";
            insert += "'" + supplier.area + "',";
            insert += supplier.credit + ",";
            insert += supplier.varRate + ",";
            insert += "'" + supplier.contact + "',";
            insert += "'" + supplier.tel + "',";
            insert += "'" + supplier.fax + "',";
            insert += "'" + supplier.mobilePhone + "',";
            insert += "'" + supplier.email + "',";
            insert += "'" + supplier.homePage + "',";
            insert += "'" + supplier.address + "',";
            insert += "'" + supplier.zipCode + "',";
            insert += "'" + supplier.bankName + "',";
            insert += "'" + supplier.bankAccount + "',";
            insert += "'" + supplier.taxAccount + "',";
            insert += "'" + supplier.note + "', 0";
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
        }

        public void delete(int pkey)
        {
            if (deleteRecord("BASE_SUPPLIER_LIST", pkey))
            {
                load();
            }
        }

        public void update(int pkey, SupplierTable supplier)
        {
            string update = "UPDATE [dbo].[BASE_SUPPLIER_LIST] SET ";

            update += "[SUPPLIER_TYPE] = " + supplier.supplierType + ",";
            update += "[NAME] = '" + supplier.name + "',";
            update += "[NAME_SHORT] = '" + supplier.nameShort + "',";
            update += "[MNEMONIC_CODE] = '" + supplier.mnemonicCode + "',";
            update += "[AREA] = '" + supplier.area + "',";
            update += "[CREDIT] = " + supplier.credit + ",";
            update += "[VAT_RATE] = " + supplier.varRate + ",";
            update += "[CONTACT] = '" + supplier.contact + "',";
            update += "[TEL] = '" + supplier.tel + "',";
            update += "[FAX] = '" + supplier.fax + "',";
            update += "[MOBILE_PHONE] = '" + supplier.mobilePhone + "',";
            update += "[E-MAIL] = '" + supplier.email + "',";
            update += "[HOME_PAGE] = '" + supplier.homePage + "',";
            update += "[COMPANY_ADDRESS] = '" + supplier.address + "',";
            update += "[ZIP_CODE] = '" + supplier.zipCode + "',";
            update += "[BANK_NAME] = '" + supplier.bankName + "',";
            update += "[BANK_ACCOUNT] = '" + supplier.bankAccount + "',";
            update += "[TAX_ACCOUNT] = '" + supplier.taxAccount + "',";
            update += "[NOTE] = '" + supplier.note + "'";
            update += "WHERE PKEY = " + Convert.ToString(pkey);

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

        private void load()
        {
            string supplierQuery = "SELECT [PKEY],[SUPPLIER_TYPE],[NAME],[NAME_SHORT],[MNEMONIC_CODE],[AREA],[CREDIT],";
            supplierQuery += "[VAT_RATE],[CONTACT],[TEL],[FAX],[MOBILE_PHONE],[E-MAIL],[HOME_PAGE],[COMPANY_ADDRESS],";
            supplierQuery += "[ZIP_CODE],[BANK_NAME],[BANK_ACCOUNT],[TAX_ACCOUNT],[NOTE], [IS_FORBID] ";
            supplierQuery += "FROM [dbo].[BASE_SUPPLIER_LIST] ORDER BY PKEY";

            m_noForbidSupplierList.Clear();
            m_forbidSupplierList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, supplierQuery))
            {
                int forbid = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    SupplierTable supplier = new SupplierTable();
                    supplier.pkey = DbDataConvert.ToInt32(row[0]);
                    supplier.supplierType = DbDataConvert.ToInt32(row[1]);
                    supplier.name = DbDataConvert.ToString(row[2]);
                    supplier.nameShort = DbDataConvert.ToString(row[3]);
                    supplier.mnemonicCode = DbDataConvert.ToString(row[4]);
                    supplier.area = DbDataConvert.ToString(row[5]);
                    supplier.credit = DbDataConvert.ToInt32(row[6]);
                    supplier.varRate = DbDataConvert.ToInt32(row[7]);
                    supplier.contact = DbDataConvert.ToString(row[8]);
                    supplier.tel = DbDataConvert.ToString(row[9]);
                    supplier.fax = DbDataConvert.ToString(row[10]);
                    supplier.mobilePhone = DbDataConvert.ToString(row[11]);
                    supplier.email = DbDataConvert.ToString(row[12]);
                    supplier.homePage = DbDataConvert.ToString(row[13]);
                    supplier.address = DbDataConvert.ToString(row[14]);
                    supplier.zipCode = DbDataConvert.ToString(row[15]);
                    supplier.bankName = DbDataConvert.ToString(row[16]);
                    supplier.bankAccount = DbDataConvert.ToString(row[17]);
                    supplier.taxAccount = DbDataConvert.ToString(row[18]);
                    supplier.note = DbDataConvert.ToString(row[19]);
                    forbid = DbDataConvert.ToInt32(row[20]);

                    if (forbid == 0)
                    {
                        m_noForbidSupplierList.Add(m_noForbidSupplierList.Count, supplier);
                    }
                    else
                    {
                        m_forbidSupplierList.Add(m_forbidSupplierList.Count, supplier);
                    }
                }
            }
        }

        public SortedDictionary<int, SupplierTable> getAllSupplierInfo()
        {
            if (m_noForbidSupplierList.Count == 0)
            {
                load();
            }

            return m_noForbidSupplierList;
        }

        public SortedDictionary<int, SupplierTable> getAllForbidSupplierInfo()
        {
            if (m_noForbidSupplierList.Count == 0)
            {
                load();
            }

            return m_forbidSupplierList;
        }

        public SortedDictionary<int, SupplierTable> getSupplierInfoFromSupplierType(int supplierTypePkey)
        {
            if (m_noForbidSupplierList.Count == 0)
            {
                load();
            }

            SortedDictionary<int, SupplierTable> supplierList = new SortedDictionary<int, SupplierTable>();

            foreach (KeyValuePair<int, SupplierTable> index in m_noForbidSupplierList)
            {
                SupplierTable supplier = new SupplierTable();
                supplier = index.Value;

                if (supplier.supplierType == supplierTypePkey)
                {
                    supplierList.Add(supplierList.Count, supplier);
                }
            }

            return supplierList;
        }

        public SortedDictionary<int, SupplierTable> getSupplierInfoFromSerachTerm(string searchTerm)
        {
            if (m_noForbidSupplierList.Count == 0)
            {
                load();
            }

            SortedDictionary<int, SupplierTable> supplierList = new SortedDictionary<int, SupplierTable>();

            foreach (KeyValuePair<int, SupplierTable> index in m_noForbidSupplierList)
            {
                SupplierTable supplier = new SupplierTable();
                supplier = index.Value;

                if (supplier.name.IndexOf(searchTerm) >= 0)
                {
                    supplierList.Add(supplierList.Count, supplier);
                }
            }

            return supplierList;
        }

        public SupplierTable getSupplierInfoFromPkey(int pkey)
        {
            if (m_noForbidSupplierList.Count == 0)
            {
                load();
            }

            SupplierTable supplier = new SupplierTable();

            foreach (KeyValuePair<int, SupplierTable> index in m_noForbidSupplierList)
            {
                SupplierTable record = new SupplierTable();
                record = index.Value;

                if (record.pkey == pkey)
                {
                    supplier = record;
                }
            }

            return supplier;
        }

        public string getSupplierNameFromPkey(int pkey)
        {
            string name = "";

            if (m_noForbidSupplierList.Count == 0)
            {
                load();
            }

            SupplierTable supplier = new SupplierTable();

            foreach (KeyValuePair<int, SupplierTable> index in m_noForbidSupplierList)
            {
                SupplierTable record = new SupplierTable();
                record = index.Value;

                if (record.pkey == pkey)
                {
                    name = record.name;
                    break;
                }
            }

            return name;
        }

        public void forbidSupplier(int pkey)
        {
            string update = "UPDATE [dbo].[BASE_SUPPLIER_LIST] SET [IS_FORBID] = 1 WHERE PKEY = ";
            update += Convert.ToString(pkey);

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

        public void noForbidSupplier(int pkey)
        {
            string update = "UPDATE [dbo].[BASE_SUPPLIER_LIST] SET [IS_FORBID] = 0 WHERE PKEY = ";
            update += Convert.ToString(pkey);

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
    }

    public class SupplierTable
    {
        public int pkey { get; set; }
        public int supplierType { get; set; }
        public string name { get; set; }
        public string nameShort { get; set; }
        public string mnemonicCode { get; set; }
        public string area { get; set; }
        public int credit { get; set; }
        public int varRate { get; set; }
        public string contact { get; set; }
        public string tel { get; set; }
        public string fax { get; set; }
        public string mobilePhone { get; set; }
        public string email { get; set; }
        public string homePage { get; set; }
        public string address { get; set; }
        public string zipCode { get; set; }
        public string bankName { get; set; }
        public string bankAccount { get; set; }
        public string taxAccount { get; set; }
        public string note { get; set; }
    }
}