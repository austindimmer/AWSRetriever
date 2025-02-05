using Amazon;
using Amazon.SageMaker;
using Amazon.SageMaker.Model;
using Amazon.Runtime;

namespace CloudOps.SageMaker
{
    public class ListFeatureGroupsOperation : Operation
    {
        public override string Name => "ListFeatureGroups";

        public override string Description => "List FeatureGroups based on given filter and order.";
 
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
            
            ListFeatureGroupsResponse resp = new ListFeatureGroupsResponse();
            do
            {
                ListFeatureGroupsRequest req = new ListFeatureGroupsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListFeatureGroups(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.FeatureGroupSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}