using Blackjack.Domain;
using Blackjack.Domain.Tests.Helpers;

namespace Blackjack.Web.Tests.Fakes
{
    public class FakeBlackjackGameResult : BlackjackGameResult
    {
        private static readonly FakePlayer Player = new FakePlayer();

        internal FakeBlackjackGameResult()
            : base(Player, new[]
                               {
                                   Player
                               })
        {
            Player.FakeScore = 10;
        }

        public new bool IsDraw()
        {
            return false;
        }
    }
}
