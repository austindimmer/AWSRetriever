using Amazon;
using Amazon.Neptune;
using Amazon.Neptune.Model;
using Amazon.Runtime;

namespace CloudOps.Neptune
{
    public class DescribeDBParametersOperation : Operation
    {
        public override string Name => "DescribeDBParameters";

        public override string Description => "Returns the detailed parameter list for a particular DB parameter group.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Neptune";

        public override string ServiceID => "Neptune";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonNeptuneConfig config = new AmazonNeptuneConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonNeptuneClient client = new AmazonNeptuneClient(creds, config);
            
            DescribeDBParametersResponse resp = new DescribeDBParametersResponse();
            do
            {
                DescribeDBParametersRequest req = new DescribeDBParametersRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = client.DescribeDBParameters(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Parameters)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}