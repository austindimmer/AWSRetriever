using Amazon;
using Amazon.Backup;
using Amazon.Backup.Model;
using Amazon.Runtime;

namespace CloudOps.Backup
{
    public class ListBackupPlanVersionsOperation : Operation
    {
        public override string Name => "ListBackupPlanVersions";

        public override string Description => "Returns version metadata of your backup plans, including Amazon Resource Names (ARNs), backup plan IDs, creation and deletion dates, plan names, and version IDs.";
 
        public override string RequestURI => "/backup/plans/{backupPlanId}/versions/";

        public override string Method => "GET";

        public override string ServiceName => "Backup";

        public override string ServiceID => "Backup";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonBackupConfig config = new AmazonBackupConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonBackupClient client = new AmazonBackupClient(creds, config);
            
            ListBackupPlanVersionsResponse resp = new ListBackupPlanVersionsResponse();
            do
            {
                try
                {
                    ListBackupPlanVersionsRequest req = new ListBackupPlanVersionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListBackupPlanVersionsAsync(req);
                    
                    foreach (var obj in resp.BackupPlanVersionsList)
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