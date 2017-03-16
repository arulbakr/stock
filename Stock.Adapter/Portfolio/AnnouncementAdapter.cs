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
    public class AnnouncementAdapter : IAnnouncementAdapter
    {
        /// <summary>
        /// Method provides annoucements.
        /// </summary>
        /// <returns>List of active announments</returns>
        public IList<AnnouncementEntity> GetAnnouncements()
        {
            IList<AnnouncementEntity> resultEntities;
            try
            {
                using (var stockDbEntities = new StockDBEntities())
                {
                    //Query data from db
                    IRepository<announcement> announceRepository =
                        new Repository<announcement, DbContext>(stockDbEntities);
                    resultEntities = GetAnnounments(announceRepository);
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
        /// Method provides annoucements.
        /// </summary>
        /// <param name="announceRepository">announcement repository</param>
        /// <returns>List of active announments</returns>
        public IList<AnnouncementEntity> GetAnnounments(IRepository<announcement> announceRepository)
        {
            List<AnnouncementEntity> resultEntities = new List<AnnouncementEntity>();
            IQueryable<announcement> announcements =
                announceRepository.FindBy(entity => entity.ActiveIndicator == Constants.DbActive);

            //Map db data into entities
            announcements.ToList().ForEach(dbRow => resultEntities.Add(new AnnouncementEntity
            {
                AnnouncementId = dbRow.AnnouncementID,
                Title = dbRow.Title,
                Description = dbRow.Description,
                DateCreated = dbRow.DateCreated,
                ActiveIndicator = dbRow.ActiveIndicator
            }));
            return resultEntities;
        }
    }
}