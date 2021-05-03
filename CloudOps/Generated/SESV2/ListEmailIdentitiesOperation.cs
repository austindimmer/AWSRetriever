using Amazon;
using Amazon.SESV2;
using Amazon.SESV2.Model;
using Amazon.Runtime;

namespace CloudOps.SESV2
{
    public class ListEmailIdentitiesOperation : Operation
    {
        public override string Name => "ListEmailIdentities";

        public override string Description => "Returns a list of all of the email identities that are associated with your AWS account. An identity can be either an email address or a domain. This operation returns identities that are verified as well as those that aren&#39;t. This operation returns identities that are associated with Amazon SES and Amazon Pinpoint.";
 
        public override string RequestURI => "/v2/email/identities";

        public override string Method => "GET";

        public override string ServiceName => "SESV2";

        public override string ServiceID => "SESv2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSESV2Config config = new AmazonSESV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSESV2Client client = new AmazonSESV2Client(creds, config);
            
            ListEmailIdentitiesResponse resp = new ListEmailIdentitiesResponse();
            do
            {
                ListEmailIdentitiesRequest req = new ListEmailIdentitiesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    PageSize = maxItems
                                        
                };

                resp = client.ListEmailIdentities(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.EmailIdentities)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}