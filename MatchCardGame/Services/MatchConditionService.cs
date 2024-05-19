namespace MatchCardGame.Services
{
    public class MatchConditionService : IMatchConditionService
    {
        private MatchCondition MatchCondition = MatchCondition.SuitMatch;

        public void SetMatchCondition(string matchCondition)
        {
            switch (matchCondition)
            {
                default:
                    MatchCondition = MatchCondition.SuitMatch;
                    break;
                case "ValueMatch":
                    MatchCondition = MatchCondition.ValueMatch;
                    break;
                case "ExactMatch":
                    MatchCondition = MatchCondition.ExactMatch;
                    break;
            }
        }

        public MatchCondition GetMatchCondition()
        {
            return MatchCondition;
        }

    }
}
