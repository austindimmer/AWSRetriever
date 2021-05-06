using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListScheduledAuditsOperation : Operation
    {
        public override string Name => "ListScheduledAudits";

        public override string Description => "Lists all of your scheduled audits.";
 
        public override string RequestURI => "/audit/scheduledaudits";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListScheduledAuditsResponse resp = new ListScheduledAuditsResponse();
            do
            {
                try
                {
                    ListScheduledAuditsRequest req = new ListScheduledAuditsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListScheduledAuditsAsync(req);
                    
                    foreach (var obj in resp.ScheduledAudits)
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