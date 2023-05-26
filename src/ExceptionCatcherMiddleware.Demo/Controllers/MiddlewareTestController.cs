using Microsoft.AspNetCore.Mvc;

namespace ExceptionCatcherMiddleware.Demo.Controllers;

[ApiController]
public class MiddlewareTestController : ControllerBase
{
    [HttpGet]
    [Route("exception/message/{exceptionMessage}")]
    public ActionResult ThrowException(string exceptionMessage)
    {
        throw new Exception(exceptionMessage);
    }
    
    [HttpGet]
    [Route("argumentException/message/{exceptionMessage}")]
    public ActionResult ThrowArgumentException(string exceptionMessage)
    {
        throw new ArgumentException(exceptionMessage);
    }

    [HttpGet]
    [Route("argumentOutOfRangeException/message/{exceptionMessage}")]
    public ActionResult ThrowArgumentOutOfRangeException(string exceptionMessage)
    {
        throw new ArgumentOutOfRangeException(exceptionMessage);
    }
}