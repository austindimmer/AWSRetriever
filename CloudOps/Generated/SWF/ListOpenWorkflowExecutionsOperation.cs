using Amazon;
using Amazon.SimpleWorkflow;
using Amazon.SimpleWorkflow.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleWorkflow
{
    public class ListOpenWorkflowExecutionsOperation : Operation
    {
        public override string Name => "ListOpenWorkflowExecutions";

        public override string Description => "Returns a list of open workflow executions in the specified domain that meet the filtering criteria. The results may be split into multiple pages. To retrieve subsequent pages, make the call again using the nextPageToken returned by the initial call.  This operation is eventually consistent. The results are best effort and may not exactly reflect recent updates and changes.   Access Control  You can use IAM policies to control this action&#39;s access to Amazon SimpleWorkflow resources as follows:   Use a Resource element with the domain name to limit the action to only specified domains.   Use an Action element to allow or deny permission to call this action.   Constrain the following parameters by using a Condition element with the appropriate keys.    tagFilter.tag: String constraint. The key is SimpleWorkflow:tagFilter.tag.    typeFilter.name: String constraint. The key is SimpleWorkflow:typeFilter.name.    typeFilter.version: String constraint. The key is SimpleWorkflow:typeFilter.version.     If the caller doesn&#39;t have sufficient permissions to invoke the action, or the parameter values fall outside the specified constraints, the action fails. The associated event attribute&#39;s cause parameter is set to OPERATION_NOT_PERMITTED. For details and example IAM policies, see Using IAM to Manage Access to Amazon SimpleWorkflow Workflows in the Amazon SimpleWorkflow Developer Guide.";
 
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
            
            WorkflowExecutionInfos resp = new WorkflowExecutionInfos();
            do
            {
                ListOpenWorkflowExecutionsRequest req = new ListOpenWorkflowExecutionsRequest
                {
                    NextPageToken = resp.NextPageToken
                    ,
                    MaximumPageSize = maxItems
                                        
                };

                resp = client.ListOpenWorkflowExecutions(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ExecutionInfos)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextPageToken));
        }
    }
}