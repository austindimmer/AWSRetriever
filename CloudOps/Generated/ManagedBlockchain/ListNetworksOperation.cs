using Amazon;
using Amazon.ManagedBlockchain;
using Amazon.ManagedBlockchain.Model;
using Amazon.Runtime;

namespace CloudOps.ManagedBlockchain
{
    public class ListNetworksOperation : Operation
    {
        public override string Name => "ListNetworks";

        public override string Description => "Returns information about the networks in which the current AWS account participates. Applies to Hyperledger Fabric and Ethereum.";
 
        public override string RequestURI => "/networks";

        public override string Method => "GET";

        public override string ServiceName => "ManagedBlockchain";

        public override string ServiceID => "ManagedBlockchain";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonManagedBlockchainConfig config = new AmazonManagedBlockchainConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonManagedBlockchainClient client = new AmazonManagedBlockchainClient(creds, config);
            
            ListNetworksResponse resp = new ListNetworksResponse();
            do
            {
                try
                {
                    ListNetworksRequest req = new ListNetworksRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListNetworksAsync(req);
                    
                    foreach (var obj in resp.Networks)
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