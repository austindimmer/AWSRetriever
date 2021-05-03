using Amazon;
using Amazon.LocationService;
using Amazon.LocationService.Model;
using Amazon.Runtime;

namespace CloudOps.LocationService
{
    public class ListGeofenceCollectionsOperation : Operation
    {
        public override string Name => "ListGeofenceCollections";

        public override string Description => "Lists geofence collections in your AWS account.";
 
        public override string RequestURI => "/geofencing/v0/list-collections";

        public override string Method => "POST";

        public override string ServiceName => "LocationService";

        public override string ServiceID => "Location";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLocationServiceConfig config = new AmazonLocationServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLocationServiceClient client = new AmazonLocationServiceClient(creds, config);
            
            ListGeofenceCollectionsResponse resp = new ListGeofenceCollectionsResponse();
            do
            {
                ListGeofenceCollectionsRequest req = new ListGeofenceCollectionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListGeofenceCollections(req);
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