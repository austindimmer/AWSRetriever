using Amazon;
using Amazon.Macie2;
using Amazon.Macie2.Model;
using Amazon.Runtime;

namespace CloudOps.Macie2
{
    public class ListCustomDataIdentifiersOperation : Operation
    {
        public override string Name => "ListCustomDataIdentifiers";

        public override string Description => "Retrieves a subset of information about all the custom data identifiers for an account.";
 
        public override string RequestURI => "/custom-data-identifiers/list";

        public override string Method => "POST";

        public override string ServiceName => "Macie2";

        public override string ServiceID => "Macie2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacie2Config config = new AmazonMacie2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacie2Client client = new AmazonMacie2Client(creds, config);
            
            ListCustomDataIdentifiersResponse resp = new ListCustomDataIdentifiersResponse();
            do
            {
                try
                {
                    ListCustomDataIdentifiersRequest req = new ListCustomDataIdentifiersRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListCustomDataIdentifiersAsync(req);
                    
                    foreach (var obj in resp.Items)
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