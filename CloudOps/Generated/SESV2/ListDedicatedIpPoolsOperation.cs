using Amazon;
using Amazon.SESV2;
using Amazon.SESV2.Model;
using Amazon.Runtime;

namespace CloudOps.SESV2
{
    public class ListDedicatedIpPoolsOperation : Operation
    {
        public override string Name => "ListDedicatedIpPools";

        public override string Description => "List all of the dedicated IP pools that exist in your AWS account in the current Region.";
 
        public override string RequestURI => "/v2/email/dedicated-ip-pools";

        public override string Method => "GET";

        public override string ServiceName => "SESV2";

        public override string ServiceID => "SESv2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSESV2Config config = new AmazonSESV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSESV2Client client = new AmazonSESV2Client(creds, config);
            
            ListDedicatedIpPoolsResponse resp = new ListDedicatedIpPoolsResponse();
            do
            {
                try
                {
                    ListDedicatedIpPoolsRequest req = new ListDedicatedIpPoolsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        PageSize = maxItems
                                            
                    };

                    resp = await client.ListDedicatedIpPoolsAsync(req);
                    
                    foreach (var obj in resp.DedicatedIpPools)
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