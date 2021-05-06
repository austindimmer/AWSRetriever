using Amazon;
using Amazon.Appflow;
using Amazon.Appflow.Model;
using Amazon.Runtime;

namespace CloudOps.Appflow
{
    public class ListFlowsOperation : Operation
    {
        public override string Name => "ListFlows";

        public override string Description => " Lists all of the flows associated with your account. ";
 
        public override string RequestURI => "/list-flows";

        public override string Method => "POST";

        public override string ServiceName => "Appflow";

        public override string ServiceID => "Appflow";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAppflowConfig config = new AmazonAppflowConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAppflowClient client = new AmazonAppflowClient(creds, config);
            
            ListFlowsResponse resp = new ListFlowsResponse();
            do
            {
                ListFlowsRequest req = new ListFlowsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListFlowsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Flows)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}