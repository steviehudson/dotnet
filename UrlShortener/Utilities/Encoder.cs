using System;
using System.Collections.Generic;
using UrlShortener.Dtos;

namespace UrlShortener.Utilities
{
    public static class Encoder
    {
        private static List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };

        private static List<char> characters = new List<char>()
        {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',    'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
         'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',  'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

        public static ShortenedUrlDto EncodeUri(Uri uri)
        {
            var shortenedUrl = new ShortenedUrlDto();
            shortenedUrl.ShortenedUrl = BuildUrl(uri);

            return shortenedUrl;
        }

        private static string BuildUrl(Uri uri)
        {
            //TODO need something in place to prevent duplicate shortenedUrls

            var trincatedUri = uri.Host.Substring(4, 6);
            var processedUri = trincatedUri.Insert(3, ".");
            var randomString = GenerateRandomString();

            if (uri.Scheme == Uri.UriSchemeHttp)
            {
                return $"http://{processedUri}/{randomString}";
            }
            else
            {
                return $"http://{processedUri}/{randomString}";
            }
        }

        private static string GenerateRandomString()
        {
            string randomString = "";
            Random random = new Random();
            for (int i = 0; i < 8; i++)
            {
                int randomValue = random.Next(0, 2);
                if (randomValue == 1)
                {
                    randomValue = random.Next(0, numbers.Count);
                    randomString += numbers[randomValue].ToString();
                }
                else
                {
                    randomValue = random.Next(0, characters.Count);
                    randomString += characters[randomValue].ToString();
                }
            }
            return randomString;
        }

        
    }
}
