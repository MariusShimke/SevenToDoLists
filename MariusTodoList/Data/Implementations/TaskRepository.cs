using MariusTodoList.Data.Abstractions;
using MariusTodoList.DTO;
using MariusTodoList.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MariusTodoList.Data.Implementations
{
    public class TaskRepository : Repository<TasksModel>, ITaskRepository
    {
        public TaskRepository(ApplicationContext dbContext) : base(dbContext) { }

        public async Task<TasksModel> GetTask(int id)
        {
            return await _dbContext.TasksModel.Where(d => d.TaskID == id).Include(d => d.User).FirstOrDefaultAsync();
        }

        public List<ExportAllTasksExcelDTO> GetAllTasks()
        {          
            //return _dbContext.Query<ExportAllTasksExcel>().FromSql("sp_excelExportRequests").ToList();
            return _dbContext.Query<ExportAllTasksExcelDTO>().FromSql("sp_excelExportRequests").ToList();
        }
    }
}
