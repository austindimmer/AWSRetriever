using Amazon;
using Amazon.SWF;
using Amazon.SWF.Model;
using Amazon.Runtime;

namespace CloudOps.SWF
{
    public class PollForDecisionTaskOperation : Operation
    {
        public override string Name => "PollForDecisionTask";

        public override string Description => "Used by deciders to get a DecisionTask from the specified decision taskList. A decision task may be returned for any open workflow execution that is using the specified task list. The task includes a paginated view of the history of the workflow execution. The decider should use the workflow type and the history to determine how to properly handle the task. This action initiates a long poll, where the service holds the HTTP connection open and responds as soon a task becomes available. If no decision task is available in the specified task list before the timeout of 60 seconds expires, an empty result is returned. An empty result, in this context, means that a DecisionTask is returned, but that the value of taskToken is an empty string.  Deciders should set their client side socket timeout to at least 70 seconds (10 seconds higher than the timeout).   Because the number of workflow history events for a single workflow execution might be very large, the result returned might be split up across a number of pages. To retrieve subsequent pages, make additional calls to PollForDecisionTask using the nextPageToken returned by the initial call. Note that you do not call GetWorkflowExecutionHistory with this nextPageToken. Instead, call PollForDecisionTask again.   Access Control  You can use IAM policies to control this action&#39;s access to Amazon SWF resources as follows:   Use a Resource element with the domain name to limit the action to only specified domains.   Use an Action element to allow or deny permission to call this action.   Constrain the taskList.name parameter by using a Condition element with the swf:taskList.name key to allow the action to access only certain task lists.   If the caller doesn&#39;t have sufficient permissions to invoke the action, or the parameter values fall outside the specified constraints, the action fails. The associated event attribute&#39;s cause parameter is set to OPERATION_NOT_PERMITTED. For details and example IAM policies, see Using IAM to Manage Access to Amazon SWF Workflows in the Amazon SWF Developer Guide.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SWF";

        public override string ServiceID => "SWF";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSWFConfig config = new AmazonSWFConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSWFClient client = new AmazonSWFClient(creds, config);
            
            DecisionTask resp = new DecisionTask();
            do
            {
                try
                {
                    PollForDecisionTaskRequest req = new PollForDecisionTaskRequest
                    {
                        NextPageToken = resp.NextPageToken
                        ,
                        MaximumPageSize = maxItems
                                            
                    };

                    resp = await client.PollForDecisionTaskAsync(req);
                    
                    foreach (var obj in resp.Events)
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
            while (!string.IsNullOrEmpty(resp.NextPageToken));
        }
    }
}