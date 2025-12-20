using Microsoft.AspNetCore.Mvc;
using Serilog.Events;

namespace CrasAPI.Common
{
    public abstract class BaseController : ControllerBase
    {
        protected void Log(
            LogEventLevel level,
            string message,
            Exception? ex = null,
            params object[] args
        )
        {
            var logger = Serilog.Log
                .ForContext("Controller", GetType().Name)
                .ForContext("Action", ControllerContext.ActionDescriptor.ActionName);

            if (ex != null)
                logger.Write(level, ex, message, args);
            else
                logger.Write(level, message, args);
        }
    }
}
