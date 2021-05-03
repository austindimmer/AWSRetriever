using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeManagedPrefixListsOperation : Operation
    {
        public override string Name => "DescribeManagedPrefixLists";

        public override string Description => "Describes your managed prefix lists and any AWS-managed prefix lists. To view the entries for your prefix list, use GetManagedPrefixListEntries.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EC2";

        public override string ServiceID => "EC2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEC2Config config = new AmazonEC2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEC2Client client = new AmazonEC2Client(creds, config);
            
            DescribeManagedPrefixListsResponse resp = new DescribeManagedPrefixListsResponse();
            do
            {
                DescribeManagedPrefixListsRequest req = new DescribeManagedPrefixListsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeManagedPrefixLists(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.PrefixLists)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}