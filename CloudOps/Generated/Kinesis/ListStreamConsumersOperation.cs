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

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonKinesisConfig config = new AmazonKinesisConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonKinesisClient client = new AmazonKinesisClient(creds, config);
            
            ListStreamConsumersResponse resp = new ListStreamConsumersResponse();
            do
            {
                ListStreamConsumersRequest req = new ListStreamConsumersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListStreamConsumers(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Consumers)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}