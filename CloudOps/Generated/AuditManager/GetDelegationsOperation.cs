using Amazon;
using Amazon.AuditManager;
using Amazon.AuditManager.Model;
using Amazon.Runtime;

namespace CloudOps.AuditManager
{
    public class GetDelegationsOperation : Operation
    {
        public override string Name => "GetDelegations";

        public override string Description => " Returns a list of delegations from an audit owner to a delegate. ";
 
        public override string RequestURI => "/delegations";

        public override string Method => "GET";

        public override string ServiceName => "AuditManager";

        public override string ServiceID => "AuditManager";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAuditManagerConfig config = new AmazonAuditManagerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAuditManagerClient client = new AmazonAuditManagerClient(creds, config);
            
            GetDelegationsResponse resp = new GetDelegationsResponse();
            do
            {
                GetDelegationsRequest req = new GetDelegationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.GetDelegationsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Delegations)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}