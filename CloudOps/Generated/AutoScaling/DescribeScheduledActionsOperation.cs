using Amazon;
using Amazon.AutoScaling;
using Amazon.AutoScaling.Model;
using Amazon.Runtime;

namespace CloudOps.AutoScaling
{
    public class DescribeScheduledActionsOperation : Operation
    {
        public override string Name => "DescribeScheduledActions";

        public override string Description => "Describes the actions scheduled for your Auto Scaling group that haven&#39;t run or that have not reached their end time. To describe the actions that have already run, call the DescribeScalingActivities API.";
 
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
            
            DescribeScheduledActionsResponse resp = new DescribeScheduledActionsResponse();
            do
            {
                try
                {
                    DescribeScheduledActionsRequest req = new DescribeScheduledActionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeScheduledActionsAsync(req);
                    
                    foreach (var obj in resp.ScheduledUpdateGroupActions)
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