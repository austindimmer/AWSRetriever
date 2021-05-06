using Amazon;
using Amazon.RDS;
using Amazon.RDS.Model;
using Amazon.Runtime;

namespace CloudOps.RDS
{
    public class DescribeCustomAvailabilityZonesOperation : Operation
    {
        public override string Name => "DescribeCustomAvailabilityZones";

        public override string Description => "Returns information about custom Availability Zones (AZs). A custom AZ is an on-premises AZ that is integrated with a VMware vSphere cluster. For more information about RDS on VMware, see the  RDS on VMware User Guide. ";
 
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
            
            DescribeCustomAvailabilityZonesResponse resp = new DescribeCustomAvailabilityZonesResponse();
            do
            {
                DescribeCustomAvailabilityZonesRequest req = new DescribeCustomAvailabilityZonesRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = await client.DescribeCustomAvailabilityZonesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.CustomAvailabilityZones)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}