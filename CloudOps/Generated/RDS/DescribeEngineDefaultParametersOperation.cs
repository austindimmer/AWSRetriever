using Amazon;
using Amazon.RDS;
using Amazon.RDS.Model;
using Amazon.Runtime;

namespace CloudOps.RDS
{
    public class DescribeEngineDefaultParametersOperation : Operation
    {
        public override string Name => "DescribeEngineDefaultParameters";

        public override string Description => "";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "RDS";

        public override string ServiceID => "RDS";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRDSConfig config = new AmazonRDSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRDSClient client = new AmazonRDSClient(creds, config);
            
            DescribeEngineDefaultParametersResponse resp = new DescribeEngineDefaultParametersResponse();
            do
            {
                try
                {
                    DescribeEngineDefaultParametersRequest req = new DescribeEngineDefaultParametersRequest
                    {
                        Marker = resp.EngineDefaultsMarker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeEngineDefaultParametersAsync(req);
                    
                    foreach (var obj in resp.EngineDefaultsParameters)
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
            while (!string.IsNullOrEmpty(resp.EngineDefaultsMarker));
        }
    }
}