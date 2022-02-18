using EventRegistration.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventRegistration.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Role> Roles { get; }
        IGenericRepository<Role> Roless { get; }
        IGenericRepository<Venue> Venues { get; }
        
    }
}