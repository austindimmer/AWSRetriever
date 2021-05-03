using Amazon;
using Amazon.MigrationHub;
using Amazon.MigrationHub.Model;
using Amazon.Runtime;

namespace CloudOps.MigrationHub
{
    public class ListApplicationStatesOperation : Operation
    {
        public override string Name => "ListApplicationStates";

        public override string Description => "Lists all the migration statuses for your applications. If you use the optional ApplicationIds parameter, only the migration statuses for those applications will be returned.";
 
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
            
            ListApplicationStatesResponse resp = new ListApplicationStatesResponse();
            do
            {
                ListApplicationStatesRequest req = new ListApplicationStatesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListApplicationStates(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ApplicationStateList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}