using Stock.Entities;

namespace Stock.Adapter.Profile
{
    public interface IProfileAdapter
    {
        /// <summary>
        /// Method gets user details for given user id.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User details</returns>
        UserDetailEntity GetUserDetails(int userId);

        /// <summary>
        /// Method updates user details.
        /// </summary>
        /// <param name="userDetailEntity">User details</param>
        /// <returns>1 or 0</returns>
        int UpdateUserDetails(UserDetailEntity userDetailEntity);

        /// <summary>
        /// Method creates bank accout information.
        /// </summary>
        /// <param name="bankInfoEntity">Bank info entity</param>
        /// <returns>1 or 0</returns>
        int CreateBankInfo(BankInfoEntity bankInfoEntity);
    }
}