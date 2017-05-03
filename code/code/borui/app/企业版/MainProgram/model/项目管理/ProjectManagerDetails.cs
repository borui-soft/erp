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

                        string insert = "INSERT INTO [dbo].[PROJECT_MATERIE_MANAGER_DETAILS]([ROW_NUMBER],[BILL_NUMBER],";
                        insert += " [NUM],[SEQUENCE],[USE_DATE],[DEVICE_NAME],[SIZE],[CL],";
                        insert += " [MATERIEL_ID],[VALUE],[MAKE_TYPE],[MATERIEL_NOTE])VALUES(";

                        insert += record.rowNumber + ",";
                        insert += "'" + record.billNumber + "',";

                        // 插入序号、序列号、使用日期、所属部件信息
                        insert += "'" + record.no + "',";
                        insert += "'" + record.sequence + "',";
                        insert += "'" + record.useDate + "',";
                        insert += "'" + record.deviceName + "',";
                        insert += "'" + record.materielSize + "',";
                        insert += "'" + record.cl + "',";

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
            string sql = "SELECT [PKEY],[BILL_NUMBER],[ROW_NUMBER],[MATERIEL_ID],[VALUE],[MAKE_TYPE],[MATERIEL_NOTE],";
            sql += "[NUM], [SEQUENCE], [USE_DATE], [DEVICE_NAME], [CL], [SIZE] FROM [dbo].[PROJECT_MATERIE_MANAGER_DETAILS] ORDER BY PKEY";

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

                    //record.num = materiel.num;
                    record.materielUnit = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", materiel.unitPurchase);


                    record.value = DbDataConvert.ToDouble(row["VALUE"]);
                    record.makeType = DbDataConvert.ToString(row["MAKE_TYPE"]);
                    record.materielNote = DbDataConvert.ToString(row["MATERIEL_NOTE"]);

                    // 读取序号、序列号、使用日期、所属部件,材料，尺寸
                    record.no = DbDataConvert.ToString(row["NUM"]);
                    record.sequence = DbDataConvert.ToString(row["SEQUENCE"]);
                    record.useDate = DbDataConvert.ToString(row["USE_DATE"]);
                    record.deviceName = DbDataConvert.ToString(row["DEVICE_NAME"]); 
                    record.materielSize = DbDataConvert.ToString(row["SIZE"]); 
                    record.cl = DbDataConvert.ToString(row["CL"]); 

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

        public double getPurchaseRequestValueFromBillNumber(string billNumber, int materielID)
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            double value = 0.0;

            foreach (KeyValuePair<int, ProjectManagerDetailsTable> index in m_tableDataList)
            {
                if (index.Value.billNumber == billNumber && index.Value.materielID == materielID)
                {
                    value += index.Value.value;
                }
            }

            return value;
        }

        public ProjectManagerDetailsTable getMaterielInfoFromBillNumber(string billNumber, int materielID)
        {
            ProjectManagerDetailsTable value = new ProjectManagerDetailsTable();

            if (m_tableDataList.Count == 0)
            {
                load();
            }

            foreach (KeyValuePair<int, ProjectManagerDetailsTable> index in m_tableDataList)
            {
                if (index.Value.billNumber == billNumber && index.Value.materielID == materielID)
                {
                    value = index.Value;
                    break;
                }
            }

            return value;
        }

        public ProjectManagerDetailsTable getMaterielInfoFromRowNum(string billNumber, int rowMum)
        {
            ProjectManagerDetailsTable value = new ProjectManagerDetailsTable();

            if (m_tableDataList.Count == 0)
            {
                load();
            }

            foreach (KeyValuePair<int, ProjectManagerDetailsTable> index in m_tableDataList)
            {
                if (index.Value.billNumber == billNumber && index.Value.rowNumber == rowMum)
                {
                    value = index.Value;
                    break;
                }
            }

            return value;
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

        // 物料详细信息,行号，物料ID，物料编号
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

        // 使用日期，所属部件，序号，序列号
        public string useDate { get; set; }
        public string deviceName { get; set; }
        public string no { get; set; }
        public string sequence { get; set; }
        public string cl { get; set; }
    }
}