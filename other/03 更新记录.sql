USE [ERP]

-- 2012-05-03 修改现金管理无法授权的问题
update ERP.DBO.base_module_list set name = '现金管理报表' where id = 504;

-- 2016-05-06 采购管理模块-库存表 添加是否能查看单价的权限(这个权限后期被应用于系统中所有和单价有关的权限控制)
insert into [BASE_MODULE_LIST] (ID, [name], sub_system_ID) values (105, '库存单价', 1);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('可查看', 'dispaly', 105);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('不可查看', 'dispaly', 105);

-- 2016-06-02 存货核算页面新增物料出入库核算，为其增加授权功能
INSERT INTO [ERP].[dbo].[BASE_ACTION_LIST]([ACTION_NAME],[MODULE_ID],[UI_ACTION_NAME])VALUES('出入库汇总',402,'labelMaterielInOutCount');


-- 2016-06-04 所有类型单据中的数量，允许输入小数，所以需要修改如下表的中的字段为float类型
ALTER TABLE [dbo].[PURCHASE_ORDER] ALTER COLUMN SUM_VALUE FLOAT;
ALTER TABLE [dbo].[PURCHASE_ORDER] ALTER COLUMN ACTUAL_VALUE FLOAT;
ALTER TABLE [dbo].[PURCHASE_ORDER_DETAILS] ALTER COLUMN VALUE FLOAT;
ALTER TABLE [dbo].[PURCHASE_IN_ORDER] ALTER COLUMN SUM_VALUE FLOAT;
ALTER TABLE [dbo].[PURCHASE_IN_ORDER_DETAILS] ALTER COLUMN VALUE FLOAT;
ALTER TABLE [dbo].[PURCHASE_SUPPLIER_PRICE_SHEET] ALTER COLUMN ORNM_FROM_VALUE FLOAT;
ALTER TABLE [dbo].[PURCHASE_SUPPLIER_PRICE_SHEET] ALTER COLUMN ORNM_TO_VALUE FLOAT;
ALTER TABLE [dbo].[SALE_ORDER] ALTER COLUMN SUM_VALUE FLOAT;
ALTER TABLE [dbo].[SALE_ORDER] ALTER COLUMN ACTUAL_VALUE FLOAT;
ALTER TABLE [dbo].[SALE_ORDER_DETAILS] ALTER COLUMN VALUE FLOAT;
ALTER TABLE [dbo].[SALE_OUT_ORDER] ALTER COLUMN SUM_VALUE FLOAT;
ALTER TABLE [dbo].[SALE_OUT_ORDER_DETAILS] ALTER COLUMN VALUE FLOAT;
ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_IN] ALTER COLUMN SUM_VALUE FLOAT;
ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_IN_DETAILS] ALTER COLUMN VALUE FLOAT;
ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_IN_EARNINGS] ALTER COLUMN SUM_VALUE FLOAT;
ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_IN_EARNINGS_DETAILS] ALTER COLUMN VALUE FLOAT;
ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_IN_OTHER] ALTER COLUMN SUM_VALUE FLOAT;
ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_IN_OTHER_DETAILS] ALTER COLUMN VALUE FLOAT;
ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_OUT] ALTER COLUMN SUM_VALUE FLOAT;
ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_OUT_DETAILS] ALTER COLUMN VALUE FLOAT;
ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_OUT_EARNINGS] ALTER COLUMN SUM_VALUE FLOAT;
ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_OUT_EARNINGS_DETAILS] ALTER COLUMN VALUE FLOAT;
ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_OUT_OTHER] ALTER COLUMN SUM_VALUE FLOAT;
ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_OUT_OTHER_DETAILS] ALTER COLUMN VALUE FLOAT;
ALTER TABLE [dbo].[STORAGE_STOCK_DETAIL] ALTER COLUMN VALUE FLOAT;
ALTER TABLE [dbo].[STORAGE_STOCK_DETAIL] ALTER COLUMN STORAGE_VALUE FLOAT;
ALTER TABLE [dbo].[INIT_STORAGE_STOCK] ALTER COLUMN VALUE FLOAT;


