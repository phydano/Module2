using System;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using PCLStorage;

namespace Module2
{
    public partial class MainPage : ContentPage
    {
        // My generated subscription key and URI last until the 15th of August 2017
        const string subscriptionKey = "509e2704d1f5448dabf1afb53ac8b65e";
        const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0";
        MobileServiceClient client = AzureService.serviceInstance.serviceClient;

        public MainPage()
        {
            InitializeComponent();
        }

        async void loadImage(object sender, EventArgs e)
        {
            // The image file that we are going to use
            String imageFile = "msimage.jpg";

            try
            {
                // Get the file from the localstorage using PCLStorage              
                var file = await FileSystem.Current.LocalStorage.GetFileAsync(imageFile);
                image.Source = ImageSource.FromFile(file.Path); // show the image 
                // Build the String to put all the words together
                StringBuilder builder = new StringBuilder();
                builder.Append("Result: ");
                builder.AppendLine();

                using (var fileStream = await file.OpenAsync(FileAccess.Read))
                {
                    // using my subscription key and uri
                    VisionServiceClient visionServiceClient = new VisionServiceClient(subscriptionKey, uriBase);
                    // analyze the image 
                    OcrResults analysisResult = await visionServiceClient.RecognizeTextAsync(fileStream);
                    // Loop to find the words following the JSON format
                    foreach (var region in analysisResult.Regions)
                    {
                        foreach (var line in region.Lines)
                        {
                            foreach (var word in line.Words)
                            {
                                builder.Append(word.Text);
                                builder.Append(" ");
                            }
                            builder.AppendLine();
                        }
                        builder.AppendLine();
                    }
                    text.Text = builder.ToString(); // display the final result
                    ocrtable ocrResult = new ocrtable()
                    {
                        Ocrtext = builder.ToString()
                    };
                    await AzureService.serviceInstance.PostOcrTable(ocrResult);
                }
            }
            catch (Exception error)
            {
                await DisplayAlert("Error", error.ToString(), "OK"); // pop an error if it occurs
            }
        }
    }
}
