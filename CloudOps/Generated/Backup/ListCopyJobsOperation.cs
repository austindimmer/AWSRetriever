using Amazon;
using Amazon.Backup;
using Amazon.Backup.Model;
using Amazon.Runtime;

namespace CloudOps.Backup
{
    public class ListCopyJobsOperation : Operation
    {
        public override string Name => "ListCopyJobs";

        public override string Description => "Returns metadata about your copy jobs.";
 
        public override string RequestURI => "/copy-jobs/";

        public override string Method => "GET";

        public override string ServiceName => "Backup";

        public override string ServiceID => "Backup";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonBackupConfig config = new AmazonBackupConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonBackupClient client = new AmazonBackupClient(creds, config);
            
            ListCopyJobsResponse resp = new ListCopyJobsResponse();
            do
            {
                ListCopyJobsRequest req = new ListCopyJobsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListCopyJobs(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.CopyJobs)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}