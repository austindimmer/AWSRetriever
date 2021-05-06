using Amazon;
using Amazon.ApplicationInsights;
using Amazon.ApplicationInsights.Model;
using Amazon.Runtime;

namespace CloudOps.ApplicationInsights
{
    public class ListApplicationsOperation : Operation
    {
        public override string Name => "ListApplications";

        public override string Description => "Lists the IDs of the applications that you are monitoring. ";
 
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
            
            ListApplicationsResponse resp = new ListApplicationsResponse();
            do
            {
                try
                {
                    ListApplicationsRequest req = new ListApplicationsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListApplicationsAsync(req);
                    
                    foreach (var obj in resp.ApplicationInfoList)
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