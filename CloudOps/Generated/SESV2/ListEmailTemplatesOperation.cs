using Amazon;
using Amazon.SESV2;
using Amazon.SESV2.Model;
using Amazon.Runtime;

namespace CloudOps.SESV2
{
    public class ListEmailTemplatesOperation : Operation
    {
        public override string Name => "ListEmailTemplates";

        public override string Description => "Lists the email templates present in your Amazon SES account in the current AWS Region. You can execute this operation no more than once per second.";
 
        public override string RequestURI => "/v2/email/templates";

        public override string Method => "GET";

        public override string ServiceName => "SESV2";

        public override string ServiceID => "SESv2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSESV2Config config = new AmazonSESV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSESV2Client client = new AmazonSESV2Client(creds, config);
            
            ListEmailTemplatesResponse resp = new ListEmailTemplatesResponse();
            do
            {
                try
                {
                    ListEmailTemplatesRequest req = new ListEmailTemplatesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        PageSize = maxItems
                                            
                    };

                    resp = await client.ListEmailTemplatesAsync(req);
                    
                    foreach (var obj in resp.TemplatesMetadata)
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