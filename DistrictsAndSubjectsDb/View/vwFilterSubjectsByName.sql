create view [dbo].[vwFilterSubjectsByName]
	as select * from [dbo].[Subject]
	where [Name] like N'%область'
