using Amazon;
using Amazon.Redshift;
using Amazon.Redshift.Model;
using Amazon.Runtime;

namespace CloudOps.Redshift
{
    public class DescribeUsageLimitsOperation : Operation
    {
        public override string Name => "DescribeUsageLimits";

        public override string Description => "Shows usage limits on a cluster. Results are filtered based on the combination of input usage limit identifier, cluster identifier, and feature type parameters:   If usage limit identifier, cluster identifier, and feature type are not provided, then all usage limit objects for the current account in the current region are returned.   If usage limit identifier is provided, then the corresponding usage limit object is returned.   If cluster identifier is provided, then all usage limit objects for the specified cluster are returned.   If cluster identifier and feature type are provided, then all usage limit objects for the combination of cluster and feature are returned.  ";
 
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
            
            DescribeUsageLimitsResponse resp = new DescribeUsageLimitsResponse();
            do
            {
                DescribeUsageLimitsRequest req = new DescribeUsageLimitsRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = client.DescribeUsageLimits(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.UsageLimits)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}