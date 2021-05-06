using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListSecurityProfilesOperation : Operation
    {
        public override string Name => "ListSecurityProfiles";

        public override string Description => "Lists the Device Defender security profiles you&#39;ve created. You can filter security profiles by dimension or custom metric.   dimensionName and metricName cannot be used in the same request. ";
 
        public override string RequestURI => "/security-profiles";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListSecurityProfilesResponse resp = new ListSecurityProfilesResponse();
            do
            {
                ListSecurityProfilesRequest req = new ListSecurityProfilesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListSecurityProfilesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SecurityProfileIdentifiers)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}