using Amazon;
using Amazon.DataSync;
using Amazon.DataSync.Model;
using Amazon.Runtime;

namespace CloudOps.DataSync
{
    public class ListAgentsOperation : Operation
    {
        public override string Name => "ListAgents";

        public override string Description => "Returns a list of agents owned by an AWS account in the AWS Region specified in the request. The returned list is ordered by agent Amazon Resource Name (ARN). By default, this operation returns a maximum of 100 agents. This operation supports pagination that enables you to optionally reduce the number of agents returned in a response. If you have more agents than are returned in a response (that is, the response returns only a truncated list of your agents), the response contains a marker that you can specify in your next request to fetch the next page of agents.";
 
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
            
            ListAgentsResponse resp = new ListAgentsResponse();
            do
            {
                ListAgentsRequest req = new ListAgentsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListAgentsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Agents)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}