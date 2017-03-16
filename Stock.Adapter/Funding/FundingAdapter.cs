using System;
using System.Collections.Generic;
using Stock.Entities;

namespace Stock.Adapter.Funding
{
    public class FundingAdapter : IFundingAdapter
    {
        /// <summary>
        /// Method retrieves all user deposit transactions.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of deposits</returns>
        public IList<UserDepositEntity> GetUserDeposits(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method retrieves all user withdrawal transactions.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of transactions</returns>
        public IList<UserWithdrawalEntity> GetUserWithdrawals(int userId)
        {
            throw new NotImplementedException();
        }
    }
}