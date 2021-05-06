using Amazon;
using Amazon.EKS;
using Amazon.EKS.Model;
using Amazon.Runtime;

namespace CloudOps.EKS
{
    public class DescribeAddonVersionsOperation : Operation
    {
        public override string Name => "DescribeAddonVersions";

        public override string Description => "Describes the Kubernetes versions that the add-on can be used with.";
 
        public override string RequestURI => "/addons/supported-versions";

        public override string Method => "GET";

        public override string ServiceName => "EKS";

        public override string ServiceID => "EKS";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEKSConfig config = new AmazonEKSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEKSClient client = new AmazonEKSClient(creds, config);
            
            DescribeAddonVersionsResponse resp = new DescribeAddonVersionsResponse();
            do
            {
                DescribeAddonVersionsRequest req = new DescribeAddonVersionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeAddonVersionsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Addons)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}