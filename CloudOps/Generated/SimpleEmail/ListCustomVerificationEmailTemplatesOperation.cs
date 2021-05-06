using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleEmail
{
    public class ListCustomVerificationEmailTemplatesOperation : Operation
    {
        public override string Name => "ListCustomVerificationEmailTemplates";

        public override string Description => "Lists the existing custom verification email templates for your account in the current AWS Region. For more information about custom verification email templates, see Using Custom Verification Email Templates in the Amazon SES Developer Guide. You can execute this operation no more than once per second.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SimpleEmail";

        public override string ServiceID => "SES";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSimpleEmailConfig config = new AmazonSimpleEmailConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSimpleEmailClient client = new AmazonSimpleEmailClient(creds, config);
            
            ListCustomVerificationEmailTemplatesResponse resp = new ListCustomVerificationEmailTemplatesResponse();
            do
            {
                try
                {
                    ListCustomVerificationEmailTemplatesRequest req = new ListCustomVerificationEmailTemplatesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
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