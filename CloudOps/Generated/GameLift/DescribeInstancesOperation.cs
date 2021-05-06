using Amazon;
using Amazon.GameLift;
using Amazon.GameLift.Model;
using Amazon.Runtime;

namespace CloudOps.GameLift
{
    public class DescribeInstancesOperation : Operation
    {
        public override string Name => "DescribeInstances";

        public override string Description => "Retrieves information about a fleet&#39;s instances, including instance IDs, connection data, and status.  This operation can be used in the following ways:   To get information on all instances that are deployed to a fleet&#39;s home Region, provide the fleet ID.   To get information on all instances that are deployed to a fleet&#39;s remote location, provide the fleet ID and location name.   To get information on a specific instance in a fleet, provide the fleet ID and instance ID.   Use the pagination parameters to retrieve results as a set of sequential pages.  If successful, an Instance object is returned for each requested instance. Instances are not returned in any particular order.   Learn more   Remotely Access Fleet Instances   Debug Fleet Issues   Related actions   DescribeInstances | GetInstanceAccess | DescribeEC2InstanceLimits | All APIs by task ";
 
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
            
            DescribeInstancesResponse resp = new DescribeInstancesResponse();
            do
            {
                DescribeInstancesRequest req = new DescribeInstancesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    Limit = maxItems
                                        
                };

                resp = await client.DescribeInstancesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Instances)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}