using Amazon;
using Amazon.LexModelBuildingService;
using Amazon.LexModelBuildingService.Model;
using Amazon.Runtime;

namespace CloudOps.LexModelBuildingService
{
    public class GetBuiltinSlotTypesOperation : Operation
    {
        public override string Name => "GetBuiltinSlotTypes";

        public override string Description => "Gets a list of built-in slot types that meet the specified criteria. For a list of built-in slot types, see Slot Type Reference in the Alexa Skills Kit. This operation requires permission for the lex:GetBuiltInSlotTypes action.";
 
        public override string RequestURI => "/builtins/slottypes/";

        public override string Method => "GET";

        public override string ServiceName => "LexModelBuildingService";

        public override string ServiceID => "Lex Model Building Service";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLexModelBuildingServiceConfig config = new AmazonLexModelBuildingServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLexModelBuildingServiceClient client = new AmazonLexModelBuildingServiceClient(creds, config);
            
            GetBuiltinSlotTypesResponse resp = new GetBuiltinSlotTypesResponse();
            do
            {
                GetBuiltinSlotTypesRequest req = new GetBuiltinSlotTypesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.GetBuiltinSlotTypesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SlotTypes)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}