using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using traffic_app.BLL.Services.IServices;
using traffic_app.DAL.Repositories.IRepositories;
using traffic_app.DTO;
using traffic_app.Entity.Entities;

namespace traffic_app.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task AddMessage(MessageToAddDTO messageToAddDTO, int userId)
        {
            Message message = _mapper.Map<Message>(messageToAddDTO);
            message.UserId = userId;
            message.CreatedAt = DateTime.Now;
            await _messageRepository.Add(message);
        }
    }
}
