MedReminder
Sistema web para gerenciamento de lembretes de medicamentos, desenvolvido com ASP.NET Core 9 Web API e Blazor WebAssembly.
🔗 Demo ao vivo: https://med-lembrete-de-remedios.vercel.app
Sobre o projeto
MedReminder é uma aplicação web que permite o controle completo dos medicamentos de uso contínuo ou temporário. Cada usuário pode cadastrar seus remédios, definir horários de tomada, marcar doses como tomadas e acompanhar o histórico de aderência ao tratamento, com sistema de autenticação e isolamento de dados.
O projeto é dividido em duas partes: uma Web API em ASP.NET Core 9 que expõe os endpoints e cuida da persistência, e um frontend em Blazor WebAssembly que consome a API e roda inteiramente no navegador.
Como testar
Acesse https://med-lembrete-de-remedios.vercel.app e você pode:

Criar uma conta — cadastre-se com email e senha para ter sua própria área isolada
Cadastrar seus medicamentos — informe nome, dosagem, horários e duração do tratamento
Marcar doses — confirme cada dose tomada e acompanhe o histórico

A API está hospedada no plano gratuito do Render. A primeira visita pode levar até 30 segundos para carregar (cold start).
Funcionalidades

Cadastro e login de usuários (ASP.NET Identity)
CRUD completo de medicamentos (criar, listar, editar, excluir)
Configuração de horários de tomada para cada medicamento
Marcação de doses como tomadas, com registro de data e hora
Histórico de aderência ao tratamento
Notificações/lembretes nos horários configurados(""" PARA FUNCIONAR A APLICACAO DEVE ESTAR ABERTA!!!""")
Isolamento de dados: cada usuário vê apenas seus próprios medicamentos
Mensagens de feedback ao usuário (sucesso/erro)
Interface responsiva

Tecnologias

ASP.NET Core 9 Web API — backend e endpoints REST (Minimal APIs)
Blazor WebAssembly — frontend SPA rodando no navegador
Entity Framework Core 9 — ORM
ASP.NET Identity — autenticação e autorização
PostgreSQL (Neon) — banco de dados na nuvem
Docker — containerização do backend
GitHub Actions — CI/CD com build e deploy automatizados
Render — hospedagem da Web API
Vercel — hospedagem do frontend Blazor WASM
GitHub — versionamento


Deploy
O deploy é totalmente automatizado via GitHub Actions. A cada push na branch main, o workflow deploy.yml dispara dois jobs em paralelo:

Build e Deploy API (Render) — faz build do backend, publica a imagem Docker e aciona o deploy no Render. As migrations do EF Core são aplicadas automaticamente no startup da aplicação.
Build e Deploy Frontend (Vercel) — faz build do projeto Blazor WebAssembly e publica os arquivos estáticos na Vercel.

Autor
Ariel Mariussi

Portfolio: https://portfolio-ariel-cyan.vercel.app/
GitHub: @ArielMariussi
Email: arielmariussi@gmail.com
