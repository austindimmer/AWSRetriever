using Amazon;
using Amazon.Route53Domains;
using Amazon.Route53Domains.Model;
using Amazon.Runtime;

namespace CloudOps.Route53Domains
{
    public class ListDomainsOperation : Operation
    {
        public override string Name => "ListDomains";

        public override string Description => "This operation returns all the domain names registered with Amazon Route 53 for the current AWS account.";
 
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
            
            ListDomainsResponse resp = new ListDomainsResponse();
            do
            {
                ListDomainsRequest req = new ListDomainsRequest
                {
                    Marker = resp.NextPageMarker
                    ,
                    MaxItems = maxItems
                                        
                };

                resp = await client.ListDomainsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Domains)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextPageMarker));
        }
    }
}