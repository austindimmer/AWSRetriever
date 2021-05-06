using Amazon;
using Amazon.ForecastService;
using Amazon.ForecastService.Model;
using Amazon.Runtime;

namespace CloudOps.ForecastService
{
    public class ListPredictorBacktestExportJobsOperation : Operation
    {
        public override string Name => "ListPredictorBacktestExportJobs";

        public override string Description => "Returns a list of predictor backtest export jobs created using the CreatePredictorBacktestExportJob operation. This operation returns a summary for each backtest export job. You can filter the list using an array of Filter objects. To retrieve the complete set of properties for a particular backtest export job, use the ARN with the DescribePredictorBacktestExportJob operation.";
 
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
            
            ListPredictorBacktestExportJobsResponse resp = new ListPredictorBacktestExportJobsResponse();
            do
            {
                try
                {
                    ListPredictorBacktestExportJobsRequest req = new ListPredictorBacktestExportJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListPredictorBacktestExportJobsAsync(req);
                    
                    foreach (var obj in resp.PredictorBacktestExportJobs)
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