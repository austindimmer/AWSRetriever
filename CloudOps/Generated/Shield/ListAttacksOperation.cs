using Amazon;
using Amazon.Shield;
using Amazon.Shield.Model;
using Amazon.Runtime;

namespace CloudOps.Shield
{
    public class ListAttacksOperation : Operation
    {
        public override string Name => "ListAttacks";

        public override string Description => "Returns all ongoing DDoS attacks or all DDoS attacks during a specified time period.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Shield";

        public override string ServiceID => "Shield";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonShieldConfig config = new AmazonShieldConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonShieldClient client = new AmazonShieldClient(creds, config);
            
            ListAttacksResponse resp = new ListAttacksResponse();
            do
            {
                ListAttacksRequest req = new ListAttacksRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListAttacks(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.AttackSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}