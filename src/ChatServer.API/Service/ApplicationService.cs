using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ChatServer.API.Helper;
using ChatServer.API.Model;
using ChatServer.API.Model.ViewModels;
using ChatServer.API.Repository.Contract;
using ChatServer.API.Service.Contract;
using Microsoft.IdentityModel.Tokens;

namespace ChatServer.API.Service {
    public class ApplicationService : IApplicationService {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService (IApplicationRepository applicationRepository) {
            this._applicationRepository = applicationRepository;
        }

        public async Task SeedData () {
            await _applicationRepository.SeedDataAsync ();
        }

    }
}