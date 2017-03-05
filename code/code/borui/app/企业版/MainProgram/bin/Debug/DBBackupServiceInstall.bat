E:\work\oa2010\code\btcc\app\MainProgram\bin\Debug\DBBackupService.exe install
@echo "准备停止服务程序..."
sc stop DBBackupService
@echo "设置允许与桌面进行交互方式允许"
sc config DBBackupServicetype= interact type= own
@echo "正在重新启动服务..."
sc start DBBackupService
@echo "启动服务成功！"