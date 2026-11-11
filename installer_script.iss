; =====================================================================
; INNO SETUP SCRIPT - THE COFFEE HOME POS (HỆ THỐNG QUẢN LÝ TIỆM CÀ PHÊ)
; Tự động đóng gói phần mềm thành bộ cài đặt Setup.exe chuyên nghiệp
; Tự động kiểm tra môi trường, khởi động LocalDB và kích hoạt Zero-Config CSDL
; =====================================================================

#define MyAppName "The Coffee Home"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "The Coffee Home Team"
#define MyAppURL "https://github.com/thecoffeehome"
#define MyAppExeName "DoAnCuoiKi_QuanLyTiemCaPhe.exe"

[Setup]
AppId={{5E4B2C89-7D1A-4F3E-9A5C-8F6B4E3D2A1C}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\TheCoffeeHome
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
OutputDir=Release_Package
OutputBaseFilename=TheCoffeeHome_Setup_v1.0
SetupIconFile=Images\AppIcon.ico
UninstallDisplayIcon={app}\{#MyAppExeName}
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
ArchitecturesInstallIn64BitMode=x64
PrivilegesRequired=lowest
DisableProgramGroupPage=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; Thư mục chạy Release
Source: "Release_Package\TheCoffeeHome_Portable\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; Gói cài đặt LocalDB (nếu người dùng copy SqlLocalDB.msi vào thư mục Redist thì sẽ tự động kèm vào)
; Source: "Redist\SqlLocalDB.msi"; DestDir: "{tmp}"; Flags: deleteafterinstall ignoreversion; Check: HasLocalDBInstaller

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\Images\AppIcon.ico"
Name: "{group}\Gỡ cài đặt {#MyAppName}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\Images\AppIcon.ico"; Tasks: desktopicon

[Run]
; Khởi động LocalDB nếu có sẵn
Filename: "sqllocaldb"; Parameters: "start MSSQLLocalDB"; Flags: runhidden; StatusMsg: "Đang kiểm tra và khởi động dịch vụ cơ sở dữ liệu ngầm..."; Check: CheckHasSqlLocalDB
; Khởi chạy ứng dụng ngay sau khi cài đặt xong
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
// Kiểm tra máy tính đã có lệnh sqllocaldb hay chưa
function CheckHasSqlLocalDB(): Boolean;
var
  ResultCode: Integer;
begin
  Result := Exec('cmd.exe', '/c sqllocaldb info', '', SW_HIDE, ewWaitUntilTerminated, ResultCode) and (ResultCode = 0);
end;

// Thông báo hướng dẫn người dùng khi cài đặt hoàn tất
procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then
  begin
    // Đảm bảo thư mục dữ liệu có quyền ghi
  end;
end;
