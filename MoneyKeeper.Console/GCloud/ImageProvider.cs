using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Console.GCloud
{
    internal class ImageProvider
    {
        string jsonContent = "{\n  \"requests\": [\n    {\n      \"image\": {\n        \"content\": \"base64_image_placeholder\"\n      },\n      \"features\": [\n        {\n          \"type\": \"TEXT_DETECTION\"\n        }\n      ]\n    }\n  ]\n}";
        public async Task<string> SendImage(string imagePath, string token, string projectId)
        {
            var base64Image = GetBase64(imagePath);
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://vision.googleapis.com/v1/images:annotate"),
                Headers =
                    {
                        { "Authorization", token },
                        { "x-goog-user-project", projectId },
                    },
                Content = new StringContent(jsonContent.Replace("base64_image_placeholder", base64Image))
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                System.Console.WriteLine(body);
                return body;
            }
        }

        public string GetBase64(string imagePath) 
        {
            var image = Image.Load(File.ReadAllBytes(imagePath));
            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, image.Metadata.DecodedImageFormat);
                byte[] imageBytes = m.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
    }
}
