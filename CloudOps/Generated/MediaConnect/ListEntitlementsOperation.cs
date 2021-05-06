using Amazon;
using Amazon.MediaConnect;
using Amazon.MediaConnect.Model;
using Amazon.Runtime;

namespace CloudOps.MediaConnect
{
    public class ListEntitlementsOperation : Operation
    {
        public override string Name => "ListEntitlements";

        public override string Description => "Displays a list of all entitlements that have been granted to this account. This request returns 20 results per page.";
 
        public override string RequestURI => "/v1/entitlements";

        public override string Method => "GET";

        public override string ServiceName => "MediaConnect";

        public override string ServiceID => "MediaConnect";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaConnectConfig config = new AmazonMediaConnectConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaConnectClient client = new AmazonMediaConnectClient(creds, config);
            
            ListEntitlementsResponse resp = new ListEntitlementsResponse();
            do
            {
                try
                {
                    ListEntitlementsRequest req = new ListEntitlementsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListEntitlementsAsync(req);
                    
                    foreach (var obj in resp.Entitlements)
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