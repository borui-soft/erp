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
    public class ITableModel
    {
        protected enum OperatorLogType
        {
            Add,
            Review,
            Change,
            ChangeReview,
            Register
        }

        protected bool deleteRecord(string tableName, int pkey, bool isShowMessage = true)
        {
            string delete = "DELETE FROM [dbo].[" + tableName + "] WHERE PKEY = " + Convert.ToString(pkey);

            try
            {
                DatabaseAccessFactoryInstance.Instance.ExecuteCommand(FormMain.DB_NAME, delete);

                if (isShowMessage)
                {
                    MessageBoxExtend.messageOK("数据删除成功");
                }

                return true;
            }
            catch (Exception error)
            {
                MessageBoxExtend.messageWarning(error.Message);
                return false;
            }
        }

        protected void writeOperatorLog(int billType, OperatorLogType type, string billNumber, string otherContent = "")
        {
            string logDesc = "";

            if (type == OperatorLogType.Add)
            {
                logDesc = "新增单据[";
            }
            else if (type == OperatorLogType.Review)
            {
                logDesc = "审核单据[";
            }
            else if (type == OperatorLogType.Change)
            {
                logDesc = "申请变更单据[";
            }
            else if (type == OperatorLogType.ChangeReview)
            {
                logDesc = "审批变更单据[";
            }
            else 
            {
                logDesc = "单据入账[";
            }

            logDesc += billNumber + "]" + otherContent;


            OperatorLog.getInctance().insert(billType, logDesc);
        }
    }
}
