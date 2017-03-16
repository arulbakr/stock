using System.Collections.Generic;
using Stock.Entities;

namespace Stock.Adapter.Portfolio
{
    public interface IAnnouncementAdapter
    {
        /// <summary>
        /// Method provides annoucements.
        /// </summary>
        /// <returns>List of active announments</returns>
        IList<AnnouncementEntity> GetAnnouncements();
    }
}