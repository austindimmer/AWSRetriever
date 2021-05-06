using Amazon;
using Amazon.GreengrassV2;
using Amazon.GreengrassV2.Model;
using Amazon.Runtime;

namespace CloudOps.GreengrassV2
{
    public class ListDeploymentsOperation : Operation
    {
        public override string Name => "ListDeployments";

        public override string Description => "Retrieves a paginated list of deployments.";
 
        public override string RequestURI => "/greengrass/v2/deployments";

        public override string Method => "GET";

        public override string ServiceName => "GreengrassV2";

        public override string ServiceID => "GreengrassV2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGreengrassV2Config config = new AmazonGreengrassV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGreengrassV2Client client = new AmazonGreengrassV2Client(creds, config);
            
            ListDeploymentsResponse resp = new ListDeploymentsResponse();
            do
            {
                ListDeploymentsRequest req = new ListDeploymentsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListDeploymentsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Deployments)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}