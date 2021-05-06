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

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRedshiftConfig config = new AmazonRedshiftConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRedshiftClient client = new AmazonRedshiftClient(creds, config);
            
            DescribeSnapshotSchedulesResponse resp = new DescribeSnapshotSchedulesResponse();
            do
            {
                try
                {
                    DescribeSnapshotSchedulesRequest req = new DescribeSnapshotSchedulesRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeSnapshotSchedulesAsync(req);
                    
                    foreach (var obj in resp.SnapshotSchedules)
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