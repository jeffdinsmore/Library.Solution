using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;

namespace Library.Controllers
{
  public class CatalogsController : Controller
  {
    private readonly LibraryContext _db;

    public CatalogsController(LibraryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Catalog> model = _db.Catalogs.ToList();
      return View(model);
    }

    [Authorize(Roles = "Administrator")]
    public ActionResult Create()
    {
      return View();
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public ActionResult Create(Catalog catalog)
    {
      _db.Catalogs.Add(catalog);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisCatalog = _db.Catalogs
        .Include(catalog => catalog.JoinEntries)
        .ThenInclude(join => join.Book)
        .FirstOrDefault(catalog => catalog.CatalogId == id);
      return View(thisCatalog);
    }

    [Authorize(Roles = "Administrator")]
    public ActionResult Edit(int id)
    {
      var thisCatalog = _db.Catalogs.FirstOrDefault(catalog => catalog.CatalogId == id);
      return View(thisCatalog);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public ActionResult Edit(Catalog catalog)
    {
      _db.Entry(catalog).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize(Roles = "Administrator")]
    public ActionResult Delete(int id)
    {
      var thisCatalog = _db.Catalogs.FirstOrDefault(catalog => catalog.CatalogId == id);
      return View(thisCatalog);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCatalog = _db.Catalogs.FirstOrDefault(catalog => catalog.CatalogId == id);
      _db.Catalogs.Remove(thisCatalog);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}