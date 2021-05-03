using Amazon;
using Amazon.SageMaker;
using Amazon.SageMaker.Model;
using Amazon.Runtime;

namespace CloudOps.SageMaker
{
    public class ListCodeRepositoriesOperation : Operation
    {
        public override string Name => "ListCodeRepositories";

        public override string Description => "Gets a list of the Git repositories in your account.";
 
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
            
            ListCodeRepositoriesResponse resp = new ListCodeRepositoriesResponse();
            do
            {
                ListCodeRepositoriesRequest req = new ListCodeRepositoriesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListCodeRepositories(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.CodeRepositorySummaryList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}