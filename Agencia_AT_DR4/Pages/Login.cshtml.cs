using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Agencia_AT_DR4.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Usuário obrigatório.")]
        public string Usuario { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "Senha obrigatória.")]
        public string Senha { get; set; } = "";

        public string? Erro { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            if (Usuario == "admin" && Senha == "admin123")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Usuario)
                };

                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("CookieAuth", principal);

                return RedirectToPage("/Index");
            }

            Erro = "Usuário ou senha inválidos.";
            return Page();
        }
    }
}