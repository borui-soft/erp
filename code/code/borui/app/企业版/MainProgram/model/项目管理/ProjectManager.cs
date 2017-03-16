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
    public class FormProject : ITableModel
    {
        private SortedDictionary<int, FormProjectMaterielTable> m_tableDataList = new SortedDictionary<int, FormProjectMaterielTable>();

        static private FormProject m_instance = null;

        private FormProject()
        {
            load();
        }

        static public FormProject getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new FormProject();
            }

            return m_instance;
        }

        public void refreshRecord()
        {
            load();
        }

        public void insert(FormProjectMaterielTable record, bool isDisplayMessageBox = true)
        {
            FormProjectMaterielTable tmpRecord  = new FormProjectMaterielTable();

            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
            if (checkBillIsExist(record.billNumber))
            {
                tmpRecord = getProjectInfoFromBillNumber(record.billNumber);
                delete(record.billNumber);
            }

            string insert = "INSERT INTO [ERP].[dbo].[PROJECT_MATERIE_MANAGER]([DATE_TYPE],[DEVICE_MODE],[MAKE_DATE],[BILL_NUMBER],[PROJECT_NUM],[MAKE_NUM],[DEVICE_NAME],[NOTE]";
            insert += ",[MAKE_ORDER_STAFF],[DESIGN_ID]";

            if (tmpRecord.billNumber != null && tmpRecord.changeStaffName.Length > 0)
            {
                insert += ", [CHANGE_STAFF_ID]";
            }

            if (tmpRecord.billNumber != null && tmpRecord.changeReviewStaffName.Length > 0)
            {
                insert += ", [CHANGE_REVIEW_STAFF_ID]";
            }

            insert += ")VALUES(";
            
            insert += record.dataType + ",";
            insert += "'" + record.deviceMode + "',";
            insert += "'" + record.makeDate + "',";
            insert += "'" + record.billNumber + "',";
            insert += "'" + record.projectNum + "',";
            insert += "'" + record.makeNum + "',";
            insert += "'" + record.deviceName + "',";
            insert += "'" + record.note + "',";

            insert += record.makeOrderStaffID + ",";
            insert += record.designStaffID;

            if (tmpRecord.billNumber != null && tmpRecord.changeStaffName.Length > 0)
            {
                insert += ", ";
                insert += tmpRecord.changeStaffID;
            }

            if (tmpRecord.billNumber != null && tmpRecord.changeReviewStaffName.Length > 0)
            {
                insert += ", ";
                insert += tmpRecord.changeReviewStaffID;
            }

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

            writeOperatorLog(101, OperatorLogType.Add, record.billNumber);
        }

        public void delete(string biilNumber)
        {
            string delete = "DELETE FROM PROJECT_MATERIE_MANAGER WHERE BILL_NUMBER = '" + biilNumber + "'"; 

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

        public void billReview(string billNumber)
        {
            string update = "UPDATE [dbo].[PROJECT_MATERIE_MANAGER] SET ";

            update += "[REVIEW_STAFF_ID] = " + DbPublic.getInctance().getCurrentLoginUserID() + ",";
            update += "REVIEW_DATE = '" + DateTime.Now.ToString("yyyyMMdd") + "', IS_REVIEW = 1";
            update += " WHERE BILL_NUMBER = '" + billNumber + "'";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                MessageBoxExtend.messageOK("单据[" + billNumber + "]审核成功");

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }

            writeOperatorLog(101, OperatorLogType.Change, billNumber);
        }

        public void billChange(string billNumber)
        {
            string update = "UPDATE [dbo].[PROJECT_MATERIE_MANAGER] SET ";

            update += "[CHANGE_STAFF_ID] = " + DbPublic.getInctance().getCurrentLoginUserID() + ",";
            update += "IS_REVIEW = 2";
            update += " WHERE BILL_NUMBER = '" + billNumber + "'";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                MessageBoxExtend.messageOK("单据[" + billNumber + "]已转换为等待变更审批状态!");

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }

            writeOperatorLog(101, OperatorLogType.ChangeReview, billNumber);
        }

        public void billChangeReview(string billNumber)
        {
            string update = "UPDATE [dbo].[PROJECT_MATERIE_MANAGER] SET ";

            update += "[CHANGE_REVIEW_STAFF_ID] = " + DbPublic.getInctance().getCurrentLoginUserID() + ",";
            update += "IS_REVIEW = 0";
            update += " WHERE BILL_NUMBER = '" + billNumber + "'";

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, update);

                MessageBoxExtend.messageOK("单据[" + billNumber + "]变更审批成功,再次进入可编辑修改状态");

                load();
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return;
            }

            writeOperatorLog(101, OperatorLogType.Review, billNumber);
        }

        private void load()
        {
            string sql = "SELECT [PKEY],[DATE_TYPE],[DEVICE_MODE],[MAKE_DATE],[BILL_NUMBER],[PROJECT_NUM],[MAKE_NUM],[DEVICE_NAME],[NOTE]";
            sql += ",[MAKE_ORDER_STAFF],[DESIGN_ID],[REVIEW_STAFF_ID],[REVIEW_DATE],[IS_REVIEW],[CHANGE_REVIEW_STAFF_ID],[CHANGE_STAFF_ID]  ";
            sql += "FROM [ERP].[dbo].[PROJECT_MATERIE_MANAGER] ORDER BY PKEY DESC";

            m_tableDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    FormProjectMaterielTable record = new FormProjectMaterielTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.dataType = DbDataConvert.ToInt32(row["DATE_TYPE"]);
                    record.deviceMode = DbDataConvert.ToString(row["DEVICE_MODE"]);
                    record.makeDate = DbDataConvert.ToDateTime(row["MAKE_DATE"]).ToString("yyyy-MM-dd");
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);
                    record.projectNum = DbDataConvert.ToString(row["PROJECT_NUM"]);
                    record.makeNum = DbDataConvert.ToString(row["MAKE_NUM"]);
                    record.deviceName = DbDataConvert.ToString(row["DEVICE_NAME"]);
                    record.note = DbDataConvert.ToString(row["NOTE"]);


                    record.makeOrderStaffID = DbDataConvert.ToInt32(row["MAKE_ORDER_STAFF"]);
                    record.makeOrderStaffName = Staff.getInctance().getStaffNameFromPkey(record.makeOrderStaffID);
                    record.designStaffID = DbDataConvert.ToInt32(row["DESIGN_ID"]);
                    record.designStaffName = Staff.getInctance().getStaffNameFromPkey(record.designStaffID);

                    // 变更和变更审核人
                    if (DbDataConvert.ToString(row["CHANGE_REVIEW_STAFF_ID"]).Length > 0)
                    {
                        record.changeReviewStaffID = DbDataConvert.ToInt32(row["CHANGE_REVIEW_STAFF_ID"]);
                        record.changeReviewStaffName = Staff.getInctance().getStaffNameFromPkey(record.changeReviewStaffID);
                    }
                    else
                    {
                        record.changeReviewStaffName = "";
                    }

                    if (DbDataConvert.ToString(row["CHANGE_STAFF_ID"]).Length > 0)
                    {
                        record.changeStaffID = DbDataConvert.ToInt32(row["CHANGE_STAFF_ID"]);
                        record.changeStaffName = Staff.getInctance().getStaffNameFromPkey(record.changeStaffID);
                    }
                    else 
                    {
                        record.changeStaffName = "";
                    }

                    if (DbDataConvert.ToString(row["REVIEW_STAFF_ID"]).Length > 0)
                    {
                        record.orderrReview = DbDataConvert.ToInt32(row["REVIEW_STAFF_ID"]);
                        record.orderrReviewName = Staff.getInctance().getStaffNameFromPkey(record.orderrReview);
                        record.reviewDate = DbDataConvert.ToDateTime(row["REVIEW_DATE"]).ToString("yyyy-MM-dd");
                    }

                    record.isReview = DbDataConvert.ToString(row["IS_REVIEW"]);

                    m_tableDataList.Add(m_tableDataList.Count, record);
                }
            }
        }

        public SortedDictionary<int, FormProjectMaterielTable> getAllPurchaseOrderInfo(int dataType)
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            SortedDictionary<int, FormProjectMaterielTable> list = new SortedDictionary<int, FormProjectMaterielTable>();

            foreach (KeyValuePair<int, FormProjectMaterielTable> index in m_tableDataList)
            {
                FormProjectMaterielTable record = new FormProjectMaterielTable();
                record = index.Value;

                if (index.Value.dataType == dataType)
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public SortedDictionary<int, FormProjectMaterielTable> getAllPurchaseOrderInfo(string projectNum, string reviewStatus, int dataType)
        {
            if (m_tableDataList.Count == 0)
            {
                load();
            }

            SortedDictionary<int, FormProjectMaterielTable> list = new SortedDictionary<int, FormProjectMaterielTable>();

            foreach (KeyValuePair<int, FormProjectMaterielTable> index in m_tableDataList)
            {
                FormProjectMaterielTable record = new FormProjectMaterielTable();
                record = index.Value;

                if (index.Value.projectNum == projectNum)
                {
                    if (dataType == 4)
                    {
                        if (reviewStatus == "0")
                        {
                            if (index.Value.isReview == "1")
                            {
                                list.Add(list.Count, index.Value);
                            }
                        }
                        else
                        {
                            list.Add(list.Count, index.Value);
                        }
                    }
                    else
                    {
                        if (index.Value.dataType == dataType)
                        {
                            if (reviewStatus == "0")
                            {
                                if (index.Value.isReview == "1")
                                {
                                    list.Add(list.Count, index.Value);
                                }
                            }
                            else
                            {
                                list.Add(list.Count, index.Value);
                            }
                        }
                    }
                }
            }

            return list;
        }

        public SortedDictionary<int, FormProjectMaterielTable> getAllReviewPurchaseOrderInfo()
        {
            SortedDictionary<int, FormProjectMaterielTable> list = new SortedDictionary<int, FormProjectMaterielTable>();

            foreach (KeyValuePair<int, FormProjectMaterielTable> index in m_tableDataList)
            {
                FormProjectMaterielTable record = new FormProjectMaterielTable();
                record = index.Value;

                if (index.Value.isReview == "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public SortedDictionary<int, FormProjectMaterielTable> getAllNotReviewPurchaseOrderInfo()
        {
            SortedDictionary<int, FormProjectMaterielTable> list = new SortedDictionary<int, FormProjectMaterielTable>();

            foreach (KeyValuePair<int, FormProjectMaterielTable> index in m_tableDataList)
            {
                FormProjectMaterielTable record = new FormProjectMaterielTable();
                record = index.Value;

                if (index.Value.isReview != "1")
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public FormProjectMaterielTable getProjectInfoFromBillNumber(string billNumber)
        {
            FormProjectMaterielTable record = new FormProjectMaterielTable();

            foreach (KeyValuePair<int, FormProjectMaterielTable> index in m_tableDataList)
            {
                if (index.Value.billNumber == billNumber)
                {
                    record = index.Value;
                    break;
                }
            }

            return record;
        }

        public bool checkBillIsExist(string billNumber)
        {
            bool isRet = false;

            foreach (KeyValuePair<int, FormProjectMaterielTable> index in m_tableDataList)
            {
                FormProjectMaterielTable record = new FormProjectMaterielTable();

                if (index.Value.billNumber == billNumber)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }
    }

    public class FormProjectMaterielTable
    {
        public int pkey { get; set; }
        public int dataType { get; set; }

        // 设备型号、制单日期、单据编号
        public string deviceMode { get; set; }
        public string makeDate { get; set; }
        public string billNumber { get; set; }

        // 项目编号、生产编号、所属部件、摘要
        public string projectNum { get; set; }
        public string makeNum { get; set; }
        public string deviceName { get; set; }
        public string note { get; set; }

        // 制单人、设计人、审核人
        public int makeOrderStaffID { get; set; }
        public string makeOrderStaffName { get; set; }
        public int designStaffID { get; set; }
        public string designStaffName { get; set; }
        public int orderrReview { get; set; }
        public string orderrReviewName { get; set; }
        public string reviewDate { get; set; }
        public string isReview { get; set; }

        // 变更和变更审核人
        public int changeStaffID { get; set; }
        public string changeStaffName { get; set; }
        public int changeReviewStaffID { get; set; }
        public string changeReviewStaffName { get; set; }
    }
}