@echo off

dotnet build -c Release
if errorlevel 1 (
    echo Cannot build the managed version
    exit /b 1
)

dotnet publish /warnaserror /p:PublishProfile="Properties\PublishProfiles\win-x64.pubxml"
if errorlevel 1 (
    echo Cannot publish the Native AOT version
    exit /b 1
)

SET NATIVE_AOT_APP=bin\Release\net8.0\win-x64\publish\win-x64\NativeAotTestApp.exe
SET MANAGED_APP=bin\Release\net8.0\NativeAotTestApp.exe

echo ----------------------------------
echo Benchmarking PDF to PNG conversion
echo ----------------------------------

SET ITERATION_COUNT=1
SET INPUT_PDF_PAGE_INDEX=0
SET INPUT_PDF=..\PDF\3BigPreview.pdf
rem SET INPUT_PDF=..\PDF\Banner Edulink One.pdf

SET MANAGED_CMD="%MANAGED_APP% pdftopng %ITERATION_COUNT% bin\managed-result.png \"%INPUT_PDF%\" %INPUT_PDF_PAGE_INDEX%"
SET NATIVE_AOT_CMD="%NATIVE_AOT_APP% pdftopng %ITERATION_COUNT% bin\native-aot-result.png \"%INPUT_PDF%\" %INPUT_PDF_PAGE_INDEX%"

hyperfine --warmup 3 %MANAGED_CMD% %NATIVE_AOT_CMD%

echo ------------------------------
echo Benchmarking string processing
echo ------------------------------

SET ITERATION_COUNT=10000
SET INPUT_STR=AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAZZZZWABBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEERRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
SET MANAGED_CMD="%MANAGED_APP% stringcompress %ITERATION_COUNT% %INPUT_STR%"
SET NATIVE_AOT_CMD="%NATIVE_AOT_APP% stringcompress %ITERATION_COUNT% %INPUT_STR%"

hyperfine --warmup 3 %MANAGED_CMD% %NATIVE_AOT_CMD%

pause