using Amazon;
using Amazon.SecurityHub;
using Amazon.SecurityHub.Model;
using Amazon.Runtime;

namespace CloudOps.SecurityHub
{
    public class DescribeActionTargetsOperation : Operation
    {
        public override string Name => "DescribeActionTargets";

        public override string Description => "Returns a list of the custom action targets in Security Hub in your account.";
 
        public override string RequestURI => "/actionTargets/get";

        public override string Method => "POST";

        public override string ServiceName => "SecurityHub";

        public override string ServiceID => "SecurityHub";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSecurityHubConfig config = new AmazonSecurityHubConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSecurityHubClient client = new AmazonSecurityHubClient(creds, config);
            
            DescribeActionTargetsResponse resp = new DescribeActionTargetsResponse();
            do
            {
                try
                {
                    DescribeActionTargetsRequest req = new DescribeActionTargetsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeActionTargetsAsync(req);
                    
                    foreach (var obj in resp.ActionTargets)
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