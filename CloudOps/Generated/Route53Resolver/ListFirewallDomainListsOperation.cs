using Amazon;
using Amazon.Route53Resolver;
using Amazon.Route53Resolver.Model;
using Amazon.Runtime;

namespace CloudOps.Route53Resolver
{
    public class ListFirewallDomainListsOperation : Operation
    {
        public override string Name => "ListFirewallDomainLists";

        public override string Description => "Retrieves the firewall domain lists that you have defined. For each firewall domain list, you can retrieve the domains that are defined for a list by calling ListFirewallDomains.  A single call to this list operation might return only a partial list of the domain lists. For information, see MaxResults. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Route53Resolver";

        public override string ServiceID => "Route53Resolver";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRoute53ResolverConfig config = new AmazonRoute53ResolverConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRoute53ResolverClient client = new AmazonRoute53ResolverClient(creds, config);
            
            ListFirewallDomainListsResponse resp = new ListFirewallDomainListsResponse();
            do
            {
                ListFirewallDomainListsRequest req = new ListFirewallDomainListsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListFirewallDomainLists(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.FirewallDomainLists)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}