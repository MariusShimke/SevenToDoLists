using MariusTodoList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MariusTodoList.Data.Abstractions
{
    interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetApplicationUser(int id);
    }
}
