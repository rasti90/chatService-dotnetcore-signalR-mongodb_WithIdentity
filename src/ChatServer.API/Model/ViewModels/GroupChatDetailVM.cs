using System.Collections.Generic;

namespace ChatServer.API.Model.ViewModels {
    public class GroupChatDetailVM {
        public string ChatName { get; set; }
        public List<string> OtherMembers { get; set; }
    }
}