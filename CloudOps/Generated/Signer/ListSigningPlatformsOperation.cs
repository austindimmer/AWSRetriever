using Amazon;
using Amazon.Signer;
using Amazon.Signer.Model;
using Amazon.Runtime;

namespace CloudOps.Signer
{
    public class ListSigningPlatformsOperation : Operation
    {
        public override string Name => "ListSigningPlatforms";

        public override string Description => "Lists all signing platforms available in code signing that match the request parameters. If additional jobs remain to be listed, code signing returns a nextToken value. Use this value in subsequent calls to ListSigningJobs to fetch the remaining values. You can continue calling ListSigningJobs with your maxResults parameter and with new values that code signing returns in the nextToken parameter until all of your signing jobs have been returned.";
 
        public override string RequestURI => "/signing-platforms";

        public override string Method => "GET";

        public override string ServiceName => "Signer";

        public override string ServiceID => "signer";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSignerConfig config = new AmazonSignerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSignerClient client = new AmazonSignerClient(creds, config);
            
            ListSigningPlatformsResponse resp = new ListSigningPlatformsResponse();
            do
            {
                ListSigningPlatformsRequest req = new ListSigningPlatformsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListSigningPlatforms(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Platforms)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}