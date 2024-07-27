using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebMVC2.Filter
{
    public class ModelStateFilterAttribute : TypeFilterAttribute
    {
        public ModelStateFilterAttribute() : base(typeof(ModelStateFilter))
        {
        }
    }

    public class ModelStateFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {
                // 如果ModelState無效
                context.Result = new ViewResult();
            }
        }
    }
}
