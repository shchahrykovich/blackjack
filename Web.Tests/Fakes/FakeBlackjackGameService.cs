using Blackjack.Domain;
using Blackjack.Domain.Players;
using Blackjack.Web.Infrastructure.Services;
using Blackjack.Web.Models;

namespace Blackjack.Web.Tests.Fakes
{
    public class FakeBlackjackGameService : IBlackjackGameService
    {
        private HumanPlayer _player;
        private BlackjackGame _game;

        public bool IsExiststs { get; set; }

        public FakeBlackjackGameService()
        {
            NewGame();
        }

        public IBlackjackGame GetCurrentGame()
        {
            return _game;
        }

        public HumanPlayer GetCurrentPlayer()
        {
            return _player;
        }

        public void NewGame()
        {
            _player = new HumanPlayer();
            _game = new BlackjackGame(_player);
            _game.Start();
        }

        public BlackjackStatisticModel GetStatistic()
        {
            BlackjackStatisticModel statistic = new BlackjackStatisticModel();
            return statistic;
        }

        public void SaveResults(BlackjackGameResult result)
        {
            
        }

        public bool IsGameExiststs()
        {
            return IsExiststs;
        }
    }
}
