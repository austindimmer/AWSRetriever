using Amazon;
using Amazon.Redshift;
using Amazon.Redshift.Model;
using Amazon.Runtime;

namespace CloudOps.Redshift
{
    public class DescribeSnapshotSchedulesOperation : Operation
    {
        public override string Name => "DescribeSnapshotSchedules";

        public override string Description => "Returns a list of snapshot schedules. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Redshift";

        public override string ServiceID => "Redshift";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRedshiftConfig config = new AmazonRedshiftConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRedshiftClient client = new AmazonRedshiftClient(creds, config);
            
            DescribeSnapshotSchedulesResponse resp = new DescribeSnapshotSchedulesResponse();
            do
            {
                DescribeSnapshotSchedulesRequest req = new DescribeSnapshotSchedulesRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = client.DescribeSnapshotSchedules(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SnapshotSchedules)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}