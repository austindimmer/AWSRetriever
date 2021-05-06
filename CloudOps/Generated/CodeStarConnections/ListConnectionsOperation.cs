using Amazon;
using Amazon.CodeStarconnections;
using Amazon.CodeStarconnections.Model;
using Amazon.Runtime;

namespace CloudOps.CodeStarconnections
{
    public class ListConnectionsOperation : Operation
    {
        public override string Name => "ListConnections";

        public override string Description => "Lists the connections associated with your account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodeStarconnections";

        public override string ServiceID => "CodeStar connections";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeStarconnectionsConfig config = new AmazonCodeStarconnectionsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeStarconnectionsClient client = new AmazonCodeStarconnectionsClient(creds, config);
            
            ListConnectionsResponse resp = new ListConnectionsResponse();
            do
            {
                ListConnectionsRequest req = new ListConnectionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListConnectionsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Connections)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}