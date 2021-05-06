using Amazon;
using Amazon.IoTDeviceAdvisor;
using Amazon.IoTDeviceAdvisor.Model;
using Amazon.Runtime;

namespace CloudOps.IoTDeviceAdvisor
{
    public class ListSuiteDefinitionsOperation : Operation
    {
        public override string Name => "ListSuiteDefinitions";

        public override string Description => "Lists the Device Advisor test suites you have created.";
 
        public override string RequestURI => "/suiteDefinitions";

        public override string Method => "GET";

        public override string ServiceName => "IoTDeviceAdvisor";

        public override string ServiceID => "IotDeviceAdvisor";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTDeviceAdvisorConfig config = new AmazonIoTDeviceAdvisorConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTDeviceAdvisorClient client = new AmazonIoTDeviceAdvisorClient(creds, config);
            
            ListSuiteDefinitionsResponse resp = new ListSuiteDefinitionsResponse();
            do
            {
                ListSuiteDefinitionsRequest req = new ListSuiteDefinitionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListSuiteDefinitionsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SuiteDefinitionInformationList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}