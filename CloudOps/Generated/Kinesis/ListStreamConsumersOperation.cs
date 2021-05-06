using Amazon;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using Amazon.Runtime;

namespace CloudOps.Kinesis
{
    public class ListStreamConsumersOperation : Operation
    {
        public override string Name => "ListStreamConsumers";

        public override string Description => "Lists the consumers registered to receive data from a stream using enhanced fan-out, and provides information about each consumer. This operation has a limit of 5 transactions per second per stream.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Kinesis";

        public override string ServiceID => "Kinesis";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonKinesisConfig config = new AmazonKinesisConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonKinesisClient client = new AmazonKinesisClient(creds, config);
            
            ListStreamConsumersResponse resp = new ListStreamConsumersResponse();
            do
            {
                try
                {
                    ListStreamConsumersRequest req = new ListStreamConsumersRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListStreamConsumersAsync(req);
                    
                    foreach (var obj in resp.Consumers)
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