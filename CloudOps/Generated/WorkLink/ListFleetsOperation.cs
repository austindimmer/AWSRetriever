using Amazon;
using Amazon.WorkLink;
using Amazon.WorkLink.Model;
using Amazon.Runtime;

namespace CloudOps.WorkLink
{
    public class ListFleetsOperation : Operation
    {
        public override string Name => "ListFleets";

        public override string Description => "Retrieves a list of fleets for the current account and Region.";
 
        public override string RequestURI => "/listFleets";

        public override string Method => "POST";

        public override string ServiceName => "WorkLink";

        public override string ServiceID => "WorkLink";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonWorkLinkConfig config = new AmazonWorkLinkConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonWorkLinkClient client = new AmazonWorkLinkClient(creds, config);
            
            ListFleetsResponse resp = new ListFleetsResponse();
            do
            {
                try
                {
                    ListFleetsRequest req = new ListFleetsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListFleetsAsync(req);
                    
                    foreach (var obj in resp.FleetSummaryList)
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