using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.PL.ViewModels
{
    public class MessegePagedViewModel
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 20;

        public int TotalCount { get; set; }
        public Guid ChatId { get; set; }

        public IEnumerable<MessegeViewModel> Massages { get; set; }
        
        public MessegeViewModel newMassage { get; set; }

    }
}
