using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Stock.Adapter.Repositories;
using Stock.Common;
using Stock.Data;
using Stock.Entities;
using Stock.Adapter.Common;

namespace Stock.Adapter.User
{
    /// <summary>
    /// Class handles database operation w.r.to User
    /// </summary>
    public class UserAdapter : IUserAdapter
    {
        /// <summary>
        /// Method retreives user object from database based on given parameters
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">Password</param>
        /// <returns>User object</returns>
        public UserEntity GetUser(string userName, string password)
        {
            UserEntity userEntity;
            try
            {
                using (var stockDbEntities = new StockDBEntities())
                {
                    IRepository<user> userRepository = new Repository<user, DbContext>(stockDbEntities);
                    userEntity = GetUser(userName, password, userRepository);
                }
            }
            catch (Exception e)
            {
                // TODO: Logger to be added
                throw;
            }
            return userEntity;
        }

        /// <summary>
        /// Method retreives user object from database based on given parameters
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">Password</param>
        /// <param name="userRepository">User repository</param>
        /// <returns>User object</returns>
        public UserEntity GetUser(string userName, string password, IRepository<user> userRepository)
        {
            UserEntity userEntity = null;
            Expression<Func<user, bool>> predicate = user => user.Email.Equals(userName)
                                                             && user.Password.Equals(password)
                                                             && user.ActiveIndicator == Constants.DbActive;
            IQueryable<user> userList = userRepository.FindBy(predicate);
            if (userList.Any())
            {
                user tempUser = userList.First();
                userEntity = new UserEntity
                {
                    ActiveIndicator = tempUser.ActiveIndicator,
                    Email = tempUser.Email,
                    FullName = tempUser.FullName,
                    UserId = tempUser.UserID
                };
            }
            return userEntity;
        }

        /// <summary>
        /// Method helps to get user profile.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User entity</returns>
        public UserEntity GetUserProfile(int userId)
        {
            UserEntity user;
            try
            {
                using (var stockDbEntities = new StockDBEntities())
                {
                    IRepository<user> userRepository = new Repository<user, DbContext>(stockDbEntities);
                    user = GetUserProfile(userId, userRepository);
                }
            }
            catch (Exception e)
            {
                // TODO: Logger to be added
                throw;
            }
            return user;
        }

        /// <summary>
        /// Method helps to get user profile.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="userRepository">User repository</param>
        /// <returns>User entity</returns>
        public UserEntity GetUserProfile(int userId, IRepository<user> userRepository)
        {
            UserEntity userEntity = null;
            //Query data from db
            IQueryable<user> dbUsers = userRepository.FindBy(UtilityAdapter.BuildUserExistPredicate(userId));

            //Map db data into entities
            if (dbUsers.Any())
            {
                user tempUser = dbUsers.First();
                userEntity = new UserEntity
                {
                    UserId = tempUser.UserID,
                    ActiveIndicator = tempUser.ActiveIndicator,
                    Gender = tempUser.Gender,
                    PassportNo = tempUser.PassportNo,
                    State = tempUser.State,
                    City = tempUser.City,
                    ContactNo = tempUser.ContactNo,
                    PostalCode = tempUser.PostalCode,
                    Adddress = tempUser.Adddress,
                    DateOfBirth = tempUser.DateOfBirth,
                    Email = tempUser.Email,
                    FullName = tempUser.FullName,
                };
                if (tempUser.country != null)
                {
                    userEntity.Country = new CountryEntity
                    {
                        CountryId = tempUser.country.CountryID,
                        Name = tempUser.country.Name
                    };
                }
            }
            return userEntity;
        }

        

