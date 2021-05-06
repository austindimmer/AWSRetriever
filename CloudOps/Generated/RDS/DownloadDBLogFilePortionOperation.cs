using Amazon;
using Amazon.RDS;
using Amazon.RDS.Model;
using Amazon.Runtime;

namespace CloudOps.RDS
{
    public class DownloadDBLogFilePortionOperation : Operation
    {
        public override string Name => "DownloadDBLogFilePortion";

        public override string Description => "";
 
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
            
            DownloadDBLogFilePortionResponse resp = new DownloadDBLogFilePortionResponse();
            do
            {
                DownloadDBLogFilePortionRequest req = new DownloadDBLogFilePortionRequest
                {
                    Marker = resp.Marker
                    ,
                    NumberOfLines = maxItems
                                        
                };

                resp = await client.DownloadDBLogFilePortionAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.LogFileData)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}