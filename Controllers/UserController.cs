using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using init.Models;

namespace init.Controllers;

public class UserController(ILogger<UserController> logger) : Controller
{
  private readonly ILogger<UserController> _logger = logger;
  private readonly UserRepository _repo = new();

  public IActionResult Index()
  {
    var users = _repo.FindMany();

    return View(users);
  }
}
