using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaManagementSystem.Infrastructure.Services.Interfaces;
using SpaManagementSystem.WebApi.RequestsModels.CustomerAccount;

namespace SpaManagementSystem.WebApi.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class CustomerAccountController : ControllerBase
    {
        private readonly ICustomerService _customerService;



        public CustomerAccountController(ICustomerService customerService)
        {
            _customerService = customerService;
        }



        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel registerModel)
        {
            if (registerModel == null)
                return BadRequest();

            await _customerService.RegisterAsync(registerModel.Email, registerModel.Password, registerModel.PhoneNumber, registerModel.FirstName,
                registerModel.LastName, registerModel.Gender, registerModel.DateOfBirth, registerModel.Preferences);

            return Created("/Account", null);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
                return BadRequest();

            var token = await _customerService.LoginAsync(loginModel.Email, loginModel.Password);

            return new JsonResult(token);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            var customer = await _customerService.GetAsync(Guid.Parse(User.Identity!.Name!));

            if (customer == null)
                return NotFound();

            return new JsonResult(customer);
        }
    }
}
