using Amazon;
using Amazon.EKS;
using Amazon.EKS.Model;
using Amazon.Runtime;

namespace CloudOps.EKS
{
    public class ListClustersOperation : Operation
    {
        public override string Name => "ListClusters";

        public override string Description => "Lists the Amazon EKS clusters in your AWS account in the specified Region.";
 
        public override string RequestURI => "/clusters";

        public override string Method => "GET";

        public override string ServiceName => "EKS";

        public override string ServiceID => "EKS";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEKSConfig config = new AmazonEKSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEKSClient client = new AmazonEKSClient(creds, config);
            
            ListClustersResponse resp = new ListClustersResponse();
            do
            {
                ListClustersRequest req = new ListClustersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListClusters(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Clusters)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}