using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Agencia_AT_DR4.Data;
using Agencia_AT_DR4.Models;

namespace Agencia_AT_DR4.Pages.PacotesTuristicos
{
    public class DeleteModel : PageModel
    {
        private readonly Agencia_AT_DR4.Data.AgenciaContext _context;

        public DeleteModel(Agencia_AT_DR4.Data.AgenciaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PacoteTuristico PacoteTuristico { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteturistico = await _context.PacotesTuristicos.FirstOrDefaultAsync(m => m.Id == id);

            if (pacoteturistico == null)
            {
                return NotFound();
            }
            else
            {
                PacoteTuristico = pacoteturistico;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var pacote = await _context.PacotesTuristicos.FindAsync(id);

            if (pacote != null)
            {
                pacote.DeletedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
