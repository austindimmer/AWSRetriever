using Amazon;
using Amazon.NetworkFirewall;
using Amazon.NetworkFirewall.Model;
using Amazon.Runtime;

namespace CloudOps.NetworkFirewall
{
    public class ListFirewallPoliciesOperation : Operation
    {
        public override string Name => "ListFirewallPolicies";

        public override string Description => "Retrieves the metadata for the firewall policies that you have defined. Depending on your setting for max results and the number of firewall policies, a single call might not return the full list. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "NetworkFirewall";

        public override string ServiceID => "Network Firewall";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonNetworkFirewallConfig config = new AmazonNetworkFirewallConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonNetworkFirewallClient client = new AmazonNetworkFirewallClient(creds, config);
            
            ListFirewallPoliciesResponse resp = new ListFirewallPoliciesResponse();
            do
            {
                ListFirewallPoliciesRequest req = new ListFirewallPoliciesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListFirewallPolicies(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.FirewallPolicies)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}