using Amazon;
using Amazon.MediaPackage;
using Amazon.MediaPackage.Model;
using Amazon.Runtime;

namespace CloudOps.MediaPackage
{
    public class ListHarvestJobsOperation : Operation
    {
        public override string Name => "ListHarvestJobs";

        public override string Description => "Returns a collection of HarvestJob records.";
 
        public override string RequestURI => "/harvest_jobs";

        public override string Method => "GET";

        public override string ServiceName => "MediaPackage";

        public override string ServiceID => "MediaPackage";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaPackageConfig config = new AmazonMediaPackageConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaPackageClient client = new AmazonMediaPackageClient(creds, config);
            
            ListHarvestJobsResponse resp = new ListHarvestJobsResponse();
            do
            {
                ListHarvestJobsRequest req = new ListHarvestJobsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListHarvestJobsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.HarvestJobs)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}