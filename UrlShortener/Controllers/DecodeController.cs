using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using UrlShortener.Dtos;
using UrlShortener.Helpers;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DecodeController : ControllerBase
    {
        private readonly ILogger<DecodeController> _logger;
        private IMemoryCache _cache;

        public DecodeController(ILogger<DecodeController> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;

        }

        [HttpPost]
        public UrlDto Decode(ShortenedUrlDto request) //TODO add logging
        {
            //TODO check url has been shortened
            var isValid = Uri.TryCreate(request.ShortenedUrl, UriKind.Absolute, out Uri uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
            var url = CacheHelper.RetrieveFromCacheByShortenedUrl(request.ShortenedUrl, _cache);

            if (isValid && url.HasValue)
            {
                return url.Value;
            }
            else
            {
                //TODO Add logging and revisit errors returned to user
                return new UrlDto()
                {
                    Url = "Invalid Url"
                };
            }
        }
    }
}
