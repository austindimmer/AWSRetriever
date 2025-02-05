using Amazon;
using Amazon.IoTSiteWise;
using Amazon.IoTSiteWise.Model;
using Amazon.Runtime;

namespace CloudOps.IoTSiteWise
{
    public class ListPortalsOperation : Operation
    {
        public override string Name => "ListPortals";

        public override string Description => "Retrieves a paginated list of AWS IoT SiteWise Monitor portals.";
 
        public override string RequestURI => "/portals";

        public override string Method => "GET";

        public override string ServiceName => "IoTSiteWise";

        public override string ServiceID => "IoTSiteWise";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTSiteWiseConfig config = new AmazonIoTSiteWiseConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTSiteWiseClient client = new AmazonIoTSiteWiseClient(creds, config);
            
            ListPortalsResponse resp = new ListPortalsResponse();
            do
            {
                ListPortalsRequest req = new ListPortalsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListPortals(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.PortalSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}