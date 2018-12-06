using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Term_Project.Models;

namespace Term_Project.Repositories.Marble
{
    public interface IMarbleRepository
    {
        List<MarbleModel> GetList();
        void Insert(MarbleModel marble);
        void Delete(int id);
    }
}
