using Amazon;
using Amazon.Route53Resolver;
using Amazon.Route53Resolver.Model;
using Amazon.Runtime;

namespace CloudOps.Route53Resolver
{
    public class ListFirewallConfigsOperation : Operation
    {
        public override string Name => "ListFirewallConfigs";

        public override string Description => "Retrieves the firewall configurations that you have defined. DNS Firewall uses the configurations to manage firewall behavior for your VPCs.  A single call might return only a partial list of the configurations. For information, see MaxResults. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Route53Resolver";

        public override string ServiceID => "Route53Resolver";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRoute53ResolverConfig config = new AmazonRoute53ResolverConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRoute53ResolverClient client = new AmazonRoute53ResolverClient(creds, config);
            
            ListFirewallConfigsResponse resp = new ListFirewallConfigsResponse();
            do
            {
                try
                {
                    ListFirewallConfigsRequest req = new ListFirewallConfigsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListFirewallConfigsAsync(req);
                    
                    foreach (var obj in resp.FirewallConfigs)
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