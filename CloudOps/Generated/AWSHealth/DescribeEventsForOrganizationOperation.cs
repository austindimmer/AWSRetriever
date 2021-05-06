using Amazon;
using Amazon.AWSHealth;
using Amazon.AWSHealth.Model;
using Amazon.Runtime;

namespace CloudOps.AWSHealth
{
    public class DescribeEventsForOrganizationOperation : Operation
    {
        public override string Name => "DescribeEventsForOrganization";

        public override string Description => "Returns information about events across your organization in AWS Organizations. You can use thefilters parameter to specify the events that you want to return. Events are returned in a summary form and don&#39;t include the affected accounts, detailed description, any additional metadata that depends on the event type, or any affected resources. To retrieve that information, use the following operations:    DescribeAffectedAccountsForOrganization     DescribeEventDetailsForOrganization     DescribeAffectedEntitiesForOrganization    If you don&#39;t specify a filter, the DescribeEventsForOrganizations returns all events across your organization. Results are sorted by lastModifiedTime, starting with the most recent event.  For more information about the different types of AWS Health events, see Event. Before you can call this operation, you must first enable AWS Health to work with AWS Organizations. To do this, call the EnableHealthServiceAccessForOrganization operation from your organization&#39;s management account.  This API operation uses pagination. Specify the nextToken parameter in the next request to return more results. ";
 
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
            
            DescribeEventsForOrganizationResponse resp = new DescribeEventsForOrganizationResponse();
            do
            {
                DescribeEventsForOrganizationRequest req = new DescribeEventsForOrganizationRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeEventsForOrganizationAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Events)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}