using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using UnitTests.DependencyModule;

namespace UnitTests.Controllers
{
    public class BaseControllerHelper 
    {
        public BaseControllerHelper()
        {
            UnitTestDependencyModule.Run();
        }

        protected static void AssertListItensAreEquals(List<SelectListItem> firstList, IEnumerable<SelectListItem> secondList)
        {
            firstList.ForEach(item => Assert.IsTrue(secondList.Any(SelectListItemEqualsCondition(item))));
        }

        protected static IEnumerable<SelectListItem> ViewTadaToListSelectListItem(object viewData)
        {
            return viewData as List<SelectListItem>;
        }

        protected static RedirectToRouteResult GetRedirectToRouteResultWith(ActionResult actionResult)
        {
            return (RedirectToRouteResult)actionResult;
        }

        protected static Func<SelectListItem, bool> SelectListItemEqualsCondition(SelectListItem item)
        {
            return
                a =>
                    a.Value == item.Value && a.Selected == item.Selected && a.Text == item.Text &&
                    a.Disabled == item.Disabled && a.Group == item.Group;
        }
    }
}
