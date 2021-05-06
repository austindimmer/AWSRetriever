using Amazon;
using Amazon.FraudDetector;
using Amazon.FraudDetector.Model;
using Amazon.Runtime;

namespace CloudOps.FraudDetector
{
    public class GetEntityTypesOperation : Operation
    {
        public override string Name => "GetEntityTypes";

        public override string Description => "Gets all entity types or a specific entity type if a name is specified. This is a paginated API. If you provide a null maxResults, this action retrieves a maximum of 10 records per page. If you provide a maxResults, the value must be between 5 and 10. To get the next page results, provide the pagination token from the GetEntityTypesResponse as part of your request. A null pagination token fetches the records from the beginning. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "FraudDetector";

        public override string ServiceID => "FraudDetector";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonFraudDetectorConfig config = new AmazonFraudDetectorConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonFraudDetectorClient client = new AmazonFraudDetectorClient(creds, config);
            
            GetEntityTypesResponse resp = new GetEntityTypesResponse();
            do
            {
                try
                {
                    GetEntityTypesRequest req = new GetEntityTypesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.GetEntityTypesAsync(req);
                    
                    foreach (var obj in resp.EntityTypes)
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