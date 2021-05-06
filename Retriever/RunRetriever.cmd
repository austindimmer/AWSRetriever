@echo off
set LOGFILE=Retriever.log
call :LOG > %LOGFILE%
exit /B

:LOG
cd C:\Dev\Euro\AWSRetriever\Retriever\bin\x64\Debug\
AWSRetriever.exe -r -p profile.comprehensive.js -o profile.comprehensive.output.json
pause