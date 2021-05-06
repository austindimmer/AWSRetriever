using Amazon;
using Amazon.DataSync;
using Amazon.DataSync.Model;
using Amazon.Runtime;

namespace CloudOps.DataSync
{
    public class ListTasksOperation : Operation
    {
        public override string Name => "ListTasks";

        public override string Description => "Returns a list of all the tasks.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DataSync";

        public override string ServiceID => "DataSync";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDataSyncConfig config = new AmazonDataSyncConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDataSyncClient client = new AmazonDataSyncClient(creds, config);
            
            ListTasksResponse resp = new ListTasksResponse();
            do
            {
                ListTasksRequest req = new ListTasksRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListTasksAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Tasks)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}