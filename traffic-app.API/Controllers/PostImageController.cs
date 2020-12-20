using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using traffic_app.BLL.Services.IServices;
using traffic_app.Core.Utility;

namespace traffic_app.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PostImageController : ControllerBase
    {
        private readonly IPostImageService _postImageService;
        public PostImageController(IPostImageService postImageService)
        {
            _postImageService = postImageService;
        }

        [HttpDelete("deletePostImage/{postImageId}")]
        public async Task<IActionResult> DeletePostImage(int postImageId)
        {
            try
            {
                return Ok(await _postImageService.DeletePostImage(postImageId));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }
    }
}
