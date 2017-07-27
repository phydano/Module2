using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Module2
{
    /* This class defines the connection to the Azure Service webserver
     */
    public class AzureService
    {
        public static AzureService instance;
        public MobileServiceClient mobileClient;
        public IMobileServiceTable<ocrtable> ocrtable;

        public AzureService()
        {
            this.mobileClient = new MobileServiceClient("http://ocrdetection.azurewebsites.net");
            this.ocrtable = this.mobileClient.GetTable<ocrtable>();
        }

        public MobileServiceClient serviceClient { get { return mobileClient; } }

        public static AzureService serviceInstance {
            get {
                if (instance == null) instance = new AzureService();
                return instance; }
        }

        // This method POST the data to the Easytable
        public async Task PostOcrTable(ocrtable ocrtable)
        {
            await this.ocrtable.InsertAsync(ocrtable);
        }
    }
}
