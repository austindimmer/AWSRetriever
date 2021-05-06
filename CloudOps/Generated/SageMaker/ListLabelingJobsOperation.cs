using Amazon;
using Amazon.SageMaker;
using Amazon.SageMaker.Model;
using Amazon.Runtime;

namespace CloudOps.SageMaker
{
    public class ListLabelingJobsOperation : Operation
    {
        public override string Name => "ListLabelingJobs";

        public override string Description => "Gets a list of labeling jobs.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SageMaker";

        public override string ServiceID => "SageMaker";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSageMakerConfig config = new AmazonSageMakerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSageMakerClient client = new AmazonSageMakerClient(creds, config);
            
            ListLabelingJobsResponse resp = new ListLabelingJobsResponse();
            do
            {
                ListLabelingJobsRequest req = new ListLabelingJobsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListLabelingJobsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.LabelingJobSummaryList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}