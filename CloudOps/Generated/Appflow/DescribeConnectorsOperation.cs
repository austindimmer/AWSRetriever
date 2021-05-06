using Amazon;
using Amazon.Appflow;
using Amazon.Appflow.Model;
using Amazon.Runtime;

namespace CloudOps.Appflow
{
    public class DescribeConnectorsOperation : Operation
    {
        public override string Name => "DescribeConnectors";

        public override string Description => " Describes the connectors vended by Amazon AppFlow for specified connector types. If you don&#39;t specify a connector type, this operation describes all connectors vended by Amazon AppFlow. If there are more connectors than can be returned in one page, the response contains a nextToken object, which can be be passed in to the next call to the DescribeConnectors API operation to retrieve the next page. ";
 
        public override string RequestURI => "/describe-connectors";

        public override string Method => "POST";

        public override string ServiceName => "Appflow";

        public override string ServiceID => "Appflow";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAppflowConfig config = new AmazonAppflowConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAppflowClient client = new AmazonAppflowClient(creds, config);
            
            DescribeConnectorsResponse resp = new DescribeConnectorsResponse();
            do
            {
                DescribeConnectorsRequest req = new DescribeConnectorsRequest
                {
                    NextToken = resp.NextToken
                                        
                };

                resp = await client.DescribeConnectorsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ConnectorConfigurations)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}