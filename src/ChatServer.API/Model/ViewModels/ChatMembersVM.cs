using System.Collections.Generic;

namespace ChatServer.API.Model.ViewModels {
    public class ChatMembersVM {
        public string ChatId { get; set; }
        public List<ChatMember> ChatMembers { get; set; }
    }
}