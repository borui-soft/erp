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
    public class ProjectManagerDetails : ITableModel
    {
        private SortedDictionary<int, ProjectManagerDetailsTable> m_tableDataList = new SortedDictionary<int, ProjectManagerDetailsTable>();

        static private ProjectManagerDetails m_instance = null;

        private ProjectManagerDetails()
        {
            load();
        }

        public void refreshRecord()
        {
            load();
        }

        static public ProjectManagerDetails getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new ProjectManagerDetails();
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
                        ProjectManagerDetailsTable record = (ProjectManagerDetailsTable)purchaseOrderDetailsRecords[i];

                        if (i == 0 && checkBillIsExist(record.billNumber))
                        {
                            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
                            delete(record.billNumber);
                        }

                        string insert = "INSERT INTO [dbo].[PROJECT_MATERIE_MANAGER_DETAILS]([ROW_NUMBER],[BILL_NUMBER],[MATERIEL_ID],[VALUE],[MAKE_TYPE],[MATERIEL_NOTE])VALUES(";

                        insert += record.rowNumber + ",";
                        insert += "'" + record.billNumber + "',";
                        insert += record.materielID + ",";
                        insert += record.value + ",";
                        insert += "'" + record.makeType + "',";
                        insert += "'" + record.materielNote + "')";

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
            string delete = "DELETE FROM PROJECT_MATERIE_MANAGER_DETAILS WHERE BILL_NUMBER = '" + billNumber + "'";

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
            string sql = "SELECT [PKEY],[BILL_NUMBER],[ROW_NUMBER],[MATERIEL_ID],[VALUE],[MAKE_TYPE],[MATERIEL_NOTE] FROM [dbo].[PROJECT_MATERIE_MANAGER_DETAILS] ORDER BY PKEY";

            m_tableDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    ProjectManagerDetailsTable record = new ProjectManagerDetailsTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);

                    record.rowNumber = DbDataConvert.ToInt32(row["ROW_NUMBER"]);
                    record.materielID = DbDataConvert.ToInt32(row["MATERIEL_ID"]);

                    // 根据物料ID得到物料相关信息
                    MaterielTable materiel = Materiel.getInctance().getMaterielInfoFromPkey(record.materielID);
                    record.materielBrand = materiel.brand;
                    record.materielName = materiel.name;
                    record.materielModel = materiel.model;
                    record.materielParameter = materiel.materielParameter;
                    record.materielSize = "";
                    record.num = materiel.num;
                    record.materielUnit = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", materiel.unitPurchase);


                    record.value = DbDataConvert.ToDouble(row["VALUE"]);
                    record.makeType = DbDataConvert.ToString(row["MAKE_TYPE"]);
                    record.materielNote = DbDataConvert.ToString(row["MATERIEL_NOTE"]);

                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        public SortedDictionary<int, ProjectManagerDetailsTable> getAllPurchaseOrderInfo()
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public SortedDictionary<int, ProjectManagerDetailsTable> getPurchaseInfoFromBillNumber(string billNumber)
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            SortedDictionary<int, ProjectManagerDetailsTable> list = new SortedDictionary<int, ProjectManagerDetailsTable>();

            foreach (KeyValuePair<int, ProjectManagerDetailsTable> index in m_tableDataList)
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

            foreach (KeyValuePair<int, ProjectManagerDetailsTable> index in m_tableDataList)
            {
                ProjectManagerDetailsTable record = new ProjectManagerDetailsTable();

                if (index.Value.billNumber == billNumber)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }
    }

    public class ProjectManagerDetailsTable
    {
        public int pkey { get; set; }
        public string billNumber { get; set; }

        // 物料详细信息
        public int rowNumber { get; set; }
        public int materielID { get; set; }
        public string num { get; set; }

        public string materielBrand { get; set; }
        public string materielName { get; set; }
        public string materielModel { get; set; }
        public string materielSize { get; set; }
        public string materielUnit { get; set; }
        public string materielParameter { get; set; }

        public double value { get; set; }
        public string makeType { get; set; }
        public string materielNote { get; set; }
    }
}