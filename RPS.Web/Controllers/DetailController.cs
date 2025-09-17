using RPS.Core.Models;
using RPS.Core.Models.Enums;
using RPS.Data;
using RPS.Web.Models.ViewModels;
using RPS.Web.Models.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RPS.Core.Models.Dto;

namespace RPS.Web.Controllers
{
    [RoutePrefix("Detail")]
    public class DetailController : Controller
    {
        private const int CURRENT_USER_ID = 21; //Fake user id for demo

        private readonly IPtUserRepository rpsUserRepo;
        private readonly IPtItemsRepository rpsItemsRepo;
        private readonly IPtTasksRepository rpsTasksRepo;
        private readonly IPtCommentsRepository rpsCommentsRepo;

        public DetailController(
            IPtUserRepository rpsUserData,
            IPtItemsRepository rpsItemsData, 
            IPtTasksRepository rpsTasksData,
            IPtCommentsRepository rpsCommentsData
            )
        {
            rpsUserRepo = rpsUserData;
            rpsItemsRepo = rpsItemsData;
            rpsTasksRepo = rpsTasksData;
            rpsCommentsRepo = rpsCommentsData;
        }

        [Route("{id:int}")]
        public ActionResult Index(int id)
        {
            var item = rpsItemsRepo.GetItemById(id);
            var users = rpsUserRepo.GetAll();
            var currentUser = users.Single(u => u.Id == CURRENT_USER_ID);

            ViewBag.screen = DetailScreenEnum.Form;
            ViewBag.users = users;
            ViewBag.currentUser = currentUser;

            return View("Details", item);
        }

        [Route("{id:int}/Form")]
        public ActionResult Form(int id)
        {
            var item = rpsItemsRepo.GetItemById(id);
            var users = rpsUserRepo.GetAll();
            var currentUser = users.Single(u => u.Id == CURRENT_USER_ID);
            ViewBag.screen = DetailScreenEnum.Form;
            ViewBag.users = users;
            ViewBag.currentUser = currentUser;

            return View("Details", item);
        }

        [Route("{id:int}/DetailsForm")]
        public ActionResult DetailsForm(int id)
        {
            var item = rpsItemsRepo.GetItemById(id);
            var users = rpsUserRepo.GetAll();

            var model = new PtItemFormVm(item, users.ToList());
            return PartialView("_Form", model);
        }

        [HttpPost]
        [Route("{id:int}/DetailsForm")]
        public ActionResult DetailsForm(int id, PtItemFormVm vm)
        {
            var item = rpsItemsRepo.GetItemById(id);
            var users = rpsUserRepo.GetAll();
            ViewBag.screen = DetailScreenEnum.Form;
            ViewBag.users = users;

            try
            {
                var updatedItem = rpsItemsRepo.UpdateItem(vm.ToPtUpdateItem());
                return RedirectToAction("Form", new { id = id });
            }
            catch
            {
                return RedirectToAction("Form", new { id = id });
            }
        }

        [Route("{id:int}/Tasks")]
        public ActionResult Tasks(int id)
        {
            var item = rpsItemsRepo.GetItemById(id);
            var users = rpsUserRepo.GetAll();
            var currentUser = users.Single(u => u.Id == CURRENT_USER_ID);
            ViewBag.screen = DetailScreenEnum.Tasks;
            ViewBag.users = users;
            ViewBag.currentUser = currentUser;

            return View("Details", item);
        }

        [Route("{id:int}/TasksForm")]
        public ActionResult TasksForm(int id)
        {
            var item = rpsItemsRepo.GetItemById(id);
            var model = new PtItemTasksVm(item);
            return PartialView("_Tasks", model);
        }

        [HttpPost]
        [Route("{id:int}/TasksForm")]
        public ActionResult TasksForm(int id, PtItemTasksVm vm)
        {
            ViewBag.screen = DetailScreenEnum.Tasks;

            try
            {
                PtNewTask taskNew = new PtNewTask
                {
                    ItemId = id,
                    Title = vm.NewTaskTitle
                };

                rpsTasksRepo.AddNewTask(taskNew);

                return RedirectToAction("Tasks", new { id = id });
            }
            catch
            {
                return RedirectToAction("Tasks", new { id = id });
            }
        }

        [HttpPost]
        [Route("{id:int}/TaskUpdate/{taskId:int}")]
        public ActionResult TaskUpdate(int id, int taskId, string title, bool? completed)
        {
            ViewBag.screen = DetailScreenEnum.Tasks;

            try
            {
                PtUpdateTask uTask = new PtUpdateTask
                {
                    Id = taskId,
                    ItemId = id,
                    Title = title,
                    Completed = completed.HasValue ? completed.Value : false
                };
                rpsTasksRepo.UpdateTask(uTask);
                return RedirectToAction("Tasks", new { id = id });
            }
            catch
            {
                return RedirectToAction("Tasks", new { id = id });
            }
        }

        [HttpPost]
        [Route("{id:int}/TaskDelete/{taskId:int}")]
        public ActionResult TaskDelete(int id, int taskId, PtItemTasksVm vm)
        {
            ViewBag.screen = DetailScreenEnum.Tasks;

            try
            {
                var result = rpsTasksRepo.DeleteTask(taskId, id);
                return RedirectToAction("Tasks", new { id = id });
            }
            catch
            {
                return RedirectToAction("Tasks", new { id = id });
            }
        }

        [Route("{id:int}/Chitchat")]
        public ActionResult Chitchat(int id)
        {
            var item = rpsItemsRepo.GetItemById(id);
            var users = rpsUserRepo.GetAll();
            var currentUser = users.Single(u => u.Id == CURRENT_USER_ID);

            ViewBag.screen = DetailScreenEnum.Chitchat;
            ViewBag.users = users;
            ViewBag.currentUser = currentUser;

            return View("Details", item);
        }

        [Route("{id:int}/ChitchatForm")]
        public ActionResult ChitchatForm(int id)
        {
            var item = rpsItemsRepo.GetItemById(id);
            var users = rpsUserRepo.GetAll();
            var currentUser = users.Single(u => u.Id == CURRENT_USER_ID);

            var model = new PtItemCommentsVm(item, currentUser);
            return PartialView("_Chitchat", model);
        }

        [HttpPost]
        [Route("{id:int}/ChitchatForm")]
        public ActionResult ChitchatForm(int id, PtItemCommentsVm vm)
        {
            ViewBag.screen = DetailScreenEnum.Chitchat;

            try
            {
                PtNewComment commentNew = new PtNewComment
                {
                    ItemId = id,
                    Title = vm.NewCommentText,
                    UserId = CURRENT_USER_ID
                };

                rpsCommentsRepo.AddNewComment(commentNew);

                return RedirectToAction("Chitchat", new { id = id });
            }
            catch
            {
                return RedirectToAction("Chitchat", new { id = id });
            }
        }
    }
}