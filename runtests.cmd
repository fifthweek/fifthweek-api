@echo off
setlocal enabledelayedexpansion

pushd %1

set testsfailed=0

dir /S /P /b *.Tests.dll >testdlls.txt
for /F "tokens=*" %%A in (testdlls.txt) do (
  echo.%%A | findstr /C:"\\bin\\" 1>nul
  if ERRORLEVEL 1 (
    echo Skipping DLL in non-bin directory
  ) ELSE (
    call :ExecuteCmd vstest.console.exe %%A
    IF !ERRORLEVEL! NEQ 0 set testsfailed=1
  )
)

IF !testsfailed! NEQ 0 goto error

echo -------
echo ALL TESTS PASSED. EXITING WITH CODE 0.
echo -------

goto end

:: Execute command routine that will echo out when error
:ExecuteCmd
setlocal
set _CMD_=%*
call %_CMD_%
if "%ERRORLEVEL%" NEQ "0" echo Failed testsfailed=%ERRORLEVEL%, command=%_CMD_%
exit /b %ERRORLEVEL%

:error
endlocal
echo -------
echo SOME TESTS FAILED. EXITING WITH CODE 1.
echo -------
call :exitSetErrorLevel
call :exitFromFunction 2>nul

:exitSetErrorLevel
exit /b 1

:exitFromFunction
()

:end
endlocal
echo Finished successfully.
exit /b 0
