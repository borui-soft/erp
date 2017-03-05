@echo "准备停止服务程序..."
sc stop DBBackupService
@echo "设置允许与桌面进行交互方式允许"
sc config DBBackupServicetype= interact type= own
E:\work\oa2010\code\btcc\app\MainProgram\bin\Debug remove
