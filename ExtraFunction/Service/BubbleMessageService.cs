using ExtraFunction.DTO;
using ExtraFunction.Model;
using ExtraFunction.Repository.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Service
{
    public class BubbleMessageService : IBubbleMessageService
    {
        private IBubbleMessageRepository bubbleMessageRepository;

        public BubbleMessageService(IBubbleMessageRepository scheduleRepository)
        {
            this.bubbleMessageRepository = scheduleRepository;
        }

        public async Task CreateBubbleMessage(CreateBubbleMessageDTO message)
        {
            if (message.Message.IsNullOrEmpty())
                throw new Exception("Message cannot be empty");
            await bubbleMessageRepository.CreateBubbleMessage(message);
        }

        public async Task DeleteBubbleMessage(Guid messageId)
        {
            await bubbleMessageRepository.DeleteBubbleMessage(messageId);
        }

        public async Task<BubbleMessage> GetBubbleMessageById(Guid messageId)
        {
            if (!await bubbleMessageRepository.CheckIfMessageExist(messageId))
            {
                throw new Exception($"Bubble message with the message id {messageId} doesn't exist");
            }
            return await bubbleMessageRepository.GetBubbleMessageById(messageId);

        }

        public async Task<IEnumerable<BubbleMessage>> GetListOfRandomBubbleMessages(uint limit)
        {
            return await bubbleMessageRepository.GetListOfRandomBubbleMessages(limit);
        }

        public async Task<BubbleMessage> GetRandomBubbleMessage()
        {
            return await bubbleMessageRepository.GetRandomBubbleMessage();
        }
    }
}
