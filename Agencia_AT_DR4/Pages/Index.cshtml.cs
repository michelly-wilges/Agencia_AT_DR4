using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Agencia_AT_DR4.Pages
{
    public class ExerciseMenuItem
    {
        public int Number { get; set; }
        public string Title { get; set; } = "";
        public string Page { get; set; } = "";
        public bool Ativo { get; set; } = false;
    }

    public class IndexModel : PageModel
    {
        public List<ExerciseMenuItem> Exercises { get; } = new()
        {
            new() { Number = 1,  Title = "Delegate para Cálculo de Desconto",       Page = "/Delegates/Desconto",          Ativo = true },
            new() { Number = 2,  Title = "Multicast Delegate — Log de Operações",   Page = "/Delegates/Log",               Ativo = true },
            new() { Number = 3,  Title = "Func com Lambda — Simulação de Reserva",  Page = "/Delegates/SimularReserva",    Ativo = true },
            new() { Number = 4,  Title = "Evento de Alerta — Limite de Capacidade", Page = "/Delegates/AlertaCapacidade",  Ativo = true },
            new() { Number = 5,  Title = "Cadastro de Reserva com Validação",       Page = "/ReservaManager/CreateReserva",Ativo = true },
            new() { Number = 6,  Title = "Cadastro de Pacote Turístico",            Page = "/PacoteManager/CreatePacote",  Ativo = true },
            new() { Number = 7,  Title = "Detalhes via Roteamento na URL",          Page = "/PacoteManager/PacoteDetails", Ativo = true },
            new() { Number = 8,  Title = "Sistema de Notas — Leitura e Escrita",    Page = "/NotasManager/ViewNotes",      Ativo = true },
            new() { Number = 9,  Title = "Criação do DbContext",                    Page = "/Index",                       Ativo = true },
            new() { Number = 10, Title = "Modelagem e Relacionamentos EF Core",     Page = "/Index",                       Ativo = true },
            new() { Number = 11, Title = "CRUD com Scaffolding",                    Page = "/Index",                       Ativo = true },
            new() { Number = 12, Title = "Exclusão Lógica e Autenticação",          Page = "/Index",                       Ativo = true },
        };
    }
}