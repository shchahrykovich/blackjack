using System;
using System.Web.Mvc;
using Blackjack.Domain.Game.Cards;
using EnsureUtility;

namespace Blackjack.Web.Infrastructure
{
    public static class HtmlHelpers
    {
        private const double CardHeight = 98;
        private const double CardWidth = 73.0769;

        public static MvcHtmlString Card(this HtmlHelper helper, Card card)
        {
            Ensure.NotNull(card, "card");

            String style = GetSryle(card);

            TagBuilder builder = new TagBuilder("div");
            builder.MergeAttribute("class", "card");
            builder.MergeAttribute("style", style);

            MvcHtmlString result = new MvcHtmlString(builder.ToString());
            return result;
        }

        private static String GetSryle(Card card)
        {
            double top = ((int) card.Suit - 1)*CardHeight;
            double left = ((int) card.Type - 1)*CardWidth;

            const String template = "background-position: -{0:0.}px -{1:0.}px;";
            String result = String.Format(template, left, top);

            return result;
        }
    }
}