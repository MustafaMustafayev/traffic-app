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
    }
}
