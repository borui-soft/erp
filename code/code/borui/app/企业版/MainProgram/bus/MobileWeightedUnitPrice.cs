using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainProgram.bus
{
    /* 
     * 此类主要用来完成，计算物料计价方式为移动加权的物料的当前单价
     */
    class MobileWeightedUnitPrice
    {
        private MobileWeightedUnitPrice()
        {
        }

        /*
         * 函数名：calculateMaterielNewPrice（针对采购入库业务）
         * 参数：
         * 1、oldPrice：本次入库前仓库系统中记录的该物料的单价
         * 2、oldMaterielCount：本次入库钱，仓存系统中记录的该物料的数量
         * 3、currnetInStoragePrice：本次采购该物料的单价
         * 4、currentInStorageCount：本次采购该物料的数量
         * 返回值：按照公式计算得到的新的加权单价
         * 计算公式：移动加权平均单价＝ (本次收入前结存商品金额+本次收入商品金额)/(本次收入前结存商品数量+本次收入商品数量 ) 
         * */
        static public double calculateMaterielNewPrice(double oldPrice, double oldMaterielCount, double currnetInStoragePrice, double currentInStorageCount)
        {
            double newPrice = 0.0;
            double oldMoney = oldPrice * oldMaterielCount;
            double newMoney = currnetInStoragePrice * currentInStorageCount;

            double moneyCount = oldMoney + newMoney;
            double materielSum = oldMaterielCount + currentInStorageCount;

            if (materielSum != 0)
            {
                newPrice = moneyCount / materielSum;

                // 保留2位小数儿
                newPrice = (double)(Math.Round(newPrice * 100)) / 100;
            }
            else 
            {
                newPrice = currnetInStoragePrice;
            }

            return newPrice;
        }

        /*
         * 函数名：calculateMaterielNewPriceOutStorage（针对采购退货业务）
         * 参数：
         * 1、oldPrice：本次入库前仓库系统中记录的该物料的单价
         * 2、oldMaterielCount：本次入库钱，仓存系统中记录的该物料的数量
         * 3、currnetInStoragePrice：本次采购该物料的单价
         * 4、currentInStorageCount：本次采购该物料的数量
         * 返回值：按照公式计算得到的新的加权单价
         * 计算公式：移动加权平均单价＝ (本次收入前结存商品金额+本次收入商品金额)/(本次收入前结存商品数量+本次收入商品数量 ) 
         * */
        static public double calculateMaterielNewPriceOutStorage(double oldPrice, double oldMaterielCount, double currnetInStoragePrice, double currentInStorageCount)
        {
            double newPrice = 0.0;
            double oldMoney = oldPrice * oldMaterielCount;
            double newMoney = currnetInStoragePrice * currentInStorageCount;

            double moneyCount = oldMoney - newMoney;
            double materielSum = oldMaterielCount - currentInStorageCount;

            if (moneyCount == 0 || materielSum == 0)
            {
                newPrice = 0.0;
            }
            else 
            {
                newPrice = moneyCount / materielSum;

                // 保留2位小数儿
                newPrice = (double)(Math.Round(newPrice * 100)) / 100;
            }

            return newPrice;
        }
    }
}
