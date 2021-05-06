using Amazon;
using Amazon.Redshift;
using Amazon.Redshift.Model;
using Amazon.Runtime;

namespace CloudOps.Redshift
{
    public class GetReservedNodeExchangeOfferingsOperation : Operation
    {
        public override string Name => "GetReservedNodeExchangeOfferings";

        public override string Description => "Returns an array of DC2 ReservedNodeOfferings that matches the payment type, term, and usage price of the given DC1 reserved node.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Redshift";

        public override string ServiceID => "Redshift";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRedshiftConfig config = new AmazonRedshiftConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRedshiftClient client = new AmazonRedshiftClient(creds, config);
            
            GetReservedNodeExchangeOfferingsResponse resp = new GetReservedNodeExchangeOfferingsResponse();
            do
            {
                try
                {
                    GetReservedNodeExchangeOfferingsRequest req = new GetReservedNodeExchangeOfferingsRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.GetReservedNodeExchangeOfferingsAsync(req);
                    
                    foreach (var obj in resp.ReservedNodeOfferings)
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
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}