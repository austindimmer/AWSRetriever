using Amazon;
using Amazon.CodeStarConnections;
using Amazon.CodeStarConnections.Model;
using Amazon.Runtime;

namespace CloudOps.CodeStarConnections
{
    public class ListConnectionsOperation : Operation
    {
        public override string Name => "ListConnections";

        public override string Description => "Lists the connections associated with your account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodeStarConnections";

        public override string ServiceID => "CodeStar connections";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeStarConnectionsConfig config = new AmazonCodeStarConnectionsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeStarConnectionsClient client = new AmazonCodeStarConnectionsClient(creds, config);
            
            ListConnectionsResponse resp = new ListConnectionsResponse();
            do
            {
                try
                {
                    ListConnectionsRequest req = new ListConnectionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListConnectionsAsync(req);
                    
                    foreach (var obj in resp.Connections)
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