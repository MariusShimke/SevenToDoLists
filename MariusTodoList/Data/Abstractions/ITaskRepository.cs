using MariusTodoList.DTO;
using MariusTodoList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MariusTodoList.Data.Abstractions
{    
    public interface ITaskRepository : IRepository<TasksModel>
    {
        Task<TasksModel> GetTask(int id);

        List<ExportAllTasksExcelDTO> GetAllTasks();
    }
}
