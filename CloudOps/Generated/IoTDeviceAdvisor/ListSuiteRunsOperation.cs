using Amazon;
using Amazon.IoTDeviceAdvisor;
using Amazon.IoTDeviceAdvisor.Model;
using Amazon.Runtime;

namespace CloudOps.IoTDeviceAdvisor
{
    public class ListSuiteRunsOperation : Operation
    {
        public override string Name => "ListSuiteRuns";

        public override string Description => "Lists the runs of the specified Device Advisor test suite. You can list all runs of the test suite, or the runs of a specific version of the test suite.";
 
        public override string RequestURI => "/suiteRuns";

        public override string Method => "GET";

        public override string ServiceName => "IoTDeviceAdvisor";

        public override string ServiceID => "IotDeviceAdvisor";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTDeviceAdvisorConfig config = new AmazonIoTDeviceAdvisorConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTDeviceAdvisorClient client = new AmazonIoTDeviceAdvisorClient(creds, config);
            
            ListSuiteRunsResponse resp = new ListSuiteRunsResponse();
            do
            {
                ListSuiteRunsRequest req = new ListSuiteRunsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListSuiteRuns(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SuiteRunsList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}