using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ChatServer.API.Helper;
using ChatServer.API.Model;
using ChatServer.API.Repository.Contract;
using ChatServer.API.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;

namespace ChatServer.API.Hub {
    [Authorize]
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub {
        private readonly IHubService _hubService;
        private readonly IDistributedCache _distributedCache;
        private readonly IUserRepository _userRepository;
        public ChatHub (IHubService hubservice, IDistributedCache distributedCache, IUserRepository userRepository) {
            this._hubService = hubservice;
            this._distributedCache = distributedCache;
            this._userRepository = userRepository;
        }

        public async Task SendMessage (string user, string message) {
            await Clients.All.SendAsync ("ReceiveMessage", user, message);
        }

        public async Task AddToChat (string chatId) {
            try {
                var userInfo = await _distributedCache.GetObjectAsync<User>(Context.ConnectionId);

                var userChatInfo = _hubService.FindUserInChat (userInfo.AppId, chatId, userInfo.Id);
                if (userChatInfo != null) {
                    await Groups.AddToGroupAsync (Context.ConnectionId, chatId);
                }
            } catch {

            }
        }

        public async Task GetChatHistory (string chatId) {
            try {
                var userInfo = await _distributedCache.GetObjectAsync<User>(Context.ConnectionId);

                var userChatInfoWithLastConversationList = await _hubService.GetChatHistoryAndDoAppropriateActions (userInfo.AppId, chatId, userInfo.Id);
                if (userChatInfoWithLastConversationList != null) {
                    await Groups.AddToGroupAsync (Context.ConnectionId, chatId);
                    await Clients.Caller.SendAsync ("GetChatHistory", userChatInfoWithLastConversationList.Chat);
                    await Clients.Caller.SendAsync ("GetUnreadConversationsCount", userChatInfoWithLastConversationList);
                }
            } catch {

            }
        }

        public async Task SendMessageToChat (string chatId, string message) {
            try {
                var userInfo = await _distributedCache.GetObjectAsync<User>(Context.ConnectionId);

                var chatConversation = await _hubService.SendMessageToChat (userInfo.AppId, chatId, userInfo.Id, message);
                if (chatConversation != null) {
                    await Clients.Group (chatId).SendAsync ("ReceiveMessageFromChat", chatId, chatConversation);
                }
            } catch {

            }
        }

        public override async Task OnConnectedAsync () {
            try {
                var user = Context.User as ClaimsPrincipal;
                var userExternalId= user.GetClaimValue("client_userExternalId");

                var userInfo = await _userRepository.GetByExternalIdAsync(userExternalId);
                await _distributedCache.SetObjectAsync(Context.ConnectionId, userInfo);

                var httpContext = Context.GetHttpContext();
                
                var access_token = httpContext.Request.Query["access_token"].ToString ();
                var result = await _hubService.MakeUserOnline (userInfo.AppId, userInfo.Id, Context.ConnectionId, access_token);
                if (!result) {
                    Context.Abort ();
                }
            } catch {
                Context.Abort ();
            }
        }

        public override async Task OnDisconnectedAsync (Exception exception) {

            var userInfo = await _distributedCache.GetObjectAsync<User>(Context.ConnectionId);

            await _distributedCache.RemoveAsync(Context.ConnectionId);
             
            var httpContext = Context.GetHttpContext();

            var access_token = httpContext.Request.Query["access_token"].ToString ();

            await _hubService.MakeUserOffline (userInfo.AppId, userInfo.Id, Context.ConnectionId, access_token);
        }

    }
}