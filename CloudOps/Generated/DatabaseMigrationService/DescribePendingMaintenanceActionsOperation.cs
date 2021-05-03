using Amazon;
using Amazon.DatabaseMigrationService;
using Amazon.DatabaseMigrationService.Model;
using Amazon.Runtime;

namespace CloudOps.DatabaseMigrationService
{
    public class DescribePendingMaintenanceActionsOperation : Operation
    {
        public override string Name => "DescribePendingMaintenanceActions";

        public override string Description => "For internal use only";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DatabaseMigrationService";

        public override string ServiceID => "Database Migration Service";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDatabaseMigrationServiceConfig config = new AmazonDatabaseMigrationServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDatabaseMigrationServiceClient client = new AmazonDatabaseMigrationServiceClient(creds, config);
            
            DescribePendingMaintenanceActionsResponse resp = new DescribePendingMaintenanceActionsResponse();
            do
            {
                DescribePendingMaintenanceActionsRequest req = new DescribePendingMaintenanceActionsRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = client.DescribePendingMaintenanceActions(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.PendingMaintenanceActions)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}