using Amazon;
using Amazon.SESV2;
using Amazon.SESV2.Model;
using Amazon.Runtime;

namespace CloudOps.SESV2
{
    public class ListConfigurationSetsOperation : Operation
    {
        public override string Name => "ListConfigurationSets";

        public override string Description => "List all of the configuration sets associated with your account in the current region.  Configuration sets are groups of rules that you can apply to the emails you send. You apply a configuration set to an email by including a reference to the configuration set in the headers of the email. When you apply a configuration set to an email, all of the rules in that configuration set are applied to the email.";
 
        public override string RequestURI => "/v2/email/configuration-sets";

        public override string Method => "GET";

        public override string ServiceName => "SESV2";

        public override string ServiceID => "SESv2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSESV2Config config = new AmazonSESV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSESV2Client client = new AmazonSESV2Client(creds, config);
            
            ListConfigurationSetsResponse resp = new ListConfigurationSetsResponse();
            do
            {
                try
                {
                    ListConfigurationSetsRequest req = new ListConfigurationSetsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        PageSize = maxItems
                                            
                    };

                    resp = await client.ListConfigurationSetsAsync(req);
                    
                    foreach (var obj in resp.ConfigurationSets)
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