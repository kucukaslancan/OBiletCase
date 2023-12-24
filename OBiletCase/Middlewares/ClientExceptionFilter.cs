using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OBiletCase.Exceptions;
using Vereyon.Web;

namespace OBiletCase.Middlewares
{
    public class ClientExceptionFilter : IExceptionFilter
    {
        private readonly IFlashMessage _flashMessage;

        public ClientExceptionFilter(IFlashMessage flashMessage)
        {
            _flashMessage = flashMessage;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ClientException)
            {
                ClientException ex = (ClientException)context.Exception;
                string? message = ex.GetClientMessage();
                _flashMessage.Warning(message);

                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "Index" }
                });

            }
        }
    }
}
