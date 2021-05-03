using Amazon;
using Amazon.EMR;
using Amazon.EMR.Model;
using Amazon.Runtime;

namespace CloudOps.EMR
{
    public class ListBootstrapActionsOperation : Operation
    {
        public override string Name => "ListBootstrapActions";

        public override string Description => "Provides information about the bootstrap actions associated with a cluster.";
 
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
            
            ListBootstrapActionsResponse resp = new ListBootstrapActionsResponse();
            do
            {
                ListBootstrapActionsRequest req = new ListBootstrapActionsRequest
                {
                    Marker = resp.Marker
                                        
                };

                resp = client.ListBootstrapActions(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.BootstrapActions)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}