using Amazon;
using Amazon.ApplicationInsights;
using Amazon.ApplicationInsights.Model;
using Amazon.Runtime;

namespace CloudOps.ApplicationInsights
{
    public class ListConfigurationHistoryOperation : Operation
    {
        public override string Name => "ListConfigurationHistory";

        public override string Description => " Lists the INFO, WARN, and ERROR events for periodic configuration updates performed by Application Insights. Examples of events represented are:    INFO: creating a new alarm or updating an alarm threshold.   WARN: alarm not created due to insufficient data points used to predict thresholds.   ERROR: alarm not created due to permission errors or exceeding quotas.   ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ApplicationInsights";

        public override string ServiceID => "Application Insights";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonApplicationInsightsConfig config = new AmazonApplicationInsightsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonApplicationInsightsClient client = new AmazonApplicationInsightsClient(creds, config);
            
            ListConfigurationHistoryResponse resp = new ListConfigurationHistoryResponse();
            do
            {
                try
                {
                    ListConfigurationHistoryRequest req = new ListConfigurationHistoryRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListConfigurationHistoryAsync(req);
                    
                    foreach (var obj in resp.EventList)
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