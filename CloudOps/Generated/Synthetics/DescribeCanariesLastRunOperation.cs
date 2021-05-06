using Amazon;
using Amazon.Synthetics;
using Amazon.Synthetics.Model;
using Amazon.Runtime;

namespace CloudOps.Synthetics
{
    public class DescribeCanariesLastRunOperation : Operation
    {
        public override string Name => "DescribeCanariesLastRun";

        public override string Description => "Use this operation to see information from the most recent run of each canary that you have created.";
 
        public override string RequestURI => "/canaries/last-run";

        public override string Method => "POST";

        public override string ServiceName => "Synthetics";

        public override string ServiceID => "synthetics";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSyntheticsConfig config = new AmazonSyntheticsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSyntheticsClient client = new AmazonSyntheticsClient(creds, config);
            
            DescribeCanariesLastRunResponse resp = new DescribeCanariesLastRunResponse();
            do
            {
                try
                {
                    DescribeCanariesLastRunRequest req = new DescribeCanariesLastRunRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeCanariesLastRunAsync(req);
                    
                    foreach (var obj in resp.CanariesLastRun)
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