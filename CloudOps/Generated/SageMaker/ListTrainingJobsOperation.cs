using Amazon;
using Amazon.SageMaker;
using Amazon.SageMaker.Model;
using Amazon.Runtime;

namespace CloudOps.SageMaker
{
    public class ListTrainingJobsOperation : Operation
    {
        public override string Name => "ListTrainingJobs";

        public override string Description => "Lists training jobs.  When StatusEquals and MaxResults are set at the same time, the MaxResults number of training jobs are first retrieved ignoring the StatusEquals parameter and then they are filtered by the StatusEquals parameter, which is returned as a response. For example, if ListTrainingJobs is invoked with the following parameters:  { ... MaxResults: 100, StatusEquals: InProgress ... }  Then, 100 trainings jobs with any status including those other than InProgress are selected first (sorted according the creation time, from the latest to the oldest) and those with status InProgress are returned. You can quickly test the API using the following AWS CLI code.  aws sagemaker list-training-jobs --max-results 100 --status-equals InProgress  ";
 
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
            
            ListTrainingJobsResponse resp = new ListTrainingJobsResponse();
            do
            {
                ListTrainingJobsRequest req = new ListTrainingJobsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListTrainingJobsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.TrainingJobSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}