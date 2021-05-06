using Amazon;
using Amazon.SecurityHub;
using Amazon.SecurityHub.Model;
using Amazon.Runtime;

namespace CloudOps.SecurityHub
{
    public class GetInsightsOperation : Operation
    {
        public override string Name => "GetInsights";

        public override string Description => "Lists and describes insights for the specified insight ARNs.";
 
        public override string RequestURI => "/insights/get";

        public override string Method => "POST";

        public override string ServiceName => "SecurityHub";

        public override string ServiceID => "SecurityHub";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSecurityHubConfig config = new AmazonSecurityHubConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSecurityHubClient client = new AmazonSecurityHubClient(creds, config);
            
            GetInsightsResponse resp = new GetInsightsResponse();
            do
            {
                try
                {
                    GetInsightsRequest req = new GetInsightsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.GetInsightsAsync(req);
                    
                    foreach (var obj in resp.Insights)
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