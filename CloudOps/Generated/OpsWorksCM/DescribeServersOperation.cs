using Amazon;
using Amazon.OpsWorksCM;
using Amazon.OpsWorksCM.Model;
using Amazon.Runtime;

namespace CloudOps.OpsWorksCM
{
    public class DescribeServersOperation : Operation
    {
        public override string Name => "DescribeServers";

        public override string Description => " Lists all configuration management servers that are identified with your account. Only the stored results from Amazon DynamoDB are returned. AWS OpsWorks CM does not query other services.   This operation is synchronous.   A ResourceNotFoundException is thrown when the server does not exist. A ValidationException is raised when parameters of the request are not valid. ";
 
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
            
            DescribeServersResponse resp = new DescribeServersResponse();
            do
            {
                try
                {
                    DescribeServersRequest req = new DescribeServersRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeServersAsync(req);
                    
                    foreach (var obj in resp.Servers)
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