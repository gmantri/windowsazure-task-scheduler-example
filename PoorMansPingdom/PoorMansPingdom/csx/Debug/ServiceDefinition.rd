<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="PoorMansPingdom" generation="1" functional="0" release="0" Id="1470a709-e736-4d4d-b2fe-945e5ece26c4" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="PoorMansPingdomGroup" generation="1" functional="0" release="0">
      <settings>
        <aCS name="PoorMansPingdomWorkerRole:BlobContainer" defaultValue="">
          <maps>
            <mapMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/MapPoorMansPingdomWorkerRole:BlobContainer" />
          </maps>
        </aCS>
        <aCS name="PoorMansPingdomWorkerRole:BlobToBeLeased" defaultValue="">
          <maps>
            <mapMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/MapPoorMansPingdomWorkerRole:BlobToBeLeased" />
          </maps>
        </aCS>
        <aCS name="PoorMansPingdomWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/MapPoorMansPingdomWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="PoorMansPingdomWorkerRole:PingItemsTableName" defaultValue="">
          <maps>
            <mapMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/MapPoorMansPingdomWorkerRole:PingItemsTableName" />
          </maps>
        </aCS>
        <aCS name="PoorMansPingdomWorkerRole:PingJobCronSchedule" defaultValue="">
          <maps>
            <mapMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/MapPoorMansPingdomWorkerRole:PingJobCronSchedule" />
          </maps>
        </aCS>
        <aCS name="PoorMansPingdomWorkerRole:PingResultsTableName" defaultValue="">
          <maps>
            <mapMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/MapPoorMansPingdomWorkerRole:PingResultsTableName" />
          </maps>
        </aCS>
        <aCS name="PoorMansPingdomWorkerRole:ProcessQueueName" defaultValue="">
          <maps>
            <mapMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/MapPoorMansPingdomWorkerRole:ProcessQueueName" />
          </maps>
        </aCS>
        <aCS name="PoorMansPingdomWorkerRole:StorageAccount" defaultValue="">
          <maps>
            <mapMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/MapPoorMansPingdomWorkerRole:StorageAccount" />
          </maps>
        </aCS>
        <aCS name="PoorMansPingdomWorkerRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/MapPoorMansPingdomWorkerRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <maps>
        <map name="MapPoorMansPingdomWorkerRole:BlobContainer" kind="Identity">
          <setting>
            <aCSMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/PoorMansPingdomWorkerRole/BlobContainer" />
          </setting>
        </map>
        <map name="MapPoorMansPingdomWorkerRole:BlobToBeLeased" kind="Identity">
          <setting>
            <aCSMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/PoorMansPingdomWorkerRole/BlobToBeLeased" />
          </setting>
        </map>
        <map name="MapPoorMansPingdomWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/PoorMansPingdomWorkerRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapPoorMansPingdomWorkerRole:PingItemsTableName" kind="Identity">
          <setting>
            <aCSMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/PoorMansPingdomWorkerRole/PingItemsTableName" />
          </setting>
        </map>
        <map name="MapPoorMansPingdomWorkerRole:PingJobCronSchedule" kind="Identity">
          <setting>
            <aCSMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/PoorMansPingdomWorkerRole/PingJobCronSchedule" />
          </setting>
        </map>
        <map name="MapPoorMansPingdomWorkerRole:PingResultsTableName" kind="Identity">
          <setting>
            <aCSMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/PoorMansPingdomWorkerRole/PingResultsTableName" />
          </setting>
        </map>
        <map name="MapPoorMansPingdomWorkerRole:ProcessQueueName" kind="Identity">
          <setting>
            <aCSMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/PoorMansPingdomWorkerRole/ProcessQueueName" />
          </setting>
        </map>
        <map name="MapPoorMansPingdomWorkerRole:StorageAccount" kind="Identity">
          <setting>
            <aCSMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/PoorMansPingdomWorkerRole/StorageAccount" />
          </setting>
        </map>
        <map name="MapPoorMansPingdomWorkerRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/PoorMansPingdomWorkerRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="PoorMansPingdomWorkerRole" generation="1" functional="0" release="0" software="D:\Projects\PoorMansPingdom\PoorMansPingdom\csx\Debug\roles\PoorMansPingdomWorkerRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="BlobContainer" defaultValue="" />
              <aCS name="BlobToBeLeased" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="PingItemsTableName" defaultValue="" />
              <aCS name="PingJobCronSchedule" defaultValue="" />
              <aCS name="PingResultsTableName" defaultValue="" />
              <aCS name="ProcessQueueName" defaultValue="" />
              <aCS name="StorageAccount" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;PoorMansPingdomWorkerRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;PoorMansPingdomWorkerRole&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/PoorMansPingdomWorkerRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/PoorMansPingdomWorkerRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/PoorMansPingdom/PoorMansPingdomGroup/PoorMansPingdomWorkerRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="PoorMansPingdomWorkerRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="PoorMansPingdomWorkerRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="PoorMansPingdomWorkerRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
</serviceModel>