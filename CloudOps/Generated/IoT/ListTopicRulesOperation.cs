using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListTopicRulesOperation : Operation
    {
        public override string Name => "ListTopicRules";

        public override string Description => "Lists the rules for the specific topic.";
 
        public override string RequestURI => "/rules";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListTopicRulesResponse resp = new ListTopicRulesResponse();
            do
            {
                ListTopicRulesRequest req = new ListTopicRulesRequest
                {
                    Marker = resp.NextMarker
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListTopicRules(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Rules)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextMarker));
        }
    }
}