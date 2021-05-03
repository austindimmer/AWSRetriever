using Amazon;
using Amazon.Outposts;
using Amazon.Outposts.Model;
using Amazon.Runtime;

namespace CloudOps.Outposts
{
    public class ListOutpostsOperation : Operation
    {
        public override string Name => "ListOutposts";

        public override string Description => "List the Outposts for your AWS account.";
 
        public override string RequestURI => "/outposts";

        public override string Method => "GET";

        public override string ServiceName => "Outposts";

        public override string ServiceID => "Outposts";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonOutpostsConfig config = new AmazonOutpostsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonOutpostsClient client = new AmazonOutpostsClient(creds, config);
            
            ListOutpostsResponse resp = new ListOutpostsResponse();
            do
            {
                ListOutpostsRequest req = new ListOutpostsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListOutposts(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Outposts)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}