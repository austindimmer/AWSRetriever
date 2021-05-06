using Amazon;
using Amazon.AutoScaling;
using Amazon.AutoScaling.Model;
using Amazon.Runtime;

namespace CloudOps.AutoScaling
{
    public class DescribeScalingActivitiesOperation : Operation
    {
        public override string Name => "DescribeScalingActivities";

        public override string Description => "Describes one or more scaling activities for the specified Auto Scaling group. To view the scaling activities from the Amazon EC2 Auto Scaling console, choose the Activity tab of the Auto Scaling group. When scaling events occur, you see scaling activity messages in the Activity history. For more information, see Verifying a scaling activity for an Auto Scaling group in the Amazon EC2 Auto Scaling User Guide.";
 
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
            
            DescribeScalingActivitiesResponse resp = new DescribeScalingActivitiesResponse();
            do
            {
                try
                {
                    DescribeScalingActivitiesRequest req = new DescribeScalingActivitiesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeScalingActivitiesAsync(req);
                    
                    foreach (var obj in resp.Activities)
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