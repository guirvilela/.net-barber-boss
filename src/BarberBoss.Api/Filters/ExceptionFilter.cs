using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;
using BarberBoss.Infrastructure.DataAcess;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BarberBoss.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BarberBossException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknowError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var barberBossException = (BarberBossException)context.Exception;
        var errorResponse = new ResponseErrorJson(barberBossException!.GetErrors());

        context.HttpContext.Response.StatusCode = barberBossException.StatusCode;

        context.Result = new ObjectResult(errorResponse);
    }

    private void ThrowUnknowError(ExceptionContext context)
    {
        var message = context.Exception.Message;

#if DEBUG
        var errorResponse = new ResponseErrorJson(message);
#else
    var errorResponse = new ResponseErrorJson(ResourceErrors.UNKNOWN_ERROR);
#endif

        context.HttpContext.Response.StatusCode =
            StatusCodes.Status500InternalServerError;

        context.Result = new ObjectResult(errorResponse);
    }
}
