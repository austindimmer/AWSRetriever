using Amazon;
using Amazon.WellArchitected;
using Amazon.WellArchitected.Model;
using Amazon.Runtime;

namespace CloudOps.WellArchitected
{
    public class ListAnswersOperation : Operation
    {
        public override string Name => "ListAnswers";

        public override string Description => "List of answers.";
 
        public override string RequestURI => "/workloads/{WorkloadId}/lensReviews/{LensAlias}/answers";

        public override string Method => "GET";

        public override string ServiceName => "WellArchitected";

        public override string ServiceID => "WellArchitected";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonWellArchitectedConfig config = new AmazonWellArchitectedConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonWellArchitectedClient client = new AmazonWellArchitectedClient(creds, config);
            
            ListAnswersResponse resp = new ListAnswersResponse();
            do
            {
                try
                {
                    ListAnswersRequest req = new ListAnswersRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListAnswersAsync(req);
                    
                    foreach (var obj in resp.LensAlias)
                    {
                        AddObject(obj);
                    }
                    
                    foreach (var obj in resp.AnswerSummaries)
                    {
                        AddObject(obj);
                    }
                    
                    foreach (var obj in resp.WorkloadId)
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