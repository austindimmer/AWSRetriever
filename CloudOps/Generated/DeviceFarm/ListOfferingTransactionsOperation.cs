using Amazon;
using Amazon.DeviceFarm;
using Amazon.DeviceFarm.Model;
using Amazon.Runtime;

namespace CloudOps.DeviceFarm
{
    public class ListOfferingTransactionsOperation : Operation
    {
        public override string Name => "ListOfferingTransactions";

        public override string Description => "Returns a list of all historical purchases, renewals, and system renewal transactions for an AWS account. The list is paginated and ordered by a descending timestamp (most recent transactions are first). The API returns a NotEligible error if the user is not permitted to invoke the operation. If you must be able to invoke this operation, contact aws-devicefarm-support@amazon.com.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DeviceFarm";

        public override string ServiceID => "Device Farm";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDeviceFarmConfig config = new AmazonDeviceFarmConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDeviceFarmClient client = new AmazonDeviceFarmClient(creds, config);
            
            ListOfferingTransactionsResponse resp = new ListOfferingTransactionsResponse();
            do
            {
                try
                {
                    ListOfferingTransactionsRequest req = new ListOfferingTransactionsRequest
                    {
                        NextToken = resp.NextToken
                                            
                    };

                    resp = await client.ListOfferingTransactionsAsync(req);
                    
                    foreach (var obj in resp.OfferingTransactions)
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
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}