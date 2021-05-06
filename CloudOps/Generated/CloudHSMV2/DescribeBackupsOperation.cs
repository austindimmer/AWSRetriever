using Amazon;
using Amazon.CloudHSMV2;
using Amazon.CloudHSMV2.Model;
using Amazon.Runtime;

namespace CloudOps.CloudHSMV2
{
    public class DescribeBackupsOperation : Operation
    {
        public override string Name => "DescribeBackups";

        public override string Description => "Gets information about backups of AWS CloudHSM clusters. This is a paginated operation, which means that each response might contain only a subset of all the backups. When the response contains only a subset of backups, it includes a NextToken value. Use this value in a subsequent DescribeBackups request to get more backups. When you receive a response with no NextToken (or an empty or null value), that means there are no more backups to get.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CloudHSMV2";

        public override string ServiceID => "CloudHSM V2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudHSMV2Config config = new AmazonCloudHSMV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudHSMV2Client client = new AmazonCloudHSMV2Client(creds, config);
            
            DescribeBackupsResponse resp = new DescribeBackupsResponse();
            do
            {
                try
                {
                    DescribeBackupsRequest req = new DescribeBackupsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeBackupsAsync(req);
                    
                    foreach (var obj in resp.Backups)
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
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}