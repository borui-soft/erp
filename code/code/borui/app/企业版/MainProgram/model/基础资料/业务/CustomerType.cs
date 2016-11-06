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
    public class CustomerType : ITableModel
    {
        private SortedDictionary<int, CustomerTypeTable> m_CustomerTypeList = new SortedDictionary<int, CustomerTypeTable>();

        static private CustomerType m_instance = null;

        private CustomerType()
        {
            load();
        }

        static public CustomerType getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new CustomerType();
            }

            return m_instance;
        }

        private void load()
        {
            SortedDictionary<int, CustomerTypeTable> customerGroupList = new SortedDictionary<int, CustomerTypeTable>();
            string customerQuery = "SELECT PKEY, TYPE_NAME, [DESC] FROM  BASE_CUSTOMER_TYPE ORDER BY PKEY";

            if (m_CustomerTypeList.Count > 0)
            {
                m_CustomerTypeList.Clear();
            }

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, customerQuery))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    CustomerTypeTable customerTyoe = new CustomerTypeTable();
                    customerTyoe.pkey = DbDataConvert.ToInt32(row[0]);
                    customerTyoe.name = DbDataConvert.ToString(row[1]);
                    customerTyoe.desc = DbDataConvert.ToString(row[2]);

                    customerGroupList.Add(customerTyoe.pkey, customerTyoe);
                }
            }

            m_CustomerTypeList = customerGroupList;
        }

        public void delete(int pkey)
        {
            if (deleteRecord("BASE_CUSTOMER_TYPE", pkey, false))
            {
                load();
            }
        }

        public void update(int pkey, CustomerTypeTable customerType)
        {
            string update = "UPDATE [dbo].[BASE_CUSTOMER_TYPE] SET ";

            update += "[TYPE_NAME] = '" + customerType.name + "',";
            update += "[DESC] = '" + customerType.desc + "' ";
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

        public void insert(CustomerTypeTable customerType)
        {
            string insert = "INSERT INTO [dbo].[BASE_CUSTOMER_TYPE] ([TYPE_NAME],[DESC]) VALUES (";

            insert += "'" + customerType.name + "',";
            insert += "'" + customerType.desc + "'";
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

        public string getCustomerTypeNameFromPkey(int pkey)
        {
            string customerTypeName = "未知分类名称";

            if (m_CustomerTypeList.ContainsKey(pkey))
            {
                CustomerTypeTable record = (CustomerTypeTable)m_CustomerTypeList[pkey];
                customerTypeName = record.name;
            }

            return customerTypeName;
        }

        public CustomerTypeTable getCustomerTypeInfoFromPkey(int pkey)
        {
            CustomerTypeTable customerType = new CustomerTypeTable();

            if (m_CustomerTypeList.ContainsKey(pkey))
            {
                customerType = (CustomerTypeTable)m_CustomerTypeList[pkey];
            }

            return customerType;
        }

        public int getMaxPkey()
        {
            int peky = -1;

            foreach (KeyValuePair<int, CustomerTypeTable> index in m_CustomerTypeList)
            {
                if(index.Key > peky)
                {
                    peky = index.Key;
                }
            }

            return peky;
        }
    }

    public class CustomerTypeTable
    {
        public int pkey { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
    }
}
