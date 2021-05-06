using Amazon;
using Amazon.Shield;
using Amazon.Shield.Model;
using Amazon.Runtime;

namespace CloudOps.Shield
{
    public class ListProtectionsOperation : Operation
    {
        public override string Name => "ListProtections";

        public override string Description => "Lists all Protection objects for the account.";
 
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
            
            ListProtectionsResponse resp = new ListProtectionsResponse();
            do
            {
                ListProtectionsRequest req = new ListProtectionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListProtectionsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Protections)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}