using Amazon;
using Amazon.GameLift;
using Amazon.GameLift.Model;
using Amazon.Runtime;

namespace CloudOps.GameLift
{
    public class DescribeGameSessionDetailsOperation : Operation
    {
        public override string Name => "DescribeGameSessionDetails";

        public override string Description => "Retrieves additional game session properties, including the game session protection policy in force, a set of one or more game sessions in a specific fleet location. You can optionally filter the results by current game session status. Alternatively, use SearchGameSessions to request a set of active game sessions that are filtered by certain criteria. To retrieve all game session properties, use DescribeGameSessions.  This operation can be used in the following ways:    To retrieve details for all game sessions that are currently running on all locations in a fleet, provide a fleet or alias ID, with an optional status filter. This approach returns details from the fleet&#39;s home Region and all remote locations.   To retrieve details for all game sessions that are currently running on a specific fleet location, provide a fleet or alias ID and a location name, with optional status filter. The location can be the fleet&#39;s home Region or any remote location.   To retrieve details for a specific game session, provide the game session ID. This approach looks for the game session ID in all fleets that reside in the AWS Region defined in the request.   Use the pagination parameters to retrieve results as a set of sequential pages.  If successful, a GameSessionDetail object is returned for each game session that matches the request.  Learn more   Find a game session   Related actions   CreateGameSession | DescribeGameSessions | DescribeGameSessionDetails | SearchGameSessions | UpdateGameSession | GetGameSessionLogUrl | StartGameSessionPlacement | DescribeGameSessionPlacement | StopGameSessionPlacement | All APIs by task ";
 
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
            
            DescribeGameSessionDetailsResponse resp = new DescribeGameSessionDetailsResponse();
            do
            {
                DescribeGameSessionDetailsRequest req = new DescribeGameSessionDetailsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    Limit = maxItems
                                        
                };

                resp = client.DescribeGameSessionDetails(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.GameSessionDetails)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}