using Amazon;
using Amazon.Redshift;
using Amazon.Redshift.Model;
using Amazon.Runtime;

namespace CloudOps.Redshift
{
    public class DescribeHsmConfigurationsOperation : Operation
    {
        public override string Name => "DescribeHsmConfigurations";

        public override string Description => "Returns information about the specified Amazon Redshift HSM configuration. If no configuration ID is specified, returns information about all the HSM configurations owned by your AWS customer account. If you specify both tag keys and tag values in the same request, Amazon Redshift returns all HSM connections that match any combination of the specified keys and values. For example, if you have owner and environment for tag keys, and admin and test for tag values, all HSM connections that have any combination of those values are returned. If both tag keys and values are omitted from the request, HSM connections are returned regardless of whether they have tag keys or values associated with them.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Redshift";

        public override string ServiceID => "Redshift";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRedshiftConfig config = new AmazonRedshiftConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRedshiftClient client = new AmazonRedshiftClient(creds, config);
            
            DescribeHsmConfigurationsResponse resp = new DescribeHsmConfigurationsResponse();
            do
            {
                try
                {
                    DescribeHsmConfigurationsRequest req = new DescribeHsmConfigurationsRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeHsmConfigurationsAsync(req);
                    
                    foreach (var obj in resp.HsmConfigurations)
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
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}