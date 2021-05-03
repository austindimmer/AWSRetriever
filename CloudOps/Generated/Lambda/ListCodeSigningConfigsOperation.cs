using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.Runtime;

namespace CloudOps.Lambda
{
    public class ListCodeSigningConfigsOperation : Operation
    {
        public override string Name => "ListCodeSigningConfigs";

        public override string Description => "Returns a list of code signing configurations. A request returns up to 10,000 configurations per call. You can use the MaxItems parameter to return fewer configurations per call. ";
 
        public override string RequestURI => "/2020-04-22/code-signing-configs/";

        public override string Method => "GET";

        public override string ServiceName => "Lambda";

        public override string ServiceID => "Lambda";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLambdaConfig config = new AmazonLambdaConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLambdaClient client = new AmazonLambdaClient(creds, config);
            
            ListCodeSigningConfigsResponse resp = new ListCodeSigningConfigsResponse();
            do
            {
                ListCodeSigningConfigsRequest req = new ListCodeSigningConfigsRequest
                {
                    Marker = resp.NextMarker
                    ,
                    MaxItems = maxItems
                                        
                };

                resp = client.ListCodeSigningConfigs(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.CodeSigningConfigs)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextMarker));
        }
    }
}