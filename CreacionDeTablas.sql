
-- Crear dos Compañías desde la API, Luego insertar los Usuarios.

CREATE TABLE Companies
(
  Id      int not null identity,
  Name    varchar(150) null,
  Address varchar(250) null,
  Country varchar(50)  null
)
GO

ALTER TABLE Companies
  ADD CONSTRAINT PK_Companies PRIMARY KEY CLUSTERED (Id)
GO

create table Employees
(
  Id        int not null identity,
  Name      varchar(300) not null,
  Age       int not null,
  Position  varchar(250) not null,
  CompanyId int constraint FK_Employee_Company REFERENCES Companies(Id)
)
GO

CREATE OR ALTER PROCEDURE [dbo].[ShowCompanyForProvidedEmployeeId] @Id int
AS
SELECT c.Id,
       c.Name,
       c.Address,
       c.Country
FROM Companies c
 JOIN Employees e ON c.Id = e.CompanyId
Where e.Id = @Id
GO


insert into Employees
(Name, Age, Position, CompanyId)
values('Juan Prez', 22, 'Junior', 1),
('Jaime Auger', 30, 'Data Scientist', 1),
('Hugo C', 40, 'Ayudante', 2),
('Pedro Riquelme', 33, 'Desarrollador', 2),
('Carlos Crisóstomo', 45, 'GG', 2)