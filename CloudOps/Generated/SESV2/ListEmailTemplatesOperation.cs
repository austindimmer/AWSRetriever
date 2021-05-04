using Amazon;
using Amazon.SimpleEmailV2;
using Amazon.SimpleEmailV2.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleEmailV2
{
    public class ListEmailTemplatesOperation : Operation
    {
        public override string Name => "ListEmailTemplates";

        public override string Description => "Lists the email templates present in your Amazon SES account in the current AWS Region. You can execute this operation no more than once per second.";
 
        public override string RequestURI => "/v2/email/templates";

        public override string Method => "GET";

        public override string ServiceName => "SimpleEmailV2";

        public override string ServiceID => "SimpleEmailV2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSimpleEmailServiceV2Config config = new AmazonSimpleEmailServiceV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSimpleEmailServiceV2Client client = new AmazonSimpleEmailServiceV2Client(creds, config);
            
            ListEmailTemplatesResponse resp = new ListEmailTemplatesResponse();
            do
            {
                ListEmailTemplatesRequest req = new ListEmailTemplatesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    PageSize = maxItems
                                        
                };

                resp = client.ListEmailTemplates(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.TemplatesMetadata)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}