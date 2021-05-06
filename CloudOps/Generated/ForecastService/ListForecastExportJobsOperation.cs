using Amazon;
using Amazon.ForecastService;
using Amazon.ForecastService.Model;
using Amazon.Runtime;

namespace CloudOps.ForecastService
{
    public class ListForecastExportJobsOperation : Operation
    {
        public override string Name => "ListForecastExportJobs";

        public override string Description => "Returns a list of forecast export jobs created using the CreateForecastExportJob operation. For each forecast export job, this operation returns a summary of its properties, including its Amazon Resource Name (ARN). To retrieve the complete set of properties, use the ARN with the DescribeForecastExportJob operation. You can filter the list using an array of Filter objects.";
 
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
            
            ListForecastExportJobsResponse resp = new ListForecastExportJobsResponse();
            do
            {
                try
                {
                    ListForecastExportJobsRequest req = new ListForecastExportJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListForecastExportJobsAsync(req);
                    
                    foreach (var obj in resp.ForecastExportJobs)
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