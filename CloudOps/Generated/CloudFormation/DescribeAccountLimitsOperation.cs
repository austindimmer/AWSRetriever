using Amazon;
using Amazon.CloudFormation;
using Amazon.CloudFormation.Model;
using Amazon.Runtime;

namespace CloudOps.CloudFormation
{
    public class DescribeAccountLimitsOperation : Operation
    {
        public override string Name => "DescribeAccountLimits";

        public override string Description => "Retrieves your account&#39;s AWS CloudFormation limits, such as the maximum number of stacks that you can create in your account. For more information about account limits, see AWS CloudFormation Limits in the AWS CloudFormation User Guide.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CloudFormation";

        public override string ServiceID => "CloudFormation";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudFormationConfig config = new AmazonCloudFormationConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudFormationClient client = new AmazonCloudFormationClient(creds, config);
            
            DescribeAccountLimitsResponse resp = new DescribeAccountLimitsResponse();
            do
            {
                DescribeAccountLimitsRequest req = new DescribeAccountLimitsRequest
                {
                    NextToken = resp.NextToken
                                        
                };

                resp = await client.DescribeAccountLimitsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.AccountLimits)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}