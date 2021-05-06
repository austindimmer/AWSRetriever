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

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEMRConfig config = new AmazonEMRConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEMRClient client = new AmazonEMRClient(creds, config);
            
            ListStudioSessionMappingsResponse resp = new ListStudioSessionMappingsResponse();
            do
            {
                try
                {
                    ListStudioSessionMappingsRequest req = new ListStudioSessionMappingsRequest
                    {
                        Marker = resp.Marker
                                            
                    };

                    resp = await client.ListStudioSessionMappingsAsync(req);
                    
                    foreach (var obj in resp.SessionMappings)
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
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}