using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace CSHttpClientSample
{
    public class Program
    {
        static EmotionProfile ep;
        static MusicController mc;

        static string dominantEmotion;
        static double dominantEmotionValue;

        static string playlistURI;
        static string playlist;

        static string subscriptionKey;
        const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect";

        static void Main()
        {
            // Get subscription key from user
            Console.Write("Enter Azure API subcription key: ");
            subscriptionKey = Console.ReadLine();

            // Get the path and filename to process from the user.
            Console.WriteLine("\nDetect faces:");
            Console.Write("Enter the path to an image with a face (currently supports one) that you wish to analzye: ");
            string imageFilePath = Console.ReadLine();

            // Execute the REST API call.
            MakeAnalysisRequest(imageFilePath);

            Console.WriteLine("\nPlease wait a moment for the results to appear. Then, press Enter to exit...\n");
            Console.ReadLine();
        }

        /// <summary>
        /// Gets the analysis of the specified image file by using the Computer Vision REST API.
        /// </summary>
        /// <param name="imageFilePath">The image file.</param>
        static async void MakeAnalysisRequest(string imageFilePath)
        {
            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameters. A third optional parameter is "details".
            string requestParameters = "returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

            // Assemble the URI for the REST API Call.
            string uri = uriBase + "?" + requestParameters;

            HttpResponseMessage response;

            // Request body. Posts a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(uri, content);

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                // Display the complete JSON response.
                Console.WriteLine("\nComplete API Response:\n");
                Console.WriteLine(JsonPrettyPrint(contentString));

                // Creates EmotionProfile using JSON raw data
                dynamic reply = JArray.Parse(contentString);
                ep = new EmotionProfile(reply);

                // Display the emotion portion of the JSON response
                Console.WriteLine("\nEmotion values:\n");
                Console.WriteLine(ep.toString());

                // Get dominant emotion
                dominantEmotion = ep.getDominantEmotionKey();
                dominantEmotionValue = ep.getDominantEmotionValue();

                // Display calculated dominant emotion
                Console.WriteLine("\nCalculated dominant emotion: " + dominantEmotion);
                Console.WriteLine("Emotion value: " + dominantEmotionValue);

                // Playlist configuration
                mc = new MusicController();
                playlistURI = mc.getMusicURI(dominantEmotion);
                playlist = mc.getMusicPlaylist(dominantEmotion);
                
                // Plays music
                Console.WriteLine("\n...Now playing: \"" + playlist + "\" on Spotify Music.");
                startMusic(playlistURI);
            }
        }

        static void startMusic(string URI)
        {
            System.Diagnostics.Process.Start("cmd", "/C start" + " " + URI);
        }

        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="imageFilePath">The image file to read.</param>
        /// <returns>The byte array of the image data.</returns>
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        /// <summary>
        /// Formats the given JSON string using indented formatting
        /// </summary>
        /// <param name="json">The raw JSON string to format.</param>
        /// <returns>The formatted JSON string.</returns>
        static string JsonPrettyPrint(string json)
        {
            JToken parsedJson = JToken.Parse(json);
            var beautifulText = parsedJson.ToString(Newtonsoft.Json.Formatting.Indented);

            return (string)beautifulText;
        }
    }
}