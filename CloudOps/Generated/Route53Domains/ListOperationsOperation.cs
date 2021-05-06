using Amazon;
using Amazon.Route53Domains;
using Amazon.Route53Domains.Model;
using Amazon.Runtime;

namespace CloudOps.Route53Domains
{
    public class ListOperationsOperation : Operation
    {
        public override string Name => "ListOperations";

        public override string Description => "Returns information about all of the operations that return an operation ID and that have ever been performed on domains that were registered by the current account. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Route53Domains";

        public override string ServiceID => "Route 53 Domains";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRoute53DomainsConfig config = new AmazonRoute53DomainsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRoute53DomainsClient client = new AmazonRoute53DomainsClient(creds, config);
            
            ListOperationsResponse resp = new ListOperationsResponse();
            do
            {
                ListOperationsRequest req = new ListOperationsRequest
                {
                    Marker = resp.NextPageMarker
                    ,
                    MaxItems = maxItems
                                        
                };

                resp = await client.ListOperationsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Operations)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextPageMarker));
        }
    }
}