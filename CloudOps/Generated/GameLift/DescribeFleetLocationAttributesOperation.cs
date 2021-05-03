using Amazon;
using Amazon.GameLift;
using Amazon.GameLift.Model;
using Amazon.Runtime;

namespace CloudOps.GameLift
{
    public class DescribeFleetLocationAttributesOperation : Operation
    {
        public override string Name => "DescribeFleetLocationAttributes";

        public override string Description => "Retrieves information on a fleet&#39;s remote locations, including life-cycle status and any suspended fleet activity.  This operation can be used in the following ways:    To get data for specific locations, provide a fleet identifier and a list of locations. Location data is returned in the order that it is requested.    To get data for all locations, provide a fleet identifier only. Location data is returned in no particular order.    When requesting attributes for multiple locations, use the pagination parameters to retrieve results as a set of sequential pages.  If successful, a LocationAttributes object is returned for each requested location. If the fleet does not have a requested location, no information is returned. This operation does not return the home Region. To get information on a fleet&#39;s home Region, call DescribeFleetAttributes.  Learn more   Setting up GameLift fleets   Related actions   CreateFleetLocations | DescribeFleetLocationAttributes | DescribeFleetLocationCapacity | DescribeFleetLocationUtilization | DescribeFleetAttributes | DescribeFleetCapacity | DescribeFleetUtilization | UpdateFleetCapacity | StopFleetActions | DeleteFleetLocations | All APIs by task ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "GameLift";

        public override string ServiceID => "GameLift";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGameLiftConfig config = new AmazonGameLiftConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGameLiftClient client = new AmazonGameLiftClient(creds, config);
            
            DescribeFleetLocationAttributesResponse resp = new DescribeFleetLocationAttributesResponse();
            do
            {
                DescribeFleetLocationAttributesRequest req = new DescribeFleetLocationAttributesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    Limit = maxItems
                                        
                };

                resp = client.DescribeFleetLocationAttributes(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.LocationAttributes)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.FleetId)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.FleetArn)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}