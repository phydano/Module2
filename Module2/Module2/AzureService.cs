using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Module2
{
    public class AzureService
    {
        public static AzureService instance;
        public MobileServiceClient client;
        public IMobileServiceTable<ocrtable> ocrtable;

        public AzureService()
        {
            this.client = new MobileServiceClient("http://ocrdetection.azurewebsites.net");
            this.ocrtable = this.client.GetTable<ocrtable>();
        }

        public MobileServiceClient serviceClient { get { return client; } }

        public static AzureService serviceInstance {
            get {
                if (instance == null) instance = new AzureService();
                return instance; }
        }

        public async Task PostOcrTable(ocrtable ocrtable)
        {
            await this.ocrtable.InsertAsync(ocrtable);
        }
    }
}
