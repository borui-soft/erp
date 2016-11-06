using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Collections;
using TIV.Core.DatabaseAccess;
using MainProgram.bus;

namespace MainProgram.model
{
    using DatabaseAccessFactoryInstance = TIV.Core.BaseCoreLib.Singleton<DatabaseFactory>;
    public class CustomerOrgStruct : ITableModel
    {
        static private CustomerOrgStruct m_instance = null;
        private SortedDictionary<int, int> m_childNodes = new SortedDictionary<int, int>();
        private SortedDictionary<int, CustomerOrgStructTable> m_CustomerOrgList = new SortedDictionary<int, CustomerOrgStructTable>();

        private CustomerOrgStruct()
        {
            load();
        }

        static public CustomerOrgStruct getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new CustomerOrgStruct();
            }

            return m_instance;
        }

        private void load()
        {
            SortedDictionary<int, CustomerOrgStructTable> customerGroupList = new SortedDictionary<int, CustomerOrgStructTable>();
            string customerQuery = "SELECT PKEY, VALUE, PARENT_PKEY FROM  BASE_CUSTOMER_ORG_STRUCT ORDER BY PKEY";

            if (m_CustomerOrgList.Count > 0)
            {
                m_CustomerOrgList.Clear();
            }

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, customerQuery))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    CustomerOrgStructTable record = new CustomerOrgStructTable();
                    record.pkey = DbDataConvert.ToInt32(row[0]);
                    record.value = DbDataConvert.ToInt32(row[1]);
                    record.parentPkey = DbDataConvert.ToInt32(row[2]);

                    customerGroupList.Add(record.pkey, record);
                }
            }

            m_CustomerOrgList = customerGroupList;
        }

        public void delete(int pkey)
        {
            if (deleteRecord("BASE_CUSTOMER_ORG_STRUCT", pkey))
            {
                load();
            }
        }

        public void update(int pkey, CustomerOrgStructTable customerOrgStruct)
        {
            string update = "UPDATE [dbo].[BASE_CUSTOMER_ORG_STRUCT] SET ";

            update += "[VALUE] = '" + customerOrgStruct.value + "',";
            update += "[PARENT_PKEY] = " + Convert.ToString(customerOrgStruct.parentPkey) + " ";
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

        public void insert(CustomerOrgStructTable customerOrgStruct)
        {
            string insert = "INSERT INTO [dbo].[BASE_CUSTOMER_ORG_STRUCT] ([VALUE],[PARENT_PKEY]) VALUES (";

            insert += "'" + customerOrgStruct.value + "',";
            insert += Convert.ToString(customerOrgStruct.parentPkey);
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
        public int getRootNodePkey()
        {
            int rootNodeID = 0;

            CustomerOrgStructTable record = new CustomerOrgStructTable();
            foreach (KeyValuePair<int, CustomerOrgStructTable> index in m_CustomerOrgList)
            {
                record = index.Value;
                if (record.parentPkey == 0)
                {
                    rootNodeID = record.pkey;
                }
            }

            return rootNodeID;
        }

        public int getNoteValueFromPkey(int pkey)
        {
            int noteValue = 0;

            if (m_CustomerOrgList.ContainsKey(pkey))
            {
                CustomerOrgStructTable record = (CustomerOrgStructTable)m_CustomerOrgList[pkey];
                noteValue = record.value;
            }

            return noteValue;
        }

        public ArrayList getNodesFormParentID(int parentID)
        {
            ArrayList nodes = new ArrayList();

            CustomerOrgStructTable record = new CustomerOrgStructTable();

            foreach (KeyValuePair<int, CustomerOrgStructTable> index in m_CustomerOrgList)
            {
                record = index.Value;
                if (record.parentPkey == parentID)
                {
                    nodes.Add(record);
                }
            }

            return nodes;
        }

        public int getPkeyFromValue(int value)
        {
            int peky = 0;
            ArrayList nodes = new ArrayList();

            CustomerOrgStructTable record = new CustomerOrgStructTable();

            foreach (KeyValuePair<int, CustomerOrgStructTable> index in m_CustomerOrgList)
            {
                record = index.Value;
                if (record.value == value)
                {
                    peky = record.pkey;
                }
            }

            return peky;
        }

        public SortedDictionary<int, int> getAllChildNodeValue(int value)
        {
            m_childNodes.Clear();
            getACustomerOrgValue(getPkeyFromValue(value), value);
            return m_childNodes;
        }

        private void getACustomerOrgValue(int parentID, int value)
        {
            ArrayList nodeList = getNodesFormParentID(parentID);

            if (nodeList.Count == 0)
            {
                if (!m_childNodes.ContainsKey(value))
                {
                    m_childNodes.Add(value, value);
                }
            }
            else
            {
                for (int i = 0; i < nodeList.Count; i++)
                {
                    CustomerOrgStructTable record = (CustomerOrgStructTable)nodeList[i];

                    if (!m_childNodes.ContainsKey(record.value))
                    {
                        m_childNodes.Add(record.value, record.value);
                    }

                    getACustomerOrgValue(record.pkey, record.value);
                }
            }
        }
    }

    public class CustomerOrgStructTable
    {
        public int pkey { get; set; }
        public int value { get; set; }
        public int parentPkey { get; set; }
    }
}
