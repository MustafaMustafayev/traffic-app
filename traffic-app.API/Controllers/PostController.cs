using System;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IHostingEnvironment _environment;
        private readonly IPostImageService _postImageService;
        private readonly IUtil _util;
        public PostController(IPostService postService, IUtil util, IHostingEnvironment environment, IPostImageService postImageService)
        {
            _postService = postService;
            _environment = environment;
            _util = util;
            _postImageService = postImageService;
        }

        [HttpGet("getPostList")]
        public async Task<IActionResult> GetPostList()
        {
            try
            {
                string tokenString = HttpContext.Request.Headers[Constants.AuthorizationHeaderName].ToString();
                PaginationDTO paginationDTO = new PaginationDTO()
                {
                    PageNumber = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageNumberHeaderName]),
                    PageSize = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageSizeHeaderName])
                };
                return Ok(await _postService.GetPostList(_util.getUserIdFromToken(tokenString), paginationDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpGet("getUserPostList")]
        public async Task<IActionResult> GetUserPostList()
        {
            try
            {
                string tokenString = HttpContext.Request.Headers[Constants.AuthorizationHeaderName].ToString();
                PaginationDTO paginationDTO = new PaginationDTO()
                {
                    PageNumber = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageNumberHeaderName]),
                    PageSize = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageSizeHeaderName])
                };
                return Ok(await _postService.GetUserPostList(_util.getUserIdFromToken(tokenString), paginationDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpGet("searcPostByCarNumber/{carNumber}")]
        public async Task<IActionResult> SearcPostByCarNumber(string carNumber)
        {
            try
            {
                PaginationDTO paginationDTO = new PaginationDTO()
                {
                    PageNumber = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageNumberHeaderName]),
                    PageSize = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageSizeHeaderName])
                };
                string tokenString = HttpContext.Request.Headers[Constants.AuthorizationHeaderName].ToString();
                return Ok(await _postService.SearchPostByCarNumber(carNumber, _util.getUserIdFromToken(tokenString), paginationDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpPost("createPost")]
        public async Task<IActionResult> CreatePost(PostToAddDTO postToAddDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Messages.InvalidModel);
                }
                string tokenString = HttpContext.Request.Headers[Constants.AuthorizationHeaderName].ToString();         
                return Ok(await _postService.CreatePost(postToAddDTO, _util.getUserIdFromToken(tokenString)));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpPost("addImageToPost")]
        public async Task<IActionResult> AddImageToPost([FromForm]PostImageDTO postImageDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Messages.InvalidModel);
                }
                string fileExtension = postImageDTO.file.FileName.Substring(postImageDTO.file.FileName.LastIndexOf('.') + 1);
                if (!Constants.ValidImageFileFormats.Contains(fileExtension.ToLower()))
                {
                    return BadRequest(Messages.InvalidImageFileFormat);
                }

                Guid guid = Guid.NewGuid();
                var uploads = Path.Combine(_environment.WebRootPath, "posts");
                string fileName = guid.ToString() + postImageDTO.file.FileName;
                string filePath = "/posts/" + fileName;

                if (postImageDTO.file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        postImageDTO.file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        Bitmap bmp = new Bitmap(ms);
                        _util.VaryQualityLevel(bmp, "wwwroot"+filePath);
                    }
                }
                PostImageToAddDTO postImageToAddDTO = new PostImageToAddDTO()
                {
                    PostId = postImageDTO.PostId,
                    ImageUrl = filePath,
                    ImageFullName = fileName,
                    ImageExtension = postImageDTO.file.FileName.Substring(postImageDTO.file.FileName.LastIndexOf('.') + 1)
                };
                await _postImageService.AddPostImage(postImageToAddDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut("updatePost")]
        public async Task<IActionResult> UpdatePost(PostToUpdateDTO postToUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Messages.InvalidModel);
                }
                await _postService.UpdatePost(postToUpdateDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpDelete("deletePost/{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            try
            {   
                return Ok(await _postService.DeletePost(postId));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }
    }
}
