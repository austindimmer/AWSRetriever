using Amazon;
using Amazon.SageMaker;
using Amazon.SageMaker.Model;
using Amazon.Runtime;

namespace CloudOps.SageMaker
{
    public class ListPipelineExecutionStepsOperation : Operation
    {
        public override string Name => "ListPipelineExecutionSteps";

        public override string Description => "Gets a list of PipeLineExecutionStep objects.";
 
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
            
            ListPipelineExecutionStepsResponse resp = new ListPipelineExecutionStepsResponse();
            do
            {
                try
                {
                    ListPipelineExecutionStepsRequest req = new ListPipelineExecutionStepsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListPipelineExecutionStepsAsync(req);
                    
                    foreach (var obj in resp.PipelineExecutionSteps)
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