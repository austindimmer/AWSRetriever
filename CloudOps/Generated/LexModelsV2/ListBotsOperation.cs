using Amazon;
using Amazon.LexModelsV2;
using Amazon.LexModelsV2.Model;
using Amazon.Runtime;

namespace CloudOps.LexModelsV2
{
    public class ListBotsOperation : Operation
    {
        public override string Name => "ListBots";

        public override string Description => "Gets a list of available bots.";
 
        public override string RequestURI => "/bots/";

        public override string Method => "POST";

        public override string ServiceName => "LexModelsV2";

        public override string ServiceID => "Lex Models V2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLexModelsV2Config config = new AmazonLexModelsV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLexModelsV2Client client = new AmazonLexModelsV2Client(creds, config);
            
            ListBotsResponse resp = new ListBotsResponse();
            do
            {
                ListBotsRequest req = new ListBotsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListBotsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.BotSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}