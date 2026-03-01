@echo off
setlocal

REM Project paths
set TYPEMODEL_PROJ=C:\_github\FSharp.SourceDjinn\src\FSharp.SourceDjinn.TypeModel\FSharp.SourceDjinn.TypeModel.fsproj
set DJINN_PROJ=C:\_github\FSharp.SourceDjinn\src\FSharp.SourceDjinn\FSharp.SourceDjinn.fsproj

REM Output paths
set TYPEMODEL_OUT=C:\_github\FSharp.SourceDjinn\src\FSharp.SourceDjinn.TypeModel\bin\Release
set DJINN_OUT=C:\_github\FSharp.SourceDjinn\src\FSharp.SourceDjinn\bin\Release

REM Destination paths
set TYPEMODEL_DEST=C:\_github\Serde.FS\.nuget-local\FSharp.SourceDjinn.TypeModel
set DJINN_DEST=C:\_github\Serde.FS\.nuget-local\FSharp.SourceDjinn

echo Building TypeModel...
dotnet pack "%TYPEMODEL_PROJ%" -c Release /p:IsPack=true

echo Building Djinn...
dotnet pack "%DJINN_PROJ%" -c Release /p:IsPack=true

echo Creating destination folders...
mkdir "%TYPEMODEL_DEST%" 2>nul
mkdir "%DJINN_DEST%" 2>nul

echo Copying TypeModel packages...
copy "%TYPEMODEL_OUT%\*.nupkg" "%TYPEMODEL_DEST%" /Y

echo Copying Djinn packages...
copy "%DJINN_OUT%\*.nupkg" "%DJINN_DEST%" /Y

echo Done.
endlocal
pause
