# ==============================================================================
# Tier 1 - Test_Pbkdf2Security.ps1
# Rubric: Mã hóa, phân quyền (3.0 pts) - PBKDF2 100k Iterations, Salt, Timing-Attack Safe
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 1: PBKDF2 Encryption & Security Tests ---" -ForegroundColor Cyan

$asm = Get-ProjectAssembly
$secType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.SecurityHelper")
Assert-NotNull $secType "SecurityHelper class exists in assembly" "Tier 1" "PBKDF2_Security"

# Test 1.1: Cryptographic Salt Generation (16 bytes = 24 base64 chars)
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$salt1 = $secType.GetMethod("GenerateSalt").Invoke($null, @())
$salt2 = $secType.GetMethod("GenerateSalt").Invoke($null, @())
$sw.Stop()

Assert-NotNull $salt1 "GenerateSalt returns non-null string" "Tier 1" "PBKDF2_Security" -DurationMs $sw.TotalMilliseconds
$salt1Bytes = [Convert]::FromBase64String($salt1)
Assert-Equal $salt1Bytes.Length 16 "Salt size is exactly 16 bytes (128 bits)" "Tier 1" "PBKDF2_Security"
Assert-True ($salt1 -ne $salt2) "Consecutive salts are cryptographically random and unique" "Tier 1" "PBKDF2_Security"

# Test 1.2: PBKDF2 Password Hashing (32 bytes = 256 bits = 44 base64 chars)
$testPassword = "CafePassword2026!#"
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$hash = $secType.GetMethod("HashPassword").Invoke($null, @($testPassword, $salt1))
$sw.Stop()

Assert-NotNull $hash "HashPassword returns non-null string" "Tier 1" "PBKDF2_Security" -DurationMs $sw.TotalMilliseconds
$hashBytes = [Convert]::FromBase64String($hash)
Assert-Equal $hashBytes.Length 32 "Hash size is exactly 32 bytes (256-bit PBKDF2-SHA256)" "Tier 1" "PBKDF2_Security"

# Test 1.3: Verify Password - Valid Credentials
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$verifyValid = $secType.GetMethod("VerifyPassword").Invoke($null, @($testPassword, $hash, $salt1))
$sw.Stop()
Assert-True $verifyValid "VerifyPassword returns True for matching password, hash, and salt" "Tier 1" "PBKDF2_Security" -DurationMs $sw.TotalMilliseconds

# Test 1.4: Verify Password - Incorrect Password
$verifyWrong = $secType.GetMethod("VerifyPassword").Invoke($null, @("WrongPassword!", $hash, $salt1))
Assert-False $verifyWrong "VerifyPassword returns False for incorrect password" "Tier 1" "PBKDF2_Security"

# Test 1.5: Tamper Resistance - Altered Hash
$alteredHashBytes = [byte[]]$hashBytes.Clone()
$alteredHashBytes[0] = $alteredHashBytes[0] -bxor 0xFF
$tamperedHash = [Convert]::ToBase64String($alteredHashBytes)
$verifyTampered = $secType.GetMethod("VerifyPassword").Invoke($null, @($testPassword, $tamperedHash, $salt1))
Assert-False $verifyTampered "VerifyPassword rejects tampered hash" "Tier 1" "PBKDF2_Security"

# Test 1.6: Tamper Resistance - Altered Salt
$verifyWrongSalt = $secType.GetMethod("VerifyPassword").Invoke($null, @($testPassword, $hash, $salt2))
Assert-False $verifyWrongSalt "VerifyPassword rejects valid password with mismatched salt" "Tier 1" "PBKDF2_Security"
