using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using traffic_app.BLL.Services.IServices;
using traffic_app.Core.Utility;
using traffic_app.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace traffic_app.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class OnTheWayPassengerPostController : ControllerBase
    {
        private readonly IOnTheWayPassengerPostService _onTheWayPassengerPostService;
        private readonly IUtil _util;
        public OnTheWayPassengerPostController(IOnTheWayPassengerPostService onTheWayPassengerPostService, IUtil util)
        {
            _onTheWayPassengerPostService = onTheWayPassengerPostService;
            _util = util;
        }

        [HttpGet("getPassengerPosts")]
        public async Task<IActionResult> GetPassengerPosts()
        {
            try
            {
                string tokenString = HttpContext.Request.Headers[Constants.AuthorizationHeaderName].ToString();

                PaginationDTO paginationDTO = new PaginationDTO()
                {
                    PageNumber = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageNumberHeaderName]),
                    PageSize = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageSizeHeaderName])
                };
                return Ok(await _onTheWayPassengerPostService.GetPassengerPosts(paginationDTO, _util.getUserIdFromToken(tokenString)));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpPost("filterPassengerPosts")]
        public async Task<IActionResult> FilterPassengerPosts(OnTheWayPassengerPostToFilterDTO onTheWayPassengerPostToFilterDTO)
        {
            try
            {
                string tokenString = HttpContext.Request.Headers[Constants.AuthorizationHeaderName].ToString();

                PaginationDTO paginationDTO = new PaginationDTO()
                {
                    PageNumber = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageNumberHeaderName]),
                    PageSize = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageSizeHeaderName])
                };
                return Ok(await _onTheWayPassengerPostService.FilterPassengerPosts(onTheWayPassengerPostToFilterDTO, paginationDTO, _util.getUserIdFromToken(tokenString)));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpGet("getPassengerOwnPosts")]
        public async Task<IActionResult> GetPassengerOwnPosts()
        {
            try
            {
                string tokenString = HttpContext.Request.Headers[Constants.AuthorizationHeaderName].ToString();

                PaginationDTO paginationDTO = new PaginationDTO()
                {
                    PageNumber = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageNumberHeaderName]),
                    PageSize = Convert.ToInt32(HttpContext.Request.Headers[Constants.PageSizeHeaderName])
                };
                return Ok(await _onTheWayPassengerPostService.GetPassengerOwnPosts(paginationDTO, _util.getUserIdFromToken(tokenString)));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpGet("getPassengerPostById/{postId}")]
        public async Task<IActionResult> GetPassengerPost(int postId)
        {
            try
            {
                return Ok(await _onTheWayPassengerPostService.GetPassengerPostById(postId));
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpPost("addPassengerPost")]
        public async Task<IActionResult> AddPassengerPost(OnTheWayPassengerPostToAddDTO  onTheWayPassengerPostToAddDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Messages.InvalidModel);
                }
                string tokenString = HttpContext.Request.Headers[Constants.AuthorizationHeaderName].ToString();
                await _onTheWayPassengerPostService.AddPassengerPost(onTheWayPassengerPostToAddDTO, _util.getUserIdFromToken(tokenString));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpPut("updateDriverPost")]
        public async Task<IActionResult> UpdatePost(OnTheWayPassengerPostToUpdateDTO onTheWayPassengerPostToUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Messages.InvalidModel);
                }                
                string tokenString = HttpContext.Request.Headers[Constants.AuthorizationHeaderName].ToString();
                await _onTheWayPassengerPostService.UpdatePasssengerPost(onTheWayPassengerPostToUpdateDTO, _util.getUserIdFromToken(tokenString));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }

        [HttpDelete("deletePassengerPost/{postId}")]
        public async Task<IActionResult> DeletePassengerPost(int postId)
        {
            try
            {
                await _onTheWayPassengerPostService.DeletePassengerPost(postId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }
    }
}
