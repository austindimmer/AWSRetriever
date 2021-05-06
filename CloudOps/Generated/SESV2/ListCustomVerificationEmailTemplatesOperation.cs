using Amazon;
using Amazon.SESV2;
using Amazon.SESV2.Model;
using Amazon.Runtime;

namespace CloudOps.SESV2
{
    public class ListCustomVerificationEmailTemplatesOperation : Operation
    {
        public override string Name => "ListCustomVerificationEmailTemplates";

        public override string Description => "Lists the existing custom verification email templates for your account in the current AWS Region. For more information about custom verification email templates, see Using Custom Verification Email Templates in the Amazon SES Developer Guide. You can execute this operation no more than once per second.";
 
        public override string RequestURI => "/v2/email/custom-verification-email-templates";

        public override string Method => "GET";

        public override string ServiceName => "SESV2";

        public override string ServiceID => "SESv2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSESV2Config config = new AmazonSESV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSESV2Client client = new AmazonSESV2Client(creds, config);
            
            ListCustomVerificationEmailTemplatesResponse resp = new ListCustomVerificationEmailTemplatesResponse();
            do
            {
                try
                {
                    ListCustomVerificationEmailTemplatesRequest req = new ListCustomVerificationEmailTemplatesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        PageSize = maxItems
                                            
                    };

                    resp = await client.ListCustomVerificationEmailTemplatesAsync(req);
                    
                    foreach (var obj in resp.CustomVerificationEmailTemplates)
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