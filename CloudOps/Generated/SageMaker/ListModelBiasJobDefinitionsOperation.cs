using Amazon;
using Amazon.SageMaker;
using Amazon.SageMaker.Model;
using Amazon.Runtime;

namespace CloudOps.SageMaker
{
    public class ListModelBiasJobDefinitionsOperation : Operation
    {
        public override string Name => "ListModelBiasJobDefinitions";

        public override string Description => "Lists model bias jobs definitions that satisfy various filters.";
 
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
            
            ListModelBiasJobDefinitionsResponse resp = new ListModelBiasJobDefinitionsResponse();
            do
            {
                ListModelBiasJobDefinitionsRequest req = new ListModelBiasJobDefinitionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListModelBiasJobDefinitions(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.JobDefinitionSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}