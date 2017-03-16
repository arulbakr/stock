using System;
using System.Linq.Expressions;
using Stock.Common;
using Stock.Data;

namespace Stock.Adapter.Common
{
    /// <summary>
    /// Class defines methods for common utilities.
    /// </summary>
    public static class UtilityAdapter
    {
        /// <summary>
        /// Method builds and returns user exist predicate for common use.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Expression predicate</returns>
        public static Expression<Func<user, bool>> BuildUserExistPredicate(int userId)
        {
            Expression<Func<user, bool>> predicate =
                userData => userData.UserID == userId && userData.ActiveIndicator == Constants.DbActive;
            return predicate;
        }

        /// <summary>
        /// Method builds and returns user details exist predicate for common use.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Expression predicate</returns>
        public static Expression<Func<user, bool>> BuildUserDetailsExistPredicate(int userId)
        {
            Expression<Func<user, bool>> predicate =
                userData => userData.UserID == userId && userData.ActiveIndicator == Constants.DbActive
                    && userData.userdetail != null && userData.userdetail.ActiveIndicator == Constants.DbActive;
            return predicate;
        }
    }
}