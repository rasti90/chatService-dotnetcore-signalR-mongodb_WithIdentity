namespace ChatServer.API.Model.ViewModels {
    public class ChatHistoryFilterModel:BaseKeysetFilterModel{
        public string ChatId{get;set;}
        public ChatHistoryFilterModel():base(){
        }
    }
}