using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListPoliciesOperation : Operation
    {
        public override string Name => "ListPolicies";

        public override string Description => "Lists your policies.";
 
        public override string RequestURI => "/policies";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListPoliciesResponse resp = new ListPoliciesResponse();
            do
            {
                ListPoliciesRequest req = new ListPoliciesRequest
                {
                    Marker = resp.NextMarker
                    ,
                    PageSize = maxItems
                                        
                };

                resp = client.ListPolicies(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Policies)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextMarker));
        }
    }
}