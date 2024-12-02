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

  public IActionResult Edit(int id)
  {
    if (id == 0)
      return View();
    else
    {
      var user = _repo.FindOne(id);

      return View(user);
    }
  }

  [HttpPost]
  public IActionResult Save(int id, User user)
  {
    id = user.Id;

    if (id == 0)
    {
        _repo.Create(user);
    }
    else
    {
        _repo.Edit(id, user);
    }

    return RedirectToAction("Index");
  }

  [HttpPost]
  public IActionResult Delete(int id)
  {
    _repo.Delete(id);

    return RedirectToAction("Index");
  }
}
