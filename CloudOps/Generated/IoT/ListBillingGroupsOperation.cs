using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListBillingGroupsOperation : Operation
    {
        public override string Name => "ListBillingGroups";

        public override string Description => "Lists the billing groups you have created.";
 
        public override string RequestURI => "/billing-groups";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListBillingGroupsResponse resp = new ListBillingGroupsResponse();
            do
            {
                ListBillingGroupsRequest req = new ListBillingGroupsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListBillingGroups(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.BillingGroups)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}