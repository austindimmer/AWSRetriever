using Amazon;
using Amazon.SimpleEmailV2;
using Amazon.SimpleEmailV2.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleEmailV2
{
    public class ListDedicatedIpPoolsOperation : Operation
    {
        public override string Name => "ListDedicatedIpPools";

        public override string Description => "List all of the dedicated IP pools that exist in your AWS account in the current Region.";
 
        public override string RequestURI => "/v2/email/dedicated-ip-pools";

        public override string Method => "GET";

        public override string ServiceName => "SimpleEmailV2";

        public override string ServiceID => "SimpleEmailV2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSimpleEmailServiceV2Config config = new AmazonSimpleEmailServiceV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSimpleEmailServiceV2Client client = new AmazonSimpleEmailServiceV2Client(creds, config);
            
            ListDedicatedIpPoolsResponse resp = new ListDedicatedIpPoolsResponse();
            do
            {
                ListDedicatedIpPoolsRequest req = new ListDedicatedIpPoolsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    PageSize = maxItems
                                        
                };

                resp = await client.ListDedicatedIpPoolsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.DedicatedIpPools)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}