using Amazon;
using Amazon.GroundStation;
using Amazon.GroundStation.Model;
using Amazon.Runtime;

namespace CloudOps.GroundStation
{
    public class ListDataflowEndpointGroupsOperation : Operation
    {
        public override string Name => "ListDataflowEndpointGroups";

        public override string Description => "Returns a list of DataflowEndpoint groups.";
 
        public override string RequestURI => "/dataflowEndpointGroup";

        public override string Method => "GET";

        public override string ServiceName => "GroundStation";

        public override string ServiceID => "GroundStation";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGroundStationConfig config = new AmazonGroundStationConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGroundStationClient client = new AmazonGroundStationClient(creds, config);
            
            ListDataflowEndpointGroupsResponse resp = new ListDataflowEndpointGroupsResponse();
            do
            {
                ListDataflowEndpointGroupsRequest req = new ListDataflowEndpointGroupsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListDataflowEndpointGroups(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.DataflowEndpointGroupList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}