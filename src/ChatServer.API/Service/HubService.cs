using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatServer.API.Model;
using ChatServer.API.Model.Enum;
using ChatServer.API.Model.ViewModels;
using ChatServer.API.Repository.Contract;
using ChatServer.API.Service.Contract;
using MongoDB.Bson;

namespace ChatServer.API.Service {
    public class HubService : IHubService {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;

        public HubService (IUserRepository userRepository, IChatRepository chatRepository) {
            this._userRepository = userRepository;
            this._chatRepository = chatRepository;
        }

        public async Task<bool> MakeUserOnline (string appId, string userId, string connectionId, string access_token) {
            var connection = new Connection () { ConnectionId = connectionId, JWTToken = access_token };
            var activity = new Activity () { ActivityType = Model.Enum.ActivityType.getOnline, ConnectionId = connectionId, Date = DateTime.Now };
            var result = await _userRepository.AddActivityAndManageConnectionToUserAsync (userId, activity, connection);
            return result;
        }

        public async Task<bool> MakeUserOffline (string appId, string userId, string connectionId, string access_token) {
            var connection = await _userRepository.GetUserConnectionAsync (userId, connectionId);
            var activity = new Activity () { ActivityType = Model.Enum.ActivityType.getOffline, ConnectionId = connectionId, Date = DateTime.Now };
            var result =await _userRepository.AddActivityAndManageConnectionToUserAsync (userId, activity, connection);
            return result;
        }

        public async Task<bool> FindUserInChat (string appId, string chatId, string userId) {
            var chat = await _chatRepository.GetAsync (appId, chatId);
            if (chat != null) {
                if (chat.ChatMembers.Any (member => member.UserId == userId)) {
                    return true;
                }
            }
            return false;
        }

        public async Task<UserChatVM> GetChatHistoryAndDoAppropriateActions (string appId, string chatId, string userId) {
            var chat = await _chatRepository.GetAsync (appId, chatId);
            if (chat != null) {
                if (chat.ChatMembers.Any (member => member.UserId == userId)) {
                    var lastChatConversations = await _chatRepository.GetChatConversationsAsync (appId, chatId, 0, 100);

                    var unReadChatconversations = await _chatRepository.GetUnReadChatConversationsAsync (appId, chatId, userId);
                    foreach (var conversation in unReadChatconversations) {
                        await _chatRepository.AddReaderToConversationAsync (chatId, conversation.Id,
                            new ChatConversationReader () { UserId = userId, Date = DateTime.Now });
                    }
                    if (chat.ChatType == ChatType.Private) {
                        var otherUser = chat.ChatMembers.Find (chatMember => chatMember.UserId != userId);
                        var otherUserInfo = await _userRepository.GetAsync (appId, otherUser.UserId);
                        chat.Name = otherUserInfo.FullName;
                    }
                    return new UserChatVM () {
                        UserId = userId, Chat = new Chat { Id = chatId, Name = chat.Name, ChatConversations = lastChatConversations, ChatType = chat.ChatType }, NewMessagesCount = 0
                    };
                }
            }
            return null;
        }

        public async Task<ChatConversation> SendMessageToChat (string appId, string chatId, string userId, string message) {
            var user = await _userRepository.GetAsync(appId, userId);
            var chat = await _chatRepository.GetAsync (appId, chatId);
            if (chat != null) {
                var chatConversation = new ChatConversation () {
                Id = ObjectId.GenerateNewId ().ToString (),
                UserId = user.Id,
                Date = DateTime.Now,
                Text = message,
                ChatConversationReaders = new List<ChatConversationReader> (),
                User = new User () {
                    Id = user.Id,
                    FullName = user.FullName
                }
                };

                await _chatRepository.AddMessageToChatAsync (chatId, chatConversation);
                return chatConversation;
            }
            return null;
        }
    }
}