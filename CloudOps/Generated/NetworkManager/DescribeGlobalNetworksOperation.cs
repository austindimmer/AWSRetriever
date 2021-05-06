using Amazon;
using Amazon.NetworkManager;
using Amazon.NetworkManager.Model;
using Amazon.Runtime;

namespace CloudOps.NetworkManager
{
    public class DescribeGlobalNetworksOperation : Operation
    {
        public override string Name => "DescribeGlobalNetworks";

        public override string Description => "Describes one or more global networks. By default, all global networks are described. To describe the objects in your global network, you must use the appropriate Get* action. For example, to list the transit gateways in your global network, use GetTransitGatewayRegistrations.";
 
        public override string RequestURI => "/global-networks";

        public override string Method => "GET";

        public override string ServiceName => "NetworkManager";

        public override string ServiceID => "NetworkManager";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonNetworkManagerConfig config = new AmazonNetworkManagerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonNetworkManagerClient client = new AmazonNetworkManagerClient(creds, config);
            
            DescribeGlobalNetworksResponse resp = new DescribeGlobalNetworksResponse();
            do
            {
                DescribeGlobalNetworksRequest req = new DescribeGlobalNetworksRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeGlobalNetworksAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.GlobalNetworks)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}