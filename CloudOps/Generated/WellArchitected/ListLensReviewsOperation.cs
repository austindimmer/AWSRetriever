using Amazon;
using Amazon.WellArchitected;
using Amazon.WellArchitected.Model;
using Amazon.Runtime;

namespace CloudOps.WellArchitected
{
    public class ListLensReviewsOperation : Operation
    {
        public override string Name => "ListLensReviews";

        public override string Description => "List lens reviews.";
 
        public override string RequestURI => "/workloads/{WorkloadId}/lensReviews";

        public override string Method => "GET";

        public override string ServiceName => "WellArchitected";

        public override string ServiceID => "WellArchitected";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonWellArchitectedConfig config = new AmazonWellArchitectedConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonWellArchitectedClient client = new AmazonWellArchitectedClient(creds, config);
            
            ListLensReviewsResponse resp = new ListLensReviewsResponse();
            do
            {
                try
                {
                    ListLensReviewsRequest req = new ListLensReviewsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListLensReviewsAsync(req);
                    
                    foreach (var obj in resp.LensReviewSummaries)
                    {
                        AddObject(obj);
                    }
                    
                    foreach (var obj in resp.WorkloadId)
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