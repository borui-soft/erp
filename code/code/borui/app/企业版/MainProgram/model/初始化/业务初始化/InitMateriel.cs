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
    public class InitMateriel : ITableModel
    {
        private SortedDictionary<int, InitMaterielTable> m_materielStockList = new SortedDictionary<int, InitMaterielTable>();

        static private InitMateriel m_instance = null;

        private InitMateriel()
        {
            load();
        }

        static public InitMateriel getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new InitMateriel();
            }

            return m_instance;
        }

        public void insert(InitMaterielTable record, bool isDisplayMessageBox = true)
        {
            double price = 0;
            double materielNum = 0;

            string insert = "INSERT INTO [dbo].[INIT_STORAGE_STOCK]([MATERIEL_ID],[VALUE],[PRICE]) VALUES (";

            price = record.price;
            materielNum = record.value;

            /* 插入逻辑
             * 1、检查库存表是否已经存在该物料的库存信息
             * 2、得到物料的计价方式，后进先出或是先进后出或是移动加权平均
             * 3、如果是移动加权平均，计算移动加权单价
             * 4、如果是后进先出或是先进后出计价方式，直接插入到数据库
             * */
            if (checkMaterielIsExist(record.materielID))
            {
                InitMaterielTable storageExistMaterielRecord = getMaterielInfoFromMaterielID(record.materielID);
                
                // 根据id，得到物料的详细信息,进一步得到物料的计价方式
                MaterielTable materielInfo = Materiel.getInctance().getMaterielInfoFromPkey(storageExistMaterielRecord.materielID);
                int valuation = materielInfo.valuation;
                string valuationName = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_VALUATION_TYPE_LIST", valuation);

                if (valuation == 0 || valuationName.IndexOf("加权") != -1)
                {
                    double newPrice = MobileWeightedUnitPrice.calculateMaterielNewPrice(
                        storageExistMaterielRecord.price,
                        storageExistMaterielRecord.value,
                        price, materielNum);

                    InitMaterielTable newRecord = new InitMaterielTable();
                    newRecord.pkey = storageExistMaterielRecord.pkey;
                    newRecord.materielID = storageExistMaterielRecord.materielID;
                    newRecord.price = newPrice;
                    newRecord.value = materielNum + storageExistMaterielRecord.value;

                    update(newRecord.pkey, newRecord, isDisplayMessageBox);
                    return;
                }
            }

            // 如果不是加权平均或仓库中还不存在该物料信息，直接插入本条记录
            insert += record.materielID + ",";
            insert += materielNum + ",";
            insert += "'" + Convert.ToString(price) + "'";
            insert += ")";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, insert);

                if (isDisplayMessageBox)
                {
                    MessageBoxExtend.messageOK("数据保存成功");
                }

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public void materielOutStorage(InitMaterielTable record, bool isDisplayMessageBox = true)
        {
            double price = 0;
            double materielNum = 0;

            price = record.price;
            materielNum = record.value;

            InitMaterielTable storageExistMaterielRecord = getMaterielInfoFromMaterielID(record.materielID);

            // 根据id，得到物料的详细信息,进一步得到物料的计价方式
            MaterielTable materielInfo = Materiel.getInctance().getMaterielInfoFromPkey(storageExistMaterielRecord.materielID);
            int valuation = materielInfo.valuation;
            string valuationName = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_VALUATION_TYPE_LIST", valuation);

            if (valuation == 0 || valuationName.IndexOf("加权") != -1)
            {
                double newPrice = MobileWeightedUnitPrice.calculateMaterielNewPriceOutStorage(
                    storageExistMaterielRecord.price,
                    storageExistMaterielRecord.value,
                    price, materielNum);

                InitMaterielTable newRecord = new InitMaterielTable();
                newRecord.pkey = storageExistMaterielRecord.pkey;
                newRecord.materielID = storageExistMaterielRecord.materielID;
                newRecord.price = newPrice;
                newRecord.value = storageExistMaterielRecord.value - materielNum;

                update(newRecord.pkey, newRecord, isDisplayMessageBox);
                return;
            }
        }

        public void delete(int pkey)
        {
            if (deleteRecord("INIT_STORAGE_STOCK", pkey))
            {
                load();
            }
        }

        public void update(int pkey, InitMaterielTable record, bool isDisplayMessageBox = true)
        {
            string update = "UPDATE [dbo].[INIT_STORAGE_STOCK] SET ";

            update += "[VALUE] = " + record.value + ",";
            update += "[PRICE] = " + record.price;
            update += " WHERE PKEY = " + Convert.ToString(pkey);

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                if (isDisplayMessageBox)
                {
                    MessageBoxExtend.messageOK("数据修改成功");
                }

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
            string sql = "SELECT [PKEY],[MATERIEL_ID],[VALUE] ,[PRICE] FROM [dbo].[INIT_STORAGE_STOCK] ORDER BY [MATERIEL_ID]";

            m_materielStockList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    InitMaterielTable record = new InitMaterielTable();

                    record.pkey = DbDataConvert.ToInt32(row[0]);
                    record.materielID = DbDataConvert.ToInt32(row[1]);
                    record.value = DbDataConvert.ToDouble(row[2]);
                    record.price = DbDataConvert.ToDouble(row[3]);

                    // 根据物料ID，得到物料信息
                    MaterielTable materielInfo = Materiel.getInctance().getMaterielInfoFromPkey(record.materielID);
                    record.name = materielInfo.name;
                    record.storage = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_STORAGE_LIST", materielInfo.storage);
                    record.unitPurchase = AuxiliaryMaterial.getInctance().getAuxiliaryMaterialNameFromPkey("BASE_UNIT_LIST", materielInfo.unit);

                    m_materielStockList.Add(m_materielStockList.Count, record);
                }
            }
        }

        public SortedDictionary<int, InitMaterielTable> getAllInitMaterielInfo()
        {
            if (m_materielStockList.Count == 0)
            {
                load();
            }

            return m_materielStockList;
        }

        public InitMaterielTable getMaterielInfoFromPkey(int pkey)
        {
            if (m_materielStockList.Count == 0)
            {
                load();
            }

            InitMaterielTable materielRecord = new InitMaterielTable();

            foreach (KeyValuePair<int, InitMaterielTable> index in m_materielStockList)
            {
                InitMaterielTable record = new InitMaterielTable();
                record = index.Value;

                if (record.pkey == pkey)
                {
                    materielRecord = record;
                    break;
                }
            }

            return materielRecord;
        }

        public void refreshRecord()
        {
            load();
        }

        public InitMaterielTable getMaterielInfoFromMaterielID(int materielID)
        {
            if (m_materielStockList.Count == 0)
            {
                load();
            }

            InitMaterielTable materielRecord = new InitMaterielTable();

            foreach (KeyValuePair<int, InitMaterielTable> index in m_materielStockList)
            {
                InitMaterielTable record = new InitMaterielTable();
                record = index.Value;

                if (record.materielID == materielID)
                {
                    materielRecord = record;
                    break;
                }
            }

            return materielRecord;
        }

        public bool checkMaterielIsExist(int materielID)
        {
            bool isRet = false;

            if (m_materielStockList.Count == 0)
            {
                load();
            }

            foreach (KeyValuePair<int, InitMaterielTable> index in m_materielStockList)
            {
                InitMaterielTable record = new InitMaterielTable();
                record = index.Value;

                if (record.materielID == materielID)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }

        public void fileImport(string filePath)
        {
            int rowIndex = 0;

            try
            {
                // 正式导入数据前，首先情况INIT_STORAGE_STOCK表数据
                DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, "DELETE FROM [dbo].[INIT_STORAGE_STOCK]");

                string sheetName = "存货初始数据";

                if (ExcelDocProc.getInstance().openFile(filePath))
                {
                    string materielID, value, price;
                    for (rowIndex = 0;; rowIndex++)
                    {
                        materielID = ExcelDocProc.getInstance().getGridValue(sheetName, rowIndex, 0);
                        price = ExcelDocProc.getInstance().getGridValue(sheetName, rowIndex, 1);
                        value = ExcelDocProc.getInstance().getGridValue(sheetName, rowIndex, 2);

                        if (materielID.Length == 0)
                        {
                            break;
                        }
                        else
                        {
                            InitMaterielTable record = new InitMaterielTable();
                            record.materielID = Convert.ToInt32(materielID.ToString());
                            record.value = Convert.ToDouble(value.ToString());
                            record.price = Convert.ToDouble(price.ToString());

                            insert(record, false);
                        }
                    }

                    MessageBoxExtend.messageOK("存货初始数据导入成功");
                }
            }
            catch (Exception)
            {
                MessageBoxExtend.messageWarning("文件导入失败，[" + Convert.ToString(rowIndex) + "]行数据有误，请仔细核对");
                return;
            }
        }

        public void materielOutStorage(int matetielID, double value)
        {
            string update = "UPDATE [dbo].[INIT_STORAGE_STOCK] SET ";

            update += "[VALUE] = " + value;
            update += " WHERE MATERIEL_ID = " + matetielID;

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }
        }

        public double getMarerielCountInfoFromMaterielID(int matetielID)
        {
            double value = 0;

            foreach (KeyValuePair<int, InitMaterielTable> index in m_materielStockList)
            {
                if (index.Value.materielID == matetielID)
                {
                    value = index.Value.value;
                    break;
                }
            }

            return value;
        }
    }

    public class InitMaterielTable
    {
        public int pkey { get; set; }
        public int materielID { get; set; }
        public double value { get; set; }
        public double price { get; set; }
        public string name { get; set; }
        public string unitPurchase { get; set; }
        public string storage { get; set; }
    }
}