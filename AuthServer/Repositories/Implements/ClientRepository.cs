﻿using AuthServer.Data.DbContext;
using AuthServer.Data.Models;
using AuthServer.Repositories.Interfaces;

namespace AuthServer.Repositories.Implements;

public class ClientRepository : GenericRepository<Clients>, IClientRepository
{
    public ClientRepository(AuthDbContext context) : base(context){}
}