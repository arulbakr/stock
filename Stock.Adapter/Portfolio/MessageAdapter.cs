using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Stock.Adapter.Repositories;
using Stock.Common;
using Stock.Data;
using Stock.Entities;

namespace Stock.Adapter.Portfolio
{
    public class MessageAdapter : IMessageAdapter
    {
        /// <summary>
        /// Method provides messages for given user id.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of messages</returns>
        public IList<MessageEntity> GetMessages(int userId)
        {
            IList<MessageEntity> resultEntities;
            try
            {
                using (var stockDbEntities = new StockDBEntities())
                {
                    //Query data from db
                    IRepository<message> messageRepository = new Repository<message, DbContext>(stockDbEntities);
                    resultEntities = GetMessages(userId, messageRepository);
                }
            }
            catch (Exception e)
            {
                // TODO: Logger to be added
                throw;
            }
            return resultEntities;
        }

        /// <summary>
        /// Method provides messages for given user id.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="messageRepository">Message repository</param>
        /// <returns>List of messages</returns>
        public IList<MessageEntity> GetMessages(int userId, IRepository<message> messageRepository)
        {
            var resultEntities = new List<MessageEntity>();
            IQueryable<message> announcements = messageRepository.FindBy(data => data.UserId == userId
                                                                                 && data.ActiveIndicator == Constants.DbActive);
            //Map db data into entities
            announcements.ToList().ForEach(dbRow => resultEntities.Add(new MessageEntity
            {
                ActiveIndicator = dbRow.ActiveIndicator,
                Description = dbRow.Description,
                MessageId = dbRow.MessageId,
                UserId = dbRow.UserId
            }));
            return resultEntities;
        }
    }
}