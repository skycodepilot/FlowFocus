using FlowFocus.Components.Account;
using FlowFocus.Components.Account.Pages;
using FlowFocus.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace FlowFocus.Tests.Components.Account.Pages
{
    /// <summary>
    /// LoginTestFixture
    /// Consolidated setup for Login component tests.
    /// </summary>
    public abstract class LoginTestFixture : Bunit.TestContext
    {
        protected readonly Mock<HttpContext> HttpContextMock = new();
        protected readonly Mock<HttpRequest> HttpRequestMock = new();
        protected readonly Mock<HttpResponse> HttpResponseMock = new();
        protected readonly Mock<IRequestCookieCollection> RequestCookiesMock = new();
        protected readonly Mock<IResponseCookies> ResponseCookiesMock = new();
        protected readonly Mock<SignInManager<ApplicationUser>> SignInManagerMock;
        protected readonly Mock<IAuthenticationService> AuthServiceMock = new();
        protected readonly ServiceProvider ServiceProvider;

        protected LoginTestFixture()
        {
            // Register navigation and logger
            Services.AddSingleton<NavigationManager>(new TestNavigationManager());
            Services.AddSingleton(Mock.Of<ILogger<Login>>());
            Services.AddSingleton<IdentityRedirectManager>();

            // Register SignInManager
            SignInManagerMock = CreateSignInManagerMock();
            Services.AddSingleton(SignInManagerMock.Object);

            // Setup HttpContext and dependencies
            SetupHttpContext();

            // Register IAuthenticationService in a custom provider for HttpContext
            var services = new ServiceCollection();
            services.AddSingleton(AuthServiceMock.Object);
            ServiceProvider = services.BuildServiceProvider();
            HttpContextMock.SetupGet(c => c.RequestServices).Returns(ServiceProvider);

            // Setup IAuthenticationService.SignOutAsync to succeed
            AuthServiceMock
                .Setup(x => x.SignOutAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.CompletedTask);
        }

        private Mock<SignInManager<ApplicationUser>> CreateSignInManagerMock()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null
            );

            var signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                null, null, null, null
            );

            signInManagerMock
                .Setup(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);

            signInManagerMock
                .Setup(m => m.GetExternalAuthenticationSchemesAsync())
                .ReturnsAsync(Array.Empty<AuthenticationScheme>());

            return signInManagerMock;
        }

        private void SetupHttpContext()
        {
            // Setup request/response/cookies
            HttpRequestMock.SetupGet(r => r.Method).Returns("GET");
            HttpRequestMock.SetupGet(r => r.Cookies).Returns(RequestCookiesMock.Object);
            HttpResponseMock.SetupGet(r => r.Cookies).Returns(ResponseCookiesMock.Object);

            RequestCookiesMock.Setup(c => c[It.IsAny<string>()]).Returns((string)null!);

            HttpContextMock.SetupGet(c => c.Request).Returns(HttpRequestMock.Object);
            HttpContextMock.SetupGet(c => c.Response).Returns(HttpResponseMock.Object);
        }

        // Minimal NavigationManager for testing
        protected class TestNavigationManager : NavigationManager
        {
            public TestNavigationManager()
            {
                Initialize("https://unit-test.example/", "https://unit-test.example/");
            }
            protected override void NavigateToCore(string uri, bool forceLoad) => NotifyLocationChanged(false);
        }
    }
}