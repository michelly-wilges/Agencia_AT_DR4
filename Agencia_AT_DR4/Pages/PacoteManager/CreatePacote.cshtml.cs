using Agencia_AT_DR4.Data;
using Agencia_AT_DR4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Agencia_AT_DR4.Pages.PacoteManager
{
    public class CreatePacoteModel : PageModel
    {
        private readonly AgenciaContext _context;

        public CreatePacoteModel(AgenciaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required(ErrorMessage = "O título é obrigatório.")]
            [MinLength(3, ErrorMessage = "O título deve ter pelo menos 3 caracteres.")]
            public string Titulo { get; set; } = "";

            [Required(ErrorMessage = "A data de início é obrigatória.")]
            public DateTime DataInicio { get; set; } = DateTime.Today.AddDays(1);

            [Required(ErrorMessage = "A capacidade máxima é obrigatória.")]
            [Range(1, 500, ErrorMessage = "A capacidade deve ser entre 1 e 500.")]
            public int CapacidadeMaxima { get; set; }

            [Required(ErrorMessage = "O preço é obrigatório.")]
            [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
            public decimal Preco { get; set; }
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            if (Input.DataInicio.Date < DateTime.Today)
            {
                ModelState.AddModelError("", "A data de início deve ser futura.");
                return Page();
            }

            var pacote = new PacoteTuristico
            {
                Titulo = Input.Titulo,
                DataInicio = Input.DataInicio,
                CapacidadeMaxima = Input.CapacidadeMaxima,
                Preco = Input.Preco
            };

            await _context.PacotesTuristicos.AddAsync(pacote);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Pacote cadastrado com sucesso!";
            return RedirectToPage("/Index");
        }
    }
}