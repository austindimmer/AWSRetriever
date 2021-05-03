using Amazon;
using Amazon.CodeStarConnections;
using Amazon.CodeStarConnections.Model;
using Amazon.Runtime;

namespace CloudOps.CodeStarConnections
{
    public class ListHostsOperation : Operation
    {
        public override string Name => "ListHosts";

        public override string Description => "Lists the hosts associated with your account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodeStarConnections";

        public override string ServiceID => "CodeStar connections";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeStarConnectionsConfig config = new AmazonCodeStarConnectionsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeStarConnectionsClient client = new AmazonCodeStarConnectionsClient(creds, config);
            
            ListHostsResponse resp = new ListHostsResponse();
            do
            {
                ListHostsRequest req = new ListHostsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListHosts(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Hosts)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}