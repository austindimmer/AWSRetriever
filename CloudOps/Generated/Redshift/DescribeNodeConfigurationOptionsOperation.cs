using Amazon;
using Amazon.Redshift;
using Amazon.Redshift.Model;
using Amazon.Runtime;

namespace CloudOps.Redshift
{
    public class DescribeNodeConfigurationOptionsOperation : Operation
    {
        public override string Name => "DescribeNodeConfigurationOptions";

        public override string Description => "Returns properties of possible node configurations such as node type, number of nodes, and disk usage for the specified action type.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Redshift";

        public override string ServiceID => "Redshift";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRedshiftConfig config = new AmazonRedshiftConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRedshiftClient client = new AmazonRedshiftClient(creds, config);
            
            DescribeNodeConfigurationOptionsResponse resp = new DescribeNodeConfigurationOptionsResponse();
            do
            {
                DescribeNodeConfigurationOptionsRequest req = new DescribeNodeConfigurationOptionsRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = client.DescribeNodeConfigurationOptions(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.NodeConfigurationOptionList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}