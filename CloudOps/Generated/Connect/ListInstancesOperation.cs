using Amazon;
using Amazon.Connect;
using Amazon.Connect.Model;
using Amazon.Runtime;

namespace CloudOps.Connect
{
    public class ListInstancesOperation : Operation
    {
        public override string Name => "ListInstances";

        public override string Description => "This API is in preview release for Amazon Connect and is subject to change. Return a list of instances which are in active state, creation-in-progress state, and failed state. Instances that aren&#39;t successfully created (they are in a failed state) are returned only for 24 hours after the CreateInstance API was invoked.";
 
        public override string RequestURI => "/instance";

        public override string Method => "GET";

        public override string ServiceName => "Connect";

        public override string ServiceID => "Connect";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonConnectConfig config = new AmazonConnectConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonConnectClient client = new AmazonConnectClient(creds, config);
            
            ListInstancesResponse resp = new ListInstancesResponse();
            do
            {
                try
                {
                    ListInstancesRequest req = new ListInstancesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListInstancesAsync(req);
                    
                    foreach (var obj in resp.InstanceSummaryList)
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