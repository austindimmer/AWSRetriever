using Amazon;
using Amazon.GameLift;
using Amazon.GameLift.Model;
using Amazon.Runtime;

namespace CloudOps.GameLift
{
    public class DescribeGameSessionQueuesOperation : Operation
    {
        public override string Name => "DescribeGameSessionQueues";

        public override string Description => "Retrieves the properties for one or more game session queues. When requesting multiple queues, use the pagination parameters to retrieve results as a set of sequential pages. If successful, a GameSessionQueue object is returned for each requested queue. When specifying a list of queues, objects are returned only for queues that currently exist in the Region.  Learn more    View Your Queues   Related actions   CreateGameSessionQueue | DescribeGameSessionQueues | UpdateGameSessionQueue | DeleteGameSessionQueue | All APIs by task ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "GameLift";

        public override string ServiceID => "GameLift";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGameLiftConfig config = new AmazonGameLiftConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGameLiftClient client = new AmazonGameLiftClient(creds, config);
            
            DescribeGameSessionQueuesResponse resp = new DescribeGameSessionQueuesResponse();
            do
            {
                DescribeGameSessionQueuesRequest req = new DescribeGameSessionQueuesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    Limit = maxItems
                                        
                };

                resp = client.DescribeGameSessionQueues(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.GameSessionQueues)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}