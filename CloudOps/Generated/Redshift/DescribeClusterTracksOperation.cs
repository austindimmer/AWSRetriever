using Amazon;
using Amazon.Redshift;
using Amazon.Redshift.Model;
using Amazon.Runtime;

namespace CloudOps.Redshift
{
    public class DescribeClusterTracksOperation : Operation
    {
        public override string Name => "DescribeClusterTracks";

        public override string Description => "Returns a list of all the available maintenance tracks.";
 
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
            
            DescribeClusterTracksResponse resp = new DescribeClusterTracksResponse();
            do
            {
                DescribeClusterTracksRequest req = new DescribeClusterTracksRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = await client.DescribeClusterTracksAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.MaintenanceTracks)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}