using Amazon;
using Amazon.Route53Resolver;
using Amazon.Route53Resolver.Model;
using Amazon.Runtime;

namespace CloudOps.Route53Resolver
{
    public class ListFirewallRuleGroupsOperation : Operation
    {
        public override string Name => "ListFirewallRuleGroups";

        public override string Description => "Retrieves the minimal high-level information for the rule groups that you have defined.  A single call might return only a partial list of the rule groups. For information, see MaxResults. ";
 
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
            
            ListFirewallRuleGroupsResponse resp = new ListFirewallRuleGroupsResponse();
            do
            {
                ListFirewallRuleGroupsRequest req = new ListFirewallRuleGroupsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListFirewallRuleGroups(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.FirewallRuleGroups)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}