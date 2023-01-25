using AutoMapper;
using ExtraFunction.DAL;
using ExtraFunction.DTO;
using ExtraFunction.Model;
using ExtraFunction.Repository.Interface;
using ExtraFunction.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFunction.Repository
{
    public class BubbleMessageRepository : IBubbleMessageRepository
    {
        private DatabaseContext dbContext;
        Random rnd = new Random();

        public BubbleMessageRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateBubbleMessage(CreateBubbleMessageDTO message)
        {
            //map DTO to normal Bubble Message
            Mapper mapper = AutoMapperUtil.ReturnMapper(new MapperConfiguration(con => con.CreateMap<CreateBubbleMessageDTO, BubbleMessage>()));
            BubbleMessage bubbleMessage = mapper.Map<BubbleMessage>(message);
            dbContext.BubbleMessages.Add(bubbleMessage);
            await dbContext.SaveChangesAsync();
        }
        public async Task<bool> CheckIfMessageExist(Guid MessageId)
        {
            await dbContext.SaveChangesAsync();
            if (dbContext.BubbleMessages.Count(x => x.Id == MessageId) > 0)
                return true;
            else
                return false;
        }

        public async Task DeleteBubbleMessage(Guid messageId)
        {
            BubbleMessage bubbleMessage = null;

            //this is to give priority to tasks
            Task getId = Task.Run(() =>
            {
                bubbleMessage = GetBubbleMessageById(messageId).Result; // get message
            });
            await getId.ContinueWith(prev =>
            {
                dbContext.BubbleMessages?.Remove(bubbleMessage); // delete message

            });
            await dbContext.SaveChangesAsync();
        }

        public async Task<BubbleMessage> GetBubbleMessageById(Guid messageId)
        {
            await dbContext.SaveChangesAsync();
            return dbContext.BubbleMessages.FirstOrDefault(x => x.Id == messageId);
        }

        public async Task<IEnumerable<BubbleMessage>> GetListOfRandomBubbleMessages(uint limit)
        {
            await dbContext.SaveChangesAsync();
            return dbContext.BubbleMessages.OrderBy(x => x.Id).Take((int)limit).ToList();
        }

        public async Task<BubbleMessage> GetRandomBubbleMessage()
        {
            await dbContext.SaveChangesAsync();
            int skipper = rnd.Next(0, dbContext.BubbleMessages.Count());
            return await dbContext.BubbleMessages.Skip(skipper).FirstOrDefaultAsync();
        }
    }
}
