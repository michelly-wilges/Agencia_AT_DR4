# Agencia_AT_DR4 — Assessment

Sistema de gerenciamento para uma agência de turismo desenvolvido em ASP.NET Core Razor Pages com Entity Framework Core e SQLite.

## 🔐 Acesso ao Sistema

| Campo   | Valor      |
|---------|------------|
| Usuário | `admin`    |
| Senha   | `admin123` |

> As credenciais estão definidas diretamente no código, sem uso de banco de dados, conforme especificado no enunciado.

## 🚀 Como Executar

1. Abra a solução `Agencia_AT_DR4.sln` no Visual Studio
2. Aguarde a restauração dos pacotes NuGet
3. Execute com `Ctrl+F5`
4. Acesse `https://localhost:{porta}` no navegador

## 🗄️ Banco de Dados

- **Provedor:** SQLite
- **Arquivo:** `agencia.db`
- **ORM:** Entity Framework Core 8.0.0

## 📋 Exercícios Implementados

| # | Descrição | Tecnologia |
|---|-----------|------------|
| 1 | Delegate para cálculo de desconto | `delegate` personalizado |
| 2 | Multicast Delegate para log | `Action<string>` com `+=` |
| 3 | Func com lambda — simulação de reserva | `Func<int, decimal, decimal>` |
| 4 | Evento de alerta — limite de capacidade | `event EventHandler` |
| 5 | Cadastro de reserva com validação | Razor Pages + ModelState |
| 6 | Cadastro de pacote turístico | Razor Pages + EF Core |
| 7 | Detalhes via roteamento na URL | `@page "{id:int}"` |
| 8 | Sistema de notas — leitura e escrita | `System.IO` |
| 9 | Criação do DbContext | EF Core + SQLite |
| 10 | Modelagem e relacionamentos | Fluent API + navegação |
| 11 | CRUD com Scaffolding | Visual Studio Scaffolding |
| 12 | Exclusão lógica e autenticação | Soft Delete + Cookie Auth |

## 🏗️ Estrutura do Projeto
```
Agencia_AT_DR4/
├── Data/
│   └── AgenciaContext.cs
├── Models/
│   ├── Cliente.cs
│   ├── CidadeDestino.cs
│   ├── PaisDestino.cs
│   ├── PacoteTuristico.cs
│   └── Reserva.cs
├── Pages/
│   ├── Delegates/
│   │   ├── Desconto.cshtml
│   │   ├── Log.cshtml
│   │   ├── SimularReserva.cshtml
│   │   └── AlertaCapacidade.cshtml
│   ├── PacoteManager/
│   │   ├── CreatePacote.cshtml
│   │   └── PacoteDetails.cshtml
│   ├── PacotesTuristicos/     ← gerado por Scaffolding
│   │   ├── Create.cshtml
│   │   ├── Delete.cshtml
│   │   ├── Details.cshtml
│   │   ├── Edit.cshtml
│   │   └── Index.cshtml
│   ├── ReservaManager/
│   │   └── CreateReserva.cshtml
│   ├── NotasManager/
│   │   └── ViewNotes.cshtml
│   ├── Login.cshtml
│   ├── Logout.cshtml
│   └── Index.cshtml
└── Program.cs
```

## 🛠️ Tecnologias

- ASP.NET Core 8 — Razor Pages
- Entity Framework Core 8.0.0
- SQLite
- Bootstrap 5
