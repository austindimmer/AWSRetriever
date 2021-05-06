using Amazon;
using Amazon.CostAndUsageReportService;
using Amazon.CostAndUsageReportService.Model;
using Amazon.Runtime;

namespace CloudOps.CostAndUsageReportService
{
    public class DescribeReportDefinitionsOperation : Operation
    {
        public override string Name => "DescribeReportDefinitions";

        public override string Description => "Lists the AWS Cost and Usage reports available to this account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CostAndUsageReportService";

        public override string ServiceID => "Cost and Usage Report Service";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCostAndUsageReportServiceConfig config = new AmazonCostAndUsageReportServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCostAndUsageReportServiceClient client = new AmazonCostAndUsageReportServiceClient(creds, config);
            
            DescribeReportDefinitionsResponse resp = new DescribeReportDefinitionsResponse();
            do
            {
                try
                {
                    DescribeReportDefinitionsRequest req = new DescribeReportDefinitionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeReportDefinitionsAsync(req);
                    
                    foreach (var obj in resp.ReportDefinitions)
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