--2016-11-4 增加预占库存功能，增加预占库存表(WAREHOUSE_MANAGEMENT_PRO_OCCUPIED)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WAREHOUSE_MANAGEMENT_PRO_OCCUPIED]') AND type in (N'U'))
DROP TABLE [dbo].[WAREHOUSE_MANAGEMENT_PRO_OCCUPIED]
CREATE TABLE [dbo].[WAREHOUSE_MANAGEMENT_PRO_OCCUPIED](
    [PKEY] [int] IDENTITY(1,1) NOT NULL,
    [TRADING_DATE] [datetime] NOT NULL,
    [BILL_NUMBER] [nvarchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
    [EXCHANGES_UNIT] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NULL,
    [SUM_VALUE] [int] NOT NULL,
    [SUM_MONEY] [money] NOT NULL,
    [MAKE_ORDER_STAFF] [int] NOT NULL,
    [APPLY_STAFF] [int] NOT NULL,
    [ORDERR_REVIEW] [int] NULL,
    [REVIEW_DATE] [datetime] NULL,
    [IS_REVIEW] [int] NULL
) ON [PRIMARY];

--2016-11-4 增加预占库存功能，增加预占库存清单表(WAREHOUSE_MANAGEMENT_PRO_OCCUPIED_DETAILS)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WAREHOUSE_MANAGEMENT_PRO_OCCUPIED_DETAILS]') AND type in (N'U'))
DROP TABLE [dbo].[WAREHOUSE_MANAGEMENT_PRO_OCCUPIED_DETAILS]
CREATE TABLE [dbo].[WAREHOUSE_MANAGEMENT_PRO_OCCUPIED_DETAILS](
    [PKEY] [int] IDENTITY(1,1) NOT NULL,
    [ROW_NUMBER] [int] NOT NULL,
    [MATERIEL_ID] [int] NOT NULL,
    [BILL_NUMBER] [nvarchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
    [PRICE] [money] NOT NULL,
    [VALUE] [int] NOT NULL,
    [NOTE] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NULL,
        [IS_CANCEL] [int] NULL
) ON [PRIMARY];


ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_PRO_OCCUPIED] ALTER COLUMN SUM_VALUE FLOAT;
ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_PRO_OCCUPIED_DETAILS] ALTER COLUMN VALUE FLOAT;


--2017-1-11 物料列表（BASE_MATERIEL_LIST）增加物料编号字段，且助记码长度修改为10
ALTER TABLE [dbo].[BASE_MATERIEL_LIST] ADD NUM [nvarchar](20);
ALTER TABLE [dbo].[BASE_MATERIEL_LIST] ALTER COLUMN MNEMONIC_CODE [nvarchar](10);

--2017-1-11 物料分类（BASE_MATERIEL_TYPE）增加物料分组编号字段
ALTER TABLE [dbo].[BASE_MATERIEL_TYPE] ADD GROUP_NUM [nvarchar](10);


--2017-1-15 生产领料和其他出库 表头信息 增加项目编号和生产编号字段
ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_OUT] ADD PROJECT_NO [nvarchar](30) COLLATE Chinese_PRC_CI_AS NULL;
ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_OUT] ADD MAKE_NO [nvarchar](30) COLLATE Chinese_PRC_CI_AS NULL;

ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_OUT_OTHER] ADD PROJECT_NO [nvarchar](30) COLLATE Chinese_PRC_CI_AS NULL;
ALTER TABLE [dbo].[WAREHOUSE_MANAGEMENT_OUT_OTHER] ADD MAKE_NO [nvarchar](30) COLLATE Chinese_PRC_CI_AS NULL;


--2017-2-18 物料列表（BASE_MATERIEL_LIST）增加品牌字段
ALTER TABLE [dbo].[BASE_MATERIEL_LIST] ADD BRAND [nvarchar](20);

--2017-2-18 采购入库单信息表（PURCHASE_IN_ORDER）增加合同编号字段
ALTER TABLE [dbo].[PURCHASE_IN_ORDER] ADD CONTRACT_NUM [nvarchar](30);



