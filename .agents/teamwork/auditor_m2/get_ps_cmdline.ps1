Get-CimInstance Win32_Process | Where-Object { $_.Name -like "*powershell*" -or $_.Name -like "*DoAn*" } | Select-Object ProcessId, Name, CommandLine | Format-List
