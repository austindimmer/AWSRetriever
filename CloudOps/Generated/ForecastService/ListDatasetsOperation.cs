using Amazon;
using Amazon.ForecastService;
using Amazon.ForecastService.Model;
using Amazon.Runtime;

namespace CloudOps.ForecastService
{
    public class ListDatasetsOperation : Operation
    {
        public override string Name => "ListDatasets";

        public override string Description => "Returns a list of datasets created using the CreateDataset operation. For each dataset, a summary of its properties, including its Amazon Resource Name (ARN), is returned. To retrieve the complete set of properties, use the ARN with the DescribeDataset operation.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ForecastService";

        public override string ServiceID => "forecast";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonForecastServiceConfig config = new AmazonForecastServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonForecastServiceClient client = new AmazonForecastServiceClient(creds, config);
            
            ListDatasetsResponse resp = new ListDatasetsResponse();
            do
            {
                try
                {
                    ListDatasetsRequest req = new ListDatasetsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListDatasetsAsync(req);
                    
                    foreach (var obj in resp.Datasets)
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