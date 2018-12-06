using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Term_Project.Models;

namespace Term_Project.Repositories.Level
{
    public interface ILevelRepository
    {
        List<LevelModel> GetList();
        void Insert(LevelModel level);
        void Delete(int id);
    }
}
