@echo off
sc stop "AmiJukeBoxService"
timeout /t 3 /nobreak >nul
sc delete "AmiJukeBoxService"
echo Done.
pause
