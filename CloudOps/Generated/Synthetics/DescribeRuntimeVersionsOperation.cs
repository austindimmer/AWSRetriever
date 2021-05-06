using Amazon;
using Amazon.Synthetics;
using Amazon.Synthetics.Model;
using Amazon.Runtime;

namespace CloudOps.Synthetics
{
    public class DescribeRuntimeVersionsOperation : Operation
    {
        public override string Name => "DescribeRuntimeVersions";

        public override string Description => "Returns a list of Synthetics canary runtime versions. For more information, see  Canary Runtime Versions.";
 
        public override string RequestURI => "/runtime-versions";

        public override string Method => "POST";

        public override string ServiceName => "Synthetics";

        public override string ServiceID => "synthetics";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSyntheticsConfig config = new AmazonSyntheticsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSyntheticsClient client = new AmazonSyntheticsClient(creds, config);
            
            DescribeRuntimeVersionsResponse resp = new DescribeRuntimeVersionsResponse();
            do
            {
                try
                {
                    DescribeRuntimeVersionsRequest req = new DescribeRuntimeVersionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeRuntimeVersionsAsync(req);
                    
                    foreach (var obj in resp.RuntimeVersions)
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