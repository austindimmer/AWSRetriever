using Amazon;
using Amazon.Outposts;
using Amazon.Outposts.Model;
using Amazon.Runtime;

namespace CloudOps.Outposts
{
    public class ListSitesOperation : Operation
    {
        public override string Name => "ListSites";

        public override string Description => "Lists the sites for the specified AWS account.";
 
        public override string RequestURI => "/sites";

        public override string Method => "GET";

        public override string ServiceName => "Outposts";

        public override string ServiceID => "Outposts";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonOutpostsConfig config = new AmazonOutpostsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonOutpostsClient client = new AmazonOutpostsClient(creds, config);
            
            ListSitesResponse resp = new ListSitesResponse();
            do
            {
                ListSitesRequest req = new ListSitesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListSitesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Sites)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}