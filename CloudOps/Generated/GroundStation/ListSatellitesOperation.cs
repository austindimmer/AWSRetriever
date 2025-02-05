using Amazon;
using Amazon.GroundStation;
using Amazon.GroundStation.Model;
using Amazon.Runtime;

namespace CloudOps.GroundStation
{
    public class ListSatellitesOperation : Operation
    {
        public override string Name => "ListSatellites";

        public override string Description => "Returns a list of satellites.";
 
        public override string RequestURI => "/satellite";

        public override string Method => "GET";

        public override string ServiceName => "GroundStation";

        public override string ServiceID => "GroundStation";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGroundStationConfig config = new AmazonGroundStationConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGroundStationClient client = new AmazonGroundStationClient(creds, config);
            
            ListSatellitesResponse resp = new ListSatellitesResponse();
            do
            {
                ListSatellitesRequest req = new ListSatellitesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListSatellites(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Satellites)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}