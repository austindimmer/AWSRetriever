using Amazon;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentity.Model;
using Amazon.Runtime;

namespace CloudOps.CognitoIdentity
{
    public class ListIdentityPoolsOperation : Operation
    {
        public override string Name => "ListIdentityPools";

        public override string Description => "Lists all of the Cognito identity pools registered for your account. You must use AWS Developer credentials to call this API.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CognitoIdentity";

        public override string ServiceID => "Cognito Identity";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCognitoIdentityConfig config = new AmazonCognitoIdentityConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCognitoIdentityClient client = new AmazonCognitoIdentityClient(creds, config);
            
            ListIdentityPoolsResponse resp = new ListIdentityPoolsResponse();
            do
            {
                try
                {
                    ListIdentityPoolsRequest req = new ListIdentityPoolsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListIdentityPoolsAsync(req);
                    
                    foreach (var obj in resp.IdentityPools)
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