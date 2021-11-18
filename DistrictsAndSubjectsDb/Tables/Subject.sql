create table [dbo].[Subject]
(
	[Code] bigint NOT NULL primary key,
	[Name] nvarchar(50) NOT NULL,
	[AdminCenterName] nvarchar(50) NOT NULL,
	[Population] float(53) NOT NULL,
	[Square] float(53) NOT NULL,
	[District] bigint NOT NULL 
	foreign key references District(Code) 
	on delete cascade
	on update cascade
)
