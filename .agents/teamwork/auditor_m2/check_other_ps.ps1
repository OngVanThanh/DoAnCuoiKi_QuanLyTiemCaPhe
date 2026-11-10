$currentPid = $PID
Write-Host "Current PID: $currentPid"
$otherPs = Get-Process -Name powershell | Where-Object { $_.Id -ne $currentPid }
foreach ($p in $otherPs) {
    Write-Host "Other powershell PID: $($p.Id) (StartTime: $($p.StartTime))"
}
