using Amazon;
using Amazon.SageMaker;
using Amazon.SageMaker.Model;
using Amazon.Runtime;

namespace CloudOps.SageMaker
{
    public class ListModelPackageGroupsOperation : Operation
    {
        public override string Name => "ListModelPackageGroups";

        public override string Description => "Gets a list of the model groups in your AWS account.";
 
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
            
            ListModelPackageGroupsResponse resp = new ListModelPackageGroupsResponse();
            do
            {
                ListModelPackageGroupsRequest req = new ListModelPackageGroupsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListModelPackageGroups(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ModelPackageGroupSummaryList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}