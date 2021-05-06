using Amazon;
using Amazon.GreengrassV2;
using Amazon.GreengrassV2.Model;
using Amazon.Runtime;

namespace CloudOps.GreengrassV2
{
    public class ListComponentsOperation : Operation
    {
        public override string Name => "ListComponents";

        public override string Description => "Retrieves a paginated list of component summaries. This list includes components that you have permission to view.";
 
        public override string RequestURI => "/greengrass/v2/components";

        public override string Method => "GET";

        public override string ServiceName => "GreengrassV2";

        public override string ServiceID => "GreengrassV2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGreengrassV2Config config = new AmazonGreengrassV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGreengrassV2Client client = new AmazonGreengrassV2Client(creds, config);
            
            ListComponentsResponse resp = new ListComponentsResponse();
            do
            {
                try
                {
                    ListComponentsRequest req = new ListComponentsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListComponentsAsync(req);
                    
                    foreach (var obj in resp.Components)
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