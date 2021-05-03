using Amazon;
using Amazon.Personalize;
using Amazon.Personalize.Model;
using Amazon.Runtime;

namespace CloudOps.Personalize
{
    public class ListBatchInferenceJobsOperation : Operation
    {
        public override string Name => "ListBatchInferenceJobs";

        public override string Description => "Gets a list of the batch inference jobs that have been performed off of a solution version.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Personalize";

        public override string ServiceID => "Personalize";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonPersonalizeConfig config = new AmazonPersonalizeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonPersonalizeClient client = new AmazonPersonalizeClient(creds, config);
            
            ListBatchInferenceJobsResponse resp = new ListBatchInferenceJobsResponse();
            do
            {
                ListBatchInferenceJobsRequest req = new ListBatchInferenceJobsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListBatchInferenceJobs(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.BatchInferenceJobs)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}