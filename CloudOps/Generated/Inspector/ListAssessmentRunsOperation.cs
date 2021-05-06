using Amazon;
using Amazon.Inspector;
using Amazon.Inspector.Model;
using Amazon.Runtime;

namespace CloudOps.Inspector
{
    public class ListAssessmentRunsOperation : Operation
    {
        public override string Name => "ListAssessmentRuns";

        public override string Description => "Lists the assessment runs that correspond to the assessment templates that are specified by the ARNs of the assessment templates.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Inspector";

        public override string ServiceID => "Inspector";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonInspectorConfig config = new AmazonInspectorConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonInspectorClient client = new AmazonInspectorClient(creds, config);
            
            ListAssessmentRunsResponse resp = new ListAssessmentRunsResponse();
            do
            {
                try
                {
                    ListAssessmentRunsRequest req = new ListAssessmentRunsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListAssessmentRunsAsync(req);
                    
                    foreach (var obj in resp.AssessmentRunArns)
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