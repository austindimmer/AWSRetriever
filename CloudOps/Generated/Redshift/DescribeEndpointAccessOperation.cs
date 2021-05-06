using Amazon;
using Amazon.Redshift;
using Amazon.Redshift.Model;
using Amazon.Runtime;

namespace CloudOps.Redshift
{
    public class DescribeEndpointAccessOperation : Operation
    {
        public override string Name => "DescribeEndpointAccess";

        public override string Description => "Describes a Redshift-managed VPC endpoint.";
 
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
            
            DescribeEndpointAccessResponse resp = new DescribeEndpointAccessResponse();
            do
            {
                DescribeEndpointAccessRequest req = new DescribeEndpointAccessRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = await client.DescribeEndpointAccessAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.EndpointAccessList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}