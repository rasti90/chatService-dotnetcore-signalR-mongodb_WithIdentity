using System.Collections.Generic;
using System.Threading.Tasks;
using ChatServer.API.Model;
using ChatServer.API.Model.ViewModels;

namespace ChatServer.API.Service.Contract {
    public interface IUserService {
        Task<List<UserChatVM>> GetUserChats (string appId, string userId);
        Task<List<User>> GetUsers (string appId);
        Task<User> GetUserInformation (string appId, string userId);
    }
}