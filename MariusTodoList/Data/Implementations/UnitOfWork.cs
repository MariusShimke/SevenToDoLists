using MariusTodoList.Data.Abstractions;
using MariusTodoList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MariusTodoList.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _dbContext;

        public UnitOfWork(ApplicationContext cont) { this._dbContext = cont; }

        public IRepository<TasksModel> TaskRepository => new Repository<TasksModel>(_dbContext);
        public IRepository<ApplicationUser> ApplicationUserRepository => new Repository<ApplicationUser>(_dbContext);


        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
