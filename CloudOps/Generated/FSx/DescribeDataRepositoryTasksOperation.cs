using Amazon;
using Amazon.FSx;
using Amazon.FSx.Model;
using Amazon.Runtime;

namespace CloudOps.FSx
{
    public class DescribeDataRepositoryTasksOperation : Operation
    {
        public override string Name => "DescribeDataRepositoryTasks";

        public override string Description => "Returns the description of specific Amazon FSx for Lustre data repository tasks, if one or more TaskIds values are provided in the request, or if filters are used in the request. You can use filters to narrow the response to include just tasks for specific file systems, or tasks in a specific lifecycle state. Otherwise, it returns all data repository tasks owned by your AWS account in the AWS Region of the endpoint that you&#39;re calling. When retrieving all tasks, you can paginate the response by using the optional MaxResults parameter to limit the number of tasks returned in a response. If more tasks remain, Amazon FSx returns a NextToken value in the response. In this case, send a later request with the NextToken request parameter set to the value of NextToken from the last response.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "FSx";

        public override string ServiceID => "FSx";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonFSxConfig config = new AmazonFSxConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonFSxClient client = new AmazonFSxClient(creds, config);
            
            DescribeDataRepositoryTasksResponse resp = new DescribeDataRepositoryTasksResponse();
            do
            {
                DescribeDataRepositoryTasksRequest req = new DescribeDataRepositoryTasksRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeDataRepositoryTasksAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.DataRepositoryTasks)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}