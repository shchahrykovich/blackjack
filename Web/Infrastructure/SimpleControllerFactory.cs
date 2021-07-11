using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Blackjack.Web.Controllers;
using Blackjack.Web.Infrastructure.Services;

namespace Blackjack.Web.Infrastructure
{
    public class SimpleControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (null != controllerType && "BlackjackController" == controllerType.Name)
            {
                IBlackjackGameService service = new BlackjackGameService(requestContext.HttpContext.Session);
                BlackjackController controller = new BlackjackController(service);
                return controller;
            }
            else
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }
        }
    }
}