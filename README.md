# nunit-engine-bug

The [NUnit-Engine](https://github.com/nunit/nunit-console) has a regression in version 3.18.3.
This project demonstrates the bug.

Set the version of `NUnit.Engine` in `nunit-regression-bug/AspectService/AspectService.csproj`
to reproduce the bug.

Build and run the code using the following commands:

```shell
dotnet build nunit-regression-bug/nunit-regression-bug.sln

./nunit-regression-bug/AspectService.Tests/bin/Debug/net8.0/AspectService \
    ./nunit-regression-bug/AspectService.Tests/bin/Debug/net8.0/AspectService.Tests.dll
```

This repository is dockerized and provides integration with VSCode to aid debugging.

With version 3.18.3 this generates output similar to:

```xml
<?xml version="1.0" encoding="utf-8"?>
<test-run id="0" name="AspectService.Tests.dll" fullname="/workspaces/nunit-bug-main/nunit-regression-bug/AspectService.Tests/bin/Debug/net8.0/AspectService.Tests.dll" runstate="Runnable" testcasecount="1" result="Failed" total="1" passed="0" failed="1" warnings="0" inconclusive="0" skipped="0" asserts="1" engine-version="3.18.3.0" clr-version="8.0.10" start-time="2024-11-15 12:53:23Z" end-time="2024-11-15 12:53:23Z" duration="0.103349">
  <command-line><![CDATA[/workspaces/nunit-bug-main/nunit-regression-bug/AspectService.Tests/bin/Debug/net8.0/AspectService.dll ./nunit-regression-bug/AspectService.Tests/bin/Debug/net8.0/AspectService.Tests.dll]]></command-line>
  <test-suite type="Assembly" id="0-1002" name="AspectService.Tests.dll" fullname="/workspaces/nunit-bug-main/nunit-regression-bug/AspectService.Tests/bin/Debug/net8.0/AspectService.Tests.dll" runstate="Runnable" testcasecount="1" result="Failed" site="Child" start-time="2024-11-15T12:53:23.5495624Z" end-time="2024-11-15T12:53:23.5742330Z" duration="0.024659" total="1" passed="0" failed="1" warnings="0" inconclusive="0" skipped="0" asserts="1">
    <environment framework-version="4.2.2.0" clr-version="8.0.10" os-version="Debian GNU/Linux 12 (bookworm)" platform="Unix" cwd="/workspaces/nunit-bug-main" machine-name="42f12b7d5a51" user="vscode" user-domain="42f12b7d5a51" culture="en-US" uiculture="en-US" os-architecture="x64" />
    <settings>
      <setting name="NumberOfTestWorkers" value="32" />
    </settings>
    <properties>
      <property name="_PID" value="47369" />
      <property name="_APPDOMAIN" value="AspectService" />
    </properties>
    <failure>
      <message><![CDATA[One or more child tests had errors]]></message>
    </failure>
    <test-suite type="TestSuite" id="0-1003" name="AspectService" fullname="AspectService" runstate="Runnable" testcasecount="1" result="Failed" site="Child" start-time="2024-11-15T12:53:23.5514056Z" end-time="2024-11-15T12:53:23.5742311Z" duration="0.022825" total="1" passed="0" failed="1" warnings="0" inconclusive="0" skipped="0" asserts="1">
      <failure>
        <message><![CDATA[One or more child tests had errors]]></message>
      </failure>
      <test-suite type="TestSuite" id="0-1004" name="Tests" fullname="AspectService.Tests" runstate="Runnable" testcasecount="1" result="Failed" site="Child" start-time="2024-11-15T12:53:23.5515349Z" end-time="2024-11-15T12:53:23.5742244Z" duration="0.022689" total="1" passed="0" failed="1" warnings="0" inconclusive="0" skipped="0" asserts="1">
        <failure>
          <message><![CDATA[One or more child tests had errors]]></message>
        </failure>
        <test-suite type="TestFixture" id="0-1000" name="Tests" fullname="AspectService.Tests.Tests" classname="AspectService.Tests.Tests" runstate="Runnable" testcasecount="1" result="Failed" site="Child" start-time="2024-11-15T12:53:23.5519914Z" end-time="2024-11-15T12:53:23.5741165Z" duration="0.022125" total="1" passed="0" failed="1" warnings="0" inconclusive="0" skipped="0" asserts="1">
          <failure>
            <message><![CDATA[One or more child tests had errors]]></message>
          </failure>
          <test-case id="0-1001" name="FailsWithNUnitEngine3_18_3" fullname="AspectService.Tests.Tests.FailsWithNUnitEngine3_18_3" methodname="FailsWithNUnitEngine3_18_3" classname="AspectService.Tests.Tests" runstate="Runnable" seed="1187169967" result="Failed" start-time="2024-11-15T12:53:23.5533866Z" end-time="2024-11-15T12:53:23.5731191Z" duration="0.019754" asserts="1">
            <failure>
              <message><![CDATA[  Assert.That(BeforeTest, Is.EqualTo(NUnitAspectAttribute.ExpectedValue))
  Expected: "Value used for StaticProperty"
  But was:  null
]]></message>
              <stack-trace><![CDATA[   at AspectService.Tests.Tests.FailsWithNUnitEngine3_18_3() in /workspaces/nunit-bug-main/nunit-regression-bug/AspectService.Tests/UnitTest1.cs:line 15
]]></stack-trace>
            </failure>
            <assertions>
              <assertion result="Failed">
                <message><![CDATA[  Assert.That(BeforeTest, Is.EqualTo(NUnitAspectAttribute.ExpectedValue))
  Expected: "Value used for StaticProperty"
  But was:  null
]]></message>
                <stack-trace><![CDATA[   at AspectService.Tests.Tests.FailsWithNUnitEngine3_18_3() in /workspaces/nunit-bug-main/nunit-regression-bug/AspectService.Tests/UnitTest1.cs:line 15
]]></stack-trace>
              </assertion>
            </assertions>
          </test-case>
        </test-suite>
      </test-suite>
    </test-suite>
  </test-suite>
</test-run>
```

With versions 3.18.1 and 3.18.2 the test passes:

```xml
<?xml version="1.0" encoding="utf-8"?>
<test-run id="0" name="AspectService.Tests.dll" fullname="/workspaces/nunit-bug-main/nunit-regression-bug/AspectService.Tests/bin/Debug/net8.0/AspectService.Tests.dll" runstate="Runnable" testcasecount="1" result="Passed" total="1" passed="1" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="1" engine-version="3.18.2.0" clr-version="8.0.10" start-time="2024-11-15 13:00:21Z" end-time="2024-11-15 13:00:21Z" duration="0.042647">
  <command-line><![CDATA[/workspaces/nunit-bug-main/nunit-regression-bug/AspectService.Tests/bin/Debug/net8.0/AspectService.dll ./nunit-regression-bug/AspectService.Tests/bin/Debug/net8.0/AspectService.Tests.dll]]></command-line>
  <test-suite type="Assembly" id="0-1002" name="AspectService.Tests.dll" fullname="/workspaces/nunit-bug-main/nunit-regression-bug/AspectService.Tests/bin/Debug/net8.0/AspectService.Tests.dll" runstate="Runnable" testcasecount="1" result="Passed" start-time="2024-11-15T13:00:21.0985600Z" end-time="2024-11-15T13:00:21.1051276Z" duration="0.006556" total="1" passed="1" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="1">
    <environment framework-version="4.2.2.0" clr-version="8.0.10" os-version="Debian GNU/Linux 12 (bookworm)" platform="Unix" cwd="/workspaces/nunit-bug-main" machine-name="42f12b7d5a51" user="vscode" user-domain="42f12b7d5a51" culture="en-US" uiculture="en-US" os-architecture="x64" />
    <settings>
      <setting name="NumberOfTestWorkers" value="32" />
    </settings>
    <properties>
      <property name="_PID" value="50598" />
      <property name="_APPDOMAIN" value="AspectService" />
    </properties>
    <test-suite type="TestSuite" id="0-1003" name="AspectService" fullname="AspectService" runstate="Runnable" testcasecount="1" result="Passed" start-time="2024-11-15T13:00:21.1003381Z" end-time="2024-11-15T13:00:21.1051257Z" duration="0.004788" total="1" passed="1" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="1">
      <test-suite type="TestSuite" id="0-1004" name="Tests" fullname="AspectService.Tests" runstate="Runnable" testcasecount="1" result="Passed" start-time="2024-11-15T13:00:21.1004805Z" end-time="2024-11-15T13:00:21.1051189Z" duration="0.004638" total="1" passed="1" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="1">
        <test-suite type="TestFixture" id="0-1000" name="Tests" fullname="AspectService.Tests.Tests" classname="AspectService.Tests.Tests" runstate="Runnable" testcasecount="1" result="Passed" start-time="2024-11-15T13:00:21.1008771Z" end-time="2024-11-15T13:00:21.1049953Z" duration="0.004118" total="1" passed="1" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="1">
          <test-case id="0-1001" name="FailsWithNUnitEngine3_18_3" fullname="AspectService.Tests.Tests.FailsWithNUnitEngine3_18_3" methodname="FailsWithNUnitEngine3_18_3" classname="AspectService.Tests.Tests" runstate="Runnable" seed="1722656862" result="Passed" start-time="2024-11-15T13:00:21.1020758Z" end-time="2024-11-15T13:00:21.1041311Z" duration="0.002075" asserts="1" />
        </test-suite>
      </test-suite>
    </test-suite>
  </test-suite>
</test-run>
```
