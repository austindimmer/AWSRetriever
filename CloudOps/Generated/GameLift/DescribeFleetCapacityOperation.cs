using Amazon;
using Amazon.GameLift;
using Amazon.GameLift.Model;
using Amazon.Runtime;

namespace CloudOps.GameLift
{
    public class DescribeFleetCapacityOperation : Operation
    {
        public override string Name => "DescribeFleetCapacity";

        public override string Description => "Retrieves the resource capacity settings for one or more fleets. The data returned includes the current fleet capacity (number of EC2 instances), and settings that can control how capacity scaling. For fleets with remote locations, this operation retrieves data for the fleet&#39;s home Region only. See DescribeFleetLocationCapacity to get capacity settings for a fleet&#39;s remote locations. This operation can be used in the following ways:    To get capacity data for one or more specific fleets, provide a list of fleet IDs or fleet ARNs.    To get capacity data for all fleets, do not provide a fleet identifier.    When requesting multiple fleets, use the pagination parameters to retrieve results as a set of sequential pages.  If successful, a FleetCapacity object is returned for each requested fleet ID. Each FleetCapacity object includes a Location property, which is set to the fleet&#39;s home Region. When a list of fleet IDs is provided, attribute objects are returned only for fleets that currently exist.  Some API operations may limit the number of fleet IDs that are allowed in one request. If a request exceeds this limit, the request fails and the error message includes the maximum allowed.   Learn more   Setting up GameLift fleets   GameLift metrics for fleets   Related actions   ListFleets | DescribeEC2InstanceLimits | DescribeFleetAttributes | DescribeFleetCapacity | DescribeFleetEvents | DescribeFleetLocationAttributes | DescribeFleetPortSettings | DescribeFleetUtilization | DescribeRuntimeConfiguration | DescribeScalingPolicies | All APIs by task ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "GameLift";

        public override string ServiceID => "GameLift";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGameLiftConfig config = new AmazonGameLiftConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGameLiftClient client = new AmazonGameLiftClient(creds, config);
            
            DescribeFleetCapacityResponse resp = new DescribeFleetCapacityResponse();
            do
            {
                try
                {
                    DescribeFleetCapacityRequest req = new DescribeFleetCapacityRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.DescribeFleetCapacityAsync(req);
                    
                    foreach (var obj in resp.FleetCapacity)
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