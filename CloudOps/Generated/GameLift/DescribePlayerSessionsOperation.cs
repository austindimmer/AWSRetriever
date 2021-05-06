using Amazon;
using Amazon.GameLift;
using Amazon.GameLift.Model;
using Amazon.Runtime;

namespace CloudOps.GameLift
{
    public class DescribePlayerSessionsOperation : Operation
    {
        public override string Name => "DescribePlayerSessions";

        public override string Description => "Retrieves properties for one or more player sessions.  This action can be used in the following ways:    To retrieve a specific player session, provide the player session ID only.   To retrieve all player sessions in a game session, provide the game session ID only.   To retrieve all player sessions for a specific player, provide a player ID only.   To request player sessions, specify either a player session ID, game session ID, or player ID. You can filter this request by player session status. Use the pagination parameters to retrieve results as a set of sequential pages.  If successful, a PlayerSession object is returned for each session that matches the request.  Available in Amazon GameLift Local.   Related actions   CreatePlayerSession | CreatePlayerSessions | DescribePlayerSessions | StartGameSessionPlacement | DescribeGameSessionPlacement | All APIs by task ";
 
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
            
            DescribePlayerSessionsResponse resp = new DescribePlayerSessionsResponse();
            do
            {
                try
                {
                    DescribePlayerSessionsRequest req = new DescribePlayerSessionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.DescribePlayerSessionsAsync(req);
                    
                    foreach (var obj in resp.PlayerSessions)
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