using Amazon;
using Amazon.GameLift;
using Amazon.GameLift.Model;
using Amazon.Runtime;

namespace CloudOps.GameLift
{
    public class DescribeFleetUtilizationOperation : Operation
    {
        public override string Name => "DescribeFleetUtilization";

        public override string Description => "Retrieves utilization statistics for one or more fleets. Utilization data provides a snapshot of how the fleet&#39;s hosting resources are currently being used. For fleets with remote locations, this operation retrieves data for the fleet&#39;s home Region only. See DescribeFleetLocationUtilization to get utilization statistics for a fleet&#39;s remote locations. This operation can be used in the following ways:    To get utilization data for one or more specific fleets, provide a list of fleet IDs or fleet ARNs.    To get utilization data for all fleets, do not provide a fleet identifier.    When requesting multiple fleets, use the pagination parameters to retrieve results as a set of sequential pages.  If successful, a FleetUtilization object is returned for each requested fleet ID, unless the fleet identifier is not found. Each fleet utilization object includes a Location property, which is set to the fleet&#39;s home Region.   Some API operations may limit the number of fleet IDs allowed in one request. If a request exceeds this limit, the request fails and the error message includes the maximum allowed.   Learn more   Setting up GameLift Fleets   GameLift Metrics for Fleets   Related actions   ListFleets | DescribeEC2InstanceLimits | DescribeFleetAttributes | DescribeFleetCapacity | DescribeFleetEvents | DescribeFleetLocationAttributes | DescribeFleetPortSettings | DescribeFleetUtilization | DescribeRuntimeConfiguration | DescribeScalingPolicies | All APIs by task ";
 
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
            
            DescribeFleetUtilizationResponse resp = new DescribeFleetUtilizationResponse();
            do
            {
                try
                {
                    DescribeFleetUtilizationRequest req = new DescribeFleetUtilizationRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.DescribeFleetUtilizationAsync(req);
                    
                    foreach (var obj in resp.FleetUtilization)
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