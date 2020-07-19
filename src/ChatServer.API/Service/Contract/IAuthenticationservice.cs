using System.Threading.Tasks;
using ChatServer.API.Model;
using ChatServer.API.Model.ViewModels;

namespace ChatServer.API.Service.Contract {
    public interface IAuthenticationService {
        Task<User> Authenticate (AuthenticateVM model);
    }
}