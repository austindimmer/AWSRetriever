using Amazon;
using Amazon.IoTAnalytics;
using Amazon.IoTAnalytics.Model;
using Amazon.Runtime;

namespace CloudOps.IoTAnalytics
{
    public class ListDatasetsOperation : Operation
    {
        public override string Name => "ListDatasets";

        public override string Description => "Retrieves information about data sets.";
 
        public override string RequestURI => "/datasets";

        public override string Method => "GET";

        public override string ServiceName => "IoTAnalytics";

        public override string ServiceID => "IoTAnalytics";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTAnalyticsConfig config = new AmazonIoTAnalyticsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTAnalyticsClient client = new AmazonIoTAnalyticsClient(creds, config);
            
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
                    
                    foreach (var obj in resp.DatasetSummaries)
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