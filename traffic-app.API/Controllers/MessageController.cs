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
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IUtil _util;
        public MessageController(IMessageService messageService, IUtil util)
        {
            _messageService = messageService;
            _util = util;
        }

        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage(MessageToAddDTO messageToAddDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Core.Utility.Messages.InvalidModel);
                }
                string tokenString = HttpContext.Request.Headers[Constants.AuthorizationHeaderName].ToString();
                await _messageService.AddMessage(messageToAddDTO, _util.getUserIdFromToken(tokenString));
                return Ok(Messages.MessageAccepted);
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.GeneralError);
            }
        }
    }
}
