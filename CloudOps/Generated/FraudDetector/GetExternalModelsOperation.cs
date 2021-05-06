using Amazon;
using Amazon.FraudDetector;
using Amazon.FraudDetector.Model;
using Amazon.Runtime;

namespace CloudOps.FraudDetector
{
    public class GetExternalModelsOperation : Operation
    {
        public override string Name => "GetExternalModels";

        public override string Description => "Gets the details for one or more Amazon SageMaker models that have been imported into the service. This is a paginated API. If you provide a null maxResults, this actions retrieves a maximum of 10 records per page. If you provide a maxResults, the value must be between 5 and 10. To get the next page results, provide the pagination token from the GetExternalModelsResult as part of your request. A null pagination token fetches the records from the beginning. ";
 
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
            
            GetExternalModelsResponse resp = new GetExternalModelsResponse();
            do
            {
                try
                {
                    GetExternalModelsRequest req = new GetExternalModelsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.GetExternalModelsAsync(req);
                    
                    foreach (var obj in resp.ExternalModels)
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