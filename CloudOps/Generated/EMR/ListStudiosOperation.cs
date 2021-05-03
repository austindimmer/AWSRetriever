using Amazon;
using Amazon.EMR;
using Amazon.EMR.Model;
using Amazon.Runtime;

namespace CloudOps.EMR
{
    public class ListStudiosOperation : Operation
    {
        public override string Name => "ListStudios";

        public override string Description => "Returns a list of all Amazon EMR Studios associated with the AWS account. The list includes details such as ID, Studio Access URL, and creation time for each Studio.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EMR";

        public override string ServiceID => "EMR";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEMRConfig config = new AmazonEMRConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEMRClient client = new AmazonEMRClient(creds, config);
            
            ListStudiosResponse resp = new ListStudiosResponse();
            do
            {
                ListStudiosRequest req = new ListStudiosRequest
                {
                    Marker = resp.Marker
                                        
                };

                resp = client.ListStudios(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Studios)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}