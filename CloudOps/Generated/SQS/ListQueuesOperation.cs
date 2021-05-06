using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Amazon.Runtime;

namespace CloudOps.SQS
{
    public class ListQueuesOperation : Operation
    {
        public override string Name => "ListQueues";

        public override string Description => "Returns a list of your queues in the current region. The response includes a maximum of 1,000 results. If you specify a value for the optional QueueNamePrefix parameter, only queues with a name that begins with the specified value are returned.  The listQueues methods supports pagination. Set parameter MaxResults in the request to specify the maximum number of results to be returned in the response. If you do not set MaxResults, the response includes a maximum of 1,000 results. If you set MaxResults and there are additional results to display, the response includes a value for NextToken. Use NextToken as a parameter in your next request to listQueues to receive the next page of results.   Cross-account permissions don&#39;t apply to this action. For more information, see Grant cross-account permissions to a role and a user name in the Amazon Simple Queue Service Developer Guide. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SQS";

        public override string ServiceID => "SQS";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSQSConfig config = new AmazonSQSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSQSClient client = new AmazonSQSClient(creds, config);
            
            ListQueuesResponse resp = new ListQueuesResponse();
            do
            {
                try
                {
                    ListQueuesRequest req = new ListQueuesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListQueuesAsync(req);
                    
                    foreach (var obj in resp.QueueUrls)
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