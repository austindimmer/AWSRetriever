using Amazon;
using Amazon.AWSHealth;
using Amazon.AWSHealth.Model;
using Amazon.Runtime;

namespace CloudOps.AWSHealth
{
    public class DescribeEventTypesOperation : Operation
    {
        public override string Name => "DescribeEventTypes";

        public override string Description => "Returns the event types that meet the specified filter criteria. You can use this API operation to find information about the AWS Health event, such as the category, AWS service, and event code. The metadata for each event appears in the EventType object.  If you don&#39;t specify a filter criteria, the API operation returns all event types, in no particular order.   This API operation uses pagination. Specify the nextToken parameter in the next request to return more results. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "AWSHealth";

        public override string ServiceID => "Health";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAWSHealthConfig config = new AmazonAWSHealthConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAWSHealthClient client = new AmazonAWSHealthClient(creds, config);
            
            DescribeEventTypesResponse resp = new DescribeEventTypesResponse();
            do
            {
                try
                {
                    DescribeEventTypesRequest req = new DescribeEventTypesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeEventTypesAsync(req);
                    
                    foreach (var obj in resp.EventTypes)
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