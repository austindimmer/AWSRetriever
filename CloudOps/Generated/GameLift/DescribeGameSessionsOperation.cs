using Amazon;
using Amazon.GameLift;
using Amazon.GameLift.Model;
using Amazon.Runtime;

namespace CloudOps.GameLift
{
    public class DescribeGameSessionsOperation : Operation
    {
        public override string Name => "DescribeGameSessions";

        public override string Description => "Retrieves a set of one or more game sessions in a specific fleet location. You can optionally filter the results by current game session status. Alternatively, use SearchGameSessions to request a set of active game sessions that are filtered by certain criteria. To retrieve the protection policy for game sessions, use DescribeGameSessionDetails. This operation can be used in the following ways:    To retrieve all game sessions that are currently running on all locations in a fleet, provide a fleet or alias ID, with an optional status filter. This approach returns all game sessions in the fleet&#39;s home Region and all remote locations.   To retrieve all game sessions that are currently running on a specific fleet location, provide a fleet or alias ID and a location name, with optional status filter. The location can be the fleet&#39;s home Region or any remote location.   To retrieve a specific game session, provide the game session ID. This approach looks for the game session ID in all fleets that reside in the AWS Region defined in the request.   Use the pagination parameters to retrieve results as a set of sequential pages.  If successful, a GameSession object is returned for each game session that matches the request.  Available in GameLift Local.   Learn more   Find a game session   Related actions   CreateGameSession | DescribeGameSessions | DescribeGameSessionDetails | SearchGameSessions | UpdateGameSession | GetGameSessionLogUrl | StartGameSessionPlacement | DescribeGameSessionPlacement | StopGameSessionPlacement | All APIs by task ";
 
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
            
            DescribeGameSessionsResponse resp = new DescribeGameSessionsResponse();
            do
            {
                DescribeGameSessionsRequest req = new DescribeGameSessionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    Limit = maxItems
                                        
                };

                resp = await client.DescribeGameSessionsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.GameSessions)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}