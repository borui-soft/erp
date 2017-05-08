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
    public class FormProjectInfoChange : ITableModel
    {
        private SortedDictionary<int, FormProjectMaterielChangeTable> m_tableDataList = new SortedDictionary<int, FormProjectMaterielChangeTable>();

        static private FormProjectInfoChange m_instance = null;

        private FormProjectInfoChange()
        {
            load();
        }

        static public FormProjectInfoChange getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new FormProjectInfoChange();
            }

            return m_instance;
        }

        public void refreshRecord()
        {
            load();
        }

        public void insert(FormProjectMaterielChangeTable record, bool isDisplayMessageBox = true)
        {
            // 根据单据编号，判断库中是否已经存在该单据 如果存在单据首先删除单据，然后再执行插入操作
            if (checkBillIsExist(record.billNumber))
            {
                delete(record.billNumber);
            }

            string insert = "INSERT INTO [ERP].[dbo].[PROJECT_MATERIE_CHANGE_MANAGER]([DATE_TYPE],[PROJECT_NUM],[SRC_BILL_NUMBER],[BILL_NUMBER],[DESIGN_ID]";
            insert += ",[CHANGE_REASON],[CHANGE_MATERIE_ID_S],[MAKE_DATE],[MAKE_ORDER_STAFF]";

            insert += ")VALUES(";
            
            insert += record.dataType + ",";
            insert += "'" + record.projectNum + "',";
            insert += "'" + record.srcBillNumber + "',";
            insert += "'" + record.billNumber + "',";
            insert += record.designStaffID + ",";
            insert += "'" + record.changeReason + "',";
            insert += "'" + record.materielIDs + "',";
            insert += "'" + record.makeOrderDate + "',";
            insert += record.makeOrderStaffID + ")";

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

            writeOperatorLog(801, OperatorLogType.Add, record.billNumber);
        }

        public SortedDictionary<int, FormProjectMaterielChangeTable> getAllChangeRecord()
        {
            if (m_tableDataList.Count <= 0)
            {
                load();
            }

            return m_tableDataList;
        }

        public void delete(string biilNumber)
        {
            string delete = "DELETE FROM PROJECT_MATERIE_CHANGE_MANAGER WHERE BILL_NUMBER = '" + biilNumber + "'"; 

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
            string update = "UPDATE [dbo].[PROJECT_MATERIE_CHANGE_MANAGER] SET ";

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

        private void load()
        {
            string sql = "SELECT [PKEY],[DATE_TYPE],[PROJECT_NUM],[SRC_BILL_NUMBER],[BILL_NUMBER],[DESIGN_ID],[CHANGE_REASON],[CHANGE_MATERIE_ID_S]";
            sql += ",[MAKE_DATE],[MAKE_ORDER_STAFF],[REVIEW_STAFF_ID],[REVIEW_DATE],[IS_REVIEW] FROM [ERP].[dbo].[PROJECT_MATERIE_CHANGE_MANAGER] ";
            sql += "ORDER BY PKEY DESC";

            m_tableDataList.Clear();

            using (DataTable dataTable = DatabaseAccessFactoryInstance.Instance.QueryDataTable(FormMain.DB_NAME, sql))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    FormProjectMaterielChangeTable record = new FormProjectMaterielChangeTable();

                    record.pkey = DbDataConvert.ToInt32(row["PKEY"]);
                    record.dataType = DbDataConvert.ToInt32(row["DATE_TYPE"]);
                    record.projectNum = DbDataConvert.ToString(row["PROJECT_NUM"]);
                    record.srcBillNumber = DbDataConvert.ToString(row["SRC_BILL_NUMBER"]);
                    record.billNumber = DbDataConvert.ToString(row["BILL_NUMBER"]);
                    record.designStaffID = DbDataConvert.ToInt32(row["DESIGN_ID"]);
                    record.designStaffName = Staff.getInctance().getStaffNameFromPkey(record.designStaffID);
                    record.changeReason = DbDataConvert.ToString(row["CHANGE_REASON"]);
                    record.materielIDs = DbDataConvert.ToString(row["CHANGE_MATERIE_ID_S"]);

                    record.makeOrderStaffID = DbDataConvert.ToInt32(row["MAKE_ORDER_STAFF"]);
                    record.makeOrderStaffName = Staff.getInctance().getStaffNameFromPkey(record.makeOrderStaffID);
                    string makeOrderDate = DbDataConvert.ToString(row["MAKE_DATE"]);
                    if (makeOrderDate.Length > 0)
                    {
                        record.makeOrderDate = DbDataConvert.ToDateTime(row["MAKE_DATE"]).ToString("yyyy-MM-dd");
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

        public SortedDictionary<int, FormProjectMaterielChangeTable> getChangeListFromSrcBillNumber(string srcBillNumber)
        {
            SortedDictionary<int, FormProjectMaterielChangeTable> list = new SortedDictionary<int, FormProjectMaterielChangeTable>();

            if (m_tableDataList.Count <= 0)
            {
                load();
            }

            foreach (KeyValuePair<int, FormProjectMaterielChangeTable> index in m_tableDataList)
            {
                if (index.Value.srcBillNumber == srcBillNumber)
                {
                    list.Add(list.Count, index.Value);
                }
            }

            return list;
        }

        public SortedDictionary<int, ProjectManagerDetailsTable> getMaterielDetailsFromSrcBillNumber(string srcBillNumber)
        {
            SortedDictionary<int, ProjectManagerDetailsTable> materielDetails = new SortedDictionary<int, ProjectManagerDetailsTable>();

            SortedDictionary<int, FormProjectMaterielChangeTable> list = new SortedDictionary<int, FormProjectMaterielChangeTable>();

            if (m_tableDataList.Count <= 0)
            {
                load();
            }

            // 根据原始单据号，找到对应的变更单
            foreach (KeyValuePair<int, FormProjectMaterielChangeTable> index in m_tableDataList)
            {
                if (index.Value.srcBillNumber == srcBillNumber)
                {
                    list.Add(list.Count, index.Value);
                }
            }

            // 根据变更单据找到详细数据
            foreach (KeyValuePair<int, FormProjectMaterielChangeTable> index in list)
            {
                SortedDictionary<int, ProjectManagerDetailsTable> tmp = ProjectManagerDetails.getInctance().getPurchaseInfoFromBillNumber(index.Value.billNumber);

                foreach (KeyValuePair<int, ProjectManagerDetailsTable> index2 in tmp)
                {
                    int sign = PublicFuction.getXXMateaielOrderSign(index2.Value.rowNumber, index2.Value.sequence, index2.Value.no);
                    materielDetails[sign] = index2.Value;
                }
            }

            return materielDetails;
        }

        public FormProjectMaterielChangeTable getProjectInfoFromBillNumber(string billNumber)
        {
            FormProjectMaterielChangeTable record = new FormProjectMaterielChangeTable();

            foreach (KeyValuePair<int, FormProjectMaterielChangeTable> index in m_tableDataList)
            {
                if (index.Value.billNumber == billNumber)
                {
                    record = index.Value;
                    break;
                }
            }

            return record;
        }

        public string getxxMaterielNumberFromBillNumber(string billNumber)
        {
            string srcNumber = "";
            foreach (KeyValuePair<int, FormProjectMaterielChangeTable> index in m_tableDataList)
            {
                if (index.Value.billNumber == billNumber)
                {
                    srcNumber = index.Value.srcBillNumber;
                    break;
                }
            }

            return srcNumber;
        }

        public bool checkBillIsExist(string billNumber)
        {
            bool isRet = false;

            foreach (KeyValuePair<int, FormProjectMaterielChangeTable> index in m_tableDataList)
            {
                FormProjectMaterielChangeTable record = new FormProjectMaterielChangeTable();

                if (index.Value.billNumber == billNumber)
                {
                    isRet = true;
                    break;
                }
            }

            return isRet;
        }
    }

    public class FormProjectMaterielChangeTable
    {
        public int pkey { get; set; }
        public int dataType { get; set; }

        // 设备型号、使用日期、单据编号
        public string projectNum { get; set; }
        public string srcBillNumber { get; set; }
        public string billNumber { get; set; }
        public string materielIDs { get; set; }
        public string changeReason { get; set; }

        // 制单人、设计人、审核人
        public int makeOrderStaffID { get; set; }
        public string makeOrderStaffName { get; set; }
        public string makeOrderDate { get; set; }
        public int designStaffID { get; set; }
        public string designStaffName { get; set; }
        public int orderrReview { get; set; }
        public string orderrReviewName { get; set; }
        public string reviewDate { get; set; }
        public string isReview { get; set; }
    }
}