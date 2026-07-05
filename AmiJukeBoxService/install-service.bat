@echo off
echo Installing Ami Jukebox Service...
sc create "AmiJukeBoxService" binPath="%~dp0AmiJukeBoxService.exe" DisplayName="Ami Jukebox Service" start=auto
echo Starting service...
sc start "AmiJukeBoxService"
echo Done! Available at: http://localhost:8083
pause
