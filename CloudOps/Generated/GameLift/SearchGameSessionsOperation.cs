using Amazon;
using Amazon.GameLift;
using Amazon.GameLift.Model;
using Amazon.Runtime;

namespace CloudOps.GameLift
{
    public class SearchGameSessionsOperation : Operation
    {
        public override string Name => "SearchGameSessions";

        public override string Description => "Retrieves all active game sessions that match a set of search criteria and sorts them into a specified order.  When searching for game sessions, you specify exactly where you want to search and provide a search filter expression, a sort expression, or both. A search request can search only one fleet, but it can search all of a fleet&#39;s locations.  This operation can be used in the following ways:    To search all game sessions that are currently running on all locations in a fleet, provide a fleet or alias ID. This approach returns game sessions in the fleet&#39;s home Region and all remote locations that fit the search criteria.   To search all game sessions that are currently running on a specific fleet location, provide a fleet or alias ID and a location name. For location, you can specify a fleet&#39;s home Region or any remote location.   Use the pagination parameters to retrieve results as a set of sequential pages.  If successful, a GameSession object is returned for each game session that matches the request. Search finds game sessions that are in ACTIVE status only. To retrieve information on game sessions in other statuses, use DescribeGameSessions. You can search or sort by the following game session attributes:    gameSessionId -- A unique identifier for the game session. You can use either a GameSessionId or GameSessionArn value.     gameSessionName -- Name assigned to a game session. This value is set when requesting a new game session with CreateGameSession or updating with UpdateGameSession. Game session names do not need to be unique to a game session.    gameSessionProperties -- Custom data defined in a game session&#39;s GameProperty parameter. GameProperty values are stored as key:value pairs; the filter expression must indicate the key and a string to search the data values for. For example, to search for game sessions with custom data containing the key:value pair &#34;gameMode:brawl&#34;, specify the following: gameSessionProperties.gameMode = &#34;brawl&#34;. All custom data values are searched as strings.    maximumSessions -- Maximum number of player sessions allowed for a game session. This value is set when requesting a new game session with CreateGameSession or updating with UpdateGameSession.    creationTimeMillis -- Value indicating when a game session was created. It is expressed in Unix time as milliseconds.    playerSessionCount -- Number of players currently connected to a game session. This value changes rapidly as players join the session or drop out.    hasAvailablePlayerSessions -- Boolean value indicating whether a game session has reached its maximum number of players. It is highly recommended that all search requests include this filter attribute to optimize search performance and return only sessions that players can join.     Returned values for playerSessionCount and hasAvailablePlayerSessions change quickly as players join sessions and others drop out. Results should be considered a snapshot in time. Be sure to refresh search results often, and handle sessions that fill up before a player can join.    Related actions   CreateGameSession | DescribeGameSessions | DescribeGameSessionDetails | SearchGameSessions | UpdateGameSession | GetGameSessionLogUrl | StartGameSessionPlacement | DescribeGameSessionPlacement | StopGameSessionPlacement | All APIs by task ";
 
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
            
            SearchGameSessionsResponse resp = new SearchGameSessionsResponse();
            do
            {
                try
                {
                    SearchGameSessionsRequest req = new SearchGameSessionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.SearchGameSessionsAsync(req);
                    
                    foreach (var obj in resp.GameSessions)
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