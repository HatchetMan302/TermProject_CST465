using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Term_Project.Models
{
    public class MIU_IdentityUser : IdentityUser
    {
        public string FavoriteMarble { get; set; }
    }
}
