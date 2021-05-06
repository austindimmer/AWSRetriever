using Amazon;
using Amazon.Translate;
using Amazon.Translate.Model;
using Amazon.Runtime;

namespace CloudOps.Translate
{
    public class ListTerminologiesOperation : Operation
    {
        public override string Name => "ListTerminologies";

        public override string Description => "Provides a list of custom terminologies associated with your account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Translate";

        public override string ServiceID => "Translate";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonTranslateConfig config = new AmazonTranslateConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonTranslateClient client = new AmazonTranslateClient(creds, config);
            
            ListTerminologiesResponse resp = new ListTerminologiesResponse();
            do
            {
                try
                {
                    ListTerminologiesRequest req = new ListTerminologiesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListTerminologiesAsync(req);
                    
                    foreach (var obj in resp.TerminologyPropertiesList)
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