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
    public class Customer : ITableModel
    {
        private SortedDictionary<int, CustomerTable> m_noForbidCustomerList = new SortedDictionary<int, CustomerTable>();
        private SortedDictionary<int, CustomerTable> m_forbidCustomerList = new SortedDictionary<int, CustomerTable>();

        static private Customer m_instance = null;

        private Customer()
        {
            load();
        }

        static public Customer getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new Customer();
            }

            return m_instance;
        }

        public void insert(CustomerTable customer)
        {
            string insert = "INSERT INTO [dbo].[BASE_CUSTOMER_LIST] ([SUPPLIER_TYPE],[NAME],[NAME_SHORT],[MNEMONIC_CODE],[AREA],";
            insert += "[CREDIT],[VAT_RATE],[CONTACT],[TEL],[FAX],[MOBILE_PHONE],[E-MAIL],[HOME_PAGE],[COMPANY_ADDRESS],[ZIP_CODE],";
            insert += "[BANK_NAME],[BANK_ACCOUNT],[TAX_ACCOUNT],[NOTE],[IS_FORBID]) VALUES(";

            insert += customer.customerType + ",";
            insert += "'" + customer.name + "',";
            insert += "'" + customer.nameShort + "',";
            insert += "'" + customer.mnemonicCode + "',";
            insert += "'" + customer.area + "',";
            insert += customer.credit + ",";
            insert += customer.varRate + ",";
            insert += "'" + customer.contact + "',";
            insert += "'" + customer.tel + "',";
            insert += "'" + customer.fax + "',";
            insert += "'" + customer.mobilePhone + "',";
            insert += "'" + customer.email + "',";
            insert += "'" + customer.homePage + "',";
            insert += "'" + customer.address + "',";
            insert += "'" + customer.zipCode + "',";
            insert += "'" + customer.bankName + "',";
            insert += "'" + customer.bankAccount + "',";
            insert += "'" + customer.taxAccount + "',";
            insert += "'" + customer.note + "', 0";
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
            if (deleteRecord("BASE_CUSTOMER_LIST", pkey))
            {
                load();
            }
        }

        public void update(int pkey, CustomerTable customer)
        {
            string update = "UPDATE [dbo].[BASE_CUSTOMER_LIST] SET ";

            update += "[SUPPLIER_TYPE] = " + customer.customerType + ",";
            update += "[NAME] = '" + customer.name + "',";
            update += "[NAME_SHORT] = '" + customer.nameShort + "',";
            update += "[MNEMONIC_CODE] = '" + customer.mnemonicCode + "',";
            update += "[AREA] = '" + customer.area + "',";
            update += "[CREDIT] = " + customer.credit + ",";
            update += "[VAT_RATE] = " + customer.varRate + ",";
            update += "[CONTACT] = '" + customer.contact + "',";
            update += "[TEL] = '" + customer.tel + "',";
            update += "[FAX] = '" + customer.fax + "',";
            update += "[MOBILE_PHONE] = '" + customer.mobilePhone + "',";
            update += "[E-MAIL] = '" + customer.email + "',";
            update += "[HOME_PAGE] = '" + customer.homePage + "',";
            update += "[COMPANY_ADDRESS] = '" + customer.address + "',";
            update += "[ZIP_CODE] = '" + customer.zipCode + "',";
            update += "[BANK_NAME] = '" + customer.bankName + "',";
            update += "[BANK_ACCOUNT] = '" + customer.bankAccount + "',";
            update += "[TAX_ACCOUNT] = '" + customer.taxAccount + "',";
            update += "[NOTE] = '" + customer.note + "'";
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
            string customerQuery = "SELECT [PKEY],[SUPPLIER_TYPE],[NAME],[NAME_SHORT],[MNEMONIC_CODE],[AREA],[CREDIT],";
            customerQuery += "[VAT_RATE],[CONTACT],[TEL],[FAX],[MOBILE_PHONE],[E-MAIL],[HOME_PAGE],[COMPANY_ADDRESS],";
            customerQuery += "[ZIP_CODE],[BANK_NAME],[BANK_ACCOUNT],[TAX_ACCOUNT],[NOTE], [IS_FORBID] ";
            customerQuery += "FROM [dbo].[BASE_CUSTOMER_LIST] ORDER BY PKEY";

            m_noForbidCustomerList.Clear();
            m_forbidCustomerList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, customerQuery))
            {
                int forbid = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    CustomerTable customer = new CustomerTable();
                    customer.pkey = DbDataConvert.ToInt32(row[0]);
                    customer.customerType = DbDataConvert.ToInt32(row[1]);
                    customer.name = DbDataConvert.ToString(row[2]);
                    customer.nameShort = DbDataConvert.ToString(row[3]);
                    customer.mnemonicCode = DbDataConvert.ToString(row[4]);
                    customer.area = DbDataConvert.ToString(row[5]);
                    customer.credit = DbDataConvert.ToInt32(row[6]);
                    customer.varRate = DbDataConvert.ToInt32(row[7]);
                    customer.contact = DbDataConvert.ToString(row[8]);
                    customer.tel = DbDataConvert.ToString(row[9]);
                    customer.fax = DbDataConvert.ToString(row[10]);
                    customer.mobilePhone = DbDataConvert.ToString(row[11]);
                    customer.email = DbDataConvert.ToString(row[12]);
                    customer.homePage = DbDataConvert.ToString(row[13]);
                    customer.address = DbDataConvert.ToString(row[14]);
                    customer.zipCode = DbDataConvert.ToString(row[15]);
                    customer.bankName = DbDataConvert.ToString(row[16]);
                    customer.bankAccount = DbDataConvert.ToString(row[17]);
                    customer.taxAccount = DbDataConvert.ToString(row[18]);
                    customer.note = DbDataConvert.ToString(row[19]);
                    forbid = DbDataConvert.ToInt32(row[20]);

                    if (forbid == 0)
                    {
                        m_noForbidCustomerList.Add(m_noForbidCustomerList.Count, customer);
                    }
                    else
                    {
                        m_forbidCustomerList.Add(m_forbidCustomerList.Count, customer);
                    }
                }
            }
        }

        public SortedDictionary<int, CustomerTable> getAllCustomerInfo()
        {
            if (m_noForbidCustomerList.Count == 0)
            {
                load();
            }

            return m_noForbidCustomerList;
        }

        public SortedDictionary<int, CustomerTable> getAllForbidCustomerInfo()
        {
            if (m_noForbidCustomerList.Count == 0)
            {
                load();
            }

            return m_forbidCustomerList;
        }

        public SortedDictionary<int, CustomerTable> getCustomerInfoFromCustomerType(int customerTypePkey)
        {
            if (m_noForbidCustomerList.Count == 0)
            {
                load();
            }

            SortedDictionary<int, CustomerTable> customerList = new SortedDictionary<int, CustomerTable>();

            foreach (KeyValuePair<int, CustomerTable> index in m_noForbidCustomerList)
            {
                CustomerTable customer = new CustomerTable();
                customer = index.Value;

                if (customer.customerType == customerTypePkey)
                {
                    customerList.Add(customerList.Count, customer);
                }
            }

            return customerList;
        }

        public CustomerTable getCustomerInfoFromPkey(int pkey)
        {
            if (m_noForbidCustomerList.Count == 0)
            {
                load();
            }

            CustomerTable customer = new CustomerTable();

            foreach (KeyValuePair<int, CustomerTable> index in m_noForbidCustomerList)
            {
                CustomerTable record = new CustomerTable();
                record = index.Value;

                if (record.pkey == pkey)
                {
                    customer = record;
                }
            }

            return customer;
        }

        public string getCustomerNameFromPkey(int pkey)
        {
            string name = "";
            if (m_noForbidCustomerList.Count == 0)
            {
                load();
            }

            CustomerTable customer = new CustomerTable();

            foreach (KeyValuePair<int, CustomerTable> index in m_noForbidCustomerList)
            {
                CustomerTable record = new CustomerTable();
                record = index.Value;

                if (record.pkey == pkey)
                {
                    name = record.name;
                }
            }

            return name;
        }

        public void forbidCustomer(int pkey)
        {
            string update = "UPDATE [dbo].[BASE_CUSTOMER_LIST] SET [IS_FORBID] = 1 WHERE PKEY = ";
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

        public void noForbidCustomer(int pkey)
        {
            string update = "UPDATE [dbo].[BASE_CUSTOMER_LIST] SET [IS_FORBID] = 0 WHERE PKEY = ";
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

    public class CustomerTable
    {
        public int pkey { get; set; }
        public int customerType { get; set; }
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