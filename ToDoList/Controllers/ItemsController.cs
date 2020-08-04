using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoList.Controllers
{
  public class ItemsController : Controller
  {
    private readonly ToDoListContext _db;

    public ItemsController(ToDoListContext db)
    {
      _db = db;
    }

      public ActionResult Index()
      {
          return View(_db.Items.ToList());
      }

    public ActionResult Create()
    {
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Item item, int CategoryId)
    {
      _db.Items.Add(item);
      if (CategoryId != 0)
      {
        _db.CategoryItem.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisItem = _db.Items
      .Include(item => item.Categories)
      .ThenInclude(join => join.Category)
      .FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    public ActionResult Edit(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      ViewBag.Completed = new SelectList(_db.Items, "Completed");
      // ViewBag.CategoryId = new SelectList(_db.Categories, "Completed", "Completed");
      return View(thisItem);
    }



// _db.Entry(item).State = EntityState.Modified;

    [HttpPost]
    public ActionResult Edit(Item item, int CategoryId)
    {
    //   (repository.Get(x => x.Major == newVersion.Major && 
    // x.Minor == newVersion.Minor && x.Build == newVersion.Build)
    // .Count() > 0)
      
      if ((CategoryId != 0));
      {
        _db.CategoryItem.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId });
      }
      _db.Entry(item).State = EntityState.Modified;
      if(!_db.CategoryItem.Get(x => x.CategoryId == CategoryId) && (!_db.CategoryItem.Get(x => x.ItemId)))
      {
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult AddCategory(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View(thisItem);
    }

    [HttpPost]
    public ActionResult AddCategory(Item item, int CategoryId)
    {
      if(CategoryId != 0)
      {
        _db.CategoryItem.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId });
      }
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
      return View(thisItem);
    }

    [HttpPost]
    public ActionResult DeleteCategory(int joinId)
    {
      var joinEntry = _db.CategoryItem.FirstOrDefault(entry => entry.CategoryItemId == joinId);
      _db.CategoryItem.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
      _db.Items.Remove(thisItem);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // public ActionResult Completed(int id)
    // {
    //   var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
    //   ViewBag.ItemId = new SelectList(_db.Items, "ItemId", "Name");
    //   return View(thisItem);
    // }

    // [HttpPost]
    // public ActionResult Completed(Item item, int CategoryId)
    // {
    //   if (CategoryId != 0)
    //   {
    //     _db.CategoryItem.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId });
    //   }
    //   _db.Entry(item).State = EntityState.Modified;
    //   _db.SaveChanges();
    //   return RedirectToAction("Details");
    // }

//     public ActionResult Completed(int id)
// {
//     var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
//     return View(thisItem);
// }

//     [HttpPost]
//     public ActionResult Completed(Item item)
//     {
//       _db.Entry(item).State = EntityState.Modified;
//       _db.SaveChanges();
//       return RedirectToAction("Details");
//     }
  }
}