using Amazon;
using Amazon.Route53Resolver;
using Amazon.Route53Resolver.Model;
using Amazon.Runtime;

namespace CloudOps.Route53Resolver
{
    public class ListFirewallRuleGroupAssociationsOperation : Operation
    {
        public override string Name => "ListFirewallRuleGroupAssociations";

        public override string Description => "Retrieves the firewall rule group associations that you have defined. Each association enables DNS filtering for a VPC with one rule group.  A single call might return only a partial list of the associations. For information, see MaxResults. ";
 
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
            
            ListFirewallRuleGroupAssociationsResponse resp = new ListFirewallRuleGroupAssociationsResponse();
            do
            {
                ListFirewallRuleGroupAssociationsRequest req = new ListFirewallRuleGroupAssociationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListFirewallRuleGroupAssociations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.FirewallRuleGroupAssociations)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}