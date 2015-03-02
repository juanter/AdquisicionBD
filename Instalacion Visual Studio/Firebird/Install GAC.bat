c:
rem cd "C:\Program Files (x86)\Microsoft SDKs\Windows\v8.1A\bin\NETFX 4.5.1 Tools\x64" 
cd "C:\Program Files (x86)\Microsoft SDKs\Windows\v8.1A\bin\NETFX 4.5.1 Tools"
gacutil /i "C:\Program Files (x86)\FirebirdClient\FirebirdSql.Data.FirebirdClient.dll"
gacutil /i "C:\Program Files (x86)\FirebirdDDEX\FirebirdSQl.VisualStudio.DataTools.dll"
gacutil /l FirebirdSql.Data.FirebirdClient
cd "C:\Program Files (x86)\Microsoft SDKs\Windows\v8.0A\bin\NETFX 4.0 Tools"
gacutil /i "C:\Program Files (x86)\FirebirdClient\FirebirdSql.Data.FirebirdClient.dll"
gacutil /i "C:\Program Files (x86)\FirebirdDDEX\FirebirdSQl.VisualStudio.DataTools.dll"
gacutil /l FirebirdSql.Data.FirebirdClient

notepad.exe "C:\Windows\Microsoft.NET\Framework\v4.0.30319\config\Machine.config"
notepad.exe "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\config\Machine.config"
echo Comprobar que existe y si no añadirlo:
echo <configuration>
echo   <configSections> 
echo   ...
echo     <section name="firebirdsql.data.firebirdclient" type="System.Data.Common.DbProviderConfigurationHandler, System.Data, Version=4.6.1.0, Culture=neutral, PublicKeyToken=3750abcc3150b00c"/>
echo   <configSections>
echo ... 
echo <DbProviderFactories>
echo ...
echo      <add name="FirebirdClient Data Provider" invariant="FirebirdSql.Data.FirebirdClient" description=".NET Framework Data Provider for Firebird" type="FirebirdSql.Data.FirebirdClient.FirebirdClientFactory, FirebirdSql.Data.FirebirdClient, Version=4.6.1.0, Culture=neutral, PublicKeyToken=3750abcc3150b00c" />
echo </DbProviderFactories>
pause