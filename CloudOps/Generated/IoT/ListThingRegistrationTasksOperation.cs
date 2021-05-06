using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListThingRegistrationTasksOperation : Operation
    {
        public override string Name => "ListThingRegistrationTasks";

        public override string Description => "List bulk thing provisioning tasks.";
 
        public override string RequestURI => "/thing-registration-tasks";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListThingRegistrationTasksResponse resp = new ListThingRegistrationTasksResponse();
            do
            {
                try
                {
                    ListThingRegistrationTasksRequest req = new ListThingRegistrationTasksRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListThingRegistrationTasksAsync(req);
                    
                    foreach (var obj in resp.TaskIds)
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