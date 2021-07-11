using System;
using System.Web.Mvc;
using Blackjack.Domain;
using Blackjack.Domain.Players;
using Blackjack.Web.Infrastructure.Services;
using Blackjack.Web.Models;
using EnsureUtility;

namespace Blackjack.Web.Controllers
{
    public partial class BlackjackController : Controller
    {
        #region Fields

        private readonly IBlackjackGameService _service;
        private HumanPlayer Player { get { return _service.GetCurrentPlayer(); }}
        private IBlackjackGame Game { get { return _service.GetCurrentGame(); } }

        #endregion

        #region Constructor

        public BlackjackController(IBlackjackGameService service)
        {
            _service = Ensure<IBlackjackGameService>.NotNull(service, "service");
        } 

        #endregion

        public virtual ActionResult Index()
        {
            return ShowInternal(false);
        }

        [HttpPost]
        public virtual ActionResult NewGame()
        {
            _service.NewGame();

            BlackjackGameModel result = GetModel(false);
            return Json(result);
        }

        [HttpPost]
        public virtual ActionResult Hit()
        {
            BlackjackGameModel result;
            if (!Player.IsFinishedTurn)
            {
                Player.Hit();
                Game.MakeTurn(Player);
                if (Player.IsFinishedTurn)
                {
                    FinishGame();
                    result = GetModel(true);
                }
                else
                {
                    result = GetModel(false);
                }
            }
            else
            {
                result = GetModel(false);
            }

            return Json(result);
        }

        [HttpPost]
        public virtual ActionResult Stand()
        {
            BlackjackGameModel result;
            if (!Player.IsFinishedTurn)
            {
                Player.Stand();
                Game.MakeTurn(Player);
                FinishGame();
                result = GetModel(true);
            }
            else
            {
                result = GetModel(false);
            }

            return Json(result);
        }

        #region Helper Methods

        private void FinishGame()
        {
            Game.FinishGame();
            BlackjackGameResult result = Game.GetResult();
            _service.SaveResults(result);
        }

        private ActionResult ShowInternal(bool showScores)
        {
            BlackjackGameModel model = GetModel(showScores);
            return View(MVC.Blackjack.Views.Index, model);
        }

        private BlackjackGameModel GetModel(bool showScores)
        {
            BlackjackStatisticModel statistic = _service.GetStatistic();
            bool isGameExists = _service.IsGameExiststs();
            BlackjackGameModel model = new BlackjackGameModel(Game, Player, statistic);
            model.ShowScore = showScores;
            model.ShowBanner = !isGameExists;
            return model;
        }

        private ActionResult Json(BlackjackGameModel model)
        {
            BlackjackGameJson json = new BlackjackGameJson(model);
            return Json(json);
        }

        #endregion
    }
}
