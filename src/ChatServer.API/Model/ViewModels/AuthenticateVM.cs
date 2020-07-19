using System.ComponentModel.DataAnnotations;

namespace ChatServer.API.Model.ViewModels {
    public class AuthenticateVM {
        [Required]
        public string APIKey { get; set; }
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string UserExternalId { get; set; }

        [Required]
        public string Fullname { get; set; }

        public override bool Equals (object obj) {
            return base.Equals (obj);
        }

        public override int GetHashCode () {
            return base.GetHashCode ();
        }

        public override string ToString () {
            return base.ToString ();
        }
    }
}