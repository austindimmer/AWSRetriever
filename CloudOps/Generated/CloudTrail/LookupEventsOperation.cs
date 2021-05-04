using Amazon;
using Amazon.CloudTrail;
using Amazon.CloudTrail.Model;
using Amazon.Runtime;

namespace CloudOps.CloudTrail
{
    public class LookupEventsOperation : Operation
    {
        public override string Name => "LookupEvents";

        public override string Description => "Looks up management events or CloudTrail Insights events that are captured by CloudTrail. You can look up events that occurred in a region within the last 90 days. Lookup supports the following attributes for management events:   AWS access key   Event ID   Event name   Event source   Read only   Resource name   Resource type   User name   Lookup supports the following attributes for Insights events:   Event ID   Event name   Event source   All attributes are optional. The default number of results returned is 50, with a maximum of 50 possible. The response includes a token that you can use to get the next page of results.  The rate of lookup requests is limited to two per second, per account, per region. If this limit is exceeded, a throttling error occurs. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CloudTrail";

        public override string ServiceID => "CloudTrail";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudTrailConfig config = new AmazonCloudTrailConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudTrailClient client = new AmazonCloudTrailClient(creds, config);
            
            LookupEventsResponse resp = new LookupEventsResponse();
            do
            {
                LookupEventsRequest req = new LookupEventsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.LookupEvents(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Events)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}