using Amazon;
using Amazon.SimpleEmailV2;
using Amazon.SimpleEmailV2.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleEmailV2
{
    public class ListCustomVerificationEmailTemplatesOperation : Operation
    {
        public override string Name => "ListCustomVerificationEmailTemplates";

        public override string Description => "Lists the existing custom verification email templates for your account in the current AWS Region. For more information about custom verification email templates, see Using Custom Verification Email Templates in the Amazon SES Developer Guide. You can execute this operation no more than once per second.";
 
        public override string RequestURI => "/v2/email/custom-verification-email-templates";

        public override string Method => "GET";

        public override string ServiceName => "SimpleEmailV2";

        public override string ServiceID => "SimpleEmailV2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSimpleEmailServiceV2Config config = new AmazonSimpleEmailServiceV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSimpleEmailServiceV2Client client = new AmazonSimpleEmailServiceV2Client(creds, config);
            
            ListCustomVerificationEmailTemplatesResponse resp = new ListCustomVerificationEmailTemplatesResponse();
            do
            {
                ListCustomVerificationEmailTemplatesRequest req = new ListCustomVerificationEmailTemplatesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    PageSize = maxItems
                                        
                };

                resp = client.ListCustomVerificationEmailTemplates(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.CustomVerificationEmailTemplates)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}