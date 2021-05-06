using Amazon;
using Amazon.ManagedBlockchain;
using Amazon.ManagedBlockchain.Model;
using Amazon.Runtime;

namespace CloudOps.ManagedBlockchain
{
    public class ListProposalVotesOperation : Operation
    {
        public override string Name => "ListProposalVotes";

        public override string Description => "Returns the list of votes for a specified proposal, including the value of each vote and the unique identifier of the member that cast the vote. Applies only to Hyperledger Fabric.";
 
        public override string RequestURI => "/networks/{networkId}/proposals/{proposalId}/votes";

        public override string Method => "GET";

        public override string ServiceName => "ManagedBlockchain";

        public override string ServiceID => "ManagedBlockchain";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonManagedBlockchainConfig config = new AmazonManagedBlockchainConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonManagedBlockchainClient client = new AmazonManagedBlockchainClient(creds, config);
            
            ListProposalVotesResponse resp = new ListProposalVotesResponse();
            do
            {
                ListProposalVotesRequest req = new ListProposalVotesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListProposalVotesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ProposalVotes)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}