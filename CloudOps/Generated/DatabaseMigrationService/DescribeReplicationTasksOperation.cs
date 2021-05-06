using Amazon;
using Amazon.DatabaseMigrationService;
using Amazon.DatabaseMigrationService.Model;
using Amazon.Runtime;

namespace CloudOps.DatabaseMigrationService
{
    public class DescribeReplicationTasksOperation : Operation
    {
        public override string Name => "DescribeReplicationTasks";

        public override string Description => "Returns information about replication tasks for your account in the current region.";
 
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
            
            DescribeReplicationTasksResponse resp = new DescribeReplicationTasksResponse();
            do
            {
                DescribeReplicationTasksRequest req = new DescribeReplicationTasksRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = await client.DescribeReplicationTasksAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ReplicationTasks)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}