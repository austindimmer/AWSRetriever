using Amazon;
using Amazon.Backup;
using Amazon.Backup.Model;
using Amazon.Runtime;

namespace CloudOps.Backup
{
    public class ListRecoveryPointsByResourceOperation : Operation
    {
        public override string Name => "ListRecoveryPointsByResource";

        public override string Description => "Returns detailed information about recovery points of the type specified by a resource Amazon Resource Name (ARN).";
 
        public override string RequestURI => "/resources/{resourceArn}/recovery-points/";

        public override string Method => "GET";

        public override string ServiceName => "Backup";

        public override string ServiceID => "Backup";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonBackupConfig config = new AmazonBackupConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonBackupClient client = new AmazonBackupClient(creds, config);
            
            ListRecoveryPointsByResourceResponse resp = new ListRecoveryPointsByResourceResponse();
            do
            {
                ListRecoveryPointsByResourceRequest req = new ListRecoveryPointsByResourceRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListRecoveryPointsByResourceAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.RecoveryPoints)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}