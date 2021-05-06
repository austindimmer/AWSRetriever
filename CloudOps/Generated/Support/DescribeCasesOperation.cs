using Amazon;
using Amazon.AWSSupport;
using Amazon.AWSSupport.Model;
using Amazon.Runtime;

namespace CloudOps.AWSSupport
{
    public class DescribeCasesOperation : Operation
    {
        public override string Name => "DescribeCases";

        public override string Description => "Returns a list of cases that you specify by passing one or more case IDs. You can use the afterTime and beforeTime parameters to filter the cases by date. You can set values for the includeResolvedCases and includeCommunications parameters to specify how much information to return. The response returns the following in JSON format:   One or more CaseDetails data types.   One or more nextToken values, which specify where to paginate the returned records represented by the CaseDetails objects.   Case data is available for 12 months after creation. If a case was created more than 12 months ago, a request might return an error.    You must have a Business or Enterprise AWSSupport plan to use the AWS AWSSupport API.    If you call the AWS AWSSupport API from an account that does not have a Business or Enterprise AWSSupport plan, the SubscriptionRequiredException error message appears. For information about changing your AWSSupport plan, see AWS AWSSupport.   ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "AWSSupport";

        public override string ServiceID => "AWSSupport";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAWSSupportConfig config = new AmazonAWSSupportConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAWSSupportClient client = new AmazonAWSSupportClient(creds, config);
            
            DescribeCasesResponse resp = new DescribeCasesResponse();
            do
            {
                DescribeCasesRequest req = new DescribeCasesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeCasesAsync(req);
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