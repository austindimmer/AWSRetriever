using Amazon;
using Amazon.DatabaseMigrationService;
using Amazon.DatabaseMigrationService.Model;
using Amazon.Runtime;

namespace CloudOps.DatabaseMigrationService
{
    public class DescribeReplicationInstanceTaskLogsOperation : Operation
    {
        public override string Name => "DescribeReplicationInstanceTaskLogs";

        public override string Description => "Returns information about the task logs for the specified task.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DatabaseMigrationService";

        public override string ServiceID => "Database Migration Service";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDatabaseMigrationServiceConfig config = new AmazonDatabaseMigrationServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDatabaseMigrationServiceClient client = new AmazonDatabaseMigrationServiceClient(creds, config);
            
            DescribeReplicationInstanceTaskLogsResponse resp = new DescribeReplicationInstanceTaskLogsResponse();
            do
            {
                try
                {
                    DescribeReplicationInstanceTaskLogsRequest req = new DescribeReplicationInstanceTaskLogsRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeReplicationInstanceTaskLogsAsync(req);
                    
                    foreach (var obj in resp.ReplicationInstanceArn)
                    {
                        AddObject(obj);
                    }
                    
                    foreach (var obj in resp.ReplicationInstanceTaskLogs)
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
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}