using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MariusTodoList;
using MariusTodoList.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MariusTodoList.Data.Abstractions;
using System.Text;
using System.IO;

namespace MariusTodoList.Controllers
{
    
    public class TasksController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public TasksController(ApplicationContext context, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _context = context;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public  ApplicationUser GetCurrentUser() {
            var userNameSess = HttpContext.Session.GetString("UserName");
            var userIdSess = HttpContext.Session.GetString("UserID");

            var currentUser = userIdSess != null ?  _context.ApplicationUser.Where(x => x.Id == userIdSess).FirstOrDefault():null;
            return currentUser;
        }
        
        // GET: Tasks
        public async Task<IActionResult> Index(int priority, int weekDay, string description)
        {           
            var user = GetCurrentUser();
            var currentUser = await GetCurrentUserAsync();

            ViewBag.TabNumber = description == null ? "":weekDay.ToString();

            if (user == null)
            {
                ViewBag.HellowMessage = "";
                return RedirectToAction("Login", "Account");
            }               

            if (description != null)
            {
                InserTask(priority, weekDay, description, user);
            }

            ViewBag.HellowMessage = "Welcome, "+ user.UserName + " | ";

            List<TasksModel> AllUserTasks = await _context.TasksModel.Where(x => x.UserID == user.UserID).ToListAsync();
            //MONDAY
            List<TasksModel> mondayList = AllUserTasks.Where(x => x.WeekDay == 1).ToList();
            ViewBag.MondayList = mondayList;
            //TUESDAY
            List<TasksModel> tuesdayList = AllUserTasks.Where(x => x.WeekDay == 2).ToList();
            ViewBag.TuesdayList = tuesdayList;
            //WEDNESDAY
            List<TasksModel> wednesdayList = AllUserTasks.Where(x => x.WeekDay == 3).ToList();
            ViewBag.WednesdayList = wednesdayList;
            //THURSDAY
            List<TasksModel> thursdayList = AllUserTasks.Where(x => x.WeekDay == 4).ToList();
            ViewBag.ThursdayList = thursdayList;
            //FRIDAY
            List<TasksModel> fridayList = AllUserTasks.Where(x => x.WeekDay == 5).ToList();
            ViewBag.FridayList = fridayList;
            //SATURDAY
            List<TasksModel> saturdayList = AllUserTasks.Where(x => x.WeekDay == 6).ToList();
            ViewBag.SaturdayList = saturdayList;
            //SUNDAY
            List<TasksModel> sundayList = AllUserTasks.Where(x => x.WeekDay == 7).ToList();
            ViewBag.SundayList = sundayList;

            return View(AllUserTasks);
        }

        public void InserTask(int priority, int weekDay, string description, ApplicationUser user)
        {

            TasksModel tasksModel = new TasksModel();
            tasksModel.Priority = priority;
            tasksModel.Description = description;
            tasksModel.WeekDay = weekDay;
            tasksModel.DtCreated = DateTime.Now;
            tasksModel.DtUpdated = null;
            tasksModel.IsActive = true;
            tasksModel.IsDone = false;
            tasksModel.UserID = user.UserID;

            _unitOfWork.TaskRepository.Insert(tasksModel);
            _unitOfWork.CommitAsync();
            //_context.Add(tasksModel);
            //_context.SaveChangesAsync();
            RedirectToAction(nameof(Index));

        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasksModel = await _context.TasksModel
                .FirstOrDefaultAsync(m => m.TaskID == id);
            if (tasksModel == null)
            {
                return NotFound();
            }

            return View(tasksModel);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,WeekDay,Description,DtCreated,IsActive")] TasksModel tasksModel)
        {
            var user = GetCurrentUser();

            if (user == null)
                return RedirectToAction("Login", "Account");

            tasksModel.UserID = user.UserID;

            if (ModelState.IsValid)
            {
                _context.Add(tasksModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tasksModel);
        }


        public async Task<IActionResult> UpdateTask(int? taskIDinline, int isDoneNumValue, int priorityInline, string descriptionInline)
        {
            if (taskIDinline == null)
            {
                return NotFound();
            }

            var tasksModel = await _context.TasksModel.FirstOrDefaultAsync(m => m.TaskID == taskIDinline);
            if (tasksModel == null)
            {
                return NotFound();
            }
            tasksModel.IsDone = isDoneNumValue == 0 ? false:true;
            tasksModel.Priority = priorityInline;
            tasksModel.Description = descriptionInline;
            tasksModel.DtUpdated = DateTime.Now;

            _context.TasksModel.Update(tasksModel);
            _context.SaveChangesAsync();
            //RedirectToAction(nameof(Index));

            return RedirectToAction(nameof(Index));
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasksModel = await _context.TasksModel.FindAsync(id);
            if (tasksModel == null)
            {
                return NotFound();
            }
            return View(tasksModel);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,WeekDay,Description,DtCreated,IsActive")] TasksModel tasksModel)
        {
            if (id != tasksModel.TaskID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasksModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksModelExists(tasksModel.TaskID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tasksModel);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasksModel = await _context.TasksModel
                .FirstOrDefaultAsync(m => m.TaskID == id);
            if (tasksModel == null)
            {
                return NotFound();
            }

            
            _context.TasksModel.Remove(tasksModel);
             _context.SaveChangesAsync();
            RedirectToAction(nameof(Index));

            return RedirectToAction(nameof(Index));
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tasksModel = await _context.TasksModel.FindAsync(id);
            _context.TasksModel.Remove(tasksModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasksModelExists(int id)
        {
            return _context.TasksModel.Any(e => e.TaskID == id);
        }


        public IActionResult ExportAllTasks()
        {
            StringBuilder sbErrors = new StringBuilder();
            try
            {
                //string reportName = "LKF_Requests";
                //var formData = _unitOfWork.TaskRepository.GetAll();
                //var t = new BusinessLogic.ReportExcelHelper().GetAllRequestsExcelFile(formData);
                //var fileResult = new FileStreamResult(new MemoryStream(t.Contents), "*/*");
                //var date = DateTime.Now.ToString("MM/dd/y");
                //fileResult.FileDownloadName = string.Format("{0}_Report_{1}.xlsx", reportName, date);

                //return fileResult;
                return null;
                //This is temHi yes tttttttttttttttttttttttttttttttttttt
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
