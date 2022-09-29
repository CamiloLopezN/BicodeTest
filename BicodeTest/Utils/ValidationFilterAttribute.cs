using BicodeTest.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace BicodeTest.Utils
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ResultStructure resultStructure = new ResultStructure();
            if (!context.ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var modelState in context.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                resultStructure.Message = $"Se ha presentado uno o mas errores: {JsonSerializer.Serialize(errors)}";
                resultStructure.State = 404;
                context.Result = new JsonResult(resultStructure);
            }
        }
    }
}
