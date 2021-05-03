using Amazon;
using Amazon.Transfer;
using Amazon.Transfer.Model;
using Amazon.Runtime;

namespace CloudOps.Transfer
{
    public class ListServersOperation : Operation
    {
        public override string Name => "ListServers";

        public override string Description => "Lists the file transfer protocol-enabled servers that are associated with your AWS account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Transfer";

        public override string ServiceID => "Transfer";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonTransferConfig config = new AmazonTransferConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonTransferClient client = new AmazonTransferClient(creds, config);
            
            ListServersResponse resp = new ListServersResponse();
            do
            {
                ListServersRequest req = new ListServersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListServers(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Servers)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}