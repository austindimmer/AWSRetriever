using Amazon;
using Amazon.MediaTailor;
using Amazon.MediaTailor.Model;
using Amazon.Runtime;

namespace CloudOps.MediaTailor
{
    public class ListSourceLocationsOperation : Operation
    {
        public override string Name => "ListSourceLocations";

        public override string Description => "Retrieves a list of source locations.";
 
        public override string RequestURI => "/sourceLocations";

        public override string Method => "GET";

        public override string ServiceName => "MediaTailor";

        public override string ServiceID => "MediaTailor";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaTailorConfig config = new AmazonMediaTailorConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaTailorClient client = new AmazonMediaTailorClient(creds, config);
            
            ListSourceLocationsResponse resp = new ListSourceLocationsResponse();
            do
            {
                ListSourceLocationsRequest req = new ListSourceLocationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListSourceLocationsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Items)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}