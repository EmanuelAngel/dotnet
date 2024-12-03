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
    try
    {
      var users = _repo.FindMany();

      return View(users);
    }
    catch (Exception)
    {
      throw;
    }
  }

  public IActionResult Edit(int id)
  {
    try
    {
      if (id == 0)
        return View(new User());
      else
      {
        var user = _repo.FindOne(id);
        return View(user);
      }
    }
    catch (Exception)
    {
      throw;
    }
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Save(User user)
  {
    try
    {
      if (ModelState.IsValid)
      {
        if (user.Id == 0)
          _repo.Create(user);
        else
          _repo.Edit(user.Id, user);

        TempData["SuccessMessage"] = user.Id == 0
          ? "User created successfully."
          : "User updated successfully.";

        return RedirectToAction("Index");
      }

      // If model is not valid, return to the edit view with the current user object
      return View("Edit", user);
    }
    catch (Exception)
    {
      throw;
    }
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Delete(int id)
  {
    try
    {
      var user = _repo.FindOne(id);

      if (user == null)
      {
        return NotFound();
      }

      _repo.Delete(id);
      
      TempData["SuccessMessage"] = "User successfully deleted.";

      return RedirectToAction("Index");
    }
    catch (Exception)
    {
      throw;
    }
  }
}
