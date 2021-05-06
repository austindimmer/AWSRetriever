using Amazon;
using Amazon.RDS;
using Amazon.RDS.Model;
using Amazon.Runtime;

namespace CloudOps.RDS
{
    public class DescribeInstallationMediaOperation : Operation
    {
        public override string Name => "DescribeInstallationMedia";

        public override string Description => "Describes the available installation media for a DB engine that requires an on-premises customer provided license, such as Microsoft SQL Server.";
 
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
            
            DescribeInstallationMediaResponse resp = new DescribeInstallationMediaResponse();
            do
            {
                try
                {
                    DescribeInstallationMediaRequest req = new DescribeInstallationMediaRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeInstallationMediaAsync(req);
                    
                    foreach (var obj in resp.InstallationMedia)
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