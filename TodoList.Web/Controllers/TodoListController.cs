using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Data;
using TodoList.Data.POCO;
using TodoList.Services.EmailService;

namespace TaskList.Web.Controllers
{
    public class TodoListController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TodoListDbContext _db;
        private IEmailService _emailService;

        public TodoListController(ILogger<HomeController> logger, TodoListDbContext db, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.TaskItems.AsEnumerable());
        }

        public IActionResult Create()
        {
            return View(new TaskItem());
        }

        [HttpPost]
        public IActionResult Create(TaskItem model)
        {
            if (ModelState.IsValid)
            {
                _db.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int Id)
        {
            return View(_db.TaskItems.FirstOrDefault(a => a.Id == Id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskItem model)
        {
            if (ModelState.IsValid)
            {
                var editModel = _db.TaskItems.Find(model.Id);
                editModel.TaskName = model.TaskName;
                editModel.IsCompleted = model.IsCompleted;
                if (editModel.IsCompleted)
                {
                    await _emailService.SendEmailAsync("Kevin@test.com", "KC@test.com", 
                        "Task Was Completed", $"Task {editModel.Id} Was Completed on {DateTime.Now}");
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int Id)
        {
            return View(_db.TaskItems.FirstOrDefault(a => a.Id == Id));
        }

        [HttpPost]
        public IActionResult Delete(TaskItem model)
        {
            var deleteModel = _db.TaskItems.Find(model.Id);
            _db.Remove(deleteModel);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}