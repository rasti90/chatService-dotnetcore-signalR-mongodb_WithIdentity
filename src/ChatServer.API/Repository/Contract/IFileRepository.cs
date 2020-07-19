using System.Collections.Generic;
using ChatServer.API.Model;

namespace ChatServer.API.Repository.Contract {
    public interface IFileRepository {
        List<File> Get ();
        File Get (string id);
        File Create (File file);
        void Update (string id, File fileIn);
        void Remove (File fileIn);
        void Remove (string id);
    }
}