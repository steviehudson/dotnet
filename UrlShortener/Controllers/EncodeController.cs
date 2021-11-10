using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using UrlShortener.Dtos;
using UrlShortener.Helpers;
using UrlShortener.Utilities;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EncodeController : ControllerBase
    {
        private readonly ILogger<EncodeController> _logger;
        private IMemoryCache _cache;

        public EncodeController(ILogger<EncodeController> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpPost]
        public ShortenedUrlDto Encode(UrlDto request)
        {
            //TODO check url length needs to be shortened
            var isValid = Uri.TryCreate(request.Url, UriKind.Absolute, out Uri uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
            
            if (isValid)
            {
                var shortenedUrl = CacheHelper.RetrieveFromCacheByUrl(request.Url, _cache);

                if (shortenedUrl.HasValue)
                {
                    return shortenedUrl.Value;
                }

                var processedUrl  = Encoder.EncodeUri(uri);
                
                CacheHelper.AddToCache(request, processedUrl, _cache);

                return processedUrl;
            }
            else
            {
                //TODO Add logging and revisit errors returned to user
                return new ShortenedUrlDto()
                {
                    ShortenedUrl = "Invalid Url"
                };
            }                 
        }
    }
}
