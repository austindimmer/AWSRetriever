using Amazon;
using Amazon.SESV2;
using Amazon.SESV2.Model;
using Amazon.Runtime;

namespace CloudOps.SESV2
{
    public class ListDeliverabilityTestReportsOperation : Operation
    {
        public override string Name => "ListDeliverabilityTestReports";

        public override string Description => "Show a list of the predictive inbox placement tests that you&#39;ve performed, regardless of their statuses. For predictive inbox placement tests that are complete, you can use the GetDeliverabilityTestReport operation to view the results.";
 
        public override string RequestURI => "/v2/email/deliverability-dashboard/test-reports";

        public override string Method => "GET";

        public override string ServiceName => "SESV2";

        public override string ServiceID => "SESv2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSESV2Config config = new AmazonSESV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSESV2Client client = new AmazonSESV2Client(creds, config);
            
            ListDeliverabilityTestReportsResponse resp = new ListDeliverabilityTestReportsResponse();
            do
            {
                try
                {
                    ListDeliverabilityTestReportsRequest req = new ListDeliverabilityTestReportsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        PageSize = maxItems
                                            
                    };

                    resp = await client.ListDeliverabilityTestReportsAsync(req);
                    
                    foreach (var obj in resp.DeliverabilityTestReports)
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