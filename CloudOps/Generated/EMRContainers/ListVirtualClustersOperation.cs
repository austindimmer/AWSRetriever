using Amazon;
using Amazon.EMRContainers;
using Amazon.EMRContainers.Model;
using Amazon.Runtime;

namespace CloudOps.EMRContainers
{
    public class ListVirtualClustersOperation : Operation
    {
        public override string Name => "ListVirtualClusters";

        public override string Description => "Lists information about the specified virtual cluster. Virtual cluster is a managed entity on Amazon ElasticMapReduce on EKS. You can create, describe, list and delete virtual clusters. They do not consume any additional resource in your system. A single virtual cluster maps to a single Kubernetes namespace. Given this relationship, you can model virtual clusters the same way you model Kubernetes namespaces to meet your requirements.";
 
        public override string RequestURI => "/virtualclusters";

        public override string Method => "GET";

        public override string ServiceName => "EMRContainers";

        public override string ServiceID => "ElasticMapReduce containers";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEMRContainersConfig config = new AmazonEMRContainersConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEMRContainersClient client = new AmazonEMRContainersClient(creds, config);
            
            ListVirtualClustersResponse resp = new ListVirtualClustersResponse();
            do
            {
                ListVirtualClustersRequest req = new ListVirtualClustersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListVirtualClustersAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.VirtualClusters)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}