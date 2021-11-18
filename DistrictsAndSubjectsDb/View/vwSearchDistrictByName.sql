create view [dbo].[vwSearchDistrictByName]
	as select * from [dbo].[District]
	where [Name] like N'Юж%'
