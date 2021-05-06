using Amazon;
using Amazon.AWSHealth;
using Amazon.AWSHealth.Model;
using Amazon.Runtime;

namespace CloudOps.AWSHealth
{
    public class DescribeEventsOperation : Operation
    {
        public override string Name => "DescribeEvents";

        public override string Description => " Returns information about events that meet the specified filter criteria. Events are returned in a summary form and do not include the detailed description, any additional metadata that depends on the event type, or any affected resources. To retrieve that information, use the DescribeEventDetails and DescribeAffectedEntities operations. If no filter criteria are specified, all events are returned. Results are sorted by lastModifiedTime, starting with the most recent event.    When you call the DescribeEvents operation and specify an entity for the entityValues parameter, AWS Health might return public events that aren&#39;t specific to that resource. For example, if you call DescribeEvents and specify an ID for an Amazon Elastic Compute Cloud (Amazon EC2) instance, AWS Health might return events that aren&#39;t specific to that resource or service. To get events that are specific to a service, use the services parameter in the filter object. For more information, see Event.   This API operation uses pagination. Specify the nextToken parameter in the next request to return more results.   ";
 
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
            
            DescribeEventsResponse resp = new DescribeEventsResponse();
            do
            {
                try
                {
                    DescribeEventsRequest req = new DescribeEventsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeEventsAsync(req);
                    
                    foreach (var obj in resp.Events)
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