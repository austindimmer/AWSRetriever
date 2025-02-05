using Amazon;
using Amazon.DocDB;
using Amazon.DocDB.Model;
using Amazon.Runtime;

namespace CloudOps.DocDB
{
    public class DescribePendingMaintenanceActionsOperation : Operation
    {
        public override string Name => "DescribePendingMaintenanceActions";

        public override string Description => "Returns a list of resources (for example, instances) that have at least one pending maintenance action.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DocDB";

        public override string ServiceID => "DocDB";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDocDBConfig config = new AmazonDocDBConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDocDBClient client = new AmazonDocDBClient(creds, config);
            
            DescribePendingMaintenanceActionsResponse resp = new DescribePendingMaintenanceActionsResponse();
            do
            {
                DescribePendingMaintenanceActionsRequest req = new DescribePendingMaintenanceActionsRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = client.DescribePendingMaintenanceActions(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.PendingMaintenanceActions)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}