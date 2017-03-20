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
    public class MaterielProOccupiedOrderDetails : ITableModel
    {
        SortedDictionary<int, MaterielProOccupiedInfo> m_materielProlist = new SortedDictionary<int, MaterielProOccupiedInfo>();
        private SortedDictionary<int, MaterielProOccupiedOrderDetailsTable> m_tableDataList = new SortedDictionary<int, MaterielProOccupiedOrderDetailsTable>();

        static private MaterielProOccupiedOrderDetails m_instance = null;

        private MaterielProOccupiedOrderDetails()
        {
            load();
        }

        static public MaterielProOccupiedOrderDetails getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new MaterielProOccupiedOrderDetails();
            }

            return m_instance;
        }

        public void refrensRecord()
        {
            load();
            m_materielProlist.Clear();
        }

        public void insert(ArrayList orderDetailsRecords)
        {
            if (orderDetailsRecords.Count <= 0)
            {
                return ;
            }
            else
            {
                try
                {
                    for (int i = 0; i < orderDetailsRecords.Count; i++)
                    {
                        MaterielProOccupiedOrderDetailsTable record = (MaterielProOccupiedOrderDetailsTable)orderDetailsRecords[i];

                        if (i == 0 && checkBillIsExist(record.billNumber))
                        {
                            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
                            delete(record.billNumber);
                        }

                        string insert = "INSERT INTO [dbo].[WAREHOUSE_MANAGEMENT_PRO_OCCUPIED_DETAILS]([ROW_NUMBER],[MATERIEL_ID],[BILL_NUMBER]";
                        insert += ",[PRICE],[VALUE],[NOTE],[IS_CANCEL])VALUES(";

                        insert += "'" + record.rowNumber + "',";
                        insert += record.materielID + ",";
                        insert += "'" + record.billNumber + "',";
                        insert += record.price + ",";
                        insert += record.value + ",";
                        insert += "'" + record.note + "', 0";
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
            string delete = "DELETE FROM WAREHOUSE_MANAGEMENT_PRO_OCCUPIED_DETAILS WHERE BILL_NUMBER = '" + billNumber + "'";

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
            m_tableDataList.Clear();

            string querySQL = "SELECT [PKEY],[ROW_NUMBER],[MATERIEL_ID], [BILL_NUMBER],[PRICE],[VALUE],[NOTE], [IS_CANCEL] FROM [WAREHOUSE_MANAGEMENT_PRO_OCCUPIED_DETAILS] ORDER BY [PKEY] DESC";

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, querySQL))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    MaterielProOccupiedOrderDetailsTable record = new MaterielProOccupiedOrderDetailsTable();

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

                    record.note = DbDataConvert.ToString(row["NOTE"]);
                    record.isCancel = DbDataConvert.ToString(row["IS_CANCEL"]);
                    
                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        // 撤销单据
        public void delProOccupied(string pkey)
        {
            string delete = "UPDATE [dbo].[WAREHOUSE_MANAGEMENT_PRO_OCCUPIED_DETAILS] SET IS_CANCEL = 1 WHERE PKEY = " + pkey;

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

        public SortedDictionary<int, MaterielProOccupiedOrderDetailsTable> getAllMaterielProOccupiedOrderInfo()
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public SortedDictionary<int, MaterielProOccupiedOrderDetailsTable> getMaterielProOccupiedInfoFromBillNumber(string billNumber)
        {
            SortedDictionary<int, MaterielProOccupiedOrderDetailsTable> list = new SortedDictionary<int, MaterielProOccupiedOrderDetailsTable>();

            foreach (KeyValuePair<int, MaterielProOccupiedOrderDetailsTable> index in m_tableDataList)
            {
                if (index.Value.billNumber == billNumber && index.Value.isCancel == "0")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public SortedDictionary<int, MaterielProOccupiedInfo> getMaterielProOccupiedList()
        {
            m_materielProlist.Clear();

            SortedDictionary<int, MaterielProOccupiedOrderTable> list =
                MaterielProOccupiedOrder.getInctance().getAllReviewMaterielProOccupiedOrderInfo();

            for (int index = 0; index < list.Count; index++)
            {
                MaterielProOccupiedOrderTable record = new MaterielProOccupiedOrderTable();
                record = (MaterielProOccupiedOrderTable)list[index];

                SortedDictionary<int, MaterielProOccupiedOrderDetailsTable> listDetails =
                    MaterielProOccupiedOrderDetails.getInctance().getMaterielProOccupiedInfoFromBillNumber(record.billNumber);

                for (int index2 = 0; index2 < listDetails.Count; index2++)
                {
                    MaterielProOccupiedOrderDetailsTable recordDeatils = new MaterielProOccupiedOrderDetailsTable();
                    recordDeatils = (MaterielProOccupiedOrderDetailsTable)listDetails[index2];

                    addDeatisToList(recordDeatils.materielID, record.applyStaffName, recordDeatils.value);
                }
            }

            return m_materielProlist;
        }

        public double getMaterielProCountInfoFromProject(int materielID, string projectNum = "")
        {
            double sum = 0.0;
            m_materielProlist.Clear();

            SortedDictionary<int, MaterielProOccupiedOrderTable> list =
                MaterielProOccupiedOrder.getInctance().getAllMaterielProOccupiedOrderInfo();

            for (int index = 0; index < list.Count; index++)
            {
                MaterielProOccupiedOrderTable record = new MaterielProOccupiedOrderTable();
                record = (MaterielProOccupiedOrderTable)list[index];

                if (projectNum.Length == 0 ||
                    (projectNum.Length > 0 && record.exchangesUnit == projectNum))
                {
                    SortedDictionary<int, MaterielProOccupiedOrderDetailsTable> listDetails =
                        MaterielProOccupiedOrderDetails.getInctance().getMaterielProOccupiedInfoFromBillNumber(record.billNumber);

                    for (int index2 = 0; index2 < listDetails.Count; index2++)
                    {
                        MaterielProOccupiedOrderDetailsTable recordDeatils = new MaterielProOccupiedOrderDetailsTable();
                        recordDeatils = (MaterielProOccupiedOrderDetailsTable)listDetails[index2];

                        if (recordDeatils.materielID == materielID)
                        {
                            sum += recordDeatils.value;
                        }
                    }
                }
            }

            return sum;
        }

        private void addDeatisToList(int materielID, string applyStaffName, double value)
        {
            if (m_materielProlist.ContainsKey(materielID))
            {
                MaterielProOccupiedInfo record = new MaterielProOccupiedInfo();
                record = (MaterielProOccupiedInfo)m_materielProlist[materielID];

                if (record.applyStaffName.IndexOf(applyStaffName) <= 0)
                {
                    record.applyStaffName += "、";
                    record.applyStaffName += applyStaffName;
                }

                record.sum += value;
            }
            else
            {
                MaterielProOccupiedInfo newRecord = new MaterielProOccupiedInfo();

                newRecord.pkey = materielID;
                newRecord.applyStaffName = applyStaffName;
                newRecord.sum = value;

                m_materielProlist.Add(materielID, newRecord);
            }
        }











        public bool checkBillIsExist(string billNumber)
        {
            bool isRet = false;

            foreach (KeyValuePair<int, MaterielProOccupiedOrderDetailsTable> index in m_tableDataList)
            {
                MaterielProOccupiedOrderDetailsTable record = new MaterielProOccupiedOrderDetailsTable();

                if (index.Value.billNumber == billNumber)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }
    }

    public class MaterielProOccupiedOrderDetailsTable
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
        public string note { get; set; }
        public string isCancel { get; set; }
    }

    public class MaterielProOccupiedInfo
    {
        // 单据基本信息
        public int pkey { get; set; }

        // 申请人
        public string applyStaffName { get; set; }

        // 总的占用数量
        public double sum { get; set; }
    }
}