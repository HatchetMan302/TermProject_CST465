using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Term_Project.Models;

namespace Term_Project.Repositories.Powerup
{
    public interface IPowerupRepository
    {
        List<PowerupModel> GetList();
        void Insert(PowerupModel powerup);
        void Delete(int id);
    }
}
