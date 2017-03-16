using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Stock.Adapter.Common;
using Stock.Adapter.Repositories;
using Stock.Common;
using Stock.Data;
using Stock.Entities;

namespace Stock.Adapter.Profile
{
    public class ProfileAdapter : IProfileAdapter
    {
        /// <summary>
        /// Method gets user details for given user id.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User details</returns>
        public UserDetailEntity GetUserDetails(int userId)
        {
            UserDetailEntity userDetailEntity;
            try
            {
                using (var stockDbEntities = new StockDBEntities())
                {
                    IRepository<user> userRepository = new Repository<user, DbContext>(stockDbEntities);
                    userDetailEntity = GetUserDetails(userId, userRepository);
                }
            }
            catch (Exception e)
            {
                // TODO: Logger to be added
                throw;
            }
            return userDetailEntity;
        }

        /// <summary>
        /// Method gets user details for given user id.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="userRepository">User repository</param>
        /// <returns>User details</returns>
        public UserDetailEntity GetUserDetails(int userId, IRepository<user> userRepository)
        {
            UserDetailEntity userDetailEntity = null;
            Expression<Func<user, bool>> predicate =
                userData => userData.UserID == userId && userData.ActiveIndicator == Constants.DbActive
                            && userData.userdetail != null;
            IQueryable<user> userList = userRepository.FindBy(predicate);
            if (userList.Any())
            {
                user userData = userList.First();
                userDetailEntity = new UserDetailEntity
                {
                    UserId = userData.UserID,
                    AnnualIncome = userData.userdetail.AnnualIncome,
                    EducationLevel = userData.userdetail.EducationLevel,
                    EmploymentStatus = userData.userdetail.EmploymentStatus,
                    NetWorth = userData.userdetail.NetWorth,
                    SourceOfFunds = userData.userdetail.SourceOfFunds,
                    UserDetailId = userData.userdetail.UserDetailId
                };
            }
            return userDetailEntity;
        }

        /// <summary>
        /// Method updates user details.
        /// </summary>
        /// <param name="userDetailEntity">User details</param>
        /// <returns>1 or 0</returns>
        public int UpdateUserDetails(UserDetailEntity userDetailEntity)
        {
            int result;
            try
            {
                using (var stockDbEntities = new StockDBEntities())
                {
                    IRepository<user> userRepository = new Repository<user, DbContext>(stockDbEntities);
                    result = UpdateUserDetails(userDetailEntity, userRepository);
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
        /// Method updates user details.
        /// </summary>
        /// <param name="userDetailEntity">User Detail Entity</param>
        /// <param name="userRepository">User repository</param>
        /// <returns>1 or -1</returns>
        public int UpdateUserDetails(UserDetailEntity userDetailEntity, IRepository<user> userRepository)
        {
            int result;
            IQueryable<user> userList = userRepository.FindBy(UtilityAdapter.BuildUserExistPredicate(userDetailEntity.UserId));
            user userDetailData = userList.First();
            //No records for given userid
            if (userDetailData == null)
            {
                return Constants.MinusOne;
            }
            if (userDetailData.userdetail != null)
            {
                userDetailData.userdetail.AnnualIncome = userDetailEntity.AnnualIncome;
                userDetailData.userdetail.DateModified = DateTime.Now;
                userDetailData.userdetail.EducationLevel = userDetailEntity.EducationLevel;
                userDetailData.userdetail.EmploymentStatus = userDetailEntity.EmploymentStatus;
                userDetailData.userdetail.NetWorth = userDetailEntity.NetWorth;
                userDetailData.userdetail.SourceOfFunds = userDetailEntity.SourceOfFunds;
                result = userRepository.Update(userDetailData);
            }
            else
            {
                // Inserting new record in UserDetails table
                userDetailData.userdetail = new userdetail
                {
                    ActiveIndicator = Constants.DbActive,
                    AnnualIncome = userDetailEntity.AnnualIncome,
                    DateCreated = DateTime.Now,
                    EducationLevel = userDetailEntity.EducationLevel,
                    EmploymentStatus = userDetailEntity.EmploymentStatus,
                    NetWorth = userDetailEntity.NetWorth,
                    SourceOfFunds = userDetailEntity.SourceOfFunds,
                };
                // Updating Users table with recent UserDetailID
                result = userRepository.Update(userDetailData);
            }
            return result;
        }

        /// <summary>
        /// Method creates bank accout information.
        /// </summary>
        /// <param name="bankInfoEntity">Bank info entity</param>
        /// <returns>1 or 0</returns>
        public int CreateBankInfo(BankInfoEntity bankInfoEntity)
        {
            int result;
            try
            {
                using (var stockDbEntities = new StockDBEntities())
                {
                    IRepository<userbank> userBankRepository = new Repository<userbank, DbContext>(stockDbEntities);
                    result = CreateBankInfo(bankInfoEntity, userBankRepository);
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
        /// Method creates bank accout information.
        /// </summary>
        /// <param name="bankInfoEntity">Bank info entity</param>
        /// <param name="userBankRepository">User bank repository</param>
        /// <returns>1 or 0</returns>
        public int CreateBankInfo(BankInfoEntity bankInfoEntity, IRepository<userbank> userBankRepository)
        {
            return userBankRepository.Add(new userbank
            {
                AccountNo = bankInfoEntity.AccountNo,
                ActiveIndicator = Constants.DbActive,
                Address = bankInfoEntity.Address,
                CountryId = bankInfoEntity.CountryId,
                DateCreated = DateTime.Now,
                IBAN = bankInfoEntity.IBAN,
                Name = bankInfoEntity.Name,
                SecurityPassword = bankInfoEntity.Password,
                SwiftCode = bankInfoEntity.SwiftCode,
                UserId = bankInfoEntity.UserId,
                DocumentImage = bankInfoEntity.DocumentImage
            });
        }
    }
}