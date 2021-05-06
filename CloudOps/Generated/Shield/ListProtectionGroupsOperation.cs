using Amazon;
using Amazon.Shield;
using Amazon.Shield.Model;
using Amazon.Runtime;

namespace CloudOps.Shield
{
    public class ListProtectionGroupsOperation : Operation
    {
        public override string Name => "ListProtectionGroups";

        public override string Description => "Retrieves the ProtectionGroup objects for the account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Shield";

        public override string ServiceID => "Shield";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonShieldConfig config = new AmazonShieldConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonShieldClient client = new AmazonShieldClient(creds, config);
            
            ListProtectionGroupsResponse resp = new ListProtectionGroupsResponse();
            do
            {
                ListProtectionGroupsRequest req = new ListProtectionGroupsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListProtectionGroupsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ProtectionGroups)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}