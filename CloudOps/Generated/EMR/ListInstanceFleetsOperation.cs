using Amazon;
using Amazon.EMR;
using Amazon.EMR.Model;
using Amazon.Runtime;

namespace CloudOps.EMR
{
    public class ListInstanceFleetsOperation : Operation
    {
        public override string Name => "ListInstanceFleets";

        public override string Description => "Lists all available details about the instance fleets in a cluster.  The instance fleet configuration is available only in Amazon EMR versions 4.8.0 and later, excluding 5.0.x versions. ";
 
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
            
            ListInstanceFleetsResponse resp = new ListInstanceFleetsResponse();
            do
            {
                try
                {
                    ListInstanceFleetsRequest req = new ListInstanceFleetsRequest
                    {
                        Marker = resp.Marker
                                            
                    };

                    resp = await client.ListInstanceFleetsAsync(req);
                    
                    foreach (var obj in resp.InstanceFleets)
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