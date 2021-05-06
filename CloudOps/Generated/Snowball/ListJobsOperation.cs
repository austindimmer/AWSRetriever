using Amazon;
using Amazon.Snowball;
using Amazon.Snowball.Model;
using Amazon.Runtime;

namespace CloudOps.Snowball
{
    public class ListJobsOperation : Operation
    {
        public override string Name => "ListJobs";

        public override string Description => "Returns an array of JobListEntry objects of the specified length. Each JobListEntry object contains a job&#39;s state, a job&#39;s ID, and a value that indicates whether the job is a job part, in the case of export jobs. Calling this API action in one of the US regions will return jobs from the list of all jobs associated with this account in all US regions.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Snowball";

        public override string ServiceID => "Snowball";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSnowballConfig config = new AmazonSnowballConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSnowballClient client = new AmazonSnowballClient(creds, config);
            
            ListJobsResponse resp = new ListJobsResponse();
            do
            {
                ListJobsRequest req = new ListJobsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListJobsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.JobListEntries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}