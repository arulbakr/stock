using System.Collections.Generic;
using Stock.Entities;

namespace Stock.Adapter.Funding
{
    public interface IFundingAdapter
    {
        /// <summary>
        /// Method retrieves all user deposit transactions.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of deposits</returns>
        IList<UserDepositEntity> GetUserDeposits(int userId);

        /// <summary>
        /// Method retrieves all user withdrawal transactions.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of transactions</returns>
        IList<UserWithdrawalEntity> GetUserWithdrawals(int userId);
    }
}