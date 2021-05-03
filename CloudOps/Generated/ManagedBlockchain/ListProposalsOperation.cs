using Amazon;
using Amazon.ManagedBlockchain;
using Amazon.ManagedBlockchain.Model;
using Amazon.Runtime;

namespace CloudOps.ManagedBlockchain
{
    public class ListProposalsOperation : Operation
    {
        public override string Name => "ListProposals";

        public override string Description => "Returns a list of proposals for the network. Applies only to Hyperledger Fabric.";
 
        public override string RequestURI => "/networks/{networkId}/proposals";

        public override string Method => "GET";

        public override string ServiceName => "ManagedBlockchain";

        public override string ServiceID => "ManagedBlockchain";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonManagedBlockchainConfig config = new AmazonManagedBlockchainConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonManagedBlockchainClient client = new AmazonManagedBlockchainClient(creds, config);
            
            ListProposalsResponse resp = new ListProposalsResponse();
            do
            {
                ListProposalsRequest req = new ListProposalsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListProposals(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Proposals)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}