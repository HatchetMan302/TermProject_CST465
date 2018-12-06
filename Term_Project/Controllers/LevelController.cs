using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Term_Project.Models;
using Term_Project.Repositories.Level;

namespace Term_Project.Controllers
{
    public class LevelController : Controller
    {
        private ILevelRepository _LevelRepo;
        private DatabaseSettings databaseSettings;
        public LevelController(IOptionsSnapshot<DatabaseSettings> settings, ILevelRepository levelRepo)
        {
            _LevelRepo = levelRepo;
            databaseSettings = settings.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_LevelRepo.GetList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(LevelModel level)
        {
            _LevelRepo.Insert(level);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (databaseSettings.AllowLevelDelete == false)
            {
                throw new InvalidOperationException("You are not permitted to delete at this time");
            }

            _LevelRepo.Delete(id);

            return RedirectToAction("Index");
        }
    }
}