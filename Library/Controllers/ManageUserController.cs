using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
  [Authorize(Roles = "Administrator")]
  public class ManageUsersController : Controller
  {
    private readonly UserManager<Patron>
        _userManager;

    public ManageUsersController(
        UserManager<Patron> userManager)
    {
      _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
      var admins = (await _userManager
          .GetUsersInRoleAsync("Administrator"))
          .ToArray();

      var everyone = await _userManager.Users
          .ToArrayAsync();

      var model = new ManageUsersViewModel
      {
        Administrators = admins,
        Everyone = everyone
      };

      return View(model);
    }
  }
}