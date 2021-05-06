using Amazon;
using Amazon.Elasticsearch;
using Amazon.Elasticsearch.Model;
using Amazon.Runtime;

namespace CloudOps.Elasticsearch
{
    public class DescribeInboundCrossClusterSearchConnectionsOperation : Operation
    {
        public override string Name => "DescribeInboundCrossClusterSearchConnections";

        public override string Description => "Lists all the inbound cross-cluster search connections for a destination domain.";
 
        public override string RequestURI => "/2015-01-01/es/ccs/inboundConnection/search";

        public override string Method => "POST";

        public override string ServiceName => "Elasticsearch";

        public override string ServiceID => "Elasticsearch Service";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonElasticsearchConfig config = new AmazonElasticsearchConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonElasticsearchClient client = new AmazonElasticsearchClient(creds, config);
            
            DescribeInboundCrossClusterSearchConnectionsResponse resp = new DescribeInboundCrossClusterSearchConnectionsResponse();
            do
            {
                DescribeInboundCrossClusterSearchConnectionsRequest req = new DescribeInboundCrossClusterSearchConnectionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeInboundCrossClusterSearchConnectionsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.CrossClusterSearchConnections)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}