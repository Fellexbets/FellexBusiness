<!-- omit in toc -->
# M2 - Conceitos e estrutura de bases de dados 

**Objetivos**

Esta unidade de formação visa capacitar os formandos com conhecimentos introdutórios de UML, bases de dados suportadas por modelos relacionais e linguagem SQL. No final desta unidade, os formandos deverão ser capazes de estruturar informação com base em diagramas de classes e no modelo relacional, criar bases de dados, disponibilizar e manipular registos e criar interrogações a partir da linguagem SQL.

---

**Avaliação:**

* Projecto M2 (individual)
  * Versões de [15](enunciado_projecto_base.md) e [20](enunciado_projecto_avancado.md) valores
  * ~Entregar até **Segunda-feira, 14 de Fevereiro, 09:00**~
  * Apresentação de 15 minutos onde o formando demonstre o processo de tomada de decisão durante o projeto, a base de dados e a demonstração das consultas (incluindo a inserção e eliminação de registos durante a apresentação)
  * [Correcção](Casos%20de%20estudo%20com%20soluções.pdf) dos projectos e casos de estudo

* [Exercícios](exer.md)
  * ~Entregar até **Segunda-feira, 14 de Fevereiro, 09:00**~
  * [Soluções](demos/solutions.ipynb) - usar "Azure Data Studio" para abrir o ficheiro

