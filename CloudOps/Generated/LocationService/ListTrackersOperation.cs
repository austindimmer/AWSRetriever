using Amazon;
using Amazon.LocationService;
using Amazon.LocationService.Model;
using Amazon.Runtime;

namespace CloudOps.LocationService
{
    public class ListTrackersOperation : Operation
    {
        public override string Name => "ListTrackers";

        public override string Description => "Lists tracker resources in your AWS account.";
 
        public override string RequestURI => "/tracking/v0/list-trackers";

        public override string Method => "POST";

        public override string ServiceName => "LocationService";

        public override string ServiceID => "Location";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLocationServiceConfig config = new AmazonLocationServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLocationServiceClient client = new AmazonLocationServiceClient(creds, config);
            
            ListTrackersResponse resp = new ListTrackersResponse();
            do
            {
                ListTrackersRequest req = new ListTrackersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListTrackersAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Entries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}