using Amazon;
using Amazon.GameLift;
using Amazon.GameLift.Model;
using Amazon.Runtime;

namespace CloudOps.GameLift
{
    public class DescribeScalingPoliciesOperation : Operation
    {
        public override string Name => "DescribeScalingPolicies";

        public override string Description => "Retrieves all scaling policies applied to a fleet. To get a fleet&#39;s scaling policies, specify the fleet ID. You can filter this request by policy status, such as to retrieve only active scaling policies. Use the pagination parameters to retrieve results as a set of sequential pages. If successful, set of ScalingPolicy objects is returned for the fleet. A fleet may have all of its scaling policies suspended (StopFleetActions). This operation does not affect the status of the scaling policies, which remains ACTIVE. To see whether a fleet&#39;s scaling policies are in force or suspended, call DescribeFleetAttributes and check the stopped actions.  Related actions   DescribeFleetCapacity | UpdateFleetCapacity | DescribeEC2InstanceLimits | PutScalingPolicy | DescribeScalingPolicies | DeleteScalingPolicy | StopFleetActions | StartFleetActions | All APIs by task ";
 
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
            
            DescribeScalingPoliciesResponse resp = new DescribeScalingPoliciesResponse();
            do
            {
                try
                {
                    DescribeScalingPoliciesRequest req = new DescribeScalingPoliciesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.DescribeScalingPoliciesAsync(req);
                    
                    foreach (var obj in resp.ScalingPolicies)
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