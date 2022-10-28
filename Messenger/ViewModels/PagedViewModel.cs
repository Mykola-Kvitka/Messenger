using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.PL.ViewModels
{
    public class PagedViewModel
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 15;

        public int TotalCount { get; set; }

    }
}
