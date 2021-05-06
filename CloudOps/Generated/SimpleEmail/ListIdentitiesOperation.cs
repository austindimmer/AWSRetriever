using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleEmail
{
    public class ListIdentitiesOperation : Operation
    {
        public override string Name => "ListIdentities";

        public override string Description => "Returns a list containing all of the identities (email addresses and domains) for your AWS account in the current AWS Region, regardless of verification status. You can execute this operation no more than once per second.";
 
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
            
            ListIdentitiesResponse resp = new ListIdentitiesResponse();
            do
            {
                try
                {
                    ListIdentitiesRequest req = new ListIdentitiesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxItems = maxItems
                                            
                    };

                    resp = await client.ListIdentitiesAsync(req);
                    
                    foreach (var obj in resp.Identities)
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