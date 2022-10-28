using Messenger.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.PL.ViewModels.Admin
{
    public class AdminUsersViewModel : PagedViewModel
    {
        public IEnumerable<UserEntity> Users { get; set; }
    }
}
