rmdir /s /q coverage
rmdir /s /q coverage-coverlet-html
dotnet build DemoLibrary.sln --configuration Debug
dotnet test /p:CollectCoverage=true /p:MergeWith=../coverage/coverage.json /p:CoverletOutput=../coverage/ /p:CoverletOutputFormat=\"json,opencover,cobertura\" /p:Exclude="[xunit.*]*" DemoLibraryTests/DemoLibraryTests.csproj --no-build --configuration Debug
cd tools/coverage
dotnet reportgenerator "-reports:../../coverage/coverage.opencover.xml" "-targetdir:../../coverage-coverlet-html" -reporttypes:HTML;Badges    
