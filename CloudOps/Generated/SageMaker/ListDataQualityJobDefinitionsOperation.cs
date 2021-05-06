using Amazon;
using Amazon.SageMaker;
using Amazon.SageMaker.Model;
using Amazon.Runtime;

namespace CloudOps.SageMaker
{
    public class ListDataQualityJobDefinitionsOperation : Operation
    {
        public override string Name => "ListDataQualityJobDefinitions";

        public override string Description => "Lists the data quality job definitions in your account.";
 
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
            
            ListDataQualityJobDefinitionsResponse resp = new ListDataQualityJobDefinitionsResponse();
            do
            {
                try
                {
                    ListDataQualityJobDefinitionsRequest req = new ListDataQualityJobDefinitionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListDataQualityJobDefinitionsAsync(req);
                    
                    foreach (var obj in resp.JobDefinitionSummaries)
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