using Amazon;
using Amazon.Redshift;
using Amazon.Redshift.Model;
using Amazon.Runtime;

namespace CloudOps.Redshift
{
    public class DescribeTableRestoreStatusOperation : Operation
    {
        public override string Name => "DescribeTableRestoreStatus";

        public override string Description => "Lists the status of one or more table restore requests made using the RestoreTableFromClusterSnapshot API action. If you don&#39;t specify a value for the TableRestoreRequestId parameter, then DescribeTableRestoreStatus returns the status of all table restore requests ordered by the date and time of the request in ascending order. Otherwise DescribeTableRestoreStatus returns the status of the table specified by TableRestoreRequestId.";
 
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
            
            DescribeTableRestoreStatusResponse resp = new DescribeTableRestoreStatusResponse();
            do
            {
                DescribeTableRestoreStatusRequest req = new DescribeTableRestoreStatusRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = client.DescribeTableRestoreStatus(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.TableRestoreStatusDetails)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}