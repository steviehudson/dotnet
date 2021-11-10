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
    public class DecodeControllerTests :TestBase
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
        public void IsValid()
        {
            //Arrange
            var logger = new Mock<ILogger<DecodeController>>();
            var cache = new Mock<IMemoryCache>();
            var sut = new DecodeController(logger.Object, cache.Object);

            var cachedUrl = new List<CacheUrlDto>
            {
                new CacheUrlDto
                {
                    ShortenedUrl = new ShortenedUrlDto
                    {
                        ShortenedUrl = "https://mus.cle/aX9824Gz"
                    },
                    Url = new UrlDto
                    {
                        Url = "https://www.musclefood.com/build-your-own-box-50"
                    }
                }      
            };

            object cachedUrlObject = cachedUrl;

            cache
                .Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedUrlObject))
                .Returns(true);

            //Act
            var url = sut.Decode(cachedUrl[0].ShortenedUrl);

            //Assert
            Assert.AreEqual(cachedUrl[0].Url, url);

        }

        [TestMethod]
        [DataRow("fdjhfg")]
        [DataRow("https://mus.cle/pppppppp")]
        [DataRow("https://www.musclefood.com/build-your-own-box-50")]
        public void ReturnsError(string shortenedUrl)
        {
            //Arrange
            var logger = new Mock<ILogger<DecodeController>>();
            var cache = new Mock<IMemoryCache>();
            var sut = new DecodeController(logger.Object, cache.Object);

            var shortenedUrlDto = new ShortenedUrlDto
            {
                ShortenedUrl = shortenedUrl
            };

            var cachedUrl = new List<CacheUrlDto>
            {
                new CacheUrlDto
                {
                    ShortenedUrl = new ShortenedUrlDto
                    {
                        ShortenedUrl = "https://mus.cle/aX9824Gz"
                    },
                    Url = new UrlDto
                    {
                        Url = "https://www.musclefood.com/build-your-own-box-50"
                    }
                }
            };

            object cachedUrlObject = cachedUrl;

            cache
                .Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedUrlObject))
                .Returns(true);

            var urlDto = new UrlDto
            {
                Url = "Invalid Url"
            };

            //Act
            var url = sut.Decode(shortenedUrlDto);

            //Assert
            Assert.AreEqual(urlDto.Url, url.Url);
        }
    }
}
