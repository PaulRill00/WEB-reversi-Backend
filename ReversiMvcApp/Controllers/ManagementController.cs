using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ReversiMvcApp.Enums;

namespace ReversiMvcApp.Controllers
{
    [Route("[controller]")]
    public class ManagementController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly PlayerController _playerController;

        // Use this because [Authorize] Is not working
        private async Task<bool> IsAllowed(string roles, ClaimsPrincipal user)
        {
            var userName = await _userManager.GetUserAsync(user);
            var role = await _userManager.GetRolesAsync(userName);

            foreach (var _role in roles.Split(","))
            {
                if (role.Contains(_role))
                    return true;
            }

            return false;
        }

        public ManagementController(UserManager<IdentityUser> userManager, PlayerController playerController)
        {
            _userManager = userManager;
            _playerController = playerController;
        }
        
        public async Task<ActionResult> Index()
        {
            if (!(await IsAllowed("Admin,Mediator", User)))
                return Redirect("./Identity/Account/AccessDenied");


            return View(await _playerController.GetPlayers());

        }

        [HttpGet("edit/{guid}")]
        public async Task<IActionResult> Edit(string guid)
        {
            if (!(await IsAllowed("Admin", User)))
                return Redirect("./Identity/Account/AccessDenied");

            var user = _playerController.GetPlayer(guid);

            return View(user);
        }

        [HttpPost("edit/{guid}")]
        public async Task<IActionResult> Edit(string guid, string name, string email)
        {
            if (!(await IsAllowed("Admin", User)))
                return Redirect("./Identity/Account/AccessDenied");

            var user = _playerController.GetPlayer(guid);

            user.Email = email;
            user.Name = name;

            await _playerController.SavePlayer(user);

            return RedirectToAction("Index");
        }

        [HttpGet("view/{guid}")]
        public async Task<IActionResult> View(string guid)
        {
            if (!(await IsAllowed("Admin,Mediator", User)))
                return Redirect("./Identity/Account/AccessDenied");

            var user = _playerController.GetPlayer(guid);

            return View(user);
        }

        [HttpGet("ban/{guid}")]
        public async Task<IActionResult> Ban(string guid)
        {
            if (!(await IsAllowed("Admin,Mediator", User)))
                return Redirect("./Identity/Account/AccessDenied");
            
            await _playerController.DeletePlayer(guid);
            return RedirectToAction("Index");
        }

        [HttpGet("promote/{guid}")]
        public async Task<IActionResult> Promote(string guid)
        {
            if (!(await IsAllowed("Admin", User)))
                return Redirect("./Identity/Account/AccessDenied");

            var user = await _userManager.FindByIdAsync(guid);

            await _userManager.AddToRoleAsync(user, "Mediator");

            return RedirectToAction("Index");
        }

        [HttpGet("demote/{guid}")]
        public async Task<IActionResult> Demote(string guid)
        {
            if (!(await IsAllowed("Admin", User)))
                return Redirect("./Identity/Account/AccessDenied");

            var user = await _userManager.FindByIdAsync(guid);

            await _userManager.RemoveFromRoleAsync(user, "Mediator");

            return RedirectToAction("Index");
        }
    }
}
