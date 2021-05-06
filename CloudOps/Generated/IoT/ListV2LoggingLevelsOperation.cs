using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListV2LoggingLevelsOperation : Operation
    {
        public override string Name => "ListV2LoggingLevels";

        public override string Description => "Lists logging levels.";
 
        public override string RequestURI => "/v2LoggingLevel";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListV2LoggingLevelsResponse resp = new ListV2LoggingLevelsResponse();
            do
            {
                try
                {
                    ListV2LoggingLevelsRequest req = new ListV2LoggingLevelsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListV2LoggingLevelsAsync(req);
                    
                    foreach (var obj in resp.LogTargetConfigurations)
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