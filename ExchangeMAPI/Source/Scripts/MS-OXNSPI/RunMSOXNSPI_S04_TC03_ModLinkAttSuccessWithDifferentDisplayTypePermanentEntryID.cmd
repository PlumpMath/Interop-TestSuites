@echo off
pushd %~dp0
"%VS120COMNTOOLS%..\IDE\mstest" /test:Microsoft.Protocols.TestSuites.MS_OXNSPI.S04_ModifyProperty.MSOXNSPI_S04_TC03_ModLinkAttSuccessWithDifferentDisplayTypePermanentEntryID /testcontainer:..\..\MS-OXNSPI\TestSuite\bin\Debug\MS-OXNSPI_TestSuite.dll /runconfig:..\..\MS-OXNSPI\MS-OXNSPI.testsettings /unique
pause