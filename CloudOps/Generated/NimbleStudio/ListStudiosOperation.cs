using Amazon;
using Amazon.NimbleStudio;
using Amazon.NimbleStudio.Model;
using Amazon.Runtime;

namespace CloudOps.NimbleStudio
{
    public class ListStudiosOperation : Operation
    {
        public override string Name => "ListStudios";

        public override string Description => "List studios in your AWS account in the requested AWS Region.";
 
        public override string RequestURI => "/2020-08-01/studios";

        public override string Method => "GET";

        public override string ServiceName => "NimbleStudio";

        public override string ServiceID => "nimble";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonNimbleStudioConfig config = new AmazonNimbleStudioConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonNimbleStudioClient client = new AmazonNimbleStudioClient(creds, config);
            
            ListStudiosResponse resp = new ListStudiosResponse();
            do
            {
                ListStudiosRequest req = new ListStudiosRequest
                {
                    NextToken = resp.NextToken
                                        
                };

                resp = client.ListStudios(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Studios)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}