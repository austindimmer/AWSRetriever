using Amazon;
using Amazon.ECS;
using Amazon.ECS.Model;
using Amazon.Runtime;

namespace CloudOps.ECS
{
    public class ListTasksOperation : Operation
    {
        public override string Name => "ListTasks";

        public override string Description => "Returns a list of tasks for a specified cluster. You can filter the results by family name, by a particular container instance, or by the desired status of the task with the family, containerInstance, and desiredStatus parameters. Recently stopped tasks might appear in the returned results. Currently, stopped tasks appear in the returned results for at least one hour. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ECS";

        public override string ServiceID => "ECS";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonECSConfig config = new AmazonECSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonECSClient client = new AmazonECSClient(creds, config);
            
            ListTasksResponse resp = new ListTasksResponse();
            do
            {
                try
                {
                    ListTasksRequest req = new ListTasksRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListTasksAsync(req);
                    
                    foreach (var obj in resp.TaskArns)
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