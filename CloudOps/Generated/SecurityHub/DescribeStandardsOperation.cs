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

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSecurityHubConfig config = new AmazonSecurityHubConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSecurityHubClient client = new AmazonSecurityHubClient(creds, config);
            
            DescribeStandardsResponse resp = new DescribeStandardsResponse();
            do
            {
                DescribeStandardsRequest req = new DescribeStandardsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeStandards(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Standards)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}