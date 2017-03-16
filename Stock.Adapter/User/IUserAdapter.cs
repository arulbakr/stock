using Stock.Entities;

namespace Stock.Adapter.User
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserAdapter
    {
        /// <summary>
        /// Method retreives user object from database based on given parameters
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">Password</param>
        /// <returns>User object</returns>
        UserEntity GetUser(string userName, string password);

        /// <summary>
        /// Method helps to get user profile.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User entity</returns>
        UserEntity GetUserProfile(int userId);

        /// <summary>
        /// Method updates user profile details.
        /// </summary>
        /// <param name="userEntity">User entity</param>
        /// <returns>true or false</returns>
        bool UpdateUserProfile(UserEntity userEntity);

        /// <summary>
        /// Method updates password with newer one.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="newPassword">New passowrd</param>
        /// <param name="oldPassword">Old password</param>
        /// <returns>1 or -1</returns>
        int ChangePassword(int userId, string newPassword, string oldPassword);

        /// <summary>
        /// Method checks if regiatration code is available or not.
        /// </summary>
        /// <param name="registrationCode">Entered code</param>
        /// <returns>true or false</returns>
        bool CheckRegistration(string registrationCode);

        /// <summary>
        /// Method registers a new user into the system.
        /// </summary>
        /// <param name="userEntity">New user details</param>
        /// <returns>true or false</returns>
        bool RegisterUser(UserEntity userEntity);
    }
}