* [Mini-teste](https://www.w3schools.com/sql/sql_quiz.asp)
  * ~Feito em sala na **Quinta-feira, 10 de Fevereiro, 14:00**~

* Entrega através de publicação no GitHub
  * Criar pasta "Entregar" e uma pasta para cada entrega
  * Nome das pastas no formato "NomeApelido-M2-Data", e.g., "JoaoSilva-M2-20220214"

---

**Semanas:**
- [24 a 28 de Janeiro de 2022](#24-a-28-de-janeiro-de-2022)
- [31 de Janeiro a 4 de Fevereiro de 2022](#31-de-janeiro-a-4-de-fevereiro-de-2022)
- [7 a 11 de Fevereiro de 2022](#7-a-11-de-fevereiro-de-2022)

---

## 24 a 28 de Janeiro de 2022
| Sessão | Dia    | Sumário                                                                                                           | Recursos                                                                                                                                                | Trabalho autónomo                                                                                                                 |
|:------:|--------|-------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------|
|  #11   | 24.JAN | Desenvolvimento orientado a modelos. A linguagem *Unified Modelling Language* (UML).                              | - [MDD](../presentations/M2%20-%20UML.pdf)                                                                                                              |                                                                                                                                   |
|  #12   | 25.JAN | Teste. Natureza dos diagramas UML: Estrutura e Comportamento, estáticos e dinâmicos.                              | - Enunciado do teste no [M1](../M1-Software%20development%20principles/)                                                                                                                                                        |                                                                                                                                   |
|  #13   | 26.JAN | Modelo de Kruchten - "4+1".                                                                                       |                                                                                                                                                         |                                                                                                                                   |
|  #14   | 27.JAN | Diagrama de classes: objecto, classe, relação e generalização.                                                    | - Exemplificação no projecto [StarterStore](../M3-.NET%20Framework%20and%20web%20development/M3_-_Projecto.pdf) e respectiva [abordagem](demos/starterstore/StarterStore-hoa-intro.pdf)                                                                                                                                                        |                                                                                                                                   |
|  #15   | 28.JAN | Informação numa empresa: utilidade e disponibilidade. Introdução aos Sistemas de Gestão de Bases de Dados (SGBD). | - [SGBD](../presentations/M2%20-%20DBMS.pdf) <br/> - SQLite3 version of Microsoft's [Northwind](https://github.com/jpwhite3/northwind-SQLite3) Database | - Reflectir sobre [utilizações apropriadas](https://www.sqlite.org/whentouse.html) do [SQLite](https://www.sqlite.org/index.html) |


## 31 de Janeiro a 4 de Fevereiro de 2022
| Sessão | Dia    | Sumário                                                                                                                                                                    | Recursos                                                                                                                                                                                                                                                                                                                                                                                                    | Trabalho autónomo |
|:------:|--------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------|
|  #16   | 31.JAN | Continuação do estudo de SGBD.                                                                                                                                             | - [DBBrowser for SQLite](https://sqlitebrowser.org/dl/) <br/> - [SQL Server 2019 Developer Edition](https://go.microsoft.com/fwlink/?linkid=866662) <br/> - [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)                                                                                               |                   |
|  #17   | 01.FEV | Continuação do estudo de SGBD: vantagens e desvantagens. Bases de dados relacionais e não-relacionais.                                                                     | - [DB-Engines Ranking - Trend Popularity](https://db-engines.com/en/ranking_trend) <br/>                                                                                                                                                                                                                                                                                                                    |                   |
|  #18   | 02.FEV | Abordagem sumária ao modelo entidade-associção e relacional. Aplicação prática no contexto de um problema de gestão de stocks.                                             | - [Northwind SQL Server](demos/sql_server/Northwind2016.bak) and [Scripts](demos/sql_server/Northwind_SQL_Server.sql) <br/> - [Northwind SQLite](demos/sqlite/Northwind_large.sqlite) and [Scripts](demos/sqlite/Northwind_SQLite.sql) <br/> - [Clientes](demos/customers/) e [Compras](demos/orders/) <br/> - Modelo de dados [Northwind Traders](demos/Northwind_ERD.png)                                 |                   |
|  #19   | 03.FEV | Restrições no modelo relacional: chaves primárias, chaves estrangeiras e chaves candidatas. Backup e recuperação de informação. Introdução às bases de dados relacionais usando SQLite e Microsoft SQL Server. | - [Primary and Foreign Key Constraints](https://docs.microsoft.com/en-us/sql/relational-databases/tables/primary-and-foreign-key-constraints?view=sql-server-ver15)                                                                                                                                                                                                                                         | Leitura do livro sobre UML - [ch1](../M3-.NET%20Framework%20and%20web%20development/bibliografia/Learning%20UML%202.0%20-%201.pdf) e [ch4-5](../M3-.NET%20Framework%20and%20web%20development/bibliografia/Learning%20UML%202.0%20-%204-5.pdf) <br/> - [Casos de estudo](Casos%20de%20estudo.pdf) <br/> - [UML Class Diagram Tutorial](https://www.lucidchart.com/pages/uml-class-diagram/#discovery__top) |
|  #20   | 04.FEV | Linguagem DDL. Linguagem DML e Queries. Exercícios.                                                                                    | - [Back Up and Restore of SQL Server Databases](https://docs.microsoft.com/en-us/sql/relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases?view=sql-server-ver15) <br/> - [Transact-SQL Data Definition Language (DDL) Statements](https://docs.microsoft.com/en-us/sql/relational-databases/blob/filestream-ddl-functions-stored-procedures-and-views?view=sql-server-ver15#ddl) | - [Exercícios](exer.md) |

## 7 a 11 de Fevereiro de 2022
| Sessão | Dia    | Sumário                                                                                                                     | Recursos                                                                                                                                                                                                                                             | Trabalho autónomo |
|:------:|--------|-----------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------|
|  #21   | 07.FEV | Continuação do estudo de Queries SQL: SELECT, INSERT, UPDATE, DELETE. Funções de Agregação e junção de tabelas. Exercícios. | - [Queries](https://docs.microsoft.com/en-us/sql/t-sql/queries/queries?view=sql-server-ver15)                                                                                                                                                        | - [Exercícios](exer.md) <br/> - [Casos de estudo](Casos%20de%20estudo.pdf) |
|  #22   | 08.FEV | Procedimentos armazenados: utilidade e aplicação. Exercícios.                                                               | - [Stored Procedures](https://docs.microsoft.com/en-us/sql/relational-databases/stored-procedures/stored-procedures-database-engine?view=sql-server-ver15)                                                                                           | - [Exercícios](exer.md) <br/> - [Casos de estudo](Casos%20de%20estudo.pdf) |
|  #23   | 09.FEV | Índices e vistas. Exercícios.                                                                                               | - [Views](https://docs.microsoft.com/en-us/sql/relational-databases/views/views?view=sql-server-ver15) <br/> - [Indexes](https://docs.microsoft.com/en-us/sql/relational-databases/indexes/indexes?view=sql-server-ver15)                            | - [Exercícios](exer.md) <br/> - [Casos de estudo](Casos%20de%20estudo.pdf) |
|  #24   | 10.FEV | Teste. [Correcção do teste](https://www.w3schools.com/sql/sql_quiz.asp). Triggers e Concorrência. Abordagem sumária aos mecanismos de replicação de dados.        | - Solução para os casos de estudo [1](sol_ce_mp3.JPEG) e [2](sol_ce_trad.JPEG) <br/> - [DDL Trigger](https://docs.microsoft.com/en-us/sql/relational-databases/triggers/ddl-triggers?view=sql-server-ver15) <br/> - [DML Triggers](https://docs.microsoft.com/en-us/sql/relational-databases/triggers/dml-triggers?view=sql-server-ver15) | - [Exercícios](exer.md) <br/> - [Casos de estudo](Casos%20de%20estudo.pdf) |
|  #25   | 11.FEV | Conclusão do estudo de SGBD. Breve introdução ao .NET Core.                                                                 | - [.NET Core](../presentations/M3%20-%20Dot_Net_Core.pdf)                                                                                                                                                                                            | - [Exercícios](exer.md) <br/> - [Casos de estudo](Casos%20de%20estudo.pdf) |