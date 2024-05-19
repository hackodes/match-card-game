namespace MatchCardGame.Interfaces
{
    public interface IMatchConditionService
    {
        public void SetMatchCondition(string matchCondition);

        public MatchCondition GetMatchCondition();
    }
}
