using Amazon;
using Amazon.SecurityHub;
using Amazon.SecurityHub.Model;
using Amazon.Runtime;

namespace CloudOps.SecurityHub
{
    public class DescribeProductsOperation : Operation
    {
        public override string Name => "DescribeProducts";

        public override string Description => "Returns information about product integrations in Security Hub. You can optionally provide an integration ARN. If you provide an integration ARN, then the results only include that integration. If you do not provide an integration ARN, then the results include all of the available product integrations. ";
 
        public override string RequestURI => "/products";

        public override string Method => "GET";

        public override string ServiceName => "SecurityHub";

        public override string ServiceID => "SecurityHub";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSecurityHubConfig config = new AmazonSecurityHubConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSecurityHubClient client = new AmazonSecurityHubClient(creds, config);
            
            DescribeProductsResponse resp = new DescribeProductsResponse();
            do
            {
                try
                {
                    DescribeProductsRequest req = new DescribeProductsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeProductsAsync(req);
                    
                    foreach (var obj in resp.Products)
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