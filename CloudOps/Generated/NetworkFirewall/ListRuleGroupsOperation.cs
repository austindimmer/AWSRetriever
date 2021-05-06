using Amazon;
using Amazon.NetworkFirewall;
using Amazon.NetworkFirewall.Model;
using Amazon.Runtime;

namespace CloudOps.NetworkFirewall
{
    public class ListRuleGroupsOperation : Operation
    {
        public override string Name => "ListRuleGroups";

        public override string Description => "Retrieves the metadata for the rule groups that you have defined. Depending on your setting for max results and the number of rule groups, a single call might not return the full list. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "NetworkFirewall";

        public override string ServiceID => "Network Firewall";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonNetworkFirewallConfig config = new AmazonNetworkFirewallConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonNetworkFirewallClient client = new AmazonNetworkFirewallClient(creds, config);
            
            ListRuleGroupsResponse resp = new ListRuleGroupsResponse();
            do
            {
                try
                {
                    ListRuleGroupsRequest req = new ListRuleGroupsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListRuleGroupsAsync(req);
                    
                    foreach (var obj in resp.RuleGroups)
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