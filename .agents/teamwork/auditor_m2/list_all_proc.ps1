Get-Process | Sort-Object ProcessName | Select-Object Id, ProcessName, MainWindowTitle | Format-Table -AutoSize
