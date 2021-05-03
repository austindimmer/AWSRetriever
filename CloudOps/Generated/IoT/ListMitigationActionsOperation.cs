using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListMitigationActionsOperation : Operation
    {
        public override string Name => "ListMitigationActions";

        public override string Description => "Gets a list of all mitigation actions that match the specified filter criteria.";
 
        public override string RequestURI => "/mitigationactions/actions";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListMitigationActionsResponse resp = new ListMitigationActionsResponse();
            do
            {
                ListMitigationActionsRequest req = new ListMitigationActionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListMitigationActions(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ActionIdentifiers)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}