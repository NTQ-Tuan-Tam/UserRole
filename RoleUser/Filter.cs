using Microsoft.AspNetCore.Mvc.Filters;

namespace RoleUser
{
    public class Filter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new System.NotImplementedException();
        }

    }
}
