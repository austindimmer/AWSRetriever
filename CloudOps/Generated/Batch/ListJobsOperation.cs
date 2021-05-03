using Amazon;
using Amazon.Batch;
using Amazon.Batch.Model;
using Amazon.Runtime;

namespace CloudOps.Batch
{
    public class ListJobsOperation : Operation
    {
        public override string Name => "ListJobs";

        public override string Description => "Returns a list of AWS Batch jobs. You must specify only one of the following items:   A job queue ID to return a list of jobs in that job queue   A multi-node parallel job ID to return a list of nodes for that job   An array job ID to return a list of the children for that job   You can filter the results by job status with the jobStatus parameter. If you don&#39;t specify a status, only RUNNING jobs are returned.";
 
        public override string RequestURI => "/v1/listjobs";

        public override string Method => "POST";

        public override string ServiceName => "Batch";

        public override string ServiceID => "Batch";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonBatchConfig config = new AmazonBatchConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonBatchClient client = new AmazonBatchClient(creds, config);
            
            ListJobsResponse resp = new ListJobsResponse();
            do
            {
                ListJobsRequest req = new ListJobsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListJobs(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.JobSummaryList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}