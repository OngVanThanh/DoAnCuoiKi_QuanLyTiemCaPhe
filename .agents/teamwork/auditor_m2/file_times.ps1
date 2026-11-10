Get-ChildItem -Path . -Filter "*.*" | Where-Object { $_.LastWriteTime -gt (Get-Date).AddHours(-5) } | Select-Object LastWriteTime, Name
