using Amazon;
using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using Amazon.Runtime;

namespace CloudOps.CloudWatch
{
    public class DescribeInsightRulesOperation : Operation
    {
        public override string Name => "DescribeInsightRules";

        public override string Description => "Returns a list of all the Contributor Insights rules in your account. For more information about Contributor Insights, see Using Contributor Insights to Analyze High-Cardinality Data.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CloudWatch";

        public override string ServiceID => "CloudWatch";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudWatchConfig config = new AmazonCloudWatchConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudWatchClient client = new AmazonCloudWatchClient(creds, config);
            
            DescribeInsightRulesResponse resp = new DescribeInsightRulesResponse();
            do
            {
                try
                {
                    DescribeInsightRulesRequest req = new DescribeInsightRulesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeInsightRulesAsync(req);
                    
                    foreach (var obj in resp.InsightRules)
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