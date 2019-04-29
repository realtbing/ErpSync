@echo off
echo ASPNETCORE_ENVIRONMENT=Development
dotnet build 
start "Api" dotnet run
exit