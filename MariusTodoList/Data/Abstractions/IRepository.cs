using MariusTodoList.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MariusTodoList.Data.Abstractions
{
    public interface IRepository<T>
    {
        T Get(object id);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAll();
        Task<T> GetAsync(object id);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        void Insert(T model);
        void InsertAll(List<T> models);

        List<ExportAllTasksExcelDTO> GetAllTasks();

    }
}
