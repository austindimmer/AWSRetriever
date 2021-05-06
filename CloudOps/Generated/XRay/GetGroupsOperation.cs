using Amazon;
using Amazon.XRay;
using Amazon.XRay.Model;
using Amazon.Runtime;

namespace CloudOps.XRay
{
    public class GetGroupsOperation : Operation
    {
        public override string Name => "GetGroups";

        public override string Description => "Retrieves all active group details.";
 
        public override string RequestURI => "/Groups";

        public override string Method => "POST";

        public override string ServiceName => "XRay";

        public override string ServiceID => "XRay";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonXRayConfig config = new AmazonXRayConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonXRayClient client = new AmazonXRayClient(creds, config);
            
            GetGroupsResponse resp = new GetGroupsResponse();
            do
            {
                GetGroupsRequest req = new GetGroupsRequest
                {
                    NextToken = resp.NextToken
                                        
                };

                resp = await client.GetGroupsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Groups)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}