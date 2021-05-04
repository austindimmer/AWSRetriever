using Amazon;
using Amazon.CodeStarconnections;
using Amazon.CodeStarconnections.Model;
using Amazon.Runtime;

namespace CloudOps.CodeStarconnections
{
    public class ListHostsOperation : Operation
    {
        public override string Name => "ListHosts";

        public override string Description => "Lists the hosts associated with your account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodeStarconnections";

        public override string ServiceID => "CodeStar connections";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeStarconnectionsConfig config = new AmazonCodeStarconnectionsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeStarconnectionsClient client = new AmazonCodeStarconnectionsClient(creds, config);
            
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