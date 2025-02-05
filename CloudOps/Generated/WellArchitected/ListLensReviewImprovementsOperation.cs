using Amazon;
using Amazon.WellArchitected;
using Amazon.WellArchitected.Model;
using Amazon.Runtime;

namespace CloudOps.WellArchitected
{
    public class ListLensReviewImprovementsOperation : Operation
    {
        public override string Name => "ListLensReviewImprovements";

        public override string Description => "List lens review improvements.";
 
        public override string RequestURI => "/workloads/{WorkloadId}/lensReviews/{LensAlias}/improvements";

        public override string Method => "GET";

        public override string ServiceName => "WellArchitected";

        public override string ServiceID => "WellArchitected";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonWellArchitectedConfig config = new AmazonWellArchitectedConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonWellArchitectedClient client = new AmazonWellArchitectedClient(creds, config);
            
            ListLensReviewImprovementsResponse resp = new ListLensReviewImprovementsResponse();
            do
            {
                ListLensReviewImprovementsRequest req = new ListLensReviewImprovementsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListLensReviewImprovements(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.WorkloadId)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.MilestoneNumber)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.LensAlias)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.ImprovementSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}