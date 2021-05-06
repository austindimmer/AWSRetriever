using Amazon;
using Amazon.WellArchitected;
using Amazon.WellArchitected.Model;
using Amazon.Runtime;

namespace CloudOps.WellArchitected
{
    public class ListWorkloadSharesOperation : Operation
    {
        public override string Name => "ListWorkloadShares";

        public override string Description => "List the workload shares associated with the workload.";
 
        public override string RequestURI => "/workloads/{WorkloadId}/shares";

        public override string Method => "GET";

        public override string ServiceName => "WellArchitected";

        public override string ServiceID => "WellArchitected";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonWellArchitectedConfig config = new AmazonWellArchitectedConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonWellArchitectedClient client = new AmazonWellArchitectedClient(creds, config);
            
            ListWorkloadSharesResponse resp = new ListWorkloadSharesResponse();
            do
            {
                ListWorkloadSharesRequest req = new ListWorkloadSharesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListWorkloadSharesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.WorkloadId)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.WorkloadShareSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}