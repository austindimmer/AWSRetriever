using Amazon;
using Amazon.OpsWorksCM;
using Amazon.OpsWorksCM.Model;
using Amazon.Runtime;

namespace CloudOps.OpsWorksCM
{
    public class DescribeBackupsOperation : Operation
    {
        public override string Name => "DescribeBackups";

        public override string Description => " Describes backups. The results are ordered by time, with newest backups first. If you do not specify a BackupId or ServerName, the command returns all backups.   This operation is synchronous.   A ResourceNotFoundException is thrown when the backup does not exist. A ValidationException is raised when parameters of the request are not valid. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "OpsWorksCM";

        public override string ServiceID => "OpsWorksCM";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonOpsWorksCMConfig config = new AmazonOpsWorksCMConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonOpsWorksCMClient client = new AmazonOpsWorksCMClient(creds, config);
            
            DescribeBackupsResponse resp = new DescribeBackupsResponse();
            do
            {
                DescribeBackupsRequest req = new DescribeBackupsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeBackupsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Backups)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}