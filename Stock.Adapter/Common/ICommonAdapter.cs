using System.Collections.Generic;
using Stock.Entities;

namespace Stock.Adapter.Common
{
    public interface ICommonAdapter
    {
        /// <summary>
        /// Method provides countries list.
        /// </summary>
        /// <returns>List of countries</returns>
        IList<CountryEntity> GetCountries();

        /// <summary>
        /// Method saves document image and its details.
        /// </summary>
        /// <param name="document">Document entity</param>
        /// <returns>1 or 0</returns>
        int UploadDocument(DocumentEntity document);
    }
}