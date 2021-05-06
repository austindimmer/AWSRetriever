using Amazon;
using Amazon.Backup;
using Amazon.Backup.Model;
using Amazon.Runtime;

namespace CloudOps.Backup
{
    public class ListBackupVaultsOperation : Operation
    {
        public override string Name => "ListBackupVaults";

        public override string Description => "Returns a list of recovery point storage containers along with information about them.";
 
        public override string RequestURI => "/backup-vaults/";

        public override string Method => "GET";

        public override string ServiceName => "Backup";

        public override string ServiceID => "Backup";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonBackupConfig config = new AmazonBackupConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonBackupClient client = new AmazonBackupClient(creds, config);
            
            ListBackupVaultsResponse resp = new ListBackupVaultsResponse();
            do
            {
                try
                {
                    ListBackupVaultsRequest req = new ListBackupVaultsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListBackupVaultsAsync(req);
                    
                    foreach (var obj in resp.BackupVaultList)
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