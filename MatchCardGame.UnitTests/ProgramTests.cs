namespace MatchCardGame.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class ProgramTests
    {
        [Fact]
        public void ConfigureServices_WhenRegisterServices_ShouldNotBeNull()
        {
            // Act
            var services = Program.ConfigureServices();

            // Assert
            services.Invoking(x => x.GetRequiredService<TextReader>()).Should().NotBeNull();
            services.Invoking(x => x.GetRequiredService<TextWriter>()).Should().NotBeNull();
            services.Invoking(x => x.GetRequiredService<IConsoleWrapper>()).Should().NotBeNull();
            services.Invoking(x => x.GetRequiredService<IInputService>()).Should().NotBeNull();
            services.Invoking(x => x.GetRequiredService<ICardService>()).Should().NotBeNull();  
            services.Invoking(x => x.GetRequiredService<IMatchConditionService>()).Should().NotBeNull();
            services.Invoking(x => x.GetRequiredService<IPlayerService>()).Should().NotBeNull();
            services.Invoking(x => x.GetRequiredService<IDeckService>()).Should().NotBeNull();
            services.Invoking(x => x.GetRequiredService<IGameService>()).Should().NotBeNull();
        }

        [Fact]
        public void RunProgram_ShouldExecuteGameService()
        {
            // Arrange
            var gameServiceMock = new Mock<IGameService>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(x => x.GetService(typeof(IGameService))).Returns(gameServiceMock.Object);

            // Act
            Program.RunProgram(serviceProviderMock.Object);

            // Assert
            gameServiceMock.Verify(x => x.Setup(), Times.Once);
            gameServiceMock.Verify(x => x.Play(), Times.Once);
        }
    }
}