// FlowFocus.Tests/Components/Account/Pages/LoginTests.cs
using FlowFocus.Components.Account.Pages;

namespace FlowFocus.Tests.Components.Account.Pages
{
    public class LoginTests : LoginTestFixture
    {
        [Fact]
        public void LoginComponent_Renders()
        {
            // Arrange is done in LoginTestFixture

            // Act
            var cut = RenderComponent<Login>(parameters => parameters
                .AddCascadingValue(HttpContextMock.Object)
            );

            // Assert
            Assert.NotNull(cut);
        }
    }
}