using Amazon;
using Amazon.ForecastService;
using Amazon.ForecastService.Model;
using Amazon.Runtime;

namespace CloudOps.ForecastService
{
    public class ListForecastsOperation : Operation
    {
        public override string Name => "ListForecasts";

        public override string Description => "Returns a list of forecasts created using the CreateForecast operation. For each forecast, this operation returns a summary of its properties, including its Amazon Resource Name (ARN). To retrieve the complete set of properties, specify the ARN with the DescribeForecast operation. You can filter the list using an array of Filter objects.";
 
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
            
            ListForecastsResponse resp = new ListForecastsResponse();
            do
            {
                try
                {
                    ListForecastsRequest req = new ListForecastsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListForecastsAsync(req);
                    
                    foreach (var obj in resp.Forecasts)
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