using Amazon;
using Amazon.IoTDeviceAdvisor;
using Amazon.IoTDeviceAdvisor.Model;
using Amazon.Runtime;

namespace CloudOps.IoTDeviceAdvisor
{
    public class ListTestCasesOperation : Operation
    {
        public override string Name => "ListTestCases";

        public override string Description => "Lists all the test cases in the test suite.";
 
        public override string RequestURI => "/testCases";

        public override string Method => "GET";

        public override string ServiceName => "IoTDeviceAdvisor";

        public override string ServiceID => "IotDeviceAdvisor";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTDeviceAdvisorConfig config = new AmazonIoTDeviceAdvisorConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTDeviceAdvisorClient client = new AmazonIoTDeviceAdvisorClient(creds, config);
            
            ListTestCasesResponse resp = new ListTestCasesResponse();
            do
            {
                try
                {
                    ListTestCasesRequest req = new ListTestCasesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListTestCasesAsync(req);
                    
                    foreach (var obj in resp.Categories)
                    {
                        AddObject(obj);
                    }
                    
                    foreach (var obj in resp.RootGroupConfiguration)
                    {
                        AddObject(obj);
                    }
                    
                    foreach (var obj in resp.GroupConfiguration)
                    {
                        AddObject(obj);
                    }
                    
                }
                catch (System.Exception)
                {
                    CheckError(resp.HttpStatusCode, "200");                
                    throw;
                }

            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}