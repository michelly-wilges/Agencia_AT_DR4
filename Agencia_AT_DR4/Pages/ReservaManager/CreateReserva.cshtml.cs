using Agencia_AT_DR4.Data;
using Agencia_AT_DR4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Agencia_AT_DR4.Pages.ReservaManager
{
    public class CreateReservaModel : PageModel
    {
        private readonly AgenciaContext _context;

        public CreateReservaModel(AgenciaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public SelectList ClienteOptions { get; set; } = null!;
        public SelectList PacoteOptions { get; set; } = null!;

        public class InputModel
        {
            [Required(ErrorMessage = "Selecione um cliente.")]
            public int ClienteId { get; set; }

            [Required(ErrorMessage = "Selecione um pacote.")]
            public int PacoteTuristicoId { get; set; }

            [Required(ErrorMessage = "Informe a data da reserva.")]
            public DateTime DataReserva { get; set; } = DateTime.Today;
        }

        public async Task OnGetAsync()
        {
            await CarregarOpcoesAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await CarregarOpcoesAsync();
                return Page();
            }

            // Verifica se cliente já reservou esse pacote na mesma data
            var reservaExistente = await _context.Reservas
                .FirstOrDefaultAsync(r =>
                    r.ClienteId == Input.ClienteId &&
                    r.PacoteTuristicoId == Input.PacoteTuristicoId &&
                    r.DataReserva.Date == Input.DataReserva.Date &&
                    r.DeletedAt == null);

            if (reservaExistente != null)
            {
                ModelState.AddModelError("", "Este cliente já possui uma reserva neste pacote para esta data.");
                await CarregarOpcoesAsync();
                return Page();
            }

            // Verifica capacidade máxima
            var pacote = await _context.PacotesTuristicos
                .Include(p => p.Reservas.Where(r => r.DeletedAt == null))
                .FirstOrDefaultAsync(p => p.Id == Input.PacoteTuristicoId);

            if (pacote != null && pacote.Reservas.Count >= pacote.CapacidadeMaxima)
            {
                ModelState.AddModelError("", "Este pacote já atingiu a capacidade máxima de participantes.");
                await CarregarOpcoesAsync();
                return Page();
            }

            // Verifica se a data é futura
            if (Input.DataReserva.Date < DateTime.Today)
            {
                ModelState.AddModelError("", "Só é possível reservar pacotes com data futura.");
                await CarregarOpcoesAsync();
                return Page();
            }

            var reserva = new Reserva
            {
                ClienteId = Input.ClienteId,
                PacoteTuristicoId = Input.PacoteTuristicoId,
                DataReserva = Input.DataReserva
            };

            await _context.Reservas.AddAsync(reserva);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Reserva cadastrada com sucesso!";
            return RedirectToPage("/Index");
        }

        private async Task CarregarOpcoesAsync()
        {
            var clientes = await _context.Clientes.OrderBy(c => c.Nome).ToListAsync();
            var pacotes = await _context.PacotesTuristicos
                .Where(p => p.DataInicio >= DateTime.Today && p.DeletedAt == null)
                .OrderBy(p => p.Titulo)
                .ToListAsync();

            ClienteOptions = new SelectList(clientes, "Id", "Nome");
            PacoteOptions = new SelectList(pacotes, "Id", "Titulo");
        }
    }
}