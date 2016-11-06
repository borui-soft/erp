using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Diagnostics;
using TIV.Core.DatabaseAccess;
using MainProgram.bus;

namespace MainProgram.model
{
    public class Version
    {
        private Version()
        {

        }

        //V1.9.2
        //1、采购模块增加历史库存库存(物料)报表
        //2、销售模块增加历史库存(商品)报表

        //V1.9.3 2016.11.3
        //1、解决红字单据入账时候报错 列名'非数字'无效的问题，原因是计算新单价时除数为0导致

        /*1.9.4 版本更新记录 2016-11-5：
         * 1、当业务系统启动后，限制在期初成本调整页面直接增加物料库存信息
         * 2、当业务系统启动后，限制在期初成本调整页面直接删除物料库存信息
         * 3、当业务系统启动后，当对物料库存信息进行编辑时，只能修改物料单价，不能修改物料数量
         * 4、期初成本页面调整的物料单价信息，插入到存货明细表（STORAGE_STOCK_DETAIL），已解决实际库存和历史库存信息可能不对应的问题
         * 5、调整存货明细账界面小数显示规范，数量一律是3位小数，金额一律是2位小数
         */
    }
}