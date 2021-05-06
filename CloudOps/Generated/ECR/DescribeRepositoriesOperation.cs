using Amazon;
using Amazon.ECR;
using Amazon.ECR.Model;
using Amazon.Runtime;

namespace CloudOps.ECR
{
    public class DescribeRepositoriesOperation : Operation
    {
        public override string Name => "DescribeRepositories";

        public override string Description => "Describes image repositories in a registry.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ECR";

        public override string ServiceID => "ECR";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonECRConfig config = new AmazonECRConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonECRClient client = new AmazonECRClient(creds, config);
            
            DescribeRepositoriesResponse resp = new DescribeRepositoriesResponse();
            do
            {
                DescribeRepositoriesRequest req = new DescribeRepositoriesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeRepositoriesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Repositories)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}