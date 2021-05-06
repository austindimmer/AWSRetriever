using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListDetectMitigationActionsExecutionsOperation : Operation
    {
        public override string Name => "ListDetectMitigationActionsExecutions";

        public override string Description => " Lists mitigation actions executions for a Device Defender ML Detect Security Profile. ";
 
        public override string RequestURI => "/detect/mitigationactions/executions";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListDetectMitigationActionsExecutionsResponse resp = new ListDetectMitigationActionsExecutionsResponse();
            do
            {
                try
                {
                    ListDetectMitigationActionsExecutionsRequest req = new ListDetectMitigationActionsExecutionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListDetectMitigationActionsExecutionsAsync(req);
                    
                    foreach (var obj in resp.ActionsExecutions)
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