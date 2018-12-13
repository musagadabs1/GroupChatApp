using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupChatApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GroupChatApp.Controllers
{
    public class GroupsController : Controller
    {
        private readonly GroupChatContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GroupsController(GroupChatContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<UserGroupViewModel> GetAll()
        {
            var groups = _context.UserGroups
                              .Where(gp => gp.UserName == _userManager.GetUserName(User))
                              .Join(_context.Groups, ug => ug.GroupId, g => g.ID, (ug, g) =>
                                             new UserGroupViewModel()
                                             {
                                                 UserName = ug.UserName,
                                                 GroupId = g.ID,
                                                 GroupName = g.GroupName
                                             })
                               .ToList();

            return groups;
        }

        [HttpPost]
        public IActionResult Create([FromBody] NewGroupViewModel group)
        {
            if (group==null ||group.GroupName=="")
            {
                return new ObjectResult(new { status="error",message="incomplete request"});
            }
            if ((_context.Groups.Any(gp => gp.GroupName==group.GroupName))==true)
            {
                return new ObjectResult(new { status = "error", message = "group name already exist" });
            }
            Group newGroup = new Group { GroupName = group.GroupName };
            //Insert the new Group into Database
            _context.Groups.Add(newGroup);
            _context.SaveChanges();
            //Insert into the user group table, groupId and userId in the user_groups table..
            foreach (var userGroup in group.UserNames)
            {
                _context.UserGroups.Add(new UserGroup {UserName=userGroup,GroupId=newGroup.ID });
                _context.SaveChanges();
            }
            return new ObjectResult(new { status = "Success", data = newGroup });
        }
        // GET: Groups
        public ActionResult Index()
        {
            return View();
        }

        // GET: Groups/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
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

        // GET: Groups/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Groups/Edit/5
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

        // GET: Groups/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Groups/Delete/5
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