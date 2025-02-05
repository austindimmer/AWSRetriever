using Amazon;
using Amazon.SageMaker;
using Amazon.SageMaker.Model;
using Amazon.Runtime;

namespace CloudOps.SageMaker
{
    public class ListEdgePackagingJobsOperation : Operation
    {
        public override string Name => "ListEdgePackagingJobs";

        public override string Description => "Returns a list of edge packaging jobs.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SageMaker";

        public override string ServiceID => "SageMaker";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSageMakerConfig config = new AmazonSageMakerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSageMakerClient client = new AmazonSageMakerClient(creds, config);
            
            ListEdgePackagingJobsResponse resp = new ListEdgePackagingJobsResponse();
            do
            {
                ListEdgePackagingJobsRequest req = new ListEdgePackagingJobsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListEdgePackagingJobs(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.EdgePackagingJobSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}