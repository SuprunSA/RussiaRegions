create view [dbo].[vwSearchSubjectByName]
	as select * from [dbo].[Subject]
	where [Name] = N'Ульяновская область'