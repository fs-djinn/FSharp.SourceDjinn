@echo off
setlocal

set BUILD_DIR=%~dp0.build
set TYPEMODEL_PROJ=%~dp0src\FSharp.SourceDjinn.TypeModel\FSharp.SourceDjinn.TypeModel.fsproj
set DJINN_PROJ=%~dp0src\FSharp.SourceDjinn\FSharp.SourceDjinn.fsproj

echo Cleaning .build folder...
if exist "%BUILD_DIR%" rd /s /q "%BUILD_DIR%"
mkdir "%BUILD_DIR%"

echo Cleaning Release builds...
dotnet clean "%TYPEMODEL_PROJ%" -c Release >nul 2>&1
dotnet clean "%DJINN_PROJ%" -c Release >nul 2>&1

echo Building TypeModel...
dotnet build "%TYPEMODEL_PROJ%" -c Release
if errorlevel 1 goto :fail

echo Packing TypeModel...
dotnet pack "%TYPEMODEL_PROJ%" -c Release /p:IsPack=true -o "%BUILD_DIR%" --no-build
if errorlevel 1 goto :fail

echo Building Djinn...
dotnet build "%DJINN_PROJ%" -c Release
if errorlevel 1 goto :fail

echo Packing Djinn...
dotnet pack "%DJINN_PROJ%" -c Release /p:IsPack=true -o "%BUILD_DIR%" --no-build
if errorlevel 1 goto :fail

echo.
echo Packages written to .build\
dir /b "%BUILD_DIR%\*.nupkg"
goto :end

:fail
echo Build failed.
exit /b 1

:end
endlocal
