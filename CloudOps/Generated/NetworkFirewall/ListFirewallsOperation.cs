using Amazon;
using Amazon.NetworkFirewall;
using Amazon.NetworkFirewall.Model;
using Amazon.Runtime;

namespace CloudOps.NetworkFirewall
{
    public class ListFirewallsOperation : Operation
    {
        public override string Name => "ListFirewalls";

        public override string Description => "Retrieves the metadata for the firewalls that you have defined. If you provide VPC identifiers in your request, this returns only the firewalls for those VPCs. Depending on your setting for max results and the number of firewalls, a single call might not return the full list. ";
 
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
            
            ListFirewallsResponse resp = new ListFirewallsResponse();
            do
            {
                ListFirewallsRequest req = new ListFirewallsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListFirewalls(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Firewalls)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}