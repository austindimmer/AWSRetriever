using Amazon;
using Amazon.RoboMaker;
using Amazon.RoboMaker.Model;
using Amazon.Runtime;

namespace CloudOps.RoboMaker
{
    public class ListFleetsOperation : Operation
    {
        public override string Name => "ListFleets";

        public override string Description => "Returns a list of fleets. You can optionally provide filters to retrieve specific fleets. ";
 
        public override string RequestURI => "/listFleets";

        public override string Method => "POST";

        public override string ServiceName => "RoboMaker";

        public override string ServiceID => "RoboMaker";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRoboMakerConfig config = new AmazonRoboMakerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRoboMakerClient client = new AmazonRoboMakerClient(creds, config);
            
            ListFleetsResponse resp = new ListFleetsResponse();
            do
            {
                ListFleetsRequest req = new ListFleetsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListFleets(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.FleetDetails)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}