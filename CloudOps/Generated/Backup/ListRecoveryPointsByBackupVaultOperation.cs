using Amazon;
using Amazon.Backup;
using Amazon.Backup.Model;
using Amazon.Runtime;

namespace CloudOps.Backup
{
    public class ListRecoveryPointsByBackupVaultOperation : Operation
    {
        public override string Name => "ListRecoveryPointsByBackupVault";

        public override string Description => "Returns detailed information about the recovery points stored in a backup vault.";
 
        public override string RequestURI => "/backup-vaults/{backupVaultName}/recovery-points/";

        public override string Method => "GET";

        public override string ServiceName => "Backup";

        public override string ServiceID => "Backup";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonBackupConfig config = new AmazonBackupConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonBackupClient client = new AmazonBackupClient(creds, config);
            
            ListRecoveryPointsByBackupVaultResponse resp = new ListRecoveryPointsByBackupVaultResponse();
            do
            {
                try
                {
                    ListRecoveryPointsByBackupVaultRequest req = new ListRecoveryPointsByBackupVaultRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListRecoveryPointsByBackupVaultAsync(req);
                    
                    foreach (var obj in resp.RecoveryPoints)
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