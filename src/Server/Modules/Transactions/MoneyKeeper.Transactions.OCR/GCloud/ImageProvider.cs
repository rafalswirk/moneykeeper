using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Console.GCloud
{
    public class ImageProvider
    {
        string jsonContent = "{\n  \"requests\": [\n    {\n      \"image\": {\n        \"content\": \"base64_image_placeholder\"\n      },\n      \"features\": [\n        {\n          \"type\": \"TEXT_DETECTION\"\n        }\n      ]\n    }\n  ]\n}";
        private readonly string _apiKey;

        public ImageProvider(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<string> SendImage(string imagePath)
        {
            var base64Image = GetBase64(imagePath);
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://vision.googleapis.com/v1/images:annotate?key={_apiKey}"),
                Content = new StringContent(GenerateJsonPayload(base64Image))
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                System.Console.WriteLine(body);
                return body;
            }
        }

        private string GenerateJsonPayload(string imageBase64)
        {
            string jsonPayload = @"{
                'requests': [
                    {
                        'image': {
                            'content': '" + imageBase64 + @"'
                        },
                        'features': [
                            {
                                'type': 'TEXT_DETECTION'
                            }
                        ]
                    }
                ]
            }";
            return jsonPayload;
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
