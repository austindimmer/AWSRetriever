using Amazon;
using Amazon.EMR;
using Amazon.EMR.Model;
using Amazon.Runtime;

namespace CloudOps.EMR
{
    public class ListInstanceGroupsOperation : Operation
    {
        public override string Name => "ListInstanceGroups";

        public override string Description => "Provides all available details about the instance groups in a cluster.";
 
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
            
            ListInstanceGroupsResponse resp = new ListInstanceGroupsResponse();
            do
            {
                try
                {
                    ListInstanceGroupsRequest req = new ListInstanceGroupsRequest
                    {
                        Marker = resp.Marker
                                            
                    };

                    resp = await client.ListInstanceGroupsAsync(req);
                    
                    foreach (var obj in resp.InstanceGroups)
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