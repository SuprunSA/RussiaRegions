create table [dbo].[Subject]
(
	[Code] bigint NOT NULL primary key,
	[Name] nvarchar(50) NOT NULL,
	[AdminCenterName] nvarchar(50) NOT NULL,
	[Population] real NOT NULL,
	[Square] real NOT NULL,
	[District] bigint NOT NULL 
	foreign key references District(Code) 
	on delete cascade
	on update cascade
)
