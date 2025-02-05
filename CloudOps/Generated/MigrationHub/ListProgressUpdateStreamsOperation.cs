using Amazon;
using Amazon.MigrationHub;
using Amazon.MigrationHub.Model;
using Amazon.Runtime;

namespace CloudOps.MigrationHub
{
    public class ListProgressUpdateStreamsOperation : Operation
    {
        public override string Name => "ListProgressUpdateStreams";

        public override string Description => "Lists progress update streams associated with the user account making this call.";
 
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
            
            ListProgressUpdateStreamsResponse resp = new ListProgressUpdateStreamsResponse();
            do
            {
                ListProgressUpdateStreamsRequest req = new ListProgressUpdateStreamsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListProgressUpdateStreams(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ProgressUpdateStreamSummaryList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}