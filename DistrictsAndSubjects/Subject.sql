create table [dbo].[Subject]
(
	[Code] bigint NOT NULL primary key,
	[Name] nvarchar(50) NOT NULL,
	[AdminCenterName] nvarchar(50) NOT NULL,
	[Population] decimal(15,3) NOT NULL,
	[Square] decimal(15,2) NOT NULL,
	[PopulationDencity] decimal(15,3) NOT NULL,
	[District] bigint NOT NULL 
	foreign key references District(Code) 
	on delete cascade
	on update cascade
)
