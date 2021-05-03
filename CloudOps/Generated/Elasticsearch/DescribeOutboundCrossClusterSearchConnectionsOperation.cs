using Amazon;
using Amazon.Elasticsearch;
using Amazon.Elasticsearch.Model;
using Amazon.Runtime;

namespace CloudOps.Elasticsearch
{
    public class DescribeOutboundCrossClusterSearchConnectionsOperation : Operation
    {
        public override string Name => "DescribeOutboundCrossClusterSearchConnections";

        public override string Description => "Lists all the outbound cross-cluster search connections for a source domain.";
 
        public override string RequestURI => "/2015-01-01/es/ccs/outboundConnection/search";

        public override string Method => "POST";

        public override string ServiceName => "Elasticsearch";

        public override string ServiceID => "Elasticsearch Service";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonElasticsearchConfig config = new AmazonElasticsearchConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonElasticsearchClient client = new AmazonElasticsearchClient(creds, config);
            
            DescribeOutboundCrossClusterSearchConnectionsResponse resp = new DescribeOutboundCrossClusterSearchConnectionsResponse();
            do
            {
                DescribeOutboundCrossClusterSearchConnectionsRequest req = new DescribeOutboundCrossClusterSearchConnectionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeOutboundCrossClusterSearchConnections(req);
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