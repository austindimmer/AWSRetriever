using Amazon;
using Amazon.GameLift;
using Amazon.GameLift.Model;
using Amazon.Runtime;

namespace CloudOps.GameLift
{
    public class ListFleetsOperation : Operation
    {
        public override string Name => "ListFleets";

        public override string Description => "Retrieves a collection of fleet resources in an AWS Region. You can call this operation to get fleets in a previously selected default Region (see https://docs.aws.amazon.com/credref/latest/refdocs/setting-global-region.htmlor specify a Region in your request. You can filter the result set to find only those fleets that are deployed with a specific build or script. For fleets that have multiple locations, this operation retrieves fleets based on their home Region only. This operation can be used in the following ways:    To get a list of all fleets in a Region, don&#39;t provide a build or script identifier.    To get a list of all fleets where a specific custom game build is deployed, provide the build ID.   To get a list of all Realtime Servers fleets with a specific configuration script, provide the script ID.    Use the pagination parameters to retrieve results as a set of sequential pages.  If successful, a list of fleet IDs that match the request parameters is returned. A NextToken value is also returned if there are more result pages to retrieve.  Fleet resources are not listed in a particular order.   Learn more   Setting up GameLift fleets   Related actions   CreateFleet | UpdateFleetCapacity | PutScalingPolicy | DescribeEC2InstanceLimits | DescribeFleetAttributes | DescribeFleetLocationAttributes | UpdateFleetAttributes | StopFleetActions | DeleteFleet | All APIs by task ";
 
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
            
            ListFleetsResponse resp = new ListFleetsResponse();
            do
            {
                try
                {
                    ListFleetsRequest req = new ListFleetsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.ListFleetsAsync(req);
                    
                    foreach (var obj in resp.FleetIds)
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