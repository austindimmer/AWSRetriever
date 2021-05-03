using Amazon;
using Amazon.EMR;
using Amazon.EMR.Model;
using Amazon.Runtime;

namespace CloudOps.EMR
{
    public class ListStudioSessionMappingsOperation : Operation
    {
        public override string Name => "ListStudioSessionMappings";

        public override string Description => "Returns a list of all user or group session mappings for the Amazon EMR Studio specified by StudioId.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EMR";

        public override string ServiceID => "EMR";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEMRConfig config = new AmazonEMRConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEMRClient client = new AmazonEMRClient(creds, config);
            
            ListStudioSessionMappingsResponse resp = new ListStudioSessionMappingsResponse();
            do
            {
                ListStudioSessionMappingsRequest req = new ListStudioSessionMappingsRequest
                {
                    Marker = resp.Marker
                                        
                };

                resp = client.ListStudioSessionMappings(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SessionMappings)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}