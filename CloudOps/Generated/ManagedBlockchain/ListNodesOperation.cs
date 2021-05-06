using Amazon;
using Amazon.ManagedBlockchain;
using Amazon.ManagedBlockchain.Model;
using Amazon.Runtime;

namespace CloudOps.ManagedBlockchain
{
    public class ListNodesOperation : Operation
    {
        public override string Name => "ListNodes";

        public override string Description => "Returns information about the nodes within a network. Applies to Hyperledger Fabric and Ethereum.";
 
        public override string RequestURI => "/networks/{networkId}/nodes";

        public override string Method => "GET";

        public override string ServiceName => "ManagedBlockchain";

        public override string ServiceID => "ManagedBlockchain";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonManagedBlockchainConfig config = new AmazonManagedBlockchainConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonManagedBlockchainClient client = new AmazonManagedBlockchainClient(creds, config);
            
            ListNodesResponse resp = new ListNodesResponse();
            do
            {
                ListNodesRequest req = new ListNodesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListNodesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Nodes)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}