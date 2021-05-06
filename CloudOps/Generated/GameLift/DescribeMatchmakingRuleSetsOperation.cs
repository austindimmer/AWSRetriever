using Amazon;
using Amazon.GameLift;
using Amazon.GameLift.Model;
using Amazon.Runtime;

namespace CloudOps.GameLift
{
    public class DescribeMatchmakingRuleSetsOperation : Operation
    {
        public override string Name => "DescribeMatchmakingRuleSets";

        public override string Description => "Retrieves the details for FlexMatch matchmaking rule sets. You can request all existing rule sets for the Region, or provide a list of one or more rule set names. When requesting multiple items, use the pagination parameters to retrieve results as a set of sequential pages. If successful, a rule set is returned for each requested name.   Learn more     Build a rule set     Related actions   CreateMatchmakingConfiguration | DescribeMatchmakingConfigurations | UpdateMatchmakingConfiguration | DeleteMatchmakingConfiguration | CreateMatchmakingRuleSet | DescribeMatchmakingRuleSets | ValidateMatchmakingRuleSet | DeleteMatchmakingRuleSet | All APIs by task ";
 
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
            
            DescribeMatchmakingRuleSetsResponse resp = new DescribeMatchmakingRuleSetsResponse();
            do
            {
                DescribeMatchmakingRuleSetsRequest req = new DescribeMatchmakingRuleSetsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    Limit = maxItems
                                        
                };

                resp = await client.DescribeMatchmakingRuleSetsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.RuleSets)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}