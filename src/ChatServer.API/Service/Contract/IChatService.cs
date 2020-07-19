using System.Collections.Generic;
using System.Threading.Tasks;
using ChatServer.API.Model;
using ChatServer.API.Model.ViewModels;

namespace ChatServer.API.Service.Contract {
    public interface IChatService {
        Task<Chat> GetChatInformation (string appId, string userId, string chatId);
        Task<Chat> CreateChat (string appId, string userId, ChatVM model);
        Task<List<ChatMember>> AddMembersToChat (string appId, ChatMembersVM model);
        Task<List<ChatMember>> GetChatMembers (string appId, string chatId);
        Task<ConversationFilteredResultVM> GetChatConversations (ChatHistoryFilterModel filter);
    }
}