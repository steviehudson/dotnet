using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using UnitTestingTest;
using UrlShortener.Controllers;
using UrlShortener.Dtos;

namespace UrlShortener.Tests.ControllersTests
{
    [TestClass]
    public class EncodeControllerTests : TestBase
    {
        private const string _cacheKey = "Urls";

        [ClassInitialize()]
        public static void ClassInitialization(TestContext tc)
        {
            //TODO: Initialization for all tests in class
            tc.WriteLine("In ClassInitialization() method");
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            //TODO: Clean up after all tests in class
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestContext.WriteLine("In TestInitialize() method");

            WriteDescription(GetType());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            TestContext.WriteLine("In TestCleanup() method");
        }

        //TODO refactor resuable code into test base

        [TestMethod]
        public void ShortenedUrlExists()
        {
            //Arrange
            var logger = new Mock<ILogger<EncodeController>>();
            var cache = new Mock<IMemoryCache>();
            var sut = new EncodeController(logger.Object, cache.Object);           

            var cachedUrl = new List<CacheUrlDto>
            {
                new CacheUrlDto
                {
                    Url = new UrlDto
                    {
                         Url = "https://www.musclefood.com/build-your-own-box-50"
                    },     
                    ShortenedUrl = new ShortenedUrlDto
                    {
                        ShortenedUrl = "https://mus.cle/aX9824Gz"
                    }
                }
            };

            object cachedUrlObject = cachedUrl;

            cache
                .Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedUrlObject))
                .Returns(true);

            //Act
            var shortenedUrl = sut.Encode(cachedUrl[0].Url);

            //Assert
            Assert.AreEqual(cachedUrl[0].ShortenedUrl, shortenedUrl);
        }

        //TODO revisit
        //IMemoryCache.Set Is an extension method and cannot be mocked using Moq framework.
        //Concrete implementation?
        [Ignore]
        [TestMethod]
        public void Encodes()
        {
            //Arrange
            var logger = new Mock<ILogger<EncodeController>>();
            var cache = new Mock<IMemoryCache>();
            var sut = new EncodeController(logger.Object, cache.Object);

            var urlToCache = new List<CacheUrlDto>
            {
                new CacheUrlDto
                {
                    Url = new UrlDto
                    {
                         Url = "https://www.musclefood.com/build-your-own-box-50"
                    },
                    ShortenedUrl = new ShortenedUrlDto
                    {
                        ShortenedUrl = "https://mus.cle/aX9824Gz"
                    }
                }
            };

            //Act
            var shortenedUrl = sut.Encode(urlToCache[0].Url);

            //Assert
            StringAssert.Contains(shortenedUrl.ShortenedUrl, "https://mus.cle/");
        }

        [TestMethod]
        [DataRow("fdjhfg")]
        public void ReturnsError(string urlRequest)
        {
            //Arrange
            var logger = new Mock<ILogger<EncodeController>>();
            var cache = new Mock<IMemoryCache>();
            var sut = new EncodeController(logger.Object, cache.Object);

            var urlDtoRequest = new UrlDto
            {
                Url = urlRequest
            };

            var urlDtoError = new UrlDto
            {
                Url = "Invalid Url"
            };

            //Act
            var url = sut.Encode(urlDtoRequest);

            //Assert
            Assert.AreEqual(urlDtoError.Url, url.ShortenedUrl);
        }
    }
}
