using Amazon;
using Amazon.Route53;
using Amazon.Route53.Model;
using Amazon.Runtime;

namespace CloudOps.Route53
{
    public class ListQueryLoggingConfigsOperation : Operation
    {
        public override string Name => "ListQueryLoggingConfigs";

        public override string Description => "Lists the configurations for DNS query logging that are associated with the current AWS account or the configuration that is associated with a specified hosted zone. For more information about DNS query logs, see CreateQueryLoggingConfig. Additional information, including the format of DNS query logs, appears in Logging DNS Queries in the Amazon Route 53 Developer Guide.";
 
        public override string RequestURI => "/2013-04-01/queryloggingconfig";

        public override string Method => "GET";

        public override string ServiceName => "Route53";

        public override string ServiceID => "Route 53";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRoute53Config config = new AmazonRoute53Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRoute53Client client = new AmazonRoute53Client(creds, config);
            
            ListQueryLoggingConfigsResponse resp = new ListQueryLoggingConfigsResponse();
            do
            {
                ListQueryLoggingConfigsRequest req = new ListQueryLoggingConfigsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems.ToString()
                                        
                };

                resp = client.ListQueryLoggingConfigs(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.QueryLoggingConfigs)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}