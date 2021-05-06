using Amazon;
using Amazon.GameLift;
using Amazon.GameLift.Model;
using Amazon.Runtime;

namespace CloudOps.GameLift
{
    public class ListGameServerGroupsOperation : Operation
    {
        public override string Name => "ListGameServerGroups";

        public override string Description => " This operation is used with the GameLift FleetIQ solution and game server groups.  Retrieves information on all game servers groups that exist in the current AWS account for the selected Region. Use the pagination parameters to retrieve results in a set of sequential segments.   Learn more   GameLift FleetIQ Guide   Related actions   CreateGameServerGroup | ListGameServerGroups | DescribeGameServerGroup | UpdateGameServerGroup | DeleteGameServerGroup | ResumeGameServerGroup | SuspendGameServerGroup | DescribeGameServerInstances | All APIs by task ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "GameLift";

        public override string ServiceID => "GameLift";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGameLiftConfig config = new AmazonGameLiftConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGameLiftClient client = new AmazonGameLiftClient(creds, config);
            
            ListGameServerGroupsResponse resp = new ListGameServerGroupsResponse();
            do
            {
                ListGameServerGroupsRequest req = new ListGameServerGroupsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    Limit = maxItems
                                        
                };

                resp = await client.ListGameServerGroupsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.GameServerGroups)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}