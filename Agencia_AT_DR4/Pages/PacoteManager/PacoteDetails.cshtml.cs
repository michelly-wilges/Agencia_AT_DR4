using Agencia_AT_DR4.Data;
using Agencia_AT_DR4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Agencia_AT_DR4.Pages.PacoteManager
{
    public class PacoteDetailsModel : PageModel
    {
        private readonly AgenciaContext _context;

        public PacoteDetailsModel(AgenciaContext context)
        {
            _context = context;
        }

        public PacoteTuristico? Pacote { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Pacote = await _context.PacotesTuristicos
                .Include(p => p.Reservas.Where(r => r.DeletedAt == null))
                    .ThenInclude(r => r.Cliente)
                .FirstOrDefaultAsync(p => p.Id == id && p.DeletedAt == null);

            return Page();
        }
    }
}