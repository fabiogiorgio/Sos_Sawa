using ExtraFunction.DTO;
using ExtraFunction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Repository.Interface
{
    public interface IBubbleMessageService
    {
        public Task CreateBubbleMessage(CreateBubbleMessageDTO message);
        public Task DeleteBubbleMessage(Guid messageId);
        public Task<BubbleMessage> GetRandomBubbleMessage();
        public Task<BubbleMessage> GetBubbleMessageById(Guid messageId);
        public Task<IEnumerable<BubbleMessage>> GetListOfRandomBubbleMessages(uint limit);
    }
}
