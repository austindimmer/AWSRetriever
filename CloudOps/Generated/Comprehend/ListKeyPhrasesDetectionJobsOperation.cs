using Amazon;
using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using Amazon.Runtime;

namespace CloudOps.Comprehend
{
    public class ListKeyPhrasesDetectionJobsOperation : Operation
    {
        public override string Name => "ListKeyPhrasesDetectionJobs";

        public override string Description => "Get a list of key phrase detection jobs that you have submitted.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Comprehend";

        public override string ServiceID => "Comprehend";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonComprehendConfig config = new AmazonComprehendConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonComprehendClient client = new AmazonComprehendClient(creds, config);
            
            ListKeyPhrasesDetectionJobsResponse resp = new ListKeyPhrasesDetectionJobsResponse();
            do
            {
                try
                {
                    ListKeyPhrasesDetectionJobsRequest req = new ListKeyPhrasesDetectionJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListKeyPhrasesDetectionJobsAsync(req);
                    
                    foreach (var obj in resp.KeyPhrasesDetectionJobPropertiesList)
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