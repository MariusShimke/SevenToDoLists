using MariusTodoList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MariusTodoList.Data.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TasksModel> TaskRepository { get; }
        IRepository<ApplicationUser> ApplicationUserRepository { get; }

        int Commit();
        Task<int> CommitAsync();
    }
}
