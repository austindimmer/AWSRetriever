using Amazon;
using Amazon.SimpleEmailV2;
using Amazon.SimpleEmailV2.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleEmailV2
{
    public class ListConfigurationSetsOperation : Operation
    {
        public override string Name => "ListConfigurationSets";

        public override string Description => "List all of the configuration sets associated with your account in the current region.  Configuration sets are groups of rules that you can apply to the emails you send. You apply a configuration set to an email by including a reference to the configuration set in the headers of the email. When you apply a configuration set to an email, all of the rules in that configuration set are applied to the email.";
 
        public override string RequestURI => "/v2/email/configuration-sets";

        public override string Method => "GET";

        public override string ServiceName => "SimpleEmailV2";

        public override string ServiceID => "SimpleEmailV2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSimpleEmailServiceV2Config config = new AmazonSimpleEmailServiceV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSimpleEmailServiceV2Client client = new AmazonSimpleEmailServiceV2Client(creds, config);
            
            ListConfigurationSetsResponse resp = new ListConfigurationSetsResponse();
            do
            {
                ListConfigurationSetsRequest req = new ListConfigurationSetsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    PageSize = maxItems
                                        
                };

                resp = client.ListConfigurationSets(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ConfigurationSets)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}