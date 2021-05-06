using Amazon;
using Amazon.NimbleStudio;
using Amazon.NimbleStudio.Model;
using Amazon.Runtime;

namespace CloudOps.NimbleStudio
{
    public class ListEulasOperation : Operation
    {
        public override string Name => "ListEulas";

        public override string Description => "List Eulas.";
 
        public override string RequestURI => "/2020-08-01/eulas";

        public override string Method => "GET";

        public override string ServiceName => "NimbleStudio";

        public override string ServiceID => "nimble";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonNimbleStudioConfig config = new AmazonNimbleStudioConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonNimbleStudioClient client = new AmazonNimbleStudioClient(creds, config);
            
            ListEulasResponse resp = new ListEulasResponse();
            do
            {
                try
                {
                    ListEulasRequest req = new ListEulasRequest
                    {
                        NextToken = resp.NextToken
                                            
                    };

                    resp = await client.ListEulasAsync(req);
                    
                    foreach (var obj in resp.Eulas)
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