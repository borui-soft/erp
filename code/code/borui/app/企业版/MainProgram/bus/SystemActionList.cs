using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace MainProgram.bus
{
    public class UserInterfaceActonState
    {
        public UserInterfaceActonState()
        {
 
        }

        public static void setUserInterfaceActonState(object activeObject, string className, bool isEnable)
        {
            if (className == "ToolStripButton")
            {
                // 如果按钮的Enable状态已为false，则不再处理
                if(((System.Windows.Forms.ToolStripButton)activeObject).Enabled)
                {
                    ((System.Windows.Forms.ToolStripButton)activeObject).Enabled = isEnable;
                }
            }
            else if (className == "Panel")
            {
                if (((System.Windows.Forms.Panel)activeObject).Enabled)
                {
                    ((System.Windows.Forms.Panel)activeObject).Enabled = isEnable;
                }
            }
            else if (className == "Label")
            {
                if (((System.Windows.Forms.Label)activeObject).Enabled)
                {
                    ((System.Windows.Forms.Label)activeObject).Enabled = isEnable;
                }
            }
            else if (className == "Button")
            {
                if (((System.Windows.Forms.Button)activeObject).Enabled)
                {
                    ((System.Windows.Forms.Button)activeObject).Enabled = isEnable;
                }
            }
        }
    }
}