using Amazon;
using Amazon.Backup;
using Amazon.Backup.Model;
using Amazon.Runtime;

namespace CloudOps.Backup
{
    public class ListBackupPlanTemplatesOperation : Operation
    {
        public override string Name => "ListBackupPlanTemplates";

        public override string Description => "Returns metadata of your saved backup plan templates, including the template ID, name, and the creation and deletion dates.";
 
        public override string RequestURI => "/backup/template/plans";

        public override string Method => "GET";

        public override string ServiceName => "Backup";

        public override string ServiceID => "Backup";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonBackupConfig config = new AmazonBackupConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonBackupClient client = new AmazonBackupClient(creds, config);
            
            ListBackupPlanTemplatesResponse resp = new ListBackupPlanTemplatesResponse();
            do
            {
                try
                {
                    ListBackupPlanTemplatesRequest req = new ListBackupPlanTemplatesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListBackupPlanTemplatesAsync(req);
                    
                    foreach (var obj in resp.BackupPlanTemplatesList)
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