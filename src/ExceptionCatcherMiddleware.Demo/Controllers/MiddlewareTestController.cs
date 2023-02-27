using Microsoft.AspNetCore.Mvc;

namespace ExceptionCatcherMiddleware.Demo.Controllers;

[ApiController]
public class MiddlewareTestController : ControllerBase
{
    [HttpGet]
    [Route("exception")]
    public ActionResult ThrowException(string exceptionMessage)
    {
        throw new Exception(exceptionMessage);
    }
    
    [HttpGet]
    [Route("argumentException")]
    public ActionResult ThrowArgumentException(string exceptionMessage)
    {
        throw new ArgumentException(exceptionMessage);
    }

    [HttpGet]
    [Route("argumentOutOfRangeException")]
    public ActionResult ThrowArgumentOutOfRangeException(string exceptionMessage)
    {
        throw new ArgumentOutOfRangeException(exceptionMessage);
    }
}