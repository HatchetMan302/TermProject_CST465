using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Term_Project.Models;
using Term_Project.Repositories.Powerup;

namespace Term_Project.Controllers
{
    [Authorize]
    public class PowerupController : Controller
    {
        private IPowerupRepository _PowerupRepo;
        private DatabaseSettings databaseSettings;
        public PowerupController(IOptionsSnapshot<DatabaseSettings> settings, IPowerupRepository powerupRepo)
        {
            _PowerupRepo = powerupRepo;
            databaseSettings = settings.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_PowerupRepo.GetList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(PowerupModel powerup)
        {
            _PowerupRepo.Insert(powerup);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (databaseSettings.AllowPowerupDelete == false)
            {
                throw new InvalidOperationException("You are not permitted to delete at this time");
            }

            _PowerupRepo.Delete(id);

            return RedirectToAction("Index");
        }
    }
}