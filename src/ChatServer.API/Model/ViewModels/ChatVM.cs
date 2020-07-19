using ChatServer.API.Model.Enum;

namespace ChatServer.API.Model.ViewModels {
    public class ChatVM {
        public ChatType ChatType { get; set; }
        public PrivateChatDetailVM PrivateChatDetail { get; set; }
        public GroupChatDetailVM GroupChatDetail { get; set; }
        public string AppId { get; set; }
        public string UserId { get; set; }
    }
}