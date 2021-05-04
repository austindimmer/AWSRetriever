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

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonBackupConfig config = new AmazonBackupConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonBackupClient client = new AmazonBackupClient(creds, config);
            
            ListBackupSelectionsResponse resp = new ListBackupSelectionsResponse();
            do
            {
                ListBackupSelectionsRequest req = new ListBackupSelectionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListBackupSelections(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.BackupSelectionsList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}