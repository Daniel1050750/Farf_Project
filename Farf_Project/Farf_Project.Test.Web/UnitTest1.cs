//using Farf_Project.Framework.Logging;
//using Farf_Project.Framework.Web.Slicing.Context;
//using Farf_Project.Framework.Web.Slicing.Shell.ContextLoaders;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.Internal;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using Microsoft.Extensions.Primitives;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Farf_Project.Web.AppSettings;
//using Farf_Project.Web.Controllers;
//using Farf_Project.Web.DependenciesMocks;
//using Farf_Project.Web.Extensions;
//using Farf_Project.Web.Loaders;
//using Farf_Project.Web.Models;
//using Farf_Project.Web.RequestHandlers;
//using Farf_Project.Web.Services.Language;
//using Farf_Project.Web.Services.Slugs;
//using Farf_Project.Web.Services.UserPreferences;
//using Farf_Project.Web.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using Xunit;

namespace Farf_Project.Test.Web
{
   public class UnitTest1
  {
//        private const string mockResourcesBasePath = "../../../../../src/Presentation.Web/MockingResources";

//        public UnitTest1()
//        {
//            var logger = new Mock<ILog>();
//            Log.Current = logger.Object;

//            MockContext.DefineResourcesBasePath(mockResourcesBasePath);
//        }

//        [Fact]
//        public async Task Index_ReturnsViewModel()
//        {
//            //ARRANGE
//            var sliceContext = new DefaultValuesSliceContextLoader().Load(new SliceContext(), null, new Uri("localhost"));
//            var settings = this.CreateSettings();

//            var designersIndexRequestHandlerMock = new Mock<IDesignersIndexRequestHandler>();
//            designersIndexRequestHandlerMock
//                .Setup(x => x.ExecuteAsync(It.IsAny<DesignersIndexRequest>()))
//                .ReturnsAsync(new DesignersIndexViewModel());

//            var target = new DesignersController(sliceContext, designersIndexRequestHandlerMock.Object);
//            target.ControllerContext = new ControllerContext()
//            {
//                HttpContext = this.GetHttpContext("http://www.farfetch.com/")
//            };

//            //ACT
//            var result = await target.Index();

//            //ASSERT
//            Assert.IsNotNull(result);
//        }

//        [Fact]
//        public async Task Index_UsingMocks_ReturnsViewModelWithCompleteDesignersByLetterWithValidDesigners()
//        {
//            //ARRANGE
//            var userPreferencesService = new Mock<IUserPreferencesService>();
//            var controller = this.CreateController(userPreferencesService.Object);

//            //ACT
//            var result = await controller.Index();

//            //ASSERT
//            Assert.IsNotNull(result);
//            Assert.IsNotNull(result.DesignersByLetter);
//            CollectionAssert.AreEqual(CharExtensions.FullDesignersIndex, result.DesignersByLetter.Keys.ToArray());
//            Assert.IsTrue(result.DesignersByLetter.Values.Any());
//            foreach (var index in result.DesignersByLetter)
//            {
//                if (index.Value.Any())
//                {
//                    Assert.IsTrue(index.Value.OrderBy(x => x.Name).SequenceEqual(index.Value));
//                }
//            }
//        }

//        [Fact]
//        public async Task Index_UsingMocks_ViewModelWithFavourites()
//        {
//            //ARRANGE
//            var userPreferences = new UserPreferencesWithFavouritesMock();
//            var controller = this.CreateController(userPreferences);

//            //ACT
//            var result = await controller.Index();

//            IEnumerable<Designer> eDesigners = Enumerable.Empty<Designer>();
//            IEnumerable<Designer> mDesigners = Enumerable.Empty<Designer>();
//            IEnumerable<Designer> lDesigners = Enumerable.Empty<Designer>();
//            IEnumerable<Designer> wDesigners = Enumerable.Empty<Designer>();

//            result.DesignersByLetter.TryGetValue('E', out eDesigners);
//            result.DesignersByLetter.TryGetValue('M', out mDesigners);
//            result.DesignersByLetter.TryGetValue('L', out lDesigners);
//            result.DesignersByLetter.TryGetValue('W', out wDesigners);

//            //RESULT
//            Assert.IsTrue(result.Context.HasFavourites);
//            Assert.AreEqual(eDesigners.Where(x => x.IsFav == true).Count(), 2);
//            Assert.AreEqual(mDesigners.Where(x => x.IsFav == true).Count(), 1);
//            Assert.AreEqual(lDesigners.Where(x => x.IsFav == true).Count(), 1);
//            Assert.IsFalse(wDesigners.Where(x => x.IsFav == true).Any());
//        }

//        [Fact]
//        public async Task Index_UsingMocks_ViewModelWithoutFavourites()
//        {
//            //ARRANGE
//            var userPreferences = new UserPreferencesWithoutFavourites();
//            var controller = this.CreateController(userPreferences);

//            //ACT
//            var result = await controller.Index();

//            //RESULT
//            Assert.IsFalse(result.Context.HasFavourites);
//        }

//        #region Private Methods

//        private DesignersController CreateController(IUserPreferencesService userPreferences)
//        {
//            var sliceContext = new DefaultValuesSliceContextLoader().Load(new SliceContext(), null, new Uri("http://www.farfetch.com/"));

//            var slugClientMock = new SlugClientMock();
//            var brandClientMock = new BrandClientMock();

//            var languageServiceMock = new Mock<ILanguageService>();
//            var slugsCacheClient = new Mock<ISlugsCacheClient>();

//            var slugsServiceMock = new Mock<SlugsService>(slugsCacheClient.Object);
//            var slugsCacheServiceMock = new Mock<SlugsCacheService>(slugClientMock, slugsCacheClient.Object);
//            var seoTokensLoaderMock = new Mock<SeoTokensLoader>(slugsServiceMock.Object, slugsCacheServiceMock.Object);

//            var designersIndexRequestHandlerMock = new Mock<DesignersIndexRequestHandler>(
//                sliceContext, languageServiceMock.Object, brandClientMock, seoTokensLoaderMock.Object, userPreferences);

//            var controller = new DesignersController(sliceContext, designersIndexRequestHandlerMock.Object);
//            controller.ControllerContext = new ControllerContext()
//            {
//                HttpContext = this.GetHttpContext("http://www.farfetch.com/")
//            };

//            return controller;
//        }

//        private IOptions<SliceSettings> CreateSettings()
//        {
//            return Options.Create(new SliceSettings()
//            {
//                ApiUserAgent = "AGENT",
//                EcommerceApi = new EcommerceApiSettings()
//                {
//                    ApiVersion = Farfetch.PublicAPI.SDK.Version.ApiVersion.v1,
//                    Url = "https://api.com"
//                },
//                Logging = new LoggingSettings()
//                {
//                    LogLevel = "Info",
//                    PathFormat = "C:\\LOGS"
//                },
//                ServiceHostName = "localhost"
//            });
//        }

//        private HttpContext GetHttpContext(string requestUrl, Uri urlReferrer = null)
//        {
//            var httpContextBase = new Mock<HttpContext>();
//            var httpRequest = new Mock<HttpRequest>();
//            var httpResponse = new Mock<HttpResponse>();

//            var uri = new Uri(requestUrl);

//            httpContextBase.Setup(x => x.Request).Returns(httpRequest.Object);
//            httpContextBase.Setup(x => x.Response).Returns(httpResponse.Object);
//            httpRequest.Setup(x => x.QueryString).Returns(new QueryString(uri.Query));

//            var queryCollection = new Dictionary<string, StringValues>();
//            var queryString = HttpUtility.ParseQueryString(uri.Query);
//            foreach (var parameter in queryString)
//            {
//                var key = parameter.ToString();
//                var value = queryString.GetValues(key).First();

//                queryCollection.Add(key, value);
//            }

//            httpRequest.Setup(x => x.Query).Returns(new QueryCollection(queryCollection));

//            return httpContextBase.Object;
//        }
//        #endregion Private Methods
//    }
}
