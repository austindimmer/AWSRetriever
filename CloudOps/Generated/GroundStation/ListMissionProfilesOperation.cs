using Amazon;
using Amazon.GroundStation;
using Amazon.GroundStation.Model;
using Amazon.Runtime;

namespace CloudOps.GroundStation
{
    public class ListMissionProfilesOperation : Operation
    {
        public override string Name => "ListMissionProfiles";

        public override string Description => "Returns a list of mission profiles.";
 
        public override string RequestURI => "/missionprofile";

        public override string Method => "GET";

        public override string ServiceName => "GroundStation";

        public override string ServiceID => "GroundStation";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGroundStationConfig config = new AmazonGroundStationConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGroundStationClient client = new AmazonGroundStationClient(creds, config);
            
            ListMissionProfilesResponse resp = new ListMissionProfilesResponse();
            do
            {
                try
                {
                    ListMissionProfilesRequest req = new ListMissionProfilesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListMissionProfilesAsync(req);
                    
                    foreach (var obj in resp.MissionProfileList)
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