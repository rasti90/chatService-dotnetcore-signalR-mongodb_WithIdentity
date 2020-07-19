using System.Threading.Tasks;
using ChatServer.API.Model.ViewModels;

namespace ChatServer.API.Service.Contract {
    public interface IApplicationService {
        Task SeedData ();
    }
}