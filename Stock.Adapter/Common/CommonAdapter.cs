using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Stock.Adapter.Repositories;
using Stock.Common;
using Stock.Data;
using Stock.Entities;

namespace Stock.Adapter.Common
{
    /// <summary>
    /// Class for common operations related to application.
    /// </summary>
    public class CommonAdapter : ICommonAdapter
    {
        /// <summary>
        /// Method provides countries list.
        /// </summary>
        /// <returns>List of countries</returns>
        public IList<CountryEntity> GetCountries()
        {
            IList<CountryEntity> countries;
            try
            {
                using (var stockDbEntities = new StockDBEntities())
                {
                    IRepository<country> countryRepository = new Repository<country, DbContext>(stockDbEntities);
                    countries = GetCountries(countryRepository);
                }
            }
            catch (Exception e)
            {
                // TODO: Logger to be added
                throw;
            }
            return countries;
        }

        /// <summary>
        /// Method provides countries list.
        /// </summary>
        /// <param name="countryRepository">Country repository</param>
        /// <returns>List of countries</returns>
        public IList<CountryEntity> GetCountries(IRepository<country> countryRepository)
        {
            var countries = new List<CountryEntity>();
            IQueryable<country> countryData = countryRepository.GetAll();
            // Mapping data into app entity
            countryData.ToList().ForEach(country => countries.Add(new CountryEntity
            {
                Name = country.Name,
                CountryId = country.CountryID
            }));
            return countries;
        }

        /// <summary>
        /// Method saves document image and its details.
        /// </summary>
        /// <param name="document">Document entity</param>
        /// <returns>1 or 0</returns>
        public int UploadDocument(DocumentEntity document)
        {
            int result;
            try
            {
                using (var stockDbEntities = new StockDBEntities())
                {
                    IRepository<userdocument> documentRepository =
                        new Repository<userdocument, DbContext>(stockDbEntities);
                    result = UploadDocument(document, documentRepository);
                }
            }
            catch (Exception e)
            {
                // TODO: Logger to be added
                throw;
            }
            return result;
        }

        /// <summary>
        /// Method saves document image and its details.
        /// </summary>
        /// <param name="document">Document entity</param>
        /// <param name="documentRepository">Document repository</param>
        /// <returns>1 or 0</returns>
        public int UploadDocument(DocumentEntity document, IRepository<userdocument> documentRepository)
        {
            return documentRepository.Add(new userdocument
            {
                UserId = document.UserId,
                ActiveIndicator = Constants.DbActive,
                DateCreated = DateTime.Now,
                DocumentImage = document.DocumentImage,
                FileName = document.FileName,
                DocumentTypeId = document.DocumentTypeId
            });
        }
    }
}