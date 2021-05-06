using Amazon;
using Amazon.PinpointEmail;
using Amazon.PinpointEmail.Model;
using Amazon.Runtime;

namespace CloudOps.PinpointEmail
{
    public class ListDeliverabilityTestReportsOperation : Operation
    {
        public override string Name => "ListDeliverabilityTestReports";

        public override string Description => "Show a list of the predictive inbox placement tests that you&#39;ve performed, regardless of their statuses. For predictive inbox placement tests that are complete, you can use the GetDeliverabilityTestReport operation to view the results.";
 
        public override string RequestURI => "/v1/email/deliverability-dashboard/test-reports";

        public override string Method => "GET";

        public override string ServiceName => "PinpointEmail";

        public override string ServiceID => "Pinpoint Email";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonPinpointEmailConfig config = new AmazonPinpointEmailConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonPinpointEmailClient client = new AmazonPinpointEmailClient(creds, config);
            
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