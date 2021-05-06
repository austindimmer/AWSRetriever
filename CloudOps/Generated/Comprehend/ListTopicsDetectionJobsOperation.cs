using Amazon;
using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using Amazon.Runtime;

namespace CloudOps.Comprehend
{
    public class ListTopicsDetectionJobsOperation : Operation
    {
        public override string Name => "ListTopicsDetectionJobs";

        public override string Description => "Gets a list of the topic detection jobs that you have submitted.";
 
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
            
            ListTopicsDetectionJobsResponse resp = new ListTopicsDetectionJobsResponse();
            do
            {
                try
                {
                    ListTopicsDetectionJobsRequest req = new ListTopicsDetectionJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListTopicsDetectionJobsAsync(req);
                    
                    foreach (var obj in resp.TopicsDetectionJobPropertiesList)
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