using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClothingStore.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.WebApp.Controllers
{
    public class StoresController : Controller
    {
        public Lib.IClothingStoreRepo Repo { get; }

        public StoresController(Lib.IClothingStoreRepo repo)
        {
            Repo = repo;
        }
        // GET: Store
        public ActionResult Index()
        {
            IEnumerable<Lib.Store> storeList = Repo.GetStores().ToList();
            //var viewModels = movies.Select(m => new MovieViewModel
            //{
            //    Id = m.Id,
            //    Title = m.Title,
            //    Genre = m.Genre,
            //    ReleaseDate = m.DateReleased
            //}).ToList();
            var storeModels = storeList.Select(s => new Stores
            {
                LocationId = s.Id,
                StoreName = s.Name
            }).ToList();

            return View(storeModels);
        }

        // GET: Store/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Store/Create
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

        // GET: Store/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Store/Edit/5
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

        // GET: Store/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Store/Delete/5
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