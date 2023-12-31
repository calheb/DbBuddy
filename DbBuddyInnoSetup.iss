; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "DbBuddy"
#define MyAppVersion "0.1.1"
#define MyAppPublisher "Caleb Hebert"
#define MyAppURL "https://github.com/calheb/DbBuddy"
#define MyAppExeName "dbb.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{9641CFA6-BBD7-452B-B440-E081470B1F5F}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=C:\Users\Caleb\source\repos\DbBuddy\LICENSE.txt
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
OutputDir=C:\Users\Caleb\Desktop
OutputBaseFilename=DbBuddy_v0.1.1_windows
;SetupIconFile=DbBuddy_v1.0_windows_x64
Compression=lzma
SolidCompression=yes
WizardStyle=modern
AlwaysRestart=yes


[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
;Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\Caleb\source\repos\DbBuddy\DbBuddy\bin\Release\net8.0\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Caleb\source\repos\DbBuddy\DbBuddy\bin\Release\net8.0\connectionStrings.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Caleb\source\repos\DbBuddy\DbBuddy\bin\Release\net8.0\dbb.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Caleb\source\repos\DbBuddy\DbBuddy\bin\Release\net8.0\dbb.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Caleb\source\repos\DbBuddy\DbBuddy\bin\Release\net8.0\dbb.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Caleb\source\repos\DbBuddy\DbBuddy\bin\Release\net8.0\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
;Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Code]
function NeedsAddPathHKCU(Param: string): boolean;
var
OrigPath: string;
begin
if not RegQueryStringValue(HKEY_CURRENT_USER,
'Environment',
'Path', OrigPath)
then begin
Result := True;
exit;
end;
// look for the path with leading and trailing semicolon
// Pos() returns 0 if not found
Result := Pos(';' + Param + ';', ';' + OrigPath + ';') = 0;
end;

[Registry]
Root: "HKCU"; Subkey: "Environment"; ValueType: expandsz; ValueName: "Path"; ValueData: "{olddata};{app}"; Check: NeedsAddPathHKCU(ExpandConstant('{app}'))



