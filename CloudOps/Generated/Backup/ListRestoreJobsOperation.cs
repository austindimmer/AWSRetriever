using Amazon;
using Amazon.Backup;
using Amazon.Backup.Model;
using Amazon.Runtime;

namespace CloudOps.Backup
{
    public class ListRestoreJobsOperation : Operation
    {
        public override string Name => "ListRestoreJobs";

        public override string Description => "Returns a list of jobs that AWS Backup initiated to restore a saved resource, including metadata about the recovery process.";
 
        public override string RequestURI => "/restore-jobs/";

        public override string Method => "GET";

        public override string ServiceName => "Backup";

        public override string ServiceID => "Backup";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonBackupConfig config = new AmazonBackupConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonBackupClient client = new AmazonBackupClient(creds, config);
            
            ListRestoreJobsResponse resp = new ListRestoreJobsResponse();
            do
            {
                try
                {
                    ListRestoreJobsRequest req = new ListRestoreJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListRestoreJobsAsync(req);
                    
                    foreach (var obj in resp.RestoreJobs)
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