        /// <summary>
        /// Method updates user profile details.
        /// </summary>
        /// <param name="userEntity">User entity</param>
        /// <returns>true or false</returns>
        public bool UpdateUserProfile(UserEntity userEntity)
        {
            bool result;
            try
            {
                using (var stockDbEntities = new StockDBEntities())
                {
                    IRepository<user> userRepository = new Repository<user, DbContext>(stockDbEntities);
                    result = UpdateUserProfile(userEntity, userRepository);
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
        /// Method updates user profile details.
        /// </summary>
        /// <param name="userEntity">User entity</param>
        /// <param name="userRepository">User repository</param>
        /// <returns>true or false</returns>
        public bool UpdateUserProfile(UserEntity userEntity, IRepository<user> userRepository)
        {
            IQueryable<user> userList = userRepository.FindBy(UtilityAdapter.BuildUserExistPredicate(userEntity.UserId));
            //Updating new changes
            if (userList.Any())
            {
                user userData = userList.First();
                userData.Adddress = userEntity.Adddress;
                userData.City = userEntity.City;
                userData.ContactNo = userEntity.ContactNo;
                userData.DateOfBirth = userEntity.DateOfBirth;
                userData.Email = userEntity.Email;
                userData.FullName = userEntity.FullName;
                userData.Gender = userEntity.Gender;
                userData.PassportNo = userEntity.PassportNo;
                userData.PostalCode = userEntity.PostalCode;
                userData.State = userEntity.State;
                userData.CountryId = userEntity.Country != null ? userEntity.Country.CountryId : userData.CountryId;
                userData.DateModified = DateTime.Now;
                return userRepository.Update(userData) > 0;
            }
            return false;
        }

        /// <summary>
        /// Method updates password with newer one.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="newPassword">New passowrd</param>
        /// <param name="oldPassword">Old password</param>
        /// <returns>1 or -1</returns>
        public int ChangePassword(int userId, string newPassword, string oldPassword)
        {
            int result;
            try
            {
                using (var stockDbEntities = new StockDBEntities())
                {
                    IRepository<user> userRepository = new Repository<user, DbContext>(stockDbEntities);
                    result = ChangePassowrd(userId, newPassword, oldPassword, userRepository);
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
        /// Method updates password with newer one.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="newPassword">New passowrd</param>
        /// <param name="oldPassword">Old passowrd</param>
        /// <param name="userRepository">User repository</param>
        /// <returns>1 or 0</returns>
        public int ChangePassowrd(int userId, string newPassword, string oldPassword, IRepository<user> userRepository)
        {
            IQueryable<user> userList = userRepository.FindBy(UtilityAdapter.BuildUserExistPredicate(userId));
            //Updating new changes
            if (userList.Any())
            {
                user userData = userList.First();
                if (userData.Password == oldPassword)
                {
                    userData.Password = newPassword;
                    userData.DateModified = DateTime.Now;
                    return userRepository.Update(userData);
                }
                return Constants.MinusOne;
            }
            return Constants.Zero;
        }

        /// <summary>
        /// Method checks if regiatration code is available or not.
        /// </summary>
        /// <param name="registrationCode">Entered code</param>
        /// <returns>true or false</returns>
        public bool CheckRegistration(string registrationCode)
        {
            bool result;
            try
            {
                using (var stockDbEntities = new StockDBEntities())
                {
                    IRepository<userregistration> userRepository = new Repository<userregistration, DbContext>(stockDbEntities);
                    result = CheckRegistration(registrationCode, userRepository);
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
        /// Method checks if regiatration code is available or not.
        /// </summary>
        /// <param name="registrationCode">Entered code</param>
        /// <param name="userRepository">User repository</param>
        /// <returns>true or false</returns>
        public bool CheckRegistration(string registrationCode, IRepository<userregistration> userRepository)
        {
            return userRepository.IsExist(code => code.Code == registrationCode && code.ActiveIndicator == Constants.DbActive);
        }

        /// <summary>
        /// Method registers a new user into the system.
        /// </summary>
        /// <param name="userEntity">New user details</param>
        /// <returns>true or false</returns>
        public bool RegisterUser(UserEntity userEntity)
        {
            bool result;
            using (var stockDbEntities = new StockDBEntities())
            {
                IRepository<user> userRepository = new Repository<user, DbContext>(stockDbEntities);
                result = RegisterUser(userEntity, userRepository);
            }
            return result;
        }

        /// <summary>
        /// Method registers a new user into the system.
        /// </summary>
        /// <param name="userEntity">New user details</param>
        /// <param name="userRepository">User repository</param>
        /// <returns>true or false</returns>
        public bool RegisterUser(UserEntity userEntity, IRepository<user> userRepository)
        {
            int? cId = null;
            //Registering new user
            user userData = new user
            {
                Adddress = userEntity.Adddress,
                City = userEntity.City,
                ContactNo = userEntity.ContactNo,
                DateOfBirth = userEntity.DateOfBirth,
                Email = userEntity.Email,
                FullName = userEntity.FullName,
                Gender = userEntity.Gender,
                PassportNo = userEntity.PassportNo,
                PostalCode = userEntity.PostalCode,
                State = userEntity.State,
                CountryId = userEntity.Country != null && userEntity.Country.CountryId > 0
                    ? userEntity.Country.CountryId
                    : cId,
                SecurityQuestion = userEntity.SecurityQuestion,
                SecurityAnswer = userEntity.SecurityAnswer,
                Password = userEntity.Password,
                DateCreated = DateTime.Now,
                ActiveIndicator = Constants.DbActive
            };
            return userRepository.Add(userData) > 0;
        }
    }
}