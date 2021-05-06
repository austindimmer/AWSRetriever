using Amazon;
using Amazon.AutoScaling;
using Amazon.AutoScaling.Model;
using Amazon.Runtime;

namespace CloudOps.AutoScaling
{
    public class DescribeNotificationConfigurationsOperation : Operation
    {
        public override string Name => "DescribeNotificationConfigurations";

        public override string Description => "Describes the notification actions associated with the specified Auto Scaling group.";
 
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
            
            DescribeNotificationConfigurationsResponse resp = new DescribeNotificationConfigurationsResponse();
            do
            {
                try
                {
                    DescribeNotificationConfigurationsRequest req = new DescribeNotificationConfigurationsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeNotificationConfigurationsAsync(req);
                    
                    foreach (var obj in resp.NotificationConfigurations)
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