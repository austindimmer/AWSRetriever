using Amazon;
using Amazon.ServerMigrationService;
using Amazon.ServerMigrationService.Model;
using Amazon.Runtime;

namespace CloudOps.ServerMigrationService
{
    public class GetReplicationJobsOperation : Operation
    {
        public override string Name => "GetReplicationJobs";

        public override string Description => "Describes the specified replication job or all of your replication jobs.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ServerMigrationService";

        public override string ServiceID => "SMS";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonServerMigrationServiceConfig config = new AmazonServerMigrationServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonServerMigrationServiceClient client = new AmazonServerMigrationServiceClient(creds, config);
            
            GetReplicationJobsResponse resp = new GetReplicationJobsResponse();
            do
            {
                try
                {
                    GetReplicationJobsRequest req = new GetReplicationJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.GetReplicationJobsAsync(req);
                    
                    foreach (var obj in resp.ReplicationJobList)
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