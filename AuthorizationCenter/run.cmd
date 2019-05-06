@echo off
echo ASPNETCORE_ENVIRONMENT=Development
dotnet build
start "AuthCenter" dotnet run
exit