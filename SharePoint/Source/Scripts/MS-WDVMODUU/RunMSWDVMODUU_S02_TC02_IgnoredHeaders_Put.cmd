@echo off
pushd %~dp0
"%VS120COMNTOOLS%..\IDE\mstest" /test:Microsoft.Protocols.TestSuites.MS_WDVMODUU.S02_IgnoredHeaders.MSWDVMODUU_S02_TC02_IgnoredHeaders_Put /testcontainer:..\..\MS-WDVMODUU\TestSuite\bin\Debug\MS-WDVMODUU_TestSuite.dll /runconfig:..\..\MS-WDVMODUU\MS-WDVMODUU.testsettings /unique
pause