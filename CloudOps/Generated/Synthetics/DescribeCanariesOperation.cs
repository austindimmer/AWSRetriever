using Amazon;
using Amazon.Synthetics;
using Amazon.Synthetics.Model;
using Amazon.Runtime;

namespace CloudOps.Synthetics
{
    public class DescribeCanariesOperation : Operation
    {
        public override string Name => "DescribeCanaries";

        public override string Description => "This operation returns a list of the canaries in your account, along with full details about each canary. This operation does not have resource-level authorization, so if a user is able to use DescribeCanaries, the user can see all of the canaries in the account. A deny policy can only be used to restrict access to all canaries. It cannot be used on specific resources. ";
 
        public override string RequestURI => "/canaries";

        public override string Method => "POST";

        public override string ServiceName => "Synthetics";

        public override string ServiceID => "synthetics";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSyntheticsConfig config = new AmazonSyntheticsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSyntheticsClient client = new AmazonSyntheticsClient(creds, config);
            
            DescribeCanariesResponse resp = new DescribeCanariesResponse();
            do
            {
                try
                {
                    DescribeCanariesRequest req = new DescribeCanariesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeCanariesAsync(req);
                    
                    foreach (var obj in resp.Canaries)
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