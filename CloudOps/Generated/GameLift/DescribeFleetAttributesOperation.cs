using Amazon;
using Amazon.GameLift;
using Amazon.GameLift.Model;
using Amazon.Runtime;

namespace CloudOps.GameLift
{
    public class DescribeFleetAttributesOperation : Operation
    {
        public override string Name => "DescribeFleetAttributes";

        public override string Description => "Retrieves core fleet-wide properties, including the computing hardware and deployment configuration for all instances in the fleet. This operation can be used in the following ways:    To get attributes for one or more specific fleets, provide a list of fleet IDs or fleet ARNs.    To get attributes for all fleets, do not provide a fleet identifier.    When requesting attributes for multiple fleets, use the pagination parameters to retrieve results as a set of sequential pages.  If successful, a FleetAttributes object is returned for each fleet requested, unless the fleet identifier is not found.   Some API operations limit the number of fleet IDs that allowed in one request. If a request exceeds this limit, the request fails and the error message contains the maximum allowed number.   Learn more   Setting up GameLift fleets   Related actions   ListFleets | DescribeEC2InstanceLimits | DescribeFleetAttributes | DescribeFleetCapacity | DescribeFleetEvents | DescribeFleetLocationAttributes | DescribeFleetPortSettings | DescribeFleetUtilization | DescribeRuntimeConfiguration | DescribeScalingPolicies | All APIs by task ";
 
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
            
            DescribeFleetAttributesResponse resp = new DescribeFleetAttributesResponse();
            do
            {
                DescribeFleetAttributesRequest req = new DescribeFleetAttributesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    Limit = maxItems
                                        
                };

                resp = client.DescribeFleetAttributes(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.FleetAttributes)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}