using System.Collections.Generic;
using System.Threading.Tasks;
using ChatServer.API.Model;

namespace ChatServer.API.Repository.Contract {
    public interface IApplicationRepository {
        Task SeedDataAsync ();
        List<Application> Get ();
        Task<List<Application>> GetAsync ();
        Application Get (string id);
        Task<Application> GetAsync (string id);
        Application GetByAPIKey (string APIKey);
        Task<Application> GetByAPIKeyAsync (string APIKey);
        Application Create (Application application);
        Task<Application> CreateAsync (Application application);
        void Update (string id, Application applicationIn);
        Task UpdateAsync (string id, Application applicationIn);
        void Remove (Application applicationIn);
        Task RemoveAsync (Application applicationIn);
        void Remove (string id);
        Task RemoveAsync (string id);
    }
}