using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Agencia_AT_DR4.Data;
using Agencia_AT_DR4.Models;
using Microsoft.AspNetCore.Authorization;

namespace Agencia_AT_DR4.Pages.PacotesTuristicos
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Agencia_AT_DR4.Data.AgenciaContext _context;

        public IndexModel(Agencia_AT_DR4.Data.AgenciaContext context)
        {
            _context = context;
        }

        public IList<PacoteTuristico> PacoteTuristico { get;set; } = default!;

        public async Task OnGetAsync()
        {
            PacoteTuristico = await _context.PacotesTuristicos
                .Where(p => p.DeletedAt == null)
                .ToListAsync();
        }
    }
}
