using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListAuditSuppressionsOperation : Operation
    {
        public override string Name => "ListAuditSuppressions";

        public override string Description => " Lists your Device Defender audit listings. ";
 
        public override string RequestURI => "/audit/suppressions/list";

        public override string Method => "POST";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListAuditSuppressionsResponse resp = new ListAuditSuppressionsResponse();
            do
            {
                try
                {
                    ListAuditSuppressionsRequest req = new ListAuditSuppressionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListAuditSuppressionsAsync(req);
                    
                    foreach (var obj in resp.Suppressions)
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