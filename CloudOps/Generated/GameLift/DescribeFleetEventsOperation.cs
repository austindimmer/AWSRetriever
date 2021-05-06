using Amazon;
using Amazon.GameLift;
using Amazon.GameLift.Model;
using Amazon.Runtime;

namespace CloudOps.GameLift
{
    public class DescribeFleetEventsOperation : Operation
    {
        public override string Name => "DescribeFleetEvents";

        public override string Description => "Retrieves entries from a fleet&#39;s event log. Fleet events are initiated by changes in status, such as during fleet creation and termination, changes in capacity, etc. If a fleet has multiple locations, events are also initiated by changes to status and capacity in remote locations.  You can specify a time range to limit the result set. Use the pagination parameters to retrieve results as a set of sequential pages.  If successful, a collection of event log entries matching the request are returned.  Learn more   Setting up GameLift fleets   Related actions   ListFleets | DescribeEC2InstanceLimits | DescribeFleetAttributes | DescribeFleetCapacity | DescribeFleetEvents | DescribeFleetLocationAttributes | DescribeFleetPortSettings | DescribeFleetUtilization | DescribeRuntimeConfiguration | DescribeScalingPolicies | All APIs by task ";
 
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
            
            DescribeFleetEventsResponse resp = new DescribeFleetEventsResponse();
            do
            {
                DescribeFleetEventsRequest req = new DescribeFleetEventsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    Limit = maxItems
                                        
                };

                resp = await client.DescribeFleetEventsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Events)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}