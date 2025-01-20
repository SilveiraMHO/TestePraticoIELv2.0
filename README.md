# CadastroIEL
TESTE PRÁTICO - Solução responsável por manter o cadastro de estudantes do IEL.

## Informações técnicas:
Este é um projeto criado no Visual Studio 2022 Community Edition com as seguintes tecnologias:
- .NET 8;
- ASP.NET Core MCV;
- Entity Framework ORM;
- SQL-Server 2022 Express.

O projeto também adota as seguintes bibliotecas:
- Bootstrap v5.1.0 (Nativo): Responsável pela estilização do site.
- toastr.js v2.1.4 (Client-Side Library): Responsável por gerar notificações.
- Fluent Validation v11.11.0 (NugetPackages): Utilizada para criar regras de validação para classes da View Model.
- Entity Framework Proxies v9.0.1 (NugetPackages): Utilizada para trabalhar com carregamento de dados do tipo "Lazy Loading".

Como mencionado, o projeto está todo mapeado e configurado com o ORM EF, com isso, existe a possibilidade de se trabalhar com CodeFirst, gerando a base de dados por Migration.
Para isso é necessário baixar as bibliotecas necessárias.

Para executar o projeto é necessário criar a base de dados e suas tabelas através do arquivo "Create_CadWebDb.sql" e para popular as tabelas basta executar o script do arquivo "Inserts_CadWebDb".

Por motivo de segurança, os arquivos "appsettings.json" e "appsettings.Development.json" não constam no repositório.