using FinalPRN221.Models;
using FinalPRN221.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalPRN221.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDBContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, ApplicationDBContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var users = _userManager.Users
                .Include(u => u.Department)
                .Include(u => u.Position).ToList();
            return View("Index", users);
        }

        [Authorize(Roles = "Employee")]
        public IActionResult IndexEmployee(string id)
        {
            var user = _userManager.Users
                .Include(u => u.Department)
                .Include(u => u.Position)
                .Where(u => u.Id == id)
                .ToList();
            return View("Index", user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                Console.WriteLine($"User with ID {id} not found!");
                return NotFound();
            }
            //đoan code này lỗi do 2 hàm bất đồng bộ được đặt trong 2 dòng code đồng bộ , dẫn tới việc
            // không cái nào chờ nhau mà chúng tranh giành datareader để truy vấn , trong khi  1 dbcontext
            //chỉ có 1 datareader => lỗi
            /* ViewBag.Departments = new SelectList(await _context.Departments.ToListAsync(), "ID", "Name"); ViewBag.Positions = new SelectList(await _context.Positions.ToListAsync(), "ID", "Name");*/
            var departments = await _context.Departments.ToListAsync();
            var positions = await _context.Positions.ToListAsync();
            ViewBag.Departments = new SelectList(departments ?? new List<Department>(), "Id", "Name");
            ViewBag.Positions = new SelectList(positions ?? new List<Position>(), "Id", "Name");
            AccountEditVM accountVM = new AccountEditVM()
            {
                ID = user.Id,
                Name = user.Name,
                DateOfBirth = user.Dob,
                Address = user.Address,
                Gender = user.Gender,
                DepartmentID = user.DepartmentId,
                PositionID = user.PositionId,
                StartDate = user.StartDate
            };
            return View("Edit", accountVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccountEditVM accountVM)
        {
            bool isAdmin = await ContainRole(await _userManager.FindByIdAsync(accountVM.ID), "Admin");
            if (!ModelState.IsValid)
            {
                var departments = await _context.Departments.ToListAsync();
                var positions = await _context.Positions.ToListAsync();
                ViewBag.Departments = new SelectList(departments ?? new List<Department>(), "Id", "Name");
                ViewBag.Positions = new SelectList(positions ?? new List<Position>(), "Id", "Name");
                return View("Edit", accountVM);
            }
            if (string.IsNullOrEmpty(accountVM.ID)) return NotFound();

            var user = await _userManager.FindByIdAsync(accountVM.ID);
            if (user == null) return NotFound();
            user.Name = accountVM.Name;
            user.Dob = accountVM.DateOfBirth;
            user.Address = accountVM.Address;
            user.Gender = accountVM.Gender;
            user.DepartmentId = accountVM.DepartmentID;
            user.PositionId = accountVM.PositionID;
            user.StartDate = accountVM.StartDate;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View("Edit", accountVM);
            }
            if (await ContainRole(user, "Admin")) return RedirectToAction("Index");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(AccountCreateVM accCreateVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", accCreateVM);
            }
            var user = CreateUser();
            user.Email = accCreateVM.Email;
            user.NormalizedEmail = accCreateVM.Email.ToUpper();
            user.UserName = accCreateVM.Email;
            user.NormalizedUserName = accCreateVM.Email.ToUpper();
            var result = await _userManager.CreateAsync(user, accCreateVM.Password);
            if (result.Succeeded)
            {
                if (await ContainRole(user, "Admin")) return RedirectToAction("Index");
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View("Create", accCreateVM);
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            bool isAdmin = await ContainRole(await _userManager.FindByIdAsync(id), "Admin");
            if (id == null) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                if (await ContainRole(user, "Admin")) return RedirectToAction("Index");
                return RedirectToAction("Index", "Home");
            }
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
            if (await ContainRole(user, "Admin")) return RedirectToAction("Index");
            return RedirectToAction("Index", "Home");
        }

        public async Task<bool> ContainRole(ApplicationUser user, string Role)
        {
            if (Role == null) return false;
            if (user == null) return false;
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains(Role)) return true;
            return false;
        }
    }
}