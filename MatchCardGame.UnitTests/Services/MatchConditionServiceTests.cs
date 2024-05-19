namespace MatchCardGame.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class MatchConditionServiceTests
    {
        [Fact]
        public void SetMatchCondition_WhenCalledWithSuitMatch_ShouldSetMatchConditionToSuitMatch()
        {
            // Arrange
            var matchConditionService = new MatchConditionService();
            var matchCondition = "SuitMatch";

            // Act
            matchConditionService.SetMatchCondition(matchCondition);

            // Assert
            matchConditionService.GetMatchCondition().Should().Be(MatchCondition.SuitMatch);
        }

        [Fact]
        public void SetMatchCondition_WhenCalledWithValueMatch_ShouldSetMatchConditionToValueMatch()
        {
            // Arrange
            var matchConditionService = new MatchConditionService();
            var matchCondition = "ValueMatch";

            // Act
            matchConditionService.SetMatchCondition(matchCondition);

            // Assert
            matchConditionService.GetMatchCondition().Should().Be(MatchCondition.ValueMatch);
        }

        [Fact]
        public void SetMatchCondition_WhenCalledWithExactMatch_ShouldSetMatchConditionToExactMatch()
        {
            // Arrange
            var matchConditionService = new MatchConditionService();
            var matchCondition = "ExactMatch";

            // Act
            matchConditionService.SetMatchCondition(matchCondition);

            // Assert
            matchConditionService.GetMatchCondition().Should().Be(MatchCondition.ExactMatch);
        }
    }
}
