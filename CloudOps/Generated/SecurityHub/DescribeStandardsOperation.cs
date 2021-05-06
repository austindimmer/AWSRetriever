using Amazon;
using Amazon.SecurityHub;
using Amazon.SecurityHub.Model;
using Amazon.Runtime;

namespace CloudOps.SecurityHub
{
    public class DescribeStandardsOperation : Operation
    {
        public override string Name => "DescribeStandards";

        public override string Description => "Returns a list of the available standards in Security Hub. For each standard, the results include the standard ARN, the name, and a description. ";
 
        public override string RequestURI => "/standards";

        public override string Method => "GET";

        public override string ServiceName => "SecurityHub";

        public override string ServiceID => "SecurityHub";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSecurityHubConfig config = new AmazonSecurityHubConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSecurityHubClient client = new AmazonSecurityHubClient(creds, config);
            
            DescribeStandardsResponse resp = new DescribeStandardsResponse();
            do
            {
                try
                {
                    DescribeStandardsRequest req = new DescribeStandardsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeStandardsAsync(req);
                    
                    foreach (var obj in resp.Standards)
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