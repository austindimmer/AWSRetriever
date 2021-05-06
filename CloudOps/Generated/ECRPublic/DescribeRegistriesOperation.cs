using Amazon;
using Amazon.ECRPublic;
using Amazon.ECRPublic.Model;
using Amazon.Runtime;

namespace CloudOps.ECRPublic
{
    public class DescribeRegistriesOperation : Operation
    {
        public override string Name => "DescribeRegistries";

        public override string Description => "Returns details for a public registry.";
 
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
            
            DescribeRegistriesResponse resp = new DescribeRegistriesResponse();
            do
            {
                DescribeRegistriesRequest req = new DescribeRegistriesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeRegistriesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Registries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}