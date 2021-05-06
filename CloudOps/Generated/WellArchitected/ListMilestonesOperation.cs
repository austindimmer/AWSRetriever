using Amazon;
using Amazon.WellArchitected;
using Amazon.WellArchitected.Model;
using Amazon.Runtime;

namespace CloudOps.WellArchitected
{
    public class ListMilestonesOperation : Operation
    {
        public override string Name => "ListMilestones";

        public override string Description => "List all milestones for an existing workload.";
 
        public override string RequestURI => "/workloads/{WorkloadId}/milestonesSummaries";

        public override string Method => "POST";

        public override string ServiceName => "WellArchitected";

        public override string ServiceID => "WellArchitected";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonWellArchitectedConfig config = new AmazonWellArchitectedConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonWellArchitectedClient client = new AmazonWellArchitectedClient(creds, config);
            
            ListMilestonesResponse resp = new ListMilestonesResponse();
            do
            {
                try
                {
                    ListMilestonesRequest req = new ListMilestonesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListMilestonesAsync(req);
                    
                    foreach (var obj in resp.WorkloadId)
                    {
                        AddObject(obj);
                    }
                    
                    foreach (var obj in resp.MilestoneSummaries)
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