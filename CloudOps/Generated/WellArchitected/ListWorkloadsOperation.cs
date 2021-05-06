using Amazon;
using Amazon.WellArchitected;
using Amazon.WellArchitected.Model;
using Amazon.Runtime;

namespace CloudOps.WellArchitected
{
    public class ListWorkloadsOperation : Operation
    {
        public override string Name => "ListWorkloads";

        public override string Description => "List workloads. Paginated.";
 
        public override string RequestURI => "/workloadsSummaries";

        public override string Method => "POST";

        public override string ServiceName => "WellArchitected";

        public override string ServiceID => "WellArchitected";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonWellArchitectedConfig config = new AmazonWellArchitectedConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonWellArchitectedClient client = new AmazonWellArchitectedClient(creds, config);
            
            ListWorkloadsResponse resp = new ListWorkloadsResponse();
            do
            {
                try
                {
                    ListWorkloadsRequest req = new ListWorkloadsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListWorkloadsAsync(req);
                    
                    foreach (var obj in resp.WorkloadSummaries)
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