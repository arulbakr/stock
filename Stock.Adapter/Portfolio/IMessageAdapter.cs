using System.Collections.Generic;
using Stock.Entities;

namespace Stock.Adapter.Portfolio
{
    public interface IMessageAdapter
    {
        /// <summary>
        /// Method provides messages for given user id.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of messages</returns>
        IList<MessageEntity> GetMessages(int userId);
    }
}