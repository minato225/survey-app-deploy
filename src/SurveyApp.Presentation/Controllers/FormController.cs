using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Forms;
using SurveyApp.Application.Forms.Contracts;
using SurveyApp.Application.Users;

namespace SurveyApp.Presentation.Controllers;

[ApiController]
[Route("api/")]
public class FormController(
    IUserService userService, IFormService formService)
    : ControllerBase
{
    [Authorize]
    [HttpPost("create-form")]
    public async Task<IActionResult> CreateForm([FromBody] CreateFormRequest request)
    {
        var adminUserId = userService.GetUserId();
        if (adminUserId == null)
        {
            return BadRequest();
        }
        await formService.CreateFormAsync(request, (Guid)adminUserId);
        return Ok();
    }
}