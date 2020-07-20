using System.Collections.Generic;
using System.Threading.Tasks;
using ChatServer.API.Model;

namespace ChatServer.API.Repository.Contract {
    public interface IUserRepository {
        List<User> GetByAppId (string appId);
        Task<List<User>> GetByAppIdAsync (string appId);
        User Get (string appId, string userId);
        Task<User> GetAsync (string appId, string userId);
        User GetByExternalId (string appId, string externalId);
        Task<User> GetByExternalIdAsync (string appId, string externalId);
        User GetByConnectionId (string connectionId);
        Task<User> GetByConnectionIdAsync (string connectionId);
        Task<Connection> GetUserConnectionAsync (string userId, string connectionId);
        User Create (User user);
        Task<User> CreateAsync (User user);
        void Update (string id, User userIn);
        Task UpdateAsync (string id, User userIn);
        Task updateFullNameAsync (string userId, string fullName);
        void AddActivityAndManageConnectionToUser (string userId, Activity activity, Connection connection);
        Task<bool> AddActivityAndManageConnectionToUserAsync (string userId, Activity activity, Connection connection);
        void Remove (User userIn);
        Task RemoveAsync (User userIn);
        void Remove (string id);
        Task RemoveAsync (string id);
    }
}