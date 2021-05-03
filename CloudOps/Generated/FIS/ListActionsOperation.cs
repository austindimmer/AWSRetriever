using Amazon;
using Amazon.FIS;
using Amazon.FIS.Model;
using Amazon.Runtime;

namespace CloudOps.FIS
{
    public class ListActionsOperation : Operation
    {
        public override string Name => "ListActions";

        public override string Description => "Lists the available AWS FIS actions.";
 
        public override string RequestURI => "/actions";

        public override string Method => "GET";

        public override string ServiceName => "FIS";

        public override string ServiceID => "fis";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonFISConfig config = new AmazonFISConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonFISClient client = new AmazonFISClient(creds, config);
            
            ListActionsResponse resp = new ListActionsResponse();
            do
            {
                ListActionsRequest req = new ListActionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListActions(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Actions)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}