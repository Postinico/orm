# ORM Entity Framework Core
ORM é uma ferramenta para armazenar dados de objetos para o banco de dados relacinal de forma automática.
Mundo OOP(classes e objetos) x Mundo relacional(Banco de dados e tabelas)


#LINKS
https://github.com/dotnet/efcore


# 1 - Code First
Ciar o bancode dados e tabelas usando a migração com base nas convenções e configurações fornecidas nas classes do seu Dominío.
Essa abordagem é útil no DDD(Domain driven Design).

# 2 - Database First
Cria as classes de dominío e contexto com base em um banco de dados existente usando os comandos do EF Core.
Com suporte limitado no EF Core; não suporta designer visual ou assistente.

# Migrations do EF core
As migrações do ef core oferecem uma maneira de aplicar alterações de forma incremental ao esquema do banco de dados para mantê-lo em sincronia com seu modelo do EF Core enquanto preserva os dados existentes no banco de dados.

Requesito
- intall-package Microsoft.EntityFrameworkCore.Tools

Commandos
add-migration inicial
remove-migration
script-migration
update-database
get-help EntityFrameworkCore


# dbContext 
é uma combinação dos padrões Repository e Unit of Work
- Gerencia a conexão com banco de dados
- realiza operações com dados, consultas e persistência (CRUD)
- gerencia a mudança de estado das entidades
- criar o modelo conceitual com base na configuração e convenção
- mapea os resultados das consultas SQL para as instância das entidades
- fornece um cache para objetos
- Gerencia a transação de persistência

* Principais métodos da classe 
    Add - Adiciona uma nova entidade ao contexto
    AddRange - Adiciona nova coleção de entidades
    Attch - Anexa uma nova entidade no contexto
    Entry - 
    Find - Encontra a entidade do contexto
    Remove - Remove a entidade do contexto
    SaveChanges - Persiste as informações no DB
    Set Cria um DbSet<t>
    Update - Anexa uma entidade desconectada com estado Modifed