using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using SeviceAbstraction;
using System.Text;

namespace PresentationLayer.Attributes
{
    internal class CashAttribute(int durationInSeconds = 90) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // create cash key 
            string cashKey = CreateCashKey(context.HttpContext.Request);
            // search for this key 
            ICashService cashService = context.HttpContext.RequestServices.GetRequiredService<ICashService>();
            var cashValue = await cashService.GetAsync(cashKey);
            // if exist return response 
            if (cashValue is not null)
            {
                context.Result = new ContentResult
                {
                    Content = cashValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                };
                return;
            }
            // else invoke next 
            var executedContext = await next.Invoke();
            //set with cash value 
            if (executedContext.Result is OkObjectResult result)
            {
                await cashService.SetAsync(cashKey, result.Value!, TimeSpan.FromSeconds(durationInSeconds));
            }
        }

        private string CreateCashKey(HttpRequest request)
        {
            StringBuilder key = new StringBuilder();
            key.Append(request.Path + '?');
            foreach (var item in request.Query.OrderBy(q => q.Key))
            {
                key.Append($"{item.Key}={item.Value}&");
            }
            return key.ToString();
        }
    }
}
