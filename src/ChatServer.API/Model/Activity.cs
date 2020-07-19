using System;
using ChatServer.API.Model.Enum;

namespace ChatServer.API.Model {
    public class Activity {
        public ActivityType ActivityType { get; set; }
        public DateTime Date { get; set; }
        public string ConnectionId { get; set; }
    }
}