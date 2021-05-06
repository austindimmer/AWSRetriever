using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class GetBehaviorModelTrainingSummariesOperation : Operation
    {
        public override string Name => "GetBehaviorModelTrainingSummaries";

        public override string Description => " Returns a Device Defender&#39;s ML Detect Security Profile training model&#39;s status. ";
 
        public override string RequestURI => "/behavior-model-training/summaries";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            GetBehaviorModelTrainingSummariesResponse resp = new GetBehaviorModelTrainingSummariesResponse();
            do
            {
                try
                {
                    GetBehaviorModelTrainingSummariesRequest req = new GetBehaviorModelTrainingSummariesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.GetBehaviorModelTrainingSummariesAsync(req);
                    
                    foreach (var obj in resp.Summaries)
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