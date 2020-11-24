using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using traffic_app.BLL.Services.IServices;
using traffic_app.Core.Utility;
using traffic_app.DTO;

namespace traffic_app.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword(UserChangePasswordDTO userChangePasswordDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Messages.InvalidModel);
                }
                return Ok(await _userService.ChangePassword(userChangePasswordDTO));
            }
            catch (Exception ex)
            {
                if (ex.Message == ErrorCodes.UserIsExist)
                {
                    return BadRequest(Messages.UserIsExist);
                }
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(UserToUpdateDTO userToUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Messages.InvalidModel);
                }
                return Ok(await _userService.UpdateUser(userToUpdateDTO));
            }
            catch (Exception ex)
            {
                if (ex.Message == ErrorCodes.UserIsExist)
                {
                    return BadRequest(Messages.UserIsExist);
                }
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
