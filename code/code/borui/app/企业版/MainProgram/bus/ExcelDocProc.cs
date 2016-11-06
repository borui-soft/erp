using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace MainProgram.bus
{
    class ExcelDocProc
    {
        private bool m_isConnect = false;
        private OleDbConnection m_conn;
        private static ExcelDocProc m_Instance;

        public static ExcelDocProc getInstance()
        {
            if ( object.Equals(m_Instance, null) )
            {
                m_Instance = new ExcelDocProc();
            }
            return m_Instance;
        }

        public bool openFile(string filePath)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";

            m_conn = new OleDbConnection(strConn);

            if ( !m_isConnect )
            {
                m_conn.Open();
                m_isConnect = true;
            }

            return true;
        }

        public string getGridValue(string sheetName, int row, int column)
        {
            string value = "";
            string strExcel = "select * from  [" + sheetName + "$] ";
            OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, m_conn);

            DataSet ds = new DataSet();
            adapter.Fill(ds, "data");

            if ( (row < ds.Tables[0].Rows.Count) && (column < ds.Tables[0].Columns.Count) )
            {
                value = ds.Tables[0].Rows[row][column].ToString();
            }

            return value;
        }

        public void closeFile()
        {
            m_conn.Close();
            m_conn.Dispose();
            int id1 = System.GC.GetGeneration(m_Instance);
            int id2 = System.GC.GetGeneration(m_conn);
            m_conn = null;
            m_Instance = null;
            System.GC.Collect(id1);
            System.GC.Collect(id2);
        }
    }
}