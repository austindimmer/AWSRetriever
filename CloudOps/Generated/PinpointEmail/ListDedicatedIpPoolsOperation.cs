using Amazon;
using Amazon.PinpointEmail;
using Amazon.PinpointEmail.Model;
using Amazon.Runtime;

namespace CloudOps.PinpointEmail
{
    public class ListDedicatedIpPoolsOperation : Operation
    {
        public override string Name => "ListDedicatedIpPools";

        public override string Description => "List all of the dedicated IP pools that exist in your Amazon Pinpoint account in the current AWS Region.";
 
        public override string RequestURI => "/v1/email/dedicated-ip-pools";

        public override string Method => "GET";

        public override string ServiceName => "PinpointEmail";

        public override string ServiceID => "Pinpoint Email";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonPinpointEmailConfig config = new AmazonPinpointEmailConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonPinpointEmailClient client = new AmazonPinpointEmailClient(creds, config);
            
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