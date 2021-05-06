using Amazon;
using Amazon.ManagedBlockchain;
using Amazon.ManagedBlockchain.Model;
using Amazon.Runtime;

namespace CloudOps.ManagedBlockchain
{
    public class ListMembersOperation : Operation
    {
        public override string Name => "ListMembers";

        public override string Description => "Returns a list of the members in a network and properties of their configurations. Applies only to Hyperledger Fabric.";
 
        public override string RequestURI => "/networks/{networkId}/members";

        public override string Method => "GET";

        public override string ServiceName => "ManagedBlockchain";

        public override string ServiceID => "ManagedBlockchain";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonManagedBlockchainConfig config = new AmazonManagedBlockchainConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonManagedBlockchainClient client = new AmazonManagedBlockchainClient(creds, config);
            
            ListMembersResponse resp = new ListMembersResponse();
            do
            {
                ListMembersRequest req = new ListMembersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListMembersAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Members)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}