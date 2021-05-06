using Amazon;
using Amazon.Glue;
using Amazon.Glue.Model;
using Amazon.Runtime;

namespace CloudOps.Glue
{
    public class GetCrawlerMetricsOperation : Operation
    {
        public override string Name => "GetCrawlerMetrics";

        public override string Description => "Retrieves metrics about specified crawlers.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Glue";

        public override string ServiceID => "Glue";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGlueConfig config = new AmazonGlueConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGlueClient client = new AmazonGlueClient(creds, config);
            
            GetCrawlerMetricsResponse resp = new GetCrawlerMetricsResponse();
            do
            {
                GetCrawlerMetricsRequest req = new GetCrawlerMetricsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.GetCrawlerMetricsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.CrawlerMetricsList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}