Get-Process | ForEach-Object {
    try {
        $proc = $_
        foreach ($mod in $proc.Modules) {
            if ($mod.FileName -like "*DoAnCuoiKi*") {
                Write-Host "Found locking process: PID $($proc.Id), Name $($proc.ProcessName), Module $($mod.FileName)"
            }
        }
    } catch {
        # ignore access denied
    }
}
