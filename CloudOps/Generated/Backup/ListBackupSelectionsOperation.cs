using Amazon;
using Amazon.Backup;
using Amazon.Backup.Model;
using Amazon.Runtime;

namespace CloudOps.Backup
{
    public class ListBackupSelectionsOperation : Operation
    {
        public override string Name => "ListBackupSelections";

        public override string Description => "Returns an array containing metadata of the resources associated with the target backup plan.";
 
        public override string RequestURI => "/backup/plans/{backupPlanId}/selections/";

        public override string Method => "GET";

        public override string ServiceName => "Backup";

        public override string ServiceID => "Backup";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonBackupConfig config = new AmazonBackupConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonBackupClient client = new AmazonBackupClient(creds, config);
            
            ListBackupSelectionsResponse resp = new ListBackupSelectionsResponse();
            do
            {
                try
                {
                    ListBackupSelectionsRequest req = new ListBackupSelectionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListBackupSelectionsAsync(req);
                    
                    foreach (var obj in resp.BackupSelectionsList)
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