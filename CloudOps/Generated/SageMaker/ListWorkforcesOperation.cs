using Amazon;
using Amazon.SageMaker;
using Amazon.SageMaker.Model;
using Amazon.Runtime;

namespace CloudOps.SageMaker
{
    public class ListWorkforcesOperation : Operation
    {
        public override string Name => "ListWorkforces";

        public override string Description => "Use this operation to list all private and vendor workforces in an AWS Region. Note that you can only have one private workforce per AWS Region.";
 
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
            
            ListWorkforcesResponse resp = new ListWorkforcesResponse();
            do
            {
                ListWorkforcesRequest req = new ListWorkforcesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListWorkforces(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Workforces)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}