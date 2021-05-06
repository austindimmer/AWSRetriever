using Amazon;
using Amazon.RDS;
using Amazon.RDS.Model;
using Amazon.Runtime;

namespace CloudOps.RDS
{
    public class DescribePendingMaintenanceActionsOperation : Operation
    {
        public override string Name => "DescribePendingMaintenanceActions";

        public override string Description => "Returns a list of resources (for example, DB instances) that have at least one pending maintenance action.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "RDS";

        public override string ServiceID => "RDS";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRDSConfig config = new AmazonRDSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRDSClient client = new AmazonRDSClient(creds, config);
            
            DescribePendingMaintenanceActionsResponse resp = new DescribePendingMaintenanceActionsResponse();
            do
            {
                try
                {
                    DescribePendingMaintenanceActionsRequest req = new DescribePendingMaintenanceActionsRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribePendingMaintenanceActionsAsync(req);
                    
                    foreach (var obj in resp.PendingMaintenanceActions)
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