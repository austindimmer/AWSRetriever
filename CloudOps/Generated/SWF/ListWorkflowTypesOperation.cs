using Amazon;
using Amazon.SimpleWorkflow;
using Amazon.SimpleWorkflow.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleWorkflow
{
    public class ListWorkflowTypesOperation : Operation
    {
        public override string Name => "ListWorkflowTypes";

        public override string Description => "Returns information about workflow types in the specified domain. The results may be split into multiple pages that can be retrieved by making the call repeatedly.  Access Control  You can use IAM policies to control this action&#39;s access to Amazon SimpleWorkflow resources as follows:   Use a Resource element with the domain name to limit the action to only specified domains.   Use an Action element to allow or deny permission to call this action.   You cannot use an IAM policy to constrain this action&#39;s parameters.   If the caller doesn&#39;t have sufficient permissions to invoke the action, or the parameter values fall outside the specified constraints, the action fails. The associated event attribute&#39;s cause parameter is set to OPERATION_NOT_PERMITTED. For details and example IAM policies, see Using IAM to Manage Access to Amazon SimpleWorkflow Workflows in the Amazon SimpleWorkflow Developer Guide.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SimpleWorkflow";

        public override string ServiceID => "SimpleWorkflow";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSimpleWorkflowConfig config = new AmazonSimpleWorkflowConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSimpleWorkflowClient client = new AmazonSimpleWorkflowClient(creds, config);
            
            WorkflowTypeInfos resp = new WorkflowTypeInfos();
            do
            {
                ListWorkflowTypesRequest req = new ListWorkflowTypesRequest
                {
                    NextPageToken = resp.NextPageToken
                    ,
                    MaximumPageSize = maxItems
                                        
                };

                resp = client.ListWorkflowTypes(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.TypeInfos)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextPageToken));
        }
    }
}