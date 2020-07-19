using System.Threading.Tasks;
using ChatServer.API.Model;
using ChatServer.API.Model.ViewModels;

namespace ChatServer.API.Service.Contract {
    public interface IHubService {
        Task<bool> FindUserInChat (string appId, string chatId, string userId);
        Task<UserChatVM> GetChatHistoryAndDoAppropriateActions (string appId, string chatId, string userId);
        Task<ChatConversation> SendMessageToChat (string appId, string chatId, string userId, string message);
        Task<bool> MakeUserOnline (string appId, string userId, string connectionId, string access_token);
        Task<bool> MakeUserOffline (string appId, string userId, string connectionId, string access_token);
    }
}