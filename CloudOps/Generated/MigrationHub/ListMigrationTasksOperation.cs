using Amazon;
using Amazon.MigrationHub;
using Amazon.MigrationHub.Model;
using Amazon.Runtime;

namespace CloudOps.MigrationHub
{
    public class ListMigrationTasksOperation : Operation
    {
        public override string Name => "ListMigrationTasks";

        public override string Description => "Lists all, or filtered by resource name, migration tasks associated with the user account making this call. This API has the following traits:   Can show a summary list of the most recent migration tasks.   Can show a summary list of migration tasks associated with a given discovered resource.   Lists migration tasks in a paginated interface.  ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "MigrationHub";

        public override string ServiceID => "Migration Hub";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMigrationHubConfig config = new AmazonMigrationHubConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMigrationHubClient client = new AmazonMigrationHubClient(creds, config);
            
            ListMigrationTasksResponse resp = new ListMigrationTasksResponse();
            do
            {
                ListMigrationTasksRequest req = new ListMigrationTasksRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListMigrationTasks(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.MigrationTaskSummaryList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}