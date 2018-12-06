using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Term_Project.Models;
using Term_Project.Repositories.Marble;

namespace Term_Project.Controllers
{
    [Authorize]
    public class MarbleController : Controller
    {
        private IMarbleRepository _MarbleRepo;
        private DatabaseSettings databaseSettings;
        public MarbleController(IOptionsSnapshot<DatabaseSettings> settings, IMarbleRepository marbleRepo)
        {
            _MarbleRepo = marbleRepo;
            databaseSettings = settings.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_MarbleRepo.GetList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(MarbleModel marble)
        {
            _MarbleRepo.Insert(marble);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if(databaseSettings.AllowMarbleDelete == false)
            {
                throw new InvalidOperationException("You are not permitted to delete at this time");
            }

            _MarbleRepo.Delete(id);

            return RedirectToAction("Index");
        }
    }
}