using Amazon;
using Amazon.ApplicationInsights;
using Amazon.ApplicationInsights.Model;
using Amazon.Runtime;

namespace CloudOps.ApplicationInsights
{
    public class ListProblemsOperation : Operation
    {
        public override string Name => "ListProblems";

        public override string Description => "Lists the problems with your application.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ApplicationInsights";

        public override string ServiceID => "Application Insights";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonApplicationInsightsConfig config = new AmazonApplicationInsightsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonApplicationInsightsClient client = new AmazonApplicationInsightsClient(creds, config);
            
            ListProblemsResponse resp = new ListProblemsResponse();
            do
            {
                ListProblemsRequest req = new ListProblemsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListProblems(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ProblemList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}