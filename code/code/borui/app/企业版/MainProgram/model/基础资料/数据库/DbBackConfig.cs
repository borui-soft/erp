using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using MainProgram.bus;

namespace MainProgram.model
{
    public class DbBackConfig : ITableModel
    {
        static private DbBackConfig m_instance = null;

        private DbBackConfig()
        {
        }

        static public DbBackConfig getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new DbBackConfig();
            }

            return m_instance;
        }

        public int getRecordCount()
        {
            return DbPublic.getInctance().tableRecordCount("SELECT COUNT(*) FROM BASE_DB_BACKUP_CONFIG");
        }
    }
}