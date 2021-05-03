using Amazon;
using Amazon.Macie;
using Amazon.Macie.Model;
using Amazon.Runtime;

namespace CloudOps.Macie
{
    public class ListCustomDataIdentifiersOperation : Operation
    {
        public override string Name => "ListCustomDataIdentifiers";

        public override string Description => "Retrieves a subset of information about all the custom data identifiers for an account.";
 
        public override string RequestURI => "/custom-data-identifiers/list";

        public override string Method => "POST";

        public override string ServiceName => "Macie";

        public override string ServiceID => "Macie";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacieConfig config = new AmazonMacieConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacieClient client = new AmazonMacieClient(creds, config);
            
            ListCustomDataIdentifiersResponse resp = new ListCustomDataIdentifiersResponse();
            do
            {
                ListCustomDataIdentifiersRequest req = new ListCustomDataIdentifiersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListCustomDataIdentifiers(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Items)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}