using Amazon;
using Amazon.AutoScaling;
using Amazon.AutoScaling.Model;
using Amazon.Runtime;

namespace CloudOps.AutoScaling
{
    public class DescribeTagsOperation : Operation
    {
        public override string Name => "DescribeTags";

        public override string Description => "Describes the specified tags. You can use filters to limit the results. For example, you can query for the tags for a specific Auto Scaling group. You can specify multiple values for a filter. A tag must match at least one of the specified values for it to be included in the results. You can also specify multiple filters. The result includes information for a particular tag only if it matches all the filters. If there&#39;s no match, no special message is returned. For more information, see Tagging Auto Scaling groups and instances in the Amazon EC2 Auto Scaling User Guide.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "AutoScaling";

        public override string ServiceID => "Auto Scaling";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAutoScalingConfig config = new AmazonAutoScalingConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAutoScalingClient client = new AmazonAutoScalingClient(creds, config);
            
            DescribeTagsResponse resp = new DescribeTagsResponse();
            do
            {
                try
                {
                    DescribeTagsRequest req = new DescribeTagsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeTagsAsync(req);
                    
                    foreach (var obj in resp.Tags)
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