--2017-3-5 增加采购申请单相关表格 增加预占库存功能，增加预占库存表(PURCHASE_APPLY_ORDER)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PURCHASE_APPLY_ORDER]') AND type in (N'U'))
DROP TABLE [dbo].[PURCHASE_APPLY_ORDER]
CREATE TABLE [dbo].[PURCHASE_APPLY_ORDER](
    [PKEY] [int] IDENTITY(1,1) NOT NULL,
    [APPLY_ID] [int] NOT NULL,
		[TRADING_DATE] [datetime] NOT NULL,
    [BILL_NUMBER] [nvarchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
    [PROJECT_NUM] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NULL,
    [PAYMENT_DATE] [datetime] NULL,
    [EXCHANGES_UNIT] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NULL,
    [SUM_VALUE] [float] NOT NULL,
    [SUM_MONEY] [money] NOT NULL,
    [SUM_OTHER_COST] [money] NOT NULL,
    [TOTAL_MONEY] [money] NOT NULL,
		[MAKE_ORDER_STAFF] [int] NOT NULL,
		[ORDERR_REVIEW] [int] NULL,
		[REVIEW_DATE] [datetime] NULL,
		[IS_REVIEW] [int] NOT NULL
) ON [PRIMARY];

--2017-3-5 采购申请单详情(PURCHASE_APPLY_ORDER_DETAILS)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PURCHASE_APPLY_ORDER_DETAILS]') AND type in (N'U'))
DROP TABLE [dbo].[PURCHASE_APPLY_ORDER_DETAILS]
CREATE TABLE [dbo].[PURCHASE_APPLY_ORDER_DETAILS](
	[PKEY] [int] IDENTITY(1,1) NOT NULL,
	[ROW_NUMBER] [int] NOT NULL,
	[MATERIEL_ID] [int] NOT NULL,
	[BILL_NUMBER] [nvarchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[PRICE] [nvarchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[VALUE] [int] NOT NULL,
	[OTHER_COST] [money] NULL
) ON [PRIMARY];

--2017-3-6 采购订单表中增加项目编号和原始单据编号字段(PURCHASE_ORDER)
ALTER TABLE [dbo].[PURCHASE_ORDER] ADD PROJECT_NUM [nvarchar](30);
ALTER TABLE [dbo].[PURCHASE_ORDER] ADD SRC_ORDER_NUM [nvarchar](30);


--2017-3-7 项目总材料管理表(PROJECT_MATERIE_MANAGER)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROJECT_MATERIE_MANAGER]') AND type in (N'U'))
DROP TABLE [dbo].[PROJECT_MATERIE_MANAGER]
CREATE TABLE [dbo].[PROJECT_MATERIE_MANAGER](
	[PKEY] [int] IDENTITY(1,1) NOT NULL,
	[DATE_TYPE] [int] NOT NULL,
	[DEVICE_MODE] [nvarchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[MAKE_DATE] [datetime] NOT NULL,
	[BILL_NUMBER] [nvarchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	
	[PROJECT_NUM] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[MAKE_NUM] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[DEVICE_NAME] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[NOTE] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	
	[MAKE_ORDER_STAFF] [int] NOT NULL,
	[DESIGN_ID] [int] NOT NULL,
	[REVIEW_STAFF_ID] [int] NULL,
	[REVIEW_DATE] [datetime] NULL,
	[IS_REVIEW] [int] NULL
) ON [PRIMARY];

--2017-3-8 项目总材料管理表详细内容(PROJECT_MATERIE_MANAGER_DETAILS)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROJECT_MATERIE_MANAGER_DETAILS]') AND type in (N'U'))
DROP TABLE [dbo].[PROJECT_MATERIE_MANAGER_DETAILS]
CREATE TABLE [dbo].[PROJECT_MATERIE_MANAGER_DETAILS](
	[PKEY] [int] IDENTITY(1,1) NOT NULL,
	[BILL_NUMBER] [nvarchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	
	[ROW_NUMBER] [int] NOT NULL,
	[MATERIEL_ID] [int] NOT NULL,
	[VALUE] [int] NOT NULL,
	[MAKE_TYPE] [nvarchar](10) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[MATERIEL_NOTE] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY];

--2017-3-8 采购申请单相关权限分配
insert into [BASE_MODULE_LIST] (ID, [name], sub_system_ID) values (106, '采购申请单', 1);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('新增', 'save', 106);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('审核', 'toolStripButtonReview', 106);

--2017-3-8 项目管理页面权限分配
insert Into [BASE_SUB_SYSTEM_LIST] ([name],ID) values ('项目管理', 8);

insert into [BASE_MODULE_LIST] (ID, [name], sub_system_ID) values (801, '设备总材料表', 8);
insert into [BASE_MODULE_LIST] (ID, [name], sub_system_ID) values (802, '电器总材料表', 8);
insert into [BASE_MODULE_LIST] (ID, [name], sub_system_ID) values (803, '工程总材料表', 8);
insert into [BASE_MODULE_LIST] (ID, [name], sub_system_ID) values (804, '材料表序时薄权限', 8);
insert into [BASE_MODULE_LIST] (ID, [name], sub_system_ID) values (805, '总材料表跟踪情况', 8);


insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('新增', 'save', 801);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('审核', 'toolStripButtonReview', 801);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('新增', 'save', 802);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('审核', 'toolStripButtonReview', 802);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('新增', 'save', 803);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('审核', 'toolStripButtonReview', 803);


insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('设备总材料表序时薄', 'labelPurchaseOrder', 804);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('电器总材料表序时薄', 'labelPurchaseIn', 804);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('工程总材料表序时薄', 'labelPurchaseInvoice', 804);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('总材料表跟踪', 'labelInventory', 805);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('设备总材料表跟踪', 'labelInventoryHistory', 805);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('电器总材料表跟踪', 'labelPurchaseOrderExecute', 805);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('工程总材料表跟踪', 'labelPurchaseInPayment', 805);



--2017-3-15 物料列表（BASE_MATERIEL_LIST）增加参数字段
ALTER TABLE [dbo].[BASE_MATERIEL_LIST] ADD PARAMETER [nvarchar](20);

--2017-3-15 采购入库单详细信息表（PURCHASE_IN_ORDER_DETAILS）增加合同编号字段
ALTER TABLE [dbo].[PURCHASE_IN_ORDER_DETAILS] ADD CONTRACT_MATERIEL_NAME [nvarchar](50);

--2017-3-15 xxx材料表录入界面，增加变更和审核变更按钮
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('申请变更', 'toolStripButtonChange', 801);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('审批变更', 'toolStripButtonChangeReview', 801);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('申请变更', 'toolStripButtonChange', 802);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('审批变更', 'toolStripButtonChangeReview', 802);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('申请变更', 'toolStripButtonChange', 803);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('审批变更', 'toolStripButtonChangeReview', 803);



--2017-3-16 项目管理表（PROJECT_MATERIE_MANAGER）增加变更申请人和变更审批人
ALTER TABLE [dbo].[PROJECT_MATERIE_MANAGER] ADD CHANGE_STAFF_ID [int] NULL;
ALTER TABLE [dbo].[PROJECT_MATERIE_MANAGER] ADD CHANGE_REVIEW_STAFF_ID [int] NULL;

ALTER TABLE [dbo].[PROJECT_MATERIE_MANAGER_DETAILS] ALTER COLUMN VALUE FLOAT;

--2017-3-20 采购申请（PURCHASE_APPLY_ORDER_DETAILS）VALUE 字段修改为float
ALTER TABLE [dbo].[PURCHASE_APPLY_ORDER_DETAILS] ALTER COLUMN VALUE FLOAT;

--2017-3-16 项目管理表（PROJECT_MATERIE_MANAGER）增加制单日期字段
ALTER TABLE [dbo].[PROJECT_MATERIE_MANAGER] ADD MAKE_ORDER_DATE [datetime] NULL;


-- 2017-3-29 材料变更申请页面增加权限控制
insert into [BASE_MODULE_LIST] (ID, [name], sub_system_ID) values (806, '总材料表变更申请单', 8);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('新增', 'save', 806);
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('审核', 'toolStripButtonReview', 806);

--2017-3-29 材料变更申请表(PROJECT_MATERIE_CHANGE_MANAGER)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROJECT_MATERIE_CHANGE_MANAGER]') AND type in (N'U'))
DROP TABLE [dbo].[PROJECT_MATERIE_CHANGE_MANAGER]
CREATE TABLE [dbo].[PROJECT_MATERIE_CHANGE_MANAGER](
	[PKEY] [int] IDENTITY(1,1) NOT NULL,
	[DATE_TYPE] [int] NOT NULL,
	[PROJECT_NUM] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[SRC_BILL_NUMBER] [nvarchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[BILL_NUMBER] [nvarchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[DESIGN_ID] [int] NOT NULL,
	[CHANGE_REASON] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[CHANGE_MATERIE_ID_S] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NULL,

	[MAKE_DATE] [datetime] NOT NULL,
	[MAKE_ORDER_STAFF] [int] NOT NULL,
	[REVIEW_STAFF_ID] [int] NULL,
	[REVIEW_DATE] [datetime] NULL,
	[IS_REVIEW] [int] NULL
) ON [PRIMARY];

-- 2017-3-29 用于控制总材变更申请序时薄查看权限
insert into [BASE_ACTION_LIST] ([action_name],[ui_action_name], module_ID) values ('总材变更申请序时薄', 'labelChangeApply', 804);