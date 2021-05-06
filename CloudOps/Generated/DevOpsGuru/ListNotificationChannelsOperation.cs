using Amazon;
using Amazon.DevOpsGuru;
using Amazon.DevOpsGuru.Model;
using Amazon.Runtime;

namespace CloudOps.DevOpsGuru
{
    public class ListNotificationChannelsOperation : Operation
    {
        public override string Name => "ListNotificationChannels";

        public override string Description => " Returns a list of notification channels configured for DevOps Guru. Each notification channel is used to notify you when DevOps Guru generates an insight that contains information about how to improve your operations. The one supported notification channel is Amazon Simple Notification Service (Amazon SNS). ";
 
        public override string RequestURI => "/channels";

        public override string Method => "POST";

        public override string ServiceName => "DevOpsGuru";

        public override string ServiceID => "DevOps Guru";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDevOpsGuruConfig config = new AmazonDevOpsGuruConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDevOpsGuruClient client = new AmazonDevOpsGuruClient(creds, config);
            
            ListNotificationChannelsResponse resp = new ListNotificationChannelsResponse();
            do
            {
                ListNotificationChannelsRequest req = new ListNotificationChannelsRequest
                {
                    NextToken = resp.NextToken
                                        
                };

                resp = await client.ListNotificationChannelsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Channels)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}