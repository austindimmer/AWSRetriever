using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListActiveViolationsOperation : Operation
    {
        public override string Name => "ListActiveViolations";

        public override string Description => "Lists the active violations for a given Device Defender security profile.";
 
        public override string RequestURI => "/active-violations";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListActiveViolationsResponse resp = new ListActiveViolationsResponse();
            do
            {
                ListActiveViolationsRequest req = new ListActiveViolationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListActiveViolations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ActiveViolations)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}