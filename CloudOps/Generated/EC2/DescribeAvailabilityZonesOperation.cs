using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeAvailabilityZonesOperation : Operation
    {
        public override string Name => "DescribeAvailabilityZones";

        public override string Description => "Describes the Availability Zones, Local Zones, and Wavelength Zones that are available to you. If there is an event impacting a zone, you can use this request to view the state and any provided messages for that zone. For more information about Availability Zones, Local Zones, and Wavelength Zones, see Regions, Zones and Outposts in the Amazon Elastic Compute Cloud User Guide.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EC2";

        public override string ServiceID => "EC2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEC2Config config = new AmazonEC2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEC2Client client = new AmazonEC2Client(creds, config);
            
            DescribeAvailabilityZonesResponse resp = new DescribeAvailabilityZonesResponse();
            DescribeAvailabilityZonesRequest req = new DescribeAvailabilityZonesRequest
            {                    
                                    
            };
            
            try
            {
                resp = await client.DescribeAvailabilityZonesAsync(req);
                
                foreach (var obj in resp.AvailabilityZones)
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
    }
}