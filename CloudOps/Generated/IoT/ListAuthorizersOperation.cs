using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListAuthorizersOperation : Operation
    {
        public override string Name => "ListAuthorizers";

        public override string Description => "Lists the authorizers registered in your account.";
 
        public override string RequestURI => "/authorizers/";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListAuthorizersResponse resp = new ListAuthorizersResponse();
            do
            {
                ListAuthorizersRequest req = new ListAuthorizersRequest
                {
                    Marker = resp.NextMarker
                    ,
                    PageSize = maxItems
                                        
                };

                resp = client.ListAuthorizers(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Authorizers)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextMarker));
        }
    }
}