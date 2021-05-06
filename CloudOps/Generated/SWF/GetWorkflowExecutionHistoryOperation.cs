using Amazon;
using Amazon.SWF;
using Amazon.SWF.Model;
using Amazon.Runtime;

namespace CloudOps.SWF
{
    public class GetWorkflowExecutionHistoryOperation : Operation
    {
        public override string Name => "GetWorkflowExecutionHistory";

        public override string Description => "Returns the history of the specified workflow execution. The results may be split into multiple pages. To retrieve subsequent pages, make the call again using the nextPageToken returned by the initial call.  This operation is eventually consistent. The results are best effort and may not exactly reflect recent updates and changes.   Access Control  You can use IAM policies to control this action&#39;s access to Amazon SWF resources as follows:   Use a Resource element with the domain name to limit the action to only specified domains.   Use an Action element to allow or deny permission to call this action.   You cannot use an IAM policy to constrain this action&#39;s parameters.   If the caller doesn&#39;t have sufficient permissions to invoke the action, or the parameter values fall outside the specified constraints, the action fails. The associated event attribute&#39;s cause parameter is set to OPERATION_NOT_PERMITTED. For details and example IAM policies, see Using IAM to Manage Access to Amazon SWF Workflows in the Amazon SWF Developer Guide.";
 
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
            
            History resp = new History();
            do
            {
                try
                {
                    GetWorkflowExecutionHistoryRequest req = new GetWorkflowExecutionHistoryRequest
                    {
                        NextPageToken = resp.NextPageToken
                        ,
                        MaximumPageSize = maxItems
                                            
                    };

                    resp = await client.GetWorkflowExecutionHistoryAsync(req);
                    
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