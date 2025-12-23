using Microsoft.AspNetCore.Mvc;
using CrasAPI.Common;
using CrasAPI.Services.Interfaces;
using static CrasAPI.Commons.CommonAttributes;
using static CrasAPI.Services.Results.UserResult;

namespace CrasAPI.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : BaseController
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [HasPermission("user_view")]
        public async Task<IActionResult> GetList()
        {
            var result = await _service.GetListAsync();

            if (!result.Success)
            {
                return result.Error switch
                {
                    UserError.RecordNotFound =>
                        NotFound(new { message = "No records found" }),
                    _ => BadRequest(new { message = "Internal Error" })
                };
            }

            return Ok(result.Users);
        }

        [HttpGet("{id}")]
        [HasPermission("user_view")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.Success)
            {
                return result.Error switch
                {
                    UserError.RecordNotFound =>
                        NotFound(new { message = "Record not found" }),
                    _ => BadRequest(new { message = "Internal Error" })
                };
            }
            return Ok(result.User);
        }
    }
}
