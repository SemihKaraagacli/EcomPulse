using EcomPulse.Service.UserService;
using EcomPulse.Service.UserService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{
    public class UserController(IUserService userService) : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateRequest request)
        {
            var result = await userService.SignUp(request);
            return CreateObjectResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await userService.GetAllUser();
            return CreateObjectResult(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateRequest request)
        {
            var result = await userService.UserUpdate(request);
            return CreateObjectResult(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await userService.GetByIdUser(id);
            return CreateObjectResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await userService.DeleteUser(id);
            return CreateObjectResult(result);
        }
    }
}
