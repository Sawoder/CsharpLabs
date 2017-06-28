chcp 861
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\MSTest.exe" /testcontainer:"C:\Users\delfi\Documents\Visual Studio 2017\Projects\ConsoleApp1\UnitTestProject1\bin\Debug\UnitTestProject1.dll" > tmp.txt
cmd /U /C type tmp.txt > UnitTestLog.txt
del tmp.txt