using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using traffic_app.BLL.Services.IServices;
using traffic_app.Core.Utility;
using traffic_app.DTO;

namespace traffic_app.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnTheWayController : ControllerBase
    {
        private readonly IOnTheWayDriverPostService _onTheWayDriverPostService;
        private readonly IUtil _util;
        private readonly IHostingEnvironment _environment;

        public OnTheWayController(IOnTheWayDriverPostService onTheWayDriverPostService, IUtil util, IHostingEnvironment environment)
        {
            _onTheWayDriverPostService = onTheWayDriverPostService;
            _util = util;
            _environment = environment;
        }

        [HttpGet("getDriverPosts")]
        public async Task<IActionResult> GetDriverPosts()
        {
            try
            {
                string tokenString = HttpContext.Request.Headers[Constants.AuthorizationHeaderName].ToString();

                PaginationDTO paginationDTO = new PaginationDTO()
                {
                    PageNumber = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageNumberHeaderName]),
                    PageSize = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageSizeHeaderName])
                };
                return Ok(await _onTheWayDriverPostService.GetDriverPosts(paginationDTO, _util.getUserIdFromToken(tokenString)));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpPost("filterDriverPosts")]
        public async Task<IActionResult> FilterDriverPosts(OnTheWayDriverPostToFilterDTO onTheWayDriverPostToFilterDTO)
        {
            try
            {
                string tokenString = HttpContext.Request.Headers[Constants.AuthorizationHeaderName].ToString();

                PaginationDTO paginationDTO = new PaginationDTO()
                {
                    PageNumber = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageNumberHeaderName]),
                    PageSize = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageSizeHeaderName])
                };
                return Ok(await _onTheWayDriverPostService.FilterDriverPosts(onTheWayDriverPostToFilterDTO ,paginationDTO, _util.getUserIdFromToken(tokenString)));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpGet("getDriverOwnPosts")]
        public async Task<IActionResult> GetDriverOwnPosts()
        {
            try
            {
                string tokenString = HttpContext.Request.Headers[Constants.AuthorizationHeaderName].ToString();

                PaginationDTO paginationDTO = new PaginationDTO()
                {
                    PageNumber = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageNumberHeaderName]),
                    PageSize = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageSizeHeaderName])
                };
                return Ok(await _onTheWayDriverPostService.GetDriverOwnPosts(paginationDTO, _util.getUserIdFromToken(tokenString)));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpGet("getDriverPostById/{postId}")]
        public async Task<IActionResult> GetDriverPost(int postId)
        {
            try
            {
                return Ok(await _onTheWayDriverPostService.GetDriverPostById(postId));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpPost("addDriverPost")]
        public async Task<IActionResult> CreatePost([FromForm]OnTheWayDriverPostToAddDTO onTheWayDriverPostToAddDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Messages.InvalidModel);
                }
                string filePath = null;
                if (onTheWayDriverPostToAddDTO.file != null)
                {
                    string fileExtension = onTheWayDriverPostToAddDTO.file.FileName.Substring(onTheWayDriverPostToAddDTO.file.FileName.LastIndexOf('.') + 1);
                    if (!Constants.ValidImageFileFormats.Contains(fileExtension.ToLower()))
                    {
                        return BadRequest(Messages.InvalidImageFileFormat);
                    }
                    Guid guid = Guid.NewGuid();
                    var uploads = Path.Combine(_environment.WebRootPath, "posts");
                    string fileName = guid.ToString() + onTheWayDriverPostToAddDTO.file.FileName;
                    filePath = "/posts/" + fileName;

                    if (onTheWayDriverPostToAddDTO.file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            onTheWayDriverPostToAddDTO.file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            Bitmap bmp = new Bitmap(ms);
                            _util.VaryQualityLevel(bmp, "wwwroot" + filePath);
                        }
                    }
                }
                string tokenString = HttpContext.Request.Headers[Constants.AuthorizationHeaderName].ToString();
                await _onTheWayDriverPostService.AddDriverPost(onTheWayDriverPostToAddDTO, filePath, _util.getUserIdFromToken(tokenString));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpPut("updateDriverPost")]
        public async Task<IActionResult> UpdatePost([FromForm] OnTheWayDriverPostToUpdateDTO onTheWayDriverPostToUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Messages.InvalidModel);
                }
                string filePath = null;
                if (onTheWayDriverPostToUpdateDTO.file != null)
                {
                    string fileExtension = onTheWayDriverPostToUpdateDTO.file.FileName.Substring(onTheWayDriverPostToUpdateDTO.file.FileName.LastIndexOf('.') + 1);
                    if (!Constants.ValidImageFileFormats.Contains(fileExtension.ToLower()))
                    {
                        return BadRequest(Messages.InvalidImageFileFormat);
                    }
                    Guid guid = Guid.NewGuid();
                    var uploads = Path.Combine(_environment.WebRootPath, "posts");
                    string fileName = guid.ToString() + onTheWayDriverPostToUpdateDTO.file.FileName;
                    filePath = "/posts/" + fileName;

                    if (onTheWayDriverPostToUpdateDTO.file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            onTheWayDriverPostToUpdateDTO.file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            Bitmap bmp = new Bitmap(ms);
                            _util.VaryQualityLevel(bmp, "wwwroot" + filePath);
                        }
                    }
                }
                string tokenString = HttpContext.Request.Headers[Constants.AuthorizationHeaderName].ToString();
                await _onTheWayDriverPostService.UpdateDriverPost(onTheWayDriverPostToUpdateDTO, filePath, _util.getUserIdFromToken(tokenString));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpDelete("deleteDriverPost/{postId}")]
        public async Task<IActionResult> DeleteDriverPost(int postId)
        {
            try
            {
                await _onTheWayDriverPostService.DeleteDriverPost(postId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }
    }
}
