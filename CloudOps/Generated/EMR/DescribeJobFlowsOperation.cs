using Amazon;
using Amazon.EMR;
using Amazon.EMR.Model;
using Amazon.Runtime;

namespace CloudOps.EMR
{
    public class DescribeJobFlowsOperation : Operation
    {
        public override string Name => "DescribeJobFlows";

        public override string Description => "This API is no longer supported and will eventually be removed. We recommend you use ListClusters, DescribeCluster, ListSteps, ListInstanceGroups and ListBootstrapActions instead. DescribeJobFlows returns a list of job flows that match all of the supplied parameters. The parameters can include a list of job flow IDs, job flow states, and restrictions on job flow creation date and time. Regardless of supplied parameters, only job flows created within the last two months are returned. If no parameters are supplied, then job flows matching either of the following criteria are returned:   Job flows created and completed in the last two weeks    Job flows created within the last two months that are in one of the following states: RUNNING, WAITING, SHUTTING_DOWN, STARTING    Amazon EMR can return a maximum of 512 job flow descriptions.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EMR";

        public override string ServiceID => "EMR";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEMRConfig config = new AmazonEMRConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEMRClient client = new AmazonEMRClient(creds, config);
            
            DescribeJobFlowsResponse resp = new DescribeJobFlowsResponse();
            DescribeJobFlowsRequest req = new DescribeJobFlowsRequest
            {                    
                                    
            };
            
            try
            {
                resp = await client.DescribeJobFlowsAsync(req);
                
                foreach (var obj in resp.JobFlows)
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
    }
}