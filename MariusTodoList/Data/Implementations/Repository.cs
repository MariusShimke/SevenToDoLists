using MariusTodoList.Data.Abstractions;
using MariusTodoList.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MariusTodoList.Data.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationContext _dbContext;
        protected DbSet<T> _table;

        public Repository(ApplicationContext con)
        {
            this._dbContext = con;
            this._table = _dbContext.Set<T>();
        }

        public T Get(object id)
        {
            return _table.Find(id);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _table.Where(predicate);
        }

        public Task<List<T>> GetAll()
        {
            return _table.ToListAsync<T>();
        }

        public Task<T> GetAsync(object id)
        {
            return _table.FindAsync(id);
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return _table.FirstOrDefaultAsync(predicate);
        }

        public void Insert(T model)
        {
            _table.Add(model);
        }

        public void InsertAll(List<T> models)
        {
            _table.AddRange(models);
        }


        public List<ExportAllTasksExcelDTO> GetAllTasks()
        {
            //return _dbContext.Query<ExportAllTasksExcel>().FromSql("sp_excelExportRequests").ToList();
            return _dbContext.Query<ExportAllTasksExcelDTO>().FromSql("SELECT * FROM TaskModels").ToList();
        }
    }

}
