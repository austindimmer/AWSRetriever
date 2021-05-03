using Amazon;
using Amazon.Support;
using Amazon.Support.Model;
using Amazon.Runtime;

namespace CloudOps.Support
{
    public class DescribeCasesOperation : Operation
    {
        public override string Name => "DescribeCases";

        public override string Description => "Returns a list of cases that you specify by passing one or more case IDs. You can use the afterTime and beforeTime parameters to filter the cases by date. You can set values for the includeResolvedCases and includeCommunications parameters to specify how much information to return. The response returns the following in JSON format:   One or more CaseDetails data types.   One or more nextToken values, which specify where to paginate the returned records represented by the CaseDetails objects.   Case data is available for 12 months after creation. If a case was created more than 12 months ago, a request might return an error.    You must have a Business or Enterprise support plan to use the AWS Support API.    If you call the AWS Support API from an account that does not have a Business or Enterprise support plan, the SubscriptionRequiredException error message appears. For information about changing your support plan, see AWS Support.   ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Support";

        public override string ServiceID => "Support";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSupportConfig config = new AmazonSupportConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSupportClient client = new AmazonSupportClient(creds, config);
            
            DescribeCasesResponse resp = new DescribeCasesResponse();
            do
            {
                DescribeCasesRequest req = new DescribeCasesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeCases(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Cases)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}