using Amazon;
using Amazon.GameLift;
using Amazon.GameLift.Model;
using Amazon.Runtime;

namespace CloudOps.GameLift
{
    public class DescribeMatchmakingConfigurationsOperation : Operation
    {
        public override string Name => "DescribeMatchmakingConfigurations";

        public override string Description => "Retrieves the details of FlexMatch matchmaking configurations.  This operation offers the following options: (1) retrieve all matchmaking configurations, (2) retrieve configurations for a specified list, or (3) retrieve all configurations that use a specified rule set name. When requesting multiple items, use the pagination parameters to retrieve results as a set of sequential pages.  If successful, a configuration is returned for each requested name. When specifying a list of names, only configurations that currently exist are returned.   Learn more    Setting up FlexMatch matchmakers   Related actions   CreateMatchmakingConfiguration | DescribeMatchmakingConfigurations | UpdateMatchmakingConfiguration | DeleteMatchmakingConfiguration | CreateMatchmakingRuleSet | DescribeMatchmakingRuleSets | ValidateMatchmakingRuleSet | DeleteMatchmakingRuleSet | All APIs by task ";
 
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
            
            DescribeMatchmakingConfigurationsResponse resp = new DescribeMatchmakingConfigurationsResponse();
            do
            {
                try
                {
                    DescribeMatchmakingConfigurationsRequest req = new DescribeMatchmakingConfigurationsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.DescribeMatchmakingConfigurationsAsync(req);
                    
                    foreach (var obj in resp.Configurations)
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