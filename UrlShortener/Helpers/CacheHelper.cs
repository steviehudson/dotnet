using Microsoft.Extensions.Caching.Memory;
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using UrlShortener.Dtos;
using Functional.Maybe;

namespace UrlShortener.Helpers
{
    public static class CacheHelper
    {
        private const string _cacheKey = "Urls";
        private static MemoryCacheEntryOptions cacheOptions = new() { AbsoluteExpiration = DateTime.Now.AddYears(5) };

        public static void AddToCache(UrlDto url, ShortenedUrlDto shortenedUrl, IMemoryCache cache)
        {
            List<CacheUrlDto> urlsToCache;

            CacheUrlDto urlToAdd = new()
            {
                Url = url,
                ShortenedUrl = shortenedUrl
            };

            cache.TryGetValue(_cacheKey, out List<CacheUrlDto> cachedUrls);
            urlsToCache = cachedUrls != null ? cachedUrls : new List<CacheUrlDto>();
            urlsToCache.Add(urlToAdd);            
            cache.Set(_cacheKey, urlsToCache, cacheOptions);
        }


        public static Maybe<ShortenedUrlDto> RetrieveFromCacheByUrl(string url, IMemoryCache cache)
        {
            cache.TryGetValue(_cacheKey, out List<CacheUrlDto> urls);

            if (urls != null)
            {
                var cachedUrl = urls.FirstOrDefault(x => x.Url.Url == url)?.ShortenedUrl ?? null;

                if (cachedUrl != null)
                {
                    return cachedUrl.ToMaybe();
                }
            }

            return Maybe<ShortenedUrlDto>.Nothing;
        }

        public static Maybe<UrlDto> RetrieveFromCacheByShortenedUrl(string shortenedUrl, IMemoryCache cache)
        {
            cache.TryGetValue(_cacheKey, out List<CacheUrlDto> urls);

            if (urls != null)
            {
                var cachedUrl = urls.FirstOrDefault(x => x.ShortenedUrl.ShortenedUrl == shortenedUrl)?.Url ?? null;

                if (cachedUrl != null)
                {
                    return cachedUrl.ToMaybe();
                }                
            }

            return Maybe<UrlDto>.Nothing;
        }
    }
}
