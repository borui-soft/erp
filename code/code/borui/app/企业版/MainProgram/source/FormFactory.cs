using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace MainProgram
{
    class FormFactory
    {
        static private FormFactory m_instance = null;

        private FormFactory()
        {
        }

        static public FormFactory getInctance()
        {
            if (m_instance == null)
            {
                m_instance = new FormFactory();
            }

            return m_instance;
        }

        public Form getFormObjectFromName(string formName)
        {
            Assembly subPage = Assembly.Load("MainProgram");
            object obj = subPage.CreateInstance("MainProgram." + formName);

            return (Form)obj;
        }
    }
}