using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GroupChatApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly GroupChatContext _groupChatContext;

        public ChatController(UserManager<IdentityUser> userManager,GroupChatContext groupChatContext)
        {
            _userManager = userManager;
            _groupChatContext = groupChatContext;
        }


        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }

        // GET: Chat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Chat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Chat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Chat/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}