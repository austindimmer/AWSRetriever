using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribePrefixListsOperation : Operation
    {
        public override string Name => "DescribePrefixLists";

        public override string Description => "Describes available AWS services in a prefix list format, which includes the prefix list name and prefix list ID of the service and the IP address range for the service. We recommend that you use DescribeManagedPrefixLists instead.";
 
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
            
            DescribePrefixListsResponse resp = new DescribePrefixListsResponse();
            do
            {
                try
                {
                    DescribePrefixListsRequest req = new DescribePrefixListsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribePrefixListsAsync(req);
                    
                    foreach (var obj in resp.PrefixLists)
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