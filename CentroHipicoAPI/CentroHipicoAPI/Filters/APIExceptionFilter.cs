using CentroHipicoAPI.Nucleo.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CentroHipicoAPI.Filters
{
    public class APIExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            string msg = "Algo salió mal. Error interno del servidor.";
            context.ExceptionHandled = true;

            if (ex != null)
            {
                if (ex.Message != null && ex.Message.Length > 0) msg = ex.Message;
            }

            var error = new DTOBase()
            {
                StatusCode = 500,
                Success = false,
                Message = msg
            };

            context.Result = new BadRequestObjectResult(error);
            base.OnException(context);
        }
    }
}
