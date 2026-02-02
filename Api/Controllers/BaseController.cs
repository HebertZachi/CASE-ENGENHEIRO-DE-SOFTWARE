using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected ActionResult NotFoundMessage(string entityName = "Item")
        {
            return NotFound(new { message = $"{entityName} not found" });
        }

        protected ActionResult CreatedResponse<T>(string actionName, T entity)
        {
            return CreatedAtAction(actionName, new { id = entity?.GetType().GetProperty("Id")?.GetValue(entity) }, entity);
        }
    }
}
