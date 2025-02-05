using Amazon;
using Amazon.SageMaker;
using Amazon.SageMaker.Model;
using Amazon.Runtime;

namespace CloudOps.SageMaker
{
    public class ListHumanTaskUisOperation : Operation
    {
        public override string Name => "ListHumanTaskUis";

        public override string Description => "Returns information about the human task user interfaces in your account.";
 
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
            
            ListHumanTaskUisResponse resp = new ListHumanTaskUisResponse();
            do
            {
                ListHumanTaskUisRequest req = new ListHumanTaskUisRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListHumanTaskUis(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.HumanTaskUiSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}