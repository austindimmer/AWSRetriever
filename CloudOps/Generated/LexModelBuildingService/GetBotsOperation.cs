using Amazon;
using Amazon.LexModelBuildingService;
using Amazon.LexModelBuildingService.Model;
using Amazon.Runtime;

namespace CloudOps.LexModelBuildingService
{
    public class GetBotsOperation : Operation
    {
        public override string Name => "GetBots";

        public override string Description => "Returns bot information as follows:    If you provide the nameContains field, the response includes information for the $LATEST version of all bots whose name contains the specified string.   If you don&#39;t specify the nameContains field, the operation returns information about the $LATEST version of all of your bots.   This operation requires permission for the lex:GetBots action.";
 
        public override string RequestURI => "/bots/";

        public override string Method => "GET";

        public override string ServiceName => "LexModelBuildingService";

        public override string ServiceID => "Lex Model Building Service";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLexModelBuildingServiceConfig config = new AmazonLexModelBuildingServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLexModelBuildingServiceClient client = new AmazonLexModelBuildingServiceClient(creds, config);
            
            GetBotsResponse resp = new GetBotsResponse();
            do
            {
                GetBotsRequest req = new GetBotsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.GetBotsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Bots)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}