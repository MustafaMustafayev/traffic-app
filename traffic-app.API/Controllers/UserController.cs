using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using traffic_app.BLL.Services.IServices;
using traffic_app.Core.Utility;
using traffic_app.DTO;

namespace traffic_app.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                return Ok(await _userService.GetUserById(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserToUpdateDTO userToUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Messages.InvalidModel);
                }
                if (await _userService.IsUserExist(userToUpdateDTO.CarNumber, userToUpdateDTO.PhoneNumber, userToUpdateDTO.UserId))
                {
                    return BadRequest(Messages.UserIsExist);
                }
                return Ok(await _userService.UpdateUser(userToUpdateDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserByI(int userId)
        {
            try
            {
                return Ok(await _userService.DeleteUser(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }
    }
}
