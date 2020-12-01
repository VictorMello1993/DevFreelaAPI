using DevFreela.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace DevFreela.API.Filters
{
    public class ValidationFilter : IActionFilter
    {
        //Depois da API enviar a requisição
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        //Antes da API enviar a requisição
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.GetErrors();

                context.Result = new BadRequestObjectResult(errors);
            }
        }
    }
}
