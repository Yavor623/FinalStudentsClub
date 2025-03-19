using Microsoft.AspNetCore.Identity;
using StudentsClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using StudentsClub.Models.Roles;
using StudentsClub.Data.Migrations;
using StudentsClub.Data;

namespace StudentsClub.Controllers
{

    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Create() 
        {
            return View(new CreateRoleViewModel());
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var roleExist =  await _roleManager.RoleExistsAsync(model.RoleName);
                if (!roleExist&&await _userManager.IsInRoleAsync(user,"Admin"))
                {
                     await _roleManager.CreateAsync(new IdentityRole(model.RoleName));
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Ролята вече съществува");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View(new EditRoleViewModel());
        }
        [HttpPost]
        public async Task<ActionResult> Edit(string id,EditRoleViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id);
                role.Name = model.RoleName;
                role.NormalizedName = model.RoleName.ToUpper();
                await _roleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<ActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(role);
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}
