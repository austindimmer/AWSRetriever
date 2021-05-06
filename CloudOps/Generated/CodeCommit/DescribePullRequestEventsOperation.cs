using Amazon;
using Amazon.CodeCommit;
using Amazon.CodeCommit.Model;
using Amazon.Runtime;

namespace CloudOps.CodeCommit
{
    public class DescribePullRequestEventsOperation : Operation
    {
        public override string Name => "DescribePullRequestEvents";

        public override string Description => "Returns information about one or more pull request events.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodeCommit";

        public override string ServiceID => "CodeCommit";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeCommitConfig config = new AmazonCodeCommitConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeCommitClient client = new AmazonCodeCommitClient(creds, config);
            
            DescribePullRequestEventsResponse resp = new DescribePullRequestEventsResponse();
            do
            {
                DescribePullRequestEventsRequest req = new DescribePullRequestEventsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribePullRequestEvents(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.PullRequestEvents)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}