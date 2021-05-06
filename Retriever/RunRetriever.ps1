# cd C:\Dev\Euro\AWSRetriever\Retriever\bin\Debug\
# AWSRetriever.exe -r -p profile.comprehensive.js -o profile.comprehensive.output.json
# -WorkingDirectory "C:\temp"
# -RedirectStandardOutput "AWSRetriever_Log.txt" -RedirectStandardError "AWSRetriever_ErrorLog.txt"
Start-Process -PSPath "C:\Dev\Euro\AWSRetriever\Retriever\bin\Release\AWSRetriever.exe" -ArgumentList "-r -p profile.comprehensive.js -o profile.comprehensive.output.json" -RedirectStandardOutput "AWSRetriever_Log.txt" -RedirectStandardError "AWSRetriever_ErrorLog.txt" -WorkingDirectory "C:\Dev\Euro\AWSRetriever\Retriever\bin\Release"