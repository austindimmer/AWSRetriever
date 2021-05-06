using Amazon;
using Amazon.ECRPublic;
using Amazon.ECRPublic.Model;
using Amazon.Runtime;

namespace CloudOps.ECRPublic
{
    public class DescribeRepositoriesOperation : Operation
    {
        public override string Name => "DescribeRepositories";

        public override string Description => "Describes repositories in a public registry.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ECRPublic";

        public override string ServiceID => "ECR PUBLIC";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonECRPublicConfig config = new AmazonECRPublicConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonECRPublicClient client = new AmazonECRPublicClient(creds, config);
            
            DescribeRepositoriesResponse resp = new DescribeRepositoriesResponse();
            do
            {
                try
                {
                    DescribeRepositoriesRequest req = new DescribeRepositoriesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeRepositoriesAsync(req);
                    
                    foreach (var obj in resp.Repositories)
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