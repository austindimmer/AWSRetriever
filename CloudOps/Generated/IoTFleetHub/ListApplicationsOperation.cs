using Amazon;
using Amazon.IoTFleetHub;
using Amazon.IoTFleetHub.Model;
using Amazon.Runtime;

namespace CloudOps.IoTFleetHub
{
    public class ListApplicationsOperation : Operation
    {
        public override string Name => "ListApplications";

        public override string Description => "Gets a list of Fleet Hub for AWS IoT Device Management web applications for the current account.  Fleet Hub for AWS IoT Device Management is in public preview and is subject to change. ";
 
        public override string RequestURI => "/applications";

        public override string Method => "GET";

        public override string ServiceName => "IoTFleetHub";

        public override string ServiceID => "IoTFleetHub";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTFleetHubConfig config = new AmazonIoTFleetHubConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTFleetHubClient client = new AmazonIoTFleetHubClient(creds, config);
            
            ListApplicationsResponse resp = new ListApplicationsResponse();
            do
            {
                ListApplicationsRequest req = new ListApplicationsRequest
                {
                    NextToken = resp.NextToken
                                        
                };

                resp = client.ListApplications(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ApplicationSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}