using Amazon;
using Amazon.LexModelBuildingService;
using Amazon.LexModelBuildingService.Model;
using Amazon.Runtime;

namespace CloudOps.LexModelBuildingService
{
    public class GetIntentsOperation : Operation
    {
        public override string Name => "GetIntents";

        public override string Description => "Returns intent information as follows:    If you specify the nameContains field, returns the $LATEST version of all intents that contain the specified string.    If you don&#39;t specify the nameContains field, returns information about the $LATEST version of all intents.     The operation requires permission for the lex:GetIntents action. ";
 
        public override string RequestURI => "/intents/";

        public override string Method => "GET";

        public override string ServiceName => "LexModelBuildingService";

        public override string ServiceID => "Lex Model Building Service";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLexModelBuildingServiceConfig config = new AmazonLexModelBuildingServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLexModelBuildingServiceClient client = new AmazonLexModelBuildingServiceClient(creds, config);
            
            GetIntentsResponse resp = new GetIntentsResponse();
            do
            {
                try
                {
                    GetIntentsRequest req = new GetIntentsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.GetIntentsAsync(req);
                    
                    foreach (var obj in resp.Intents)
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